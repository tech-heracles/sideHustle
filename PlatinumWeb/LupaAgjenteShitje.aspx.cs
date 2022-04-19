using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using System.Resources;
using System.Globalization;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaAgjenteShitje : MyPageBase
    {
 
        protected void Page_Load(object sender, EventArgs e)
        {
            String array = Request.QueryString["array"];
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "AgjSh");
            //int idAgjenti = 0;
            //if (Request.QueryString["agjenti"] != null)
            //    int.TryParse(Request.QueryString["agjenti"], out idAgjenti);
            mbushPopUpListeAgjentesh();
            bool kerkosaposhkruar = true;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            bool endlessScroll = false;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                endlessScroll = true;

            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaAgjShitje, idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar, ci, endlessScroll);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAgjShitje", idKonfigambjenti, "LupaAgjenteShitje.aspx");
            }
            else
            {
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar, ci, endlessScroll);
            }
            //if (Request.QueryString["vjenNga"] == "Agjenti")
            //{
            //    var idDrejtori = 0;
            //    if (int.TryParse(Request.QueryString["drejtori"], out idDrejtori))
            //    {
            //        gvLupaAgjShitje.Selection.SelectRowByKey(idDrejtori);
            //        gvLupaAgjShitje.FocusedRowIndex = gvLupaAgjShitje.FindVisibleIndexByKeyValue(idDrejtori);

            //    }
            //    gvLupaAgjShitje.SettingsBehavior.AllowSelectSingleRowOnly = true;
            //}
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaAgjenteShitje.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), Request.QueryString["theme"] != null && Request.QueryString["theme"] == "Moderno" ? true : false);
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAgjShitje", "LupaAgjenteShitje.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaAgjShitje.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdAgjentShitje", gvLupaAgjShitje);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaAgjShitje.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdAgjentShitje";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAgjShitje", Convert.ToInt32(cmbKonfigurimi.Value), "LupaAgjenteShitje.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAgjShitje", "LupaAgjenteShitje.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAgjShitje", Convert.ToInt32(cmbKonfigurimi.Value), "LupaAgjenteShitje.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaAgjShitje.FilterExpression = String.Empty;
            }
        }

        private void mbushPopUpListeAgjentesh()
        {//mbush griden e popupit me te dhena

            DbCore.DbAdmin.colAgjenteShitje colAgjentet = new DbCore.DbAdmin.colAgjenteShitje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            //colAgjentet = new DbCore.DbAdmin.colAgjenteShitje(colAgjentet.Where(x => x.IdAgjentShitje != idAgjenti));
            gvLupaAgjShitje.DataSource = colAgjentet;
            gvLupaAgjShitje.DataBind();
        }

        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar, CultureInfo ci, bool endlessScroll)
        {//konfiguron popupgriden
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaAgjShitje, "gvLupaAgjShitje", "LupaAgjenteShitje.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));

            //funk.konfiguroGrideListeMadhe(gvLupaAgjShitje, "IdAgjentShitje");
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaAgjShitje, "IdAgjentShitje", kerkosaposhkruar, endlessScroll);
        }

        protected void gvLupaAgjShitje_DataBound(object sender, EventArgs e)
        {
            if (gvLupaAgjShitje.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim

                gvLupaAgjShitje.Columns.Add(check);

                gvLupaAgjShitje.Settings.ShowFilterRow = true;
                gvLupaAgjShitje.KeyFieldName = "IdAgjentShitje";
                gvLupaAgjShitje.SettingsBehavior.AllowSelectByRowClick = true;
            }
        }

        protected void gvLupaAgjShitje_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //gvLupaAgjShitje.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvLupaAgjShitje_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvLupaAgjShitje.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAgjShitje", "LupaAgjenteShitje.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaAgjShitje.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaAgjShitje);
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
            gvLupaAgjShitje.Selection.UnselectAll();
        }

        //protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        //{
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
        //    //filtra.mbushFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaAgjShitje", "LupaAgjenteShitje.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    filtra.mbushFilterPerGrideSipasKodit(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
        //    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    gvLupaAgjShitje.FilterExpression = filtra.FiltraVlera;
        //    if (filtra.DrejtimRenditje == true)
        //        gvLupaAgjShitje.SortBy(gvLupaAgjShitje.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
        //    else
        //        gvLupaAgjShitje.SortBy(gvLupaAgjShitje.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);

        //    konfiguroPopupGride();
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
        //        //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvLupaAgjShitje", "LupaAgjenteShitje.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaAgjShitje", "LupaAgjenteShitje.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        filtri.GridaKokaId = koka.IdGridaKoka;
        //        filtri.FiltraVlera = gvLupaAgjShitje.FilterExpression;
        //        System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaAgjShitje.GetSortedColumns();
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
        //            filtri.KoloneRenditje = "IdAgjentShitje";
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
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaAgjShitje", "LupaAgjenteShitje.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    if (DbCore.DbAdmin.clsFiltraGrida.ekzistonFilterSipasKoditPerGride(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka))
        //        args.IsValid = false;
        //    //dbAdmin.Dispose();
        //}

        protected void gvLupaAgjShitje_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLupaAgjShitje.PageIndex;
            e.Properties["cpPageRow"] = gvLupaAgjShitje.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLupaAgjShitje.VisibleRowCount;
        }

    }
}
