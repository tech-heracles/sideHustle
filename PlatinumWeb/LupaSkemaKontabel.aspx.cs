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
using System.Globalization;
using System.Resources;

namespace PlatinumWeb
{
    public partial class LupaSkemaKontabel : System.Web.UI.Page
    {
        //private DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti;
        private int idKonfigambjenti;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            DbCore.clsFunksione.percaktoThemeAmbjenteDheJQuery(Page, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            //nivel.Kodi = "SkKont"; //eshte kodi i lupes, mund te shihet ne DB ne T_NIVELREGJISTRIMI
            //nivel.IdNdermarje = idNdermarrje;
            //nivel = nivel.merrNivelRegjSipasKodi();
            int idNivel = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("SkKont", idNdermarrje);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), idNdermarrje, "gvLupaSkemaKont", 1, "LupaSkemaKontabel.aspx");
            if (vleraQueryString != "")
            {
                //ketu me intereson id e nivelit
                //ne rastin kur kemi disa konfigurime te lupes per kontrollin i cili gjeneroi thirrjen e kesaj lupe
                //duhet te gjejme cili nga konfigurimet e ka idNivel sa niveli i artikujve
                //i kontrollojme me radhe te gjitha konfigurimet qe i jane kaluar ne query string
                string[] idte = vleraQueryString.Split('-');
                if (idte.Length > 1)
                {
                    DbCore.DbShare.clsKonfigurimAmbjenti konfigLupa = new DbCore.DbShare.clsKonfigurimAmbjenti();
                    for (int i = 0; i < idte.Length; i++)
                    {
                        konfigLupa.IdKonfigAmbjente = Convert.ToInt32(idte[i]);
                        konfigLupa = konfigLupa.merrSipasId();
                        if (konfigLupa.IdNivel == idNivel)
                        {
                            idKonfigambjenti = Convert.ToInt32(idte[i]);
                        }
                    }
                }
                else
                    if (idte.Length == 1)
                    {
                        idKonfigambjenti = Convert.ToInt32(Request.QueryString["idKonfigAmbjente"].ToString());
                        if (idKonfigambjenti == 0)
                            merrKonfiguriminDefaultTeLupes(idNivel);
                    }
                    else
                        merrKonfiguriminDefaultTeLupes(idNivel);//nqs nuk ia kemi kaluar si idKonfig ambjente i kalohet id e default
            }
            else
            {
                merrKonfiguriminDefaultTeLupes(idNivel);
            } 
            //DbCore.DbShare.clsKusht kushtkss = new DbCore.DbShare.clsKusht(idKonfigambjenti, "KSSH");
            //DbCore.DbShare.clsAlternativaKushti alterkss = new DbCore.DbShare.clsAlternativaKushti(kushtkss.Vlera);
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            mbushPopUpListeSkemaKotabel();
            if (!IsPostBack)
            {
                DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                konfiguroPopupGride(true, kerkosaposhkruar);
            }
            else
            {
                konfiguroPopupGride(false, kerkosaposhkruar);
            }

            //container.Attributes["width"] = "340px";
            //container.Attributes["height"] = "500px";
            //container.Attributes["src"] = "LupaFiltra.aspx?grida=gvLupaSkemaKont&page=LupaSkemaKontabel.aspx";
            DbCore.clsFunksione.AplikoFilterDefault(gvLupaSkemaKont, idKonfigambjenti);
        }
        private void merrKonfiguriminDefaultTeLupes(int idNivel)
        {
            //do marr konfigurimin default per kete nivel regjistrimi i cili eshte i vetem per nje ndermarrje
            //DbCore.DbShare.clsKonfigurimAmbjenti ambj = new DbCore.DbShare.clsKonfigurimAmbjenti(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idNivel);
            ////ambj.IdNivel = idNivel;
            ////ambj.IdNdermarje = ;
            ////ambj = ambj.merrDefaultSipasNivel();
            //idKonfigambjenti = ambj.IdKonfigAmbjente;
            idKonfigambjenti = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idNivel);
        }
        private DbCore.DbAdmin.clsFiltraGrida merrFilterDefault(int idkonfigAmbjenti)
        {
            DbCore.DbAdmin.clsFiltraGrida ofiltri = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbShare.clsKusht oKusht = new DbCore.DbShare.clsKusht();
            DbCore.DbShare.colKusht colKushtet = new DbCore.DbShare.colKusht();

            oKusht.IdKonfigurimAmbjente = idkonfigAmbjenti;
            colKushtet = oKusht.merrTeGjitheKushteKonfigurimi(idkonfigAmbjenti);
            if (colKushtet.Count > 0)
            {
                oKusht = colKushtet[0]; //cdo konfigurim ambjenti per LUPAT ka vetem nje kusht qe eshte filtri default i grides
                ofiltri.IdFiltra = oKusht.Vlera;
                ofiltri = ofiltri.merrFilterSipasId();
            }
            return ofiltri;
        }

        //private void AplikoFilterDefault()
        //{
        //    DbCore.DbAdmin.clsFiltraGrida ofilter = new DbCore.DbAdmin.clsFiltraGrida();
        //    ofilter = merrFilterDefault(idKonfigambjenti);
        //    if (ofilter != null)
        //        gvLupaSkemaKont.FilterExpression = ofilter.FiltraVlera;
        //}


        private void mbushPopUpListeSkemaKotabel()
        {//mbush griden e popupit me te dhena
            //dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            //DbCore.DbKontabiliteti.colSkemaKontabelKoka colSkema = dbKontabiliteti.merrGjitheSkematKontabelDefault(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //gvLupaSkemaKont.DataSource = colSkema;
            //gvLupaSkemaKont.DataBind();
            DbCore.DbKontabiliteti.colSkemaKontabelKoka colSkema = new DbCore.DbKontabiliteti.colSkemaKontabelKoka();
            if (Request.QueryString.ToString() == "")
            {
                DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
                //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
                oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
                //colSkema = dbKontabiliteti.merrGjitheSkematKontabelAndAutorizim(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
                colSkema.mbushGjitheSkematKontabelAndAutorizim(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
                gvLupaSkemaKont.DataSource = colSkema;
                gvLupaSkemaKont.DataBind();
            }
            else
            {
                if (int.Parse(Request.QueryString["default"]) == 1)
                {
                    colSkema.mbushGjitheSkematKontabelDefault(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //colSkema = dbKontabiliteti.merrGjitheSkematKontabelDefault(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                }
                else
                {
                    DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
                    //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
                    oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
                    //colSkema = dbKontabiliteti.merrGjitheSkematKontabelAndAutorizim(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
                    colSkema.mbushGjitheSkematKontabelAndAutorizim(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
                }
                gvLupaSkemaKont.DataSource = colSkema;
                gvLupaSkemaKont.DataBind();
            }
        }

        private void konfiguroPopupGride(bool visibleIndex, bool kerkosaposhkruar)
        {//konfiguron popupgriden
            DbCore.clsFunksione.percaktoVisibleColumnsSipasKonfigurimit(gvLupaSkemaKont, "gvLupaSkemaKont", "LupaSkemaKontabel.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session));
            //funk.konfiguroGrideListeMadhe(gvLupaSkemaKont, "IdSkemaKontabelKoka");
            DbCore.clsFunksione.konfiguroGrideListeMadhePopupiPaTheme(gvLupaSkemaKont, "IdSkemaKontabelKoka", kerkosaposhkruar);
        }



        private void shtoMonedhepopup()
        {// shton kombobox tek grida per monedhen
            ////DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            ////gvLupaSkemaKont.Columns.Remove(gvLupaSkemaKont.Columns["IdMonedha"]);
            ////GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            ////DbCore.DbAdmin.colMonedhat colMonedhat = new DbCore.DbAdmin.colMonedhat();
            ////colMonedhat = dbAdmin.merrGjitheMonedhat();
            ////colnew.PropertiesComboBox.DataSource = colMonedhat;
            ////colnew.PropertiesComboBox.TextField = "KodiMonedha";
            ////colnew.PropertiesComboBox.ValueField = "IdMonedha";
            ////colnew.FieldName = "IdMonedha";
            ////gvLupaSkemaKont.Columns.Add(colnew);
        }

        protected void gvLupaSkemaKont_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaSkemaKont.Settings.ShowFilterRow = true;

            gvLupaSkemaKont.KeyFieldName = "IdSkemaKontabelKoka";
            gvLupaSkemaKont.SettingsBehavior.AllowSelectByRowClick = true;


        }

        protected void gvLupaSkemaKont_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //kur popupgrida ben callback
            //  mbushPopUpListeLlogarish();
            //  konfiguroPopupGride();
            gvLupaSkemaKont.Selection.UnselectAll();
        }


        //protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        //{
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
        //    //filtra.mbushFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaSkemaKont", "LupaSkemaKontabel.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    filtra.mbushFilterPerGrideSipasKodit(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
        //    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    gvLupaSkemaKont.FilterExpression = filtra.FiltraVlera;
        //    if (filtra.DrejtimRenditje == true)
        //        gvLupaSkemaKont.SortBy(gvLupaSkemaKont.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
        //    else
        //        gvLupaSkemaKont.SortBy(gvLupaSkemaKont.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);

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
        //        //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvLupaSkemaKont", "LupaSkemaKontabel.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaSkemaKont", "LupaSkemaKontabel.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        filtri.GridaKokaId = koka.IdGridaKoka;
        //        filtri.FiltraVlera = gvLupaSkemaKont.FilterExpression;
        //        System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaSkemaKont.GetSortedColumns();
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
        //            filtri.KoloneRenditje = "IdSkemaKontabelKoka";
        //            filtri.DrejtimRenditje = true;
        //        }
        //        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        //        //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
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
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaSkemaKont", "LupaSkemaKontabel.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    if (DbCore.DbAdmin.clsFiltraGrida.ekzistonFilterSipasKoditPerGride(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka))
        //        args.IsValid = false;
        //    //dbAdmin.Dispose();
        //}

        protected void gvLupaSkemaKont_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaSkemaKont.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), "gvLupaSkemaKont", "LupaSkemaKontabel.aspx", idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaSkemaKont.FilterExpression = filtra.FiltraVlera;
                        if (filtra.DrejtimRenditje == true)
                            gvLupaSkemaKont.SortBy(gvLupaSkemaKont.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
                        else
                            gvLupaSkemaKont.SortBy(gvLupaSkemaKont.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);
                    }
                }
            }
            gvLupaSkemaKont.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
               percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session),  DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaSkemaKontabel.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);

        }
      

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), "gvLupaSkemaKont", "LupaSkemaKontabel.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaSkemaKont.FilterExpression;
            System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaSkemaKont.GetSortedColumns();
            if (kolona.Count > 0)
            {
                filtri.KoloneRenditje = kolona[0].FieldName;
                if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
                    filtri.DrejtimRenditje = true;
                else
                    filtri.DrejtimRenditje = false;
            }
            else
            {
                filtri.KoloneRenditje = "IdSkemaKontabelKoka";
                filtri.DrejtimRenditje = true;
            }
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), idNdermarrje, "gvLupaSkemaKont", 1, "LupaSkemaKontabel.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
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
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), "gvLupaSkemaKont", "LupaSkemaKontabel.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), idNdermarrje, "gvLupaSkemaKont", 1, "LupaSkemaKontabel.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaSkemaKont.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaSkemaKont", "LupaSkemaKontabel.aspx", idNdermarrje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}

    }
}
