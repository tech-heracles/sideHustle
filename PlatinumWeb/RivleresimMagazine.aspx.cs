using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Resources;
using System.Globalization;
using DbCore.DbAdmin;
using System.IO;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbInventari;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class RivleresimMagazine : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ////Response.Cache.SetCacheability(HttpCacheability.NoCache);
            ////if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            //if (!DbCore.mySessionObjects.isLogedIn(Session))
            //{
            //    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            //}
            //int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            ////if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            //if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            //{
            //    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            //}
            ////if (CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"] != null)
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            //if (periudha == null)
            //{
            //    //  DbCore.clsFunksione funksione = new DbCore.clsFunksione();
            //    //periudha = ((DbCore.DbAdmin.clsPeriudhaKontabel)CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"]);
            //    periudha = new DbCore.DbAdmin.clsPeriudhaKontabel();
            //}
            //int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);
            //CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            //ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //if (!IsPostBack)
            //{
            //    mbushHiddenFieldMePerkthime(ci, rm);
            //    //Session.Add("colArtikull2", DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDT(-5, -5));
            //    DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerRivleresim(-5, -5));
            //    //Session.Add("colArtikull", DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDT(-5, -5));
            //    DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerRivleresim(-5, -5));
            //    konfiguroVleraFillestare(idNdermarrje, periudha);
            //    string lastArt = DbCore.DbAdmin.clsLogRivleresimInventari.lastArtRivleresim(idPerdoruesi, idNdermarrje);
            //    if (!String.IsNullOrEmpty(lastArt))
            //        clsMenuInfo.ShtoMesazhInformues(MenuInfo,"Artikulli i fundit ne rivleresim: " + lastArt, pnlMesazhi);
            //}
            //perktheKontrollet(ci, rm);
            //DataTable dt1 = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            //DataTable dt2 = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            //gvRivleresim2.DataSource = dt2;
            //gvRivleresim2.DataBind();
            //dt2.Dispose();
            //gvRivleresim.DataSource = dt1;
            //gvRivleresim.DataBind();
            //dt1.Dispose();
            // clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
        } 
        private void perktheKontrollet(CultureInfo ci, ResourceManager rm)
        {
            btnZgjidhGjitha.ToolTip = rm.GetString("zgjidhTeGjithaBtn", ci);
            gidaSelectMbrapa.ToolTip = rm.GetString("GridaZgjidhMbrapa", ci);
            btnHiqZgjedhjen.ToolTip = rm.GetString("hiqZgjedhjenBtn", ci);
            btnZgjidhGjitha2.ToolTip = rm.GetString("zgjidhTeGjithaBtn", ci);
            ASPxButton1.ToolTip = rm.GetString("btnZgjidhTeGjithe", ci);
            gidaSelectMbrapa2.ToolTip = rm.GetString("GridaZgjidhMbrapa", ci);
            btnHiqZgjedhjen2.ToolTip = rm.GetString("hiqZgjedhjenBtn", ci);
            gridaSelectTeGjitha.ToolTip = rm.GetString("btnZgjidhTeGjithe", ci);
            lblMagazina.Text = rm.GetString("filterMagazina", ci);
            lblPeriudha.Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            lblArtikujtPerRivleresim.Text = rm.GetString("lblArtikujtPerRivleresim", ci);
            ASPxRoundPanel1.HeaderText = rm.GetString("headerRivleresimMagazine", ci);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", ci);
        }

        private void mbushHiddenFieldMePerkthime(CultureInfo ci, ResourceManager rm)
        {
            hfState.Set("msgRivleresimMagDuhetTeKaloniArtikujtQeDoniTeRivleresoniTekGridaPoshte", rm.GetString("msgRivleresimMagDuhetTeKaloniArtikujtQeDoniTeRivleresoniTekGridaPoshte", ci));
            hfState.Set("msgRivleresimMagazineUNdaluaTek", rm.GetString("msgRivleresimMagazineUNdaluaTek", ci));
            hfState.Set("msgRivleresimMagazinePerfundoiMeSukses", rm.GetString("msgRivleresimMagazinePerfundoiMeSukses", ci));
            hfState.Set("msgRivleresimMagazineNdodhi1Gabim", rm.GetString("msgRivleresimMagazineNdodhi1Gabim", ci));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", ci));
            hfState.Set("headerPopUpZgjidhArtikullin", rm.GetString("headerPopUpZgjidhArtikullin", ci));
            hfState.Add("MsgDataGabim", rm.GetString("MsgBlerjeShitjeGabimData", ci));
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "RivleresimMagazine.aspx", this, MenuInfo, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            //if (e.Item.Name == "Rivleresim")
            //{
            //    Page.Validate();                
            //}
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idNdermarrje, DbCore.DbAdmin.clsPeriudhaKontabel periudha)
        {

            this.dtePeriudhaNga.Date = periudha.FillimiPeriudha;
            AspxWebControlUtils.vendosDateEditMask(dtePeriudhaNga);
            this.dtePeriudhaDeri.Date = periudha.MbarimiPeriudha;
            AspxWebControlUtils.vendosDateEditMask(dtePeriudhaDeri);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbMagazina);
            // DbCore.clsFunksione.percaktoTemplateCombo(cmbArtikuj);
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, cmbMagazina, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false, 1,true);
            // funk.mbushComboArtikulli(cmbArtikuj);
            mbushListeArtikujsh();
            konfiguroGride();
        }
        private void mbushListeArtikujsh()
        {//mbush griden e popupit me te dhena  
            // string[] artikuj = cmbArtikuj.Text.Split(',');
            string[] magazina = cmbMagazina.Text.Split(',');
            //DbCore.DbInventari.colArtikujt colArtikujt = new DbCore.DbInventari.colArtikujt();
            //if (cmbArtikuj.Text != "")
            //    {

            //    for (int i = 0; i < artikuj.Length; i++)
            //        {
            //        DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();
            //        art.merrSipasKodArtikullit(artikuj[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //        colArtikujt.Add(art);
            //        }

            //    } 
            DataTable dt;
            //else 
            if (cmbMagazina.Text != "")
            {
                string idmag = "";
                for (int i = 0; i < magazina.Length; i++)
                {
                    DbCore.DbRegjistrim.clsNjesiAdministrative mag = new DbCore.DbRegjistrim.clsNjesiAdministrative(magazina[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    idmag += mag.IdNjesiAdministrative + ",";
                }
                idmag = idmag.Substring(0, idmag.Length - 1);
                dt = DbCore.DbInventari.colArtikujt.merrSipasMagazinave(idmag);
            }
            else
            {
                dt = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerRivleresim(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));

            }
            gvRivleresim.DataSource = dt;//CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"] = dt;
            DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, dt);
            gvRivleresim.DataBind();
            dt.Dispose();
            //CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"] = DbCore.DbInventari.colArtikujt.merrSipasArtikujAktivNdermarrjesAndAutorizimeDT(-5, -5);
            DataTable dt2 = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerRivleresim(-5, -5);
            DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt2);
            gvRivleresim2.DataSource = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerRivleresim(-5, -5);
            gvRivleresim2.DataBind();
            dt2.Dispose();
        }

        private void konfiguroGride()
        {
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvRivleresim, "gvRivleresim", "RivleresimMagazine.aspx", 1, true, DbCore.mySessionObjects.ktheGjuhe(Session));
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvRivleresim2, "gvRivleresim", "RivleresimMagazine.aspx", 1, true, DbCore.mySessionObjects.ktheGjuhe(Session));
            // funk.konfiguroGrideListeMadhe(gvRivleresim, "IdArtikulli");
        }
        protected void gvRivleresim_DataBound(object sender, EventArgs e)
        {
            if (this.gvRivleresim.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvRivleresim.Settings.ShowFilterRow = true;
                gvRivleresim.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvRivleresim.Settings.ShowFilterRowMenu = true;
                gvRivleresim.Columns.Add(check);


                gvRivleresim.KeyFieldName = "IdArtikulli";
                gvRivleresim.SettingsBehavior.AllowSelectByRowClick = true;
                gvRivleresim.SettingsBehavior.AllowFocusedRow = true;
            }
            this.gvRivleresim.Columns["#"].VisibleIndex = 0;

        }
        protected void gvRivleresim2_DataBound(object sender, EventArgs e)
        {
            if (this.gvRivleresim2.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvRivleresim2.Settings.ShowFilterRow = true;
                gvRivleresim2.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvRivleresim2.Settings.ShowFilterRowMenu = true;
                gvRivleresim2.Columns.Add(check);


                gvRivleresim2.KeyFieldName = "IdArtikulli";
                gvRivleresim2.SettingsBehavior.AllowSelectByRowClick = true;
                gvRivleresim2.SettingsBehavior.AllowFocusedRow = true;
            }
            this.gvRivleresim2.Columns["#"].VisibleIndex = 0;

        }

        protected void gvRivleresim_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //mbushListaCallback();
        }
        protected void gvRivleresim2_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //mbushListaCallback();
        }
        private bool zgjidhMbrapa(object sender, ASPxGridViewCustomCallbackEventArgs e) {
            ASPxGridView grida = (ASPxGridView)sender;
            if (e.Parameters == "ZGJIDHMBRAPA")
            {
                List<object> selected = grida.GetSelectedFieldValues(grida.KeyFieldName);
                if (selected.Count() == 0)
                    return false; 
                for (int i = grida.FindVisibleIndexByKeyValue(selected.Last()); i < grida.VisibleRowCount; i++)
                {
                    grida.Selection.SelectRowByKey(grida.GetRowValues(i, grida.KeyFieldName));
                }
                return true;
            }
            return false;
        }
        protected void gvRivleresim_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
             
            if (e.Parameters == "mag") {
                mbushListeArtikujsh();
                return;
            }        
            //mbushListaCallback();
            zgjidhMbrapa(sender, e);
        }
        //private void mbushListaCallback()
        //{

        //    //gvRivleresim2.DataSource =CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"];// col2;
        //    gvRivleresim2.DataSource = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
        //    gvRivleresim2.DataBind();
        //    //gvRivleresim.DataSource = CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"];// col;
        //    gvRivleresim.DataSource = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
        //    gvRivleresim.DataBind();
        //}
        protected void gvRivleresim2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            
            //mbushListaCallback();
            zgjidhMbrapa(sender, e);
        }
       
        protected void gvRivleresim_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvRivleresim.VisibleRowCount;
            e.Properties["cpNoPage"] = gvRivleresim.PageIndex;
        }
        protected void gvRivleresim2_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvRivleresim2.VisibleRowCount;
            e.Properties["cpNoPage"] = gvRivleresim2.PageIndex;
        }

        protected void btnDjathtas1_Click(object sender, EventArgs e)
        {
            //DataTable dt1= (DataTable)   CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"];
            DataTable dt1 = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            //DataTable dt2 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"];
            DataTable dt2 = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            //  DbCore.DbInventari.colArtikujt col = (DbCore.DbInventari.colArtikujt)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"];
            //   DbCore.DbInventari.colArtikujt col2 = (DbCore.DbInventari.colArtikujt)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"];
       DataColumn[]     keys = new DataColumn[1];
            keys[0] = dt1.Columns[0];
            dt1.PrimaryKey = keys;
            gvRivleresim.DataSource = dt1;
            gvRivleresim.DataBind();
            List<object> id = gvRivleresim.GetSelectedFieldValues("IdArtikulli");
            foreach (object i in id)
            {
                DataRow dd = dt1.Rows.Find(i);
                dt2.Rows.Add(dd.ItemArray);
                //DataRow dr = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDR(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), Convert.ToInt32(i));
                //dt2.ImportRow(dr);
              


                //DataRow[] drs = dt1.Select("IdArtikulli = " + i);
                //if (drs.Length > 1)
                //{
                //    CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                //    ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                //    throw new DbCore.MyException(rm.GetString("msgShtoArtikulGabimNdodhen2ArtikujNeGride", ci));
                //}
                //if (drs.Length == 0) return;
                //DataRow dr1 = drs[0];
                dt1.Rows.Remove(dd);
               
            }
            //CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"] = dt1;
            DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, dt1);
            //CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"] = dt2;
            DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt2);
            gvRivleresim2.Selection.UnselectAll();
            gvRivleresim.Selection.UnselectAll();
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();
            dt2.Dispose();
            gvRivleresim.DataSource = dt1;
            gvRivleresim.DataBind();
            dt1.Dispose();
        }

        protected void btnDjathtasGjitha_Click(object sender, EventArgs e)
        {
            //DataTable dt1 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"];
            DataTable dt1 = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            //DataTable dt2 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"];
            DataTable dt2 = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            //DbCore.DbInventari.colArtikujt col = (DbCore.DbInventari.colArtikujt)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"];
            //DbCore.DbInventari.colArtikujt col2 = (DbCore.DbInventari.colArtikujt)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"];
            dt2.Merge(dt1);

            //  col2.AddRange(col);
            // col = new DbCore.DbInventari.colArtikujt();
            dt1 = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerRivleresim(-5, -5);
            //CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"] = dt1;
            DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, dt1);
            //CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"] = dt2;
            DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt2);
            gvRivleresim2.Selection.UnselectAll();
            gvRivleresim.Selection.UnselectAll();
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();
            dt2.Dispose();
            gvRivleresim.DataSource = dt1;
            gvRivleresim.DataBind();
            dt1.Dispose();
        }

        protected void btnMajtas1_Click(object sender, EventArgs e)
        {
            //DataTable dt1 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"];
            DataTable dt1 = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            //DataTable dt2 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"];
            DataTable dt2 = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            //  DbCore.DbInventari.colArtikujt col2 = (DbCore.DbInventari.colArtikujt)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"];
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt2.Columns[0];
            dt2.PrimaryKey = keys;

            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();
            List<object> id = gvRivleresim2.GetSelectedFieldValues("IdArtikulli");

            foreach (object i in id)
            {
                DataRow dd = dt2.Rows.Find(i);
                dt1.Rows.Add(dd.ItemArray);
                //DataRow dr = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDR(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), Convert.ToInt32(i));
                //dt1.ImportRow(dr);
                //DataRow[] drs = dt2.Select("IdArtikulli = " + i);
                //if (drs.Length > 1)
                //{
                //    CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                //    ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                //    throw new DbCore.MyException(rm.GetString("msgShtoArtikulGabimNdodhen2ArtikujNeGride", ci));
                //}
                //if (drs.Length == 0) return;
                //DataRow dr1 = drs[0];
                dt2.Rows.Remove(dd);
                
            }

            //CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"] = dt1;
            DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, dt1);
            //CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"] = dt2;
            DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt2);
            gvRivleresim2.Selection.UnselectAll();
            gvRivleresim.Selection.UnselectAll();
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();
            dt2.Dispose();
            gvRivleresim.DataSource = dt1;
            gvRivleresim.DataBind();
            dt1.Dispose();
        }

        protected void btnMajtaGjitha_Click(object sender, EventArgs e)
        {
            //DataTable dt1 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"];
            DataTable dt1 = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            //DataTable dt2 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"];
            DataTable dt2 = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            // DbCore.DbInventari.colArtikujt col2 = (DbCore.DbInventari.colArtikujt)CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"];
            dt1.Merge(dt2);
            //   col.AddRange(col2);
            // col2 = new DbCore.DbInventari.colArtikujt();
            dt2 = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerRivleresim(-5, -5);
            //CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"] = dt1;// col;
            DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, dt1);
            //CacheLayer.GlobalCacheManager.MySessionCache["colArtikull2"] = dt2;
            DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt2);
            gvRivleresim2.Selection.UnselectAll();
            gvRivleresim.Selection.UnselectAll();
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();
            dt2.Dispose();
            gvRivleresim.DataSource = dt1;// col;
            gvRivleresim.DataBind();
            dt1.Dispose();
        }

        protected void ProgressBar1_RunTask(object sender, EO.Web.ProgressTaskEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DateTimeOffset timezoneIShqiperise = TimeZoneInfo.ConvertTime(DateTime.Now,
                         TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));
            if (timezoneIShqiperise.Hour < 18)
            {
                return;

            }

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dtePeriudhaNga.Date, DbCore.IMBUtils.DataBase.MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, DbCore.DbRegjistrim.KategoriDokumenti.RivleresimInventari, 0))
            {

                e.UpdateProgress(e.Value, DbCore.IMBUtils.Messages.MessagesResource.Messages["msgPeriodIsClosed"] + " - " + dtePeriudhaNga.Date.ToShortDateString());
                return;
            }

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            int position = 0;
            e.UpdateProgress(position);
            string[] magazina = cmbMagazina.Text.Split(',');
            DataTable artikujPerRivleresim = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            DataTable artikujPerTuZgjedhur = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idndermarjevit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            bool eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
            int idArt = 0;
            DataTable tebera = artikujPerTuZgjedhur.Clone();
            String kodArtikulli = "";
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int nrArtkujve = artikujPerRivleresim.Rows.Count;
            int nrMagazinave;
            DbCore.DbRegjistrim.colNjesiAdministrative magazinat = null;
            if (cmbMagazina.Text != "")
            {
                nrMagazinave = magazina.Length;
            }
            else
            {
                magazinat = new DbCore.DbRegjistrim.colNjesiAdministrative();
                magazinat.mbushGjitheNjesiAdministrativeAktiveSipasLlojit(idNdermarrje, idPerdorues, 1);
                nrMagazinave = magazinat.Count;
            }
            DbCore.DbAdmin.clsLogRivleresimInventari log = new clsLogRivleresimInventari();
            try
            {
                log = new DbCore.DbAdmin.clsLogRivleresimInventari(idPerdorues, idNdermarrje, nrArtkujve, nrMagazinave, dtePeriudhaNga.Date, dtePeriudhaDeri.Date);
            }
            catch (Exception)
            {
                throw new DbCore.MyException(rm.GetString("msgGabimGjateRuajtjesSeRivleresimitNeLog", ci));
            }
            double nrDitesh = (dtePeriudhaDeri.Date - dtePeriudhaNga.Date).TotalDays + 1;
            //TimeSpan tsMax = TimeSpan.Zero;
            DateTime data = dtePeriudhaNga.Date;
            string arsyeStop = " ";

            if (cmbMagazina.Text != "")
            {
                try
                {
                    string idmag = "";
                    int max = magazina.Length * artikujPerRivleresim.Rows.Count;
                    //System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Lowest;
                    int roundedPosition = 0;
                    int j = 0;
                    try
                    {
                        for (; j < artikujPerRivleresim.Rows.Count; j++)
                        {
                            DataRow dr = artikujPerRivleresim.Rows[j];
                            for (int i = 0; i < magazina.Length; i++)
                            {
                                DbCore.DbRegjistrim.clsNjesiAdministrative mag = new DbCore.DbRegjistrim.clsNjesiAdministrative(magazina[i], idNdermarrje, idPerdorues);
                                idmag = mag.IdNjesiAdministrative + "";
                                //  colArtikujt.merrSipasMagazinave(idmag);
                                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(Convert.ToInt32(dr["IdArtikulli"]));
                                idArt = Convert.ToInt32(dr["IdArtikulli"]);
                                kodArtikulli = Convert.ToString(dr["KodArtikulli"]);
                                int metodeKostoje = Convert.ToInt32(dr["MetodeKostojeArtikulli"]);
                                bool kontrollCmimPerDetajim = Convert.ToBoolean(dr["KontrollCmimi"]);
                                data = dtePeriudhaNga.Date;
                                arsyeStop = " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci) + " " + kodArtikulli;
                                for (int m = 0; m < nrDitesh; m++)
                                {
                                    if (e.IsStopped)
                                    {
                                        //System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
                                        log.logStopRivleresim(DateTime.Now, arsyeStop, kodArtikulli, data);
                                        e.UpdateProgress(100, arsyeStop + " - " + data.ToShortDateString());
                                        kaloArtikujtSiper(--i, artikujPerRivleresim, artikujPerTuZgjedhur);
                                        return;
                                    }
                                    mesazh = DbCore.DbRegjistrim.clsTrupiMagazina.bejRivleresim(kontrollCmimPerDetajim, idArt, mag.IdNjesiAdministrative, metodeKostoje, data, log, ci, rm, idNdermarrje,idPerdorues);
                                    log.logRivleresim(kodArtikulli, mag.Kodi, data);
                                    if (!mesazh.Status)
                                    {
                                        log.logStopRivleresim(arsyeStop + " - " + data.ToShortDateString());
                                        e.UpdateProgress(e.Value, arsyeStop + " - " + data.ToShortDateString());
                                        throw new DbCore.MyException("rivleresimi stoped");
                                    }
                                    data = data.AddDays(1);
                                }
                                //mesazh = art.rivleresimCmimiMesatar(mag.IdNjesiAdministrative, dtePeriudhaNga.Date, dtePeriudhaDeri.Date);
                                position++;
                                roundedPosition = (int)Math.Round((double)((position / (double)max) * 100));

                                if (e.Value != roundedPosition)
                                    e.UpdateProgress(roundedPosition, " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci) + " " + kodArtikulli);
                            }

                        }
                    }
                    catch (DbCore.MyException ex)
                    {
                        kaloArtikujtSiper(--j, artikujPerRivleresim, artikujPerTuZgjedhur);
                        throw ex;
                    }
                    kaloArtikujtSiper(j, artikujPerRivleresim, artikujPerTuZgjedhur);
                    log.logFinishedRivleresim("Rivleresimi perfundoi me sukses - maxDelay: " + log.MaxTs.ToString());
                    return;
                }
                catch (Exception ex)
                {
                    e.UpdateProgress(e.Value, " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci) + " " + kodArtikulli);
                    log.logStopRivleresim(e.Value.ToString() + " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci), kodArtikulli, data, ex.Message);
                    return;
                }
            }
            if (hfState.Get("Veprimi").ToString() == "Riruaj")
            {
                try
                {
                    List<object> idartikuj = artikujPerRivleresim.AsEnumerable().Select(r => r.Field<object>("IdArtikulli")).ToList();
                    DataTable dt =DbCore.DbRegjistrim.colKokaMagazina.ktheRreshtaMagazineArtikulliPerRiruajtje(idNdermarrje, idartikuj, dtePeriudhaNga.Date, dtePeriudhaDeri.Date);
                    try
                    {               
                        mesazh = DbCore.DbRegjistrim.colKokaMagazina.riruajMag(ci, rm,  dt.AsEnumerable().Select(r => r.Field<object>("IDKOKAMAGAZINA")).ToList(), idPerdorues, idNdermarrje, idndermarjevit, idgjuha, eshteOwn, new ASPxHiddenField(), e,true,log);
                    }
                    catch (DbCore.MyException ex)
                    {
                       
                        throw ex;
                    }
                    kaloArtikujtSiper(100, artikujPerRivleresim, artikujPerTuZgjedhur);
                    log.logFinishedRivleresim("Rivleresimi perfundoi me sukses - maxDelay: " + log.MaxTs.ToString());
                    return;

                }

                catch (Exception ex)
                {
                    e.UpdateProgress(e.Value, " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci) + " " + kodArtikulli);
                    log.logStopRivleresim(e.Value.ToString() + " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci) + " " + kodArtikulli, kodArtikulli, data, ex.Message);
                    return;
                }
            }
            try
            {
                int max = artikujPerRivleresim.Rows.Count;
                int i = 0;
                try
                {
                    for (; i < max; i++)
                    {
                        DataRow dr = artikujPerRivleresim.Rows[i];
                        //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(Convert.ToInt32(dr["IdArtikulli"]));
                        idArt = Convert.ToInt32(dr["IdArtikulli"]);
                        kodArtikulli = Convert.ToString(dr["KodArtikulli"]);
                        int metodeKostoje = Convert.ToInt32(dr["MetodeKostojeArtikulli"]);
                        bool kontrollCmimPerDetajim = Convert.ToBoolean(dr["KontrollCmimi"]);
                        //for (int j = 0; j < magazinat.Count; j++)
                        //{
                        // DbCore.DbRegjistrim.clsNjesiAdministrative mag = magazinat[j];
                        data = dtePeriudhaNga.Date;
                        for (int m = 0; m < nrDitesh; m++)
                        {
                            if (e.IsStopped)
                            {
                                log.logStopRivleresim(DateTime.Now, "rivleresimi stoped", kodArtikulli, data);
                                kaloArtikujtSiper(--i, artikujPerRivleresim, artikujPerTuZgjedhur);
                                e.UpdateProgress(100, " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci) + " " + kodArtikulli);
                                //clsMenuInfo.ShtoMesazhInformues(MenuInfo, rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci), pnlMesazhi);
                                return;
                                //throw new DbCore.MyException("rivleresimi stoped");
                            }
                            mesazh = DbCore.DbRegjistrim.clsTrupiMagazina.bejRivleresim(kontrollCmimPerDetajim, idArt, 0, metodeKostoje, data, log,ci,rm, idNdermarrje,idPerdorues);
                            if (!mesazh.Status)
                            {
                                throw new DbCore.MyException(mesazh.PershkrimMesazhi);
                            }
                            log.logRivleresim(kodArtikulli, data);
                            data = data.AddDays(1);
                        }
                        //mesazh = art.rivleresimCmimiMesatar(mag.IdNjesiAdministrative, dtePeriudhaNga.Date, dtePeriudhaDeri.Date);
                        position++;
                        
                        int roundedPosition = (int)Math.Round((double)((position / (double)max) * 100));
                        if (e.Value != roundedPosition)
                            e.UpdateProgress(roundedPosition, " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci) + " " + kodArtikulli);
                        // }

                    }
                }
                catch (DbCore.MyException ex)
                {
                    kaloArtikujtSiper(--i, artikujPerRivleresim, artikujPerTuZgjedhur);
                    throw ex;
                }
                kaloArtikujtSiper(i, artikujPerRivleresim, artikujPerTuZgjedhur);
                log.logFinishedRivleresim("Rivleresimi perfundoi me sukses - maxDelay: " + log.MaxTs.ToString());
            }
            catch (Exception ex)
            {
                e.UpdateProgress(e.Value, " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci) + " " + kodArtikulli);
                log.logStopRivleresim(e.Value.ToString() + " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci) + " " + kodArtikulli, kodArtikulli, data, ex.Message);

            }
        }

        private void kaloArtikujtSiper(int i, DataTable artikujPerRivleresim, DataTable artikujPerTuZgjedhur)
        {
            for (; i > 0; i--)
            {
                if (artikujPerRivleresim.Rows.Count > 0)
                {
                    DataRow dr = artikujPerRivleresim.Rows[0];
                    DataRow drnew = artikujPerTuZgjedhur.NewRow();
                    drnew.ItemArray = dr.ItemArray;
                    artikujPerRivleresim.Rows.RemoveAt(0);
                    artikujPerTuZgjedhur.Rows.Add(drnew);
                }
            }            
        }
    }
}