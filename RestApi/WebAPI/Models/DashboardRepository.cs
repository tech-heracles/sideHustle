using DbCore;
using DbCore.DbDashboard;
using DbCore.IMBUtils.Messages;
using Newtonsoft.Json;
using System.Web.SessionState;
using System.Xml.Linq;

namespace RestApi.WebAPI.Models
{
    class DashboardRepository
    {
        internal static object GetUserDashboards(HttpSessionState session)
        {
            return new colDashboard(mySessionObjects.ktheIdPerdoruesi(session));
        }

        internal static object GetDashboardUsers(int idDashboard, HttpSessionState session)
        {
            return clsDashboard.GetDashboardUsers(idDashboard, mySessionObjects.ktheIdPerdoruesi(session));
        }

        internal static object DeleteDashboard(int idDashboard, HttpSessionState session)
        {
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            clsDashboard dashboard = mySessionObjects.MerrNgaSession<clsDashboard>(session, $"dashboard_{idDashboard}");
            if (dashboard == null)
            {
                dashboard = new clsDashboard(idDashboard, idPerdoruesi);
                mySessionObjects.RuajNeSession<clsDashboard>(session, dashboard, $"dashboard_{idDashboard}");
            }
            if (dashboard.IdDashboard == 0)
                return new MesazhGabimi(MessagesResource.Messages["msg_D_reopenManager"]);

            if (dashboard.IdDashboard < 0)
                return new MesazhGabimi(MessagesResource.Messages["msg_D_deleteDefault"]);

            if (dashboard.IdDashboard > 0 && dashboard.IdKrijuesi != idPerdoruesi)
                return clsDashboard.FshiPerdoruesNgaDashboardi(idDashboard, idPerdoruesi.ToString());

           clsMesazh mesazh = dashboard.Fshi();
           if(mesazh.Status)
                mySessionObjects.hiqObjectNeSesion(session, $"dashboard_{dashboard.IdDashboard}");

            return mesazh;
        }

        internal static object ShareDashboard(int idDashboard, string idPerdoruesish, HttpSessionState session)
        {
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            clsDashboard dashboard = mySessionObjects.MerrNgaSession<clsDashboard>(session, $"dashboard_{idDashboard}");
            if (dashboard == null)
            {
                dashboard = new clsDashboard(idDashboard, idPerdoruesi);
                mySessionObjects.RuajNeSession(session, dashboard, $"dashboard_{idDashboard}");
            }

            if (dashboard.IdDashboard == 0)
                return new MesazhGabimi(MessagesResource.Messages["msg_D_reopenManager"]);

            if (dashboard.IdKrijuesi != idPerdoruesi || dashboard.IdDashboard < 0)
                return new MesazhGabimi(MessagesResource.Messages["msg_D_shareException"]);

            return clsDashboard.ShtoPerdoruesNeDashboard(idDashboard, idPerdoruesish);
        }

        internal static object SaveDashboardsChanges(object dashboards, HttpSessionState session)
        {
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            colDashboard oldDashboardsList = new colDashboard(idPerdoruesi);
            colDashboard dashboardsList = JsonConvert.DeserializeObject<colDashboard>(dashboards.ToString());
            foreach (clsDashboard dashboard in dashboardsList)
            {
                clsDashboard oldDashboard = oldDashboardsList.Find(x => x.IdDashboard == dashboard.IdDashboard);
                if (oldDashboard == null)
                    continue;
                if(oldDashboard.IdKrijuesi != idPerdoruesi)
                {
                    dashboard.Name = oldDashboard.Name;
                    dashboard.RefreshTime = oldDashboard.RefreshTime;
                    dashboard.DashboardXml = oldDashboard.DashboardXml;
                }
                else
                {
                    XDocument xmlDoc = XDocument.Parse(dashboard.DashboardXml);
                    xmlDoc.Root.Element("Title").SetAttributeValue("Text", dashboard.Name);
                    dashboard.DashboardXml = xmlDoc.ToString();
                }
                try
                {
                    dashboard.UpdateUserDashboard(idPerdoruesi);
                }
                catch
                {
                    continue;
                }
            }
            return new MesazhSuksesi(MessagesResource.Messages["msg_D_updatedSuccessfully"]); 
        }
    }
}
