using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using DbCore.DbRegjistrim;
using DbCore.DbAdmin;
using System.Data;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace RestApi.WebAPI.Controllers
{
    [Authorize]//lejon ta aksesojne vetem userat e loguar
    public class KonfigurimeController : ApiController, IRequiresSessionState
    {
        private HttpSessionState Session => HttpContext.Current.Session;

        [HttpGet]
        public HttpResponseMessage popupShowDelay()
        {
            //Timeout - 3 per arsye se therritja e ketij ws behet 1 minute pasi je loguar 
            //dhe mesazhi duhet te dale 2 minuta para mbarimit te timeout te session-it (1+2) = 3 :D
            return Request.KthePergjigje((60000 * (Session.Timeout - 3)));
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKonfigDB(JObject param)
        {
            try
            {
                int idKomp = param["idKomp"].Value<int>();
                string kodKonf = param["kodKonf"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                string kodKontrollKlienti = param["kodKontrollKlienti"].Value<string>();
                int idKlienti = param["idKlienti"].Value<int>();
                bool shtim = param["shtim"].Value<bool>();
                bool merrFormatKursi = param["merrFormatKursi"].Value<bool>();
                int idGjuha = param["idGjuha"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheKonfigDB(idKomp, kodKonf, idNdermarrje, kodKontrollKlienti, idKlienti, shtim, merrFormatKursi, idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKonfigAmbjentiMeFormatNumrash(JObject param)
        {
            try
            {
                int idKomp = param["idKomp"].Value<int>();
                string kodKonf = param["kodKonf"].Value<string>();
                string kodKontrolli = param["kodKontrolli"].Value<string>();
                int idObjekti = param["idObjekti"].Value<int>();
                bool shtim = param["shtim"].Value<bool>();
                bool merrFormatKursi = param["merrFormatKursi"].Value<bool>();
                bool merrGjitheKonf = param["merrGjitheKonf"].Value<bool>();
                int idGjuha = param["idGjuha"].Value<int>();
                DateTime? dateDok = param["dateDok"] == null ? (DateTime?)null : param["dateDok"].Value<DateTime>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                //HttpStatusCode.Forbidden
                return Request.KthePergjigje(KonfigurimeRepository.ktheKonfigAmbjentiMeFormatNumrash(idKomp, kodKonf, kodKontrolli, idObjekti, shtim, merrFormatKursi, merrGjitheKonf, idGjuha, dateDok, idPerdoruesi, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

   
        [HttpPost, HttpGet]
        public HttpResponseMessage merrVlerenNrAutomatik(JObject param)
        {
            try
            {
                string kodKontrolli = param["kodKontrolli"].Value<string>();
                int idNrAuto = param["idNrAuto"].Value<int>();
                DateTime date = param["date"].Value<DateTime?>().GetValueOrDefault();

                return Request.KthePergjigje(KonfigurimeRepository.merrVlerenNrAutomatik(kodKontrolli, idNrAuto, date));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage eshteLidhur(JObject param)
        {
            try
            {
                int idkomp = param["idkomp"].Value<int>();
                string kodkonfi = param["kodkonfi"].Value<string>();
                string iddokumenti = param["iddokumenti"].Value<string>();
                int idndermarje = param["idNdermarrje"].Value<int>();
                int gjuhe = param["idGjuha"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.eshteLidhur(idkomp, kodkonfi, iddokumenti, idndermarje, gjuhe));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage ktheTipiIdTransportuesi(JObject param)
        {
            try
            {
                int idTransportuesi = param["idTransportues"].Value<int>();
                if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                    return Request.KthePergjigje(KonfigurimeRepository.ktheTipiIdTransportuesi(idTransportuesi));
                else
                    return Request.KthePergjigje("");
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage ktheQytetePerNdermarrje(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheQytetePerNdermarrje(idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrBankaSipasKodit(JObject param)
        {
            try
            {
                string kodBanka = param["kodBanka"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.merrBankaSipasKodit(kodBanka, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrBankaSipasLLojit(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                bool lloji = param["idLlojiArka"].Value<bool>();
                return Request.KthePergjigje(KonfigurimeRepository.merrBankaSipasLLojit(idNdermarrje, idPerdoruesi, lloji));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ruajKolonaGride(JObject param)
        {
            try
            {
                int idGride = param["idGride"].Value<int>();
                int idGjuha = param["idGjuha"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idViti = param["idViti"].Value<int>();
                Dictionary<string, string>[] gridColumns = param["gridColumns"].ToObject<Dictionary<string, string>[]>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ruajKolonaGride(idGride, gridColumns, idGjuha, idNdermarrje, idViti, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
  
   

        [HttpPost, HttpGet]
        public HttpResponseMessage RuajKolonaGrideDheFilter(JObject param)
        {
            try
            {
                int idGride = param["idGride"].Value<int>();
                int idGjuha = param["idGjuha"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idViti = param["idViti"].Value<int>();
                Dictionary<string, string>[] gridColumns = param["gridColumns"].ToObject<Dictionary<string, string>[]>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                string kodFiltri = param["kodFiltri"].Value<string>();
                string filterExp = param["filterExp"].Value<string>();
                string koloneRenditje = param["koloneRenditje"].Value<string>();
                bool renditja = param["renditja"].Value<bool>();
                return Request.KthePergjigje(KonfigurimeRepository.RuajKolonaGrideDheFilter(idGride, gridColumns, idGjuha, idNdermarrje, idViti, idPerdoruesi, kodFiltri, filterExp, koloneRenditje, renditja));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheTipeStandarteKasashPeshoreshSipasLlojit(JObject param)
        {
            try
            {
                int lloji = param["lloji"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheTipeStandarteKasashPeshoreshSipasLlojit(lloji));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajStyleRaporti(JObject param)
        {
            try
            {
                string styleName = param["styleName"].Value<string>();
                string zoomFactor = param["zoomFactor"].Value<string>();
                int exportFormat = param["exportFormat"].Value<int>();
                int exportMode = param["exportMode"].Value<int>();
                int idDesign = param["idDesign"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ruajStyleRaporti(styleName, zoomFactor, exportFormat, exportMode, idDesign, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheVleraFiltri(JObject param)
        {
            try
            {
                string id = param["id"].Value<string>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheVleraFiltri(id));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage lexoMesazhNgaSessioni(JObject param)
        {
            try
            {

                string guidString = param["guidString"] != null ? param["guidString"].Value<string>() : "";
                return Request.KthePergjigje(KonfigurimeRepository.lexoMesazhNgaSessioni(Session, guidString));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage merrNgaSessionURLART(JObject param)
        {
            try
            {
                return Request.KthePergjigje(KonfigurimeRepository.merrNgaSessionURLART(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraKodArt(JObject param)
        {
            try
            {
                string kodKodBarArt = param["kodKodBarArt"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                string idDetajimi = param["idDetajimi"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                KonfigurimTVSHGjateRregj llojTvsh = (KonfigurimTVSHGjateRregj)param["tvshkont"].Value<int>();
                clsTaksa taksaKF = param["TaksaKF"].ToObject<clsTaksa>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheRowVleraKodArt(kodKodBarArt, rreshti, data, idDetajimi, idNdermarrje, idPerdoruesi, llojTvsh, taksaKF));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArtikujPerberes2(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheArtikujPerberes2(id, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKostonArtikujvePerberes(JObject param)
        {
            try
            {
                string artikujtPerberes = param["artikujtPerberes"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheKostonArtikujvePerberes(artikujtPerberes, idNdermarrje, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrcolMeCmimeMeFormatNumrash(JObject param)
        {
            try
            {
                int idartikulli = param["idartikulli"].Value<int>();
                bool merrCmimeBlerje = param["merrCmimeBlerje"].Value<bool>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.merrcolMeCmimeMeFormatNumrash(idartikulli, idNdermarrje, idPerdorues, merrCmimeBlerje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheAktivitete(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheAktivitete(prefixText, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrSkeme(JObject param)
        {
            try
            {
                int idkodifikim = param["idkodifikim"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.merrSkeme(idkodifikim));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajNeSessionURLLupaShpejte(JObject param)
        {
            try
            {
                string url = param["url"].Value<string>();
                string lupa = param["lupa"].Value<string>();
                return Request.KthePergjigje(KonfigurimeRepository.ruajNeSessionURLLupaShpejte(url, lupa, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrNgaSessionURLLupaShpejte(JObject param)
        {
            try
            {
                string lupa = param["lupa"].Value<string>();
                return Request.KthePergjigje(KonfigurimeRepository.merrNgaSessionURLLupaShpejte(lupa, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheTeDhenaArtikullit(JObject param)
        {
            try
            {
                int idArt = param["idArt"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheTeDhenaArtikullit(idArt, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kontrolloDetajimePerVeprime(JObject param)
        {
            try
            {
                string detajimereja = param["detajimereja"].Value<string>();
                string detajimevjetra = param["detajimevjetra"].Value<string>();
                string kodartikulli = param["kodartikulli"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.kontrolloDetajimePerVeprime(detajimereja, detajimevjetra, kodartikulli, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage eshteKodifikimiKFPrind(JObject param)
        {
            try
            {
                string input = param["input"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.eshteKodifikimiKFPrind(input, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage eshteKodifikimiPrind(JObject param)
        {
            try
            {
                string input = param["input"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.eshteKodifikimiPrind(input, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage eshteKPFPrind(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int niveli = param["niveli"].Value<int>();
                int grupi = param["grupi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.eshteKPFPrind(kodi, niveli, grupi, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajNeSessionURLllog(JObject param)
        {
            try
            {
                string url = param["url"].Value<string>();
                return Request.KthePergjigje(KonfigurimeRepository.ruajNeSessionURLllog(url, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrNgaSessionURLllog(JObject param)
        {
            try
            {
                return Request.KthePergjigje(KonfigurimeRepository.merrNgaSessionURLllog(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKodDheIdTransportuesi(JObject param)
        {
            try
            {
                string kodTransportues = param["kodTransportues"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheKodDheIdTransportuesi(kodTransportues, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KaVeprimeProfesioni(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.KaVeprimeProfesioni(id));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage fshiTeDhenatPivotGrideNgaSessioni(JObject param)
        {
            try
            {
                return Request.KthePergjigje(KonfigurimeRepository.fshiTeDhenatPivotGrideNgaSessioni(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage ekzistonNrLlogarie(JObject param)
        {
            try
            {

                string prefixText = param["prefixText"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ekzistonNrLlogarie(prefixText, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKonfigurimFormatNumri(JObject param)
        {
            try
            {

                int idKonfigurim = param["idKonfigurim"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                string kodKontrolli = param["kodKontrolli"].Value<string>();
                int idObjekt = param["idObjekt"].Value<int>();
                bool shtim = param["shtim"].Value<bool>();
                int idKomponente = param["idKomponente"].Value<int>();
                bool merrFormatKursi = param["merrFormatKursi"].Value<bool>();
                int idGjuha = param["idGjuha"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheKonfigurimFormatNumri(idKonfigurim, idNdermarrje, kodKontrolli, idObjekt, shtim, idKomponente, merrFormatKursi, idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKonfig(JObject param)
        {
            try
            {
                int idKomp = param["idKomp"].Value<int>();
                string kodKonf = param["kodKonf"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idGjuha = param["idGjuha"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheKonfig(idKomp, kodKonf, idNdermarrje, idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage ktheVleratEkonfigurimitSipaasLlojitKases(JObject param)
        {
            try
            {
                int idkonfigurimi = param["idkonfigurimi"].Value<int>();
                string Lloji = param["Lloji"].Value<string>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheVleratEkonfigurimitSipaasLlojitKases(idkonfigurimi, Lloji));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajKonfigMenuMajtas(JObject param)
        {
            try
            {
                string konfigurimi = param["konfigurimi"].Value<string>();
                int idndermarje = param["idNdermarrje"].Value<int>();
                int idPerdorues = param["idPerdoruesi"].Value<int>();
                return Request.CreateResponse(HttpStatusCode.OK, KonfigurimeRepository.ruajKonfigMenuMajtas(idPerdorues, idndermarje, konfigurimi));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajKonfigListRaportesh(JObject param)
        {
            try
            {
                string konfigurimi = param["konfigurimi"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idModuli = param["idModuli"].Value<int>();
                return Request.CreateResponse(HttpStatusCode.OK, KonfigurimeRepository.ruajKonfigListRaportesh(idPerdoruesi, idNdermarrje, idModuli, konfigurimi));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheIdKonfigurimiSipasKodit(JObject param)
        {
            try
            {
                string kodKonfig = param["kodKonfig"].Value<string>();
                int idNderm = param["idNderm"].Value<int>();
                return Request.CreateResponse(HttpStatusCode.OK, KonfigurimeRepository.ktheIdKonfigurimiSipasKodit(kodKonfig, idNderm));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheTeDrejtaAmbjenteshPerCRM(JObject param)
        {
            try
            {
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idVitNdermarrje = param["idVitNdermarrje"].Value<int>();
                return Request.CreateResponse(HttpStatusCode.OK, KonfigurimeRepository.ktheTeDrejtaAmbjenteshPerCRM( idPerdoruesi,  idNdermarrje, idVitNdermarrje));
            }
            
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheIdMagNgaKodi(JObject param)
        {
            try
            {
                string kodMag = param["kodMag"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.CreateResponse(HttpStatusCode.OK, KonfigurimeRepository.ktheIdMagNgaKodi(kodMag, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheIdSerialNgaKodi(JObject param)
        {
            try
            {
                string kodSeriali = param["kodSeriali"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.CreateResponse(HttpStatusCode.OK, KonfigurimeRepository.ktheIdSerialNgaKodi(kodSeriali, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheIdKonfigurimiSipasIdKodifikimi(JObject param)
        {
            try
            {
                int idKodifikimi = param["idKodifikimi"].Value<int>();
                int idNderm = param["idNderm"].Value<int>();
                return Request.CreateResponse(HttpStatusCode.OK, KonfigurimeRepository.ktheIdKonfigurimiSipasIdKodifikimi(idKodifikimi, idNderm));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage rregulloGabimAsistenti(JObject param)
        {
            try
            {
                string id = param["id"].Value<string>();
                int idNderm = param["idNderm"].Value<int>();
                int idViti = param["idViti"].Value<int>();
                return Request.CreateResponse(HttpStatusCode.OK, KonfigurimeRepository.rregulloGabimAsistenti(id, idNderm, idViti));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage kthedatesrv(JObject param)
        {
            try
            {

                return Request.CreateResponse(HttpStatusCode.OK, DateTime.Now);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage merrTeDhenaArtikulli(JObject param)
        {
            try
            {
                int idkomp = param["idkomp"].Value<int>();
                string kodkonfi = param["kodkonfi"].Value<string>();
                string idArtikulli = param["idArtikulli"].Value<string>();
                bool merrCmime = param["merrCmime"].Value<bool>();
                bool merrCmimeBlerje = param["merrCmimeBlerje"].Value<bool>();
                int idndermarje = param["idndermarje"].Value<int>();
                int gjuhe = param["gjuhe"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                string kodArtikuli = param["kodArtikulli"].Value<string>();
                return Request.KthePergjigje(KonfigurimeRepository.merrTeDhenaArtikulli(idkomp, kodkonfi, idArtikulli, idndermarje, gjuhe, merrCmime, idPerdorues, merrCmimeBlerje, kodArtikuli, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage merrKonfigurimeMobile()
        {
            try
            {
                DbCore.DbAdmin.clsNdermarrje ndermarrja = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                DbCore.DbAdmin.clsPerdorues perdorues = DbCore.mySessionObjects.kthePerdorues(Session);
                return Request.KthePergjigje(KonfigurimeRepository.merrKonfigurimeMobile(perdorues, ndermarrja, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null, ex);
            }
        }
       

        [HttpGet]
        public HttpResponseMessage merrMenuPersonalizuar(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.merrMenuPersonalizuar(Session, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null, ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage merrMenuSipasTeDrejtave(JObject param)
        {
            try
            {
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idviti = param["idviti"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.merrMenuSipasTeDrejtave(Session, idPerdoruesi, idNdermarrje, idviti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null, ex);
            }
        }
        [HttpGet]
        public HttpResponseMessage merrAutorizimet(JObject param)
        {
            try
            {
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.merrAutorizimet(idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage RuajFilterGrida(JObject param)
        {
            try
            {
                bool ruajFilter = param["ruajFilter"].Value<bool>();
                string filterExpression = param["filterExpression"].Value<string>();
                string idGrida = param["idGrida"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.RuajFilterGrida(Session, ruajFilter, filterExpression, idGrida, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage GetGridColumns(JObject param)
        {
            try
            {
                string gridId = param["gridId"].Value<string>();
                int komponenteId = param["komponenteId"].Value<int>();
                int konfigId = param["konfigId"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.GetGridColumns(Session, gridId, komponenteId, konfigId));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

     
        [HttpPost, HttpGet]
        public HttpResponseMessage GetGridColumnsByIdKonfig(JObject param)
        {
            try
            {
                string emerGrida = param["emerGrida"].Value<string>();
                string emerKomponente = param["emerKomponente"].Value<string>();
                int idKonfig = param["idKonfig"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.GetGridColumnsByIdKonfig(Session, emerGrida, emerKomponente, idKonfig));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage SaveGridColumns(JObject param)
        {
            try
            {
                object gridColumns = param["gridColumns"].Value<object>();
                return Request.KthePergjigje(KonfigurimeRepository.SaveGridColumns(Session, gridColumns));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrTeDhenaPerNumerAutomatik(JObject param)
        {
            try
            {
                int idkomp = param["idkomp"].Value<int>();
                string kodkonfi = param["kodkonfi"].Value<string>();
                string iddokumenti = param["iddokumenti"].Value<string>();
                int idndermarje = param["idNdermarrje"].Value<int>();
                int gjuhe = param["idGjuha"].Value<int>();
                int kategoria = param["kategoria"].Value<int>();
                int selectedIdLlojPeriudhe = param["selectedIdLlojPeriudhe"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.merrTeDhenaPerNumerAutomatik(idkomp, kodkonfi, iddokumenti, idndermarje, gjuhe, kategoria, selectedIdLlojPeriudhe));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrNiveleCmimeshNdermarrjeSipasLlojit(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();                
                int lloji = param["lloji"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.merrNiveleCmimeshNdermarrjeSipasLlojit(Session, idNdermarrje, lloji));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKonfigurimDok(JObject param)
        {
            try
            {

                int IdKoka = param["IdKoka"].Value<int>();
                string lloji = param["lloji"].Value<string>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheKonfigurimDok(Session, lloji, IdKoka));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKonfigurimAmbjentiSipasNenkategorise(JObject param)
        {
            try
            {
                int idNiveli = param["idNiveli"].Value<int>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheKonfigurimAmbjentiSipasNenkategorise(Session, idNiveli));
            }
            catch(Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheNenkategoriSipasKategorive(JObject param)
        {
            try
            {
                int idNiveli = param["idNiveli"].Value<int>();
                object katDokAndKomponentObj = param["katDokAndKomponentObj"].Value<object>();
                return Request.KthePergjigje(KonfigurimeRepository.ktheNenkategoriSipasKategorive(Session, katDokAndKomponentObj, idNiveli));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage GetMenuToolbarItems(JObject param)
        {
            try
            {
                string emerKomponente = param["emerKomponente"].Value<string>();
                
                return Request.KthePergjigje(KonfigurimeRepository.GetMenuToolbarItems(Session, emerKomponente));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage GetPageConfigurations(JObject param)
        {
            try
            {
                string emerKomponente = param["emerKomponente"].Value<string>();

                return Request.KthePergjigje(KonfigurimeRepository.GetPageConfigurations(Session, emerKomponente));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
    }
}