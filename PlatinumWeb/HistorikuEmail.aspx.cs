using DbCore;
using DbCore.DbRegjistrim;
using DevExpress.Web;
using System;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class HistorikuEmail : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, false, "FaqePaautorizuar", false);
                return;
            }
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idVitNderm = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            percaktoTemplateMenu(ASPxMenu1, idVitNderm, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (!IsPostBack)
            {
                hfState.Set("idPerdoruesi", idPerdorues);
                hfState.Set("merrMesazh", "");
                hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
                CacheLayer.GlobalCacheManager.MySessionCache["colHistorikuEmail" + idNdermarrje + idPerdorues + idVitNderm] = null;
            }
            mbushGriden(idNdermarrje, idPerdorues, idVitNderm);
            konfiguroGride(idGjuha, idNdermarrje);
            GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idNdermarrje, grid_HistorikuEmail, "grid_HistorikuEmail", "HistorikuEmail.aspx");
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            grid_HistorikuEmail.PercaktoTitlePanel(Page, MenuInfo, pnlMesazhi, hfState, idPerdorues, idNdermarrje, idVitNderm, DbCore.mySessionObjects.ktheGjuhe(Session), 0, "HistorikuEmail.aspx", 3032, "IdEmail", rm, cultinf);
        }
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "HistorikuEmail.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
        }
        private void konfiguroGride(int idGjuha, int idNdermarrje)
        {
            GridUtil.PercaktoSettings(grid_HistorikuEmail, Session);
            GridUtil.konfigGrideListeEMadhePaTheme(grid_HistorikuEmail, "IdEmail");
            GridViewDataDateColumn DataEDergimit = grid_HistorikuEmail.Columns["DataEDergimit"] as GridViewDataDateColumn;
            DataEDergimit.PropertiesDateEdit.UseMaskBehavior = true;
            DataEDergimit.PropertiesDateEdit.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";
            GridViewDataDateColumn DataEPergjigjes = grid_HistorikuEmail.Columns["DataEPergjigjes"] as GridViewDataDateColumn;
            DataEPergjigjes.PropertiesDateEdit.UseMaskBehavior = true;
            DataEPergjigjes.PropertiesDateEdit.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";

        }
        private void shtoButoninResend()
        {
            GridViewDataColumn resend = new GridViewDataColumn();
            resend.Caption = "Ridergo";
            resend.Width = 10;
            grid_HistorikuEmail.Columns.Add(resend);
            GridViewDataTextColumn col8 = new GridViewDataTextColumn();
            col8.DataItemTemplate = new PlatinumWeb.Templates.MyButtonTemplate("Ridergo");
            col8.VisibleIndex = 8;
            grid_HistorikuEmail.Columns.Add(col8);
        }
        protected void grid_HistorikuEmail_DataBound(object sender, EventArgs e)
        {
            if (grid_HistorikuEmail.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = 30;
                check.VisibleIndex = 0;
                grid_HistorikuEmail.Settings.ShowFilterRow = true;
                grid_HistorikuEmail.Columns.Insert(0, check);
                grid_HistorikuEmail.KeyFieldName = "IdEmail";
                grid_HistorikuEmail.SettingsBehavior.AllowSelectByRowClick = true;
                grid_HistorikuEmail.SettingsBehavior.AllowFocusedRow = true;
            }
        }
        protected void grid_HistorikuEmail_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }
        protected void grid_HistorikuEmail_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }
        protected void grid_HistorikuEmail_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

        }

        private void mbushGriden(int idNdermarrje, int idPerdoruesi, int idVitNderm)
        {
            if (CacheLayer.GlobalCacheManager.MySessionCache["colHistorikuEmail" + idNdermarrje + idPerdoruesi + idVitNderm] == null)
                CacheLayer.GlobalCacheManager.MySessionCache["colHistorikuEmail" + idNdermarrje + idPerdoruesi + idVitNderm] = colEmail.ktheHistorikun(idNdermarrje, idPerdoruesi);
            grid_HistorikuEmail.DataSource = CacheLayer.GlobalCacheManager.MySessionCache["colHistorikuEmail" + idNdermarrje + idPerdoruesi + idVitNderm];
            grid_HistorikuEmail.DataBind();

        }

        protected void grid_HistorikuEmail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            clsMesazh msg = EmailComposer.riDergoEmailFaturen(Convert.ToInt32(e.Parameters));
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, msg.PershkrimMesazhi + (msg.Status ? ":Green" : ":Red"));
            hfState.Set("merrMesazh", "Po");
        }
    }
}