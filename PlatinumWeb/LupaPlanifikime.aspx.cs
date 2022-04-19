using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbRegjistrim;
using System.Data;
using DevExpress.Web;
using PlatinumWeb.Templates;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaPlanifikime : MyPageBase
    {
        private int idKonfigAmbjenti;
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            else
                vleraQueryString = "";
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            //nivel.Kodi = "LBUR"; //eshte kodi i lupes, mund te shihet ne DB ne T_NIVELREGJISTRIMI
            //nivel.IdNdermarje = idNdermarrje;
            //nivel = nivel.merrNivelRegjSipasKodi();
            int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("LPlan", idNdermarrje);
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
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                //AplikoFilterDefault();
                cmbKonfigurimi.Value = idKonfigAmbjenti.ToString();
                GridUtil.AplikoFilterDefault(gvPlanifikime, idKonfigAmbjenti);
                mbushPopUpListeNgaDB();
                konfiguroPopupGride(true, kerkosaposhkruar, DbCore.mySessionObjects.ktheGjuhe(Session));
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvPlanifikime", idKonfigAmbjenti, "LupaPlanifikime.aspx");
            }
            else
            {
                mbushPopUpListeNgaSession();
                konfiguroPopupGride(false, kerkosaposhkruar, DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            //mbushComboBoxFiltra(idNdermarrje);

        }

        private void merrKonfiguriminDefaultTeLupes(int idNivel)
        {
            //do marr konfigurimin default per kete nivel regjistrimi i cili eshte i vetem per nje ndermarrje
            //DbCore.DbShare.clsKonfigurimAmbjenti ambj = new DbCore.DbShare.clsKonfigurimAmbjenti();
            //ambj.IdNivel = idNivel;
            //ambj.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //ambj = ambj.merrDefaultSipasNivel();
            idKonfigAmbjenti = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idNivel);

        }



        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            { mbushPopUpListeNgaDB(); return; }
            gvPlanifikime.DataSource = tmpObject;
            gvPlanifikime.DataBind();
        }
        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbProdhimi.colKokaPlanifikim.merrKokaPlanifikimDT(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            var eFiltruar = dt.Select("Ngjyra= 'gri' or Ngjyra='verdhe'").GetDataTable(dt);
            gvPlanifikime.DataSource = eFiltruar;
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, eFiltruar);
            gvPlanifikime.DataBind();
            dt.Dispose();
        }


        private void konfiguroPopupGride(bool visibleIndex, bool kerkosaposhkruar, int idGjuha)
        {
            shtoStatus();
            shtoModel(idGjuha);
            shtoNivel();
            shtoMagazina();
            shtoColor();
            // shtoTip();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvPlanifikime, "gvPlanifikime", "LupaPlanifikime.aspx", idKonfigAmbjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvPlanifikime, "IdKokaPlanifikim", kerkosaposhkruar, endlessScroll);
        }
        private void shtoColor()
        {
            GridViewDataColumn g = gvPlanifikime.Columns["Ngjyra"] as GridViewDataColumn;
            g.DataItemTemplate = new MyGaugeTemplate();
        }

        /// <summary>
        /// kthen kolonen e idkonfigambjente ne kombo me kodet e konfigurimit
        /// </summary>
        private void shtoModel(int idGjuha)
        {
            gvPlanifikime.Columns.Remove(gvPlanifikime.Columns["IdKonfigAmbjente"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbShare.colKonfigurimAmbjenti colKonfig = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKategori = 44 };
            colKonfig.Add(new DbCore.DbShare.clsKonfigurimAmbjenti(0, "", "", 1, 0, true, 0, 0, 0, 0, 0, 0, 0, "", 0, 1));
            colKonfig.mbushKonfigAmbjSipasIdKategori(konf.IdKategori, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idGjuha);
            colnew.PropertiesComboBox.DataSource = colKonfig;
            colnew.PropertiesComboBox.TextField = "KodKonfigAmbjente";
            colnew.PropertiesComboBox.ValueField = "IdKonfigAmbjente";
            colnew.FieldName = "IdKonfigAmbjente";
            gvPlanifikime.Columns.Add(colnew);
        }
        /// <summary>
        private void shtoStatus()
        {
            gvPlanifikime.Columns.Remove(gvPlanifikime.Columns["IdStatusDok"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("", null);
            colnew.PropertiesComboBox.Items.Add("Draft", 0);
            colnew.PropertiesComboBox.Items.Add("Ruajtur", 1);
            colnew.FieldName = "IdStatusDok";
            gvPlanifikime.Columns.Add(colnew);
        }

        /// <summary>
        /// kthen kolonen e idnivelit ne kombo me kodet e nivelit
        /// </summary>
        private void shtoNivel()
        {
            gvPlanifikime.Columns.Remove(gvPlanifikime.Columns["IdNivel"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            ////DbCore.DbRegjistrim.colNivelRegjistrimi nivelet = new DbCore.DbRegjistrim.colNivelRegjistrimi();
            ////nivelet.Add(new DbCore.DbRegjistrim.clsNivelRegjistrimi(0, 0, "", "", 0, true, 0, 0, 0));
            ////nivelet.mbushGjitheNivelRegjistrimiSipasKategoriMeKonvertime(44, idndermarje, idperdoruesi);
            colnew.PropertiesComboBox.DataSource = DbCore.DbRegjistrim.colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(44, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), true);
            colnew.PropertiesComboBox.TextField = "Kodi";
            colnew.PropertiesComboBox.ValueField = "IdNivel";
            colnew.FieldName = "IdNivel";
            gvPlanifikime.Columns.Add(colnew);
        }

        /// <summary>
        /// kthen kolonen e idmagazines ne kombo me kodet e magazines
        /// </summary>
        private void shtoMagazina()
        {
            gvPlanifikime.Columns.Remove(gvPlanifikime.Columns["IdMagazina"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbRegjistrim.colNjesiAdministrative colMagazinat = new DbCore.DbRegjistrim.colNjesiAdministrative();
            colMagazinat.Add(new DbCore.DbRegjistrim.clsNjesiAdministrative(0, "", "", "", 0, false, true, 0, 0, DateTime.Today, 0, 0, 0, new DbCore.DbAdmin.colLidhjetAutorizim(), 0, 0, DateTime.Now, 0, 0, new DbCore.DbAsete.colHistorikStatusMagazine(), "", false, "", "", 1, 0, "", "", 0, 0, 0, false, false, false,"",0));
            colMagazinat.mbushGjitheNjesiAdministrative(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            colnew.PropertiesComboBox.DataSource = colMagazinat;
            colnew.PropertiesComboBox.TextField = "Kodi";
            colnew.PropertiesComboBox.ValueField = "IdNjesiAdministrative";
            colnew.FieldName = "IdMagazina";
            gvPlanifikime.Columns.Add(colnew);
        }


        protected void gvPlanifikime_DataBound(object sender, EventArgs e)
        {
            gvPlanifikime.Settings.ShowFilterRow = true;
            gvPlanifikime.KeyFieldName = "IdKokaPlanfikim";
            gvPlanifikime.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvPlanifikime_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

            gvPlanifikime.Selection.UnselectAll();
        }



        protected void gvPlanifikime_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvPlanifikime.PageIndex;
            e.Properties["cpPageRow"] = gvPlanifikime.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvPlanifikime.VisibleRowCount;
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvPlanifikime_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    gvPlanifikime.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvPlanifikime", "LupaPlanifikime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvPlanifikime.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvPlanifikime);
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
            gvPlanifikime.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaPlanifikime.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvPlanifikime", "LupaPlanifikime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvPlanifikime.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdKokaPlanifikim", gvPlanifikime);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvPlanifikime.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdKokaPlanifikim";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvPlanifikime", Convert.ToInt32(cmbKonfigurimi.Value), "LupaPlanifikime.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvPlanifikime", "LupaPlanifikime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvPlanifikime", Convert.ToInt32(cmbKonfigurimi.Value), "LupaPlanifikime.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvPlanifikime.FilterExpression = String.Empty;
            }
        }


    }
}