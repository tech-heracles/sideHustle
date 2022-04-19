using AlphaWeb.Infrastructure.Data.AdoNet;
using DbCore.DbDashboard;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.Data.Filtering;
using DevExpress.DataAccess.Sql;
using DevExpress.Xpo.DB;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Web.Framework.Dashboard;

namespace PlatinumWeb.Dashboard
{
    public partial class Dashboard : MyPageBase
    {
        private int _idPerdoruesi;
        private int _idNdermarrja;
        private int _idViti;
        private int _idNdermarrjeVit;
        protected void Page_Load(object sender, EventArgs e)
        {
            _idPerdoruesi = IdPerdoruesi;
            _idNdermarrja = IdNdermarrja;
            _idViti = IdViti;
            _idNdermarrjeVit = IdNdermarrjeVit;

            if (!IsPostBack)
            {
                ShtoVleraTePergjithshmeNeHfState();
                PrepareInitalDashboard();
                PrepareUserRights();
                PrepareMessages();
            }
            SetDashboardConfigurations();
        }

        private void SetDashboardConfigurations()
        {
            string connectionString = ConnectionStringsManager.Instance.GetConnectionString(MyConnectionsManager.GetSelectedConNameServer());
            ASPxDashboard1.SetConnectionStringsProvider(new CustomDataSourceWizardConnectionStringsProvider(connectionString));
            ASPxDashboard1.SetDBSchemaProvider(new DashboardCustomDBSchemaProvider(_idPerdoruesi, _idNdermarrja, _idViti));
            ASPxDashboard1.SetDashboardStorage(new DataBaseEditaleDashboardStorage(_idPerdoruesi, _idNdermarrja, _idNdermarrjeVit, ASPxDashboard1));
        }

        private void PrepareUserRights()
        {
            DbCore.DbAdmin.clsTeDrejtaRoli teDrejta = new DbCore.DbAdmin.clsTeDrejtaRoli();

            int owner = clsDashboard.KtheDashboardOwnerPerLicenceStart(_idPerdoruesi);
            if (owner < 1 || _idPerdoruesi == owner)
                teDrejta.merrTeDrejtaPerKeteKomponente(_idPerdoruesi, _idNdermarrja, _idViti, "Dashboard.aspx");

            hfState.Set("teDrejta", Newtonsoft.Json.JsonConvert.SerializeObject(teDrejta));

            if (!teDrejta.DMod)
                ASPxDashboard1.WorkingMode = WorkingMode.ViewerOnly;
            else
                if(string.IsNullOrEmpty(ASPxDashboard1.InitialDashboardId))
                ASPxDashboard1.WorkingMode = WorkingMode.Designer;

        }

        private void PrepareInitalDashboard()
        {
            clsDashboard dashboard = new clsDashboard(0, _idPerdoruesi);            
            if(dashboard.IdDashboard != 0)
            {
                ASPxDashboard1.InitialDashboardId = dashboard.IdDashboard.ToString();
                Timer.Interval = dashboard.RefreshTime * 60000;
            }
        }

        private void PrepareMessages()
        {
            hfState.Set("msg_D_btnSave", MessagesResource.Messages["msg_D_btnSave"]);
            hfState.Set("msg_D_btnSaveAs", MessagesResource.Messages["msg_D_btnSaveAs"]);
            hfState.Set("msg_D_saveEmptyNameException", MessagesResource.Messages["msg_D_saveEmptyNameException"]);
            hfState.Set("msg_D_saveDefaultException", MessagesResource.Messages["msg_D_saveDefaultException"]);
            hfState.Set("msg_D_btnViewerMode", MessagesResource.Messages["msg_D_btnViewerMode"]);
            hfState.Set("msg_D_btnManage", MessagesResource.Messages["msg_D_btnManage"]);
            hfState.Set("msg_D_btnRefresh", MessagesResource.Messages["msg_D_btnRefresh"]);
            hfState.Set("msg_D_btnChange", MessagesResource.Messages["msg_D_btnChange"]);
            hfState.Set("msg_D_btnCreate", MessagesResource.Messages["msg_D_btnCreate"]);
            hfState.Set("msg_D_btnEdit", MessagesResource.Messages["msg_D_btnEdit"]);
            hfState.Set("msg_D_btnShare", MessagesResource.Messages["msg_D_btnShare"]);
            hfState.Set("msg_D_btnDelete", MessagesResource.Messages["msg_D_btnDelete"]);
            hfState.Set("msg_D_btnCancel", MessagesResource.Messages["msg_D_btnCancel"]);
            hfState.Set("msg_D_columnTitle", MessagesResource.Messages["msg_D_columnTitle"]);
            hfState.Set("msg_D_columnOwner", MessagesResource.Messages["msg_D_columnOwner"]);
            hfState.Set("msg_D_columnRefreshTime", MessagesResource.Messages["msg_D_columnRefreshTime"]);
            hfState.Set("msg_D_columnUserName", MessagesResource.Messages["msg_D_columnUserName"]);
            hfState.Set("msg_D_columnName", MessagesResource.Messages["msg_D_columnName"]);
            hfState.Set("msg_D_columnLastName", MessagesResource.Messages["msg_D_columnLastName"]);
            hfState.Set("msg_D_columnRole", MessagesResource.Messages["msg_D_columnRole"]);
            hfState.Set("msg_D_titleManager", MessagesResource.Messages["msg_D_titleManager"]);
            hfState.Set("msg_D_titleSharing", MessagesResource.Messages["msg_D_titleSharing"]);
            hfState.Set("msg_D_confirmDeleteTitle", MessagesResource.Messages["msg_D_confirmDeleteTitle"]);
            hfState.Set("msg_D_confirmSaveTitle", MessagesResource.Messages["msg_D_confirmSaveTitle"]);
            hfState.Set("msg_D_loseChanges", MessagesResource.Messages["msg_D_loseChanges"]);
            hfState.Set("msg_D_saveChangesQuestion", MessagesResource.Messages["msg_D_saveChangesQuestion"]);
            hfState.Set("msg_D_deleteQuestion", MessagesResource.Messages["msg_D_deleteQuestion"]);
        }

        protected void ASPxDashboard1_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
        {
            if (!IsCallback)
                e.Properties["cpDashboardMessage"] = string.Empty;
        }
    }
}