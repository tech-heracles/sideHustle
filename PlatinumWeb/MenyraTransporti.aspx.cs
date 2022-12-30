using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class MenyraTransporti : MyPageBase
    {
        ArrayList vlerat = new ArrayList();
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
                return;
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
                AspxWebControlUtils.percaktoTedrejtatPerKeteFaqe("Qytetet.aspx", ASPxMenu1);

                if (Request.QueryString["indexrow"] != null)
                {
                    grid_MenyraTransporti.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
                }
            }


            konfiguroVleraFillestare();
        }

        /// <summary>
        /// Mbush griden e menyrave te transportit me listen e menyrave te transportit te ndermarrjes
        /// </summary>
        private void konfiguroVleraFillestare()
        {
            int idNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.colMenyraTransporti colTransport = new DbCore.DbAdmin.colMenyraTransporti(idNdermarje);
            //DbCore.DbAdmin.colMenyraTransporti colTransport = dbAdmin.merrMenyratTransportit(idNdermarje);
            grid_MenyraTransporti.DataSource = colTransport;
            grid_MenyraTransporti.DataBind();
        }

        /// <summary>
        /// Shton nje kolone checkbox ne gride per te bere selektim
        /// </summary>
        protected void grid_MenyraTransporti_DataBound(object sender, EventArgs e)
        {
            if (grid_MenyraTransporti.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.Width = 20;
                check.SetColVisibleIndex(0);
                grid_MenyraTransporti.Columns.Add(check);
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);

                grid_MenyraTransporti.SettingsText.CommandUpdate = "Ruaj";
                grid_MenyraTransporti.SettingsText.CommandCancel = "Anullo";

                grid_MenyraTransporti.Settings.ShowFilterRow = true;
                grid_MenyraTransporti.KeyFieldName = "IdMenyreTransporti";
                grid_MenyraTransporti.SettingsBehavior.AllowSelectByRowClick = true;
                grid_MenyraTransporti.SettingsBehavior.AllowFocusedRow = true;
                grid_MenyraTransporti.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_MenyraTransporti.Settings.ShowFilterRowMenu = true;
            }
        }

        /// <summary>
        /// Konfiguron griden e menyrave te transportit
        /// </summary>
        private void konfiguroGride()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_MenyraTransporti, "grid_MenyraTransporti", "MenyraTransporti.aspx");
            //funk.konfiguroGrideListeMadhe(grid_MenyraTransporti, "IdMenyreTransporti");
            GridUtil.konfigGrideListeEMadhePaTheme(grid_MenyraTransporti, "IdMenyreTransporti");
        }

        protected void grid_MenyraTransporti_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Filter)
            {
                ASPxComboBox combo = (sender as ASPxGridView).FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;
            }
        }

        protected void grid_MenyraTransporti_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "PershkrimiMenyreTransporti")
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

        /// <summary>
        /// Therret funksionin <see cref="konfiguroVleraFillestare"/>
        /// </summary>
        protected void grid_MenyraTransporti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            konfiguroVleraFillestare();
        }

        protected void grid_MenyraTransporti_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsMenyreTransporti transporti = new DbCore.DbAdmin.clsMenyreTransporti();

            transporti.IdMenyreTransporti = int.Parse(e.Keys["IdMenyreTransporti"].ToString());
            transporti.KodiMenyreTransporti = e.NewValues["KodiMenyreTransporti"].ToString();
            transporti.PershkrimiMenyreTransporti = e.NewValues["PershkrimiMenyreTransporti"].ToString();
            transporti.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            transporti.IdStatusDok = 1;
            transporti.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (transporti.KodiMenyreTransporti != "")
            {
                e.Cancel = true;
                transporti.modifiko();
                grid_MenyraTransporti.CancelEdit();
            }
        }


        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat = grid_MenyraTransporti.GetSelectedFieldValues("IdMenyreTransporti");
            foreach (int id in rreshtat)
            {
                //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                DbCore.DbAdmin.clsMenyreTransporti clsTransporti = new DbCore.DbAdmin.clsMenyreTransporti(id);
                //DbCore.DbAdmin.colMenyraTransporti colTransporti = dbAdmin.ktheMenyreTransporti(id);

                //foreach (DbCore.DbAdmin.clsMenyreTransporti q in colTransporti)
                //{
                clsTransporti.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                clsTransporti.fshi();
                //}
            }
            Response.Redirect("MenyraTransporti.aspx");
            return;
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Modifiko")
            {
                int indeksi = grid_MenyraTransporti.FocusedRowIndex;
                grid_MenyraTransporti.StartEdit(indeksi);
            }
            else if (e.Item.Name == "Shto")
            {
                Response.Redirect("Shto_MenyreTransporti.aspx");
                return;
            }
        }

        protected void grid_MenyraTransporti_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }
    }
}