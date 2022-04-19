using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_KushtDergimi : MyPageBase
    {
        DbCore.DbAdmin.clsKushtDergimi kusht;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent caching, so can't be viewed offline
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (Page.IsPostBack == false)
            {
                EmrateTabeve();
                konfiguroVleraFillestare(idNdermarrje);
                konfiguroGride(idNdermarrje);
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
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);
         
        }

        private void konfiguroVleraFillestare(int idNdermarrje)
        {

            DbCore.DbAdmin.colKushteDergimi colKushteDergimi = new DbCore.DbAdmin.colKushteDergimi(idNdermarrje);
            //DbCore.DbAdmin.colKushteDergimi colKushteDergimi = dbAdmin.merrKushtetDergimit(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            grid_KushteDergimi.DataSource = colKushteDergimi;
            grid_KushteDergimi.DataBind();
        }

        private void konfiguroGride(int idNdermarrje)
        {
            GridUtil.percaktoVisibleColumnsShto(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, grid_KushteDergimi, "grid_KushteDergimi", "Shto_KushtDergimi.aspx");
            //funksione.percaktoAtributeTeGridesShto(grid_KushteDergimi, "IdKushtDergimi");
            GridUtil.percaktoAtributeTeGridesShtoPaTheme(grid_KushteDergimi, "IdKushtDergimi");
        }

        protected void grid_KushteDergimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            konfiguroVleraFillestare(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void pastro_ASPxButton_Click(object sender, EventArgs e)
        {
            grid_KushteDergimi.AddNewRow();
        }

        protected void ruaj_Button_Click(object sender, EventArgs e)
        {
            grid_KushteDergimi.UpdateEdit();
            if (kusht != null)
            {
                DbCore.clsMesazh mesazh = kusht.ruaj();
                pergjigja.Text = mesazh.PershkrimMesazhi;
                if (mesazh.Status == true)
                    Response.Redirect("KushteDergimi.aspx?ruaj=ok&indexrow=" + grid_KushteDergimi.VisibleRowCount);
                else pergjigja.ForeColor = Color.Red;
            }
        }

        protected void grid_KushteDergimi_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            kusht = new DbCore.DbAdmin.clsKushtDergimi();
            kusht.KodiKushtDergimi = e.NewValues["KodiKushtDergimi"].ToString();
            kusht.PershkrimiKushtDergimi = e.NewValues["PershkrimiKushtDergimi"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kusht.IdNdermarje = idNdermarrje;
            kusht.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kusht.IdStatusDok = 1;            
            if (kusht.KodiKushtDergimi != "")
            {
                e.Cancel = true;
                konfiguroVleraFillestare(idNdermarrje);
            }
        }

        protected void grid_KushteDergimi_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            foreach (GridViewColumn column in grid_KushteDergimi.Columns)
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

        protected void grid_KushteDergimi_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (!grid_KushteDergimi.IsNewRowEditing)
                grid_KushteDergimi.DoRowValidation();
        }

    }
}
