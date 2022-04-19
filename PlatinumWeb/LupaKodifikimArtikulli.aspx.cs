using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using DbCore.DbRegjistrim;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaKodifikimArtikulli : MyPageBase
    {
        public static int idNdermVit = -1;
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        //private DbCore.DbInventari.clsDatabaseInventari dbInventari;
        protected void Page_Load(object sender, EventArgs e)
        {
            String array = Request.QueryString["array"];

            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            //clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            //nivel.Kodi = "KodArt"; //mund te shihet ne DB ne T_NIVELREGJISTRIMI
            //nivel.IdNdermarje = idNdermarrje;
            //nivel = nivel.merrNivelRegjSipasKodi();
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, idGjuha);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "KodArt");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            mbushPopUpListeKodifikimi(idNdermarrje);
            hfState.Set("idNdermarrje", idNdermarrje);
            if (!IsPostBack)
            {
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                //AplikoFilterDefault(); 
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaKodArt, idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar, idGjuha);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaKodArt", idKonfigambjenti, "LupaKodifikimArtikulli.aspx");
            }
            else
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar, idGjuha);
            //Container.Attributes["width"] = "350px";
            //Container.Attributes["height"] = "400px";
            //Container.Attributes["src"] = "LupaFiltra.aspx?grida=gvLupaKodArt&page=LupaKodifikimArtikulli.aspx";
            //mbushComboBoxFiltra(idNdermarrje);
            
        }
        private void mbushPopUpListeKodifikimi(int idNdermarrje)
        {//mbush griden e popupit me te dhena
            idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            DbCore.DbInventari.colKodifikimeArtikulli col = new DbCore.DbInventari.colKodifikimeArtikulli();
            int llojKodifikimi;
            bool llojartikulli;
            int nivelKodifikimi;
            if (!String.IsNullOrEmpty(Request.QueryString["llojKodifikimi"]))
            {
                llojKodifikimi = int.Parse(Request.QueryString["llojKodifikimi"]);
                if (!String.IsNullOrEmpty(Request.QueryString["llojartikulli"]))
                {
                    llojartikulli = bool.Parse(Request.QueryString["llojartikulli"]);
                    if (!String.IsNullOrEmpty(Request.QueryString["nivelKodifikimi"]))
                    {
                        nivelKodifikimi = Convert.ToInt32(Request.QueryString["nivelKodifikimi"]);
                        col.merrKodifikimArtikulliSipasLlojitDheNivelit(llojKodifikimi, idNdermarrje, llojartikulli, nivelKodifikimi);
                    }
                    else
                        col.merrKodifikimArtikulliSipasLlojit(llojKodifikimi, idNdermarrje, llojartikulli);
                }
                else
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["nivelKodifikimi"]))
                    {
                        nivelKodifikimi = Convert.ToInt32(Request.QueryString["nivelKodifikimi"]);
                        col.merrKodifikimArtikulliSipasLlojDheNivelKodifikimit(llojKodifikimi, idNdermarrje, nivelKodifikimi);
                    }
                    else
                        col.merrKodifikimArtikulliSipasLlojKodifikimit(llojKodifikimi, idNdermarrje);
                }
            }
            else
                col.mbushGjitheKodifikimetArtikulliSipasNdermarrjes(idNdermarrje);
            this.gvLupaKodArt.DataSource = col;
            gvLupaKodArt.DataBind();
            shtoPrind(idNdermarrje);
        }
        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar, int idGjuha)
        {//konfiguron popupgriden
            //shtoPrind();
            shtoLloji();
            shtoLlojArtikulli();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaKodArt, "gvLupaKodArt", "LupaKodifikimArtikulli.aspx", idKonfigambjenti, visibleIndex, idGjuha);
            //funk.konfiguroGrideListeMadhe(gvLupaKodArt, "IdKodifikimi");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKodArt, "IdKodifikimi", kerkosaposhkruar, endlessScroll);
            //DbCore.clsFunksione.konfiguroGrideListeMadhePopupiPaTheme(gvLupaKodArt, "IdKodifikimi");
        }

        private void shtoLloji()
        {//shton comboboxin e llojit
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int visibleindex = gvLupaKodArt.Columns["LlojKodifikimi"].VisibleIndex;
            gvLupaKodArt.Columns.Remove(gvLupaKodArt.Columns["LlojKodifikimi"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("1", 1);
            colnew.PropertiesComboBox.Items.Add("2", 2);
            colnew.FieldName = "LlojKodifikimi";
            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            colnew.VisibleIndex = visibleindex;
            gvLupaKodArt.Columns.Add(colnew);
        }

        private void shtoLlojArtikulli()
        {   //shton comboboxin e llojit te artikullit
            int visibleindex = gvLupaKodArt.Columns["LlojArtikulli"].VisibleIndex;
            gvLupaKodArt.Columns.Remove(gvLupaKodArt.Columns["LlojArtikulli"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("", "");
            colnew.PropertiesComboBox.Items.Add("Afatgjate", true);
            colnew.PropertiesComboBox.Items.Add("Afatshkurter", false);
            colnew.FieldName = "LlojArtikulli";
            colnew.Caption = "Lloj Artikulli";
            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            colnew.VisibleIndex = visibleindex;
            gvLupaKodArt.Columns.Add(colnew);
        }

        private void shtoPrind(int idNdermarrje)
        {
            gvLupaKodArt.Columns.Remove(gvLupaKodArt.Columns["IdPrindi"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbInventari.colKodifikimeArtikulli kodifikimet = new DbCore.DbInventari.colKodifikimeArtikulli();
            int llojKodifikimi;
            bool llojartikulli;
            if (!String.IsNullOrEmpty(Request.QueryString["llojKodifikimi"]))
            {
                llojKodifikimi = int.Parse(Request.QueryString["llojKodifikimi"]);
                if (!String.IsNullOrEmpty(Request.QueryString["llojartikulli"]))
                {
                    llojartikulli = bool.Parse(Request.QueryString["llojartikulli"]);
                    kodifikimet.merrKodifikimArtikulliSipasLlojit(llojKodifikimi, idNdermarrje, llojartikulli);
                }
                else kodifikimet.merrKodifikimArtikulliSipasLlojKodifikimit(llojKodifikimi, idNdermarrje);
            }
            else
                kodifikimet.mbushGjitheKodifikimetArtikulliSipasNdermarrjes(idNdermarrje);
            //kodifikimet.mbushGjitheKodifikimetArtikulliSipasNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //kodifikimet = dbInventari.merrGjitheKodifikimetArtikulliSipasNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbInventari.clsKodifikimArtikulli kod = new DbCore.DbInventari.clsKodifikimArtikulli();
            kod.IdKodifikimi = 0;
            kodifikimet.Add(kod);
            colnew.PropertiesComboBox.DataSource = kodifikimet;
            colnew.PropertiesComboBox.TextField = "PershkrimKodifikimi";
            colnew.PropertiesComboBox.ValueField = "IdKodifikimi";
            colnew.FieldName = "IdPrindi";
            gvLupaKodArt.Columns.Add(colnew);
        }



        protected void gvLupaKodArt_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaKodArt.Settings.ShowFilterRow = true;


            gvLupaKodArt.KeyFieldName = "IdKodifikimi";
            gvLupaKodArt.SettingsBehavior.AllowSelectByRowClick = true;


        }

        protected void gvLupaKodArt_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //kur popupgrida ben callback

           // gvLupaKodArt.Selection.UnselectAll();
        }

        protected void gvLupaKodArt_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

        }



        //protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        //{
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
        //    //filtra.mbushFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    filtra.mbushFilterPerGrideSipasKodit(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
        //    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    gvLupaKodArt.FilterExpression = filtra.FiltraVlera;
        //    if (filtra.DrejtimRenditje == true)
        //        gvLupaKodArt.SortBy(gvLupaKodArt.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
        //    else
        //        gvLupaKodArt.SortBy(gvLupaKodArt.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);

        //    konfiguroPopupGride();
        //    this.Filtri_ASPxTextBox.Text = "";
        //}

        //protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //    {
        //        //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //        DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
        //        filtri.FiltraKodi = Kodi_ASPxTextBox.Text;
        //        filtri.FiltraShenime = Shenime_ASPxTextBox.Text;
        //        filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
        //        //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvLupaKodArt", "LupaKodifikimArtikulli.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKodArt", "LupaKodifikimArtikulli.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        filtri.GridaKokaId = koka.IdGridaKoka;
        //        filtri.FiltraVlera = gvLupaKodArt.FilterExpression;
        //        System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaKodArt.GetSortedColumns();
        //        if (kolona.Count > 0)
        //        {
        //            filtri.KoloneRenditje = kolona[0].FieldName;
        //            if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
        //                filtri.DrejtimRenditje = true;
        //            else
        //                filtri.DrejtimRenditje = false;
        //        }
        //        else
        //        {
        //            filtri.KoloneRenditje = "IdKodifikimi";
        //            filtri.DrejtimRenditje = true;
        //        }
        //        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        //        //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
        //        oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
        //        filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
        //        filtri.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
        //        filtri.IdStatusDok = 1;
        //        filtri.ruaj();
        //        Kodi_ASPxTextBox.Text = "";
        //        Shenime_ASPxTextBox.Text = "";
        //        //Universal_ASPxCheckBox.Text = "";
        //        popRuaj.ShowOnPageLoad = false;
        //    }
        //}


        //protected void Kodi_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    args.IsValid = true;
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    //if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKodArt", "LupaKodifikimArtikulli.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    if (DbCore.DbAdmin.clsFiltraGrida.ekzistonFilterSipasKoditPerGride(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka))
        //        args.IsValid = false;
        //    //dbAdmin.Dispose();
        //}

        //private DbCore.DbAdmin.clsFiltraGrida merrFilterDefault(int idkonfigAmbjenti)
        //{
        //    DbCore.DbAdmin.clsFiltraGrida ofiltri = new DbCore.DbAdmin.clsFiltraGrida();
        //    DbCore.DbShare.clsKusht oKusht = new DbCore.DbShare.clsKusht();
        //    DbCore.DbShare.colKusht colKushtet = new DbCore.DbShare.colKusht();

        //    oKusht.IdKonfigurimAmbjente = idkonfigAmbjenti;
        //    colKushtet = oKusht.merrTeGjitheKushteKonfigurimi(idkonfigAmbjenti);
        //    if (colKushtet.Count > 0)
        //    {
        //        oKusht = colKushtet[0]; //cdo konfigurim ambjenti per LUPAT ka vetem nje kusht qe eshte filtri default i grides
        //        ofiltri.IdFiltra = oKusht.Vlera;
        //        ofiltri = ofiltri.merrFilterSipasId();
        //    }
        //    return ofiltri;
        //}

        //private void AplikoFilterDefault()
        //{
        //    DbCore.DbAdmin.clsFiltraGrida ofilter = new DbCore.DbAdmin.clsFiltraGrida();
        //    ofilter = merrFilterDefault(idKonfigambjenti);
        //    if (ofilter != null)
        //        gvLupaKodArt.FilterExpression = ofilter.FiltraVlera;
        //}

        protected void gvLupaKodArt_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaKodArt.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKodArt", "LupaKodifikimArtikulli.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaKodArt.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaKodArt);
                    }
                }
            }
            gvLupaKodArt.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheGjuhe(Session));
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje, int idGjuha)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaKodifikimArtikulli.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }


        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvLupaKodArt", "LupaKodifikimArtikulli.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaKodArt.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdKodifikimi", gvLupaKodArt);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaKodArt.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdKodifikimi";
            //    filtri.DrejtimRenditje = true;
            //}
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaKodArt", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKodifikimArtikulli.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje, idGjuha);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvLupaKodArt", "LupaKodifikimArtikulli.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                filtra.IdPerdoruesi = idPerdoruesi;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaKodArt", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKodifikimArtikulli.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje, idGjuha);
                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                gvLupaKodArt.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKodArt", "LupaKodifikimArtikulli.aspx", idNdermarrje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
    }
}