using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using DbCore.DbAdmin;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Backup : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }

            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }

            percaktoTemplateMenu(ASPxMenu1,DbCore.mySessionObjects.ktheIdVitNdermarrje(Session),DbCore.mySessionObjects.ktheIdPerdoruesi(Session),DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (!IsPostBack)
            {
                EmrateTabeve();
                //Session.Add("colNder2", colNdermarrjet.merrNdermarjetDT(-5, -5));
                DbCore.mySessionObjects.ruajColNder2NeSession(Session, colNdermarrjet.merrNdermarjetDT(-5, -5));
                //Session.Add("colNder", colNdermarrjet.merrNdermarjetDT(-5, -5));
                DbCore.mySessionObjects.ruajColNderNeSession(Session, colNdermarrjet.merrNdermarjetDT(-5, -5));
                konfiguroVleraFillestare();

                int idlicenca = DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(idPerd);
                clsLicenca licenca = new clsLicenca(idlicenca);
                //licenca.IdLicenca = ;
                //licenca.merrLicenceSipasId();
                string pathDir = HttpContext.Current.Server.MapPath(null) + @"\Licencat\";
                DirectoryExtension.CreateDirIfNotExists(pathDir);
                pathDir += licenca.KodLicenca + "\\";
                DirectoryExtension.CreateDirIfNotExists(pathDir);
                DirectoryInfo di = new DirectoryInfo(pathDir);
                int i = 0;
                foreach (FileInfo fi in di.GetFiles())
                {
                    if (fi.Name.EndsWith(".rar"))
                    {
                        HtmlTableRow row = new HtmlTableRow();
                        HtmlTableCell cell = new HtmlTableCell();
                        ASPxHyperLink hyperlink = new ASPxHyperLink();
                        hyperlink.NavigateUrl = "downloading.aspx?file=" + fi.Name;
                        hyperlink.Text = fi.Name;
                        i++;
                        hl.Rows.Add(row);
                        cell.Controls.Add(hyperlink);
                        row.Cells.Add(cell);
                    }
                }

                pnlLidhur.Update();
            }
            // clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
        }
        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("MenuItemBackup", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("downloadTab", cultinf);
            ASPxLabel1.Text = rm.GetString("TabItemDownload", cultinf);
            btnZgjidhGjitha.Text = rm.GetString("btnZgjidhGjitha", cultinf);
            btnZgjidhGjitha2.Text = rm.GetString("btnZgjidhGjitha", cultinf);
            btnHiqZgjedhjen.Text = rm.GetString("btnHiqZgjidhjen", cultinf);
            btnHiqZgjedhjen2.Text = rm.GetString("btnHiqZgjidhjen", cultinf);
            btnZgjidhKunder.Text = rm.GetString("btnZgjidhKunder", cultinf);
           btnZgjidhKunde2r.Text = rm.GetString("btnZgjidhKunder", cultinf);
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Backup.aspx", this, MenuInfo, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));

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
            if (e.Item.Name == "Backup")
            {
                Page.Validate();
                backup();
            }

        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        private void konfiguroVleraFillestare()
        {


            mbushlistndermarjesh();
            konfiguroGride();
        }
        private void mbushlistndermarjesh()
        {

            DataTable dt;


            dt = colNdermarrjet.merrNdermarjetDT(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(DbCore.mySessionObjects.ktheIdPerdoruesi(Session)));

            gvNdermarjet.DataSource = dt;
            //CacheLayer.GlobalCacheManager.MySessionCache["colNder"] = dt;
            DbCore.mySessionObjects.ruajColNderNeSession(Session, dt);
            gvNdermarjet.DataBind();
            dt.Dispose();

            //CacheLayer.GlobalCacheManager.MySessionCache["colNder2"] = colNdermarrjet.merrNdermarjetDT(-5, -5);
            DataTable dt2 = colNdermarrjet.merrNdermarjetDT(-5, -5);
            DbCore.mySessionObjects.ruajColNder2NeSession(Session, dt2);
            gvNdermarjet2.DataSource = dt2;
            gvNdermarjet2.DataBind();
            dt2.Dispose();
        }

        private void konfiguroGride()
        {


            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvNdermarjet, "gvNdermarjet", "Backup.aspx");
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvNdermarjet2, "gvNdermarjet", "Backup.aspx");
            // funk.konfiguroGrideListeMadhe(gvNdermarjet, "IdArtikulli");
        }

        protected void gvNdermarjet_DataBound(object sender, EventArgs e)
        {
            if (this.gvNdermarjet.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvNdermarjet.Settings.ShowFilterRow = true;
                gvNdermarjet.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvNdermarjet.Settings.ShowFilterRowMenu = true;
                gvNdermarjet.Columns.Add(check);


                gvNdermarjet.KeyFieldName = "IdNdermarrje";
                gvNdermarjet.SettingsBehavior.AllowSelectByRowClick = true;
                gvNdermarjet.SettingsBehavior.AllowFocusedRow = true;
            }
            this.gvNdermarjet.Columns["#"].VisibleIndex = 0;

        }
        protected void gvNdermarjet2_DataBound(object sender, EventArgs e)
        {
            if (this.gvNdermarjet2.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvNdermarjet2.Settings.ShowFilterRow = true;
                gvNdermarjet2.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvNdermarjet2.Settings.ShowFilterRowMenu = true;
                gvNdermarjet2.Columns.Add(check);


                gvNdermarjet2.KeyFieldName = "IdNdermarrje";
                gvNdermarjet2.SettingsBehavior.AllowSelectByRowClick = true;
                gvNdermarjet2.SettingsBehavior.AllowFocusedRow = true;
            }
            this.gvNdermarjet2.Columns["#"].VisibleIndex = 0;

        }

        protected void gvNdermarjet_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushListaCallback();
        }
        protected void gvNdermarjet2_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushListaCallback();
        }

        protected void gvNdermarjet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.ToString() == "nder")
                mbushlistndermarjesh();
            else
            {
                mbushListaCallback();
            }
        }
        private void mbushListaCallback()
        {
            //gvNdermarjet2.DataSource = CacheLayer.GlobalCacheManager.MySessionCache["colNder2"];// col2;
            DataTable dt = DbCore.mySessionObjects.merrColNder2NgaSesioni(Session);
            gvNdermarjet2.DataSource = dt;
            gvNdermarjet2.DataBind();
            dt.Dispose();
            //gvNdermarjet.DataSource = CacheLayer.GlobalCacheManager.MySessionCache["colNder"];// col;    
            DataTable dt2 = DbCore.mySessionObjects.merrColNderNgaSesioni(Session);
            gvNdermarjet.DataSource = dt2;
            gvNdermarjet.DataBind();
            dt2.Dispose();
        }
        protected void gvNdermarjet2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            mbushListaCallback();
        }
        private void backup()
        {
            this.prbNdermarje.Position = 0;
            pnlProgressBar.Update();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true);

            //DataTable dt = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colNder2"];

            DataTable dt = DbCore.mySessionObjects.merrColNder2NgaSesioni(Session);
            
            clsNdermarrje nderm = new clsNdermarrje();
            int idlicenca = DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));

            mesazh = nderm.backup(dt, idlicenca);

            this.prbNdermarje.Position = prbNdermarje.Maximum;
            pnlProgressBar.Update();
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Backupi perfundoi me sukses!", pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            clsLicenca licenca = new clsLicenca(idlicenca);
            //licenca.IdLicenca = ;
            //licenca.merrLicenceSipasId();
            string pathDir = HttpContext.Current.Server.MapPath(null) + @"\Licencat\" + licenca.KodLicenca + "\\";
            DirectoryInfo di = new DirectoryInfo(pathDir);
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Name.EndsWith(".rar"))
                {
                    HtmlTableRow row = new HtmlTableRow();
                    HtmlTableCell cell = new HtmlTableCell();
                    ASPxHyperLink hyperlink = new ASPxHyperLink();
                    hyperlink.NavigateUrl = "downloading.aspx?file=" + fi.Name;
                    hyperlink.Text = fi.Name;

                    hl.Rows.Add(row);
                    cell.Controls.Add(hyperlink);
                    row.Cells.Add(cell);
                }
            }
            pnlLidhur.Update();
            dt.Dispose();
        }

        protected void gvNdermarjet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvNdermarjet.VisibleRowCount;
            e.Properties["cpNoPage"] = gvNdermarjet.PageIndex;
        }

        protected void gvNdermarjet2_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvNdermarjet2.VisibleRowCount;
            e.Properties["cpNoPage"] = gvNdermarjet2.PageIndex;
        }

        protected void btnDjathtas1_Click(object sender, EventArgs e)
        {
            //DataTable dt1 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colNder"];
            DataTable dt1 = DbCore.mySessionObjects.merrColNderNgaSesioni(Session);
            //DataTable dt2 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colNder2"];

            DataTable dt2 = DbCore.mySessionObjects.merrColNder2NgaSesioni(Session);

            List<object> id = gvNdermarjet.GetSelectedFieldValues("IdNdermarrje");
            foreach (object i in id)
            {
                DataRow dr = colNdermarrjet.merrSipasNdermarjeDR(Convert.ToInt32(i));
                dt2.ImportRow(dr);


                DataRow[] drs = dt1.Select("IdNdermarrje = " + i);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 ndermarje me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr1 = drs[0];
                dt1.Rows.Remove(dr1);

            }
            //CacheLayer.GlobalCacheManager.MySessionCache["colNder"] = dt1;
            DbCore.mySessionObjects.ruajColNderNeSession(Session, dt1);
            //CacheLayer.GlobalCacheManager.MySessionCache["colNder2"] = dt2;
            DbCore.mySessionObjects.ruajColNder2NeSession(Session, dt2);
            gvNdermarjet2.Selection.UnselectAll();
            gvNdermarjet.Selection.UnselectAll();
            gvNdermarjet2.DataSource = dt2;
            gvNdermarjet2.DataBind();
            dt2.Dispose();
            gvNdermarjet.DataSource = dt1;
            gvNdermarjet.DataBind();
            dt1.Dispose();
        }

        protected void btnDjathtasGjitha_Click(object sender, EventArgs e)
        {
            //DataTable dt1 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colNder"];

            DataTable dt1 = DbCore.mySessionObjects.merrColNderNgaSesioni(Session);

            //DataTable dt2 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colNder2"];
            DataTable dt2 = DbCore.mySessionObjects.merrColNder2NgaSesioni(Session);

            dt2.Merge(dt1);
            dt1 = colNdermarrjet.merrNdermarjetDT(-5, -5);
            //CacheLayer.GlobalCacheManager.MySessionCache["colNder"] = dt1;
            DbCore.mySessionObjects.ruajColNderNeSession(Session, dt1);
            //CacheLayer.GlobalCacheManager.MySessionCache["colNder2"] = dt2;
            DbCore.mySessionObjects.ruajColNder2NeSession(Session, dt2);
            gvNdermarjet2.Selection.UnselectAll();
            gvNdermarjet.Selection.UnselectAll();
            gvNdermarjet2.DataSource = dt2;
            gvNdermarjet2.DataBind();
            dt2.Dispose();
            gvNdermarjet.DataSource = dt1;
            gvNdermarjet.DataBind();
            dt1.Dispose();
        }

        protected void btnMajtas1_Click(object sender, EventArgs e)
        {
            //DataTable dt1 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colNder"];
            DataTable dt1 = DbCore.mySessionObjects.merrColNderNgaSesioni(Session);
            //DataTable dt2 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colNder2"];
            DataTable dt2 = DbCore.mySessionObjects.merrColNder2NgaSesioni(Session);
            //  DbCore.DbInventari.colArtikujt col2 = (DbCore.DbInventari.colArtikujt)CacheLayer.GlobalCacheManager.MySessionCache["colNder2"];
            List<object> id = gvNdermarjet2.GetSelectedFieldValues("IdNdermarrje");

            foreach (object i in id)
            {
                DataRow dr = colNdermarrjet.merrSipasNdermarjeDR(Convert.ToInt32(i));
                dt1.ImportRow(dr);
                DataRow[] drs = dt2.Select("IdNdermarrje = " + i);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 ndermarje me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr1 = drs[0];
                dt2.Rows.Remove(dr1);

            }

            //CacheLayer.GlobalCacheManager.MySessionCache["colNder"] = dt1;
            DbCore.mySessionObjects.ruajColNderNeSession(Session, dt1);
            //CacheLayer.GlobalCacheManager.MySessionCache["colNder2"] = dt2;
            DbCore.mySessionObjects.ruajColNder2NeSession(Session, dt2);
            gvNdermarjet2.Selection.UnselectAll();
            gvNdermarjet.Selection.UnselectAll();
            gvNdermarjet2.DataSource = dt2;
            gvNdermarjet2.DataBind();
            dt2.Dispose();
            gvNdermarjet.DataSource = dt1;
            gvNdermarjet.DataBind();
            dt1.Dispose();
        }

        protected void btnMajtaGjitha_Click(object sender, EventArgs e)
        {
            //DataTable dt1 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colNder"];
            DataTable dt1 = DbCore.mySessionObjects.merrColNderNgaSesioni(Session);
            //DataTable dt2 = (DataTable)CacheLayer.GlobalCacheManager.MySessionCache["colNder2"];
            DataTable dt2 = DbCore.mySessionObjects.merrColNder2NgaSesioni(Session);

            dt1.Merge(dt2);
            dt2 = colNdermarrjet.merrNdermarjetDT(-5, -5);
            //CacheLayer.GlobalCacheManager.MySessionCache["colNder"] = dt1;// col;
            DbCore.mySessionObjects.ruajColNderNeSession(Session, dt1);
            //CacheLayer.GlobalCacheManager.MySessionCache["colNder2"] = dt2;
            DbCore.mySessionObjects.ruajColNder2NeSession(Session, dt2);
            gvNdermarjet2.Selection.UnselectAll();
            gvNdermarjet.Selection.UnselectAll();
            gvNdermarjet2.DataSource = dt2;
            gvNdermarjet2.DataBind();
            dt2.Dispose();
            gvNdermarjet.DataSource = dt1;// col;
            gvNdermarjet.DataBind();
            dt1.Dispose();
        }
    }
}