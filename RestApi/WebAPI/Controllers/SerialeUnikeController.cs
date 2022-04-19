using System;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System.Web.SessionState;

namespace RestApi.WebAPI.Controllers
{
    /// <summary>
    /// controlleri i cili permban te gjitha web service-et qe do perdoren ne lidhje me serialet unike
    /// </summary>

    [Authorize]
    public class SerialeUnikeController : ApiController, IRequiresSessionState
    {
        private HttpSessionState Session => HttpContext.Current.Session;

        [HttpPost, HttpGet]
        public HttpResponseMessage merrKategoriSeriali()
        {
            try
            {
                return Request.KthePergjigje(SerialeUnikeRepository.merrKategoriSeriali(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }

        }

        [HttpPost, HttpGet]
        public HttpResponseMessage dergoFileTransferimiSerialeUnike(JObject param)
        {
            try
            {
                int idNdermarje = param["idNdermarje"].Value<int>();
                string kategoriSeriali = param["kategoriSeriali"].Value<string>();
                string data = param["data"].Value<string>();
                int idLlojDokumentMag = param["idLlojDokumentMag"].Value<int>();
                int idMetoda = param["idMetoda"].Value<int>();
                return Request.KthePergjigje(SerialeUnikeRepository.DergoFileTransferimiSerialeUnike(idNdermarje, kategoriSeriali, data, idLlojDokumentMag, idMetoda));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }

        }

        [HttpPost, HttpGet]
        public HttpResponseMessage dergoFileTransferimiSerialeUnikePerShumeMetoda(JObject param)
        {
            try
            {
                int idNdermarje = param["idNdermarje"].Value<int>();
                string kategoriSeriali = param["kategoriSeriali"].Value<string>();
                string data = param["data"].Value<string>();
                int idLlojDokumentMag = param["idLlojDokumentMag"].Value<int>();
                int[] idMetoda = param["idMetoda"].ToObject<int[]>();
                return Request.KthePergjigje(SerialeUnikeRepository.DergoFileTransferimiSerialeUnike(idNdermarje, kategoriSeriali, data, idLlojDokumentMag, idMetoda));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }

        }


        [HttpPost, HttpGet]
        public HttpResponseMessage dergoFileTransferimiGjendjeAparate(JObject param)
        {
            try
            {
                int idNdermarje = param["idNdermarje"].Value<int>();
                string data = param["data"].Value<string>();
                int idMetoda = param["idMetoda"].Value<int>();
                return Request.KthePergjigje(SerialeUnikeRepository.DergoFileTransferimiGjendjeAparate(idNdermarje, data, idMetoda));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }

        }
        [HttpPost, HttpGet]
        public HttpResponseMessage MbushTrupKategoriSeriali(JObject param)
        {
            try
            {
                var guidString = param["guidString"].Value<string>();
                var idNdermarrje = param["idNdermarrje"].Value<int>();
                var idSeti = param["idSeti"].Value<int>();
                return Request.KthePergjigje(SerialeUnikeRepository.merrTrupSerialeUnike(Session, guidString, idNdermarrje, idSeti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ShtoSerial(JObject param)
        {
            try
            {
                string seriali = param["seriali"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                string guidString = param["guidString"].Value<string>();
                DateTime date = (param["date"].Value<DateTime>()).ToLocalTime();
                string serialiIVjeter = param["serialiIVjeter"].Value<string>();
                int sasia = param["sasia"].Value<int>();
                string ArtikujSet = param["ArtikujSet"].Value<string>();
                int idMag = param["IdMag"].Value<int>();
                bool gjenerimAutomatik = param["gjenerimAutomatik"].Value<bool>();
                bool Kthim = param["Kthim"].Value<bool>();
                bool KontrolloSasi = param["KontrolloSasi"].Value<bool>();
                string artikujMeSasi = param["artikujMeSasi"].Value<string>();
                string IdKthimesh = param["IdKthimesh"].Value<string>();
                bool RiktheSerialTevjeter = param["RiktheSerialTevjeter"].Value<bool>();
                int idDok = param["Id"].Value<int>();
                bool shitje = param["Shitje"].Value<bool>();
                bool MerrMagazinePerberesi = param["MerrMagazinePerberesi"].Value<bool>();
                return Request.KthePergjigje(SerialeUnikeRepository.ShtoSerial(Session, guidString, seriali, date, idNdermarrje, serialiIVjeter, sasia, ArtikujSet, idMag, gjenerimAutomatik, KontrolloSasi, artikujMeSasi, RiktheSerialTevjeter, idDok, Kthim, IdKthimesh, shitje, MerrMagazinePerberesi ));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage HiqSerialet(JObject param)
        {
            try
            {
                string guidString = param["guidString"].Value<string>();
                return Request.KthePergjigje(SerialeUnikeRepository.HiqSerialet(Session, guidString));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage MbyllLupeSerialesh(JObject param)
        {
            try
            {
                var guidString = param["guidString"].Value<string>();
                var kaNdryshime = param["kaNdryshime"].Value<bool>();
                return Request.KthePergjigje(SerialeUnikeRepository.MbyllLupeSerialesh(Session, guidString, kaNdryshime));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage HiqSerial(JObject param)
        {
            try
            {
                string guidString = param["guidString"].Value<string>();
                string seriali = param["seriali"].Value<string>();
                return Request.KthePergjigje(SerialeUnikeRepository.HiqSerial(Session, guidString, seriali));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage HiqSerialetPerArtikullDheMagazine(JObject param)
        {
            try
            {
                string guidString = param["guidString"].Value<string>();
                int idArtikulli = param["idArtikulli"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                string mag = param["mag"].Value<string>();
                bool set = param["Set"].Value<bool>();
                return Request.KthePergjigje(SerialeUnikeRepository.HiqSerialetPerArtikullDheMagazine(Session, guidString, idArtikulli, mag, idNdermarrje, set));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage MergeSerialeTeReja(JObject param)
        {
            try
            {
                string guidString = param["guidString"].Value<string>();
                return Request.KthePergjigje(SerialeUnikeRepository.MergeSerialeTeReja(Session, guidString));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
    }
}