using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace RestApi.WebAPI.Controllers
{
    public class DashboardController : ApiController, IRequiresSessionState
    {
        private HttpSessionState Session { get { return HttpContext.Current.Session; } }

        [HttpPost, HttpGet]
        public HttpResponseMessage GetUserDashboards(JObject param)
        {
            try
            {
                return Request.KthePergjigje(DashboardRepository.GetUserDashboards(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        
        [HttpPost, HttpGet]
        public HttpResponseMessage GetDashboardUsers(JObject param)
        {
            try
            {
                var idDashboard = param.Value<int>("idDashboard");
                return Request.KthePergjigje(DashboardRepository.GetDashboardUsers(idDashboard, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage DeleteDashboard(JObject param)
        {
            try
            {
                int idDashboard = param.Value<int>("idDashboard");
                return Request.KthePergjigje(DashboardRepository.DeleteDashboard(idDashboard, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ShareDashboard(JObject param)
        {
            try
            {
                int idDashboard = param.Value<int>("idDashboard");
                string idPerdoruesish = param.Value<string>("idPerdoruesish");
                return Request.KthePergjigje(DashboardRepository.ShareDashboard(idDashboard, idPerdoruesish, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage SaveDashboardsChanges(JObject param)
        {
            try
            {
                object dashboards = param.Value<object>("dashboards");
                return Request.KthePergjigje(DashboardRepository.SaveDashboardsChanges(dashboards, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
    }
}
