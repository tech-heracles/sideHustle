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
using System.Data;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaLlogaria : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LLOG");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            //    mbushPopUpListeLlogarish();
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                mbushHiddenFieldMePerkthime(ci, rm);
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                //AplikoFilterDefault();
                mbushPopUpListeLlogarishNgaDB(idNdermarrje, idPerdoruesi);
                GridUtil.AplikoFilterDefault(gvLupaLlog, idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaLlog", idKonfigambjenti, "LupaLlogaria.aspx");
            }
            else
            {
                mbushPopUpListeLlogarishNgaSession(idNdermarrje, idPerdoruesi);
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar);
            }
            if (Request.QueryString["veprimi"] == "LlogariAzhornimi" || Request.QueryString["vjenNga"] == "KonfigurimDokumentashBuxhetim")
            {
                gvLupaLlog.Columns["#"].VisibleIndex = 0;
            }
            //mbushComboBoxFiltra(idNdermarrje);            
        }
       

        private void mbushPopUpListeLlogarishNgaSession(int idNdermarrje, int idPerdoruesi)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            {
                mbushPopUpListeLlogarishNgaDB(idNdermarrje, idPerdoruesi);
                return;
            }
            gvLupaLlog.DataSource = tmpObject;
            gvLupaLlog.DataBind();
        }

        private void mbushPopUpListeLlogarishNgaDB(int idNdermarrje, int idPerdoruesi)
        {//mbush griden e popupit me te dhena            
            
            DbCore.DbKontabiliteti.colLlogarite colLlog = new DbCore.DbKontabiliteti.colLlogarite();
            DataTable dt = new DataTable();
            if (Request.QueryString["veprimi"] == "LlogariAzhornimi")
            {
                colLlog.mbushLLogariteAzhornimit(idNdermarrje, DbCore.DbAdmin.clsNdermarrje.ktheIdMonedheNdermSipasID(idNdermarrje));
                gvLupaLlog.DataSource = colLlog;
                DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, colLlog);
            }
            else if (Request.QueryString["vjennga"] == "RegjistrimQendraKostoDXDATAGRID" && Request.QueryString["id"] != null)
            {
                dt = DbCore.DbKontabiliteti.colLlogarite.ktheLlogariNdermarrjesAndAutorizimeDTDheKokaFletekontabelPerQK(idNdermarrje, idPerdoruesi, int.Parse(Request.QueryString["id"]));
                gvLupaLlog.DataSource = dt;
                DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            }
            else if (Request.QueryString["vjennga"] == "RegjistrimQendraKostoDXDATAGRID")
            {
                dt = DbCore.DbKontabiliteti.colLlogarite.ktheLlogariNdermarrjesAndAutorizimeDTTeMundshmePerQK(idNdermarrje, idPerdoruesi);
                gvLupaLlog.DataSource = dt;
                DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            }
            else if (!String.IsNullOrEmpty(Request.QueryString["Raportuese"]))
            {
                dt = DbCore.DbKontabiliteti.colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeAktivDTRaportuese(idNdermarrje, idPerdoruesi);
                DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
                gvLupaLlog.DataSource = dt;
            }
            else
            {
                var idNdermarrjeLlogari = !(String.IsNullOrEmpty(Request.QueryString["idNdermarrje"])) ? Convert.ToInt32(Request.QueryString["idNdermarrje"]) : idNdermarrje;
                var idPerdoruesLlogari = !(String.IsNullOrEmpty(Request.QueryString["idPerdoruesi"])) ? Convert.ToInt32(Request.QueryString["idPerdoruesi"]) : idPerdoruesi;
                dt = DbCore.DbKontabiliteti.colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeAktivDT(idNdermarrjeLlogari, idPerdoruesLlogari);
                DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
                gvLupaLlog.DataSource = dt;
            }
            //gvLupaArtikull.DataSource = colArtikujt;
            gvLupaLlog.DataBind();
            dt.Dispose();
        }
        private void mbushPopUpListeLlogarish(int idNdermarrje)
        {//mbush griden e popupit me te dhena
            //dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            DbCore.DbKontabiliteti.colLlogarite colLlog = new DbCore.DbKontabiliteti.colLlogarite();
            if (Request.QueryString["veprimi"] == "LlogariAzhornimi")
            {
                //DbCore.DbAdmin.clsNdermarrje nd = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                //DbCore.DbAdmin.clsNdermarrje nd = dbAdmin.ktheNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session))[0];
                colLlog.mbushLLogariteAzhornimit(idNdermarrje, DbCore.DbAdmin.clsNdermarrje.ktheIdMonedheNdermSipasID(idNdermarrje));
                //colLlog = dbKontabiliteti.merrLLogariteAzhornimit(nd.IdNdermarrje, funk.ktheNdermarrjeVit(), nd.NdermarrjeMonedha);
            }
            else
                colLlog.mbushLLogariteNdermarrjesAndAutorizime(idNdermarrje, oPerdorues.IdPerdorues);
            //colLlog = dbKontabiliteti.merrLLogariteNdermarrjesAndAutorizime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);

            gvLupaLlog.DataSource = colLlog;
            gvLupaLlog.DataBind();
        }

        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar)
        {//konfiguron popupgriden
            shtoMonedhepopup();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaLlog, "gvLupaLlog", "LupaLlogaria.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhe(gvLupaLlog, "IdLlogari");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaLlog, "IdLlogari", kerkosaposhkruar, endlessScroll);
        }

        private void shtoMonedhepopup()
        {// shton kombobox tek grida per monedhen
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int visibleindex = gvLupaLlog.Columns["IdMonedha"].VisibleIndex;
            gvLupaLlog.Columns.Remove(gvLupaLlog.Columns["IdMonedha"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colMonedhat colMonedhat = new DbCore.DbAdmin.colMonedhat();
            colMonedhat.mbushGjitheMonedhatAktive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            //colMonedhat = dbAdmin.merrGjitheMonedhatAktive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            colnew.PropertiesComboBox.DataSource = colMonedhat;
            colnew.PropertiesComboBox.TextField = "PershkrimiMonedha";
            colnew.PropertiesComboBox.ValueField = "IdMonedha";
            colnew.FieldName = "IdMonedha";
            colnew.VisibleIndex = visibleindex;
            gvLupaLlog.Columns.Add(colnew);
        }

        protected void gvLupaLlog_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            if (Request.QueryString["veprimi"] == "LlogariAzhornimi" || Request.QueryString["vjenNga"] == "KonfigurimDokumentashBuxhetim")
            {
                if (gvLupaLlog.Columns["#"] == null)
                {
                    DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                    check.Width = 20;
                    check.ShowSelectCheckbox = true;
                    //check.Width = Unit.Percentage(2);
                    //  check.SetColVisibleIndex(0);
                    gvLupaLlog.Columns.Add(check);
                    gvLupaLlog.SettingsBehavior.AllowSelectByRowClick = true;
                    gvLupaLlog.SettingsResizing.ColumnResizeMode = DevExpress.Web.ColumnResizeMode.Control;
                }
            }
            gvLupaLlog.Settings.ShowFilterRow = true;
            gvLupaLlog.KeyFieldName = "IdLlogari";
            gvLupaLlog.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaLlog_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //kur popupgrida ben callback
            string vjenNga = Request.QueryString["vjenNga"];
            if (String.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]) && vjenNga != "KonfigurimDokumentashBuxhetim" && vjenNga != "VeprimeBanka")
                gvLupaLlog.Selection.UnselectAll();
        }

        protected void gvLupaLlog_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaLlog.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaLlog", "LupaLlogaria.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaLlog.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaLlog);
                    }
                }
            }
            if (String.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
                gvLupaLlog.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaLlogaria.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
            aSPxMenu1.Items.FindByName("Shto").Text = rm.GetString("menuItemShtoLlogari", ci);
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaLlog", "LupaLlogaria.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaLlog.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdLlogari", gvLupaLlog);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaLlog.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdLlogari";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaLlog", Convert.ToInt32(cmbKonfigurimi.Value), "LupaLlogaria.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaLlog", "LupaLlogaria.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaLlog", Convert.ToInt32(cmbKonfigurimi.Value), "LupaLlogaria.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaLlog.FilterExpression = String.Empty;
            }
        }

      


        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("lupaShtoLlogari", rm.GetString("lupaShtoLlogari", cultinf));
            hfState.Set("headerPopUpKlonoLlogari", rm.GetString("headerPopUpKlonoLlogari", cultinf));
        }
    }
}
