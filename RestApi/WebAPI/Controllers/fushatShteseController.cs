using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using Newtonsoft.Json.Linq;
using System.Web.SessionState;

namespace RestApi.WebAPI.Controllers
{
    public class FushatShteseController : ApiController, IRequiresSessionState
    {
        public HttpResponseMessage MerrDataAktivizimi(JObject param)
        {
            try
            {
                JToken id;
                JToken modeli;
                int idLidhese = 0;
                int idModeli = 0;
                if (param.TryGetValue("idLidhese", out id) && id.ToString() != "")
                    idLidhese = id.Value<int>();
                if (param.TryGetValue("idModeli", out modeli) && modeli.ToString() != "")
                    idModeli = modeli.Value<int>();

                if (idModeli == 0 || idLidhese == 0)
                    return Request.KthePergjigje(null);
                return Request.KthePergjigje(FushatShteseRepository.KtheDataAktivizimi(idLidhese, idModeli));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
    }
}
