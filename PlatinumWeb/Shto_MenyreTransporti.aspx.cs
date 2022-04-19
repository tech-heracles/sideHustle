using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using System.Drawing;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;


namespace PlatinumWeb
{
    public partial class Shto_MenyreTransporti : MyPageBase
    {
        DbCore.DbAdmin.clsMenyreTransporti transporti;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent caching, so can't be viewed offline
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }
            
            if (Page.IsPostBack == false)
            {
                EmrateTabeve();
                konfiguroVleraFillestare();
                konfiguroGride(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
        }

        private void konfiguroVleraFillestare()
        {            
            DbCore.DbAdmin.colMenyraTransporti colTransporti = new DbCore.DbAdmin.colMenyraTransporti();
            //DbCore.DbAdmin.colMenyraTransporti colTransporti = dbAdmin.merrMenyratTransportit(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            grid_MenyraTransporti.DataSource = colTransporti;
            grid_MenyraTransporti.DataBind();
        }

        private void konfiguroGride(int idNdermarrje)
        {
            GridUtil.percaktoVisibleColumnsShto(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, grid_MenyraTransporti, "grid_MenyraTransporti", "Shto_MenyreTransporti.aspx");
            //funksione.percaktoAtributeTeGridesShto(grid_MenyraTransporti, "IdMenyreTransporti");
            GridUtil.percaktoAtributeTeGridesShtoPaTheme(grid_MenyraTransporti, "IdMenyreTransporti");

        }

        protected void grid_MenyraTransporti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            konfiguroVleraFillestare();
        }

        protected void pastro_ASPxButton_Click(object sender, EventArgs e)
        {
            grid_MenyraTransporti.AddNewRow();
        }

        protected void ruaj_Button_Click(object sender, EventArgs e)
        {
            grid_MenyraTransporti.UpdateEdit();
            if (transporti != null)
            {
                DbCore.clsMesazh mesazh = transporti.ruaj();
                pergjigja.Text = mesazh.PershkrimMesazhi;
                if (mesazh.Status == true)
                    Response.Redirect("MenyraTransporti.aspx?ruaj=ok&indexrow=" + grid_MenyraTransporti.VisibleRowCount);
                else pergjigja.ForeColor = Color.Red;
            }
        }

        protected void grid_MenyraTransporti_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            transporti = new DbCore.DbAdmin.clsMenyreTransporti();
            transporti.KodiMenyreTransporti = e.NewValues["KodiMenyreTransporti"].ToString();
            transporti.PershkrimiMenyreTransporti = e.NewValues["PershkrimiMenyreTransporti"].ToString();
            transporti.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            transporti.IdStatusDok = 1;
            transporti.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            
            if (transporti.KodiMenyreTransporti != "")
            {
                e.Cancel = true;
                konfiguroVleraFillestare();
            }
        }

        protected void grid_MenyraTransporti_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            foreach (GridViewColumn column in grid_MenyraTransporti.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    if (e.NewValues[dataColumn.FieldName] == null)
                    {
                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    }
                }
            }
            if (e.Errors.Count > 0) e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0) e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
        }

        protected void grid_MenyraTransporti_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (!grid_MenyraTransporti.IsNewRowEditing)
                grid_MenyraTransporti.DoRowValidation();
        }

    }
}
