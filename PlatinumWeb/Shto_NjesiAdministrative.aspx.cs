using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace PlatinumWeb
{
    public partial class Shto_NjesiAdministrative : MyPageBase
    {
        public static int idNderm = -1;
        public static bool isShtim = true;
        public static int id = 0;
        public static DbCore.DbRegjistrim.clsNjesiAdministrative njesia;
        private int idgjuha, idPerdoruesi, idNdermarrje, idviti, idKonfigurim;
        private string komponente = "Shto_NjesiAdministrative.aspx";
        private string guidString;

        protected void Page_Init(object sender, EventArgs e)
        {
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            if (Request.QueryString["id"] != null)
            {
                id = int.Parse(Request.QueryString["id"]);
            }
            else
                id = 0;

            if (id != 0)
            {
                njesia = new DbCore.DbRegjistrim.clsNjesiAdministrative(id, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            }
            else
                njesia = new DbCore.DbRegjistrim.clsNjesiAdministrative();
        }

     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }

            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);

            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
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
                hfState.Set("idPerdorues", idPerdoruesi);

                hfState.Set("colAutorizime", Newtonsoft.Json.JsonConvert.SerializeObject(new colAutorizimetKoka(IdPerdoruesi)));
                ASPxPageControl1.ActiveTabIndex = 0;
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, cultinf, idgjuha);
                idKonfigurim = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
                if (id != 0)
                {
                    njesia = new DbCore.DbRegjistrim.clsNjesiAdministrative(id, idPerdoruesi);
                    txtPershkrimDege.Text = njesia.Pershkrimi;
                }
                else
                    njesia = new DbCore.DbRegjistrim.clsNjesiAdministrative();
                mbushGridNjesishNgaDB();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 509);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvNjesiAdm", gvNjesiAdm, cmbKonfigurimi.Text.Split(';')[0], "509", (int)hfState["idGjuha"]);
                konfiguroVleraFillestareHistorik();
                konfiguroGrideHistorik();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                gvNjesiAdm.Columns["#"].VisibleIndex = 0;

                ucFushatShtese.KonfiguroVleraFillestare(idNdermarrje, idPerdoruesi, idgjuha, komponente, "Magazina", 0, idKonfigurim);
                if (!String.IsNullOrEmpty(Request.QueryString["vjenNga"]) && !String.IsNullOrEmpty(Request.QueryString["idmagazina"]) && Request.QueryString["vjenNga"] == "GIS")
                {
                    int idNjesiPerSelektim = 0;
                    if (Int32.TryParse(Request.QueryString["idmagazina"], out idNjesiPerSelektim))
                    {
                        int rowIndex = gvNjesiAdm.FindVisibleIndexByKeyValue(idNjesiPerSelektim);
                        if (rowIndex == ASPxGridView.InvalidRowIndex)
                            hfState.Set("idNjesiPerSelektim", "0");
                        hfState.Set("idNjesiPerSelektim", rowIndex);
                    }
                }
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridNjesishNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 509);
                konfiguroGrideHistorik();
            }
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            GridUtil.konfigGrideListeEMadhePaTheme(gvNjesiAdm, "IdNjesiAdministrative");
            ucFushatShtese.percaktoTemplateFushash();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvNjesiAdm", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            //DbCore.clsFunksione.AplikoFilterDefault(gvNjesiAdm, int.Parse(cmbKonfigurimi.Value.ToString()));
            percaktoTemplate();
            GridUtil.ToolTipButonaveMbiGride(gvNjesiAdm, cultinf, rm);
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "");
        }

        bool IsRowVisibleOnScreen(int rowIndex)
        {
            int startIndex = gvNjesiAdm.PageIndex * gvNjesiAdm.SettingsPager.PageSize;
            int endIndex = startIndex + gvNjesiAdm.SettingsPager.PageSize;
            return rowIndex >= startIndex && rowIndex < endIndex;
        }
        void GoToPage(int rowIndex)
        {
            gvNjesiAdm.PageIndex = rowIndex / gvNjesiAdm.SettingsPager.PageSize;
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
            hfState.Set("headerPopUpZgjidhAutorizimet", rm.GetString("headerPopUpZgjidhAutorizimet", cultinf));
            hfState.Set("headerPopUpZgjidhElementinPerIntegrim", rm.GetString("headerPopUpZgjidhElementinPerIntegrim", cultinf));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", cultinf));
            hfState.Set("msgCeljeMagazinatDuhetTeZgjidhniNjeMagazine", rm.GetString("msgCeljeMagazinatDuhetTeZgjidhniNjeMagazine", cultinf));
            hfState.Set("headerPopUpZgjidhQendrenKostos", rm.GetString("headerPopUpZgjidhQendrenKostos", cultinf));
            hfState.Set("headerPopUpZgjidhSkemenKostos", rm.GetString("headerPopUpZgjidhSkemenKostos", cultinf));
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", rm.GetString("regjisDokZgjidhDokPerTeBashkengjitur", cultinf));
            hfState.Set("msgKodiMagazinaHapsira", rm.GetString("msgKodiMagazinaHapsira", cultinf));

        }

        private void EmratEKontrolleve(ResourceManager rm, CultureInfo cultinf)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("filterMagazina", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("ndryshimiStatusitTab", cultinf);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idgjuha, "gvNjesiAdm", komponente, "FilterDefault", gvNjesiAdm.FilterExpression, gvNjesiAdm, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(gvNjesiAdm, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 509, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvNjesiAdm", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
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

        protected void gvNjesiAdm_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvNjesiAdm.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;

                check.Width = Unit.Percentage(2);
                // check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvNjesiAdm.Settings.ShowFilterRow = true;
                gvNjesiAdm.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvNjesiAdm.Settings.ShowFilterRowMenu = true;
                gvNjesiAdm.Columns.Add(check);
                gvNjesiAdm.KeyFieldName = "IdNjesiAdministrative";
                gvNjesiAdm.SettingsBehavior.AllowSelectByRowClick = true;
                gvNjesiAdm.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        private void konfiguroGride(string kodKonfigurimi, int idKomponente)
        {//konfiguron griden

            KonfigurimComboGride.shtoInventarizime(gvNjesiAdm, Session, komponente, guidString);
            KonfigurimComboGride.shto_DegeAdministrative(gvNjesiAdm, idNdermarrje, Session, komponente, guidString);
            KonfigurimComboGride.shto_LlojNjesiAdministrative(gvNjesiAdm, Session, komponente, guidString);
            KonfigurimComboGride.shtoElementePerIntegrim(gvNjesiAdm, idNdermarrje, 1, Session, komponente, guidString);
            KonfigurimComboGride.shto_LlojLayeri(gvNjesiAdm, Session, komponente, guidString);

            percaktoTamplateAutorizime();
            this.gvNjesiAdm.Columns["#"].VisibleIndex = 0;
        }

    

        private void shto_Autorizim()
        {//shtohen komboja me Autorizimeve tek grida e KPF
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvNjesiAdm.Columns["KodiAutorizim"].GetType())
            {
                gvNjesiAdm.Columns.Remove(gvNjesiAdm.Columns["KodiAutorizim"]);
                gvNjesiAdm.Columns.Add(colnew);
                DbCore.DbAdmin.colAutorizimetKoka colAutorizim = new DbCore.DbAdmin.colAutorizimetKoka(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                colnew.PropertiesComboBox.DataSource = colAutorizim;
                colnew.PropertiesComboBox.TextField = "KodiAutorizim";
                colnew.PropertiesComboBox.ValueField = "KodiAutorizim";
                colnew.FieldName = "KodiAutorizim";

                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, colAutorizim, "colAutorizim");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvNjesiAdm.Columns["KodiAutorizim"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "colAutorizim");
                }
            }
        }

   
        //thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        protected void gvNjesiAdm_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvNjesiAdm, ci, rm);
            if (e.CallbackName == "COLUMNMOVE" && gvNjesiAdm.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvNjesiAdm.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
                gvNjesiAdm.Selection.UnselectAll();

            }
            //  konfiguroVleraFillestare();
            percaktoTamplateAutorizime();
        }

        //bere  me e konfigurueshme
        protected void gvNjesiAdm_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "Kodi" || e.Column.FieldName == "Pershkrimi")
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNjesiAdm", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvNjesiAdm", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                // konfiguroVleraFillestare();
                hfStatusi.Value = "true";
                gvNjesiAdm.FilterExpression = String.Empty;
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
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvNjesiAdm", "Shto_NjesiAdministrative.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNjesiAdm", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvNjesiAdm.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvNjesiAdm);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvNjesiAdm.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Kodi";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvNjesiAdm", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
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
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = gvNjesiAdm.GetSelectedFieldValues("IdNjesiAdministrative");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //  List<object> rreshtat = gvNjesiAdm.GetSelectedFieldValues("IdNjesiAdministrative");
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeMagazinatZgjidhniMagazinen", ci), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative oNjesiAdm = new DbCore.DbRegjistrim.clsNjesiAdministrative(Convert.ToInt32(id), idPerdorues);
                if (oNjesiAdm.IdNjesiAdministrative == 0)
                    continue;
                //DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(oNjesiAdm.IdKonfig);
                //konf.IdKonfigAmbjente = oNjesiAdm.IdKonfig;
                //konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);

                //konf = new DbCore.DbShare.clsDatabaseShare().merrKonfigAmbjSipasId(konf)[0];

                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(oNjesiAdm.IdNjesiAdministrative.ToString(), DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdNivel(oNjesiAdm.IdKonfig).ToString());
                if (lidhur)
                {
                    TePaFshire.Add(oNjesiAdm.Kodi);
                    continue;
                }
                mesazh = oNjesiAdm.fshi(idPerdorues, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqNjesiNgaGrida(oNjesiAdm.IdNjesiAdministrative, rm, ci);
                    #endregion
                    TeFshire.Add(oNjesiAdm.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgCeljeMagazinatPrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgCeljeMagazinatSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgCeljeMagazinatPrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgCeljeMagazinatSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgCeljeMagazinatPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgCeljeMagazinatPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("lidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        private void hiqNjesiNgaGrida(int idmag, ResourceManager rm, CultureInfo ci)
        {
            if (this.gvNjesiAdm.DataSource != null)
            {
                DataTable dt = (DataTable)gvNjesiAdm.DataSource;
                DataRow[] drs = dt.Select("IdNjesiAdministrative = " + idmag);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgCeljeMagazinatNdodhen2NeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvNjesiAdm.DataBind();
            }
            else mbushGridNjesishNgaDB();
        }
        private void shtoNjesiNeGrid(int idNdermarrje, int idPerdorues, int idmag, ResourceManager rm, CultureInfo ci)
        {
            if (gvNjesiAdm.DataSource != null)
            {
                DataTable dt = (DataTable)gvNjesiAdm.DataSource;
                DataRow[] drs = dt.Select("IdNjesiAdministrative = " + idmag);
                if (drs.Length > 0)
                    throw new DbCore.MyException(rm.GetString("msgCeljeMagazinatEkzistonNeGride", ci));
                DataRow newArtDr = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarrjesDR(idNdermarrje, idmag, idPerdorues);
                dt.ImportRow(newArtDr);
            }
            else mbushGridNjesishNgaDB();
        }
        private void modifikoNjesiNeGrid(int idNdermarrje, int idPerdorues, int idmag, ResourceManager rm, CultureInfo ci)
        {
            if (gvNjesiAdm.DataSource != null)
            {
                DataTable dt = (DataTable)gvNjesiAdm.DataSource;
                DataRow[] drs = dt.Select("IdNjesiAdministrative = " + idmag);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgCeljeMagazinatNdodhen2NeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarrjesDR(idNdermarrje, idmag, idPerdorues);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                if (newArtDr!=null){
                    object[] arr = newArtDr.ItemArray;
                    dr.ItemArray = arr;
                }
            }
            else mbushGridNjesishNgaDB();
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
                ruajMagazine();
            }
        }
        protected void gvNjesiAdm_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdInventarizimi")
            {
                if (Converter.ConvertToInt(e.Value) == -3)
                {
                    e.Criteria = null;
                }
            }
            if (e.Column.FieldName == "LlojLayeri")
            {
                if (Converter.ConvertToInt(e.Value) == 1)
                {
                    e.Criteria = null;
                }
            }
            else if (e.Column.FieldName == "IdDegeAdministrative" || e.Column.FieldName == "IdLlojMagazine")
            {
                if (Converter.ConvertToInt(e.Value)==0 || Converter.ConvertToInt(e.Value) == -1)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void gvNjesiAdm_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "NdjekjeGjendje" || e.Column.FieldName == "Aktiv")
            {
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add("Po", true);
                (e.Editor as ASPxComboBox).Items.Add("Jo", false);
            }
        }
        protected void gvNjesiAdm_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvNjesiAdm.PageIndex;
            e.Properties["cpPageRow"] = gvNjesiAdm.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvNjesiAdm.VisibleRowCount;
        }

        private void inicializoObjekte()
        {
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
        }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            inicializoObjekte();
            mbushComboInventarizime();
            ConfigureAspxComboBox.percaktoTemplateCombo(false, false, cmbDegeAdministrative);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btneCaktoNeHarte);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btneKodiPerIntegrim);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(qendraKostos_TextBox);
            ConfigureAspxComboBox.KonfiguroComboBoxComboQytete(IdNdermarrja, btnQyteti, false);
            ConfigureAspxComboBox.mbushComboTipiMag(btnTipiMag);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbMagPrind);
            ConfigureAspxComboBox.mbushComboLlojQendre(cmbLlojiQ, true, rm, cultinf);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 23, rm, cultinf, idGjuha);
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            int idMag = 0;
            bool parse = false;
            if (!String.IsNullOrEmpty(Request.QueryString["idmagazina"]))
                parse = Int32.TryParse(Request.QueryString["idmagazina"], out idMag);
            if (parse && idMag > 0)
            {
                int idKonfigurimi = clsNjesiAdministrative.ktheIdKonfigSipasId(idMag);
                konf = new DbCore.DbShare.clsKonfigurimAmbjenti(idKonfigurimi);
                cmbKonfigurimi.SelectedItem = cmbKonfigurimi.Items.FindByText(konf.KodKonfigAmbjente);
            }
            else
            {
                cmbKonfigurimi.SelectedIndex = 0;
                konf = new DbCore.DbShare.clsKonfigurimAmbjenti(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
            }
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            ConfigureAspxComboBox.ShtoKolonaKodiDhePershkrimi(cmbDegeAdministrative, "IdDegeAdministrative");
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, false);
            cmbDegeAdministrative.Items.RemoveAt(0);
            ConfigureAspxComboBox.mbushComboStatusMagazine(idNdermarrje, cmbStatusi);
            ConfigureAspxComboBox.mbushComboLlojMagazine(cmbLloji);
            ConfigureAspxComboBox.mbushComboLlojLayerMagazine(cmbLlojLayer);
            ConfigureAspxComboBox.mbushComboElementeshPerIntegrim(idNdermarrje, btneKodiPerIntegrim, 1, false);
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, cmbMagPrind, idPerdoruesi, false, 0, true);
        }

        private void mbushComboInventarizime()
        {
            DbCore.DbRegjistrim.colInventarizim colInventarizim = new DbCore.DbRegjistrim.colInventarizim();
            colInventarizim.mbushGjitheInventarizime();
            colInventarizim.RemoveAt(0);
            cmbInventarizimi.DataSource = colInventarizim;
            cmbInventarizimi.TextField = "Pershkrimi";
            cmbInventarizimi.ValueField = "IdInventarizimi";
            cmbInventarizimi.DataBind();
            cmbInventarizimi.SelectedIndex = 0;
            cmbInventarizimi.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        private void mbushGridNjesishNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridNjesishNgaDB();
            else
            {
                gvNjesiAdm.DataSource = tmpObject;
                gvNjesiAdm.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridNjesishNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarrjesDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvNjesiAdm.DataSource = dt;
            gvNjesiAdm.DataBind();
            dt.Dispose();
        }

        private void mbushListeNjesiAdministrative(int idNdermarrje)
        {//mbush griden me te dhena
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            DbCore.DbRegjistrim.colNjesiAdministrative colNjesiAdm = new DbCore.DbRegjistrim.colNjesiAdministrative();
            colNjesiAdm.mbushGjitheNjesiAdministrative(idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            gvNjesiAdm.DataSource = colNjesiAdm;
            gvNjesiAdm.DataBind();
        }

        private void percaktoTamplateAutorizime()
        {//templatet per kolonat e Autorizimeve

            GridViewDataColumn col = gvNjesiAdm.Columns["Aktiv"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
            GridViewDataColumn col1 = gvNjesiAdm.Columns["NdjekjeGjendje"] as GridViewDataColumn;
            col1.DataItemTemplate = new MyCheckTemplate(true, false);

        }
        private void ruajMagazine()
        {
            njesia = new DbCore.DbRegjistrim.clsNjesiAdministrative();
            if (Page.IsValid == false)
                return;
            else
            {
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                int idNderVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                if (isValidNjesiAdministrative(rm, ci, idNdermarrje, idNderVit))
                {
                    bool eshteShtim = hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim";
                    int idnivel = 0;
                    try
                    {
                        njesia = krijoMagazine(eshteShtim, out idnivel);
                        hfArkiva.Set("kopjoArkiven", hfShtimModifikim.Value == "klonim");
                    }
                    catch (DbCore.MyException e)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    catch (Exception ec)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Error(ec.Message);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ec.Message, pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                    int idPeriudheZgjedhur = DbCore.mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;
                    int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    int idVitNdermarrje = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idVitNdermarrje, komponente);
                    if (eshteShtim)
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }

                        mesazh = njesia.ruajMagazine(hfNrAutoKF, idNderVit, idPeriudheZgjedhur, idnivel);

                        if (mesazh.Status && cbAktiv.Checked && cbPerdorues.Checked)
                        {
                            if (!DbCore.DbTollona.clsPerdoruesTolloni.ekzistonPerdorues(njesia.Kodi))
                            {
                                DbCore.DbTollona.clsPerdoruesTolloni perdorues = new DbCore.DbTollona.clsPerdoruesTolloni(0, njesia.Kodi, njesia.Kodi, njesia.Kodi, "p", false, true, true, 1);
                                mesazh = perdorues.ruaj();
                            }
                        }



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
                        DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                        konf.IdKonfigAmbjente = njesia.IdKonfig;
                        konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                        eshteShtim = false;
                        njesia.IdNjesiAdministrative = int.Parse(hfId.Value.ToString());
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(njesia.IdNjesiAdministrative.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeMagazinatEshteELidhur", ci), pnlMesazhi);
                            return;
                        }
                        DbCore.DbRegjistrim.clsNjesiAdministrative njesivjeter = new DbCore.DbRegjistrim.clsNjesiAdministrative(njesia.IdNjesiAdministrative);
                        if (njesia.ColHistorik.Count > 0)//nqs po ndryshojme statusin e magazines ruajme id e statusit te vjeter
                        {
                            DbCore.DbAsete.clsHistorikStatusMagazine historik = new DbCore.DbAsete.clsHistorikStatusMagazine();
                            historik.merrHistorikMagazinaAktualeSipasIdNjesiAdministrative(njesia.IdNjesiAdministrative);
                            njesia.IdHistorikFundit = historik.IdHistorikuNjesiAdministrative;
                        }
                        else
                        {

                            //nqs nuk po ndryshohet statusi kalojme statusin e njejte
                            njesia.IdStatusAktualMagazine = njesivjeter.IdStatusAktualMagazine;
                            njesia.DataNdryshimStatus = njesivjeter.DataNdryshimStatus;
                            njesia.IdHistorikFundit = njesivjeter.IdHistorikFundit;
                        }
                        if (lidhur)
                        {
                            if ((njesivjeter.IdLlojMagazine == 2) || (njesivjeter.IdLlojMagazine == 3))
                            {
                                if (njesia.IdLlojMagazine == 1)
                                {
                                    mesazh = new DbCore.clsMesazh("Nuk mund te ndryshoni llojin e magazines!");
                                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                                    return;
                                }

                            }
                            if ((njesivjeter.IdLlojMagazine == 1) || (njesivjeter.IdLlojMagazine == 3))
                            {
                                if (njesia.IdLlojMagazine == 2)
                                {
                                    mesazh = new DbCore.clsMesazh("Nuk mund te ndryshoni llojin e magazines!");
                                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                                    return;
                                }

                            }
                        }
                        mesazh = njesia.modifikoMagazine(idNderVit, idPeriudheZgjedhur, idnivel, rm, ci);
                        dbRegjistrim.Dispose();
                        if (mesazh.Status && cbAktiv.Checked && cbPerdorues.Checked)
                        {
                            if (!DbCore.DbTollona.clsPerdoruesTolloni.ekzistonPerdorues(njesia.Kodi))
                            {
                                DbCore.DbTollona.clsPerdoruesTolloni perdorues = new DbCore.DbTollona.clsPerdoruesTolloni(0, njesia.Kodi, njesia.Kodi, njesia.Kodi, "p", false, true, true, 1);
                                mesazh = perdorues.ruaj();
                            }
                        }
                    }
                    if (mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
                        if (eshteShtim)
                            shtoNjesiNeGrid(idNdermarrje, idPerdoruesi, njesia.IdNjesiAdministrative, rm, ci);
                        else //modifikim
                            modifikoNjesiNeGrid(idNdermarrje, idPerdoruesi, njesia.IdNjesiAdministrative, rm, ci);
                        hfStatusi.Value = "true";

                        return;
                    }
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "false";
                }
            }
        }

        /// <summary>
        /// krijon magazinen qe do te ruhet
        /// </summary>
        /// <returns>kthen clsNjesiAdministrative me magazinen qe do te ruhet</returns>
        private DbCore.DbRegjistrim.clsNjesiAdministrative krijoMagazine(bool shtim, out int idnivel)
        {//krijon nje llogari sipas te dhenave te futura nga perdoruesi
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxPageControl1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, this.ASPxPageControl1, null, null);

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            idnivel = konfig.IdNivel;
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
            int iddege = 0;
            if (cmbDegeAdministrative.Text != string.Empty && cmbDegeAdministrative.Text != " ()")
                iddege = int.Parse(cmbDegeAdministrative.Value.ToString());

            int idMagPrind = 0;
            if (cmbMagPrind.Text != "")
            {
                idMagPrind = clsNjesiAdministrative.ktheIdMagazine(cmbMagPrind.Text, idNdermarrje);
            }
            int ElementPerIntegrim = 0;
            if (btneKodiPerIntegrim.Text != "" && DbCore.DbInventari.clsElementePerIntegrim.ekzistonElementMeKeteKod(btneKodiPerIntegrim.Text, idNdermarrje))
                ElementPerIntegrim = DbCore.DbInventari.clsElementePerIntegrim.ktheIdElementi(btneKodiPerIntegrim.Text, idNdermarrje);
            int idlloj = 0;
            if (cmbLloji.Text != "")
                idlloj = int.Parse(cmbLloji.Value.ToString());
            DbCore.DbAsete.colHistorikStatusMagazine col = new DbCore.DbAsete.colHistorikStatusMagazine();
            if (hfStatusMagazine.Value != "" && hfStatusMagazine.Value != "0" && hfData.Value != "" && int.Parse(cmbLloji.Value.ToString()) != 1)
            {
                DbCore.DbAsete.clsHistorikStatusMagazine historik = new DbCore.DbAsete.clsHistorikStatusMagazine(0, 0, int.Parse(hfStatusMagazine.Value.ToString()), DateTime.Parse(hfData.Value.ToString()), 1, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                col.Add(historik);
            }
            float kohezgjatja = 0;
            if (txtKohezgjatja.Text != "")
                float.TryParse(txtKohezgjatja.Text, out kohezgjatja);
            DbCore.DbRegjistrim.clsNjesiAdministrative njesi;
            string koordinata = "";
            if (btneCaktoNeHarte.Text != "" && hfState.Contains("geom"))
            {
                string geomsToDeserialize = Convert.ToString(hfState.Get("geom"));
                if (!String.IsNullOrEmpty(geomsToDeserialize))
                {
                    System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                    object[] geoms = (object[])serializusi.DeserializeObject(geomsToDeserialize);
                    koordinata = Convert.ToString(geoms[0]);
                }
            }
            //colArkiva oArkiva = new colArkiva();
            //if (hfArkiva != null)
            //    oArkiva = clsArkiva.krijoArkiva(njesia.IdNjesiAdministrative, 1, 23, dteDtRegjistrimi.Date, idPerdoruesi, hfArkiva);
            int idqendra, idskema;
            if (this.qendraKostos_TextBox.Text != "")
            {
                if (cmbLlojiQ.Value.ToString() == "1")
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

            hfNrAutoKF = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "txtKodi", "Kodi");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoKF, hfNrAuto, "txtKodi", "Kodi");
            int idqyteti = 0;
            if (btnQyteti.Value != null)
            {
                idqyteti = int.Parse(btnQyteti.Value.ToString());
            }
            DbCore.DbAdmin.colVleraFushaShtese colFushatShtese = ucFushatShtese.merrFushatShtese();
            if (hfStatusMagazine.Value != "" && hfStatusMagazine.Value != "0" && int.Parse(cmbLloji.Value.ToString()) != 1)
                njesi = new DbCore.DbRegjistrim.clsNjesiAdministrative(int.Parse(hfId.Value.ToString()), DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), DbCore.clsFunksione.ktheStringunPaHapesira(txtPershkrimi.Text, false), txtAdresa.Text, int.Parse(cmbInventarizimi.Value.ToString()), cbNdjekjeGjendje.Checked, cbAktiv.Checked, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), dteDtRegjistrimi.Date, konfig.IdKonfigAmbjente, iddege, cmbDegeAdministrative.Text.Split(' ')[0], shtim, colLidhje, idlloj, int.Parse(hfStatusMagazine.Value.ToString()), DateTime.Parse(hfData.Value.ToString()), 0, kohezgjatja, col, koordinata, cbPerdorues.Checked, txtShenime.Text, txtTelefon.Text, int.Parse(cmbLlojLayer.Value.ToString()), idMagPrind, colFushatShtese, txtEmail.Text, hfArkiva, cmbMagPrind.Text, ElementPerIntegrim, idqendra, idskema, int.Parse(cmbLlojiQ.Value.ToString()), rm, ci, cbDet1.Checked, cbDet2.Checked, cbOwnShop.Checked, btnTipiMag.Value.ToString(),idqyteti);
            else
            {
                if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                    njesi = new DbCore.DbRegjistrim.clsNjesiAdministrative(int.Parse(hfId.Value.ToString()), DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), DbCore.clsFunksione.ktheStringunPaHapesira(txtPershkrimi.Text, false), txtAdresa.Text, int.Parse(cmbInventarizimi.Value.ToString()), cbNdjekjeGjendje.Checked, cbAktiv.Checked, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), dteDtRegjistrimi.Date, konfig.IdKonfigAmbjente, iddege, cmbDegeAdministrative.Text.Split(' ')[0], shtim, colLidhje, idlloj, 0, DateTime.Now, 0, kohezgjatja, col, koordinata, cbPerdorues.Checked, txtShenime.Text, txtTelefon.Text, int.Parse(cmbLlojLayer.Value.ToString()), idMagPrind, colFushatShtese, txtEmail.Text, hfArkiva, cmbMagPrind.Text, ElementPerIntegrim, idqendra, idskema, int.Parse(cmbLlojiQ.Value.ToString()), rm, ci, cbDet1.Checked, cbDet2.Checked, cbOwnShop.Checked, btnTipiMag.Value.ToString(), idqyteti);
                else
                    njesi = new DbCore.DbRegjistrim.clsNjesiAdministrative(int.Parse(hfId.Value.ToString()), DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), DbCore.clsFunksione.ktheStringunPaHapesira(txtPershkrimi.Text, false), txtAdresa.Text, int.Parse(cmbInventarizimi.Value.ToString()), cbNdjekjeGjendje.Checked, cbAktiv.Checked, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), dteDtRegjistrimi.Date, konfig.IdKonfigAmbjente, iddege, cmbDegeAdministrative.Text.Split(' ')[0], shtim, colLidhje, idlloj, 0, DateTime.Now, 0, kohezgjatja, col, koordinata, cbPerdorues.Checked, txtShenime.Text, txtTelefon.Text, int.Parse(cmbLlojLayer.Value.ToString()), idMagPrind, colFushatShtese, txtEmail.Text, hfArkiva, cmbMagPrind.Text, ElementPerIntegrim, idqendra, idskema, int.Parse(cmbLlojiQ.Value.ToString()), rm, ci, cbDet1.Checked, cbDet2.Checked, cbOwnShop.Checked, "", idqyteti);
            }
            return njesi;
        }
        

        protected void btneKodiPerIntegrim_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKodiPerIntegrim"))
                {

                    ConfigureAspxComboBox.mbushComboElementeshPerIntegrim(idNdermarrje, btneKodiPerIntegrim, 1, false);
                }
            }
        }

        //TODO me vone
        ///// <summary>
        ///// perdoret per te mbushur combon e magazinave prind ne momentin qe perdoruesi fillon te shkruaje
        ///// </summary>
        ///// <param name="source">derguesi</param>
        ///// <param name="e">argumentat</param>
        //protected void cmbMagPrind_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        //{
        //    if (IsCallback)
        //    {
        //        if (Request.Params["__CALLBACKID"].ToString().Contains("cmbMagPrind"))
        //        {
        //            //DbCore.clsFunksione.mbushComboAutorizime(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), cmbAutorizimi);
        //            DbCore.clsFunksione.mbushComboMagazinat(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbMagPrind, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false, 0, true);
        //        }
        //    }
        //}
        //protected void cmbMagPrind_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        //{
        //    if (IsCallback)
        //    {
        //        if (Request.Params["__CALLBACKID"].ToString().Contains("cmbMagPrind"))
        //        {
        //            DbCore.clsFunksione.mbushComboMagazinat(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbMagPrind, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false, 0, true);
        //        }
        //    }
        //}

        private bool isValidNjesiAdministrative(ResourceManager rm, CultureInfo ci, int idNdermarrje, int idNderVit)
        {
            bool isValid;
            isValid = true;
            if (njesia == null)
            {
                isValid = false;
            }
            else
            {
                DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                if (dbRegjistrime.ekzistonKodNjesiAdministrative(DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), idNdermarrje) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
                {
                    isValid = false;
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeMagazinatEkzistonKjoMagazine", ci), pnlMesazhi);
                    return isValid;
                }
                dbRegjistrime.Dispose();
            }
            if (this.qendraKostos_TextBox.Text != "")
            {
                if (cmbLlojiQ.Value.ToString() == "1")
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
            if (cmbDegeAdministrative.Text != string.Empty && cmbDegeAdministrative.Text != " ()")
            {
                DbCore.DbRegjistrim.clsDegeAdministrative dege = new DbCore.DbRegjistrim.clsDegeAdministrative(cmbDegeAdministrative.Text.Split(' ')[0], idNdermarrje);
                if (dege.IdDegeAdministrative == -1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaNukEkzistonDegaAdmin", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return isValid;
                }
                else
                {
                    dege = new DbCore.DbRegjistrim.clsDegeAdministrative(cmbDegeAdministrative.Text.Split(' ')[0], idNdermarrje);
                    if (dege.Aktiv == false)
                    {
                        isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaJoAtkiveDegaAdmin", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return isValid;
                    }
                }
            }
            if (hfShtimModifikim.Value == "shtim" && int.Parse(cmbLloji.Value.ToString()) != 1 && ((hfStatusMagazine.Value == "" || hfStatusMagazine.Value == "0") || hfData.Value == ""))
            {
                isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeMagazinatZgjidhStatusMagDheDateFillimi", ci), pnlMesazhi);
                hfStatusi.Value = "false";
                return isValid;
            }
            if (hfShtimModifikim.Value == "shtim" && int.Parse(cmbLloji.Value.ToString()) != 1)
            {
                if (DateTime.Parse(hfData.Value).Year != new DbCore.DbAdmin.clsNdermarrjeViti(idNderVit).Viti)
                {
                    isValid = false;
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeMagazinatZgjidhDataNukEshteESakte", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return isValid;
                }
            }
            if (hfShtimModifikim.Value == "modifikim" && int.Parse(cmbLloji.Value.ToString()) != 1)
            {
                if (hfStatusMagazine.Value != "" && hfStatusMagazine.Value != "0" && hfData.Value == "")
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeMagazinatZgjidhDatenFillimStatusit", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return isValid;
                }
                if ((hfStatusMagazine.Value == "" || hfStatusMagazine.Value == "0") && hfData.Value != "")
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeMagazinatZgjidhStatusin", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return isValid;
                }
                if (hfStatusMagazine.Value != "" && hfStatusMagazine.Value != "0" && hfData.Value != "")
                {
                    if (DateTime.Parse(hfData.Value).Year != new DbCore.DbAdmin.clsNdermarrjeViti(idNderVit).Viti)
                    {
                        isValid = false;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeMagazinatZgjidhDataNukEshteESakte", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return isValid;
                    }
                    DbCore.DbAsete.clsHistorikStatusMagazine historik = new DbCore.DbAsete.clsHistorikStatusMagazine();
                    historik.merrHistorikMagazinaAktualeSipasIdNjesiAdministrative(int.Parse(hfId.Value));
                    if (historik.IdStatusMagazine == int.Parse(hfStatusMagazine.Value))
                    {
                        isValid = false;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeMagazinatEshteTashmeNeKeteStatus", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return isValid;
                    }
                    if (historik.DataStatusit >= DateTime.Parse(hfData.Value))
                    {
                        isValid = false;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeMagazinatVendosniDateMeTeMadheSeStatusiAktual", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return isValid;
                    }

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
            return isValid;
        }
        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvNjesiAdm_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvNjesiAdm.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNjesiAdm",  komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        gvNjesiAdm.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvNjesiAdm);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvNjesiAdm", gvNjesiAdm, kodkonfigurimi, idkomponente, (int)hfState["idGjuha"]);
            }
            else
            {
                idkomponente = e.Parameters;
            }
            gvNjesiAdm.Selection.UnselectAll();
        }

        #region grida e histroikut
        private void konfiguroVleraFillestareHistorik()
        {
            DbCore.DbAsete.colHistorikStatusMagazine col = new DbCore.DbAsete.colHistorikStatusMagazine();
            int id = 0;
            if (hfShtimModifikim.Value == "modifikim")
                id = int.Parse(hfId.Value.ToString());
            col.merrHistorikMagazinaSipasIdNjesiAdministrative(id);
            gvStatus.DataSource = col;
            gvStatus.DataBind();
        }

        private void konfiguroGrideHistorik()
        {
          
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvStatus, "gvStatus", komponente);
            KonfigurimComboGride.shto_StatusMagazine(gvStatus, idNdermarrje, Session, komponente, guidString);

            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvStatus, "IdHistorikuNjesiAdministrative");
            gvStatus.Settings.ShowFilterRow = false;
            gvStatus.SettingsBehavior.AllowSort = false;
            gvStatus.SettingsBehavior.AllowGroup = false;
            gvStatus.SettingsEditing.Mode = GridViewEditingMode.Inline;
            gvStatus.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
            percaktoTemplate();

        }
        private void percaktoTemplate()
        {
            GridViewDataComboBoxColumn col7 = gvStatus.Columns["IdStatusMagazine"] as GridViewDataComboBoxColumn;

            col7.EditItemTemplate = new MyComboTemplate();
            GridViewDataColumn col17 = gvStatus.Columns["IdHistorikuNjesiAdministrative"] as GridViewDataColumn;
            col17.EditItemTemplate = new MyLabelTemplate();
            col17.DataItemTemplate = new MyButtonTemplate("");
        }
  

   
        protected void gvStatus_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters != "")
            {
                string[] fusha = new string[1];
                fusha[0] = "IdHistorikuNjesiAdministrative";
                object id = gvStatus.GetRowValues(int.Parse(e.Parameters), fusha);
                DbCore.DbAsete.clsHistorikStatusMagazine historik = new DbCore.DbAsete.clsHistorikStatusMagazine();
                historik.merrHistorikMagazinaAktualeSipasIdNjesiAdministrative(int.Parse(hfId.Value));
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                if ((int)id != historik.IdHistorikuNjesiAdministrative)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgCeljeMagazinatStatusiNukMundTeFshihet", ci) + ":Red");
                    return;
                }
                if (gvStatus.VisibleRowCount == 1)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Nuk mund te fshini statusin e fundit te magazines" + "!:Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mund te fshini statusin e fundit te magazines", pnlMesazhi);
                    return;
                }
                DbCore.clsMesazh mesazh = historik.fshiTrans(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                if (!mesazh.Status)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + "!:Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgFshirjaPerfundoiMeSukses", ci) + ":Green");
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgFshirjaPerfundoiMeSukses", ci), pnlMesazhi);
                    modifikoNjesiNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), int.Parse(hfId.Value), rm, ci);
                }
            }
            konfiguroVleraFillestareHistorik();
            konfiguroGrideHistorik();
            gvStatus.AddNewRow();
        }

        protected void gvStatus_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvStatus.PageIndex;
            e.Properties["cpPageRow"] = gvStatus.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvStatus.VisibleRowCount;
        }

        protected void gvStatus_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.GridViewRowType.InlineEdit)
            {
                GridViewDataComboBoxColumn col1 = ((ASPxGridView)sender).Columns["IdStatusMagazine"] as GridViewDataComboBoxColumn;
                ASPxComboBox lbl1 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col1, "cmbBox") as ASPxComboBox;

                if (lbl1 != null)
                {
                    DbCore.DbAsete.colStatusMagazine_Asete col = new DbCore.DbAsete.colStatusMagazine_Asete();
                    col.Add(new DbCore.DbAsete.clsStatusMagazine_Asete());
                    col.merrStatusMagazineTePerdorshmeTeNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    lbl1.DataSource = col;
                    lbl1.TextField = "Emertimi";
                    lbl1.ValueField = "IdStatusMagazine";
                    lbl1.ClientInstanceName = "IdStatusMagazine";
                    lbl1.DataBind();
                }
            }
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataColumn col0 = ((ASPxGridView)sender).Columns["IdHistorikuNjesiAdministrative"] as GridViewDataColumn;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "btnFshi" + e.VisibleIndex.ToString();
                    btn0.ClientSideEvents.Click = "function(s,e){     e.processOnServer = false;	popFshiStatus.Show(); indexStatus= " + e.VisibleIndex.ToString() + "}";
                }
            }
        }
        #endregion



        protected void gvNjesiAdm_PreRender(object sender, EventArgs e)
        {

        }
        protected void qendraKostos_TextBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("qendraKostos_TextBox"))
                {
                    if (cmbLlojiQ.Value.ToString() == "1")
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
        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {

            try
            {
                gridExport.WriteXlsxToResponse("Magazinat", true);
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
                gridExport.WritePdfToResponse("Magazinat", true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }
    }
}