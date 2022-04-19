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
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaAcr : System.Web.UI.Page
    {
        //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin;
        private int idKonfigambjenti;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            DbCore.clsFunksione.percaktoThemeAmbjenteDheJQuery(Page, mySessionObjects.ktheIdPerdoruesi(Session));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String array = Request.QueryString["array"];

            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            nivel.Kodi = "ACR"; //mund te shihet ne DB ne T_NIVELREGJISTRIMI
            nivel.IdNdermarje = idNdermarrje;
            nivel = nivel.merrNivelRegjSipasKodi();
            percaktoTemplateMenu(ASPxMenu1,mySessionObjects.ktheIdVitNdermarrje(Session),mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
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
                    for (int i = 0; i < idte.Length - 1; i++)
                    {
                        konfigLupa.IdKonfigAmbjente = Convert.ToInt32(idte[i]);
                        konfigLupa = konfigLupa.merrSipasId();
                        if (konfigLupa.IdNivel == nivel.IdNivel)
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
                            merrKonfiguriminDefaultTeLupes(nivel.IdNivel);
                    }
                    else
                        merrKonfiguriminDefaultTeLupes(nivel.IdNivel);
            }
            else
            {
                merrKonfiguriminDefaultTeLupes(nivel.IdNivel);
            }

            if (idKonfigambjenti == -1)
                merrKonfiguriminDefaultTeLupes(nivel.IdNivel);
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(idNdermarrje, "gvLupaAcr", "LupaAcr.aspx");
            mbushPopUpListeACRsh();
            konfiguroPopupGride();

            if (!IsPostBack)
            {
            //    DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                AplikoFilterDefault();
            }

            //container.Attributes["width"] = "350px";
            //container.Attributes["height"] = "400px";
            //container.Attributes["src"] = "LupaFiltra.aspx?grida=gvLupaAcr&page=LupaAcr.aspx";
            
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
            clsToolbarConfig.percaktoTemplateMenu(idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaAcr.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false);
            aSPxMenu1.Items.FindByName("Anullo").Text = "Mbyll";

        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaAcr", "LupaAcr.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaAcr.FilterExpression;
            System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaAcr.GetSortedColumns();
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
                filtri.KoloneRenditje = "IdCR";
                filtri.DrejtimRenditje = true;
            }
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(idNdermarrje, "gvLupaAcr", "LupaAcr.aspx");
            percaktoTemplateMenu(ASPxMenu1, mySessionObjects.ktheIdVitNdermarrje(Session), mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            if (mesazh.StatusMesazhi == true)
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
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaAcr", "LupaAcr.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(idNdermarrje, "gvLupaAcr", "LupaAcr.aspx");
                percaktoTemplateMenu(ASPxMenu1, mySessionObjects.ktheIdVitNdermarrje(Session), mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.StatusMesazhi == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaAcr.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{            
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaAcr", "LupaAcr.aspx", idNdermarrje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}

        private void merrKonfiguriminDefaultTeLupes(int idNivel)
        {
            //do marr konfigurimin default per kete nivel regjistrimi i cili eshte i vetem per nje ndermarrje
            DbCore.DbShare.clsKonfigurimAmbjenti ambj = new clsKonfigurimAmbjenti();
            ambj.IdNivel = idNivel;
            DbCore.clsFunksione funk = new DbCore.clsFunksione(mySessionObjects.ktheCultureInfo(Session));
            ambj.IdNdermarje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            ambj = ambj.merrDefaultSipasNivel();
            idKonfigambjenti = ambj.IdKonfigAmbjente;
        }

        private void mbushPopUpListeACRsh()
        {//mbush griden e popupit me te dhena
            //dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            //DbCore.DbAdmin.colListeAmbjenteshCeljeRegjistrim colListeAmbjenteshCeljeRegjistrim = dbAdmin.merrGjitheListeAmbjenteshCeljeRegjistrim();
            DbCore.DbAdmin.colListeAmbjenteshCeljeRegjistrim colListeAmbjenteshCeljeRegjistrim = new DbCore.DbAdmin.colListeAmbjenteshCeljeRegjistrim();
            colListeAmbjenteshCeljeRegjistrim.mbushGjitheListeAmbjenteshCeljeRegjistrim();
            gvLupaAcr.DataSource = colListeAmbjenteshCeljeRegjistrim;
            gvLupaAcr.DataBind();
        }
        private void konfiguroPopupGride()
        {//konfiguron popupgriden
            DbCore.clsFunksione.percaktoVisibleColumnsSipasKonfigurimit(gvLupaAcr, "gvLupaAcr", "LupaAcr.aspx", idKonfigambjenti);
            //funk.konfiguroGrideListeMadhe(gvLupaAcr, "IdCR");
            DbCore.clsFunksione.konfiguroGrideListeMadhePopupiPaTheme(gvLupaAcr, "IdCR");
         }

        protected void gvLupaAcr_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaAcr.Settings.ShowFilterRow = true;

            gvLupaAcr.KeyFieldName = "IdCR";
            gvLupaAcr.SettingsBehavior.AllowSelectByRowClick = true;


        }

        protected void gvLupaAcr_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //kur popupgrida ben callback
            //  mbushPopUpListeLlogarish();
            //  konfiguroPopupGride();
            gvLupaAcr.Selection.UnselectAll();
        }

        protected void gvLupaAcr_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

        }

        //protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        //{
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
        //    //filtra.mbushFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaAcr", "LupaAcr.aspx", mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    filtra.mbushFilterPerGrideSipasKodit(Filtri_ASPxTextBox.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
        //    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    gvLupaAcr.FilterExpression = filtra.FiltraVlera;
        //    if (filtra.DrejtimRenditje == true)
        //        gvLupaAcr.SortBy(gvLupaAcr.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
        //    else
        //        gvLupaAcr.SortBy(gvLupaAcr.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);

        //    konfiguroPopupGride();
        //    this.Filtri_ASPxTextBox.Text = "";
        //}

        //protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        //{
            //if (Page.IsValid)
            //{
            //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            //    DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            //    filtri.FiltraKodi = Kodi_ASPxTextBox.Text;
            //    filtri.FiltraShenime = Shenime_ASPxTextBox.Text;
            //    filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            //    //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvLupaAcr", "LupaAcr.aspx",mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaAcr", "LupaAcr.aspx", mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //    filtri.GridaKokaId = koka.IdGridaKoka;
            //    filtri.FiltraVlera = gvLupaAcr.FilterExpression;
            //    System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaAcr.GetSortedColumns();
            //    if (kolona.Count > 0)
            //    {
            //        filtri.KoloneRenditje = kolona[0].FieldName;
            //        if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //            filtri.DrejtimRenditje = true;
            //        else
            //            filtri.DrejtimRenditje = false;
            //    }
            //    else
            //    {
            //        filtri.KoloneRenditje = "IdCR";
            //        filtri.DrejtimRenditje = true;
            //    }
            //    DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //    //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
            //    oPerdorues = mySessionObjects.kthePerdorues(Session);
            //    filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            //    filtri.IdNdermarje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //    filtri.IdStatusDok = 1;
            //    filtri.ruaj();
            //    Kodi_ASPxTextBox.Text = "";
            //    Shenime_ASPxTextBox.Text = "";
            //    //Universal_ASPxCheckBox.Text = "";
            //    popRuaj.ShowOnPageLoad = false;
            //}
        //}


        //protected void Kodi_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    args.IsValid = true;
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    //if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session)))
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaAcr", "LupaAcr.aspx", mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    if (DbCore.DbAdmin.clsFiltraGrida.ekzistonFilterSipasKoditPerGride(Kodi_ASPxTextBox.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka))
        //        args.IsValid = false;
        //    //dbAdmin.Dispose();
        //}

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

        private void AplikoFilterDefault()
        {
            DbCore.DbAdmin.clsFiltraGrida ofilter = new DbCore.DbAdmin.clsFiltraGrida();
            ofilter = merrFilterDefault(idKonfigambjenti);
            if (ofilter != null)
                gvLupaAcr.FilterExpression = ofilter.FiltraVlera;
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1,mySessionObjects.ktheIdVitNdermarrje(Session),mySessionObjects.ktheIdPerdoruesi(Session),mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvLupaAcr_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvLupaAcr.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaAcr", "LupaAcr.aspx", mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaAcr.FilterExpression = filtra.FiltraVlera;
                        if (filtra.DrejtimRenditje == true)
                            gvLupaAcr.SortBy(gvLupaAcr.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
                        else
                            gvLupaAcr.SortBy(gvLupaAcr.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);
                    }                    
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
            gvLupaAcr.Selection.UnselectAll();
        }
    }
}
