using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing;
using System.Data;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using System.Linq;
using DbCore.IMBUtils.Types;
using DbCore.IMBUtils.Validation;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_Llogari : MyPageBase
    {
        public static int idNdermVit = -1;
        private int idgjuha, idviti, idNdermarrje, idPerdoruesi;
        string komponente = "Shto_Llogari.aspx";
        string guidString;

        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {  // Prevent caching, so can't be viewed offline
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
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
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.Messages.CurrentCultureInfo;
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmrateTabeve(rm, cultinf);
                vendosHfMePerkthime(rm, cultinf);
                EmratEKontrolleve(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                ASPxPageControl1.TabPages[3].ClientVisible = false;
                ASPxPageControl1.ActiveTabIndex = 0;
                konfiguroVleraFillestare(idPerdoruesi, rm, cultinf, idgjuha);
                hfState.Set("colAutorizime", Newtonsoft.Json.JsonConvert.SerializeObject(new DbCore.DbAdmin.colAutorizimetKoka(IdPerdoruesi)));
                hfMonedhaNder.Value = DbCore.DbAdmin.clsMonedha.ktheMonedhenENdermarrjes(idNdermarrje);
                grid_ListLlogarish.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idviti, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Llogari.aspx", rm, cultinf);
                mbushGridLlogarishNgaDB();
                
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_Llogari.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 107, idgjuha, idNdermarrje);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_ListLlogarish", grid_ListLlogarish, cmbKonfigurimi.Text.Split(';')[0], 107.ToString(), idgjuha);
            }
            else
            {
                guidString = (string)hfState["guidString"];
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 107, idgjuha, idNdermarrje);
                grid_ListLlogarish.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idviti, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Llogari.aspx", rm, cultinf);
                mbushGridLlogarishNgaSession();
            }
            GridUtil.konfigGrideListeEMadhePaTheme(grid_ListLlogarish, "IdLlogari");
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, grid_ListLlogarish.ID, int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Llogari.aspx");
            GridUtil.ToolTipButonaveMbiGride(grid_ListLlogarish, cultinf, rm);
        }

        private void EmratEKontrolleve(ResourceManager rm, CultureInfo cultinf)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
            hfState.Set("msgZgjidhniObjektivenEKostos", rm.GetString("msgZgjidhniObjektivenEKostos", cultinf));
            hfState.Set("headerPopUpZgjidhQendrenKostos", rm.GetString("headerPopUpZgjidhQendrenKostos", cultinf));
            hfState.Set("headerPopUpZgjidhSkemenKostos", rm.GetString("headerPopUpZgjidhSkemenKostos", cultinf));
            hfState.Set("headerPopUpZgjidhKategorineEShpenzimit", rm.GetString("headerPopUpZgjidhKategorineEShpenzimit", cultinf));
            hfState.Set("msgShtoLlogariDuhetTeZgjidhniNjeLlogari", rm.GetString("msgShtoLlogariDuhetTeZgjidhniNjeLlogari", cultinf));
            hfState.Set("headerPopUpZgjidhAutorizimet", rm.GetString("headerPopUpZgjidhAutorizimet", cultinf));
            hfState.Set("headerPopUpZgjidhNenGrupin", rm.GetString("headerPopUpZgjidhNenGrupin", cultinf));
            hfState.Set("headerPopUpText", rm.GetString("headerPopUpText", cultinf));
            hfState.Set("headerPopUpZgjidhGrupin", rm.GetString("headerPopUpZgjidhGrupin", cultinf));
            hfState.Set("headerPopUpZgjidhLlogarineStandarte", rm.GetString("headerPopUpZgjidhLlogarineStandarte", cultinf));
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("llogariaTab", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("grupimTab", cultinf);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("fushatShteseTab", cultinf);

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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_Llogari.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {

            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idgjuha, grid_ListLlogarish.ID, "Shto_Llogari.aspx", "FilterDefault", grid_ListLlogarish.FilterExpression, grid_ListLlogarish, "NrLlogari", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(grid_ListLlogarish, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 107, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, grid_ListLlogarish.ID, int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Llogari.aspx");

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
        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListLlogarish_DataBound(object sender, EventArgs e)
        {
            if (grid_ListLlogarish.Columns.Count == 0 || grid_ListLlogarish.Columns["#"] != null)
                 return;
             // shton colonen # per selektim dhe disa karakteristika te grides
             //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
             //perzgjidh
             GridViewCommandColumn check = new GridViewCommandColumn("#");
             check.ShowSelectCheckbox = true;
             check.Width = Unit.Percentage(2);
             //   check.SetColVisibleIndex(0);
             //behet per te afishuar rreshtin qe do sherbej per filtrim
             grid_ListLlogarish.Settings.ShowFilterRow = true;
             grid_ListLlogarish.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
             grid_ListLlogarish.Settings.ShowFilterRowMenu = true;
             grid_ListLlogarish.Columns.Add(check);

             grid_ListLlogarish.KeyFieldName = "IdLlogari";
             grid_ListLlogarish.SettingsBehavior.AllowSelectByRowClick = true;
             grid_ListLlogarish.SettingsBehavior.AllowFocusedRow = true;
             grid_ListLlogarish.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// ben konfigurimin e grides sipas konfigurimit te zgjedhur
        /// </summary>
        private void konfiguroGride(string kodKonfigurimi, int idKomponente, int idGjuha, int idNdermarrje)
        {
            KonfigurimComboGride.shtoGrupetLlogaria(grid_ListLlogarish, idNdermarrje, idgjuha, Session, komponente, guidString);
            KonfigurimComboGride.shtoNenGrupetLlogaria(grid_ListLlogarish, idNdermarrje, idgjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMonedhe(grid_ListLlogarish, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "IdMonedha");
            KonfigurimComboGride.shtoKPF_D(grid_ListLlogarish, idNdermarrje, 1, idPerdoruesi, Session, komponente, guidString);
        }

        /// <summary>
        ///  thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListLlogarish_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && grid_ListLlogarish.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                grid_ListLlogarish.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(grid_ListLlogarish, ci, rm);
        }

        /// <summary>
        ///   sherben per te vendosur filtra tek header-i i grides (mund te ndryshohet me vone per tu
        ///bere  me e konfigurueshme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_ListLlogarish_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "EmerLlogari1" || e.Column.FieldName == "EmerLlogari2")
            {
                e.Values.Clear();
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
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, grid_ListLlogarish.ID, "Shto_Llogari.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                filtra.IdPerdoruesi = idPerdoruesi;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, grid_ListLlogarish.ID, int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Llogari.aspx");
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //ASPxButton btnRuaj = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("Button1") as ASPxButton;
                //ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;

                //  btnRuaj.ClientEnabled = false;
                //  btnFshiFilter.ClientEnabled = false;
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                konfiguroVleraFillestare(idPerdoruesi, rm, ci, idGjuha);
                hfStatusi.Value = "true";
                grid_ListLlogarish.FilterExpression = String.Empty;
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
            filtri.FiltraUniversal = false;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), grid_ListLlogarish.ID, "Shto_Llogari.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = grid_ListLlogarish.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrLlogari", grid_ListLlogarish);
           
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, grid_ListLlogarish.ID, int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Llogari.aspx");
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
         
        }
       
        /// <summary>
        /// perdoret per te fshire reshtat e zgjedhur ne gride pasi perdoruesi ka konfirmuar fshirjen
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = grid_ListLlogarish.GetSelectedFieldValues("IdLlogari");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //List<object> rreshtat = grid_ListLlogarish.GetSelectedFieldValues("IdLlogari");
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariZgjidhniTePaktenNjeLlogari", ci), pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            foreach (object id in rreshtat)
            {
                DbCore.DbKontabiliteti.clsLlogari oLlogari = new DbCore.DbKontabiliteti.clsLlogari();
                oLlogari.IdLlogari = Convert.ToInt32(id);
                oLlogari = oLlogari.merrLlogariSipasId();


                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(oLlogari.IdKonfig, DbCore.mySessionObjects.ktheGjuhe(Session));

                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(oLlogari.IdLlogari.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(oLlogari.NrLlogari);
                    continue;
                }
                oLlogari.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazhi = oLlogari.fshiLlogari();
                if (oLlogari.IdLlogari == 0)
                    continue;
                if (mesazhi.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqLlogariNgaGrida(oLlogari.IdLlogari, rm, ci);
                    #endregion
                    TeFshire.Add(oLlogari.NrLlogari);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }

            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgShtoLlogariPrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgShtoLlogariSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgShtoLlogariPrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgShtoLlogariSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgShtoLlogariPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgShtoLlogariPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgClejeArkaBankaSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("lidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            grid_ListLlogarish.Selection.UnselectAll();
            pnlMesazhi.Update();
        }
        private void hiqLlogariNgaGrida(int idllogari, ResourceManager rm, CultureInfo ci)
        {
            if (this.grid_ListLlogarish.DataSource != null)
            {
                DataTable dt = (DataTable)grid_ListLlogarish.DataSource;
                DataRow[] drs = dt.Select("IdLlogari = " + idllogari);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgShtoLlogariNdodhen2LlogMeTeNjejtenIDNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                grid_ListLlogarish.DataBind();
            }
            else mbushGridLlogarishNgaDB();
        }
        private void shtoLlogariNeGrid(int idNdermarrje, int idPerdorues, int idllogari, ResourceManager rm, CultureInfo ci, int idGjuha)
        {
            if (grid_ListLlogarish.DataSource != null)
            {
                DataTable dt = (DataTable)grid_ListLlogarish.DataSource;
                DataRow[] drs = dt.Select("IdLlogari = " + idllogari);
                if (drs.Length > 0)
                    throw new DbCore.MyException(rm.GetString("msgShtoLlogariekzistonNeGride", ci));
                DataRow newArtDr = DbCore.DbKontabiliteti.colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeDR(idNdermarrje, idPerdorues, idllogari, idGjuha);
                dt.ImportRow(newArtDr);
            }
            else mbushGridLlogarishNgaDB();
        }
        private void modifikoLlogariNeGrid(int idNdermarrje, int idPerdorues, int idLlogari, ResourceManager rm, CultureInfo ci, int idGjuha)
        {
            if (grid_ListLlogarish.DataSource != null)
            {
                DataTable dt = (DataTable)grid_ListLlogarish.DataSource;
                DataRow[] drs = dt.Select("IdLlogari = " + idLlogari);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgShtoLlogariNdodhen2LlogMeTeNjejtenIDNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbKontabiliteti.colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeDR(idNdermarrje, idPerdorues, idLlogari, idGjuha);
                if (newArtDr != null)
                {
                    object[] arr = newArtDr.ItemArray;
                    dr.ItemArray = arr;
                }
            }
            else mbushGridLlogarishNgaDB();
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
                ruajLlogari();
            }
        }
        /// <summary>
        /// perdoret per te shfaqur te gjithe komponentet kur zgjidhet (...)
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumenti</param>
        protected void grid_ListLlogarish_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "KPF1" ||
                  e.Column.FieldName == "IdMonedha" || e.Column.FieldName == "Grupi" ||
                    e.Column.FieldName == "Nengrupi")
            {
                if (Converter.ConvertToInt(e.Value) == -3)
                {
                    e.Criteria = null;
                }
            }
        }

        /// <summary>
        /// ruan disa karakteristika te grides
        /// </summary>
        /// <param name="sender">dergues</param>
        /// <param name="e">argumentat</param>
        protected void gridllog_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_ListLlogarish.PageIndex;
            e.Properties["cpPageRow"] = grid_ListLlogarish.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_ListLlogarish.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te percaktuar templatet e komboboxeve dhe te mbushe vlerat fillestare te gridave
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        private void konfiguroVleraFillestare(int idPerdoruesi, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            inicializoObjekte();

            //  funk.percaktoTemplateComboMeEnableCallbackPaButon(cmbMonedha);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbObjektiva, cmbGrupi, FSC_1_Debi_TextBox, FSC_2_Debi_TextBox, FSC_3_Debi_TextBox, cmbKategori);
            ConfigureAspxComboBox.mbushComboGrupeLlogarish(idNdermarrje, cmbGrupi, idGjuha);
            ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmbObjektiva, IdNdermarrja);
            ConfigureAspxComboBox.mbushComboKPFBij(idPerdoruesi, idNdermarrje, FSC_1_Debi_TextBox, 1);
            ConfigureAspxComboBox.mbushComboKPFBij(idPerdoruesi, idNdermarrje, FSC_2_Debi_TextBox, 2);
            ConfigureAspxComboBox.mbushComboKPFBij(idPerdoruesi, idNdermarrje, FSC_3_Debi_TextBox, 3);
            ConfigureAspxComboBox.KonfiguroComboBoxKategoriShpenzimi(cmbKategori, idNdermarrje, false);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbNengrupi, llogKorresponduese_TextBox, llogKonsoliduese_TextBox, qendraKostos_TextBox);

            ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, false, cmbMonedha);
            ConfigureAspxComboBox.mbushComboLlojQendre(cmbLloji, true, rm, ci);
            ConfigureAspxComboBox.KonfiguroComboBoxTaksat(idPerdoruesi, idNdermarrje, nivelTakse_ComboBox, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, false, false, true);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 14, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }

        /// <summary>
        /// mbush kombon e modeleve te fushave shtese
        /// </summary>
        private void mbushComboModeli()
        {//mbush kombon e modelit me te dhena nga databasa

            DbCore.DbAdmin.clsLlojModeliFushaShtese llojmod = new DbCore.DbAdmin.clsLlojModeliFushaShtese("Llogari");
            int idlloj = llojmod.IdLlojModeliFushaShtese;
            DbCore.DbAdmin.colModeletFushaShtese colModeli = new DbCore.DbAdmin.colModeletFushaShtese(idlloj, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            cmbModeli.DataSource = colModeli;
            cmbModeli.TextField = "KodiModeliFushashtese";
            cmbModeli.ValueField = "IdModeliFushaShtese";
            cmbModeli.DataBind(); 
        }

        /// <summary>
        /// inicializon dbKontabilitetin
        /// </summary>
        private void inicializoObjekte()
        {
        }
        private void mbushGridLlogarishNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridLlogarishNgaDB();
            else
            {
                grid_ListLlogarish.DataSource = tmpObject;
                grid_ListLlogarish.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridLlogarishNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbKontabiliteti.colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheGjuhe(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            grid_ListLlogarish.DataSource = dt;
            grid_ListLlogarish.DataBind();
            dt.Dispose();
        }
        /// <summary>
        /// mbush griden e llogarive
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        private void mbushListeLlogarish(int idPerdoruesi)
        {//mbush griden me te dhena 
            DbCore.DbKontabiliteti.colLlogarite colLlog = new DbCore.DbKontabiliteti.colLlogarite();
            colLlog.mbushLLogariteNdermarrjesAndAutorizime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi);
            grid_ListLlogarish.DataSource = colLlog;
            grid_ListLlogarish.DataBind();
        }


        /// <summary>
        ///sherben per te ruajtur nje llogarie 
        /// </summary>
        private void ruajLlogari()
        {
            DbCore.DbKontabiliteti.clsLlogari llogari;
            if (Page.IsValid == false)
                return;
           
            else
            {
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                if (isValidLlogari(rm, ci))
                {
                    try
                    {
                        llogari = krijoLlogari(idPerdoruesi, idGjuha, rm, ci);
                    }
                    catch (Exception e)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }                   
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    bool eshteShtim;
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_Llogari.aspx");
              
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = llogari.ruaj(false, "", "", "", "");
                        eshteShtim = true;
                    }  //mesazh = llogari.ruajLlogari();
                    else
                    {
                        if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                        konf.mbushKonfigAmbjSipasId(llogari.IdKonfig, idGjuha);

                        llogari.IdLlogari = int.Parse(hfId.Value.ToString());
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(llogari.IdLlogari.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgShtoLlogariEshtElidhur", ci);
                        }
                        else
                            mesazh = llogari.modifiko();
                        eshteShtim = false;
                        dbRegjistrim.Dispose();
                    }

                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
                        if (eshteShtim)
                            shtoLlogariNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi, llogari.IdLlogari, rm, ci, idGjuha);
                        else //modifikim
                            modifikoLlogariNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi, llogari.IdLlogari, rm, ci, idGjuha);
                        hfStatusi.Value = "true";
                        pastroFusha();
                        ASPxPageControl1.ActiveTabIndex = 0;
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimiNeRuajtje", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                    //ASPxPageControl1.ActiveTabIndex = 0;

                    //mbushListeLlogarish();
                }
                else
                {
                    //mbushListeLlogarish();
                }
            }
            pnlMesazhi.Update();
        }

        /// <summary>
        /// pastron fushat e faqes
        /// </summary>
        private void pastroFusha()
        {//pastron fushat
            // pergjigja.Text = "";
            this.emer_TextBox.Text = "";
            this.FSC_1_Debi_TextBox.Text = "";
            this.FSC_2_Debi_TextBox.Text = "";
            this.FSC_3_Debi_TextBox.Text = "";
            this.llogKonsoliduese_TextBox.Text = "";
            this.llogKorresponduese_TextBox.Text = "";
            this.cmbAutorizimiHf.Value = "";
            this.nivelTakse_ComboBox.SelectedIndex = -1;
            this.numri_TextBox.Text = "";
            //this.qendraKostos_TextBox.Text = "";
            this.txtEmerLlogarie1.Text = "";
            this.txtEmerLlogarie2.Text = "";
            this.txtNr.Text = "";
            this.cmbGrupi.SelectedIndex = -1;
            this.cmbMonedha.SelectedIndex = -1;
            this.cmbNengrupi.SelectedIndex = -1;
            //this.ASPxGridView_Llogarite.CancelEdit();
            //this.ASPxGridView_Llogarite.AddNewRow();
            HiddenField1.Value = "";

            txtNr6.Text = "";
            txtEmertimi6.Text = "";
            this.cmbModeli.SelectedIndex = 0;
       
            hfFushatShtese.Value = "";
            HiddenField2.Value = "";
        }

        /// <summary>
        /// krijon llogarine qe do te ruhet
        /// </summary>
        /// <returns>kthen clsLlogari me llogarine qe do te ruhet</returns>
        /// <param name="idPerdoruesi"></param>
        private DbCore.DbKontabiliteti.clsLlogari krijoLlogari(int idPerdoruesi, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
           
            DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string nrLlog = DbCore.clsFunksione.ktheStringunPaHapesira(this.txtNr.Text, true);
            string emerLlog1 = DbCore.clsFunksione.ktheStringunPaHapesira(this.txtEmerLlogarie1.Text, false);
            string emerLlog2 = DbCore.clsFunksione.ktheStringunPaHapesira(this.txtEmerLlogarie2.Text, false); 
            string emerLlogFr = DbCore.clsFunksione.ktheStringunPaHapesira(this.txtEmerLlogarieFr.Text, false);
            int nengrup = DbCore.DbKontabiliteti.clsNenGrupiLlogaria.ktheIDNenGrupLlogaria(cmbNengrupi.Text, idNdermarrje, idGjuha);
            int grup = DbCore.DbKontabiliteti.clsGrupiLlogaria.mbushIDGrupLlogari(cmbGrupi.Text, idNdermarrje, idGjuha);
            int monedha = 0;
            mon.mbushMonedhen(cmbMonedha.Text, idNdermarrje);
            if (mon != null)
            {
                monedha = mon.IdMonedha;
            }
            else monedha = 0;
            int idKpf1 = 0;
            if (this.FSC_1_Debi_TextBox.Text != "")
                idKpf1 = DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.FSC_1_Debi_TextBox.Text, idNdermarrje, 1);
            else idKpf1 = 0;
            int idKpf2 = 0;
            if (this.FSC_2_Debi_TextBox.Text != "")
                idKpf2 = DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.FSC_2_Debi_TextBox.Text, idNdermarrje, 2);
             else idKpf2 = 0;
            int idKpf3 = 0;
            if (this.FSC_3_Debi_TextBox.Text != "")
            idKpf3 = DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.FSC_3_Debi_TextBox.Text, idNdermarrje, 3);
            else idKpf3 = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            int idLlogariKonsoliduese = 0;
            if (this.llogKonsoliduese_TextBox.Text != "")
            idLlogariKonsoliduese = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(this.llogKonsoliduese_TextBox.Text, idNdermarrje);  
            else idLlogariKonsoliduese = 0;
            int llogarikorresponduese = 0;
            if (llogKorresponduese_TextBox.Text != "")
                llogarikorresponduese = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(this.llogKorresponduese_TextBox.Text, idNdermarrje);
            else
                llogarikorresponduese = 0;

            var OColLidhjeAutorizim = new DbCore.DbAdmin.colLidhjetAutorizim();
            if (cmbAutorizimiHf.Value == "")
                OColLidhjeAutorizim = new DbCore.DbAdmin.colLidhjetAutorizim();
            else
            {
                DbCore.DbAdmin.colLidhjetAutorizim colLidhjet = new DbCore.DbAdmin.colLidhjetAutorizim();
                string[] pars1 = cmbAutorizimiHf.Value.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    DbCore.DbAdmin.clsLidhjeAutorizim lidhje = new DbCore.DbAdmin.clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                    colLidhjet.Add(lidhje);
                }
                OColLidhjeAutorizim = colLidhjet;
            }

            int niveltakse = 0;
            if (nivelTakse_ComboBox.Text != null && nivelTakse_ComboBox.Text != "")
                niveltakse = Convert.ToInt32(nivelTakse_ComboBox.Value);
            else
                niveltakse = 0;

            int idobjektiva;
            if (cmbObjektiva.Text != "")
            {
                DbCore.DbQendraKosto.clsObjektivaKosto obj = new DbCore.DbQendraKosto.clsObjektivaKosto(cmbObjektiva.Text, idNdermarrje);
                idobjektiva = obj.Id;
            }
            else idobjektiva = 0;

            int idkategori=0;
            if (cmbKategori.Text != "")
            {
                DbCore.DbKontabiliteti.clsKategoriShpenzimi obj = new DbCore.DbKontabiliteti.clsKategoriShpenzimi(cmbKategori.Text, idNdermarrje);
                idkategori = obj.Id;
            }
            else idkategori = 0;
            int idqendra, idskema;
            if (this.qendraKostos_TextBox.Text != "")
            {
                if (cmbLloji.Value.ToString() == "1")
                {
                    DbCore.DbQendraKosto.clsQendraKosto obj = new DbCore.DbQendraKosto.clsQendraKosto(qendraKostos_TextBox.Text, idNdermarrje);
                    idqendra = obj.Id;
                    idskema = 0;
                }
                else
                {
                    DbCore.DbQendraKosto.clsKokaSkemaQK obj = new DbCore.DbQendraKosto.clsKokaSkemaQK(qendraKostos_TextBox.Text, idNdermarrje);
                    idskema = obj.IdKoka;
                    idqendra = 0;
                }
            }
            else
            {
                idqendra = 0; idskema = 0;
            }

            string shenime1 = this.txtShenime1.Text;
            string shenime2 = this.txtShenime2.Text;
            string shenime3 = this.txtShenime3.Text;
            string shenime4 = this.txtShenime4.Text;
            string shenime5 = this.txtShenime5.Text;

            bool shtim = (hfShtimModifikim.Value.ToString() == "shtim" || hfShtimModifikim.Value.ToString() == "klonim") ? true : false;

            DbCore.DbKontabiliteti.clsLlogari llogari =  new DbCore.DbKontabiliteti.clsLlogari(nrLlog, emerLlog1, emerLlog2, emerLlogFr, idqendra, qendraKostos_TextBox.Text, idKpf1, idKpf2, idKpf3, niveltakse, monedha, grup, nengrup, idLlogariKonsoliduese, llogarikorresponduese, idNdermarrje, cmbGrupi.Text, cmbNengrupi.Text, cmbMonedha.Text, FSC_1_Debi_TextBox.Text, FSC_2_Debi_TextBox.Text, FSC_3_Debi_TextBox.Text,idPerdoruesi, konfig.IdKonfigAmbjente, nivelTakse_ComboBox.Text, 1, idobjektiva, cmbObjektiva.Text, idskema, int.Parse(cmbLloji.Value.ToString()) ,idkategori, cmbKategori.Text, checkAktivLlogaria.Checked, shtim, idGjuha,rm,ci, OColLidhjeAutorizim , shenime1, shenime2, shenime3, shenime4, shenime5);
            return llogari;
        }

        /// <summary>
        /// kontrollon nese te dhenat qe jane plotesuara jane 
        ///te lejueshme apo jo
        /// </summary>
        /// <returns> true  ose false</returns>
        private bool isValidLlogari(ResourceManager rm, CultureInfo ci)
        {
            bool isValid;
            isValid = true;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            DbCore.clsMesazh kontrollNrLlogarie = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(DbCore.clsFunksione.ktheStringunPaHapesira(this.txtNr.Text, true), FusheKontrolli.Kodi, false);
            if (!kontrollNrLlogarie.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrollNrLlogarie.PershkrimMesazhi, pnlMesazhi);
                return false;
            }
            DbCore.clsMesazh kontrollEmerLlogarie1 = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(DbCore.clsFunksione.ktheStringunPaHapesira(this.txtEmerLlogarie1.Text, false), FusheKontrolli.Emri, true);
            if (!kontrollEmerLlogarie1.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrollEmerLlogarie1.PershkrimMesazhi, pnlMesazhi);
                return false;
            }
            DbCore.clsMesazh kontrollEmerLlogarie2 = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(DbCore.clsFunksione.ktheStringunPaHapesira(this.txtEmerLlogarie1.Text, false), FusheKontrolli.Emri, true);
            if (!kontrollEmerLlogarie2.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrollEmerLlogarie2.PershkrimMesazhi, pnlMesazhi);
                return false;
            }

            else
                if (this.FSC_1_Debi_TextBox.Text != "")
            {
                if (DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.FSC_1_Debi_TextBox.Text, idNdermarrje, 1) == -1)
                {
                    isValid = false; hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariLlogStandarteEStrukturesNukEkziston", ci), pnlMesazhi);
                    return isValid;
                }
                else
                {
                    DbCore.DbKontabiliteti.clsKPF kpf = new DbCore.DbKontabiliteti.clsKPF(DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.FSC_1_Debi_TextBox.Text, idNdermarrje, 1));

                    if (DbCore.DbKontabiliteti.clsKPF.eshtePrind(kpf.KodiKPF, idNdermarrje, kpf.GrupiKPF, kpf.NiveliKPF))
                    {
                        isValid = false; hfStatusi.Value = "false";
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariNukMundTeZgjdhniLlogPrind", ci), pnlMesazhi);
                        return isValid;
                    }
                }
            }
            else
            {
                isValid = false; hfStatusi.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariZgjidhStrukturenEPare", ci), pnlMesazhi);
                return isValid;
            }

            if (this.FSC_2_Debi_TextBox.Text != "")
            {
                if (DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.FSC_2_Debi_TextBox.Text, idNdermarrje, 2) == -1)
                {
                    isValid = false; hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariLlogStandarteEstruktures2NukEkziston", ci), pnlMesazhi);
                    return isValid;
                }
                else
                {
                    DbCore.DbKontabiliteti.clsKPF kpf = new DbCore.DbKontabiliteti.clsKPF(DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.FSC_2_Debi_TextBox.Text, idNdermarrje, 2));

                    if (DbCore.DbKontabiliteti.clsKPF.eshtePrind(kpf.KodiKPF, idNdermarrje, kpf.GrupiKPF, kpf.NiveliKPF))
                    {
                        isValid = false; hfStatusi.Value = "false";
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariNukMundTeZgjdhniLlogPrind", ci), pnlMesazhi);
                        return isValid;
                    }
                }
            }
            if (this.FSC_3_Debi_TextBox.Text != "")
            {
                if (DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.FSC_3_Debi_TextBox.Text, idNdermarrje, 3) == -1)
                {
                    isValid = false; hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariLlogStandarteEstruktures3NukEkziston", ci), pnlMesazhi);
                    return isValid;
                }
                else
                {
                    DbCore.DbKontabiliteti.clsKPF kpf = new DbCore.DbKontabiliteti.clsKPF(DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.FSC_3_Debi_TextBox.Text, idNdermarrje, 3));

                    if (DbCore.DbKontabiliteti.clsKPF.eshtePrind(kpf.KodiKPF, idNdermarrje, kpf.GrupiKPF, kpf.NiveliKPF))
                    {
                        isValid = false; hfStatusi.Value = "false";
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariNukMundTeZgjdhniLlogPrind", ci), pnlMesazhi);
                        return isValid;
                    }
                }
            }
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            if (this.cmbGrupi.Text != "")
            {
                if (DbCore.DbKontabiliteti.clsGrupiLlogaria.mbushIDGrupLlogari(cmbGrupi.Text, idNdermarrje, idGjuha) == -1)
                {
                    isValid = false; hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariGrupiNukEkziston", ci), pnlMesazhi);
                    return isValid;
                }
            }
            if (this.cmbNengrupi.Text != "")
            {
                if (DbCore.DbKontabiliteti.clsNenGrupiLlogaria.ktheIDNenGrupLlogaria(cmbNengrupi.Text, idNdermarrje, idGjuha) == -1)
                {
                    isValid = false; hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariNenGrupiNukEkziston", ci), pnlMesazhi);
                    return isValid;
                }
                int idgrupi = DbCore.DbKontabiliteti.clsGrupiLlogaria.mbushIDGrupLlogari(cmbGrupi.Text, idNdermarrje, idGjuha);
                int idnengrupi = DbCore.DbKontabiliteti.clsNenGrupiLlogaria.ktheIDNenGrupLlogaria(cmbNengrupi.Text, idNdermarrje, idGjuha);
                DbCore.DbKontabiliteti.clsNenGrupiLlogaria nen = new DbCore.DbKontabiliteti.clsNenGrupiLlogaria(idnengrupi, idGjuha);
                if (nen.IdGrupiLlogaria != idgrupi)
                {
                    isValid = false; hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariNenGrupiNukIPerketKetijGrupi", ci), pnlMesazhi);
                    return isValid;
                }
            }
            if (cmbObjektiva.Text != "")
            {
                if (!DbCore.DbQendraKosto.clsObjektivaKosto.ekzistonOK(cmbObjektiva.Text, idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariObjektivaEKostosNukEkziston", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
                DbCore.DbQendraKosto.clsObjektivaKosto obj = new DbCore.DbQendraKosto.clsObjektivaKosto(cmbObjektiva.Text, idNdermarrje);
                if (!obj.Aktiv)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariObjektivaEKostosNukEsteAktive", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
            }
            if (cmbKategori.Text != "")
            {
                if (!DbCore.DbKontabiliteti.clsKategoriShpenzimi.EkzistonKategori(cmbKategori.Text, idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariKategoriaEshpenzimitNukEkziston", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
                if (DbCore.DbKontabiliteti.clsKategoriShpenzimi.KontrolloEshtePrindKategoriaShpenzimit(Convert.ToInt32(cmbKategori.Value)))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKategoriaEshteEDetajuar", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
            }
            if (this.qendraKostos_TextBox.Text != "")
            {
                if (cmbLloji.Value.ToString() == "1")
                {
                    if (!DbCore.DbQendraKosto.clsQendraKosto.ekzistonQK(qendraKostos_TextBox.Text, idNdermarrje))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgStrukturaAdministrativeNukEkzistonQendra", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                    DbCore.DbQendraKosto.clsQendraKosto obj = new DbCore.DbQendraKosto.clsQendraKosto(qendraKostos_TextBox.Text, idNdermarrje);
                    if (!obj.Aktiv)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgStrukturaAdministrativeQendraJoAktive", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                    DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                    col.mbushQendraSipasPrindit(obj.Id);
                    if (col.Count > 0)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgStrukturaAdministrativeQendraPrindNukZgjidhet", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                }
                else
                {
                    if (!DbCore.DbQendraKosto.clsKokaSkemaQK.ekzistonSkeme(qendraKostos_TextBox.Text, idNdermarrje))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgStrukturaAdministrativeNukEkzistonSkemaQK", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                }
            }
         

            return isValid;

        }
        protected void cmbGrupi_SelectedIndexChanged(object sender, EventArgs e)
        {//lidh combon e nengrupeve ne varesi te grupit te selektuar

        }


        /// <summary>
        /// thirret kur ndyshon modeli i zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbModeli_SelectedIndexChanged(object sender, EventArgs e)
        {//ndryshohen te dhenat e grides sipas modelit te zgjedhur
            //if (new DbCore.DbAdmin.clsDatabaseAdmin().merrModelinFushaShteseSipasKodit(cmbModeli.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)).Count > 0)
            //{
            //    CacheLayer.GlobalCacheManager.MySessionCache["idmod"] = new DbCore.DbAdmin.clsDatabaseAdmin().merrModelinFushaShteseSipasKodit(cmbModeli.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session))[0].IdModeliFushaShtese;
            //    mbushListeFushashShtese();
            //    percaktoTemplateFushash();
            //}
            //else CacheLayer.GlobalCacheManager.MySessionCache["idmod"] = "0";
        }
        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListLlogarish_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    grid_ListLlogarish.FilterExpression = "";
                else
                {
                    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, grid_ListLlogarish.ID, "Shto_Llogari.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        grid_ListLlogarish.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_ListLlogarish);

                        int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                        System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                        System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                        konfiguroVleraFillestare(idPerdoruesi, rm, ci, idGjuha);
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

            grid_ListLlogarish.Selection.UnselectAll();
        }

        protected void cmbNengrupi_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            //if (e.Parameter != "null")
            //{
            //    int idGrupiLlogari = int.Parse(e.Parameter.ToString());

            //    new DbCore.clsFunksione().mbushComboNenGrupe(cmbNengrupi, idGrupiLlogari);


            //}
        }
        /// <summary>
        /// perdoret per te mbushur combon e monedhes ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbMonedha_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbMonedha"))
                {
                    ConfigureAspxComboBox.mbushComboMonedha(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), false, cmbMonedha);
                }
            }
        }
        /// <summary>
        /// perdoret per te mbushur combon e grupit ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbGrupi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbGrupi"))
                {
                    ConfigureAspxComboBox.mbushComboGrupeLlogarish(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbGrupi, DbCore.mySessionObjects.ktheGjuhe(Session));
                }
            }
        }
        /// <summary>
        /// perdoret per te mbushur combon e nengrupit ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbNengrupi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbNengrupi"))
                {

                    if (cmbGrupi.Value != null)


                        ConfigureAspxComboBox.mbushComboNenGrupe(cmbNengrupi, int.Parse(cmbGrupi.Value.ToString()), DbCore.mySessionObjects.ktheGjuhe(Session));
                }
            }
        }

        protected void cmbNengrupi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbNengrupi"))
                {
                    if (string.IsNullOrWhiteSpace(e.Filter)) return;
                    DbCore.DbKontabiliteti.colNenGrupetLlogaria col = new DbCore.DbKontabiliteti.colNenGrupetLlogaria();

                    if (cmbGrupi.Value != null)



                        col.mbushNenGrupetLlogariaSipasGrupitPozitive(int.Parse(cmbGrupi.Value.ToString()), DbCore.mySessionObjects.ktheGjuhe(Session));

                    var dsReal = col.Where(x => x.PershkrimiNenGrupiLlogaria.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);
                    cmbNengrupi.TextField = "PershkrimiNenGrupiLlogaria";
                    cmbNengrupi.ValueField = "IdNenGrupiLlogaria";
                    cmbNengrupi.DataSource = dsReal.ToList();


                    cmbNengrupi.DataBind();

                }
            }
        }
        /// <summary>
        /// perdoret per te mbushur combon e kpf 1 ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void FSC_1_Debi_TextBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("FSC_1_Debi_TextBox"))
                {
                    ConfigureAspxComboBox.mbushComboKPFBij(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), FSC_1_Debi_TextBox, 1);
                }
            }
        }
        /// <summary>
        /// perdoret per te mbushur combon e kpf2 ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void FSC_2_Debi_TextBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("FSC_2_Debi_TextBox"))
                {
                    ConfigureAspxComboBox.mbushComboKPFBij(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), FSC_2_Debi_TextBox, 2);
                }
            }
        }
        /// <summary>
        /// perdoret per te mbushur combon e kfp3 ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void FSC_3_Debi_TextBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("FSC_3_Debi_TextBox"))
                {
                    ConfigureAspxComboBox.mbushComboKPFBij(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), FSC_3_Debi_TextBox, 3);
                }
            }
        }
        /// <summary>
        /// perdoret per te mbushur combon e qendren e kostos ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void qendraKostos_TextBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("qendraKostos_TextBox"))
                {
                    if (cmbLloji.Value.ToString() == "1")
                        ConfigureAspxComboBox.mbushComboQendraKostoBij(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), qendraKostos_TextBox);
                    else ConfigureAspxComboBox.mbushComboSkemaQendraKosto(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), qendraKostos_TextBox);
                }
            }
        }


        protected void qendraKostos_TextBox_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("qendraKostos_TextBox"))
                {
                    if (string.IsNullOrWhiteSpace(e.Filter)) return;
                    DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                    if (qendraKostos_TextBox.Value != null)
                        col.mbushQendraSipasPrindit(int.Parse(qendraKostos_TextBox.Value.ToString()));
                    else
                        col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    qendraKostos_TextBox.DataSource = col.Where(x => x.Kodi.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);

                    qendraKostos_TextBox.TextField = "Kodi";
                    qendraKostos_TextBox.ValueField = "Id";
                    qendraKostos_TextBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    qendraKostos_TextBox.DataBind();


                }
            }
        }
        /// <summary>
        /// perdoret per te mbushur combon e nivelin e takses ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void nivelTakse_ComboBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("nivelTakse_ComboBox"))
                {
                    ConfigureAspxComboBox.KonfiguroComboBoxTaksat(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), nivelTakse_ComboBox, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, false);
                }
            }
        }
        /// <summary>
        /// perdoret per te mbushur combon e llogarine konsoliduese ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void llogKonsoliduese_TextBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].ToString().Contains("llogKonsoliduese_TextBox"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, llogKonsoliduese_TextBox, e);
            }
        }

        protected void llogKonsoliduese_TextBox_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].ToString().Contains("llogKonsoliduese_TextBox"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, llogKonsoliduese_TextBox, e);
            }
        }

        /// <summary>
        /// perdoret per te mbushur combon e llogarise koresponduese ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void llogKorresponduese_TextBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].ToString().Contains("llogKorresponduese_TextBox"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, llogKorresponduese_TextBox, e);
            }
        }

        protected void llogKorresponduese_TextBox_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].ToString().Contains("llogKorresponduese_TextBox"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, llogKorresponduese_TextBox, e);
            }
        }

        protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.TabControlEventArgs e)
        {

        }

        protected void cmbObjektiva_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbObjektiva"))
                ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmbObjektiva, IdNdermarrja);
        }

        protected void cmbKategori_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbKategori"))
                ConfigureAspxComboBox.KonfiguroComboBoxKategoriShpenzimi(cmbKategori, IdNdermarrja, false);
        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {

            try
            {
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                gridExport.WriteXlsxToResponse(rm.GetString("MenuItemLlogarite", ci), true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                gridExport.WritePdfToResponse(rm.GetString("MenuItemLlogarite", ci), true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

    }
}