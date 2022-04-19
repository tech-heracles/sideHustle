using System;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System.Web.SessionState;
using DbCore.IMBUtils.Messages;
using System.Web;
using DbCore;
using System.Data;
using DbCore.DbBuxheti;
using System.Net;
using System.Net.Http.Headers;

namespace RestApi.WebAPI.Controllers
{
    [Authorize]
    public class BuxhetiController : ApiController, IRequiresSessionState
    {
        private System.Web.SessionState.HttpSessionState Session { get { return HttpContext.Current.Session; } }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheLlojeBuxhetiSipasIdKategoriBuxhetimi(JObject param)
        {
            try
            {
                var idKategoriBuxhetimi = param.Value<int>("idKategoriBuxhetimi");
                return Request.KthePergjigje(BuxhetiRepository.KtheLlojeBuxhetiSipasIdKategoriBuxhetimi(idKategoriBuxhetimi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheKategoriBuxhetimiSipasIdKategoriBuxhetimi(JObject param)
        {
            try
            {
                var idKategoriBuxhetimi = param.Value<int>("idKategoriBuxhetimi");
                return Request.KthePergjigje(BuxhetiRepository.KtheKategoriBuxhetimiSipasIdKategoriBuxhetimi(MessagesResource.Messages, idKategoriBuxhetimi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheDokumentBuxheti(JObject param)
        {
            try
            {
                var idKomponente = param.Value<int>("idKomponente");
                var idKonfigurim = param.Value<int>("idKonfigurim");
                var idNdermarrje = param.Value<int>("idNdermarrje");
                var idGjuha = param.Value<int>("idGjuha");
                var emerGride = param.Value<string>("emerGride");
                int idKokaBuxheti = 0;
                Int32.TryParse(param.Value<string>("idKokaBuxheti"), out idKokaBuxheti);

                return Request.KthePergjigje(BuxhetiRepository.KtheDokumentBuxheti(idKomponente, idKonfigurim, idNdermarrje, idGjuha, emerGride, idKokaBuxheti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheDokumentAlokimBuxheti(JObject param)
        {
            try
            {
                var idKomponente = param.Value<int>("idKomponente");
                var idKonfigurim = param.Value<int>("idKonfigurim");
                var idNdermarrje = param.Value<int>("idNdermarrje");
                var idGjuha = param.Value<int>("idGjuha");
                var emerGride = param.Value<string>("emerGride");
                var shtimModifikim = param.Value<string>("shtimModifikim");
                var llojKonvertimiNga = param.Value<string>("llojKonvertimiNga");
                int idKokaAlokimi = 0;
                Int32.TryParse(param.Value<string>("idKokaAlokimi"), out idKokaAlokimi);

                return Request.KthePergjigje(BuxhetiRepository.KtheDokumentAlokimBuxheti(idKomponente, idKonfigurim, idNdermarrje, idGjuha, emerGride, idKokaAlokimi, shtimModifikim, llojKonvertimiNga));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        

        [HttpPost, HttpGet]
        public HttpResponseMessage RuajDokumentBuxheti(JObject param)
        {
            try
            {
                var komponente = param.Value<string>("komponente");
                var idNdermarrje = param.Value<int>("idNdermarrje");
                var idPerdoruesi = param.Value<int>("idPerdoruesi");
                var idViti = param.Value<int>("idViti");
                var kokaBuxheti = param.Value<object>("kokaBuxheti");
                var trupiBuxheti = param.Value<object>("trupiBuxheti");
                var idSkemeAprovimi = param.Value<int>("idSkemaWF");
                var statusAprovimi = param.Value<int>("statusAprovimi");
                var idEtapa = param.Value<int>("idEtapa");

                return Request.KthePergjigje(BuxhetiRepository.RuajDokumentBuxheti(Session, komponente, idNdermarrje, idPerdoruesi, idViti, kokaBuxheti, trupiBuxheti, idSkemeAprovimi, statusAprovimi, idEtapa));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage EksportoDokumentBuxheti(JObject param)
        {
            try
            {
                var idNdermarrje = param.Value<int>("idNdermarrje");
                var eshtekonvertim = param.Value<bool>("eshtekonvertim");
                var idkokabuxheti = param.Value<int>("idkokabuxheti");
                var data = param.Value<string>("data");

                var myResponse = Request.CreateResponse(HttpStatusCode.OK);
                myResponse.Content = BuxhetiRepository.EksportoDokumentAlokimBuxheti(idNdermarrje, eshtekonvertim, idkokabuxheti, data);
                myResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                myResponse.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                myResponse.Content.Headers.ContentDisposition.FileName = $"AlokimBuxheti.xlsx";
                return myResponse;
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage EksportoDokumentMiratimBuxheti(JObject param)
        {
            try
            {
                var idkokabuxheti = param.Value<int>("idkokabuxheti");
                var idNdermarrje = param.Value<int>("idNdermarrje");
                var idKomponente = param.Value<int>("idKomponente");
                var data = param.Value<string>("data");
                var idNiveli = param.Value<int>("idNiveli");
                var llojDok = param.Value<string>("llojDok");
                var nrDok = param.Value<string>("nrDok");
                var totaliFaktik = param.Value<decimal>("totaliFaktik");

                var myResponse = Request.CreateResponse(HttpStatusCode.OK);
                myResponse.Content = BuxhetiRepository.EksportoDokumentMiratimBuxheti(idkokabuxheti, idNdermarrje, idKomponente, data, idNiveli, llojDok, nrDok, totaliFaktik);
                myResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                myResponse.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                myResponse.Content.Headers.ContentDisposition.FileName = $"MiratimBuxheti.xlsx";
                return myResponse;
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage PostoDokumentMiratimPlanifikimBuxheti(JObject param)
        {
            try
            {
                var idKokaBuxheti = param.Value<int>("idKokaBuxheti");
                var komponente = param.Value<string>("komponente");
                var idNdermarrje = param.Value<int>("idNdermarrje");
                var idPerdoruesi = param.Value<int>("idPerdoruesi");
                var idViti = param.Value<int>("idViti");

                return Request.KthePergjigje(BuxhetiRepository.PostoDokumentMiratimPlanifikimBuxheti(idKokaBuxheti, idPerdoruesi, idNdermarrje, idViti, komponente));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage PostoDokumentAlokimBuxheti(JObject param)
        {
            try
            {
                var idKokaAlokimi = param.Value<int>("idKokaAlokimi");
                var komponente = param.Value<string>("komponente");
                var idNdermarrje = param.Value<int>("idNdermarrje");
                var idPerdoruesi = param.Value<int>("idPerdoruesi");
                var idViti = param.Value<int>("idViti");
                var idSkemeAprovimi = param.Value<int>("idSkemaWF");
                var statusAprovimi = param.Value<int>("statusAprovimi");

                return Request.KthePergjigje(BuxhetiRepository.PostoDokumentAlokimBuxheti(idKokaAlokimi, idPerdoruesi, idNdermarrje, idViti, komponente, idSkemeAprovimi, statusAprovimi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage FshiDokumentBuxheti(JObject param)
        {
            try
            {
                int[] ids = param["ids"].ToObject<int[]>();
                string komponente = param["komponente"].Value<string>();
                return Request.KthePergjigje(BuxhetiRepository.FshiDokumentBuxheti(Session, ids, komponente));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KontrolloKonvertuar(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();
                int idKonfigKonvertimiNga = param["idKonfigKonvertimiNga"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idGjuha = param["idGjuha"].Value<int>();
                return Request.KthePergjigje(BuxhetiRepository.KontrolloKonvertuar(id, idKonfigKonvertimiNga, idNdermarrje, idPerdoruesi, idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage NdryshoStatusDokumenti(JObject param)
        {
            try
            {
                int[] idBuxhetiKoka = param["idBuxhetiKoka"].ToObject<int[]>();
                int idStatusDok = param["idStatusDok"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idViti = param["idViti"].Value<int>();
                string komponente = param["komponente"].Value<string>();
                string shenime = param["shenime"].Value<string>();
                return Request.KthePergjigje(BuxhetiRepository.NdryshoStatusDokumenti(idBuxhetiKoka, idStatusDok, idPerdoruesi, idNdermarrje, idViti, komponente, shenime, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheNdermarrjeSipasId(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(BuxhetiRepository.KtheNdermarrjeSipasId(idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheACListeLlojeBuxheti(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                string infixText = param["infixText"].Value<string>();
                return Request.KthePergjigje(BuxhetiRepository.KtheACListeLlojeBuxheti(infixText, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheACListeLlojeBuxhetiSipasLlogarise(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idLlogaria = param["idLlogaria"].Value<int>();
                string infixText = param["infixText"].Value<string>();
                return Request.KthePergjigje(BuxhetiRepository.KtheACListeLlojeBuxhetiSipasLlogarise(infixText, idNdermarrje, idLlogaria));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheACListeKategoriBuxhetimi(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                string infixText = param["infixText"].Value<string>();
                return Request.KthePergjigje(BuxhetiRepository.KtheACListeKategoriBuxhetimi(infixText, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheListeTaksashSipasNdermarrjes(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(BuxhetiRepository.ktheListeTaksashSipasNdermarrjes(idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheDokumentBuxhetiNgaKonvertimi(JObject param)
        {
            try
            {
                var idKomponente = param.Value<int>("idKomponente");
                var idKonfigurim = param.Value<int>("idKonfigurim");
                var idNdermarrje = param.Value<int>("idNdermarrje");
                var idGjuha = param.Value<int>("idGjuha");
                var emerGride = param.Value<string>("emerGride");
                var llojKonvertimiNga = param.Value<string>("llojKonvertimiNga");
                int idKokaBuxheti = 0;
                Int32.TryParse(param.Value<string>("idKokaBuxheti"), out idKokaBuxheti);

                return Request.KthePergjigje(BuxhetiRepository.KtheDokumentBuxhetiNgaKonvertimi(idKomponente, idKonfigurim, idNdermarrje, idGjuha, emerGride, idKokaBuxheti, llojKonvertimiNga));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheKomponenteBuxhetiMeId(JObject param)
        {
            try
            {
                int idKomponente = param["id"].Value<int>();
                return Request.KthePergjigje(BuxhetiRepository.KtheKomponenteBuxhetiMeId(idKomponente));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage RuajKomponenteBuxheti(JObject param)
        {
            try
            {
                object komponenteBuxheti = param["komponenteBuxheti"].Value<object>();
                object komponenteBuxhetiLidhje = param["komponenteBuxhetiLidhje"].Value<object>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idViti = param["idViti"].Value<int>();
                string komponente = param["komponente"].Value<string>();
                return Request.KthePergjigje(BuxhetiRepository.RuajKomponenteBuxheti(komponenteBuxheti, komponenteBuxhetiLidhje, komponente, idPerdoruesi, idNdermarrje, idViti, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage FshiKomponenteBuxheti(JObject param)
        {
            try
            {
                int[] idKomponenteBuxheti = param["idKomponenteBuxheti"].ToObject<int[]>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idViti = param["idViti"].Value<int>();
                string komponente = param["komponente"].Value<string>();
                return Request.KthePergjigje(BuxhetiRepository.FshiKomponenteBuxheti(idKomponenteBuxheti, komponente, idPerdoruesi, idNdermarrje, idViti, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheKategoriPerLidhjeSipasBuxhetit(JObject param)
        {
            try
            {
                int idKomponenteBuxheti = param["idKomponenteBuxheti"].Value<int>();
                int? idLlojBuxheti = param["idLlojBuxheti"].Value<int?>();
                return Request.KthePergjigje(BuxhetiRepository.KtheKategoriPerLidhjeSipasBuxhetit(idKomponenteBuxheti, idLlojBuxheti != null ? (int)idLlojBuxheti : 0));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheKomponenteBuxhetiVlera(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(BuxhetiRepository.KtheKomponenteBuxhetiVlera(idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage RuajKomponenteBuxhetiVlera(JObject param)
        {
            try
            {
                object komponenteBuxhetiVlere = param["komponenteBuxhetiVlere"].Value<object>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idViti = param["idViti"].Value<int>();
                string komponente = param["komponente"].Value<string>();
                return Request.KthePergjigje(BuxhetiRepository.RuajKomponenteBuxhetiVlera(komponenteBuxhetiVlere, komponente, idPerdoruesi, idNdermarrje, idViti, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage GjeneroPlanifikimBuxheti(JObject param)
        {
            try
            {
                var idKomponente = param.Value<int>("idKomponente");
                var idKonfigurim = param.Value<int>("idKonfigurim");
                var idKokaBuxheti = param.Value<int>("idKokaBuxheti");
                var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);

                return Request.KthePergjigje(BuxhetiRepository.GjeneroPlanifikimBuxheti(Session, idKomponente, idKonfigurim, idNdermarrje, idPerdoruesi, idKokaBuxheti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheLlogariDhePrindSipasIds(JObject param)
        {
            try
            {
                var idPrindi = param.Value<int>("idPrindi");
                var idLlogari = param.Value<int>("idLlogari");
                return Request.KthePergjigje(BuxhetiRepository.KtheLlogariDhePrindSipasIds(idPrindi, idLlogari));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
    }
}
