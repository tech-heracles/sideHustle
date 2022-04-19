using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class KushteDergimi : MyPageBase
    {
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent caching, so can't be viewed offline
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            if (!IsPostBack)
            {
                if (Request.QueryString["ruaj"] == "ok")
                    pergjigja.Text = MessagesResource.Messages["mesazhRuajtjeMeSukses"];

                konfiguroVleraFillestare();
                konfiguroGride();
                AspxWebControlUtils.konfiguroMenuPaTheme(ASPxMenu1);
                ASPxMenu1.AutoPostBack = false;
                AspxWebControlUtils.percaktoTedrejtatPerKeteFaqe("KushteDergimi.aspx", ASPxMenu1);

                if (Request.QueryString["indexrow"] != null)
                {
                    grid_KushteDergimi.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
                }

            }
          

            konfiguroVleraFillestare();
        }

        private void konfiguroVleraFillestare()
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.colKushteDergimi colKushteDergimi = new DbCore.DbAdmin.colKushteDergimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //DbCore.DbAdmin.colKushteDergimi colKushteDergimi = dbAdmin.merrKushtetDergimit();
            grid_KushteDergimi.DataSource = colKushteDergimi;
            grid_KushteDergimi.DataBind();
        }

        protected void grid_KushteDergimi_DataBound(object sender, EventArgs e)
        {
            if (grid_KushteDergimi.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.Width = 20;
                check.SetColVisibleIndex(0);
                grid_KushteDergimi.Columns.Add(check);
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);

                grid_KushteDergimi.SettingsText.CommandUpdate = "Ruaj";
                grid_KushteDergimi.SettingsText.CommandCancel = "Anullo";

                grid_KushteDergimi.Settings.ShowFilterRow = true;
                grid_KushteDergimi.KeyFieldName = "IdKushtDergimi";
                grid_KushteDergimi.SettingsBehavior.AllowSelectByRowClick = true;
                grid_KushteDergimi.SettingsBehavior.AllowFocusedRow = true;
                grid_KushteDergimi.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_KushteDergimi.Settings.ShowFilterRowMenu = true;
            }
        }

        private void konfiguroGride()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_KushteDergimi, "grid_KushteDergimi", "KushteDergimi.aspx");
            //funk.konfiguroGrideListeMadhe(grid_KushteDergimi, "IdKushtDergimi");
            GridUtil.konfigGrideListeEMadhePaTheme(grid_KushteDergimi, "IdKushtDergimi");
        }

        protected void grid_KushteDergimi_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Filter)
            {
                ASPxComboBox combo = (sender as ASPxGridView).FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;
            }
        }

        protected void grid_KushteDergimi_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "PershkrimiKushtDergimi")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue("Nga D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue("Nga H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue("Nga L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue("Nga P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue("Nga T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue("Nga X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }

        protected void grid_KushteDergimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            konfiguroVleraFillestare();
        }

        protected void grid_KushteDergimi_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsKushtDergimi kusht = new DbCore.DbAdmin.clsKushtDergimi();

            kusht.IdKushtDergimi= int.Parse(e.Keys["IdKushtDergimi"].ToString());
            kusht.KodiKushtDergimi = e.NewValues["KodiKushtDergimi"].ToString();
            kusht.PershkrimiKushtDergimi = e.NewValues["PershkrimiKushtDergimi"].ToString();
            kusht.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kusht.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kusht.IdStatusDok = 1;
            if (kusht.KodiKushtDergimi != "")
            {
                e.Cancel = true;
                kusht.modifiko();
                grid_KushteDergimi.CancelEdit();
            }
        }


        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat = grid_KushteDergimi.GetSelectedFieldValues("IdKushtDergimi");
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            foreach (int id in rreshtat)
            {
                
                //DbCore.DbAdmin.colKushteDergimi colKushteDergimi = dbAdmin.ktheKushtDergimi(id);
                DbCore.DbAdmin.clsKushtDergimi clsKushtDergimi = new DbCore.DbAdmin.clsKushtDergimi(id);
                //foreach (DbCore.DbAdmin.clsKushtDergimi k in colKushteDergimi)
                //{
                clsKushtDergimi.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                clsKushtDergimi.fshi();
                //}
            }
            Response.Redirect("KushteDergimi.aspx");
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Modifiko")
            {
                int indeksi = grid_KushteDergimi.FocusedRowIndex;
                grid_KushteDergimi.StartEdit(indeksi);
            }
            else
                if (e.Item.Name == "Shto")
                {
                    Response.Redirect("Shto_KushtDergimi.aspx");
                }
        }

        protected void grid_KushteDergimi_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }
    }
}
