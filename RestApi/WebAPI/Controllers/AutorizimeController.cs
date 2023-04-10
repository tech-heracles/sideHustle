using System;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System.Web.SessionState;
using DbCore.DbAdmin;
using System.Threading.Tasks;
using CacheLayer;
using DbCore;
using System.ComponentModel;

namespace RestApi.WebAPI.Controllers
{

    public class AutorizimeController : ApiController, IRequiresSessionState
    {
        private System.Web.SessionState.HttpSessionState Session { get { return HttpContext.Current.Session; } }

        [HttpPost, HttpGet]
        public HttpResponseMessage kaTeDrejteTeHapeAmbjentin(JObject param)
        {
            try
            {
                string emerkomponente = param.Value<string>("emerkomponente");
                string url = param.Value<string>("url");
                bool newTab = param.Value<bool>("newTab");
                int idNdermarje = param.Value<int>("idNdermarje");
                int idPerdoruesi = param.Value<int>("idPerdoruesi");
                int idGjuha = param.Value<int>("idGjuha");          
                return Request.KthePergjigje(AutorizimeRepository.kaTeDrejteTeHapeAmbjentin(Session, emerkomponente, url, newTab, idNdermarje, idPerdoruesi, idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage fshiGrideNgaSessioniLupa()
        {
            try
            {
                return Request.KthePergjigje(AutorizimeRepository.fshiGridNgaSessioniLupa(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheInfoLart(JObject param)
        {
            try
            {
                string vlera = param.Value<string>("idDokRegjistrimi");
                string urlKomponente = param.Value<string>("urlKomponente");
                int idNdermarrje = param.Value<int>("idNdermarrje");
                int IdPerdoruesi = param.Value<int>("IdPerdoruesi");
                int idGjuha = param.Value<int>("idGjuha");
                
                int idDokRegjistrimi = 0;
                if (!int.TryParse(vlera, out idDokRegjistrimi)) idDokRegjistrimi = 0;
                return Request.KthePergjigje(AutorizimeRepository.KtheInfoLart(Session, urlKomponente, idDokRegjistrimi, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session),  idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KonfirmoEmail(JObject param)
        {
            try
            {
                string email = param.Value<string>("email");
                return Request.KthePergjigje(AutorizimeRepository.KonfirmoEmail(email));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public async Task<HttpResponseMessage> CheckIfEmailIsVerified(JObject param)
        {
            try
            {
                string email = param.Value<string>("email");
                bool status = await AutorizimeRepository.CheckIfEmailIsVerified(email);
                return Request.KthePergjigje(status);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheInfoLartPerdorues(JObject param)
        {
            try
            {   
                string urlKomponente = param.Value<string>("urlKomponente");
                int id = param.Value<int>("id");
                int idNdermarrje = param.Value<int>("idNdermarrje");
                int idPerdoruesi = param.Value<int>("idPerdoruesi");
                bool Logu = param.Value<bool>("Logu");
                string ci = param.Value<string>("ci");
                return Request.KthePergjigje(AutorizimeRepository.KtheInfoLart(urlKomponente, id, idNdermarrje, idPerdoruesi, Logu, ci));
            } 
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage createLoginWithGmail(JObject param)
        {
            try
            {   
                string uid = param.Value<string>("uid");
                int idNdermarrje = param.Value<int>("idNdermarrje");
                int idPerdoruesi = param.Value<int>("idPerdoruesi");
                string email = param.Value<string>("email");
                AutorizimeRepository.createLoginWithGmail(uid, idNdermarrje, idPerdoruesi, email,Session);
                return Request.KthePergjigje(true);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigje(false);

            }
        }
        [HttpPost, HttpGet]
        public async Task<HttpResponseMessage> userControls(JObject param)
        {
            try
            {   
                string uid = param.Value<string>("uid");
                int idNdermarrje = param.Value<int>("idNdermarrje");
                int idPerdoruesi = param.Value<int>("idPerdoruesi");
                string email = param.Value<string>("email");
                return Request.KthePergjigje(await AutorizimeRepository.userControls(idPerdoruesi, email));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigje("");

            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheUserTeKonfirmuar(JObject param)
        {
            try
            {   
                string shenime = param.Value<string>("shenime");
                return Request.KthePergjigje(AutorizimeRepository.merrShenimePerdoruesi(shenime));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigje(false);

            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheLicence(JObject param)
        {
            try
            {
                int idNdermarrje = param.Value<int>("idNdermarrje");
                int idPerdoruesi = param.Value<int>("idPerdoruesi");
                clsLicenca licenca = new clsLicenca();
                licenca.mbushLicencen(idPerdoruesi);
                return Request.KthePergjigje(new {kodlicenca = licenca.KodLicenca, datelicenca = licenca.DateSkadimi, llojlicenca = licenca.IdLlojLicenca, nrperdorues = licenca.NrPerdoruesish, nrndermarrje = licenca.NrNdermarjesh });
            } 
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ruajNeSessionURLART(JObject param)
        {
            try
            {
                string url = param.Value<string>("url");

                return Request.KthePergjigje(AutorizimeRepository.ruajNeSessionURLART(Session, url));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage vendosPeriudhen(JObject param)
        {
            try
            {
                int idPeriudha = param.Value<int>("idPeriudha");
                int idgjuha = param.Value<int>("idgjuha");
                string otherScopeID = param.Value<string>("otherScopeID");
                return Request.KthePergjigje(AutorizimeRepository.vendosPeriudhen(Session, idPeriudha, idgjuha, otherScopeID));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheMesazhPerPerdoruesin(JObject param)
        {
            try
            {
                return Request.KthePergjigje(AutorizimeRepository.ktheMesazhPerPerdoruesin());
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
    }
}