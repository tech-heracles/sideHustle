using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Types;
using DbCore.IMBUtils.Validation;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbAdmin;
using DbCore.CustomEntites;
using DbCore.DbInventari;

namespace PlatinumWeb
{
    public partial class Shto_NivelCmimi : MyPageBase
    {
        public static int idNdermVit = -1;
        //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        private int idviti, idgjuha, idNdermarrje, idPerdoruesi;
        private string komponente = "Shto_NivelCmimi.aspx";
        private string guidString;

        protected void Page_Init(object sender, EventArgs e)
        {//perdoret per te vene nr automatik te dokumentit
            int idregj = DbCore.DbAdmin.clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CNC");
            int idlloji = DbCore.DbAdmin.clsLlojKodi.ktheIDLlojKodi("Kod");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                vendosHfMePerkthime(rm, cultinf);
                EmrateTabeve(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, cultinf, idgjuha);
                ASPxPageControl1.ActiveTabIndex = 0;
                mbushGridNiveleshNgaDB();
                hfState.Set("colAutorizime", Newtonsoft.Json.JsonConvert.SerializeObject(new colAutorizimetKoka(IdPerdoruesi)));
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 410, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvNivelCmimi", gvNivelCmimi, cmbKonfigurimi.Text.Split(';')[0], 410.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridNiveleshNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 410, rm, cultinf);
                ConfigureAspxComboBox.mbushComboNiveleCmimeshPrind(idNdermarrje, btneEmertimPrindi);
            }
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvNivelCmimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            GridUtil.konfigGrideListeEMadhePaTheme(gvNivelCmimi, "IdNivelCmimi");
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            GridUtil.EmrateButonaveMbiGride(gvNivelCmimi);
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
            hfState.Set("msgZgjidhNivelinECmimitPrind", rm.GetString("msgZgjidhNivelinECmimitPrind", cultinf));
            hfState.Set("MsgBlerjeShitjeAutorizime", rm.GetString("MsgBlerjeShitjeAutorizime", cultinf));
            hfState.Set("msgZgjidhni1NivelCmimi", rm.GetString("msgZgjidhni1NivelCmimi", cultinf));
            hfState.Set("msgZgjidhniNivelinECmimit", rm.GetString("msgZgjidhniNivelinECmimit", cultinf));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("checkboxAdministrimiNivelCmimi", cultinf);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idgjuha, "gvNivelCmimi ", komponente, "FilterDefault", gvNivelCmimi.FilterExpression, gvNivelCmimi, "KodNivelCmimi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvNivelCmimi, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 410, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvNivelCmimi ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

        }
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

        }

        protected void gvNivelCmimi_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvNivelCmimi.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                //   check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvNivelCmimi.Settings.ShowFilterRow = true;
                gvNivelCmimi.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvNivelCmimi.Settings.ShowFilterRowMenu = true;
                gvNivelCmimi.Columns.Add(check);
                gvNivelCmimi.KeyFieldName = "IdNivelCmimi";
                gvNivelCmimi.SettingsBehavior.AllowSelectByRowClick = true;
                gvNivelCmimi.SettingsBehavior.AllowFocusedRow = true;
            } //this.gvNivelCmimi.Columns["#"].VisibleIndex = 0;
        }
        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            inicializoObjekte();
            mbushcomboLloj();
            mbushcomboBrutoNeto();
            mbushcomboDetajim();
            ConfigureAspxComboBox.KonfiguroComboBoxPrioriteti(cmbPrioriteti);
            ConfigureAspxComboBox.mbushComboMonedha(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, false, cmbMonedha);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneEmertimPrindi, btneCmimRetail);
            ConfigureAspxComboBox.mbushComboNiveleCmimeshPrind(idNdermarrje, btneEmertimPrindi);
           
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 25, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            //cmbKonfigurimi.SelectedIndex = -1;
        }

        private void mbushcomboLloj()
        {//mbush combon e llojit           
            cmbLloji.Items.Add("Cmim Shitje", 0);
            cmbLloji.Items.Add("Cmim Blerje", 1);
            cmbLloji.SelectedIndex = 0;
        }

        private void mbushcomboBrutoNeto()
        {//mbush combon e brutoneto        
            cmbBrutoNeto.Items.Add("", 0);
            cmbBrutoNeto.Items.Add("TVSH", 1);
        }
        private void mbushcomboDetajim()
        {
            cmbDetajim.Items.Add("", (int)Detajim.PaDetajim);
            cmbDetajim.Items.Add(DbCore.IMBUtils.Messages.MessagesResource.Messages["lblDetajim1"], (int)Detajim.Detajim1);
            cmbDetajim.Items.Add(DbCore.IMBUtils.Messages.MessagesResource.Messages["lblDetajim2"], (int)Detajim.Detajim2);
            cmbDetajim.SelectedIndex = 0;
        }

        private void inicializoObjekte()
        {
        }
        private void mbushGridNiveleshNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridNiveleshNgaDB();
            else
            {
                gvNivelCmimi.DataSource = tmpObject;
                gvNivelCmimi.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridNiveleshNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbInventari.colNiveleCmimesh.merrNiveleNdermarjeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvNivelCmimi.DataSource = dt;
            gvNivelCmimi.DataBind();
            dt.Dispose();
            DbCore.DbInventari.colNiveleCmimesh colNivelePrind = new DbCore.DbInventari.colNiveleCmimesh();
            colNivelePrind.mbushGjitheNiveleCmimeshPrindiSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            foreach (DbCore.DbInventari.clsNivelCmimi n in colNivelePrind)
            {
                int prioriteti = 0;
                DbCore.DbInventari.colNiveleCmimesh newcolNivCmim = new DbCore.DbInventari.colNiveleCmimesh();
                newcolNivCmim.mbushNivelSipasPrindit(n.IdNivelCmimi);
                if (newcolNivCmim.Count > 0)
                {
                    prioriteti = newcolNivCmim[0].PrioritetiNivelCmimi + 1;
                    if (prioriteti < n.PrioritetiNivelCmimi + 1)
                        prioriteti = n.PrioritetiNivelCmimi + 1;
                }
                else
                    prioriteti = n.PrioritetiNivelCmimi + 1;
                hfPrioriteteMax.Value += "," + n.KodNivelCmimi + ":" + prioriteti;
            }
            if (hfPrioriteteMax.Value != "")
                hfPrioriteteMax.Value = hfPrioriteteMax.Value.Substring(1);
        }
        private void mbushListeNiveleCmimesh()
        {//mbush griden me te dhena
            hfPrioriteteMax.Value = "";
            DbCore.DbInventari.colNiveleCmimesh colNivele = new DbCore.DbInventari.colNiveleCmimesh();
            colNivele.mbushGjitheNiveleCmimeshSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            this.gvNivelCmimi.DataSource = colNivele;
            this.gvNivelCmimi.DataBind();
            DbCore.DbInventari.colNiveleCmimesh colNivelePrind = new DbCore.DbInventari.colNiveleCmimesh();
            colNivelePrind.mbushGjitheNiveleCmimeshPrindiSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            foreach (DbCore.DbInventari.clsNivelCmimi n in colNivelePrind)
            {
                int prioriteti = 0;
                DbCore.DbInventari.colNiveleCmimesh newcolNivCmim = new DbCore.DbInventari.colNiveleCmimesh();
                newcolNivCmim.mbushNivelSipasPrindit(n.IdNivelCmimi);
                if (newcolNivCmim.Count > 0)
                {
                    prioriteti = newcolNivCmim[0].PrioritetiNivelCmimi + 1;
                    if (prioriteti < n.PrioritetiNivelCmimi + 1)
                        prioriteti = n.PrioritetiNivelCmimi + 1;
                }
                else
                    prioriteti = n.PrioritetiNivelCmimi + 1;
                hfPrioriteteMax.Value += "," + n.KodNivelCmimi + ":" + prioriteti;
            }
            if (hfPrioriteteMax.Value != "")
                hfPrioriteteMax.Value = hfPrioriteteMax.Value.Substring(1);

        }

        private void konfiguroGride(string kodKonfigurimi, int idKomponente, ResourceManager rm, CultureInfo ci)
        {//konfiguron griden

            KonfigurimComboGride.shtoPrioritet(gvNivelCmimi, rm, ci, "PrioritetiNivelCmimi");
            KonfigurimComboGride.shtoLlojNivelCmimi(gvNivelCmimi, rm, ci);
            KonfigurimComboGride.shtoPrindSipasNivelCmimi(gvNivelCmimi, idNdermarrje, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMonedhe(gvNivelCmimi, idNdermarrje, idPerdoruesi, Session, komponente, guidString);
            KonfigurimComboGride.shtoBrutoNeto(gvNivelCmimi, rm, ci);
            KonfigurimComboGride.shtoDetajim(gvNivelCmimi);

            shto_NivelCmimBaze();
            this.gvNivelCmimi.Columns["#"].VisibleIndex = 0;
        }

        private void shto_NivelCmimBaze()
        {
            gvNivelCmimi.Columns.Remove(gvNivelCmimi.Columns["NivelCmimiBaze"]);
            GridViewDataCheckColumn colnew = new GridViewDataCheckColumn();
            colnew.PropertiesCheckEdit.ValueChecked = "True";
            colnew.PropertiesCheckEdit.ValueUnchecked = "False";
            colnew.PropertiesCheckEdit.ValueGrayed = "";

            colnew.FieldName = "NivelCmimiBaze";
            gvNivelCmimi.Columns.Add(colnew);
        }

   
        //thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        protected void gvNivelCmimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvNivelCmimi.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvNivelCmimi.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.EmrateButonaveMbiGride(gvNivelCmimi);
            // konfiguroVleraFillestare();
        }

        //sherben per te vendosur filtra tek header-i i grides (mund te ndryshohet me vone per tu
        //bere  me e konfigurueshme
        protected void gvNivelCmimi_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "EmerBanka")
            {
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + " D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + " T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }
        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNivelCmimi", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvNivelCmimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //ASPxButton btnRuaj = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("Button1") as ASPxButton;
                //ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;

                //  btnRuaj.ClientEnabled = false;
                //  btnFshiFilter.ClientEnabled = false;
                //   konfiguroVleraFillestare();
                hfStatusi.Value = "true";
                gvNivelCmimi.FilterExpression = String.Empty;
            }
        }
        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNivelCmimi", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));

            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvNivelCmimi.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodNivelCmimi", gvNivelCmimi);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvNivelCmimi.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "KodNivelCmimi";
            //    filtri.DrejtimRenditje = true;
            //}
            //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);

            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvNivelCmimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            //mbushComboBoxFiltra();
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshin rreshtat e selektuar
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = gvNivelCmimi.GetSelectedFieldValues("IdNivelCmimi");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //List<object> rreshtat = gvNivelCmimi.GetSelectedFieldValues("IdNivelCmimi");
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNivelCmimiZgjidhni1NivelCmimi", ci), pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            string nivelbaze = "";
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            foreach (object id in rreshtat)
            {
                //DbCore.DbInventari.clsNivelCmimi niveli = new DbCore.DbInventari.clsNivelCmimi();
                //niveli.IdNivelCmimi = Convert.ToInt32(id);
                DbCore.DbInventari.clsNivelCmimi clsNivel = new DbCore.DbInventari.clsNivelCmimi(Convert.ToInt32(id));

                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(clsNivel.IdKonfig);
                if (clsNivel.NivelCmimiBaze)
                {
                    //TePaFshire.Add(clsNivel.KodNivelCmimi);
                    nivelbaze = clsNivel.KodNivelCmimi;
                    continue;
                }
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(clsNivel.IdNivelCmimi.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(clsNivel.KodNivelCmimi);
                    continue;
                }
                clsNivel.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = clsNivel.fshi();
                if (clsNivel.IdNivelCmimi == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqNivelNgaGrida(clsNivel.IdNivelCmimi, rm, ci);
                    #endregion
                    TeFshire.Add(clsNivel.KodNivelCmimi);
                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = ""; string mesazhInfoGabimNivelCmimiBaze = "";
            if (nivelbaze != "")
            {
                mesazhInfoGabimNivelCmimiBaze = String.Format("{0}{1}{2}", rm.GetString("msgNivelCmimiPrefixNjejes", ci), nivelbaze, rm.GetString("msgNivelCmimiNjejesGabimiNivelCmimiBaze", ci));
            }
            else mesazhInfoGabimNivelCmimiBaze = "";

            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgNivelCmimiPrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgCeljeMagazinatSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgNivelCmimiPrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgClejeArkaBankaSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgNivelCmimiPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("msgClejeArkaBankaSuffixNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgNivelCmimiPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                //mesazhInfoGabim = mesazhInfoGabimNivelCmimiBaze + mesazhInfoGabim + lidhesMesazhi + mesazhInfoSukses;
                mesazhInfoGabim += rm.GetString("regjMagLidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
            {
                if (mesazhInfoGabimNivelCmimiBaze != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimNivelCmimiBaze + " " + mesazhInfoGabim, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            }
            else
            {
                if (mesazhInfoGabimNivelCmimiBaze != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimNivelCmimiBaze + " " + mesazhInfoSukses, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            }
        }

        private void hiqNivelNgaGrida(int idnivel, ResourceManager rm, CultureInfo ci)
        {
            if (this.gvNivelCmimi.DataSource != null)
            {
                DataTable dt = (DataTable)gvNivelCmimi.DataSource;
                DataRow[] drs = dt.Select("IdNivelCmimi = " + idnivel);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgNivelCmimiNdodhen2MeTeNjejtenIdNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvNivelCmimi.DataBind();
            }
            else mbushGridNiveleshNgaDB();
        }

        private void shtoNivelNeGrid(int idNdermarrje, int idnivel, ResourceManager rm, CultureInfo ci)
        {
            if (gvNivelCmimi.DataSource != null)
            {
                DataTable dt = (DataTable)gvNivelCmimi.DataSource;
                DataRow[] drs = dt.Select("IdNivelCmimi = " + idnivel);
                if (drs.Length > 0)
                    throw new DbCore.MyException(rm.GetString("msgNivelCmimiNiveliEkzistonNeGride", ci));
                DataRow newArtDr = DbCore.DbInventari.colNiveleCmimesh.merrNivelCmimiSipasNdermarjesDR(idNdermarrje, idnivel);
                dt.ImportRow(newArtDr);
            }
            else mbushGridNiveleshNgaDB();
        }

        private void modifikoNivelNeGrid(int idNdermarrje, int idnivel, ResourceManager rm, CultureInfo ci)
        {
            if (gvNivelCmimi.DataSource != null)
            {
                DataTable dt = (DataTable)gvNivelCmimi.DataSource;
                DataRow[] drs = dt.Select("IdNivelCmimi = " + idnivel);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgNivelCmimiNdodhen2MeTeNjejtenIdNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbInventari.colNiveleCmimesh.merrNivelCmimiSipasNdermarjesDR(idNdermarrje, idnivel);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridNiveleshNgaDB();
        }

        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajNivelCmimi();
            }
        }

        protected void gvNivelCmimi_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdPrindi")
            {
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
            }
            else if (e.Column.FieldName != "KodNivelCmimi" && e.Column.FieldName != "PershkrimNivelCmimi" && e.Column.FieldName != "NivelCmimiBaze" && e.Column.FieldName != "NjesiTeVarura" && e.Column.FieldName != "TeVaruraNgaMonedha" && Converter.ConvertToInt(e.Value) == -1)
            {
                e.Criteria = null;
            }
        }

        private void ruajNivelCmimi()
        {//ben ruajtjen vetem me te dhenat e grides

            if (Page.IsValid == false)
                return;
            else
            {
                try
                {
                    int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                    ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    if (!isValidNivel(idPerdoruesi, rm, ci))
                    {
                        hfStatusi.Value = "false";
                        return;
                    }

                    bool eshteShtim;
                    DbCore.DbInventari.clsNivelCmimi niveli = krijoNivel(idPerdoruesi);
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = niveli.ruaj();
                        eshteShtim = true;
                    }
                    else
                    {
                        if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        eshteShtim = false;
                        DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                        konf.mbushKonfigAmbjSipasId(niveli.IdKonfig);
                        niveli.IdNivelCmimi = int.Parse(hfId.Value.ToString());
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(niveli.IdNivelCmimi.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgNivelCmimiEshteILidhur", ci);
                        }
                        else mesazh = niveli.modifiko();
                        dbRegjistrim.Dispose();
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgBlerjeShitjeRuajtjeMeSukses", ci), pnlMesazhi);
                        hfStatusi.Value = "true";
                        hfPrindi.Value = "";
                        HiddenField1.Value = "";
                        hfPrioriteteMax.Value = "";
                        if (eshteShtim)
                        {
                            shtoNivelNeGrid(idNdermarrje, niveli.IdNivelCmimi, rm, ci);
                            shtoNivelNeSesionPerCombo(niveli);
                        }
                        else //modifikim
                            modifikoNivelNeGrid(idNdermarrje, niveli.IdNivelCmimi, rm, ci);
                        konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 410, rm, ci);
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgBlerjeShitjeRuajtjeMeGabime", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                    ASPxPageControl1.ActiveTabIndex = 0;
                }
                catch (Exception ex)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                    hfStatusi.Value = "false";
                }
            }
        }

        private void shtoNivelNeSesionPerCombo(DbCore.DbInventari.clsNivelCmimi niveli)
        {
            var sessionKey = DbCore.IMBUtils.Cache.SessionKeyUtils.MerrSessionKeyPerCmb(komponente, "IdPrindi");
            DbCore.DbInventari.colNiveleCmimesh colNivelet = new DbCore.DbInventari.colNiveleCmimesh();
            colNivelet = DbCore.mySessionObjects.MerrNgaSession<colNiveleCmimesh>(Session, sessionKey, guidString);
            if (colNivelet.Find(x => x.IdNivelCmimi == niveli.IdNivelCmimi) != null)
                return;
            colNivelet.Add(niveli);
            DbCore.mySessionObjects.RuajNeSession(sessionKey, guidString, colNivelet);
        }

        private DbCore.DbInventari.clsNivelCmimi krijoNivel(int idPerdoruesi)
        {//krijon nje artikull sipas te dhenave te futura nga perdoruesi
            DbCore.DbInventari.clsNivelCmimi nivel = new DbCore.DbInventari.clsNivelCmimi();
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            nivel.IdKonfig = konfig.IdKonfigAmbjente;

            //nivel.IdNderViti = idNdermVit;
            nivel.KodNivelCmimi = DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true);
            nivel.PershkrimNivelCmimi = DbCore.clsFunksione.ktheStringunPaHapesira(txtEmertimi.Text, false);
            int idNivelPrindi = 0;
            if (!String.IsNullOrEmpty(btneEmertimPrindi.Text))               
            {
                idNivelPrindi = DbCore.DbInventari.clsNivelCmimi.ktheIdNivelCmimiNgaPershkrimi(btneEmertimPrindi.Text, idNdermarrje);
                if (idNivelPrindi == 0)
                    throw new DbCore.MyException($"Niveli i cmimit me pershkrim {btneEmertimPrindi.Text} nuk ekziston!");
            }
            nivel.IdPrindi = idNivelPrindi;
            nivel.LlojiNivelCmimi = int.Parse(cmbLloji.Value.ToString());
            nivel.IdMonedha = int.Parse(cmbMonedha.Value.ToString());
            if (cmbBrutoNeto.Text == "")
                nivel.BrutoNetoNivelCmimi = 0;
            else
                nivel.BrutoNetoNivelCmimi = int.Parse(cmbBrutoNeto.Value.ToString());
            nivel.Detajim = int.Parse(cmbDetajim.Value.ToString());
     
            nivel.PrioritetiNivelCmimi = int.Parse(cmbPrioriteti.Value.ToString());
            nivel.IdPerdoruesi = idPerdoruesi;
            nivel.IdNdermarje = idNdermarrje;
            nivel.IdStatusDok = 1;
            nivel.NjesiTeVarura = cbNjesiTeVarura.Checked;
            nivel.TeVaruraNgaMonedha = cbTeVaruraNgaMonedha.Checked;
            nivel.NivelCmimiBaze = cbNivelCmimBaze.Checked;
            int idCmimRetail = 0;
            if (!String.IsNullOrEmpty(btneCmimRetail.Text))
            {
                idCmimRetail = DbCore.DbInventari.clsNivelCmimi.ktheIdNivelCmimiNgaPershkrimi(btneCmimRetail.Text, idNdermarrje);
                if (idCmimRetail == 0)
                    throw new DbCore.MyException($"Niveli i cmimit me pershkrim {btneEmertimPrindi.Text} nuk ekziston!");
            }
            nivel.IdCmimRetail = idCmimRetail;
            DbCore.DbAdmin.colLidhjetAutorizim colLidhje;
            if (cmbAutorizimiHf.Value == "")
                colLidhje = new DbCore.DbAdmin.colLidhjetAutorizim();
            else
            {
                DbCore.DbAdmin.colLidhjetAutorizim colLidhjet = new DbCore.DbAdmin.colLidhjetAutorizim();
                string[] pars11 = cmbAutorizimiHf.Value.Split(',');
                for (int i = 0; i < pars11.Length; i++)
                {
                    DbCore.DbAdmin.clsLidhjeAutorizim lidhje = new DbCore.DbAdmin.clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars11[i]);
                    colLidhjet.Add(lidhje);
                }
                colLidhje = colLidhjet;
            }
            nivel.OColLidhjetAutorizim = colLidhje;
            return nivel;
        }

        //kontrollon nese niveli ekziston
        private bool isValidNivel(int idPerdoruesi, ResourceManager rm, CultureInfo ci)
        {
            int id = int.Parse(hfId.Value.ToString());
            DbCore.DbInventari.clsNivelCmimi clsNivel = new DbCore.DbInventari.clsNivelCmimi();
            if (hfShtimModifikim.Value == "modifikim" && id > 0)
                clsNivel = new DbCore.DbInventari.clsNivelCmimi(id);

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.clsMesazh mesazh = clsNivel.kontrollDetajimNjejteNdermarrje(hfShtimModifikim.Value == "modifikim" ? int.Parse(hfId.Value.ToString()) : 0, int.Parse(cmbDetajim.Value.ToString()));
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return false;
            }

            DbCore.clsMesazh kontrollKodNivelCmimi = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), FusheKontrolli.Kodi, false);
            if (!kontrollKodNivelCmimi.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrollKodNivelCmimi.PershkrimMesazhi, pnlMesazhi);
                return false;
            }
            DbCore.clsMesazh kontrolltxtEmertimi = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(DbCore.clsFunksione.ktheStringunPaHapesira(txtEmertimi.Text, false), FusheKontrolli.Pershkrimi, true);
            if (!kontrolltxtEmertimi.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrolltxtEmertimi.PershkrimMesazhi, pnlMesazhi);
                return false;
            }

            if (clsNivel.IdPrindi == 0)
            {
                DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
                if (!String.IsNullOrEmpty(btneEmertimPrindi.Text) && dbInventari.kaBijNivelCmimi(id) && (hfShtimModifikim.Value == "modifikim"))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNivelCmimiKaNiveleBijDheNukKalonSiBijINjeNiveliTjeter", ci), pnlMesazhi);
                    dbInventari.Dispose();
                    return false;
                }
                if ((hfShtimModifikim.Value == "modifikim") && !String.IsNullOrEmpty(btneEmertimPrindi.Text) && DbCore.DbInventari.clsNivelCmimi.ktheIdNivelCmimiNgaPershkrimi(btneEmertimPrindi.Text, idNdermarrje) == id)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNivelCmimiNukMundTeJeteBirIVetvetes", ci), pnlMesazhi);
                    dbInventari.Dispose();
                    return false;
                }
                if ((hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim") && dbInventari.ekzistonNivelCmimi(DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNivelCmimiEkziston1NCMeKeteKod", ci), pnlMesazhi);
                    // mbushListeNiveleCmimesh();
                    dbInventari.Dispose();
                    return false;
                }
                if ((hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim" || hfShtimModifikim.Value == "modifikim") && (dbInventari.ekzistonPershkrimiNivelCmimi(txtEmertimi.Text, idNdermarrje, clsNivel.IdNivelCmimi)))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNivelCmimiEkziston1NCMeKetePershkrim", ci) + txtEmertimi.Text, pnlMesazhi);
                    dbInventari.Dispose();
                    return false;
                }


                if (cbNivelCmimBaze.Checked && DbCore.DbInventari.clsNivelCmimi.kaNivelCmimiBaze(idNdermarrje, Convert.ToInt32(cmbLloji.Value)))
                {
                    if (hfShtimModifikim.Value == "modifikim")
                    {
                        DbCore.DbInventari.clsNivelCmimi nv = new DbCore.DbInventari.clsNivelCmimi();
                        nv.mbushNivelCmimiBaze(idNdermarrje, Convert.ToInt32(cmbLloji.Value));
                        if (int.Parse(hfId.Value.ToString()) != nv.IdNivelCmimi)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNivelCmimiEkziston1NCPerKeteNdermarrje", ci), pnlMesazhi);
                            return false;
                        }
                    }
                    else if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNivelCmimiEkziston1NCPerKeteNdermarrje", ci), pnlMesazhi);
                        return false;
                    }
                }
                if (cmbAutorizimiHf.Value != "")
                {
                    string[] pars1 = cmbAutorizimiHf.Value.Split(',');
                    for (int i = 0; i < pars1.Length; i++)
                    {
                        int idAutorizimKoka = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                        if (idAutorizimKoka == 0 || idAutorizimKoka == -1)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaNiveliAutorizimitNukEkziston", ci), pnlMesazhi);
                            return false;
                        }
                    }
                }
                dbInventari.Dispose();
            }
            return true;
        }

        protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.TabControlEventArgs e)
        {
        }


        protected void gvNivelCmimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvNivelCmimi.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNivelCmimi", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        gvNivelCmimi.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvNivelCmimi);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
            }
            else
            {
                idkomponente = e.Parameters;
            }
            gvNivelCmimi.Selection.UnselectAll();
        }

        protected void gvNivelCmimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvNivelCmimi.PageIndex;
            e.Properties["cpPageRow"] = gvNivelCmimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvNivelCmimi.VisibleRowCount;
        }

        protected void btneEmertimPrindi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {

            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneEmertimPrindi"))
                {
                    ConfigureAspxComboBox.mbushComboNiveleCmimeshPrind(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneEmertimPrindi);
                }
            }
        }
        
    }
}