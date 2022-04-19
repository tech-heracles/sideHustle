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
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaKpf : MyPageBase
    {
        //private DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);

            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            //nivel.Kodi = "KPF"; // mund te shihet ne DB ne T_NIVELREGJISTRIMI
            //nivel.IdNdermarje = idNdermarrje;
            //nivel = nivel.merrNivelRegjSipasKodi();
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "KPF");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            hfState.Set("idNdermarrje", idNdermarrje);
            if (!IsPostBack)
            {
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                //AplikoFilterDefault();
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaKpf, idKonfigambjenti);
                mbushPopUpListeKPFshNgaDB(id);
                konfiguroPopupGrideKPF1(idKonfigambjenti, true, kerkosaposhkruar);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKpf", idKonfigambjenti, "LupaKpf.aspx");
            }
            else
            {
                mbushPopUpListeKPFshNgaSession(id);
                konfiguroPopupGrideKPF1(idKonfigambjenti, false, kerkosaposhkruar);
            }

            //mbushComboBoxFiltra(idNdermarrje);
            //Container.Attributes["width"] = "350px";
            //Container.Attributes["height"] = "400px";
            //Container.Attributes["src"] = "LupaFiltra.aspx?grida=gvLupaKpf&page=LupaKpf.aspx";
        }
        private void mbushPopUpListeKPFshNgaSession(int grup)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            { mbushPopUpListeKPFshNgaDB(grup); return; }
            gvLupaKpf.DataSource = tmpObject;
            gvLupaKpf.DataBind();
        }
        private void mbushPopUpListeKPFshNgaDB(int grup)
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbKontabiliteti.colKPFte.merrSipasKPFNdermarrjesAndAutorizimePerLupeKPF(grup, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            //DbCore.DbInventari.colArtikujt colArtikujt = dbInventari.merrArtikujAktivNdermarrjesAndAutorizime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaKpf.DataSource = dt;
            //gvLupaArtikull.DataSource = colArtikujt;
            gvLupaKpf.DataBind();
            dt.Dispose();
        }
        private void mbushPopUpGrideKPFsh2()
        {//mbush griden e popupit me te dhena
            //dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            //DbCore.DbKontabiliteti.colKPFte colKPFte = dbKontabiliteti.merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(2, funk.ktheNdermarrjeVit(), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            //DbCore.DbKontabiliteti.colKPFte colKPFte = new DbCore.DbKontabiliteti.colKPFte();
            //colKPFte.mbushGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(2, funk.ktheNdermarrjeVit(), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DataTable dt = DbCore.DbKontabiliteti.colKPFte.merrSipasKPFNdermarrjesAndAutorizimePerLupeKPF(2, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            gvLupaKpf.DataSource = dt;// colKPFte;
            gvLupaKpf.DataBind();
            dt.Dispose();
        }

        private void mbushPopUpGrideKPFsh3()
        {//mbush griden e popupit me te dhena
            //dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            //DbCore.DbKontabiliteti.colKPFte colKPFte = dbKontabiliteti.merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(3, funk.ktheNdermarrjeVit(), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            //  DbCore.DbKontabiliteti.colKPFte colKPFte = new DbCore.DbKontabiliteti.colKPFte();
            //   colKPFte.mbushGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(3, funk.ktheNdermarrjeVit(), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DataTable dt = DbCore.DbKontabiliteti.colKPFte.merrSipasKPFNdermarrjesAndAutorizimePerLupeKPF(3, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            gvLupaKpf.DataSource = dt;// colKPFte;
            gvLupaKpf.DataBind();
            dt.Dispose();
        }

        private void mbushPopUpGrideKPFsh1()
        {//mbush griden e popupit me te dhena
            //dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            //DbCore.DbKontabiliteti.colKPFte colKPFte = dbKontabiliteti.merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(1, funk.ktheNdermarrjeVit(), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            // DbCore.DbKontabiliteti.colKPFte colKPFte = new DbCore.DbKontabiliteti.colKPFte();
            //  colKPFte.mbushGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(1, funk.ktheNdermarrjeVit(), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DataTable dt = DbCore.DbKontabiliteti.colKPFte.merrSipasKPFNdermarrjesAndAutorizimePerLupeKPF(1, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            gvLupaKpf.DataSource = dt;// colKPFte;
            gvLupaKpf.DataBind();
            dt.Dispose();
        }

        private void konfiguroPopupGrideKPF1(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar)
        {//konfiguron popupgriden
            shtoNivelKPF1();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaKpf, "gvLupaKpf", "LupaKPF.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhe(gvLupaKpf, "IdKPF");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKpf, "IdKPF", kerkosaposhkruar, endlessScroll);
        }

        private void shtoNivelKPF1()
        {
            int visibleindex = gvLupaKpf.Columns["NiveliKPF"].VisibleIndex;
            gvLupaKpf.Columns.Remove(gvLupaKpf.Columns["NiveliKPF"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            List<int> colNivelKPF = new List<int>();
            colNivelKPF.Add(1);
            colNivelKPF.Add(2);
            colNivelKPF.Add(3);
            colNivelKPF.Add(4);
            colNivelKPF.Add(5);

            colnew.PropertiesComboBox.DataSource = colNivelKPF;
            // colnew.PropertiesComboBox.TextField = "PershkrimiGrupiLlogaria";
            //  colnew.PropertiesComboBox.ValueField = "IdGrupiLlogaria";
            colnew.VisibleIndex = visibleindex;
            colnew.FieldName = "NiveliKPF";
            gvLupaKpf.Columns.Add(colnew);
        }



        protected void gvLupaKpf_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaKpf.Settings.ShowFilterRow = true;
            gvLupaKpf.KeyFieldName = "IdKPF";
            gvLupaKpf.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaKpf_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //kur popupgrida ben callback
            //  mbushPopUpListeLlogarish();
            //  konfiguroPopupGride();
            gvLupaKpf.Selection.UnselectAll();
        }

        protected void txtKPF_TextChanged(object sender, EventArgs e)
        {

        }

        //protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        //{
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
        //    //filtra.mbushFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKpf", "LupaKpf.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    filtra.mbushFilterPerGrideSipasKodit(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
        //    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    gvLupaKpf.FilterExpression = filtra.FiltraVlera;
        //    if (filtra.DrejtimRenditje == true)
        //        gvLupaKpf.SortBy(gvLupaKpf.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
        //    else
        //        gvLupaKpf.SortBy(gvLupaKpf.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);

        //    konfiguroPopupGrideKPF1();
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
        //        //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvLupaKpf", "LupaKpf.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKpf", "LupaKpf.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        filtri.GridaKokaId = koka.IdGridaKoka;
        //        filtri.FiltraVlera = gvLupaKpf.FilterExpression;
        //        System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaKpf.GetSortedColumns();
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
        //            filtri.KoloneRenditje = "IdKPF";
        //            filtri.DrejtimRenditje = true;
        //        }
        //        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        //        //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
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
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKpf", "LupaKpf.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    if (DbCore.DbAdmin.clsFiltraGrida.ekzistonFilterSipasKoditPerGride(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka))
        //        args.IsValid = false;
        //    //dbAdmin.Dispose();
        //}

        //private DbCore.DbAdmin.clsFiltraGrida merrFilterDefault(int idkonfigAmbjenti)
        //{
        //    DbCore.DbAdmin.clsFiltraGrida ofiltri = new DbCore.DbAdmin.clsFiltraGrida();
        //    DbCore.DbShare.clsKusht oKusht = new DbCore.DbShare.clsKusht();
        //    DbCore.DbShare.colKusht colKushtet = new DbCore.DbShare.colKusht();

        //    oKusht.IdKonfigurimAmbjente = idkonfigAmbjenti;
        //    colKushtet = oKusht.merrTeGjitheKushteKonfigurimi(idkonfigAmbjenti);
        //    if (colKushtet.Count > 0)
        //    {
        //        oKusht = colKushtet[0]; //cdo konfigurim ambjenti per LUPAT ka vetem nje kusht qe eshte filtri default i grides
        //        ofiltri.IdFiltra = oKusht.Vlera;
        //        ofiltri = ofiltri.merrFilterSipasId();
        //    }
        //    return ofiltri;
        //}

        //private void AplikoFilterDefault()
        //{
        //    DbCore.DbAdmin.clsFiltraGrida ofilter = new DbCore.DbAdmin.clsFiltraGrida();
        //    ofilter = merrFilterDefault(idKonfigambjenti);
        //    if (ofilter != null)
        //        gvLupaKpf.FilterExpression = ofilter.FiltraVlera;
        //}

        protected void gvLupaKpf_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaKpf.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKpf", "LupaKpf.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaKpf.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaKpf);
                    }
                }
            }
            gvLupaKpf.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaKpf.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKpf", "LupaKpf.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaKpf.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdKPF", gvLupaKpf);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaKpf.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdKPF";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKpf", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKpf.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKpf", "LupaKpf.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKpf", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKpf.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaKpf.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKpf", "LupaKpf.aspx", idNdermarrje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
    }
}
