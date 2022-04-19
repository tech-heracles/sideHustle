using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DbCore.DbShare;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaKonfigUrdherPagesa : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"];
            else
                vleraQueryString = "";

            //DbCore.DbRegjistrim.clsNivelRegjistrimi nivel = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //nivel.Kodi = "LKUP"; // mund te shihet ne DB ne T_NIVELREGJISTRIMI
            //nivel.IdNdermarje = idNdermarrje;
            //nivel = nivel.merrNivelRegjSipasKodi();
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LKUP");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            if (!IsPostBack)
            {
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);             
                //AplikoFilterDefault();
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaKonfig, idKonfigambjenti);
                mbushPopUpListeNgaDB();
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKonfig", idKonfigambjenti, "LupaKonfigUrdherPagesa.aspx");
            }
            else
            {
                mbushPopUpListeNgaSession();
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar);
            }
            //mbushComboBoxFiltra(idNdermarrje);
            //Container.Attributes["width"] = "350px";
            //Container.Attributes["height"] = "400px";
            //Container.Attributes["src"] = "LupaFiltra.aspx?grida=gvLupaKonfig&page=LupaKonfigUrdherPagesa.aspx";            
        }
        ///// <summary>
        ///// merr filtrin default
        ///// </summary>
        ///// <param name="idkonfigAmbjenti"></param>
        ///// <returns></returns>
        //private static DbCore.DbAdmin.clsFiltraGrida merrFilterDefault(int idkonfigAmbjenti)
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
        ///// <summary>
        ///// aplikon filtrin 
        ///// </summary>
        //private void AplikoFilterDefault()
        //{
        //    DbCore.DbAdmin.clsFiltraGrida ofilter = new DbCore.DbAdmin.clsFiltraGrida();
        //    ofilter = merrFilterDefault(idKonfigambjenti);
        //    if (ofilter != null)
        //        gvLupaKonfig.FilterExpression = ofilter.FiltraVlera;
        //}
        /// <summary>
        /// mbush griden nga sessioni
        /// </summary>
        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            { mbushPopUpListeNgaDB(); return; }
            gvLupaKonfig.DataSource = tmpObject;
            gvLupaKonfig.DataBind();
        }
        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena            
            DbCore.DbArkaBanka.colKonfigUrdherPagese col = new DbCore.DbArkaBanka.colKonfigUrdherPagese();
            if (Request.QueryString["vjenNga"] == "Grupi")
            {
                col = new DbCore.DbArkaBanka.colKonfigUrdherPagese(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Grup));
            }
            else if (Request.QueryString["vjenNga"] == "Titulli")
            {
                col = new DbCore.DbArkaBanka.colKonfigUrdherPagese(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Titull));
            }
            else if (Request.QueryString["vjenNga"] == "Kapitulli")
            {
                col = new DbCore.DbArkaBanka.colKonfigUrdherPagese(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Kapitull));
            }
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, col); gvLupaKonfig.DataSource = col;
            gvLupaKonfig.DataBind();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar)
        {//konfiguron popupgriden

            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaKonfig, "gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhe(gvLupaKonfig, "Id");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKonfig, "Id", kerkosaposhkruar, endlessScroll);
        }

        /// <summary>
        /// data boundi i grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaKonfig_DataBound(object sender, EventArgs e)
        {
            gvLupaKonfig.Settings.ShowFilterRow = true;
            gvLupaKonfig.KeyFieldName = "Id";
            gvLupaKonfig.SettingsBehavior.AllowSelectByRowClick = true;
        }
        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaKonfig_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaKonfig.Selection.UnselectAll();
        }

        ///// <summary>
        ///// aplikon filtrin 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        //{
        //    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
        //    //filtra.mbushFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    filtra.mbushFilterPerGrideSipasKodit(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
        //    gvLupaKonfig.FilterExpression = filtra.FiltraVlera;
        //    if (filtra.DrejtimRenditje == true)
        //        gvLupaKonfig.SortBy(gvLupaKonfig.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
        //    else
        //        gvLupaKonfig.SortBy(gvLupaKonfig.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);

        //    konfiguroPopupGride();
        //    Filtri_ASPxTextBox.Text = "";
        //}
        ///// <summary>
        ///// ruan filtrin 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //    {
        //        DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = Kodi_ASPxTextBox.Text, FiltraShenime = Shenime_ASPxTextBox.Text, FiltraUniversal = false, IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session), IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), IdStatusDok = 1/* Universal_ASPxCheckBox.Checked;*/ };

        //        DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        filtri.GridaKokaId = koka.IdGridaKoka;
        //        filtri.FiltraVlera = gvLupaKonfig.FilterExpression;
        //        System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaKonfig.GetSortedColumns();
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
        //            filtri.KoloneRenditje = "Kodi";
        //            filtri.DrejtimRenditje = true;
        //        }
        //        filtri.ruaj();
        //        Kodi_ASPxTextBox.Text = "";
        //        Shenime_ASPxTextBox.Text = "";
        //        //Universal_ASPxCheckBox.Text = "";
        //        popRuaj.ShowOnPageLoad = false;
        //    }
        //}

        ///// <summary>
        ///// validon nese ekziston filtri
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="args"></param>
        //protected void Kodi_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    args.IsValid = true;
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    if (DbCore.DbAdmin.clsFiltraGrida.ekzistonFilterSipasKoditPerGride(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka))
        //        args.IsValid = false;
        //    //using (DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin())
        //    //{
        //    //    if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
        //    //        args.IsValid = false;
        //    //    else
        //    //        args.IsValid = true;
        //    //}
        //}
        protected void gvLupaKonfig_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaKonfig.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaKonfig.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaKonfig);
                    }
                }
            }
            gvLupaKonfig.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaKonfigUrdherPagesa.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaKonfig.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvLupaKonfig);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaKonfig.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Kodi";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKonfig", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKonfigUrdherPagesa.aspx");
            //mbushComboBoxFiltra(idNdermarrje);
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKonfig", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKonfigUrdherPagesa.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaKonfig.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", idNdermarrje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
    }
}