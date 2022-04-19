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
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaNivelZbritjePrind : MyPageBase
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
            //nivel.Kodi = "NivZbPrind"; //mund te shihet ne DB ne T_NIVELREGJISTRIMI
            //nivel.IdNdermarje = idNdermarrje;
            //nivel = nivel.merrNivelRegjSipasKodi();
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            //mbushComboBoxFiltra(idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "NivZbPrind");
            mbushPopUpListeNiveleZbritje();
            //DbCore.DbShare.clsKusht kushtkss = new DbCore.DbShare.clsKusht(idKonfigambjenti, "KSSH");
            //DbCore.DbShare.clsAlternativaKushti alterkss = new DbCore.DbShare.clsAlternativaKushti(kushtkss.Vlera);
            bool kerkosaposhkruar = true;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                GridUtil.AplikoFilterDefault(gvLupaNivZbPrind, idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNivZbPrind", idKonfigambjenti, "LupaNivelZbritjePrind.aspx");
            }
            else
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar);
            ////Container.Attributes["width"] = "350px";
            ////Container.Attributes["height"] = "400px";
            ////Container.Attributes["src"] = "LupaFiltra.aspx?grida=gvLupaNivZbPrind&page=LupaNivelZbritjePrind.aspx";
        }
        private void mbushPopUpListeNiveleZbritje()
        {//mbush griden e popupit me te dhena

            //idNdermVit =  DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            DbCore.DbInventari.colNiveleZbritjesh colNiveleZbritjesh = new DbCore.DbInventari.colNiveleZbritjesh();
            colNiveleZbritjesh.mbushGjitheNiveleZbritjeshPrindiSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //DbCore.DbInventari.colNiveleZbritjesh colNiveleZbritjesh = dbInventari.merrGjitheNiveleZbritjeshPrindiSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            this.gvLupaNivZbPrind.DataSource = colNiveleZbritjesh;
            gvLupaNivZbPrind.DataBind();
        }

        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar)
        {//konfiguron popupgriden
            shtoPrioritet();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaNivZbPrind, "gvLupaNivZbPrind", "LupaNivelZbritjePrind.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhe(gvLupaNivZbPrind, "IdNivelZbritje");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaNivZbPrind, "IdNivelZbritje", kerkosaposhkruar, endlessScroll);
        }

        private void shtoPrioritet()
        {//mbush combon e brutoneto
            int visibleindex = gvLupaNivZbPrind.Columns["PrioritetiNivelZbritje"].VisibleIndex;
            gvLupaNivZbPrind.Columns.Remove(gvLupaNivZbPrind.Columns["PrioritetiNivelZbritje"]);
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
            colnew.FieldName = "PrioritetiNivelZbritje";
            colnew.VisibleIndex = visibleindex;
            gvLupaNivZbPrind.Columns.Add(colnew);
        }

        //sherben per ta bere ne forme combo-je shtyllen e monedhave

        protected void gvLupaNivZbPrind_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaNivZbPrind.Settings.ShowFilterRow = true;
            gvLupaNivZbPrind.KeyFieldName = "IdNivelZbritje";
            gvLupaNivZbPrind.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaNivZbPrind_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //kur popupgrida ben callback
            //  mbushPopUpListeLlogarish();
            //  konfiguroPopupGride();
            gvLupaNivZbPrind.Selection.UnselectAll();
        }

        protected void gvLupaNivZbPrind_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
        }

        protected void gvLupaNivZbPrind_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaNivZbPrind.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNivZbPrind", "LupaNivelZbritjePrind.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaNivZbPrind.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaNivZbPrind);
                    }
                }
            }
            gvLupaNivZbPrind.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaNivelZbritjePrind.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNivZbPrind", "LupaNivelZbritjePrind.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaNivZbPrind.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivelZbritje", gvLupaNivZbPrind);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaNivZbPrind.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdNivelZbritje";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNivZbPrind", Convert.ToInt32(cmbKonfigurimi.Value), "LupaNivelZbritjePrind.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNivZbPrind", "LupaNivelZbritjePrind.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNivZbPrind", Convert.ToInt32(cmbKonfigurimi.Value), "LupaNivelZbritjePrind.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaNivZbPrind.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaNivZbPrind", "LupaNivelZbritjePrind.aspx", idNdermarrje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}

    }
}
