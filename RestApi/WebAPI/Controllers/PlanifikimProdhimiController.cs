using System;
using System.Web;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.SessionState;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace RestApi.WebAPI.Controllers
{
    [Authorize]
    public class PlanifikimProdhimiController : ApiController, IRequiresSessionState
    {
        private System.Web.SessionState.HttpSessionState Session { get { return HttpContext.Current.Session; } }

        [HttpPost, HttpGet]
        public HttpResponseMessage MerrDokumentPlanifikimProdhimi(JObject param)
        {
            try
            {
                int idDokumenti = param["idDokumenti"].Value<int>();
                return Request.KthePergjigje(PlanifikimProdhimiRepository.MerrDokumentPlanifikimProdhimi(idDokumenti, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

    }
}
