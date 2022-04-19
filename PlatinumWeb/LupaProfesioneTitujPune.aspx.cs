using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbRegjistrim;
using System.Data;
using DbCore.IMBUtils.Logging;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class LupaProfesioneTitujPune : MyPageBase
    {
        private string gabimEkzistence = "Ekziston nje profesion me kete kod. Ju lutem shenoni nje tjeter!";
        private string gabimEkzistencePozicion = "Ekziston nje pozicion pune me kete kod. Ju lutem shenoni nje tjeter!";
        private string gabimEkzistence1 = "Ekziston nje profesion me kete pershkrim anglisht. Ju lutem shenoni nje tjeter!";
        private string gabimEkzistencePozicion1 = "Ekziston nje pozicion pune me kete pershkrim anglisht. Ju lutem shenoni nje tjeter!";
        private string gabimEkzistencePozicionShqip = "Ekziston nje pozicion pune me kete pershkrim. Ju lutem shenoni nje tjeter!";
        private int idKonfigAmbjenti;
        private int Lloji;
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            else
                vleraQueryString = "";
            Lloji = Request.QueryString["vjenNga"] == "PunonjesProfesione" ? 1 : 2;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            //nivel.Kodi = "LBUR"; //eshte kodi i lupes, mund te shihet ne DB ne T_NIVELREGJISTRIMI
            //nivel.IdNdermarje = idNdermarrje;
            //nivel = nivel.merrNivelRegjSipasKodi();
            int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("LPTP", idNdermarrje);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
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
                            idKonfigAmbjenti = Convert.ToInt32(idte[i]);
                        }
                    }
                }
                else
                    if (idte.Length == 1)
                    {
                        idKonfigAmbjenti = Convert.ToInt32(Request.QueryString["idKonfigAmbjente"]);
                        if (idKonfigAmbjenti == 0)
                            merrKonfiguriminDefaultTeLupes(idNivel);
                    }
                    else
                        merrKonfiguriminDefaultTeLupes(idNivel);//nqs nuk ia kemi kaluar si idKonfig ambjente i kalohet id e default
            }
            else
            {
                merrKonfiguriminDefaultTeLupes(idNivel);
            }
            //DbCore.DbShare.clsKusht kushtkss = new DbCore.DbShare.clsKusht(idKonfigAmbjenti, "KSSH");
            //DbCore.DbShare.clsAlternativaKushti alterkss = new DbCore.DbShare.clsAlternativaKushti(kushtkss.Vlera);
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigAmbjenti.ToString();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
             int   idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
              int  idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                //AplikoFilterDefault();
                GridUtil.AplikoFilterDefault(gvProfesione, idKonfigAmbjenti);
                mbushPopUpListeNgaDB();
                konfiguroPopupGride(true, kerkosaposhkruar);
                hfState.Set("idPerdoruesi", idperdoruesi);
                hfState.Set("idGjuha", DbCore.mySessionObjects.ktheGjuhe(Session));
                hfState.Set("idNdermarrje", idndermarje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvProfesione", idKonfigAmbjenti, "LupaProfesioneTitujPune.aspx");

            }
            else
            {
                mbushPopUpListeNgaSession();
                konfiguroPopupGride(false, kerkosaposhkruar);
            }
            //mbushComboBoxFiltra(idNdermarrje);
        }

        private void merrKonfiguriminDefaultTeLupes(int idNivel)
        {
            idKonfigAmbjenti = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idNivel);

        }


        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            { mbushPopUpListeNgaDB(); return; }
            gvProfesione.DataSource = tmpObject;
            gvProfesione.DataBind();
        }
        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena 
            
            

            DataTable dt = DbCore.DbListPagesat.colProfesioneTitujPune.merrProfesioneTitujPuneDTAktive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Lloji);
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvProfesione.DataSource = dt;
            gvProfesione.DataBind();
            dt.Dispose();
        }


        private void konfiguroPopupGride(bool visibleIndex, bool kerkosaposhkruar)
        {
        
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvProfesione, "gvProfesione", "LupaProfesioneTitujPune.aspx", idKonfigAmbjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvProfesione, "Id", kerkosaposhkruar, endlessScroll);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (Request.QueryString["vjenNga"] != "PunonjesProfesione")
            {
                gvProfesione.Columns["Pershkrimi"].Caption = rm.GetString("labelTitullshqip", ci);
                gvProfesione.Columns["PershkrimiAng"].Caption = rm.GetString("labelTitullanglisht", ci);
            }
            else gvProfesione.Columns["PershkrimiAng"].Visible = false;
        }

        protected void gvProfesione_DataBound(object sender, EventArgs e)
        {
            gvProfesione.Settings.ShowFilterRow = true;
            gvProfesione.KeyFieldName = "Id";
            gvProfesione.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvProfesione_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

            gvProfesione.Selection.UnselectAll();
        }


        protected void gvProfesione_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvProfesione.PageIndex;
            e.Properties["cpPageRow"] = gvProfesione.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvProfesione.VisibleRowCount;
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvProfesione_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvProfesione.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvProfesione", "LupaProfesioneTitujPune.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvProfesione.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvProfesione);
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
            gvProfesione.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaProfesioneTitujPune.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvProfesione", "LupaProfesioneTitujPune.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvProfesione.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Id", gvProfesione);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvProfesione.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Id";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvProfesione", Convert.ToInt32(cmbKonfigurimi.Value), "LupaProfesioneTitujPune.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvProfesione", "LupaProfesioneTitujPune.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvProfesione", Convert.ToInt32(cmbKonfigurimi.Value), "LupaProfesioneTitujPune.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvProfesione.FilterExpression = String.Empty;
            }
        }

        protected void gvProfesione_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView gride = sender as ASPxGridView;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "ProfesioneTitujPune.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gride, "Id");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            if (e.NewValues["Aktiv"] == null)
                e.NewValues["Aktiv"] = false;
            
            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAutoPerKod(hfNrAuto, e.NewValues["Kodi"].ToString());

            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasKod("PTP", idndermarje);
            DbCore.DbListPagesat.clsProfesioneTitujPune kategori = new DbCore.DbListPagesat.clsProfesioneTitujPune() { Kodi = e.NewValues["Kodi"].ToString(), Pershkrimi = e.NewValues["Pershkrimi"].ToString(), PershkrimiAng = (Request.QueryString["vjenNga"] == "PunonjesProfesione") ? e.NewValues["Pershkrimi"].ToString() : e.NewValues["PershkrimiAng"].ToString(), IdNdermarje = idndermarje, IdPerdoruesi = idperdoruesi, IdStatusDok = 1, IdKrijuesi = idperdoruesi, IdKonfig = konf.IdKonfigAmbjente, Aktiv = bool.Parse(e.NewValues["Aktiv"].ToString()) };
            if (Request.QueryString["vjenNga"] == "PunonjesProfesione") kategori.Lloji = 1;
            else  kategori.Lloji = 2;

            hfNrAutoKF = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "Kodi", "Kodi");
            e.Cancel = true;
            gride.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = kategori.ruaj(hfNrAutoKF);

            if (!mesazh)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNdodhiGabimRed", ci));
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgAdministrimiRuajtjaPerfundoiGabime", ci), pnlMesazhi);
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session,  rm.GetString("msgRuajtjeMeSuksesGreen", ci));
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo,  rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci), pnlMesazhi);
            }
            mbushPopUpListeNgaDB();
        }

        protected void rowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//per tu rregulluar me vone
         //validimi ne jane plotesuar gjithe fushat e detyruara
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            ASPxGridView grida = sender as ASPxGridView;
            foreach (GridViewColumn column in grida.Columns)
            {
                if (column.Visible)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    if (dataColumn.FieldName == "Aktiv" && e.NewValues[dataColumn.FieldName] == null)
                        e.NewValues[dataColumn.FieldName] = false;
                    if (e.NewValues[dataColumn.FieldName] == null)//validimi per kolonat e detyrueshme
                    {
                        e.Errors[dataColumn] = rm.GetString("msgVleraNukMundTeJeteNull", ci);
                    }
                    if (e.NewValues["Kodi"] == null)
                    {
                        e.Errors[dataColumn] = rm.GetString("msgKodiNukMundTeJeteBosh", ci);
                    }
                }
            }
            if (e.NewValues["Kodi"] != null)
            {
                try
                {

                    if (Lloji == 2 && DbCore.DbListPagesat.clsProfesioneTitujPune.ekzistonProfesionTitujPunePershkrimiAng(e.NewValues["PershkrimiAng"].ToString(), Convert.ToInt32(hfState.Get("idNdermarrje").ToString()), Lloji, 0))
                        e.RowError = gabimEkzistencePozicion1;

                    if (DbCore.DbListPagesat.clsProfesioneTitujPune.ekzistonProfesionTitujPunePershkrimi(e.NewValues["Pershkrimi"].ToString(), Convert.ToInt32(hfState.Get("idNdermarrje").ToString()), Lloji))
                        e.RowError = Lloji == 1 ? gabimEkzistence1 : gabimEkzistencePozicionShqip;

                    if (DbCore.DbListPagesat.clsProfesioneTitujPune.ekzistonProfesionTitujPune(e.NewValues["Kodi"].ToString(), Convert.ToInt32(hfState.Get("idNdermarrje").ToString()), Lloji))
                        e.RowError = Lloji == 1 ? gabimEkzistence : gabimEkzistencePozicion;

                   


                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex.Message);
                    e.RowError = ex.Message;
                }


            }
            else e.RowError = rm.GetString("msgKodiNukMundTeJeteBosh", ci);

            if (e.Errors.Count > 0)
            {
                e.RowError = rm.GetString("msgStrukturaAdministrativePlotesoFushat", ci);
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = rm.GetString("msgStrukturaAdministrativeKorrigjoGabimet", ci);
            }
        }

   

        protected void gvProfesione_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            grid.SettingsText.CommandUpdate = rm.GetString("btnUpdate", ci);
            grid.SettingsText.CommandCancel = rm.GetString("btnCancel", ci); ;
          
        }

    }
}