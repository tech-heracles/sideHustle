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
    public partial class LupaSkemaKontabelArtikulli : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String array = Request.QueryString["array"];
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "SkKontArt");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            mbushPopUpListeSkemash();
            
            if (!IsPostBack)
            {
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaSkemaKontArt, idKonfigambjenti);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaSkemaKontArt", idKonfigambjenti, "LupaSkemaKontabelArtikulli.aspx");
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar);
                string llojiartikull = Request.QueryString["llojiart"];
                if (llojiartikull != null)
                {
                    if (llojiartikull == "afatshkurter")
                        gvLupaSkemaKontArt.FilterExpression = "[LlojiArt]=false";
                    else if (llojiartikull == "aqt")
                        gvLupaSkemaKontArt.FilterExpression = "[LlojiArt]=true";
                }
            }
            else
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar);
            
            //Container.Attributes["width"] = "350px";
            //Container.Attributes["height"] = "400px";
            //Container.Attributes["src"] = "LupaFiltra.aspx?grida=gvLupaSkemaKontArt&page=LupaSkemaKontabelArtikulli.aspx";
        }
        private void mbushPopUpListeSkemash()
        {//mbush griden e popupit me te dhena            
            DbCore.DbInventari.colSkematKontabilitetiArtikulli colSkemat;
            if (Request.QueryString["value"] != null)
                colSkemat = new DbCore.DbInventari.colSkematKontabilitetiArtikulli(int.Parse(Request.QueryString["value"].ToString()), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            else
                colSkemat = new DbCore.DbInventari.colSkematKontabilitetiArtikulli(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            gvLupaSkemaKontArt.DataSource = colSkemat;
            gvLupaSkemaKontArt.DataBind();
        }

        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar)
        {//konfiguron popupgriden
            shtoLloji();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaSkemaKontArt, "gvLupaSkemaKontArt", "LupaSkemaKontabelArtikulli.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhePopupi(gvLupaSkemaKontArt, "IdSkemaKontabilitetiArtikulli");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaSkemaKontArt, "IdSkemaKontabilitetiArtikulli", kerkosaposhkruar, endlessScroll);
            string llojiartikull = Request.QueryString["llojiart"];
            //komentuar per arsye qe te ruhej filtri edhe kur ndryshoje faqe ne gride
            
            //if (llojiartikull != null)
            //{
            //    if (llojiartikull == "afatshkurter")
            //        gvLupaSkemaKontArt.FilterExpression = "[LlojiArt]=false";
            //    else if (llojiartikull == "aqt")
            //        gvLupaSkemaKontArt.FilterExpression = "[LlojiArt]=true";
            //}
            ASPxLabel1.Text = "";
        }

        private void shtoLloji()
        {//shton comboboxin e llojit
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int visibleindex = gvLupaSkemaKontArt.Columns["LlojiArt"].VisibleIndex;
            gvLupaSkemaKontArt.Columns.Remove(gvLupaSkemaKontArt.Columns["LlojiArt"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();

            colnew.PropertiesComboBox.Items.Add("Afatgjate", true);
            colnew.PropertiesComboBox.Items.Add("Afatshkurter", false);
            colnew.FieldName = "LlojiArt";
            colnew.Caption = "Lloji";
            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            colnew.VisibleIndex = visibleindex;
            gvLupaSkemaKontArt.Columns.Add(colnew);
        }

        private void shtoLlogariInventar(int idGjuha)
        {// shton kombobox tek grida per llogarine
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int visibleindex = gvLupaSkemaKontArt.Columns["IdLlogariInventari"].VisibleIndex;
            gvLupaSkemaKontArt.Columns.Remove(gvLupaSkemaKontArt.Columns["IdLlogariInventari"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DataTable dt = DbCore.DbKontabiliteti.colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idGjuha);
            colnew.PropertiesComboBox.DataSource = dt;// colLlog;
            colnew.PropertiesComboBox.TextField = "NrLlogari";
            colnew.PropertiesComboBox.ValueField = "IdLlogari";
            colnew.FieldName = "NrLlogariInventari";
            colnew.VisibleIndex = visibleindex;
            gvLupaSkemaKontArt.Columns.Add(colnew);
            dt.Dispose();
        }

        #region metodaShtimi komentuar
        //private void shtoLlogariBlerje()
        //{// shton kombobox tek grida per llogarine
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    int visibleindex = gvLupaSkemaKontArt.Columns["IdLlogariBlerje"].VisibleIndex;
        //    gvLupaSkemaKontArt.Columns.Remove(gvLupaSkemaKontArt.Columns["IdLlogariBlerje"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    DataTable dt = DbCore.DbKontabiliteti.colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
        //    colnew.PropertiesComboBox.DataSource = dt;// colLlog;
        //    colnew.PropertiesComboBox.TextField = "NrLlogari";
        //    colnew.PropertiesComboBox.ValueField = "IdLlogari";
        //    colnew.FieldName = "NLlogariBlerje";
        //    gvLupaSkemaKontArt.Columns.Add(colnew);
        //    colnew.VisibleIndex = visibleindex;
        //    dt.Dispose();
        //}


        //private void shtoLlogariShitje()
        //{// shton kombobox tek grida per llogarine
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    int visibleindex = gvLupaSkemaKontArt.Columns["IdLlogariShitje"].VisibleIndex;
        //    gvLupaSkemaKontArt.Columns.Remove(gvLupaSkemaKontArt.Columns["IdLlogariShitje"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    DataTable dt = DbCore.DbKontabiliteti.colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));

        //    colnew.PropertiesComboBox.DataSource = dt;// colLlog;
        //    colnew.PropertiesComboBox.TextField = "NrLlogari";
        //    colnew.PropertiesComboBox.ValueField = "IdLlogari";
        //    colnew.FieldName = "IdLlogariShitje";
        //    gvLupaSkemaKontArt.Columns.Add(colnew);
        //    colnew.VisibleIndex = visibleindex;
        //    dt.Dispose();
        //}

        //private void shtoLlogariTretet()
        //{// shton kombobox tek grida per llogarine
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    int visibleindex = gvLupaSkemaKontArt.Columns["IdLlogariTekTeTretet"].VisibleIndex;
        //    gvLupaSkemaKontArt.Columns.Remove(gvLupaSkemaKontArt.Columns["IdLlogariTekTeTretet"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    DataTable dt = DbCore.DbKontabiliteti.colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));

        //    colnew.PropertiesComboBox.DataSource = dt;// colLlog;
        //    colnew.PropertiesComboBox.TextField = "NrLlogari";
        //    colnew.PropertiesComboBox.ValueField = "IdLlogari";
        //    colnew.FieldName = "IdLlogariTekTeTretet";
        //    gvLupaSkemaKontArt.Columns.Add(colnew);
        //    colnew.VisibleIndex = visibleindex;
        //    dt.Dispose();
        //}

        //private void shtoLlogariShpenzime()
        //{// shton kombobox tek grida per llogarine
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    int visibleindex = gvLupaSkemaKontArt.Columns["IdLlogariShpenzimi"].VisibleIndex;
        //    gvLupaSkemaKontArt.Columns.Remove(gvLupaSkemaKontArt.Columns["IdLlogariShpenzimi"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    DataTable dt = DbCore.DbKontabiliteti.colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
        //    colnew.PropertiesComboBox.DataSource = dt;// colLlog;
        //    colnew.PropertiesComboBox.TextField = "NrLlogari";
        //    colnew.PropertiesComboBox.ValueField = "IdLlogari";
        //    colnew.FieldName = "IdLlogariShpenzimi";
        //    gvLupaSkemaKontArt.Columns.Add(colnew);
        //    colnew.VisibleIndex = visibleindex;
        //    dt.Dispose();
        //}
        #endregion metodaShtimi komentuar

        protected void gvLupaSkemaKontArt_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaSkemaKontArt.Settings.ShowFilterRow = true;
            gvLupaSkemaKontArt.KeyFieldName = "IdSkemaKontabilitetiArtikulli";
            gvLupaSkemaKontArt.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaSkemaKontArt_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //kur popupgrida ben callback
            //  mbushPopUpListeLlogarish();
            //  konfiguroPopupGride();
            gvLupaSkemaKontArt.Selection.UnselectAll();
        }

        protected void gvLupaSkemaKontArt_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

        }

        protected void gvLupaSkemaKontArt_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaSkemaKontArt.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaSkemaKontArt", "LupaSkemaKontabelArtikulli.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaSkemaKontArt.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaSkemaKontArt);
                    }
                }
                gvLupaSkemaKontArt.Selection.UnselectAll();
            }
            else
            {
                DbCore.DbKontabiliteti.clsLlogari oLlogari = new DbCore.DbKontabiliteti.clsLlogari();

                string value = txtPersh.Text;
                string text = "";
                string[] pars1 = value.Split(',');
                for (int i = 2; i < pars1.Length; i++)
                {
                    if (pars1[i] == "0")
                        text += ",";
                    else
                    {
                        oLlogari = new DbCore.DbKontabiliteti.clsLlogari();
                        oLlogari.IdLlogari = int.Parse(pars1[i]);
                        oLlogari = oLlogari.merrLlogariSipasId();
                        text += oLlogari.NrLlogari + ",";
                    }
                }
                txtPersh.Text = pars1[0] + "," + pars1[1] + "," + text.Substring(0, text.Length - 1);
            }
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaSkemaKontabelArtikulli.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaSkemaKontArt", "LupaSkemaKontabelArtikulli.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaSkemaKontArt.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdSkemaKontabilitetiArtikulli", gvLupaSkemaKontArt);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaSkemaKontArt.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdSkemaKontabilitetiArtikulli";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaSkemaKontArt", Convert.ToInt32(cmbKonfigurimi.Value), "LupaSkemaKontabelArtikulli.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaSkemaKontArt", "LupaSkemaKontabelArtikulli.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaSkemaKontArt", Convert.ToInt32(cmbKonfigurimi.Value), "LupaSkemaKontabelArtikulli.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaSkemaKontArt.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaSkemaKontArt", "LupaSkemaKontabelArtikulli.aspx", idNdermarrje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
    }
}
