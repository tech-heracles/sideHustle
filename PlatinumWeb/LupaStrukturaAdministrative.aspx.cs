using DevExpress.Web;
using System;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaStrukturaAdministrative : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"];
            else
                vleraQueryString = "";
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LSA");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            if (!IsPostBack)
            {
                DbCore.IMBUtils.Cache.CacheDataProvider.ClearSessionCache("sipasPrindit"); 
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaStruktura, idKonfigambjenti);
                mbushPopUpListeNgaDB(idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaStruktura", idKonfigambjenti, "LupaStrukturaAdministrative.aspx");
            }
            else
            {
                mbushPopUpListeNgaSession(idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar);
            }
        }

        /// <summary>
        /// mbush griden nga sessioni
        /// </summary>
        private void mbushPopUpListeNgaSession(int idKonfigambjenti)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushPopUpListeNgaDB(idKonfigambjenti);
            else
            {
                gvLupaStruktura.DataSource = tmpObject;
                gvLupaStruktura.DataBind();
            }
        }
        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        private void mbushPopUpListeNgaDB(int idKonfigambjenti)
        {//mbush griden e popupit me te dhena  
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(idKonfigambjenti);
            DbCore.DbListPagesat.colStrukturatAdministrative col = new DbCore.DbListPagesat.colStrukturatAdministrative();
            if (konf.KodKonfigAmbjente == "LHir")
                col.mbushGjitheStrukturaAdmSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            else if (Request.QueryString["vjenNga"] == "Departamenti"||Request.QueryString["vjenNga"] == "ImportDep")
            {

                if (Request.QueryString["RaportiEmerReal"] == "labourOfficeReport" || Request.QueryString["RaportiEmerReal"] == "deklarimNeFinance")
                { col.mbushGjitheStrukturaAdmSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)); }
                else if(Request.QueryString["RaportiEmerReal"] == "pasqyraSigurimeveRaportuese")
                {
                    col.mbushGjitheStrukturaAdmPrindiSipasNdermarjesRaportuese(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                }
                else
                    col.mbushGjitheStrukturaAdmPrindiSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session),false);
            }
            else
            {
                string prindi = Request.QueryString["IdPrindi"];
                if (prindi.Contains(","))
                    col.mbushStrukturaAdmSipasShumePrind(prindi);
                else
                    col.mbushStrukturaAdmSipasPrindit(Convert.ToInt32(prindi),false);
            }
            col.RemoveAll(s=>s.IdStrukturaAdm==0);
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, col);
            gvLupaStruktura.DataSource = col;
            gvLupaStruktura.DataBind();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar)
        {//konfiguron popupgriden
            shto_LlojQendre();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaStruktura, "gvLupaStruktura", "LupaStrukturaAdministrative.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhe(gvLupaStruktura, "IdStrukturaAdm");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaStruktura, "IdStrukturaAdm", kerkosaposhkruar, endlessScroll);
        }

        /// <summary>
        /// data boundi i grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaStruktura_DataBound(object sender, EventArgs e)
        {
            gvLupaStruktura.Settings.ShowFilterRow = true;
            gvLupaStruktura.KeyFieldName = "IdStrukturaAdm";
            gvLupaStruktura.SettingsBehavior.AllowSelectByRowClick = true;
        }
        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaStruktura_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaStruktura.Selection.UnselectAll();
        }


        /// <summary>
        /// shtohet comboja me llojet e qendrave te kostos
        /// </summary>
        private void shto_LlojQendre()
        {

            int visibleindex = gvLupaStruktura.Columns["LlojQendre"].VisibleIndex;
            gvLupaStruktura.Columns.Remove(gvLupaStruktura.Columns["LlojQendre"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("", "");
            colnew.PropertiesComboBox.Items.Add("Qender Kosto", 1);
            colnew.PropertiesComboBox.Items.Add("Skema Qender Kosto", 2);
            colnew.FieldName = "LlojQendre";
            colnew.VisibleIndex = visibleindex;
            gvLupaStruktura.Columns.Add(colnew);
        }

        ///// <summary>
        ///// aplikon filtrin 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        //{
        //   DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
        //    //filtra.mbushFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //   DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaStruktura", "LupaStrukturaAdministrative.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //   filtra.mbushFilterPerGrideSipasKodit(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
        //    gvLupaStruktura.FilterExpression = filtra.FiltraVlera;
        //    if (filtra.DrejtimRenditje == true)
        //        gvLupaStruktura.SortBy(gvLupaStruktura.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
        //    else
        //        gvLupaStruktura.SortBy(gvLupaStruktura.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);

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
        //        DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = Kodi_ASPxTextBox.Text, FiltraShenime = Shenime_ASPxTextBox.Text, FiltraUniversal = false ,IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session),IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session),IdStatusDok = 1/* Universal_ASPxCheckBox.Checked;*/ };

        //        DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaStruktura", "LupaStrukturaAdministrative.aspx",DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        filtri.GridaKokaId = koka.IdGridaKoka;
        //        filtri.FiltraVlera = gvLupaStruktura.FilterExpression;
        //        System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaStruktura.GetSortedColumns();
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
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaStruktura", "LupaStrukturaAdministrative.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    if (DbCore.DbAdmin.clsFiltraGrida.ekzistonFilterSipasKoditPerGride(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka))
        //        args.IsValid = false;
        //    //using (DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin())
        //    //{
        //    //    if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text,DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
        //    //        args.IsValid = false;
        //    //    else
        //    //        args.IsValid = true;
        //    //}
        //}

        protected void gvLupaStruktura_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaStruktura.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaStruktura", "LupaStrukturaAdministrative.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaStruktura.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaStruktura);
                    }
                }
            }
            gvLupaStruktura.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaStrukturaAdministrative.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaStruktura", "LupaStrukturaAdministrative.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaStruktura.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvLupaStruktura);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaStruktura.GetSortedColumns();
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
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaStruktura", Convert.ToInt32(cmbKonfigurimi.Value), "LupaStrukturaAdministrative.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaStruktura", "LupaStrukturaAdministrative.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaStruktura", Convert.ToInt32(cmbKonfigurimi.Value), "LupaStrukturaAdministrative.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaStruktura.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaStruktura", "LupaStrukturaAdministrative.aspx", idNdermarrje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
    }
}