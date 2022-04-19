using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DbCore.DbRegjistrim;
using System.Data;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using DbCore;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaMagazina : MyPageBase
    {
        //private DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim;
        //private String veprimi;


        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            //nivel.Kodi = "MAG"; //eshte kodi i lupes, mund te shihet ne DB ne T_NIVELREGJISTRIMI
            //nivel.IdNdermarje = idNdermarrje;
            //nivel = nivel.merrNivelRegjSipasKodi();
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigAmbjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "MAG");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigAmbjenti.ToString();
                mbushPopUpListeNgaDB();
                konfiguroPopupGride(idKonfigAmbjenti, true, kerkosaposhkruar);
                GridUtil.AplikoFilterDefault(gvLupaMagazina, idKonfigAmbjenti);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaMagazina", idKonfigAmbjenti, "LupaMagazina.aspx");
                if (Request.QueryString["aqt"] != null && Request.QueryString["aqt"] == "true")
                    gvLupaMagazina.FilterExpression = "[IdLlojMagazine] = 2.0m Or [IdLlojMagazine] = 3.0m";
            }
            else
            {
                mbushPopUpListeNgaSession();
                konfiguroPopupGride(idKonfigAmbjenti, false, kerkosaposhkruar);
            }
            String array = Request.QueryString["array"];
        }

        private void percaktoTamplate()
        {
            GridViewDataColumn col = gvLupaMagazina.Columns["Aktiv"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }
        
        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            { mbushPopUpListeNgaDB(); return; }
            else
            {
                gvLupaMagazina.DataSource = tmpObject;
                gvLupaMagazina.DataBind();
            }
        }

        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena            
            //DbCore.clsFunksione funk = new DbCore.clsFunksione(DbCore.mySessionObjects.ktheCultureInfo(Session));
            //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            //oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);

            DataTable dt; bool kushtMerrMagMeAutorizim = true;
            if (!String.IsNullOrEmpty(Request.QueryString["kushtMerrMagMeAutorizim"]))
            {

                if (Request.QueryString["kushtMerrMagMeAutorizim"] == "Jo") kushtMerrMagMeAutorizim = false;
                else kushtMerrMagMeAutorizim = true;

            }


            if (!String.IsNullOrEmpty(Request.QueryString["vjenNga"]) && Request.QueryString["vjenNga"] == "gis")
            {
                int idLlojLayeri = 0;
                if (!String.IsNullOrEmpty(Request.QueryString["idLlojLayer"]) && Int32.TryParse(Request.QueryString["idLlojLayer"], out idLlojLayeri))
                    dt = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarjePerLupeDege(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), true, idLlojLayeri, true);
                else
                    dt = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarjePerLupeDege(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), true, -1, true);
            }
            else
                dt = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarjePerLupeDege(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false, -1, kushtMerrMagMeAutorizim);

            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaMagazina.DataSource = dt;
            gvLupaMagazina.DataBind();
            dt.Dispose();
        }

        private void mbushPopUpListe()
        {
            DbCore.DbRegjistrim.colNjesiAdministrative col = new colNjesiAdministrative();
         
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
          
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
     
            col.mbushGjitheNjesiAdministrativeAktive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
            gvLupaMagazina.DataSource = col;
            gvLupaMagazina.DataBind();
        }

        private void konfiguroPopupGride(int idKonfigAmbjenti, bool visibleIndex, bool kerkosaposhkruar)
        {
            percaktoTamplate();
            shto_Lloj();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaMagazina, "gvLupaMagazina", "LupaMagazina.aspx", idKonfigAmbjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));            
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaMagazina, "IdNjesiAdministrative", kerkosaposhkruar, endlessScroll);
        }

        private void shto_Lloj()
        {//shtohen komboja me Autorizimeve tek grida e KPF
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvLupaMagazina.Columns["IdLlojMagazine"].GetType())
            {
                gvLupaMagazina.Columns.Remove(gvLupaMagazina.Columns["IdLlojMagazine"]);
                gvLupaMagazina.Columns.Add(colnew);
                DbCore.DbAsete.colLlojNjesiAdministrative col = new DbCore.DbAsete.colLlojNjesiAdministrative();
                col.Add(new DbCore.DbAsete.clsLlojNjesiAdministrative());
                col.merrLlojNjesiAdministrative(); ;
                colnew.PropertiesComboBox.DataSource = col;
                colnew.PropertiesComboBox.TextField = "Emertimi";
                colnew.PropertiesComboBox.ValueField = "IdLlojNjesiAdministrative";
                colnew.FieldName = "IdLlojMagazine";

                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, col, "lloji");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvLupaMagazina.Columns["IdLlojMagazine"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "lloji");
                }
            }

        }
        protected void gvLupaMagazina_DataBound(object sender, EventArgs e)
        {
            gvLupaMagazina.Settings.ShowFilterRow = true;
            gvLupaMagazina.KeyFieldName = "IdNjesiAdministrative";
            gvLupaMagazina.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaMagazina_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            percaktoTamplate();
            if (String.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]) && !(String.IsNullOrEmpty(Request.QueryString["idKonfigAmbjente"])))
                gvLupaMagazina.Selection.UnselectAll();
        }


        protected void gvLupaMagazina_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLupaMagazina.PageIndex;
            e.Properties["cpPageRow"] = gvLupaMagazina.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLupaMagazina.VisibleRowCount;
        }

        protected void gvLupaMagazina_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaMagazina.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaMagazina", "LupaMagazina.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaMagazina.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaMagazina);
                    }
                }
            }
            if (String.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
                gvLupaMagazina.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
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
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaMagazina.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", cultinf);

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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaMagazina", "LupaMagazina.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaMagazina.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNjesiAdministrative", gvLupaMagazina);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaMagazina.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdNjesiAdministrative";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaMagazina", Convert.ToInt32(cmbKonfigurimi.Value), "LupaMagazina.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaMagazina", "LupaMagazina.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaMagazina", Convert.ToInt32(cmbKonfigurimi.Value), "LupaMagazina.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaMagazina.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaMagazina", "LupaMagazina.aspx", idNdermarrje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
    }
}