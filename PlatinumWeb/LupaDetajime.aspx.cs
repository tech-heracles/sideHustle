using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DbCore.DbRegjistrim;
using System.Resources;
using System.Globalization;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaDetajime : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            //nivel.Kodi = "DET"; //eshte kodi i lupes, mund te shihet ne DB ne T_NIVELREGJISTRIMI
            //nivel.IdNdermarje = idNdermarrje;
            //nivel = nivel.merrNivelRegjSipasKodi();
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "DET");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            bool endlessScroll = false;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                endlessScroll = true;
            mbushPopUpListe(idPerdoruesi, idNdermarrje);
            hfState.Set("idNdermarrje", idNdermarrje);
            if (!IsPostBack)
            {
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                //AplikoFilterDefault();
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvDetajimArtikulli, idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar, endlessScroll);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvDetajimArtikulli", idKonfigambjenti, "LupaDetajime.aspx");
            }
            else
            {
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar, endlessScroll);
            }
            //Container.Attributes["width"] = "340px";
            //Container.Attributes["height"] = "350px";
            //Container.Attributes["src"] = "LupaFiltra.aspx?grida=gvDetajimArtikulli&page=LupaDetajime.aspx";
            //mbushComboBoxFiltra(idNdermarrje);
            gvDetajimArtikulli.Columns["#"].VisibleIndex = 0;
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvDetajimArtikulli, cultinf, rm);
        }
        private void mbushPopUpListe(int idPerdoruesi, int idNdermarrje)
        {
            DbCore.DbInventari.colDetajimeArtikulli col = new DbCore.DbInventari.colDetajimeArtikulli();
            int veprimi = -1;
            if (Request.QueryString["veprimi"] != null)
            {
                veprimi = int.Parse(Request.QueryString["veprimi"]);
                col.mbushDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(idNdermarrje, idPerdoruesi, veprimi);
            }
            else if (Request.QueryString["veprimet"] != null && Request.QueryString["veprimet"] == "raporti")
            {
                col.mbushDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(idNdermarrje, idPerdoruesi, 3);
                col.mbushDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(idNdermarrje, idPerdoruesi, 4);
            }
            else
                col.mbushDetajimeSipasNdermarrjesAndAutorizim(idNdermarrje, idPerdoruesi);
            gvDetajimArtikulli.DataSource = col;
            gvDetajimArtikulli.DataBind();
            if (Request.QueryString["detajime"] != null && Request.QueryString["detajime"] != "")
            {
                string detajimet = Request.QueryString["detajime"];
                string[] det = detajimet.Split(',');
                for (int i = det.Length - 1; i >= 0; i--)
                {
                    DbCore.DbInventari.clsDetajimArtikulli detaj = new DbCore.DbInventari.clsDetajimArtikulli();
                    detaj.mbushDetajimArtikulli(det[i], idNdermarrje);
                    if (!IsPostBack)
                        gvDetajimArtikulli.Selection.SelectRowByKey(detaj.IdDetajimArtikulli);
                    DbCore.DbInventari.clsDetajimArtikulli dett = col.Find(x => x.IdDetajimArtikulli == detaj.IdDetajimArtikulli);
                    col.Remove(dett);
                    col.Insert(0, dett);
                }
            }
            gvDetajimArtikulli.DataSource = col;
            gvDetajimArtikulli.DataBind();
        }

        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar, bool endlessScroll)
        {
            shto_Lloj();
            shto_Autorizim();
            shto_Kategori();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvDetajimArtikulli, "gvDetajimArtikulli", "LupaDetajime.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvDetajimArtikulli, "IdDetajimArtikulli", kerkosaposhkruar, endlessScroll);
        }

        protected void gvDetajimArtikulli_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvDetajimArtikulli.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                gvDetajimArtikulli.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvDetajimArtikulli.Settings.ShowFilterRowMenu = true;
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvDetajimArtikulli.Settings.ShowFilterRow = true;
                gvDetajimArtikulli.Columns.Add(check);

                gvDetajimArtikulli.KeyFieldName = "IdDetajimArtikulli";
                gvDetajimArtikulli.SettingsBehavior.AllowSelectByRowClick = true;
                gvDetajimArtikulli.SettingsBehavior.AllowFocusedRow = true;

            }
        }

        private void shto_Autorizim()
        {//shtohen komboja me Autorizimeve tek grida e KPF

            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int visibleindex = gvDetajimArtikulli.Columns["IdNivelAutorizimi"].VisibleIndex;
            gvDetajimArtikulli.Columns.Remove(gvDetajimArtikulli.Columns["IdNivelAutorizimi"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colAutorizimetKoka colAutorizim = new DbCore.DbAdmin.colAutorizimetKoka();
            colAutorizim.mbushGjitheAutorizimet(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            //colAutorizim = dbAdmin.merrGjitheAutorizimet();
            colnew.PropertiesComboBox.DataSource = colAutorizim;
            colnew.PropertiesComboBox.TextField = "KodiAutorizim";
            colnew.PropertiesComboBox.ValueField = "IdAutorizimKoka";
            colnew.FieldName = "IdNivelAutorizimi";
            colnew.VisibleIndex = visibleindex;
            gvDetajimArtikulli.Columns.Add(colnew);
        }

        private void shto_Lloj()
        {//shtohen komboja me Autorizimeve tek grida 

            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int visibleindex = gvDetajimArtikulli.Columns["LlojDetajimArtikulli"].VisibleIndex;
            gvDetajimArtikulli.Columns.Remove(gvDetajimArtikulli.Columns["LlojDetajimArtikulli"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("Alfanumerik", 1);
            colnew.PropertiesComboBox.Items.Add("Numerik", 2);
            colnew.PropertiesComboBox.Items.Add("Date", 3);
            // colnew.PropertiesComboBox.Items.Add("Garanci", 4);
            colnew.VisibleIndex = visibleindex;
            colnew.FieldName = "LlojDetajimArtikulli";
            gvDetajimArtikulli.Columns.Add(colnew);
        }

        private void shto_Kategori()
        {//shtohen komboja  tek grida 

            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int visibleindex = gvDetajimArtikulli.Columns["KategoriDetajimi"].VisibleIndex;
            gvDetajimArtikulli.Columns.Remove(gvDetajimArtikulli.Columns["KategoriDetajimi"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("Detajim 1", 1);
            colnew.PropertiesComboBox.Items.Add("Detajim 2", 2);
            colnew.FieldName = "KategoriDetajimi";
            colnew.VisibleIndex = visibleindex;
            gvDetajimArtikulli.Columns.Add(colnew);
        }

        protected void gvDetajimArtikulli_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //gvDetajimArtikulli.Selection.UnselectAll();
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvDetajimArtikulli, cultinf, rm);
        }

        protected void gvDetajimArtikulli_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }
 
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvDetajimArtikulli_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    gvDetajimArtikulli.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvDetajimArtikulli", "LupaDetajime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvDetajimArtikulli.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvDetajimArtikulli);
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
            gvDetajimArtikulli.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaDetajime.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvDetajimArtikulli", "LupaDetajime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvDetajimArtikulli.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdDetajimArtikulli", gvDetajimArtikulli);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvDetajimArtikulli.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdDetajimArtikulli";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvDetajimArtikulli", Convert.ToInt32(cmbKonfigurimi.Value), "LupaDetajime.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvDetajimArtikulli", "LupaDetajime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvDetajimArtikulli", Convert.ToInt32(cmbKonfigurimi.Value), "LupaDetajime.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvDetajimArtikulli.FilterExpression = String.Empty;
            }
        }
    }
}