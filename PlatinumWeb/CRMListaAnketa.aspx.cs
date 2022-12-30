using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Globalization;
using System.Resources;
using DbCore.DbCRM;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class CRMListaAnketa : MyPageBase
    {
       // DbCore.DbCRM.clsKokaAnketa anketa;
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
                return;
            }
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerd, idNdermarrje);
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LA", idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvCRMListaAnketa", idkonf, "CRMListaAnketa.aspx");
            hfState.Set("idPerdoruesi", idPerd);
            hfState.Set("idNdermarrje", idNdermarrje);
            hfState.Set("idViti", idViti);

            if (!IsPostBack)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                konfiguroVleraFillestare();
                konfiguroGride();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, "CRMListaAnketa.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                lblUserEmri.Text = DbCore.mySessionObjects.ktheEmerPerdorues(Session);
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", ci));
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare();
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "CRMListaAnketa.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true);
        }
        protected void gvCRMListaAnketa_DataBound(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (gvCRMListaAnketa.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                gvCRMListaAnketa.Columns.Add(check);
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //check.SetColVisibleIndex(0);

                gvCRMListaAnketa.SettingsText.CommandUpdate = rm.GetString("buttonRuaj", cultinf);
                gvCRMListaAnketa.SettingsText.CommandCancel = rm.GetString("labelAnullo", cultinf);

                gvCRMListaAnketa.Settings.ShowFilterRow = true;
                gvCRMListaAnketa.KeyFieldName = "IdKokaAnketa";
                gvCRMListaAnketa.SettingsBehavior.AllowSelectByRowClick = true;
                gvCRMListaAnketa.SettingsBehavior.AllowFocusedRow = true;
                gvCRMListaAnketa.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvCRMListaAnketa.Settings.ShowFilterRowMenu = true;
            }
        }


        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        private void konfiguroVleraFillestare()
        {
            DbCore.DbCRM.colKokaAnketa col = new DbCore.DbCRM.colKokaAnketa(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            gvCRMListaAnketa.DataSource = col;
            gvCRMListaAnketa.DataBind();
        }

        private void konfiguroGride()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvCRMListaAnketa, "gvCRMListaAnketa", "CRMListaAnketa.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvCRMListaAnketa, "IdKokaAnketa");
            gvCRMListaAnketa.Columns["#"].VisibleIndex = 0;
        }

        protected void gvCRMListaAnketa_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gvCRMListaAnketa_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            int a = gvCRMListaAnketa.FocusedRowIndex;
            gvCRMListaAnketa.Selection.SelectRow(a);
            List<object> rreshtat = gvCRMListaAnketa.GetSelectedFieldValues("IdKokaAnketa");
           clsDatabaseCRM DbCRM = new clsDatabaseCRM();
            foreach (int id in rreshtat)
            {
                clsKokaAnketa col = new clsKokaAnketa(id);
                col.IdModifikues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCRM.kaVeprimeCRMListaAnketa(id))
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ka veprime me kete ankete", pnlMesazhi);
                else
                {
                    col.fshi();
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                }

                konfiguroVleraFillestare();
            }
            DbCRM.Dispose();
            pnlGrida.Update();
        }


       
        protected void gvCRMListaAnketa_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvCRMListaAnketa.IsNewRowEditing)
                {
                    gvCRMListaAnketa.DoRowValidation();
                }
        }

      
        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvCRMListaAnketa", "CRMListaAnketa.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LA", idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvCRMListaAnketa", idkonf, "CRMListaAnketa.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //ASPxButton btnRuaj = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("Button1") as ASPxButton;
                //ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;

                //  btnRuaj.ClientEnabled = false;
                //  btnFshiFilter.ClientEnabled = false;
                //  konfiguroVleraFillestare();
                gvCRMListaAnketa.FilterExpression = String.Empty;
            }
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvCRMListaAnketa", "CRMListaAnketa.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvCRMListaAnketa", "CRMListaAnketa.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvCRMListaAnketa.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvCRMListaAnketa);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvCRMListaAnketa.GetSortedColumns();
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
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LA", idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvCRMListaAnketa", idkonf, "CRMListaAnketa.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LA", idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idGjuha, "gvCRMListaAnketa", "CRMListaAnketa.aspx", "FilterDefault", gvCRMListaAnketa.FilterExpression, gvCRMListaAnketa, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(gvCRMListaAnketa, "LA", idNdermarrje, idPerdoruesi, 2007, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvCRMListaAnketa", idkonf, "CRMListaAnketa.aspx");
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        //protected void ButtonOk_Click2(object sender, EventArgs e)
        //{
        //    int a = gvCRMListaAnketa.FocusedRowIndex;
        //    gvCRMListaAnketa.Selection.SelectRow(a);
        //    List<object> rreshtat = gvCRMListaAnketa.GetSelectedFieldValues("IdKokaAnketa");
        //    DbCore.DbCRM.clsDatabaseCRM dbCRM = new DbCore.DbCRM.clsDatabaseCRM();
        //    foreach (int id in rreshtat)
        //    {
        //        DbCore.DbCRM.clsKokaAnketa col = new DbCore.DbCRM.clsKokaAnketa(id);
        //        col.IdModifikues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
        //        if (!(dbCRM.kaVeprimeCRMListaAnketa(col.IdKokaAnketa))&&(col.DtFillimi> DateTime.Today) )
        //            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ka veprime me kete anketa", pnlMesazhi);
        //        else
        //        {
        //            DbCore.DbCRM.clsKokaAnketa.fshi(col.IdKokaAnketa, col.IdModifikues);
        //            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
        //        }

        //        konfiguroVleraFillestare();

        //    }
        //    dbCRM.Dispose();
        //    pnlGrida.Update();
        //}

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            //if (e.Item.Name == "Modifiko")
            //{
            //    int indeksi = gvCRMListaAnketa.FocusedRowIndex;
            //    gvCRMListaAnketa.StartEdit(indeksi);
            //    konfiguroGride();             
            //}

            //else if (e.Item.Name == "Shto")
            //{
            //    gvCRMListaAnketa.AddNewRow();
            //    konfiguroGride();               
            //}
        }

        protected void gvCRMListaAnketa_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }

        protected void gvCRMListaAnketa_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //  konfiguroVleraFillestare();            
        }

        protected void gvCRMListaAnketa_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
        }

        protected void gvCRMListaAnketa_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvCRMListaAnketa.FilterExpression = "";
                else
                {

                    GridUtil.AplikoFilter(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvCRMListaAnketa, arr[2], "gvCRMListaAnketa", "CRMListaAnketa.aspx", 1);
                    ////DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    //DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    
                    ////filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhePerdoruesi(base.Session), "gvCRMListaAnketa", "CRMListaAnketa.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    ////DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //if (filtra.FiltraKodi != null)
                    //{
                    //    gvCRMListaAnketa.FilterExpression = filtra.FiltraVlera;
                    //    if (filtra.DrejtimRenditje == true)
                    //        gvCRMListaAnketa.SortBy(gvCRMListaAnketa.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
                    //    else
                    //        gvCRMListaAnketa.SortBy(gvCRMListaAnketa.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);

                    //    //  konfiguroVleraFillestare();

                    //}
                }
            }
            konfiguroGride();
        }

        protected void gvCRMListaAnketa_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvCRMListaAnketa.PageIndex;
            e.Properties["cpPageRow"] = gvCRMListaAnketa.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvCRMListaAnketa.VisibleRowCount;
        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {

            try
            {
                gridExport.WriteXlsxToResponse("Anketat", true);
            }
            catch (Exception)
            {
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("Anketat", true);
            }
            catch (Exception)
            {
            }
        }
    }
}