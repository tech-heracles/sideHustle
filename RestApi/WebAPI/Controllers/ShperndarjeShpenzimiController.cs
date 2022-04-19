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
    [Authorize]
    public class ShperndarjeShpenzimiController : ApiController, IRequiresSessionState
    {

        private System.Web.SessionState.HttpSessionState Session { get { return HttpContext.Current.Session; } }
        
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheFaturaPerShperndarje(JObject param)
        {
            try
            {
                return Request.KthePergjigje(ShperndarjeShpenzimiRepository.KtheFaturaPerShperndarje(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheFaturatFiltruaraShpenzimiObj(JObject param)
        {
            try
            {
                int idKokaMagazina = param["id"].Value<int>();
                return Request.KthePergjigje(ShperndarjeShpenzimiRepository.KtheFaturatFiltruaraShpenzimiObj(idKokaMagazina));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheACListeLlogarishSipasKlases(JObject param)
        {
            try
            {
                string infixText = param["infixText"].Value<string>();
                int pershk = param["pershk"].Value<int>();
                string klasa = param["klasa"].Value<string>();
                return Request.KthePergjigje(ShperndarjeShpenzimiRepository.KtheACListeLlogarishSipasKlases(infixText, pershk, klasa, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage RuajShperndarjeShpenzimesh(JObject param)
        {
            try
            {
                string komponente = param["komponente"].Value<string>(); 
                object kokaDokumentit = param["kokaDokumentit"].Value<object>(); 
                object trupiDokumentit = param["trupiDokumentit"].Value<object>(); 
                int idKonfigAmbjente = param["idKonfigAmbjente"].Value<int>(); 
                int statusDokumenti = param["statusDokumenti"].Value<int>(); 
                bool kontrolloRivleresim = param["kontrolloRivleresim"].Value<bool>(); 
                int menyreKontabilizimi = param["menyreKontabilizimi"].Value<int>(); 
                bool iLidhur = param["iLidhur"].Value<bool>();
                object llogarite = param["llogarite"].Value<object>();

                return Request.KthePergjigje(ShperndarjeShpenzimiRepository.RuajShperndarjeShpenzimesh(komponente, kokaDokumentit, trupiDokumentit, idKonfigAmbjente, statusDokumenti, kontrolloRivleresim, menyreKontabilizimi, iLidhur, llogarite, Session));   
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage MerrDokumentShperndarjeShpenzimi(JObject param)
        {
            try
            {
                int idDokumenti = param["idDokumenti"].Value<int>();
                return Request.KthePergjigje(ShperndarjeShpenzimiRepository.MerrDokumentShperndarjeShpenzimi(idDokumenti, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ShperndarjeShpenzimiKryejRivleresim(JObject param)
        {
            try
            {
                return Request.KthePergjigje(ShperndarjeShpenzimiRepository.ShperndarjeShpenzimiKryejRivleresim(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        
        [HttpPost, HttpGet]
        public HttpResponseMessage FshiDokumentShperndarjeShpenzimi(JObject param)
        {
            try
            {
                int idDokumenti = param["idDokumenti"].Value<int>();
                bool kontrolloRivleresim = param["kontrolloRivleresim"].Value<bool>();
                return Request.KthePergjigje(ShperndarjeShpenzimiRepository.FshiDokumentShperndarjeShpenzimi(idDokumenti, kontrolloRivleresim, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
    }
}
