using DbCore;
using DbCore.DbDashboard;
using DbCore.IMBUtils.Messages;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Web.Framework.Dashboard
{
    public class DataBaseEditaleDashboardStorage : IEditableDashboardStorage
    {
        int userId = 0;
        int companyYearId = 0;
        int companyId = 0;
        ASPxDashboard dashboardControl = null;

        #region Messages
        private string msg_D_notExists => MessagesResource.Messages["msg_D_notExists"];
        private string msg_D_licenceException => MessagesResource.Messages["msg_D_licenceException"];
        private string msg_D_saveNotOwnerException => MessagesResource.Messages["msg_D_saveNotOwnerException"];
        private string msg_D_saveDefaultException => MessagesResource.Messages["msg_D_saveDefaultException"];
        #endregion

        public DataBaseEditaleDashboardStorage(int userId, int companyId, int yearId, ASPxDashboard dashboardControl)
            : base()
        {
            this.userId = userId;
            this.companyYearId = yearId;
            this.companyId = companyId;
            this.dashboardControl = dashboardControl;
        }

        string IEditableDashboardStorage.AddDashboard(XDocument document, string dashboardName)
        {
            if (!CanCreateNewDashboard())
                return string.Empty;

            clsDashboard dashboard = new clsDashboard();
            dashboard.IdKrijuesi = userId;
            dashboard.Name = document.Root.Element("Title").Attribute("Text").Value;

            if (document.Root.Attribute("CurrencyCulture") == null)
                document.Root.SetAttributeValue("CurrencyCulture", GetCurrencyFromCompany());

            document.Root.Element("Title").SetAttributeValue("Alignment", "Left");
            document.Root.Element("Title").SetAttributeValue("IncludeMasterFilterState", "false");
            dashboard.DashboardXml = document.ToString();
            dashboard.RefreshTime = 30;
            clsMesazh mesazh = dashboard.Ruaj();
            if(mesazh)
                mySessionObjects.RuajNeSession(HttpContext.Current.Session, dashboard, $"dashboard{dashboard.IdDashboard}");
            SetDashboardMessage(mesazh);
            return dashboard.IdDashboard.ToString();
        }


        XDocument IDashboardStorage.LoadDashboard(string dashboardID)
        {
            if (int.TryParse(dashboardID, out int dashboardId))
            {
                XDocument dsDocument = mySessionObjects.MerrNgaSession<XDocument>(HttpContext.Current.Session, $"dashboardXml_{dashboardId}");
                if (dsDocument != null)
                    return dsDocument;

                clsDashboard dashboard = mySessionObjects.MerrNgaSession<clsDashboard>(HttpContext.Current.Session, $"dashboard{dashboardId}");
                if(dashboard == null)
                {
                    dashboard = new clsDashboard(dashboardId, userId);
                    mySessionObjects.RuajNeSession(HttpContext.Current.Session, dashboard, $"dashboard{dashboardId}");
                }

                if (string.IsNullOrEmpty(dashboard.DashboardXml))
                {
                    SetDashboardMessage(new MesazhGabimi(msg_D_notExists));
                    return null;
                }

                DevExpress.DashboardCommon.Dashboard ds = new DevExpress.DashboardCommon.Dashboard();
                ds.LoadFromXDocument(XDocument.Parse(dashboard.DashboardXml));

                foreach (DashboardSqlDataSource dataSource in ds.DataSources)
                {
                    foreach (var query in dataSource.Queries)
                    {
                        if (query.GetType() == typeof(StoredProcQuery))
                        {
                            query.Parameters.FirstOrDefault(x => x.Name == "@COMPANYID").Value = Convert.ToDecimal(companyId);
                            query.Parameters.FirstOrDefault(x => x.Name == "@COMPANYYEARID").Value = Convert.ToDecimal(companyYearId);
                            query.Parameters.FirstOrDefault(x => x.Name == "@USERID").Value = Convert.ToDecimal(userId);
                        }
                    }
                }

                dsDocument = ds.SaveToXDocument();
                mySessionObjects.RuajNeSession(HttpContext.Current.Session, dsDocument, $"dashboardXml_{dashboardId}");
                return dsDocument;
            }
            return null;
        }

        IEnumerable<DashboardInfo> IDashboardStorage.GetAvailableDashboardsInfo()
        {
            return new List<DashboardInfo>();
        }

        void IDashboardStorage.SaveDashboard(string dashboardID, XDocument document)
        {
            String[] parameters = dashboardID.Split(';');
            string oldDashboardId = dashboardID;
            int dashboardId = 0;
            string newName = string.Empty;
            bool isSaveAs = (parameters.Length > 1 && parameters[0].ToLower() == "saveas");

            if (isSaveAs)
            {
                int.TryParse(parameters[1], out dashboardId);
                newName = parameters[2];
                document.Root.Element("Title").SetAttributeValue("Text", newName);
                isSaveAs = true;
            }
            else
            {
                int.TryParse(dashboardID, out dashboardId);
                if (dashboardId < 0)
                {
                    SetDashboardMessage(new MesazhGabimi(msg_D_saveDefaultException));
                    return;
                }
            }

            clsDashboard dashboard = new clsDashboard(dashboardId);
            if (dashboard.IdDashboard > 0 && dashboard.IdKrijuesi != userId)
            {
                SetDashboardMessage(new MesazhGabimi(msg_D_saveNotOwnerException));
                return;
            }

            bool isTheSame = (newName == dashboard.Name);

            if (document.Root.Attribute("CurrencyCulture") == null)
                document.Root.SetAttributeValue("CurrencyCulture", GetCurrencyFromCompany());

            dashboard.Name = document.Root.Element("Title").Attribute("Text").Value;
            document.Root.Element("Title").SetAttributeValue("Alignment", "Left");
            document.Root.Element("Title").SetAttributeValue("IncludeMasterFilterState", "false");
            dashboard.DashboardXml = document.ToString();

            clsMesazh mesazh;
            if ((!isSaveAs || isTheSame) && dashboard.IdDashboard > 0)
            {
                mesazh = dashboard.Modifiko();
                if (mesazh)
                {
                    mySessionObjects.RuajNeSession(HttpContext.Current.Session, dashboard, $"dashboard{dashboardId}");
                    mySessionObjects.hiqObjectNeSesion(HttpContext.Current.Session, $"dashboardXml_{dashboardId}");
                }
            }
            else
            {
                if (!CanCreateNewDashboard())
                    return;
                dashboard.IdKrijuesi = userId;
                dashboard.RefreshTime = 30;
                mesazh = dashboard.Ruaj();
                if (mesazh)
                {
                    mySessionObjects.RuajNeSession(HttpContext.Current.Session, dashboard, $"dashboard{dashboard.IdDashboard}");
                }
            }
            SetDashboardMessage(mesazh);
        }

        private string GetCurrencyFromCompany()
        {
            var monedha = DbCore.DbAdmin.clsNdermarrje.ktheMonedheNdermSipasID(companyId, new DbCore.DbAdmin.clsDatabaseAdmin());
            switch (monedha.KodiMonedha.ToLower())
            {
                case "eur":
                    return "sq-XK";
                case "usd":
                    return "en-US";
                case "lek":
                    return "sq-AL";
                default:
                    return "sq-AL";
            }
        }

        private bool CanCreateNewDashboard()
        {
            if (clsDashboard.KtheDashboardOwnerPerLicenceStart(userId) > 0)
            {
                SetDashboardMessage(new MesazhGabimi(msg_D_licenceException));
                return false;
            }
            return true;
        }
        
        private void SetDashboardMessage(clsMesazh message)
        {
            dashboardControl.JSProperties["cpDashboardMessage"] = Newtonsoft.Json.JsonConvert.SerializeObject(message);
        }
    }

}