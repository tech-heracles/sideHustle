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
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaNivelCmimiPrind : MyPageBase
    {
        public static int idNdermVit = -1;
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        //private DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti;
        protected void Page_Load(object sender, EventArgs e)
        {
            String array = Request.QueryString["array"];
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            //nivel.Kodi = "NivCmPrind"; //mund te shihet ne DB ne T_NIVELREGJISTRIMI
            //nivel.IdNdermarje = idNdermarrje;
            //nivel = nivel.merrNivelRegjSipasKodi();
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            //mbushComboBoxFiltra(idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "NivCmPrind");
            bool kerkosaposhkruar = true;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            mbushPopUpListeNiveleCmimi();
            if (!IsPostBack)
            {
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaNivCmPrind, idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNivCmPrind", idKonfigambjenti, "LupaNivelCmimiPrind.aspx");
            }
            else
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar);
            ////Container.Attributes["width"] = "350px";
            ////Container.Attributes["height"] = "400px";
            ////Container.Attributes["src"] = "LupaFiltra.aspx?grida=gvLupaNivCmPrind&page=LupaNivelCmimiPrind.aspx";
        }
        private void mbushPopUpListeNiveleCmimi()
        {//mbush griden e popupit me te dhena            
            string KF = "";
            KF = Request.QueryString["KF"];
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            DbCore.DbInventari.colNiveleCmimesh colNiveleCmimesh = new DbCore.DbInventari.colNiveleCmimesh();
            colNiveleCmimesh.mbushGjitheNiveleCmimeshPrindiSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //DbCore.DbInventari.colNiveleCmimesh colNiveleCmimesh = dbInventari.merrGjitheNiveleCmimeshPrindiSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbInventari.colNiveleCmimesh colNiveleShitje = new DbCore.DbInventari.colNiveleCmimesh();
            DbCore.DbInventari.colNiveleCmimesh colNiveleBlerje = new DbCore.DbInventari.colNiveleCmimesh();

            foreach (DbCore.DbInventari.clsNivelCmimi c in colNiveleCmimesh)
            {
                if (c.LlojiNivelCmimi == 0)
                    colNiveleShitje.Add(c);
                else
                    colNiveleBlerje.Add(c);
            }
            if (KF == "Klient")
            { gvLupaNivCmPrind.DataSource = colNiveleShitje; }
            else
            {
                if (KF == "Furnitor")
                {
                    gvLupaNivCmPrind.DataSource = colNiveleBlerje;
                }
                else
                {
                    this.gvLupaNivCmPrind.DataSource = colNiveleCmimesh;
                }
            }
            gvLupaNivCmPrind.DataBind();
        }

        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar)
        {//konfiguron popupgriden
            shtoLloj();
            shtoMonedhe();
            shtoBrutoNeto();
            shtoPrioritet();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaNivCmPrind, "gvLupaNivCmPrind", "LupaNivelCmimiPrind.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhe(gvLupaNivCmPrind, "IdNivelCmimi");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaNivCmPrind, "IdNivelCmimi", kerkosaposhkruar, endlessScroll);
        }

        private void shtoLloj()
        {//mbush combon e llojit
            int visibleindex = gvLupaNivCmPrind.Columns["LlojiNivelCmimi"].VisibleIndex;
            gvLupaNivCmPrind.Columns.Remove(gvLupaNivCmPrind.Columns["LlojiNivelCmimi"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("Cmim Shitje", 0);
            colnew.PropertiesComboBox.Items.Add("Cmim Blerje", 1);
            colnew.FieldName = "LlojiNivelCmimi";
            colnew.VisibleIndex = visibleindex;
            gvLupaNivCmPrind.Columns.Add(colnew);
        }

        private void shtoBrutoNeto()
        {//mbush combon e brutoneto
            int visibleindex = gvLupaNivCmPrind.Columns["BrutoNetoNivelCmimi"].VisibleIndex;
            gvLupaNivCmPrind.Columns.Remove(gvLupaNivCmPrind.Columns["BrutoNetoNivelCmimi"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("", 0);
            colnew.PropertiesComboBox.Items.Add("TVSH", 1);
            colnew.FieldName = "BrutoNetoNivelCmimi";
            colnew.VisibleIndex = visibleindex;
            gvLupaNivCmPrind.Columns.Add(colnew);
        }

        private void shtoPrioritet()
        {//mbush combon e brutoneto
            int visibleindex = gvLupaNivCmPrind.Columns["PrioritetiNivelCmimi"].VisibleIndex;
            gvLupaNivCmPrind.Columns.Remove(gvLupaNivCmPrind.Columns["PrioritetiNivelCmimi"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("0", 0);
            colnew.PropertiesComboBox.Items.Add("1", 1);
            colnew.PropertiesComboBox.Items.Add("2", 2);
            colnew.PropertiesComboBox.Items.Add("3", 3);
            colnew.PropertiesComboBox.Items.Add("4", 4);
            colnew.PropertiesComboBox.Items.Add("5", 5);
            colnew.PropertiesComboBox.Items.Add("6", 6);
            colnew.PropertiesComboBox.Items.Add("7", 7);
            colnew.PropertiesComboBox.Items.Add("8", 8);
            colnew.PropertiesComboBox.Items.Add("9", 9);
            colnew.PropertiesComboBox.Items.Add("10", 10);
            colnew.PropertiesComboBox.Items.Add("11", 11);
            colnew.PropertiesComboBox.Items.Add("12", 12);
            colnew.PropertiesComboBox.Items.Add("13", 13);
            colnew.PropertiesComboBox.Items.Add("14", 14);
            colnew.PropertiesComboBox.Items.Add("15", 15);
            colnew.PropertiesComboBox.Items.Add("16", 16);
            colnew.PropertiesComboBox.Items.Add("17", 17);
            colnew.PropertiesComboBox.Items.Add("18", 18);
            colnew.PropertiesComboBox.Items.Add("19", 19);
            colnew.PropertiesComboBox.Items.Add("20", 20);
            colnew.FieldName = "PrioritetiNivelCmimi";
            colnew.VisibleIndex = visibleindex;
            gvLupaNivCmPrind.Columns.Add(colnew);
        }

        //sherben per ta bere ne forme combo-je shtyllen e monedhave
        private void shtoMonedhe()
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int visibleindex = gvLupaNivCmPrind.Columns["IdMonedha"].VisibleIndex;
            gvLupaNivCmPrind.Columns.Remove(gvLupaNivCmPrind.Columns["IdMonedha"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colMonedhat colMonedhat = new DbCore.DbAdmin.colMonedhat();
            colMonedhat.mbushGjitheMonedhatAktive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            //colMonedhat = dbAdmin.merrGjitheMonedhatAktive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            colnew.PropertiesComboBox.DataSource = colMonedhat;
            colnew.PropertiesComboBox.TextField = "KodiMonedha";
            colnew.PropertiesComboBox.ValueField = "IdMonedha";
            colnew.FieldName = "IdMonedha";
            colnew.VisibleIndex = visibleindex;
            gvLupaNivCmPrind.Columns.Add(colnew);
        }

        protected void gvLupaNivCmPrind_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaNivCmPrind.Settings.ShowFilterRow = true;
            gvLupaNivCmPrind.KeyFieldName = "IdNivelCmimi";
            gvLupaNivCmPrind.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaNivCmPrind_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //kur popupgrida ben callback
            //  mbushPopUpListeLlogarish();
            //  konfiguroPopupGride();
            gvLupaNivCmPrind.Selection.UnselectAll();
        }

        protected void gvLupaNivCmPrind_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
        }

        protected void gvLupaNivCmPrind_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaNivCmPrind.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNivCmPrind", "LupaNivelCmimiPrind.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaNivCmPrind.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaNivCmPrind);
                    }
                }
            }
            gvLupaNivCmPrind.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaNivelCmimiPrind.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNivCmPrind", "LupaNivelCmimiPrind.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaNivCmPrind.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivelCmimi", gvLupaNivCmPrind);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaNivCmPrind.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdNivelCmimi";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNivCmPrind", Convert.ToInt32(cmbKonfigurimi.Value), "LupaNivelCmimiPrind.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNivCmPrind", "LupaNivelCmimiPrind.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNivCmPrind", Convert.ToInt32(cmbKonfigurimi.Value), "LupaNivelCmimiPrind.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaNivCmPrind.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaNivCmPrind", "LupaNivelCmimiPrind.aspx", idNdermarrje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
    }
}