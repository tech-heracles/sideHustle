using DbCore;
using DbCore.DbQendraKosto;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace RestApi.WebAPI.Controllers
{
    public class QendraKostoController : ApiController, IRequiresSessionState
    {

        private HttpSessionState Session { get { return HttpContext.Current.Session; } }
        private int idNdermarrje => mySessionObjects.merrIdNdermarrjeSesioni(Session);
        private int idVitNdermarrje => mySessionObjects.ktheNdermarrjeVit(Session);
        private int idPerdoruesi => mySessionObjects.ktheIdPerdoruesi(Session);
        private int idGjuha => mySessionObjects.ktheGjuhe(Session);

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheDataSourceKolonash(JObject param)
        {
            try
            {
                int idKokaFleteKontabel = param.Value<int>("idKokaFleteKontabel");
                bool gjithellogarite = param.Value<bool>("gjitheLlogarite");
                return Request.KthePergjigje(QendraKostoRepository.KtheDataSourceKolonash(idNdermarrje, idPerdoruesi, idKokaFleteKontabel, gjithellogarite));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheKokaQKSipasIDGjeneruesDheKonfig(JObject param)
        {
            try
            {
                int idGjenerues = param.Value<int>("idGjenerues");
                int idKonfig = param.Value<int>("idKonfig");
                int idKoka = param.Value<int>("idKoka");
                bool sipasKokes = param.Value<bool>("sipasKokes");
                return Request.KthePergjigje(QendraKostoRepository.KtheKokaQKSipasIDGjeneruesDheKonfigOseIdKoka(idGjenerues, idKonfig, idKoka, sipasKokes));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrPershkrimLlogQKRe(JObject param)
        {
            try
            {
                int idLlog = param["idLlog"].Value<int>();
                decimal vlefta = param["vlefta"].Value<decimal>();
                string kodqendra = param["kodqendra"].Value<string>();
                DateTime data = param["data"].Value<DateTime>();
                return Request.KthePergjigje(QendraKostoRepository.merrPershkrimLlogQKRe(idLlog, vlefta, kodqendra, data, idNdermarrje));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage VendosQKoseSkemeNeGride(JObject param)
        {
            try
            {
                int idGjenerues = param.Value<int>("idGjenerues");
                string kodi = param.Value<string>("kodi");
                int lloji = param.Value<int>("lloji");
                return Request.KthePergjigje(QendraKostoRepository.VendosQKoseSkemeNeGride(idGjenerues, lloji, kodi, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage RuajTrupRegjQendraKosto(JObject param)
        {
            try
            {
                int idKoka = param.Value<int>("idKoka");
                object[] trupi = param["trupi"].ToObject<object[]>();

                string nrDok = param.Value<string>("nrDok");
                DateTime dteDtDok = param.Value<DateTime>("dteDtDok");
                string nrRef = param.Value<string>("nrRef");
                DateTime dteDtRegj = param.Value<DateTime>("dteDtRegj");
                string shenime = param.Value<string>("shenime");
                int idStatusDok = param.Value<int>("idStatusDok");
                int idKonfig = param.Value<int>("idKonfig");
                int idGjenerues = param.Value<int>("idGjenerues");
                int idKonfigGjenerues = param.Value<int>("idKonfigGjenerues");

                string komponenteNga = param.Value<string>("komponenteNga");
                string hfShtimModifikim = param.Value<string>("hfShtimModifikim");
                bool kontrolloLidhur = param.Value<bool>("kontrolloLidhur");
                bool kontrolloShperndare = param.Value<bool>("kontrolloShperndare");

                return Request.KthePergjigje(QendraKostoRepository.RuajQendraKosto(idGjuha, idNdermarrje, idVitNdermarrje, idPerdoruesi, idKoka, trupi, nrDok, dteDtDok, nrRef, dteDtRegj, shenime, idStatusDok, idKonfig, idGjenerues, idKonfigGjenerues, komponenteNga, hfShtimModifikim, kontrolloLidhur, kontrolloShperndare));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage LlogaritVleraQK(JObject param)
        {
            try
            {
                string nrllogari = param["nrllogari"].Value<string>();
                string kodqendra = param["kodqendra"].Value<string>();
                DateTime data = param["data"].Value<DateTime>();
                decimal vlefta = param["vlefta"].Value<decimal>();
                string lloji = param["lloji"].Value<string>();
                return Request.KthePergjigje(QendraKostoRepository.LlogaritVleraQK(nrllogari, kodqendra, data, vlefta, lloji, idNdermarrje));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage LlogaritVleraQKMeKursTrupi(JObject param)
        {
            try
            {
                string nrllogari = param["nrllogari"].Value<string>();
                string kodqendra = param["kodqendra"].Value<string>();
                DateTime data = param["data"].Value<DateTime>();
                decimal vlefta = param["vlefta"].Value<decimal>();
                string lloji = param["lloji"].Value<string>();
                decimal kursiTrupi = param["kursiTrupi"].Value<decimal>();
                return Request.KthePergjigje(QendraKostoRepository.LlogaritVleraQKMeKursTrupi(nrllogari, kodqendra, data, vlefta, lloji, kursiTrupi, idNdermarrje));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrPershkrimLlog(JObject param)
        {
            try
            {
                int idLlog = param["idLlog"].Value<int>();
                int idGjenerues = param["idGjenerues"].Value<int>();
                int idMonedhaQendra = param["idMonedhaQendra"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                return Request.KthePergjigje(QendraKostoRepository.merrPershkrimLlog(idLlog, idGjenerues, idMonedhaQendra, data, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage MerrTrupFKSipasKokes(JObject param)
        {
            try
            {
                int idKoka = param["idKoka"].Value<int>();
                return Request.KthePergjigje(QendraKostoRepository.MerrTrupFKSipasKokes(idKoka));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
    }
}
