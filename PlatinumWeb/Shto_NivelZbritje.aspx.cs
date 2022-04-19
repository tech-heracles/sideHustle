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
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_NivelZbritje : MyPageBase
    {
        private const string prefixMesazhNjejes = "Niveli me Kod: ";
        private const string prefixMesazhShumes = "Niveli me Kod: ";
        private const string suffixMesazhNjejesGabimi = " eshte e lidhur dhe nuk mund te fshihet";
        private const string suffixMesazhShumesGabimi = " jane te lidhur dhe nuk mund te fshihen";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje nivel zbritje!";
        public static int idNdermVit = -1;
        private int idgjuha, idPerdoruesi, idviti, idNdermarrje;
        private string komponente = "Shto_NivelZbritje.aspx";
        private string guidString;
        //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

        protected void Page_Init(object sender, EventArgs e)
        {//perdoret per te vene nr automatik te dokumentit            
            int idregj = DbCore.DbAdmin.clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CNZ");
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

            idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);

            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmrateTabeve(rm, ci);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, ci, idgjuha);
                ASPxPageControl1.ActiveTabIndex = 0;
                mbushGridNiveleshNgaDB();
                
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 420, rm, ci);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvNivelZbritje", gvNivelZbritje, cmbKonfigurimi.Text.Split(';')[0], 420.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridNiveleshNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 420, rm, ci);
            }
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvNivelZbritje", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            GridUtil.konfigGrideListeEMadhePaTheme(gvNivelZbritje, "IdNivelZbritje");
            GridUtil.EmrateButonaveMbiGride(gvNivelZbritje);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", ci);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("MenuItemNivelZbritje", ci);
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);

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
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idgjuha, "gvNivelZbritje ", komponente, "FilterDefault", gvNivelZbritje.FilterExpression, gvNivelZbritje, "KodNivelZbritje", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvNivelZbritje, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 420, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvNivelZbritje ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

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
        protected void gvNivelZbritje_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvNivelZbritje.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                // check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvNivelZbritje.Settings.ShowFilterRow = true;
                gvNivelZbritje.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvNivelZbritje.Settings.ShowFilterRowMenu = true;
                gvNivelZbritje.Columns.Add(check);

                gvNivelZbritje.KeyFieldName = "IdNivelZbritje";
                gvNivelZbritje.SettingsBehavior.AllowSelectByRowClick = true;
                gvNivelZbritje.SettingsBehavior.AllowFocusedRow = true;
            } //this.gvNivelZbritje.Columns["#"].VisibleIndex = 0;
        }
        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ConfigureAspxComboBox.KonfiguroComboBoxPrioriteti(cmbPrioriteti);
            //DbCore.clsFunksione.percaktoTemplateComboMeEnableCallback(btneEmertimPrindi);
            ConfigureAspxComboBox.KonfiguroComboBoxNiveleZbritjePrind(IdNdermarrja, btneEmertimPrindi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneEmertimPrindi);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 26, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            //cmbKonfigurimi.SelectedIndex = -1;
            inicializoObjekte();
            //   mbushListeNiveleZbritjesh();
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
                gvNivelZbritje.DataSource = tmpObject;
                gvNivelZbritje.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridNiveleshNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbInventari.colNiveleZbritjesh.merrNiveleZbritjeshNdermarjeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvNivelZbritje.DataSource = dt;
            gvNivelZbritje.DataBind();
            dt.Dispose();
            DbCore.DbInventari.colNiveleZbritjesh colNivelePrind = new DbCore.DbInventari.colNiveleZbritjesh();
            colNivelePrind.mbushGjitheNiveleZbritjeshSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            foreach (DbCore.DbInventari.clsNivelZbritje n in colNivelePrind)
            {
                int prioriteti = 0;
                DbCore.DbInventari.colNiveleZbritjesh colNiveleZbrit = new DbCore.DbInventari.colNiveleZbritjesh();
                colNiveleZbrit.mbushNiveleZbritjeshSipasPrindit(n.IdNivelZbritje);
                if (colNiveleZbrit.Count > 0)
                {
                    prioriteti = colNiveleZbrit[0].PrioritetiNivelZbritje + 1;
                    if (prioriteti < n.PrioritetiNivelZbritje + 1)
                        prioriteti = n.PrioritetiNivelZbritje + 1;
                }
                else prioriteti = n.PrioritetiNivelZbritje + 1;

                hfPrioriteteMax.Value += "," + n.KodNivelZbritje + ":" + prioriteti;
            }
            if (hfPrioriteteMax.Value != "")
                hfPrioriteteMax.Value = hfPrioriteteMax.Value.Substring(1);
        }
        private void mbushListeNiveleZbritjesh()
        {//mbush griden me te dhena
            hfPrioriteteMax.Value = "";
            DbCore.DbInventari.colNiveleZbritjesh colNivele = new DbCore.DbInventari.colNiveleZbritjesh();

            colNivele.mbushGjitheNiveleZbritjeshSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            this.gvNivelZbritje.DataSource = colNivele;
            this.gvNivelZbritje.DataBind();
            DbCore.DbInventari.colNiveleZbritjesh colNivelePrind = new DbCore.DbInventari.colNiveleZbritjesh();
            colNivelePrind.mbushGjitheNiveleZbritjeshSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            foreach (DbCore.DbInventari.clsNivelZbritje n in colNivelePrind)
            {
                int prioriteti = 0;
                DbCore.DbInventari.colNiveleZbritjesh colNiveleZbrit = new DbCore.DbInventari.colNiveleZbritjesh();
                colNiveleZbrit.mbushNiveleZbritjeshSipasPrindit(n.IdNivelZbritje);
                if (colNiveleZbrit.Count > 0)
                {
                    prioriteti = colNiveleZbrit[0].PrioritetiNivelZbritje + 1;
                    if (prioriteti < n.PrioritetiNivelZbritje + 1)
                        prioriteti = n.PrioritetiNivelZbritje + 1;
                }
                else prioriteti = n.PrioritetiNivelZbritje + 1;

                hfPrioriteteMax.Value += "," + n.KodNivelZbritje + ":" + prioriteti;
            }
            if (hfPrioriteteMax.Value != "")
                hfPrioriteteMax.Value = hfPrioriteteMax.Value.Substring(1);
        }

        private void konfiguroGride(string kodKonfigurimi, int idKomponente, ResourceManager rm, CultureInfo ci)
        {//konfiguron griden
            KonfigurimComboGride.shtoPrioritet(gvNivelZbritje, rm, ci);
            KonfigurimComboGride.shtoPrindSipasNivelZbritje(gvNivelZbritje, idNdermarrje, Session, komponente, guidString);
            this.gvNivelZbritje.Columns["#"].VisibleIndex = 0;

        }
       
       //thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        protected void gvNivelZbritje_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvNivelZbritje.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvNivelZbritje.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";


            }
            // konfiguroVleraFillestare();
        }

        //sherben per te vendosur filtra tek header-i i grides (mund te ndryshohet me vone per tu
        //bere  me e konfigurueshme
        protected void gvNivelZbritje_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "EmerBanka")
            {
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue("Nga D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue("Nga H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue("Nga L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue("Nga P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue("Nga T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue("Nga X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNivelZbritje", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvNivelZbritje", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //   konfiguroVleraFillestare();
                hfStatusi.Value = "true";
                gvNivelZbritje.FilterExpression = String.Empty;
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNivelZbritje", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));

            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvNivelZbritje.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodNivelZbritje", gvNivelZbritje);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvNivelZbritje.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "KodNivelZbritje";
            //    filtri.DrejtimRenditje = true;
            //}
            //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvNivelZbritje", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
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
                rreshtat = gvNivelZbritje.GetSelectedFieldValues("IdNivelZbritje");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            // List<object> rreshtat = gvNivelZbritje.GetSelectedFieldValues("IdNivelZbritje");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            foreach (object id in rreshtat)
            {
                DbCore.DbInventari.clsNivelZbritje niveli = new DbCore.DbInventari.clsNivelZbritje();
                niveli.IdNivelZbritje = Convert.ToInt32(id);
                DbCore.DbInventari.clsNivelZbritje clsNivel = new DbCore.DbInventari.clsNivelZbritje();
                clsNivel.mbushNivelZbritje(niveli.IdNivelZbritje);

                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(clsNivel.IdKonfig);

                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(clsNivel.IdNivelZbritje.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(clsNivel.KodNivelZbritje);
                    continue;
                } clsNivel.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = clsNivel.fshi();
                if (clsNivel.IdNivelZbritje == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqNivelNgaGrida(clsNivel.IdNivelZbritje);
                    #endregion
                    TeFshire.Add(clsNivel.KodNivelZbritje);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TePaFshire), suffixMesazhNjejesGabimi);
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TePaFshire), suffixMesazhShumesGabimi);
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeFshire), suffixMesazhNjejesSuksesi);
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeFshire), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);


        }
        private void hiqNivelNgaGrida(int idnivel)
        {
            if (this.gvNivelZbritje.DataSource != null)
            {
                DataTable dt = (DataTable)gvNivelZbritje.DataSource;
                DataRow[] drs = dt.Select("IdNivelZbritje = " + idnivel);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 nivele zbritjesh me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvNivelZbritje.DataBind();
            }
            else mbushGridNiveleshNgaDB();
        }
        private void shtoNivelNeGrid(int idNdermarrje, int idnivel)
        {
            if (gvNivelZbritje.DataSource != null)
            {
                DataTable dt = (DataTable)gvNivelZbritje.DataSource;
                DataRow[] drs = dt.Select("IdNivelZbritje = " + idnivel);
                if (drs.Length > 0)
                    throw new Exception("GABIM: Niveli ekziston ne gride");
                DataRow newArtDr = DbCore.DbInventari.colNiveleZbritjesh.merrNivelZbritjeSipasNdermarjesDR(idNdermarrje, idnivel);
                dt.ImportRow(newArtDr);
            }
            else mbushGridNiveleshNgaDB();
        }
        private void modifikoNivelNeGrid(int idNdermarrje, int idnivel)
        {
            if (gvNivelZbritje.DataSource != null)
            {
                DataTable dt = (DataTable)gvNivelZbritje.DataSource;
                DataRow[] drs = dt.Select("IdNivelZbritje = " + idnivel);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 nivele cmimesh me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbInventari.colNiveleZbritjesh.merrNivelZbritjeSipasNdermarjesDR(idNdermarrje, idnivel);
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
                Page.Validate("entries"); ruajNivelZbritje();
            }
        }
        protected void gvNivelZbritje_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName != "KodNivelZbritje" && e.Column.FieldName != "PershkrimNivelZbritje")
            {
                try
                {
                    int vlera = Converter.ConvertToInt(e.Value);
                    if (vlera == 0)
                        e.Criteria = null;
                }
                catch (Exception err)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                }
            }
            //if (ImbUtil.ConvertToInt(e.Value)==0)
            //{
            //    e.Criteria = null;
            //}
        }
        private void ruajNivelZbritje()
        {//ben ruajtjen vetem me te dhenat e grides
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (Page.IsValid == false)
                return;
            else
            {
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (isValidNivel(idPerdoruesi))
                {
                    bool eshteShtim;
                    DbCore.DbInventari.clsNivelZbritje niveli = krijoNivel(idPerdoruesi);
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = niveli.ruajNivelZbritje(niveli.IdNivelZbritje, niveli.KodNivelZbritje, niveli.PershkrimNivelZbritje, niveli.IdPrindi, niveli.PrioritetiNivelZbritje, niveli.IdPerdoruesi, niveli.IdNdermarje, niveli.IdKonfig, niveli.IdStatusDok);
                        eshteShtim = true;
                    }
                    else
                    {
                        if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        eshteShtim = false;
                        DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                        konf.mbushKonfigAmbjSipasId(niveli.IdKonfig);
                        niveli.IdNivelZbritje = int.Parse(hfId.Value.ToString());
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(niveli.IdNivelZbritje.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = "Niveli i cmimit  eshte e lidhur";
                        }
                        else mesazh = niveli.modifikoNivelZbritje(niveli.IdNivelZbritje, niveli.KodNivelZbritje, niveli.PershkrimNivelZbritje, niveli.IdPrindi, niveli.PrioritetiNivelZbritje, niveli.IdPerdoruesi, niveli.IdNdermarje, niveli.IdKonfig, niveli.IdStatusDok);
                        dbRegjistrim.Dispose();
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                        hfStatusi.Value = "true";
                        hfPrindi.Value = "";
                        HiddenField1.Value = "";
                        hfPrioriteteMax.Value = "";
                        if (eshteShtim)
                            shtoNivelNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), niveli.IdNivelZbritje);
                        else //modifikim
                            modifikoNivelNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), niveli.IdNivelZbritje);
                        konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 420, rm, ci);

                        int idregj = DbCore.DbAdmin.clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CNC");
                        int idlloji = DbCore.DbAdmin.clsLlojKodi.ktheIDLlojKodi("Kod");
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                    ASPxPageControl1.ActiveTabIndex = 0;
                }
                else hfStatusi.Value = "false";
            }
        }
        private DbCore.DbInventari.clsNivelZbritje krijoNivel(int idPerdoruesi)
        {//krijon nje artikull sipas te dhenave te futura nga perdoruesi
            DbCore.DbInventari.clsNivelZbritje nivel = new DbCore.DbInventari.clsNivelZbritje();
            //DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            nivel.IdKonfig = konfig.IdKonfigAmbjente;

            //nivel.IdNderViti = idNdermVit;
            nivel.KodNivelZbritje = txtKodi.Text;
            nivel.PershkrimNivelZbritje = txtEmertimi.Text;
            if (btneEmertimPrindi.Text == "")
                nivel.IdPrindi = 0;
            else
                nivel.IdPrindi = DbCore.DbInventari.clsNivelZbritje.ktheIdNivelZbritjeSipasPershkrimit(btneEmertimPrindi.Text, idNdermarrje);
            //nivel.IdPrindi = dbInventari.merrNivelZbritjeSipasPershkrimit(btneEmertimPrindi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session))[0].IdNivelZbritje;

            nivel.PrioritetiNivelZbritje = int.Parse(cmbPrioriteti.Value.ToString());
            nivel.IdPerdoruesi = idPerdoruesi;
            nivel.IdStatusDok = 1;

            nivel.IdNdermarje = idNdermarrje;
            return nivel;
        }

        //kontrollon nese niveli ekziston
        private bool isValidNivel(int idPerdoruesi)
        {
            DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
            bool isValid = true;
            DbCore.clsMesazh kontrollNivel = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), FusheKontrolli.Kodi, false);
            if (!kontrollNivel.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrollNivel.PershkrimMesazhi, pnlMesazhi);
                return false;
            }
            kontrollNivel = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(DbCore.clsFunksione.ktheStringunPaHapesira(txtEmertimi.Text, false), FusheKontrolli.Pershkrimi, false);
            if (!kontrollNivel.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrollNivel.PershkrimMesazhi, pnlMesazhi);
                return false;
            }
            int id = int.Parse(hfId.Value.ToString());
            DbCore.DbInventari.clsNivelZbritje niveli = new DbCore.DbInventari.clsNivelZbritje();
            niveli.mbushNivelZbritje(id);
            if (niveli.IdPrindi == 0)
            {
                if (krijoNivel(idPerdoruesi).IdPrindi != 0 && dbInventari.kaBijNivelZbritje(id) && (hfShtimModifikim.Value == "modifikim"))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ky nivel ka nivele bij dhe nuk mund te kaloje si bij i nje niveli tjeter", pnlMesazhi);
                    isValid = false;

                }
                if (krijoNivel(idPerdoruesi).IdPrindi == id && (hfShtimModifikim.Value == "modifikim"))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Niveli nuk mund te jete bir i vetvetes", pnlMesazhi);
                    isValid = false;
                }
            } if (dbInventari.ekzistonNivelZbritje(txtKodi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
            {
                isValid = false;

                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje nivel cmimi me kete kod!Ju lutemi shenoni nje kod tjeter!", pnlMesazhi);
                mbushListeNiveleZbritjesh();
                return isValid;
            }
            return isValid;
        }


        protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.TabControlEventArgs e)
        {
        }


        protected void gvNivelZbritje_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvNivelZbritje.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNivelZbritje", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        gvNivelZbritje.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvNivelZbritje);
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
            gvNivelZbritje.Selection.UnselectAll();
        }

        protected void gvNivelZbritje_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvNivelZbritje.PageIndex;
            e.Properties["cpPageRow"] = gvNivelZbritje.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvNivelZbritje.VisibleRowCount;

        }

        protected void btneEmertimPrindi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneEmertimPrindi"))
                ConfigureAspxComboBox.KonfiguroComboBoxNiveleZbritjePrind(IdNdermarrja, btneEmertimPrindi);
        }
    }
}
