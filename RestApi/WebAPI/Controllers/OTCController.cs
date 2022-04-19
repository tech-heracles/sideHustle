using DbCore.DbOTC;
using Newtonsoft.Json.Linq;
using RestApi.Models;
using RestApi.WebAPI.ApiUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RestApi
{
    [Authorize]
    public class OTCController : ApiController
    {
        [HttpPost, HttpGet]
        public HttpResponseMessage DergoKodMeSms(JObject param)
        {
            try
            {
                var msisdn = param["msisdn"].Value<string>();

                return Request.KthePergjigje(OTCRepository.DergoKodMeSms(msisdn));
            } catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ValidoGoldenMSISDN(JObject param)
        {
            try
            {
                var msisdn = param["msisdn"].Value<string>();
                var kodiFitues = param["kodiFitues"].Value<string>();
                int lloji = param["lloji"].Value<int>();

                return Request.KthePergjigje(OTCRepository.ValidoMSISDN(msisdn, kodiFitues, lloji));
            } catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null, ex);
            }
        }
    }
}
