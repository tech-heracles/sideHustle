using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.SessionState;

namespace RestApi.WebAPI.Controllers
{
    public class AutomatizimController : ApiController, IReadOnlySessionState
    {

        public HttpResponseMessage DergoEmailRaportKartolinaDitelindjes(JObject param)
        {
            try
            {
                var kodNdermarrje = param["KODNDERMARRJE"].Value<string>();
                var username = param["USERNAME"].ToObject<string>();

                return Request.KthePergjigje(AutomatizimRepository.DergoEmailRaportKartolinaDitelindjes(kodNdermarrje, username));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage DergoEmailRaportCRM(JObject param)
        {
            try
            {
                var kodNdermarrje = param["KODNDERMARRJE"].Value<string>();
                var username = param["USERNAME"].ToObject<string>();
                var emailet = param["EMAILET"].ToObject<string>();
                return Request.KthePergjigje(AutomatizimRepository.DergoEmailRaportCRM(kodNdermarrje, username, emailet));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage NjoftoMeEmailPerTakimetEPanisura(JObject param)
        {
            try
            {
                var kodNdermarrje = param["KODNDERMARRJE"].Value<string>();
                var emailet = param["EMAILET"].ToObject<string>();
                return Request.KthePergjigje(AutomatizimRepository.NjoftoMeEmailPerTakimetEPanisura(kodNdermarrje, emailet));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        public HttpResponseMessage eksportAutomatikDokumentesh(JObject param)
        {
            try
            {
                var templateEksporti  = param["templateEksporti"].Value<string>();
                var idNdermarrje = param["idNdermarrje"].ToObject<int>();
                var idPerdorues  = param["idPerdorues"].ToObject<int>();

                return Request.KthePergjigje(AutomatizimRepository.eksportAutomatikDokumentesh(templateEksporti, idNdermarrje, idPerdorues));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage importAutomatikShitjeBlerje(JObject param)
        {
            try
            {
                var templateImporti = param["templateImporti"].Value<string>();
                var idNdermarrje = param["idNdermarrje"].ToObject<int>();
                var idPerdorues = param["idPerdorues"].ToObject<int>();

                return Request.KthePergjigje(AutomatizimRepository.importAutomatikShitjeBlerje(templateImporti, idNdermarrje, idPerdorues));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage MerrLogeSistemiDataTable(JObject param)
        {
            try
            {
                DateTime dataNga = DateTime.Parse(param["dataNga"].Value<string>());
                DateTime dataDeri = DateTime.Parse(param["dataDeri"].ToObject<string>());
                string moduli = param["moduli"].ToObject<string>();
                string verbosity = param["verbosity"].ToObject<string>();

                return Request.KthePergjigje(AutomatizimRepository.MerrLogeSistemiDataTable(dataNga, dataDeri, moduli, verbosity));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage RuajNivelVerbosity(JObject param)
        {
            try
            {
                int verbosity = param["verbosity"].ToObject<int>();
                string moduli = param["moduli"].ToObject<string>();

                return Request.KthePergjigje(AutomatizimRepository.RuajNivelVerbosity(verbosity, moduli));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

    }
}