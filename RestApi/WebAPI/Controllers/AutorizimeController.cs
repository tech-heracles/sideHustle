using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using DbCore.DbAdmin;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;

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
				return Request.KthePergjigje(AutorizimeRepository.KtheInfoLart(Session, urlKomponente, idDokRegjistrimi, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idGjuha));
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
		public async Task<HttpResponseMessage> createLoginWithGmail(JObject param)
		{
			try
			{
				string uid = param.Value<string>("uid");
				int idNdermarrje = param.Value<int>("idNdermarrje");
				int idPerdoruesi = param.Value<int>("idPerdoruesi");
				string email = param.Value<string>("email");
				string accessToken = param.Value<string>("accessToken");
				string url = await AutorizimeRepository.createLoginWithGmail(uid, idNdermarrje, idPerdoruesi, email, Session, accessToken);
				return Request.KthePergjigje(url);
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
				string alphaOrganization = param.Value<string>("alphaOrganization");
				string accessToken = param.Value<string>("accessToken");
				return Request.KthePergjigje(await AutorizimeRepository.userControls(idPerdoruesi, email, idNdermarrje, alphaOrganization, uid, accessToken));
			}
			catch (Exception ex)
			{
				return Request.KthePergjigje("");

			}
		}
		[HttpPost, HttpGet]
		public async Task<HttpResponseMessage> goToDelta(JObject param)
		{
			try
			{
				string uid = param.Value<string>("uid");
				int idNdermarrje = param.Value<int>("idNdermarrje");
				int idPerdoruesi = param.Value<int>("idPerdoruesi");
				string email = param.Value<string>("email");
				string alphaOrganization = param.Value<string>("alphaOrganization");
				string accessToken = param.Value<string>("accessToken");
				return Request.KthePergjigje(await AutorizimeRepository.goToDelta(idPerdoruesi, email, idNdermarrje, alphaOrganization, uid, accessToken));
			}
			catch (Exception ex)
			{
				return Request.KthePergjigje("");

			}
		}
		[HttpPost, HttpGet]
		public async Task<HttpResponseMessage> getUserOrganization(JObject param)
		{
			try
			{
				string uid = param.Value<string>("uid");
				return Request.KthePergjigje(await AutorizimeRepository.getUserOrganization(uid));
			}
			catch (Exception ex)
			{
				return Request.KthePergjigje("Error");

			}
		}
		[HttpPost]
		public async Task<HttpResponseMessage> changeOrganization(JObject param)
		{
			try
			{
				string uid = param.Value<string>("uid");
				string organization = param.Value<string>("organization");
				int enterprise_id = param.Value<int>("enterprise_id");
				int idPerdoruesi = param.Value<int>("idPerdoruesi");
				return Request.KthePergjigje(await AutorizimeRepository.changeOrganization(uid, organization, enterprise_id, idPerdoruesi));
			}
			catch (Exception ex)
			{
				return Request.KthePergjigje(false);

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
				return Request.KthePergjigje(new { kodlicenca = licenca.KodLicenca, datelicenca = licenca.DateSkadimi, llojlicenca = licenca.IdLlojLicenca, nrperdorues = licenca.NrPerdoruesish, nrndermarrje = licenca.NrNdermarjesh });
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