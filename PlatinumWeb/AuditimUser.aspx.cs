using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using System.Drawing;
using DbCore.DbAdmin;
using DevExpress.Data.Filtering;
using PlatinumWeb.Templates;
using System.Resources;
using System.Globalization;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class AuditimUser : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, false, "FaqePaautorizuar", false);
            }


            mbushGriden(idPerdorues);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
  

            if (!IsPostBack)
            {
                hfState.Set("idPerdoruesi", idPerdorues);
                konfiguroGride(idGjuha, idNdermarrje);
                hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", ci));

            }

            grid_ListAuditimUser.PercaktoTitlePanel(Page, MenuInfo, pnlMesazhi,hfState, idPerdorues, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheGjuhe(Session),0, "AuditimUser.aspx", 118, "LoginDatetime", rm, ci);


        }
        
       
        private colTrackUser MerrTeDhenatNgaDB(int idPerdorues)
        {
           
            DbCore.DbAdmin.colTrackUser colUserTack = new DbCore.DbAdmin.colTrackUser();
            DbCore.DbAdmin.colRolPerdorues rolPerd = new DbCore.DbAdmin.colRolPerdorues();
            rolPerd.mbushRolePerdoruesSipasPerdoruesi(idPerdorues);
            DbCore.DbAdmin.clsRoli rol = new DbCore.DbAdmin.clsRoli(rolPerd[0].IdRoli);
            colUserTack.mbushAllTrackUser(rol.IdLicenca, idPerdorues);
            return colUserTack;
        }

        private void konfiguroGride(int IdGjuha, int idNdermarrje)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            GridUtil.PercaktoSettings(grid_ListAuditimUser,Session);
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, idNdermarrje, grid_ListAuditimUser, "grid_ListAuditimUser", "AuditimUser.aspx");
            shtoLoginTime();
            shtoLogoutTime();
            grid_ListAuditimUser.ToolTip = rm.GetString("HyrjeDalje", ci);
            GridUtil.konfigGrideListeEMadhePaTheme(grid_ListAuditimUser, "SessionID");
            grid_ListAuditimUser.FilterExpression = "[aktiv]= true";
        }


        private void shtoLoginTime()
        {
            grid_ListAuditimUser.Columns.Remove(grid_ListAuditimUser.Columns["LoginDatetime"]);
            GridViewDataDateColumn colnew = new GridViewDataDateColumn();
            colnew.PropertiesDateEdit.DateOnError = DateOnError.Undo;
            colnew.FieldName = "LoginDatetime";
            colnew.Settings.AutoFilterCondition = AutoFilterCondition.Greater;
            grid_ListAuditimUser.Columns.Add(colnew);
        }

        private void shtoLogoutTime() //TODO: Te shtohet opsioni i selektimit te ores 
        {
            grid_ListAuditimUser.Columns.Remove(grid_ListAuditimUser.Columns["LogoutDatetime"]);
            GridViewDataDateColumn colnew = new GridViewDataDateColumn();
            colnew.PropertiesDateEdit.DateOnError = DateOnError.Undo;
            colnew.FieldName = "LogoutDatetime";
            colnew.Settings.AutoFilterCondition = AutoFilterCondition.Greater;
            grid_ListAuditimUser.Columns.Add(colnew);
        }
   
        protected void grid_ListAuditimUser_DataBound(object sender, EventArgs e)
        {
            if (grid_ListAuditimUser.Columns["#"] == null)
            {
                ////behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                ////perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = 30;
                check.VisibleIndex = 0;
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                grid_ListAuditimUser.Settings.ShowFilterRow = true;
                grid_ListAuditimUser.Columns.Insert(0,check);
                grid_ListAuditimUser.KeyFieldName = "SessionID";
                grid_ListAuditimUser.SettingsBehavior.AllowSelectByRowClick = true;
                grid_ListAuditimUser.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void grid_ListAuditimUser_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "aktiv")
            {
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add("Online", true);
                (e.Editor as ASPxComboBox).Items.Add("Offline", false);
            }
            
        }


        protected void grid_ListAuditimUser_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "Username" || e.Column.FieldName == "EmriPerdorues")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + " D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + " T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }

        private void mbushGriden(int idPerdoruesi)
        {
            if (CacheLayer.GlobalCacheManager.MySessionCache["coltrackUser"] == null)
                CacheLayer.GlobalCacheManager.MySessionCache["coltrackUser"] = MerrTeDhenatNgaDB(idPerdoruesi);
            grid_ListAuditimUser.DataSource = CacheLayer.GlobalCacheManager.MySessionCache["coltrackUser"];
            grid_ListAuditimUser.DataBind();

        }

        protected void grid_ListAuditimUser_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }
    }
}