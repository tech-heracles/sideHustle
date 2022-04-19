using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using DbCore;
using DbCore.DbListPagesat;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System.Linq;
using DbCore.IMBUtils.Logging;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Fiskalizimi.API;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace RestApi.WebAPI.Controllers
{
    /// <summary>
    ///     kjo klase shperben si public API per modulin e listpageses,
    ///     ne kete klase duhet te validohen parametrat qe vijne nga burime te ndryshme,
    ///     KUJDES!! NUK LEJOHET TE VENDOSEN BUSSINES RULES NE CONTROLLER
    /// </summary>
    public class ListPagesaController : ApiController, IRequiresSessionState
    {
        private HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }
        public ListPagesaController()
        {
            StaticCache.ClearCache();
        }

        /// <summary>
        ///     llogarit pagen per nje punonjes ne baze te komponenteve me te cilat ai eshte i lidhur
        /// </summary>
        /// <param name="parametrat"></param>
        /// <param name="colKompPage"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage LlogaritPage(JObject parametrat)
        {
            try
            {
                var arrvlera = parametrat["arrvlera"].ToObject<decimal[]>();
                var arrparam = parametrat["arrvleraParam"].ToObject<decimal[]>();
                var arrkodi = parametrat["arrKodi"].ToObject<string[]>();
                var arrmosndrysho = parametrat["arrMosNdrysho"].ToObject<bool[]>();
                var data = (parametrat["data"].ToObject<DateTime>()).ToLocalTime();
                var idpunonjes = parametrat["idpunonjesi"].ToObject<int>();
                var llogariDP = parametrat["llogaritDP"].Value<bool>();
                //var nrpersonal = parametrat["nrpersonal"].Value<string>();
                //var emri = parametrat["emri"].Value<string>();
                var muaji = parametrat["muaji"].Value<int>();
                var ditemuajindryshueshme = parametrat["ditemuajindryshueshme"].Value<bool>();
                var kodi = parametrat["kodi"].Value<string>();
                var vjenNgaMuajt = parametrat["vjenNgaMuajt"].Value<bool>();
                var ditemuaji = parametrat["ditemuaji"].Value<string>() == bool.TrueString;
                var pagemuaji = parametrat["pagemuaji"].Value<string>() == bool.TrueString;
                var muajitjeter = parametrat["muajitjeter"].Value<int>();
                var vititjeter = parametrat["vititjeter"].Value<int>();
                var merrimporte = parametrat["merrimporte"].Value<bool>();
                var kodimporti = parametrat["kodimporti"].Value<string>();
                var vjenNgaKomponentja = parametrat["vjenNgaKomponentja"].Value<bool>();
                var newrecord = parametrat["newrecord"].Value<bool>();
                var llogaritDiteLejeNgaImporti = parametrat["llogaritDiteLejeNgaImporti"].Value<bool>();
                var idNdermarrjeVit = parametrat["idNdermarrjeVit"].Value<int>();
                var idGjuha = parametrat["idGjuha"].Value<int>();
                var idNdermarrje = parametrat["idNdermarrje"].Value<int>();
                var idKokaLp = parametrat["idKokaLp"].Value<int?>().GetValueOrDefault();
                var merrVlereDefault = parametrat["merrVlereDefault"].Value<bool>();
                var buttonClickNgaKomponente = parametrat["buttonClickNgaKomponente"].Value<bool>();

                object result = ListPagesaRepository.LlogaritPagePerPunonjes(idpunonjes, data, data, arrkodi, arrparam, arrmosndrysho, arrvlera, llogariDP, muaji,
                      ditemuajindryshueshme, kodi, vjenNgaMuajt, ditemuaji, pagemuaji, muajitjeter, vititjeter, merrimporte, kodimporti, vjenNgaKomponentja, newrecord,
                      llogaritDiteLejeNgaImporti, Session, idNdermarrjeVit, idGjuha, idNdermarrje,
                      idKokaLp, merrVlereDefault, buttonClickNgaKomponente);

                return Request.KthePergjigje(result);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(parametrat, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage LlogaritKomponenteMuaji(JObject parametrat)
        {
            try
            {
                var arrvlera = parametrat["arrvlera"].ToObject<decimal[]>();
                var arrparam = parametrat["arrvleraParam"].ToObject<decimal[]>();
                var arrkodi = parametrat["arrKodi"].ToObject<string[]>();
                var arrmosndrysho = parametrat["arrMosNdrysho"].ToObject<bool[]>();
                var data = parametrat["data"].ToObject<DateTime>();
                var idpunonjes = parametrat["idpunonjesi"].ToObject<int>();
                var llogariDP = parametrat["llogaritDP"].Value<bool>();
                var muaji = parametrat["muaji"].Value<int>();
                var ditemuajindryshueshme = parametrat["ditemuajindryshueshme"].Value<bool>();
                var kodi = parametrat["kodi"].Value<string>();
                var vjenNgaMuajt = parametrat["vjenNgaMuajt"].Value<bool>();
                var ditemuaji = parametrat["ditemuaji"].Value<string>() == bool.TrueString;
                var pagemuaji = parametrat["pagemuaji"].Value<string>() == bool.TrueString;
                var muajitjeter = parametrat["muajitjeter"].Value<int>();
                var vititjeter = parametrat["vititjeter"].Value<int>();
                var merrimporte = parametrat["merrimporte"].Value<bool>();
                var kodimporti = parametrat["kodimporti"].Value<string>();
                var vjenNgaKomponentja = parametrat["vjenNgaKomponentja"].Value<bool>();
                var newrecord = parametrat["newrecord"].Value<bool>();
                var llogaritDiteLejeNgaImporti = parametrat["llogaritDiteLejeNgaImporti"].Value<bool>();
                var idNdermarrjeVit = parametrat["idNdermarrjeVit"].Value<int>();
                var idGjuha = parametrat["idGjuha"].Value<int>();
                var idNdermarrje = parametrat["idNdermarrje"].Value<int>();
                var idKokaLp = parametrat["idKokaLp"].Value<int?>().GetValueOrDefault();
                var merrVlereDefault = parametrat["merrVlereDefault"].Value<bool>();

                var result = ListPagesaRepository.LlogaritPagePerPunonjesPerMuajin(idpunonjes, data.ToLocalTime(), arrkodi, arrparam, arrmosndrysho, arrvlera, llogariDP, muaji,
                    ditemuajindryshueshme, kodi, vjenNgaMuajt, ditemuaji, pagemuaji, muajitjeter, vititjeter, merrimporte, kodimporti, vjenNgaKomponentja, newrecord,
                    llogaritDiteLejeNgaImporti, Session, idNdermarrjeVit, idGjuha, idNdermarrje,
                    idKokaLp, merrVlereDefault);
                return Request.KthePergjigje(result);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(parametrat, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage DiscardNdryshimetPerMuajt(JObject parametra)
        {
            try
            {
                var idPunonjesi = parametra["idPunonjesi"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.DiscardNdryshimetPerMuajt(Session, idPunonjesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(parametra, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage RuajNdryshimetPerMuajt(JObject parametra)
        {
            try
            {
                var idPunonjesi = parametra["idPunonjesi"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.RuajNdryshimetPerMuajt(Session, idPunonjesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(parametra, ex);
            }
        }
        /// <summary>
        ///     llogarit pagen per nje grup punonjesish
        /// </summary>
        /// <param name="parametrat"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage LlogaritPagePerShumePunonjes(JObject parametrat)
        {
            try
            {
                var data = (parametrat["data"].ToObject<DateTime>()).ToLocalTime();
                var nrDitesh = parametrat["nrDitesh"].Value<double>();
                var punonjesitIDs = parametrat["punonjesitIDs"].ToObject<List<int>>();
                var muaji = parametrat["muaji"].Value<int>();
                var ditemuajindryshueshme = parametrat["ditemuajindryshueshme"].Value<bool>();
                var llogaritDiteLejeNgaImporti = parametrat["llogaritDiteLejeNgaImporti"].Value<bool>();
                var muajitjeter = parametrat["muajitjeter"].Value<int>();
                var vititjeter = parametrat["vititjeter"].Value<int>();
                var idNdermarrje = parametrat["idNdermarrje"].Value<int>();
                var idGjuha = parametrat["idGjuha"].Value<int>();
                var llogariDP = false;
                var kodi = "PP";
                var vjenNgaMuajt = false;


                var merrimporte = true;
                var kodimporti = "";
                var vjenNgaKomponentja = false;
                var newrecord = true;


                return Request.KthePergjigje(ListPagesaRepository.LlogaritPagePerShumePunonjes(data, false, nrDitesh, punonjesitIDs, llogariDP, muaji,
                    ditemuajindryshueshme, kodi, vjenNgaMuajt, false, false, muajitjeter, vititjeter, merrimporte, kodimporti, vjenNgaKomponentja, newrecord,
                    llogaritDiteLejeNgaImporti, 0, true, idNdermarrje, idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(parametrat, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage MerrVlereDefault(JObject parameters)
        {
            try
            {
                //int muaj, int vit, int idpunonjes, int idNdermarrje, string kod, bool heraPare
                var muaj = parameters["muaj"].Value<int>();
                var vit = parameters["vit"].Value<int>();
                var idpunonjes = parameters["idpunonjes"].Value<int>();
                var idNdermarrje = parameters["idNdermarrje"].Value<int>();
                var kodKomponente = parameters["kodKomponente"].Value<string>();
                var heraPare = parameters["heraPare"].Value<bool>();

                var result = ListPagesaRepository.merrVleraDefault(muaj, vit, idpunonjes, kodKomponente, heraPare, Session, idNdermarrje);

                return Request.KthePergjigje(result);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(parameters, ex);
            }
        }

        [HttpPost]

        public HttpResponseMessage RimerrVlera(JObject parametrat)
        {
            try
            {
                var colFillestare = parametrat["colFillestare"].ToObject<List<colKompListPagese>>();
                var DPZero = parametrat["DPZero"].Value<bool>();
                var data = (parametrat["data"].ToObject<DateTime>()).ToLocalTime();
                var personalNrs = parametrat["personalNrs"].ToObject<string[]>();
                var muaji = parametrat["muaji"].Value<int>();
                var ditemuajindryshueshme = parametrat["ditemuajindryshueshme"].Value<bool>();
                var muajitjeter = parametrat["muajitjeter"].Value<int>();
                var vititjeter = parametrat["vititjeter"].Value<int>();
                var llogaritDiteLejeNgaImporti = parametrat["llogaritDiteLejeNgaImporti"].Value<bool>();
                var idNdermarrje = parametrat["idNdermarrje"].Value<int>();
                var idGjuha = parametrat["idGjuha"].Value<int>();
                var idKokaLp = parametrat["idKokaLp"].Value<int?>().GetValueOrDefault();
                var lpERe = parametrat["listPageseRe"].Value<bool?>().GetValueOrDefault();
                var pastroVleratPerMuajt = parametrat["pastroVleratPerMuajt"].Value<bool>();

                var punonjesit = new colPunonjes(personalNrs, idNdermarrje);
                var punonjesIds = Utils.MerrIdPunonjesishSipasNumravePersonal(personalNrs, punonjesit);

                Dictionary<int, colKompListPagese> teGrupuaraSipasPunonjesit = new Dictionary<int, colKompListPagese>(punonjesIds.Count);

                for (var i = 0; i < punonjesIds.Count; i++)
                    teGrupuaraSipasPunonjesit[punonjesIds[i]] = colFillestare[i];

                if (pastroVleratPerMuajt) PastroSessionNgaKomponenteMuaji();

                return Request.KthePergjigje(ListPagesaRepository.RimerrVlera(teGrupuaraSipasPunonjesit, DPZero, data, false, personalNrs, muaji, ditemuajindryshueshme, "PP", false, false, false, muajitjeter, vititjeter, true, "", false, false, llogaritDiteLejeNgaImporti, punonjesit, punonjesIds, idNdermarrje, idGjuha, idKokaLp, null, lpERe));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(parametrat, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage FshiKomponenteVleraTePunonjesit(JObject parametrat)
        {
            try
            {
                var nrPersonal = parametrat["nrPersonal"].Value<string>();
                var idNdermarrje = parametrat["idNdermarrje"].Value<int>();
                var punonjes = new clsPunonjes(nrPersonal, idNdermarrje);
                Utils.HiqKomponenteMuajiNgaSessioniPerPunonjes(punonjes.IdPunonjes, Session);

                return Request.KthePergjigje("OK");
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(parametrat, ex);
            }
        }


        [HttpPost]
        public HttpResponseMessage KaTeDhenaPerTeMarre(JObject parametrat)
        {
            try
            {
                var id = parametrat["id"].Value<int>();
                var result = ListPagesaRepository.KaTeDhenaPerTeMarre(id);

                return Request.KthePergjigje(result);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(parametrat, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage FshiPunesimeNgaSessioni()
        {
            try
            {
                return Request.KthePergjigje(mySessionObjects.ruajPunesimNeSesion(Session, null));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage fshiRaportNgaSessioni()
        {
            try
            {
                return Request.KthePergjigje(ListPagesaRepository.fshiRaportNgaSessioni(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage MerrPrindGrupimi(JObject param)
        {
            try
            {
                var idgrupilocal = param["idgrupilocal"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.MerrPrindGrupimi(idgrupilocal));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage MerrPrindQK(JObject param)
        {
            try
            {
                var idqk = param["idqk"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.MerrPrindQK(idqk));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage merrKategoriPage(JObject param)
        {
            try
            {
                var idllojpage = param["idllojpage"].Value<int>();
                var idndermarje = param["idndermarje"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.merrKategoriPage(idllojpage, idndermarje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage merrSigurimeSuplementare(JObject param)
        {
            try
            {
                var date = param["date"].Value<DateTime>();
                var idndermarje = param["idndermarje"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.merrSigurimeSuplementare(date, idndermarje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]

        public HttpResponseMessage llogaritVlera(JObject param)
        {
            try
            {
                var arrvlera = param["arrvlera"].ToObject<decimal[]>();
                var arrparam = param["arrvleraParam"].ToObject<decimal[]>();
                var tipi = param["tipi"].Value<string>();
                var data = param["data"].Value<DateTime>();
                var idsigurimi = param["idsigurimi"].Value<int>();
                var idmonedha = param["idmonedha"].Value<int>();
                var idPunonjes = param["idPunonjes"].Value<int?>().GetValueOrDefault(0);
                var idndermarje = param["idndermarje"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.llogaritVlera(arrvlera, arrparam, tipi, data.ToLocalTime(), idsigurimi, idmonedha, Session, idndermarje, idPunonjes));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage merrDataKomponenteListPagesaDheShtesaPaga(JObject param)
        {
            try
            {
                var idpunonjes = param["idpunonjes"].Value<int>();
                var klonim = param["klonim"].Value<bool>();
                return Request.KthePergjigje(ListPagesaRepository.merrDataKomponenteListPagesaDheShtesaPaga(idpunonjes, klonim));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage merrSkemaSigurimi(JObject param)
        {
            try
            {
                var datendryshimi = param["datendryshimi"].Value<string>();
                var date = param["date"].Value<DateTime>();
                var idpunonjes = param["idpunonjes"].Value<int>();
                var idndermarje = param["idndermarje"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.merrSkemaSigurimi(date, datendryshimi, idpunonjes, idndermarje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage kontrolloUsername(JObject param)
        {
            try
            {
                var username = param["username"].Value<string>();
                var nr = param["nr"].Value<int>();
                var idndermarje = param["idndermarje"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.kontrolloUsername(username, nr, idndermarje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage MerrIdPunonjesi(JObject param)
        {
            try
            {
                var nrPersonal = param["nrPersonal"].ToString();
                var idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.MerrIdPunonjesi(nrPersonal, idNdermarrje));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }

        [HttpDelete]
        public HttpResponseMessage PastroSessionNgaKomponenteMuaji()
        {
            try
            {
                return Request.KthePergjigje(ListPagesaRepository.PastroSession(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null, ex);
            }
        }


        [HttpPost]
        public HttpResponseMessage ktheACListeKonfigurimeshUrdherPagese(JObject param)
        {
            try
            {
                var infixText = param["infixText"].Value<string>();
                var lloj = param["lloj"].Value<int>();
                var idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.ktheACListeKonfigurimeshUrdherPagese(infixText, lloj, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage ktheACListeLlogarishBeginWith(JObject param)
        {
            try
            {
                var infixText = param["infixText"].Value<string>();
                var idPerdoruesi = param["idPerdoruesi"].Value<int>();
                var idNderrmarje = param["idNderrmarje"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.ktheACListeLlogarishBeginWith(infixText, idPerdoruesi, idNderrmarje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage ktheVleraKonfigMeID(JObject param)
        {
            try
            {
                var idja = param["idja"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.ktheVleraKonfigMeID(idja));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage ktheVleraKonfigMeKod(JObject param)
        {
            try
            {
                var kodi = param["kodi"].Value<string>();
                var lloji = param["lloji"].Value<int>();
                var idNderrmarje = param["idNderrmarje"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.ktheVleraKonfigMeKod(kodi, lloji, idNderrmarje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage KtheVleraLlogMeID(JObject param)
        {
            try
            {
                var idja = param["idja"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.KtheVleraLlogMeID(idja));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage ktheVleraLlogMeKod(JObject param)
        {
            try
            {
                var kodi = param["kodi"].Value<string>();
                var idNderrmarje = param["idNderrmarje"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.ktheVleraLlogMeKod(kodi, idNderrmarje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage merrBankaPunonjes(JObject param)
        {
            try
            {
                var idpunonjes = param["idpunonjes"].Value<int>();
                var data = param["data"].Value<DateTime>();
                return Request.KthePergjigje(ListPagesaRepository.merrBankaPunonjes(idpunonjes, data));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage AplikoNdryshiminPerPunonjesit(JObject param)
        {
            try
            {
                var idpunonjesish = param["idPunonjesish"].ToObject<List<int>>();
                var data = param["dtAktivizimi"].Value<DateTime>();
                var lloji = (LlojKomponentePage)param["lloji"].Value<int>();
                var idNdermarrje = param["idNdermarrje"].Value<int>();
                var idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.AplikoNdryshiminEKomponentvePerPunonjesitSipasDates(idpunonjesish, data, lloji, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }


        [HttpPost]
        public HttpResponseMessage MerrTrupDokumentiListpagese(JObject param)
        {
            try
            {
                var idKoka = param["idkoka"].Value<int>();
                var idGjuha = param["idGjuha"].Value<int>();
                var muaji = param["muaji"].Value<int>();
                var rimerrVlera = param["rimerrVlera"].Value<bool>();
                var llogaritDiteLejeNgaImporti = param["llogaritDiteLejeNgaImporti"].Value<bool>();
                var hiqPunonjesTelarguarKlonim = param["hiqPunonjesTelarguar"].Value<bool>();
                var cmbData = (param["cmbData"].ToObject<DateTime>()).ToLocalTime();
                return Request.KthePergjigje(ListPagesaRepository.MerrTrupDokumentiListpagese(idKoka, idGjuha, muaji, rimerrVlera, Session, llogaritDiteLejeNgaImporti, hiqPunonjesTelarguarKlonim, cmbData));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage RuajRegjistrimListPagese(JObject param)
        {
            try
            {
                var veprimi = param["veprimi"].Value<string>();
                var shtimModifikim = param["shtimModifikim"].Value<string>();
                var dtDok = param["DtDok"].Value<string>();
                var dtRegjistrimi = param["DtRegjistrimi"].Value<string>();
                var kontabilizim = param["kontabilizim"].Value<string>();
                var idNgaQueryString = param["IdNgaQueryString"].Value<string>();
                var idEtapa = param["IdEtapa"].Value<int>();
                var ServerUrl = param["ServerUrl"].Value<string>();
                var Vlefta = param["Vlefta"].Value<string>();
                var Vlefta2 = param["Vlefta2"].Value<string>();
                var IdKonfigurimi = param["IdKonfigurimi"].Value<string>();
                var KodKonfigurimi = param["KodKonfigurimi"].Value<string>();
                var NrDok = param["NrDok"].Value<string>();
                var Muaji = param["Muaji"].Value<int>();
                var IdMonedha = param["IdMonedha"].Value<int>();
                var KodMonedha = param["KodMonedha"].Value<string>();
                var Kursi = param["Kursi"].Value<string>();
                var Shenime = param["Shenime"].Value<string>();
                var GridDataObject = param["gridDataObject"].ToString();
                var HfKomp = param["hfKomp"].ToString();
                var IdSkema = param["IdSkema"].Value<int>();
                var Lidhur = param["Lidhur"].Value<string>();
                var lblStatusAprovimi = param["lblStatusAprovimi"].Value<string>();
                var nrAutoNrDok = param["nrAutoNrDok"].Value<string>();
                var hfStatus1 = param["hfStatus1"].Value<string>();
                var pergjigja = param["pergjigjaValue"].Value<string>();
                var idGjuha = param["idGjuha"].Value<int>();
                var idPerdoruesi = param["idPerdoruesi"].Value<int>();
                var idNdermarrjeVit = param["idNdermarrjeVit"].Value<int>();
                var idViti = param["idViti"].Value<int>();
                var idNdermarrje = param["idNdermarrje"].Value<int>();
                var idDepartamenti = param["idDepartamenti"].Value<int>();
                var idNenDepartamenti = param["idNenDepartamenti"].Value<int>();
                var Departamenti = param["Departamenti"].Value<string>();
                var NenDepartamenti = param["Nendepartamenti"].Value<string>();
                var hfArkiva = param["hfArkiva"].Value<string>();
                ImbLogger.LogInfoWebApi($"Filloi Ruajtja e dokumentit te LP me NRDOK {NrDok}!");
                var result = ListPagesaRepository.RuajRegjistrimListPagese(Session, veprimi, shtimModifikim, dtDok, dtRegjistrimi, kontabilizim, idNgaQueryString, idEtapa, ServerUrl, IdKonfigurimi, KodKonfigurimi, Vlefta, Vlefta2, NrDok, Muaji, IdMonedha, KodMonedha, Kursi,
                    Shenime, GridDataObject, HfKomp, IdSkema, Lidhur, lblStatusAprovimi, nrAutoNrDok, hfStatus1, pergjigja, idGjuha, idPerdoruesi, idNdermarrjeVit, idViti, idNdermarrje, idDepartamenti, idNenDepartamenti, Departamenti, NenDepartamenti, hfArkiva);
                ImbLogger.LogInfoWebApi($"Perfundoi Ruajtja e dokumentit te LP me NRDOK {NrDok}!");
                return Request.KthePergjigje(result);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage FshiDokument(JObject param)
        {
            try
            {
                int[] ids = param["ids"].ToObject<int[]>();
                string guidString = param["guidString"].ToObject<string>();
                return Request.KthePergjigje(ListPagesaRepository.FshiDokument(Session, ids, guidString));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage merrTeDhenaPerPunonjesDheListePagesatEShtesaPaga(JObject param)
        {
            try
            {
                int idkomp = param["idkomp"].Value<int>();
                string kodkonfi = param["kodkonfi"].Value<string>();
                int idpunonjes = param["idpunonjes"].Value<int>();
                int idndermarje = param["idNdermarrje"].Value<int>();
                int gjuhe = param["idGjuha"].Value<int>();
                var klonim = param["klonim"].Value<bool>();
                return Request.KthePergjigje(ListPagesaRepository.merrTeDhenaPerPunonjesDheListePagesatEShtesaPaga(idkomp, kodkonfi, idpunonjes, idndermarje, gjuhe, klonim));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage ktheACListePunonjesish(JObject param)
        {
            try
            {
                string infixText = param["infixText"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.ktheACListePunonjesish(infixText, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage kthePunonjesMeKodRow(JObject param)
        {
            try
            {
                var kodPunonjes = param["kodPunonjes"].ToString();
                var idNdermarrje = param["idNdermarrje"].Value<int>();
                var rreshti = param["rreshti"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.kthePunonjesMeKodRow(kodPunonjes, idNdermarrje, rreshti));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }

        [HttpPost]
        public HttpResponseMessage kthePunonjesMeIdRow(JObject param)
        {
            try
            {
                var idPunonjes = param["idPunonjes"].Value<int>();
                var rreshti = param["rreshti"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.kthePunonjesMeIdRow(idPunonjes, rreshti));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }


        [HttpPost]
        public HttpResponseMessage kthePunonjesitMeIdRow(JObject param)
        {
            try
            {
                string idPunonjesish = param["idPunonjesish"].Value<string>();
                var rreshti = param["rreshti"].Value<int>();
                return Request.KthePergjigje(ListPagesaRepository.KthePunonjesitMeIdRow(idPunonjesish, rreshti));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpPost]
        public HttpResponseMessage gjeneroKodinTCR(JObject param)
        {
            try
            {
                var kodi = param["kodi"].Value<string>();
                var idNderrmarje = param["idNderrmarje"].Value<int>();
                var kodBiznesi = param["kodBiznesi"].Value<string>();
                clsNdermarrje nderm = new clsNdermarrje(idNderrmarje);
                if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                {
                    if (nderm.Fiskalizimi)
                    {
                        var xml = clsFunksioneFiskalizimi.gjeneroKodinTCR(new clsNdermarrje(idNderrmarje), kodi, kodBiznesi);
                        if (xml == "Problem certifikate")
                            return Request.KthePergjigje(xml);
                        else 
                            return Request.KthePergjigje(clsFunksioneFiskalizimi.InvokeService(xml, "TCRCode", false));
                    }
                    else
                    {
                        return Request.KthePergjigje("");
                    }
                }
                else
                {
                    return Request.KthePergjigje("");
                }

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);


            }
        }
        [HttpPost]
        public HttpResponseMessage kontrolloVleratPerGjeneriminETcr(JObject param)
        {
            try
            {
                var kodi = param["kodi"].Value<string>();
                var idNderrmarje = param["idNderrmarje"].Value<int>();
                var kodBiznesi = param["kodBiznesi"].Value<string>();
                clsNdermarrje nderm = new clsNdermarrje(idNderrmarje);

                return Request.KthePergjigje(clsFunksioneFiskalizimi.kontrolloVleratPerTCR(new clsNdermarrje(idNderrmarje), kodBiznesi));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);


            }
        }
    }
}