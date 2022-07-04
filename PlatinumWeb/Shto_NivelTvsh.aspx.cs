using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_NivelTvsh : MyPageBase
    {
        public static bool isShtim = true;

        DbCore.DbKontabiliteti.clsLlogari oLlog = new DbCore.DbKontabiliteti.clsLlogari();

        public static int id = 0;
        private int idgjuha, idviti, idPerdoruesi, idNdermarrje;
        public static DbCore.DbRegjistrim.clsTaksa taksa;
        private string komponente = "Shto_NivelTvsh.aspx";
        private string guidString;

        protected void Page_Init(object sender, EventArgs e)
        {
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
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
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
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdorues", idPerdoruesi);
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
                konfiguroGride(idNdermarrje, idPerdoruesi, cmbKonfigurimi.Text.Split(';')[0], 520, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvShtoNivelTvsh", gvShtoNivelTvsh, cmbKonfigurimi.Text.Split(';')[0], 520.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridNiveleshNgaSession();
                konfiguroGride(idNdermarrje, idPerdoruesi, cmbKonfigurimi.Text.Split(';')[0], 520, rm, cultinf);
            }
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvShtoNivelTvsh", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            GridUtil.konfigGrideListeEMadhePaTheme(gvShtoNivelTvsh, "IdTaksa");
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            GridUtil.EmrateButonaveMbiGride(gvShtoNivelTvsh);
            mbushComboTipiPerjashtimit();
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
            hfState.Set("msgTaksaDuetTeZgjidhniNjeTakse", rm.GetString("msgTaksaDuetTeZgjidhniNjeTakse", cultinf));
            hfState.Set("headerPopUpZgjidhAutorizimet", rm.GetString("headerPopUpZgjidhAutorizimet", cultinf));
            hfState.Set("popupAdministrimiUniversal", rm.GetString("popupAdministrimiUniversal", cultinf));
            hfState.Set("cmbItemBlerjeShitjeperqindje", rm.GetString("cmbItemBlerjeShitjeperqindje", cultinf));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("MenuItemTaksat", cultinf);
        }

        ///// <summary>
        /////      mbush combon e filtrave
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{

        //    //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("ASPxGridView_Llogarite", "Shto_Llogari.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvShtoNivelTvsh", "Shto_NivelTvsh.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());  //colFiltra = dbAdmin.merrGjitheFiltratGridaByGridaKoka(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
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
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idgjuha, "gvShtoNivelTvsh ", komponente, "FilterDefault", gvShtoNivelTvsh.FilterExpression, gvShtoNivelTvsh, "KodTaksa", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvShtoNivelTvsh, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 520, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvShtoNivelTvsh ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

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
        private void inicializoObjekte()
        {
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
        }
        protected void gvShtoNivelTvsh_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvShtoNivelTvsh.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //  check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvShtoNivelTvsh.Settings.ShowFilterRow = true;
                gvShtoNivelTvsh.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvShtoNivelTvsh.Settings.ShowFilterRowMenu = true;
                gvShtoNivelTvsh.Columns.Add(check);
                gvShtoNivelTvsh.KeyFieldName = "IdTaksa";
                gvShtoNivelTvsh.SettingsBehavior.AllowSelectByRowClick = true;
                gvShtoNivelTvsh.SettingsBehavior.AllowFocusedRow = true;
            } //gvShtoNivelTvsh.Columns["#"].VisibleIndex = 0;
        }


        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo ci, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            inicializoObjekte();
            //  mbushListeTaksash();

            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbLlogDebi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbLlogKredi); ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbLlogDog);
            mbushComboLloji();
            mbushComboNjesia(rm, ci);
            ConfigureAspxComboBox.mbushComboLlogaria(idNdermarrje, idPerdoruesi, cmbLlogDebi);
            ConfigureAspxComboBox.mbushComboLlogaria(idNdermarrje, idPerdoruesi, cmbLlogKredi); ConfigureAspxComboBox.mbushComboLlogaria(idNdermarrje, idPerdoruesi, cmbLlogDog);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 28, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            //cmbKonfigurimi.SelectedIndex = -1;
        }
        private void mbushGridNiveleshNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridNiveleshNgaDB();
            else
            {
                gvShtoNivelTvsh.DataSource = tmpObject;
                gvShtoNivelTvsh.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridNiveleshNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbRegjistrim.colTaksa.merrTaksaNdermarjeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvShtoNivelTvsh.DataSource = dt;
            gvShtoNivelTvsh.DataBind();
            dt.Dispose();
        }
        private void mbushListeTaksash(int idNdermarrje)
        {//mbush griden me te dhena
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();

            DbCore.DbRegjistrim.colTaksa col = new DbCore.DbRegjistrim.colTaksa(idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            //col.Add(new DbCore.DbRegjistrim.clsTaksa());
            //col.AddRange(dbRegjistrime.merrGjitheTaksa(idNderm,DbCore.mySessionObjects.ktheIdPerdoruesi(Session)));

            gvShtoNivelTvsh.DataSource = col;
            gvShtoNivelTvsh.DataBind();
        }
        private void mbushComboLloji()
        {
            DbCore.DbRegjistrim.colLlojTakse lloj = new DbCore.DbRegjistrim.colLlojTakse();
            lloj.mbushGjitheLlojeTaksash();
            //DbCore.DbRegjistrim.colLlojTakse lloj = dbRegjistrim.merrGjitheLlojeTaksash();
            cmbLloji.DataSource = lloj;
            cmbLloji.TextField = "Pershkrim";
            cmbLloji.ValueField = "IdLlojTakse";
            cmbLloji.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            cmbLloji.ClientSideEvents.SelectedIndexChanged = "function(s,e){SelectedIndexChangedLloji(0); }";
            cmbLloji.DataBind();
        }
        private void mbushComboNjesia(ResourceManager rm, CultureInfo ci)
        {
            cmbNjesia.Items.Add(rm.GetString("cmbItemBlerjeShitjevlere", ci), rm.GetString("cmbItemBlerjeShitjevlere", ci));
            cmbNjesia.Items.Add(rm.GetString("cmbItemBlerjeShitjeperqindje", ci), rm.GetString("cmbItemBlerjeShitjeperqindje", ci));
            cmbNjesia.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;

        }
        private void konfiguroGride(int idNdermarrje, int idPerdoruesi, string kodKonfigurimi, int idKomponente, ResourceManager rm, CultureInfo ci)
        {

            KonfigurimComboGride.shto_AutorizimSipasNdermarrjes(gvShtoNivelTvsh, idNdermarrje, Session, komponente, guidString);
            KonfigurimComboGride.shtoLlojTakse(gvShtoNivelTvsh, Session, komponente, guidString);
            KonfigurimComboGride.shto_Njesi(gvShtoNivelTvsh, rm, ci);
            KonfigurimComboGride.shto_Llogari(gvShtoNivelTvsh, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "LlogariDebi");
            KonfigurimComboGride.shto_Llogari(gvShtoNivelTvsh, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "LlogariDogane");
            KonfigurimComboGride.shto_Llogari(gvShtoNivelTvsh, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "LlogariKredi");
            KonfigurimComboGride.shtoAktivPoOseJo(gvShtoNivelTvsh, rm, ci, "FurnizimeZero");
           
            gvShtoNivelTvsh.SettingsEditing.NewItemRowPosition = DevExpress.Web.GridViewNewItemRowPosition.Top;
            //    mbushListeTaksash();
            GridViewDataTextColumn col3 = gvShtoNivelTvsh.Columns["NormaPerqindje"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            gvShtoNivelTvsh.Columns["#"].VisibleIndex = 0;
        }
        private void shto_TakseNdermarje()
        {
            gvShtoNivelTvsh.Columns.Remove(gvShtoNivelTvsh.Columns["TakseNdermarje"]);
            GridViewDataCheckColumn colnew = new GridViewDataCheckColumn();
            colnew.PropertiesCheckEdit.ValueChecked = "True";
            colnew.PropertiesCheckEdit.ValueUnchecked = "false";
            colnew.PropertiesCheckEdit.ValueGrayed = "";

            colnew.FieldName = "TakseNdermarje";
            gvShtoNivelTvsh.Columns.Add(colnew);
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvShtoNivelTvsh", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvShtoNivelTvsh", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                // konfiguroVleraFillestare();
                hfStatusi.Value = "true";
                gvShtoNivelTvsh.FilterExpression = String.Empty;
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
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("ASPxGridView_Llogarite", "Shto_Llogari.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvShtoNivelTvsh", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvShtoNivelTvsh.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodTaksa", gvShtoNivelTvsh);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvShtoNivelTvsh.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "KodTaksa";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvShtoNivelTvsh", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
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
                rreshtat = gvShtoNivelTvsh.GetSelectedFieldValues("IdTaksa");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //List<object> rreshtat = gvShtoNivelTvsh.GetSelectedFieldValues("IdTaksa");
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgTaksaZgjidhniTePakten1Takse", ci), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsTaksa taksa = new DbCore.DbRegjistrim.clsTaksa(Convert.ToInt32(id));

                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(taksa.IdKonfig);
                if (Convert.ToInt32(id) == nderm.IdTakse)
                {
                    TePaFshire.Add(taksa.KodTaksa);
                    continue;
                }
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(taksa.IdTaksa.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(taksa.KodTaksa);
                    continue;
                } taksa.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = taksa.Fshi();
                if (taksa.IdTaksa == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqNivelNgaGrida(taksa.IdTaksa, rm, ci);
                    #endregion
                    TeFshire.Add(taksa.KodTaksa);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgTaksaPrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgCeljeMagazinatSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgTaksaPrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgCeljeMagazinatSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgTaksaPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("msgClejeArkaBankaSuffixNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgTaksaPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgClejeArkaBankaSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }
        private void hiqNivelNgaGrida(int idnivel, ResourceManager rm, CultureInfo ci)
        {
            if (this.gvShtoNivelTvsh.DataSource != null)
            {
                DataTable dt = (DataTable)gvShtoNivelTvsh.DataSource;
                DataRow[] drs = dt.Select("IdTaksa = " + idnivel);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgTaksaNdodhen2TaksaMeTeNjejtenIdNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvShtoNivelTvsh.DataBind();
            }
            else mbushGridNiveleshNgaDB();
        }
        private void shtoNivelNeGrid(int idNdermarrje, int idnivel, ResourceManager rm, CultureInfo ci)
        {
            if (gvShtoNivelTvsh.DataSource != null)
            {
                DataTable dt = (DataTable)gvShtoNivelTvsh.DataSource;
                DataRow[] drs = dt.Select("IdTaksa = " + idnivel);
                if (drs.Length > 0)
                    throw new Exception(rm.GetString("msgTaksaTaksaEkzistonNeGride", ci));
                DataRow newArtDr = DbCore.DbRegjistrim.colTaksa.merrTaksaSipasNdermarjesDR(idNdermarrje, idnivel);
                dt.ImportRow(newArtDr);
            }
            else mbushGridNiveleshNgaDB();
        }
        private void modifikoNivelNeGrid(int idNdermarrje, int idnivel, ResourceManager rm, CultureInfo ci)
        {
            if (gvShtoNivelTvsh.DataSource != null)
            {
                DataTable dt = (DataTable)gvShtoNivelTvsh.DataSource;
                DataRow[] drs = dt.Select("IdTaksa = " + idnivel);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgTaksaNdodhen2TaksaMeTeNjejtenIdNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbRegjistrim.colTaksa.merrTaksaSipasNdermarjesDR(idNdermarrje, idnivel);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridNiveleshNgaDB();
        }

        protected DbCore.DbRegjistrim.clsTaksa krijoTakse(int idTaksa, bool shtim, string lidhurNderfaqe)
        {
            var konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, IdNdermarrja);

            var konvertuar = false;

            int idLlojTakse;
            konvertuar = int.TryParse(cmbLloji.Value.ToString(), out idLlojTakse);

            if (!konvertuar)
                throw new DbCore.MyException("Lloji i takses nuk eshte numer!");

            decimal perqindja;

            if (idLlojTakse == 3 && cmbNjesia.Text.ToLower() == "vlere" && txtPerqindja.Text.Trim() == string.Empty)
                perqindja = -1;
            else
            {
                konvertuar = Decimal.TryParse(txtPerqindja.Text, out perqindja);
                if (!konvertuar)
                    throw new DbCore.MyException("Norma nuk eshte numer!");
            }
            
            return new DbCore.DbRegjistrim.clsTaksa(MessagesResource.Messages,idTaksa, txtKodi.Text, txtPershkrimi.Text, perqindja, IdNdermarrja, cmbLlogDebi.Text, cmbLlogKredi.Text, idLlojTakse, cmbNjesia.Text, idPerdoruesi, konfig.IdKonfigAmbjente, 1, cbAktiv.Checked, cmbLlogDog.Text, cbPerjashtuar.Checked, cbAplikoTvshNeFleteDoganore.Checked, cbTakseNdermarje.Checked, hfAutorizime.Value.ToString(), cbFurnizimeZero.Checked, cbShitjePaTvshTaksa.Checked, shtim, lidhurNderfaqe,cmbTipiPerjashtimit.Text);
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
                RuajTaksa();
            }
        }

        protected void gvShtoNivelTvsh_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvShtoNivelTvsh.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvShtoNivelTvsh.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            GridUtil.EmrateButonaveMbiGride(gvShtoNivelTvsh);
        }

        protected void RuajTaksa()
        {
            if (Page.IsValid == false)
                return;

            DbCore.DbRegjistrim.clsTaksa taksa;
            var shtim = hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim";
            var id = shtim ? 0 : int.Parse(hfId.Value);

            try
            {
                taksa = krijoTakse(id, shtim, hfLidhur.Value);
            }
            catch (Exception ex)
            {
                shtoMesazhNeNderfaqe(ex.Message, "false");
                return;
            }

            var  mesazh = new DbCore.clsMesazh();
            bool eshteShtim;
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, komponente);
            if(taksa.TipiIPerjashtimit != "" && taksa.NormaPerqindje != 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(ASPxMenu1, "Taksa duhet te jete 0 nese ka tip përjashtimi", pnlMesazhi);
                return;
            }
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    shtoMesazhNeNderfaqe(MessagesResource.Messages["msgNukKeniTeDrejta"], "false");
                    return;
                }

                mesazh = taksa.Ruaj();
                eshteShtim = true;
            }

            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    shtoMesazhNeNderfaqe(MessagesResource.Messages["msgNukKeniTeDrejta"], "false");
                    return;
                }

                eshteShtim = false;
                mesazh = taksa.Modifiko();
            }

            if (mesazh.Status)
            {
                shtoMesazhNeNderfaqe(MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"], "true");

                if (eshteShtim)
                    shtoNivelNeGrid(IdNdermarrja, taksa.IdTaksa, rm, ci);
                else
                    modifikoNivelNeGrid(IdNdermarrja, taksa.IdTaksa, rm, ci);
            }
            else
                shtoMesazhNeNderfaqe(MessagesResource.Messages["msgAdministrimiRuajtjaPerfundoiGabime"], "false");

            ASPxPageControl1.ActiveTabIndex = 0;
        }

        protected void shtoMesazhNeNderfaqe(string mesazhi, string status)
        {
            if (status == "true")
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi, pnlMesazhi);
            hfStatusi.Value = status;
        }

        protected void gvShtoNivelTvsh_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvShtoNivelTvsh.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvShtoNivelTvsh", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        gvShtoNivelTvsh.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvShtoNivelTvsh);
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
            gvShtoNivelTvsh.Selection.UnselectAll();
        }

        protected void cmbLlogDebi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbLlogDebi"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbLlogDebi, e);
            }
        }

        protected void cmbLlogDebi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbLlogDebi"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbLlogDebi, e);
            }
        }

        protected void cmbLlogKredi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbLlogKredi"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbLlogKredi, e);
            }
        }

        protected void cmbLlogKredi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbLlogKredi"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbLlogKredi, e);
            }
        }

        protected void cmbLlogDog_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbLlogDog"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbLlogDog, e);
            }
        }

        protected void cmbLlogDog_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbLlogDog"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbLlogDog, e);
            }
        }

        protected void gvShtoNivelTvsh_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvShtoNivelTvsh.PageIndex;
            e.Properties["cpPageRow"] = gvShtoNivelTvsh.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvShtoNivelTvsh.VisibleRowCount;
        }

        protected void gvShtoNivelTvsh_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "KodTaksa" || e.Column.FieldName == "Pershkrimi")
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

        protected void gvShtoNivelTvsh_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Value.ToString() == "0")
            {
                e.Criteria = null;
            }
        }
        private void mbushComboTipiPerjashtimit()
        {
            cmbTipiPerjashtimit.Items.Add("");
            cmbTipiPerjashtimit.Items.Add("TYPE_1");
            cmbTipiPerjashtimit.Items.Add("TYPE_2");
            cmbTipiPerjashtimit.Items.Add("TAX_FREE");
            cmbTipiPerjashtimit.Items.Add("MARGIN_SCHEME");
        }
    }
}