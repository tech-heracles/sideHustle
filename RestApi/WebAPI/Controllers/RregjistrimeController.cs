using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DbCore.DbRegjistrim;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System.Web.SessionState;
using System.Net.Http.Formatting;
using System.IO;
using System.Globalization;
using DbCore;
using System.Collections.Generic;
using DbCore.DbInventari;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.DbAdmin;

namespace RestApi.WebAPI.Controllers
{
    [Authorize]
    public class RregjistrimeController : ApiController, IRequiresSessionState
    {
        private System.Web.SessionState.HttpSessionState Session { get { return HttpContext.Current.Session; } }


        [HttpPost, HttpGet]
        public HttpResponseMessage kontrollobundle(JObject param)
        {
            try
            {
                int[] idartikujsh = param["idartikujsh"].ToObject<int[]>();

                return Request.KthePergjigje(RregjistrimeRepository.kontrollobundle(idartikujsh));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        public HttpResponseMessage ktheRowVleraIdArtTvshEPlote(JObject param)
        {
            try
            {
                JToken listeImeiArt;
                string[][] listeImeiArtikull = new String[0][];
                int idArt = param["idArt"].Value<int>();
                string kodArt = param["kodKodbarArt"].Value<string>();
                int rreshti = param["idRreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>().ToLocalTime();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                KonfigurimTVSHGjateRregj llojTvsh = (KonfigurimTVSHGjateRregj)param["llojTvsh"].Value<int>();
                bool meDetajim = param["meDetajim"].Value<bool>();
                string kodMag = param["magazine"].Value<string>();
                bool merrPershkrimMagazine = param["merrPershkrimMagazine"].Value<bool>();
                bool merrZbritjeAnalitike = param["merrZbritjeAnalitike"].Value<bool>();
                int ZbritjaKlientit = (param["ZbritjaKlientit"].ToString() == string.Empty || param["ZbritjaKlientit"] == null) ? 0 : param["ZbritjaKlientit"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                bool merrCmim = param["merrCmim"].Value<bool>();
                int NivelCmimi = param["NivelCmimi"].Value<int>();
                string monedha = param["monedha"].Value<string>();
                int njesiDef = param["njesiDef"].Value<int>();
                double kursi = param["kursi"].Value<double>();
                double sasia = (param["sasia"] == null || param["sasia"].ToString() == string.Empty) ? 1 : param["sasia"].Value<double>();
                int shitjeblerje = param["shitjeblerje"].Value<int>();
                bool gjendjeMinMax = param["gjendjeMinMax"].Value<bool>();
                bool kontrolloImeiFifo = param["kontrolloImeiFifo"].Value<bool>();
                if (param.TryGetValue("listeIMEIArtikull", out listeImeiArt))
                    listeImeiArtikull = listeImeiArt.ToObject<string[][]>();
                bool promocione = param["promocione"].Value<bool>();
                clsTaksa taksaKF = param["TaksaKF"].ToObject<clsTaksa>();
                object sasiaNeGride = param["sasiaNeGride"] == null ? null : param["sasiaNeGride"].Value<object>();
                object magazinatKoka = param["magazinatKoka"] == null ? null : param["magazinatKoka"].Value<object>();
                bool merrMagMeAutorizim = param["merrMagMeAutorizim"] == null ? true : param["merrMagMeAutorizim"].Value<bool>();
                string detajim = param["detajim"].Value<string>();
                bool merrSipasDetajimit = param["merrSipasDetajimit"] == null ? false : param["merrSipasDetajimit"].Value<bool>();
                int counterWsKodi = param["counterWsKodi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraIdArtTvshEPlote(idArt, kodArt, rreshti, data, kodMag, idPerdoruesi, llojTvsh, meDetajim, merrPershkrimMagazine, merrZbritjeAnalitike, ZbritjaKlientit, idNdermarrje, merrCmim, NivelCmimi, monedha, njesiDef, kursi, sasia, shitjeblerje, gjendjeMinMax, kontrolloImeiFifo, listeImeiArtikull, promocione, taksaKF, sasiaNeGride, magazinatKoka, merrMagMeAutorizim, detajim, merrSipasDetajimit, counterWsKodi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        public HttpResponseMessage ktheTvshSipasArtikullit(JObject param)
        {
            try
            {
                int[] idartikulli = param["idartikulli"].ToObject<int[]>();
                string[] lloji = param["lloji"].ToObject<string[]>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheTvshSipasArtikullit(idartikulli, lloji));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage ktheKonfigurimeWebhook(JObject param)
        {
            try
            {
                int idnderrmarje = param["idndermarje"].Value<int>();
                
                return Request.KthePergjigje(RregjistrimeRepository.ktheKonfigurimWebhhok(idnderrmarje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        //[HttpPost, HttpGet]
        //public HttpResponseMessage kontrolloNrAutoDrejtFundit(JObject param)
        //{
        //    try
        //    {
        //        //long countNrFundit = 0;
        //        int idNrAuto = param["idNrAuto"].Value<int>();
        //        string kodKontrolli = param["kodKontrolli"].Value<string>();
        //        DateTime data = param["data"].Value<DateTime>();
        //        //bool eshteNrDrejtFundit = DbCore.DbAdmin.clsNrAutom.kontrolloEshteNrAutoDrejtFundit(idNrAuto, data, ref countNrFundit);
        //        return Request.KthePergjigje(RregjistrimeRepository.kontrolloNrAutoDrejtFundit(idNrAuto, data, kodKontrolli));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.KthePergjigjeGabim(param, ex);
        //    }
        //}


        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraLlogIDTvsh(JObject param)
        {
            try
            {
                int idja, rreshti, idPerdoruesi, nrLlogarive = param.Count;
                object[] all = new object[nrLlogarive];
                for (int i = 0; i < nrLlogarive; i++)
                {
                    JObject p = (JObject)param["" + i.ToString() + ""].Value<object>();
                    idja = p["idja"].Value<int>();
                    rreshti = p["index"].Value<int>();
                    KonfigurimTVSHGjateRregj llojTvsh = (KonfigurimTVSHGjateRregj)p["llojTvsh"].Value<int>();
                    idPerdoruesi = p["idPerdoruesi"].Value<int>();
                    clsTaksa taksaKF = p["TaksaKF"].ToObject<clsTaksa>();
                    all[i] = RregjistrimeRepository.KtheVleraLlogIDTvsh(idja, rreshti, llojTvsh, idPerdoruesi, taksaKF);

                }

                return Request.KthePergjigje(all);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        //[HttpPost, HttpGet]
        //public HttpResponseMessage ktheRowVleraKodArtTvsh(JObject param)
        //{
        //    try
        //    {
        //        string kodKodBarArt = param["kodKodbarArt"].Value<string>();
        //        int rreshti = param["index"].Value<int>();
        //        DateTime data = param["date"].Value<DateTime>();
        //        int idDetajim = param["detajim"].Value<int>();
        //        int idPerdoruesi = param["idPerdoruesi"].Value<int>();
        //        KonfigurimTVSHGjateRregj llojTvsh = (KonfigurimTVSHGjateRregj)param["tvshkont"].Value<int>();
        //        int idNdermarrje = param["idNdermarrje"].Value<int>();

        //        return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraKodArtTvsh(kodKodBarArt, rreshti, data, idDetajim, idPerdoruesi, llojTvsh, idNdermarrje));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.KthePergjigjeGabim(param,ex);
        //    }
        //}
        //[HttpPost, HttpGet]
        //public HttpResponseMessage ktheRowVleraKodArtDetajimTvsh(JObject param)
        //{
        //    try
        //    {
        //        string kodKodBarArt = param["kodKodbarArt"].Value<string>();
        //        int rreshti = param["index"].Value<int>();
        //        DateTime data = param["date"].Value<DateTime>();
        //        int idmag = param["magazine"].Value<int>();
        //        int idPerdoruesi = param["idPerdoruesi"].Value<int>();
        //        KonfigurimTVSHGjateRregj llojTvsh = (KonfigurimTVSHGjateRregj)param["tvshkont"].Value<int>();
        //        int idNdermarrje = param["idNdermarrje"].Value<int>();


        //        return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraKodArtDetajimTvsh(kodKodBarArt, rreshti, data, idmag, idPerdoruesi, llojTvsh, idNdermarrje));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.KthePergjigjeGabim(param,ex);
        //    }
        //}
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraLlogKodTvsh(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int rreshti = param["index"].Value<int>();

                KonfigurimTVSHGjateRregj llojTvsh = (KonfigurimTVSHGjateRregj)param["llojTvsh"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                clsTaksa taksaKF = param["TaksaKF"].ToObject<clsTaksa>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheVleraLlogKodTvsh(kodi, rreshti, llojTvsh, idPerdoruesi, idNdermarrje, taksaKF));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraLlogShpenzimiMeKod(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int rreshti = param["index"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.KtheVleraLlogShpenzimiMeKod(kodi, rreshti, idPerdoruesi, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraKatShpenzimiMeId(JObject param)
        {
            try
            {
                int id = param["id"].Value<int?>().GetValueOrDefault();
                int rreshti = param["index"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.KtheVleraKatShpenzimiMeId(id, rreshti, idPerdoruesi, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraKatShpenzimiMeKod(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int rreshti = param["index"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.KtheVleraKatShpenzimiMeKod(kodi, rreshti, idPerdoruesi, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraMagazinaMeKod(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                string idja = param["idja"].Value<string>();
                DateTime datedok = param["datedok"].Value<DateTime>();

                return Request.KthePergjigje(RregjistrimeRepository.KtheVleraMagazinaMeKod(kodi, idNdermarrje, idja, datedok));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraMagazinaMeID(JObject param)
        {
            try
            {
                int id = param["id"].Value<int?>().GetValueOrDefault();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                string idja = param["idja"].Value<string>();
                DateTime datedok = param["datedok"].Value<DateTime>();

                return Request.KthePergjigje(RregjistrimeRepository.KtheVleraMagazinaMeID(id, idja, datedok));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }




        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraLlogShpenzimiID(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["index"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.KtheVleraLlogShpenzimiID(idja, rreshti, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraDetajimMeKod(JObject param)
        {
            try
            {

                string kodi = param["kodi"].Value<string>();
                int rreshti = param["index"].Value<int>();
                int lloji = param["lloji"].Value<int>();
                string kodartikulli = param["kodartikulli"].Value<string>();
                int idkokamagazina = param["idkokamag"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                string magazina = param["magazine"].Value<string>();
                string dtDok = param["date"].Value<string>();

                bool kontrolloImeiFifo = param["kontrolloImeiFifo"].Value<bool>();
                string[] listeIMEI = param["listeImei"].ToObject<string[]>();
                bool promocione = param["promocione"].Value<bool>();
                bool DokumentTransferimiOwn = param["DokumentTransferimiOwn"].Value<bool>();
                bool merrPerberesit = param["merrPerberesit"].Value<bool>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraDetajimMeKod(kodi, rreshti, lloji, kodartikulli, idkokamagazina, idNdermarrje, idPerdorues, magazina, dtDok, kontrolloImeiFifo, listeIMEI, promocione, DokumentTransferimiOwn, merrPerberesit));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraDetajimMeID(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["index"].Value<int>();
                int lloji = param["lloji"].Value<int>();
                string kodartikulli = param["kodartikulli"].Value<string>();
                int idkokamagazina = param["idkokamagazina"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                string magazina = param["magazine"].Value<string>();
                string dtDok = param["date"].Value<string>();
                bool kontrolloImeiFifo = param["kontrolloImeiFifo"].Value<bool>();
                string[] listeIMEI = param["listeImei"].ToObject<string[]>();
                bool promocione = param["promocione"].Value<bool>();
                bool DokumentTransferimiOwn = param["DokumentTransferimiOwn"].Value<bool>();
                bool merrPerberesit = param["merrPerberesit"].Value<bool>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraDetajimMeID(idja, rreshti, lloji, kodartikulli, idkokamagazina, idNdermarrje, idPerdorues, magazina, dtDok, kontrolloImeiFifo, listeIMEI, promocione, DokumentTransferimiOwn, merrPerberesit));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheGjendjeArtikulli(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                string mag = param["mag"].Value<string>();
                DateTime data = param["data"].Value<DateTime>().ToLocalTime();
                string koddetajim = param["koddetajim"].Value<string>();
                string koddetajim2 = param["koddetajim2"].Value<string>();
                int iddok = param["iddok"].Value<int>();
                int idndermarje = param["idndermarje"].Value<int>();
                int idKonfigAmbjente = param["idKonfigAmbjente"].Value<int>();
                int idreshti = param["idreshti"].Value<int>();
                double totalartikulli = param["totalartikulli"].Value<double>();
                double totaldetajim1 = param["totaldetajim1"].Value<double>();
                double totaldetajim2 = param["totaldetajim2"].Value<double>();
                bool shitje_blerje = param["shitje_blerje"].Value<bool>();
                bool ekzekutimProdhim = param["ekzekutimProdhim"].Value<bool>();
                
                return Request.KthePergjigje(RregjistrimeRepository.ktheGjendjeArtikulli(idja, mag, data, koddetajim, koddetajim2, iddok, idndermarje, idKonfigAmbjente, idreshti, totalartikulli, totaldetajim1, totaldetajim2, shitje_blerje, ekzekutimProdhim));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArtikullPerberes(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();



                return Request.KthePergjigje(RregjistrimeRepository.ktheArtikullPerberes(id));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kthePershkrimMagSipasId(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();



                return Request.KthePergjigje(RregjistrimeRepository.kthePershkrimMagSipasId(id));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheInfoKF(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int idInfo = param["idInfo"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.ktheInfoKF(idja, data, idInfo));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheInfoKFSipasKodit(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int idNdermarje = param["idNdermarje"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int idInfo = param["idInfo"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheInfoKFSipasKodit(kodi, idNdermarje, data, idInfo));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage mbushInfoArtikulliMeDetajime(JObject param)
        {
            try
            {
                int idArt = param["idkodi"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                string detajim = param["detajim"].Value<string>();
                int rreshti = param["index"].Value<int>();
                int idInfo = param["idInfo"].Value<int>();
                string detajim2 = param["detajim2"].Value<string>();
                int idViti = param["idViti"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idklient = param["idklient"].Value<int>();
                string mag = param["mag"].Value<string>();
                string njesiart = param["njesiart"].Value<string>();
                int idKarta = param["idKarta"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.mbushInfoArtikulliMeDetajime(idArt, data, detajim, rreshti, idInfo, detajim2, idViti, idPerdoruesi, idklient, mag, njesiart, idKarta));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage mbushMagazinenSipasID(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();

                return Request.KthePergjigje(new clsNjesiAdministrative(kodi, idNdermarrje).IdLlojMagazine);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage mbushInfoLlogarie(JObject param)
        {
            try
            {
                int idja = param["idKodi"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int rreshti = param["index"].Value<int>();
                int idInfo = param["idInfoLlog"].Value<int>();
                int idNderVit = param["idNderVit"].Value<int>();

                DateTime dt = new DateTime(1970, 1, 1);
                return Request.KthePergjigje(RregjistrimeRepository.mbushInfoLlogarie(idja, data, rreshti, idInfo, idNderVit, dt));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage CelSerialNqsNukEkziston(JObject param)
        {
            try
            {
                string kodserial = param["kodserial"].Value<string>();
                int idndermarje = param["idndermarje"].Value<int>();
                int idperdoruesi = param["idperdoruesi"].Value<int>();
                int idartikulli = param["idartikulli"].Value<int>();
                int lastsel = param["lastsel"].Value<int>();
                string serialeekzistuese = param["serialeekzistuese"].Value<string>();
                string[] serialeteperdoruraNeKeteFature = param["serialeteperdoruraNeKeteFature"].ToObject<string[]>();
                bool celnqsnukekziston = param["celnqsnukekziston"].Value<bool>();
                string magazina = param["magazina"].Value<string>();
                double sasishuma = param["sasishuma"].Value<double>();
                int iddokmodmagazine = param["iddokmodmagazine"].Value<int>();


                return Request.KthePergjigje(RregjistrimeRepository.CelSerialNqsNukEkziston(kodserial, idndermarje, idperdoruesi, idartikulli, lastsel, serialeekzistuese, serialeteperdoruraNeKeteFature, celnqsnukekziston, magazina, sasishuma, iddokmodmagazine));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheACListeLlogarish(JObject param)
        {
            try
            {
                string infixText = param["infixText"].Value<string>();
                int pershk = param["pershk"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheACListeLlogarish(infixText, pershk, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheNjesiArtikulliComboMeKodRow(JObject param)
        {
            try
            {
                string kodArtikulli = param["kodi"].Value<string>();
                int idreshti = param["index"].Value<int>();
                string textnjesizgjedhur = param["textNjesiZgjedhur"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheNjesiArtikulliComboMeKodRow(kodArtikulli, idreshti, textnjesizgjedhur, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage KtheListKategoriShpenzimesh(JObject param)
        {
            try
            {
                string infixText = param["prefixText"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheListKategoriShpenzimesh(infixText, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage KtheListMagazinat(JObject param)
        {
            try
            {
                string infixText = param["prefixText"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                bool meAutorizim = param["meAutorizim"].Value<bool>();
                int llojArt = param["llojArt"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheListMagazinat(infixText, idNdermarrje, idPerdoruesi, meAutorizim, llojArt));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage ktheACListeArtikujshKodPershkKodbarEShpejt(JObject param)
        {
            try
            {
                string infixText = param["infixText"].Value<string>();
                int pershk = param["pershk"].Value<int>();
                string grup = param["grup"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                bool artikujTeShitshem = param["artikujTeShitshem"].Value<bool>();
                bool merrVetemAfatgjate = param["merrVetemAfatgjate"].Value<bool>();
                bool merrSipasDetajimit = param["merrSipasDetajimit"].Value<bool>();
                string klasa = param["klasa"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheACListeArtikujshKodPershkKodbarEShpejt(infixText, pershk, grup, idNdermarrje, idPerdoruesi, artikujTeShitshem, merrVetemAfatgjate, merrSipasDetajimit, klasa));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheACListeArtikujshKodPershkKodbarFull(JObject param)
        {
            try
            {
                string infixText = param["infixText"].Value<string>();
                int pershk = param["pershk"].Value<int>();
                string grup = param["grup"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                bool artikujTeShitshem = param["artikujTeShitshem"].Value<bool>();
                bool merrVetemAfatgjate = param["merrVetemAfatgjate"].Value<bool>();
                bool merrSipasDetajimit = param["merrSipasDetajimit"].Value<bool>();
                string klasa = param["klasa"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheACListeArtikujshKodPershkKodbarFull(infixText, pershk, grup, idNdermarrje, idPerdoruesi, artikujTeShitshem, merrVetemAfatgjate, merrSipasDetajimit, klasa));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheACListeArtikujshKodPershkKodbarEShpejtSet(JObject param)
        {
            try
            {
                string infixText = param["infixText"].Value<string>();
                int pershk = param["pershk"].Value<int>();
                string grup = param["grup"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                bool artikujTeShitshem = param["artikujTeShitshem"].Value<bool>();



                return Request.KthePergjigje(RregjistrimeRepository.ktheACListeArtikujshKodPershkKodbarEShpejtSet(infixText, pershk, grup, idNdermarrje, idPerdoruesi, artikujTeShitshem));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kthePershkrimMakro(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                int idNderViti = param["idNderViti"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.kthePershkrimMakro(prefixText, idNderViti, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage kthePershkrimMagSipasKodit(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.kthePershkrimMagSipasKodit(kodi, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheListeDetajimeshArtikulliNew(JObject param)
        {
            try
            {
                string infixText = param["infixText"].Value<string>();
                string art = param["art"].Value<string>();
                int lloji = param["lloji"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                bool merrPerberesit = param["merrPerberesit"].Value<bool>();
                DateTime date = param["date"].Value<DateTime>().ToLocalTime();
                bool sipasGjendjes = param["sipasGjendjes"].Value<bool>();
                string detajimi1 = param["detajimi1"].Value<string>();
                string magazina = param["mag"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheListeDetajimeshArtikulliNew(infixText, art, lloji, idNdermarrje, idPerdoruesi, merrPerberesit, date, sipasGjendjes, detajimi1, magazina));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KontrolloFaturaPaprintuara(JObject param)
        {
            try
            {

                int idndermarje = param["idndermarje"].Value<int>();
                int idperdorues = param["idperdorues"].Value<int>();
                int iddege = param["iddege"].Value<int>();
                int idnivel = param["idnivel"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.KontrolloFaturaPaprintuara(idndermarje, idperdorues, iddege, idnivel));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KontrolloFaturaBankaPaprintuara(JObject param)
        {
            try
            {

                int idndermarje = param["idndermarje"].Value<int>();
                int idperdorues = param["idperdorues"].Value<int>();
                int iddege = param["iddege"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.KontrolloFaturaBankaPaprintuara(idndermarje, idperdorues, iddege));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KonvertoAuto(JObject param)
        {
            try
            {
                int[] ids = param["ids"].ToObject<int[]>();
                string pageId = param["pageId"].ToObject<string>();
                return Request.KthePergjigje(RregjistrimeRepository.KonvertoAuto(ids, pageId, Session));
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
                int[] ids = param["ids"].ToObject<int[]>();
                string kodkonfig = param["kodkonfig"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idGjuha = param["idGjuha"].Value<int>();
                string pageId = param["pageId"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.KontrolloKonvertuar(ids, kodkonfig, idNdermarrje, idPerdoruesi, idGjuha, pageId));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage LidhArketime(JObject param)
        {
            try
            {
                int[] ids = param["ids"].ToObject<int[]>();
                int idNdermarrje = param["idNdermarrje"].ToObject<int>();
                int idPerdoruesi = param["idPerdoruesi"].ToObject<int>();
                int idGjuha = param["idGjuha"].ToObject<int>();
                return Request.KthePergjigje(RregjistrimeRepository.LidhArketime(ids, Session, idNdermarrje, idPerdoruesi, idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage RuajIdInventarizimi(JObject param)
        {
            try
            {
                int[] ids = param["ids"].ToObject<int[]>();
                string pageID = param["pageId"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.RuajIdInventarizimi(ids, pageID));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheGrupimDokumentashNderm(JObject param)
        {
            try
            {

                string kodkonfig = param["kodkonfig"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheGrupimDokumentashNderm(kodkonfig, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }
        [HttpPost, HttpGet]
        public HttpResponseMessage getInfoArtStructure(JObject param)
        {
            try
            {

                int idkoka = param["idkoka"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.getInfoArtStructure(idkoka, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }
        [HttpPost, HttpGet]
        public HttpResponseMessage MerrMenuPerPerdoruesSipasSkemes(JObject param)
        {
            try
            {

                int idskema = param["idskema"].Value<int>();
                int idperdoruesi = param["idperdoruesi"].Value<int>();
                bool isNotModifikim = param["isNotModifikim"].Value<bool>();
                string status = param["status"].Value<string>();
                int idkokashitje = param["idkokashitje"].Value<int>();
                string kodkonf = param["kodkonf"].Value<string>();
                int idlloji = param["idlloji"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.MerrMenuPerPerdoruesSipasSkemes(idskema, idperdoruesi, isNotModifikim, status, idkokashitje, kodkonf, idlloji));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheDetyrimi(JObject param)
        {
            try
            {

                int idKf = param["idKlientFurnitori"].Value<int>();
                int idKokaShitje = param["idKokaShitje"].Value<int>();
                DateTime date = param["date"].Value<DateTime>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheDetyrimi(idKf, idKokaShitje, date));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }
        [HttpPost, HttpGet]
        [Obsolete("duhet marre ne pageload jo nga javascript-i, sepse nuk ndryshon nga konfigurimi. shiko shitjen pershembull", false)]
        public HttpResponseMessage ktheIdMonedheNdermarrje(JObject param)
        {
            try
            {

                int idNdermarrje = param["idNdermarrje"].Value<int>();



                return Request.KthePergjigje(RregjistrimeRepository.ktheIdMonedheNdermarrje(idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheTargeAutomjeti(JObject param)
        {
            try
            {

                int idAutomjeti = param["idAutomjeti"].Value<int>();



                return Request.KthePergjigje(RregjistrimeRepository.ktheTargeAutomjeti(idAutomjeti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }
        public HttpResponseMessage ktheIDAutomjeti(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                string Auto = param["Automjeti"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheidAutomjetSipasShasise(Auto, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }
        public HttpResponseMessage ktheTargeTransportues(JObject param)
        {
            try
            {

                int idtransportues = param["idTransportues"].Value<int>();



                return Request.KthePergjigje(RregjistrimeRepository.ktheTargeTransportuesi(idtransportues));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKlientAutomjeti(JObject param)
        {
            try
            {

                int idAutomjeti = param["idAutomjeti"].Value<int>();



                return Request.KthePergjigje(RregjistrimeRepository.ktheKlientAutomjeti(idAutomjeti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrEmertimKlienti(JObject param)
        {
            try
            {

                int idKlient = param["idKlient"].Value<int>();



                return Request.KthePergjigje(RregjistrimeRepository.merrEmertimKlienti(idKlient));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheOKlientFurnitor(JObject param)
        {
            try
            {
                string idKlientFurnitor = param["idKlientFurnitor"].Value<string>();
                DateTime date = param["date"].Value<DateTime>();
                //bool mosPlotesoTeDhena = param["mosPlotesoTeDhena"].Value<bool>();
                int idKonfigurimi = param["idKonfigurimi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                int idKomponente = param["idKomponente"].Value<int>();
                int idGjuha = param["idGjuha"].Value<int>();
                string llojKursi = param["llojKursi"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheOKlientFurnitor(idKlientFurnitor, date, idKonfigurimi, idNdermarrje, idPerdorues, idKomponente, idGjuha, llojKursi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKlientFurnitorSipasId(JObject param)
        {
            try
            {
                int idKlientFurnitor = param["idKlientFurnitor"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKlientFurnitorSipasId(idKlientFurnitor));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKategoriZbritje(JObject param)
        {
            try
            {
                string kf = param["kf"].Value<string>();
                DateTime date = param["date"].Value<DateTime>();
                decimal vlefte = param["vlefte"].Value<decimal>();
                decimal kursi = param["kursi"].Value<decimal>();
                int idmonedha = param["idmonedha"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKategoriZbritje(kf, date, vlefte, kursi, idmonedha, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheAdresatKlientFurnitor(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.ktheAdresatKlientFurnitor(prefixText, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheDegeMagazineSipasKodit(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.ktheDegeMagazineSipasKodit(kodi, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]

        public HttpResponseMessage kthePershkrimDegeSipasID(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();


                return Request.KthePergjigje(RregjistrimeRepository.kthePershkrimDegeSipasID(id));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheTemplateNiveli(JObject param)
        {
            try
            {
                int idNiveli = param["idNiveli"].Value<int>();
                string veprimi = param["veprimi"].Value<string>();
                bool mod = param["mod"].Value<bool>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idGjuha = param["idGjuha"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheTemplateNiveli(idNiveli, veprimi, mod, idPerdoruesi, idNdermarrje, idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrKursiSipasMonedhesDatesDheLlojit(JObject param)
        {
            try
            {
                int idMonedha = param["idMonedha"].Value<int>();
                DateTime date = param["date"].Value<DateTime>();
                int lloji = param["lloji"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.merrKursiSipasMonedhesDatesDheLlojit(idMonedha, date, lloji));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKursinFunditSipasLlojitDB(JObject param)
        {
            try
            {
                int idMonedha = param["idMonedha"].Value<int>();
                int lloji = param["lloji"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();


                return Request.KthePergjigje(RregjistrimeRepository.ktheKursinFunditSipasLlojitDB(idMonedha, lloji, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheMaturim(JObject param)
        {
            try
            {
                string idmat = param["idmat"].Value<string>();
                DateTime dataFat = param["dataFat"].Value<DateTime>();



                return Request.KthePergjigje(RregjistrimeRepository.ktheMaturim(idmat, dataFat));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheCmimArtikulliRow(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                string kodArtikulli = param["kodArtikulli"].Value<string>();
                int nivelCmimi = param["nivelCmimi"].Value<int>();
                string date = param["date"].Value<string>();
                string monedha = param["monedha"].Value<string>();
                string njesia = param["njesia"].Value<string>();
                decimal kursi = param["kursi"].Value<decimal>();
                int idRreshti = param["idRreshti"].Value<int>();
                decimal sasi = param["sasi"].Value<decimal>();
                int shitjeblerje = param["shitjeblerje"].Value<int>();
                string detajim = param["detajim"].Value<string>();

                return Request.KthePergjigje(RregjistrimeRepository.ktheCmimArtikulliRow(idNdermarrje, idPerdoruesi, kodArtikulli, nivelCmimi, date, monedha, njesia, kursi, idRreshti, sasi, shitjeblerje, detajim));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheCmimArtikulliRowNivelBaze(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                string kodArtikulli = param["kodArtikulli"].Value<string>();                
                string date = param["date"].Value<string>();               
                string njesia = param["njesia"].Value<string>();                
                int idRreshti = param["idRreshti"].Value<int>();
                decimal sasi = param["sasi"].Value<decimal>();
                int shitjeblerje = param["shitjeblerje"].Value<int>();               
                return Request.KthePergjigje(RregjistrimeRepository.ktheCmimArtikulliRowNivelBaze(idNdermarrje, idPerdoruesi, kodArtikulli, date, njesia, idRreshti, sasi, shitjeblerje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheCmimArtikulli(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                string kodArtikulli = param["kodArtikulli"].Value<string>();
                int nivelCmimi = param["nivelCmimi"].Value<int>();
                string date = param["date"].Value<string>();
                string monedha = param["monedha"].Value<string>();
                int njesia = param["njesia"].Value<int>();
                decimal kursi = param["kursi"].Value<decimal>();
                int idRreshti = param["idRreshti"].Value<int>();
                decimal sasi = param["sasi"].Value<decimal>();
                int shitjeblerje = param["shitjeblerje"].Value<int>();
                string detajim = param["detajim"].Value<string>();

                return Request.KthePergjigje(RregjistrimeRepository.ktheCmimArtikulli(idNdermarrje, idPerdoruesi, kodArtikulli, nivelCmimi, date, monedha, kursi, idRreshti, sasi, shitjeblerje, detajim, njesia));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheCmimetSipasNiveleveArtComboMeKodRow(JObject param)
        {
            try
            {
                string kodArtikulli = param["kodi"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                string date = param["data"].Value<string>();
                string monedha = param["monedha"].Value<string>();
                string njesia = param["njesia"].Value<string>();
                string kursi = param["kursi"].Value<string>();
                int idRreshti = param["index"].Value<int>();
                string sasi = param["sasi"].Value<string>();
                int nivelCmimi = param["nivel"].Value<int>();
                int shitjeblerje = param["shitjeblerje"].Value<int>();
                string detajim = param["detajim"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheCmimetSipasNiveleveArtComboMeKodRow(kodArtikulli, idNdermarrje, idPerdorues, date, monedha, njesia, kursi, idRreshti, sasi, nivelCmimi, shitjeblerje, detajim));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheZbritjeAnalitikeArtikulliRow(JObject param)
        {
            try
            {
                string kodArtikulli = param["kodArtikulli"].Value<string>();
                int zbritjaKlientit = param["ZbritjaKlientit"].Value<int>();
                string date = param["date"].Value<string>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.ktheZbritjeAnalitikeArtikulliRow(kodArtikulli, zbritjaKlientit, date, idPerdoruesi, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrUrlKthim(JObject param)
        {
            try
            {

                int id = param["id"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                string pageId = param["pageId"].Value<string>();

                return Request.KthePergjigje(RregjistrimeRepository.merrUrlKthim(id, idNdermarrje, pageId));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KontrolloKthim(JObject param)
        {
            try
            {

                int ids = param["ids"].Value<int>();
                bool kthyer = param["kthyer"].Value<bool>();
                string pageId = param["pageId"].Value<string>();

                return Request.KthePergjigje(RregjistrimeRepository.KontrolloKthim(ids, kthyer, pageId));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrUrlPaguaj(JObject param)
        {
            try
            {

                int id = param["id"].Value<int>();
                string veprimi = param["veprimi"].Value<string>();
                string vjenNga = param["vjenNga"].Value<string>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idVitNdermarrje = param["idVitNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrUrlPaguaj(id, veprimi, vjenNga, idPerdoruesi, idNdermarrje, idVitNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage gjejEtapeDokumenti(JObject param)
        {
            try
            {

                int idperdoruesi = param["idperdoruesi"].Value<int>();
                int idDok = param["idDok"].Value<int>();
                var lloji = param["lloji"].ToString();
                return Request.KthePergjigje(RregjistrimeRepository.gjejEtapeDokumenti(idperdoruesi, idDok, lloji));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage mbushFushaPerKonvertim(JObject param)
        {
            try
            {

                object result = param["result"].Value<object>();
                bool merrtedhena = param["merrtedhena"].Value<bool>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.mbushFushaPerKonvertim(result, merrtedhena, idNdermarrje, idPerdorues));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArtikullSipasKodit(JObject param)
        {
            try
            {

                string kodi = param["kodi"].Value<string>();
                bool merrfurnitor = param["merrfurnitor"].Value<bool>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                bool merrDetajime = param["merrDetajime"].Value<bool>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheArtikullSipasKodit(kodi, merrfurnitor, merrDetajime, idNdermarrje, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajHapurMbyllur(JObject param)
        {
            try
            {
                bool hapur = param["hapur"].Value<bool>();
                int idperdorues = param["idperdorues"].Value<int>();
                int idperdoruesveprimi = param["idperdoruesveprimi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ruajHapurMbyllur(hapur, idperdorues, idperdoruesveprimi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajHapurMbyllurplus(JObject param)
        {
            try
            {
                bool hapur = param["hapur"].Value<bool>();
                int idperdorues = param["idperdorues"].Value<int>();
                int idperdoruesveprimi = param["idperdoruesveprimi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ruajHapurMbyllurplus(hapur, idperdorues, idperdoruesveprimi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajHapurMbyllurminus(JObject param)
        {
            try
            {


                bool hapur = param["hapur"].Value<bool>();
                int idperdorues = param["idperdorues"].Value<int>();
                int idperdoruesveprimi = param["idperdoruesveprimi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ruajHapurMbyllurminus(hapur, idperdorues, idperdoruesveprimi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrPerqindjeAgjenti(JObject param)
        {
            try
            {
                int idAgj = param["idAgj"].Value<int>();
                string llojAgj = param["llojAgj"].Value<string>();
                int idKlient = param["idKlient"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrPerqindjeAgjenti(idAgj, llojAgj, idKlient));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage merrGjendjeArtikulliMag(JObject param)
        {
            try
            {
                int idartikulli = param["idartikulli"].Value<int>();
                String mag = param["mag"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrGjendjeArtikulliMag(idartikulli, mag, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrurlMedianInputCheck(JObject param)
        {
            try
            {
                return Request.KthePergjigje(RregjistrimeRepository.merrurlMedianInputCheck());
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraArtMeID(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int iddetajim = param["iddetajim"].Value<int>();
                string magazina = param["magazina"].Value<string>();
                bool meDetajim = param["meDetajim"].Value<bool>();
                object sasiaNeGride = param["sasiaNeGride"] == null ? null : param["sasiaNeGride"].Value<object>();
                object magazinatKoka = param["magazinatKoka"] == null ? null : param["magazinatKoka"].Value<object>();
                bool merrMagMeAutorizim = param["merrMagMeAutorizim"] == null ? true : param["merrMagMeAutorizim"].Value<bool>();
                string magazinaDest = param["magazinaDest"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int njesiDef = param["njesiDef"].Value<int>();
                decimal sasiaNeRresht = param["sasiaNeRresht"].Value<decimal>();
                bool merrCmim = param["merrCmim"].Value<bool>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraArtMeID(idja, rreshti, data, magazina, sasiaNeGride, magazinatKoka, merrMagMeAutorizim, meDetajim, iddetajim, magazinaDest, idNdermarrje, idPerdoruesi, njesiDef, sasiaNeRresht, merrCmim));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraArtikujMeID(JObject param)
        {
            try
            {
                int idja, rreshti, iddetajim, nrArtikujve = param.Count;
                DateTime data;
                string magazina, magazinaDest;
                bool meDetajim, merrMagMeAutorizim;
                object sasiaNeGride, magazinatKoka;
                object[] all = new object[nrArtikujve];
                for (int i = 0; i < nrArtikujve; i++)
                {
                    JObject p = (JObject)param["" + i.ToString() + ""].Value<object>();
                    idja = p["idja"].Value<int>();
                    rreshti = p["rreshti"].Value<int>();
                    data = p["data"].Value<DateTime>();
                    iddetajim = p["iddetajim"].Value<int>();
                    magazina = p["magazina"].Value<string>();
                    meDetajim = p["meDetajim"].Value<bool>();
                    sasiaNeGride = p["sasiaNeGride"] == null ? null : p["sasiaNeGride"].Value<object>();
                    magazinatKoka = p["magazinatKoka"] == null ? null : p["magazinatKoka"].Value<object>();
                    merrMagMeAutorizim = p["merrMagMeAutorizim"] == null ? true : p["merrMagMeAutorizim"].Value<bool>();
                    magazinaDest = p["magazinaDest"].Value<string>();
                    int idNdermarrje = p["idNdermarrje"].Value<int>();
                    int idPerdoruesi = p["idPerdoruesi"].Value<int>();
                    int njesiDef = param["njesiDef"].Value<int>();
                    decimal sasiaNeRresht = param["sasiaNeRresht"].Value<decimal>();
                    bool merrCmim = param["merrCmim"].Value<bool>();
                    all[i] = RregjistrimeRepository.ktheRowVleraArtMeID(idja, rreshti, data, magazina, sasiaNeGride, magazinatKoka, merrMagMeAutorizim, meDetajim, iddetajim, magazinaDest, idNdermarrje, idPerdoruesi, njesiDef, sasiaNeRresht, merrCmim);
                }
                return Request.KthePergjigje(all);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraArtMeIDSet(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();


                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraArtMeIDSet(idja, rreshti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraArtMeKodOseKodBar(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int index = param["index"].Value<int>();
                DateTime data = param["date"].Value<DateTime>();
                int iddetajim = param["iddetajim"].Value<int>();
                string magazina = param["magazina"].Value<string>();
                bool meDetajim = param["meDetajim"].Value<bool>();
                object sasiteNeGrideObj = param["sasiaNeGride"] == null ? null : param["sasiaNeGride"].Value<object>();
                object magazinatKoka = param["magazinatKoka"] == null ? null : param["magazinatKoka"].Value<object>();
                bool merrMagMeAutorizim = param["merrMagMeAutorizim"] == null ? true : param["merrMagMeAutorizim"].Value<bool>();
                string magazinaDest = param["magazinaDest"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                bool merrSipasDetajimit = param["merrSipasDetajimit"].Value<bool>();
                int njesiDef = param["njesiDef"].Value<int>();
                decimal sasiaNeRresht =  param["sasiaNeRresht"].Value<decimal>();
                bool merrCmim = param["merrCmim"].Value<bool>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraArtMeKodOseKodBar(kodi, index, data, magazina, sasiteNeGrideObj, magazinatKoka, merrMagMeAutorizim, iddetajim, meDetajim, magazinaDest, idNdermarrje, idPerdorues, merrSipasDetajimit, njesiDef, sasiaNeRresht, merrCmim));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraArtMeKodSet(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int index = param["index"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraArtMeKodSet(kodi, index, idNdermarrje, idPerdorues));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrGjitheArtikujtSet(JObject param)
        {
            try
            {
                int nr = param.Count;
                object[] all = new Object[nr];
                for (int i = 0; i < nr; i++)
                {
                    JObject p = (JObject)param["" + i.ToString() + ""].Value<object>();


                    string kodi = p["kodi"].Value<string>();
                    int index = p["index"].Value<int>();
                    decimal sasia = p["sasia"].Value<decimal>();
                    DateTime data = p["date"].Value<DateTime>();
                    object magazinatKoka = p["magazinatKoka"] == null ? null : p["magazinatKoka"].Value<object>();
                    bool merrMagMeAutorizim = p["merrMagMeAutorizim"] == null ? true : p["merrMagMeAutorizim"].Value<bool>();
                    int idNdermarrje = p["idNdermarrje"].Value<int>();
                    int idPerdoruesi = p["idPerdoruesi"].Value<int>();
                    int idArtikulli = p["idArtikulli"].Value<int>();
                    all[i] = RregjistrimeRepository.merrArtikujSet(kodi, index, data, sasia, magazinatKoka, merrMagMeAutorizim, idNdermarrje, idPerdoruesi, idArtikulli);
                }
                return Request.KthePergjigje(all);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrArtikujSet(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int index = param["index"].Value<int>();
                decimal sasia = param["sasia"].Value<decimal>();
                DateTime data = param["date"].Value<DateTime>();
                object magazinatKoka = param["magazinatKoka"] == null ? null : param["magazinatKoka"].Value<object>();
                bool merrMagMeAutorizim = param["merrMagMeAutorizim"] == null ? true : param["merrMagMeAutorizim"].Value<bool>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrArtikujSet(kodi, index, data, sasia, magazinatKoka, merrMagMeAutorizim, idNdermarrje, idPerdoruesi, 0));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraArtRezMeKodOseKodBar(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int idmag = param["idmag"].Value<int>();
                int iddetajim = param["iddetajim"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraArtRezMeKodOseKodBar(kodi, rreshti, data, iddetajim, idmag, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage EkzistonFazaKontrates(JObject param)
        {
            try
            {
                int idFaza = param["idFaza"].Value<int>();
                int idKontrata = param["idKontrata"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.EkzistonFazaKontrates(idFaza, idKontrata, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRoletSipasPerdoruesit(JObject param)
        {
            try
            {
                return Request.KthePergjigje(RregjistrimeRepository.ktheRoletSipasPerdoruesit(param["idPerdoruesi"].Value<int>()));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        //[HttpPost, HttpGet]
        //public HttpResponseMessage ekzistonKodBar(JObject param)
        //{
        //    try
        //    {
        //        return Request.KthePergjigje(RregjistrimeRepository.ekzistonKodBar(param["kodbar"].Value<string>(), param["idNdermarrje"].Value<int>() ));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.KthePergjigjeGabim(param, ex);
        //    }
        //}
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArtInfo(JObject param)
        {
            try
            {
                return Request.KthePergjigje(RregjistrimeRepository.ktheArtInfo(
                    param["kodbar"].Value<string>(),
                    param["idNdermarrje"].Value<int>(),
                    param["idInfoArt"].Value<int>(),
                    param["mag"].Value<string>(),
                    param["eshteAfatShkurter"].Value<bool>(),
                    param["data"].Value<DateTime>(),
                    param["idViti"].Value<int>(),
                    param["idPerdoruesi"].Value<int>()
                    ));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kontrolloLimitSasiKarte(JObject param)
        {
            try
            {
                int idartikulli = param["idartikulli"].Value<int>();
                int idKarte = param["idkarta"].Value<int>();
                DateTime dtDok = param["dtDok"].Value<DateTime>();
                double totali = param["totali"].Value<double>();


                return Request.KthePergjigje(RregjistrimeRepository.kontrolloLimitSasiKarte(idartikulli, idKarte, dtDok, totali));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }

        [HttpPost, HttpGet]
        public HttpResponseMessage kontrolloLimitVlereKarte(JObject param)
        {
            try
            {
                int idartikulli = param["idartikulli"].Value<int>();
                int idKarte = param["idkarta"].Value<int>();
                DateTime dtDok = param["dtDok"].Value<DateTime>();
                double totali = param["totali"].Value<double>();


                return Request.KthePergjigje(RregjistrimeRepository.kontrolloLimitVlereKarte(idartikulli, idKarte, dtDok, totali));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }


        [HttpPost, HttpGet]
        public HttpResponseMessage merrKonfigurimPasqyre(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.merrKonfigurimPasqyre(id));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKartaKlientiDT(JObject param)
        {
            try
            {
                int idKlient = param["idKlient"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKartaKlientiDT(idKlient, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKartaKlientiAll(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKartaKlientiAll(idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKategoriZbritjeKarteKlienti(JObject param)
        {
            try
            {
                int idKatZbritje = param["idKatZbritje"].Value<int>();
                DateTime date = param["date"].Value<DateTime>();
                decimal vlefte = param["vlefte"].Value<decimal>();
                decimal kursi = param["kursi"].Value<decimal>();
                int idmonedha = param["idmonedha"].Value<int>();


                return Request.KthePergjigje(RregjistrimeRepository.ktheKategoriZbritjeKarteKlienti(idKatZbritje, date, vlefte, kursi, idmonedha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage kthePolitikeKarte(JObject param)
        {
            try
            {
                int idPolitike = param["idPolitike"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.kthePolitikeKarte(idPolitike));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKarteKlienti(JObject param)
        {
            try
            {
                int idKarte = param["idKarte"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKarteKlienti(idKarte));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage kthePikeKarte(JObject param)
        {
            try
            {

                int idKarte = param["idkarta"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.kthePikeKarte(idKarte, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }

        public HttpResponseMessage kthePikeDheLidhjeKarte(JObject param)
        {
            try
            {
                int idKarte = param["idkarta"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.kthePikeDheLidhjeKarte(idKarte, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheTrupiPolitikeKarte(JObject param)
        {
            try
            {
                int idPolitike = param["idPolitike"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheTrupiPolitikeKarte(idPolitike));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArrayMagazinatClientSideTeSerializuar(JObject param)
        {
            //GTOCHECK merr ne fillim ,ska nevoj per ws

            try
            {
                var merrMeAutorizim = param["merrMeAutorizim"].Value<bool>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheArrayMagazinatClientSideTeSerializuar(idNdermarrje, idPerdoruesi, merrMeAutorizim));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);

            }

        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ruajNeSessionTrupInfoArt(JObject param)
        {
            try
            {


                int idkoka = param["idkoka"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.ruajNeSessionTrupInfoArt(idkoka, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKlientFurnitor(JObject param)
        {
            try
            {


                string emri = param["emri"].Value<string>();
                DateTime data = param["data"].Value<DateTime>();

                return Request.KthePergjigje(RregjistrimeRepository.ktheKlientFurnitor(emri, data, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }


        [HttpPost, HttpGet]
        public HttpResponseMessage ktheTemplatetNjesiVartese(JObject param)
        {
            try
            {
                return Request.KthePergjigje(RregjistrimeRepository.ktheTemplatetNjesiVartese(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheEkzistenceDhePershkrimArtikulliNgaKodbari(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                string kodbar = param["kodbar"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheEkzistenceDhePershkrimArtikulliNgaKodbari(idNdermarrje, kodbar));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrMesazhNgaSesioni(JObject param)
        {
            try
            {

                return Request.KthePergjigje(RregjistrimeRepository.merrMesazhNgaSesioni(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage eshteVeprimILejuar(JObject param)
        {
            try
            {
                string emerKomponente = param["emerKomponente"].Value<string>();
                string veprimi = param["veprimi"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.eshteVeprimILejuar(emerKomponente, veprimi, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheIndexSelectedFilterPeriudhaKusht(JObject param)
        {
            try
            {

                int idKonfigurimi = param["idKonfigurimi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheIndexSelectedFilterPeriudhaKusht(idKonfigurimi));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheTeDhenaLlojPeriudhe(JObject param)
        {
            try
            {
                string vleratId = param["vleratId"].Value<string>();

                return Request.KthePergjigje(RregjistrimeRepository.KtheTeDhenaLlojPeriudhe(vleratId));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheTeDhenaLlojPeriudheNew(JObject param)
        {
            try
            {
                int kategoria = param["kategoria"].Value<int>();
                int selectedIdLlojPeriudhe = param["selectedIdLlojPeriudhe"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheTeDhenaLlojPeriudheNew(kategoria, selectedIdLlojPeriudhe));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrTotalAmortizimi(JObject param)
        {
            try
            {

                return Request.KthePergjigje(RregjistrimeRepository.merrTotalAmortizimi(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrPershkrimQendra(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                string key = param["key"].Value<string>();

                return Request.KthePergjigje(RregjistrimeRepository.merrPershkrimQendra(kodi, key, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrPershkrimObjektiva(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                string key = param["key"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.merrPershkrimObjektiva(kodi, key, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KontrolloKonvertuarRezervime(JObject param)
        {
            try
            {
                int[] ids = param["ids"].ToObject<int[]>();
                string pageId = param["pageId"].ToObject<string>();
                return Request.KthePergjigje(RregjistrimeRepository.KontrolloKonvertuarRezervime(ids, pageId, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KontrolloEkzistonIMEI(JObject param)
        {
            try
            {
                string detajim = param["detajim"].Value<string>();
                int idartikulli = param["idartikulli"].Value<int>();
                string key = param["key"].Value<string>();
                DateTime data = param["data"].Value<DateTime>();
                int idmag = param["idmag"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KontrolloEkzistonIMEI(detajim, idartikulli, key, data, idmag, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KontrolloKaAutorizimStatusi(JObject param)
        {
            try
            {
                int idstatusi = param["idstatusi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KontrolloKaAutorizimStatusi(idstatusi, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KontrolloKaGjendjeSwap(JObject param)
        {
            try
            {
                string detajim = param["detajim"].Value<string>();
                DateTime data = param["data"].Value<DateTime>();
                int idmag = param["idmag"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KontrolloKaGjendjeSwap(detajim, data, idmag, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowGjendjeSerialPerMagazine(JObject param)
        {
            try
            {
                string serial = param["serial"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int idndermarja = param["idndermarja"].Value<int>();
                bool rezerva = param["rezerva"].Value<bool>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowGjendjeSerialPerMagazine(serial, rreshti, data, idndermarja, rezerva));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowGjendjeArtikulliPerMagazine(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                string magazina = param["magazina"].Value<string>();
                string serial = param["serial"].Value<string>();
                int idndermarja = param["idndermarja"].Value<int>();
                bool rezerva = param["rezerva"].Value<bool>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowGjendjeArtikulliPerMagazine(idja, rreshti, data, magazina, serial, idndermarja, rezerva));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrTrupKarakteristikaAmortizimi(JObject param)
        {
            try
            {

                int id = param["id"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrTrupKarakteristikaAmortizimi(id));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheEmerLlogarie(JObject param)
        {
            try
            {

                string prefixText = param["prefixText"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheEmerLlogarie(prefixText, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheFaturatFiltruaraShpezobj(JObject param)
        {
            try
            {

                int id = param["id"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheFaturatFiltruaraShpezobj(id));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheMsgFatureShperndare(JObject param)
        {
            try
            {
                int idKokaMagazina = param["idKokaMagazina"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheMsgFatureShperndare(idKokaMagazina));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraBurimeMeIDRow(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheVleraBurimeMeIDRow(idja, rreshti));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheVleraBurimeMeKodRow(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheVleraBurimeMeKodRow(kodi, rreshti, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraAktiviteteMeIDRow(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheVleraAktiviteteMeIDRow(idja, rreshti));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheVleraAktiviteteMeKodRow(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheVleraAktiviteteMeKodRow(kodi, rreshti, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraArtMeIDPerRec(JObject param)
        {
            try
            {

                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int idmag = param["idmag"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraArtMeIDPerRec(idja, rreshti, data, idmag, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraArtMeKodRec(JObject param)
        {
            try
            {

                string kodi = param["kodi"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                string kodmag = param["kodmag"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraArtMeKodRec(kodi, rreshti, data, kodmag, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheBurime(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheBurime(prefixText, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheAktiviteteSipasBurimit(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                int idburimi = param["idburimi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheAktiviteteSipasBurimit(prefixText, idburimi, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArtikullPlanifikimi(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheArtikullPlanifikimi(idja, rreshti, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArtikullPerProdhimPlanifikim(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                int idplanifikimi = param["idplanifikimi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheArtikullPerProdhimPlanifikim(prefixText, idplanifikimi, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheEmailPerdoruesi(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                string[] perdorues = param["perdorues"].ToObject<string[]>();
                string[] lloji = param["lloji"].ToObject<string[]>();
                int key = param["key"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheEmailPerdoruesi(prefixText, perdorues, lloji, key, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kontrolloKaAdresaroli(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                string[] perdorues = param["perdorues"].ToObject<string[]>();
                string[] lloji = param["lloji"].ToObject<string[]>();
                int key = param["key"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.kontrolloKaAdresaroli(prefixText, perdorues, lloji, key, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrMin(JObject param)
        {
            try
            {
                DateTime date = param["date"].Value<DateTime>();
                return Request.KthePergjigje(RregjistrimeRepository.merrMin(date, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrKursiSipasKodMonedhesAndDates(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                string date = param["date"].Value<string>();
                int lloji = param["lloji"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrKursiSipasKodMonedhesAndDates(prefixText, date, lloji, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArrayKlienteFurnitoresh(JObject param)
        {
            try
            {
                string infixText = param["infixText"].Value<string>();
                int tipKlientFurnitor = param["tipKlientFurnitor"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheArrayKlienteFurnitoresh(infixText, tipKlientFurnitor, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKlientFurnitorVeprimeKF(JObject param)
        {
            try
            {
                string inFixText = param["inFixText"].Value<string>();
                DateTime data = param["data"].Value<DateTime>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKlientFurnitorVeprimeKF(inFixText, data, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrKursetMonedhaveDate(JObject param)
        {
            try
            {
                DateTime datedok = param["datedok"].Value<DateTime>();
                int llojKursi = param["llojKursi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrKursetMonedhaveDate(datedok, llojKursi, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheNrLlogarie(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheNrLlogarie(prefixText));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheNrLlogarish(JObject param)
        {
            try
            {
                var ids = param["ids"].ToObject<int[]>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheNrLlogarish(ids));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage ktheNdermarrjetDheVitet(JObject param)
        {
            try
            {
                int idRoli = param["idRoli"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheNdermarrjetDheVitet(idRoli));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRolet(JObject param)
        {
            try
            {
                int idGjuha = param["idGjuha"].Value<int>();
                int idRoli = param["idRoli"].Value<int>();
                int idViti = param["idViti"].Value<int>();
                string idlicence = param["idlicence"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRolet(idGjuha, idRoli, idViti, idlicence, idNdermarrje));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheIdVitiSipasKodVitiDheIdNdermarrje(JObject param)
        {
            try
            {
                string kodViti = param["kodViti"].Value<string>();
                string check = param["check"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheIdVitiSipasKodVitiDheIdNdermarrje(kodViti, check, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheVitetPerNdermarrjen(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheVitetPerNdermarrjen(idNdermarrje));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKurseMonedhashSipasDates(JObject param)
        {
            try
            {
                string dtDokumenti = param["dtDokumenti"].Value<string>();
                int llojKursi = param["llojKursi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKurseMonedhashSipasDates(dtDokumenti, llojKursi, idNdermarrje, idPerdorues));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheLlojeClientSide(JObject param)
        {
            try
            {

                string text = param["text"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheLlojeClientSide(text));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrKursinMonedhenGjendjenSipasBankesAndDates(JObject param)
        {
            try
            {

                int idBanka = param["idBanka"].Value<int>();
                string dtDokumenti = param["dtDokumenti"].Value<string>();
                int idKonfigurimi = param["idKonfigurimi"].Value<int>();
                int llojKursi = param["llojKursi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrKursinMonedhenGjendjenSipasBankesAndDates(idBanka, dtDokumenti, idKonfigurimi, llojKursi, idPerdorues, idNdermarrje));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheEmerKlientFurnitorNew(JObject param)
        {
            try
            {

                string EmertimiKF = param["EmertimiKF"].Value<string>();
                bool LlojiKF = param["LlojiKF"].Value<bool>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheEmerKlientFurnitorNew(EmertimiKF, LlojiKF, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheIsValidDateDokumenti(JObject param)
        {
            try
            {

                string dt = param["dt"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheIsValidDateDokumenti(dt, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKushtePagese(JObject param)
        {
            try
            {

                object[] prefixText = param["prefixText"].ToObject<object[]>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKushtePagese(prefixText));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KthePathinEThemit(JObject param)
        {
            try
            {

                string input = param["input"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.KthePathinEThemit(input));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheThemeAmbjenteSipasId(JObject param)
        {
            try
            {

                int id = param["id"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheThemeAmbjenteSipasId(id));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheEmratThemeSelektuar(JObject param)
        {
            try
            {

                int id = param["id"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheEmratThemeSelektuar(id, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheIDThemeBgImg(JObject param)
        {
            try
            {

                string input = param["input"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheIDThemeBgImg(input));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheIDThemeDevExJQuery(JObject param)
        {
            try
            {

                string input = param["input"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheIDThemeDevExJQuery(input));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KaVeprimeVendndodhje(JObject param)
        {
            try
            {

                int id = param["id"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KaVeprimeVendndodhje(id));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArrayMeDataZbritje(JObject param)
        {
            try
            {

                string kodi = param["kodi"].Value<string>();
                string kodbar = param["kodbar"].Value<string>();
                string emer1 = param["emer1"].Value<string>();
                string emer2 = param["emer2"].Value<string>();
                string kodifikim1 = param["kodifikim1"].Value<string>();
                string kodifikim2 = param["kodifikim2"].Value<string>();
                string furnitori = param["furnitori"].Value<string>();
                string njesia = param["njesia"].Value<string>();
                string datafill = param["datafill"].Value<string>();
                string datambar = param["datambar"].Value<string>();
                string idnivelcmimi = param["idnivelcmimi"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheArrayMeDataZbritje(kodi, kodbar, emer1, emer2, kodifikim1, kodifikim2, furnitori, njesia, datafill, datambar, idnivelcmimi, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kaVeprimeOpsion(JObject param)
        {
            try
            {

                int idopsioni = param["idopsioni"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.kaVeprimeOpsion(idopsioni));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArtikullPerProdhim(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                int klasa = param["klasa"].Value<int>();
                //int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheArtikullPerProdhim(prefixText, klasa, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheTemplateteNivelit(JObject param)
        {
            try
            {
                string lloji = param["lloji"].Value<string>();
                string veprimi = param["veprimi"].Value<string>();
                bool mod = param["mod"].Value<bool>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheTemplateteNivelit(lloji, veprimi, mod, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrDataNdryshimiAktiviteti(JObject param)
        {
            try
            {
                int idkoka = param["idkoka"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrDataNdryshimiAktiviteti(idkoka));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrDataNdryshimiArtPerberes(JObject param)
        {
            try
            {
                int idartikulli = param["idartikulli"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrDataNdryshimiArtPerberes(idartikulli));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrGjendjeArtikulli(JObject param)
        {
            try
            {
                int idartikulli = param["idartikulli"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrGjendjeArtikulli(idartikulli));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheIdMonedhaSipasIdLlogarise(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int idrreshti = param["idrreshti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheIdMonedhaSipasIdLlogarise(idja, idrreshti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ndryshoNjesi(JObject param)
        {
            try
            {
                int key = param["key"].Value<int>();
                int njesia = param["njesia"].Value<int>();
                int idart = param["idart"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ndryshoNjesi(key, njesia, idart));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraArtMeIDMeRec(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int magazina = param["magazina"].Value<int>();
                int magazina2 = param["magazina2"].Value<int>();
                string sasiplanrec = param["sasiplanrec"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraArtMeIDMeRec(idja, rreshti, data, magazina, magazina2, sasiplanrec, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraArtMeRec(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int magazina = param["magazina"].Value<int>();
                int magazina2 = param["magazina2"].Value<int>();
                string sasiplanrec = param["sasiplanrec"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraArtMeRec(kodi, rreshti, data, magazina, magazina2, sasiplanrec, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheNormeTvsh(JObject param)
        {
            try
            {

                int idTvsh = param["id"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.ktheNormeTvsh(idTvsh));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage MerrPlanifikimeTeGjeneruara(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();
                int magazinaprod = param["magazinaprod"].Value<int>();
                int magazinarec = param["magazinarec"].Value<int>();
                DateTime date = param["date"].Value<DateTime>();
                string sasiplanrec = param["sasiplanrec"].Value<string>();
                string sasiburimi = param["sasiburimi"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.MerrPlanifikimeTeGjeneruara(id, magazinaprod, magazinarec, date, sasiplanrec, sasiburimi, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheMonedheNdermarrjeClientSide(JObject param)
        {
            try
            {
                return Request.KthePergjigje(RregjistrimeRepository.ktheMonedheNdermarrjeClientSide(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrKursiSipasMonedhesAndDatesDheRreshti(JObject param)
        {
            try
            {
                int idMonedha = param["idMonedha"].Value<int>();
                DateTime date = param["date"].Value<DateTime>();
                int idrreshti = param["idrreshti"].Value<int>();
                string kodmonedha = param["kodmonedha"].Value<string>();
                int lloji = param["lloji"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrKursiSipasMonedhesAndDatesDheRreshti(idMonedha, date, idrreshti, kodmonedha, lloji, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrKursetDheMonedhatSipasDates(JObject param)
        {
            try
            {
                string[] prefixText = param["prefixText"].ToObject<string[]>();
                DateTime date = param["date"].Value<DateTime>();
                return Request.KthePergjigje(RregjistrimeRepository.merrKursetDheMonedhatSipasDates(prefixText, date, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage kaVeprimeFusha(JObject param)
        {
            try
            {
                int iddokumenti = param["iddokumenti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.kaVeprimeFusha(iddokumenti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrBankaSipasId(JObject param)
        {
            try
            {
                int idbanka = param["idbanka"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrBankaSipasId(idbanka));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrKfSipasId(JObject param)
        {
            try
            {
                int idkf = param["idkf"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrKfSipasId(idkf));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheMonedheLlogSipasKodit(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheMonedheLlogSipasKodit(kodi, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajFusha(JObject param)
        {
            try
            {
                string vlera = param["vlera"].Value<string>();
                string fusha = param["fusha"].Value<string>();
                int index = param["index"].Value<int>();
                RregjistrimeRepository.ruajFusha(vlera, fusha, index, Session);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKonfigurimetENivelit(JObject param)
        {
            try
            {
                string kodNiveli = param["kodNiveli"].Value<string>();
                int idKategori = param["idKategori"].Value<int>();
                int idGjuha = param["idGjuha"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKonfigurimetENivelit(kodNiveli, idKategori, idGjuha, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheBuxhetimViteSipasKonfigurimit(JObject param)
        {
            try
            {
                string kodKonfigAmbjente = param["kodKonfigAmbjente"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheBuxhetimViteSipasKonfigurimit(Session, kodKonfigAmbjente));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage gjejEtapeDokumentiList(JObject param)
        {
            try
            {

                int idkoka = param["idkoka"].Value<int>();
                int idperdoruesi = param["idperdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.gjejEtapeDokumentiList(idkoka, idperdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kthePershkrimArtikulli(JObject param)
        {
            try
            {

                string prefixText = param["prefixText"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.kthePershkrimArtikulli(prefixText, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKurseSipasIdMonedhe(JObject param)
        {
            try
            {

                int idMon = param["idMon"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKurseSipasIdMonedhe(idMon));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrImazh(JObject param)
        {
            try
            {

                int idndermarje = param["idndermarje"].Value<int?>().GetValueOrDefault();

                return Request.KthePergjigje(RregjistrimeRepository.merrImazh(idndermarje, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        public HttpResponseMessage merrMeTvshNdermarrje(JObject param)
        {
            try
            {

                int idndermarje = param["idndermarje"].Value<int?>().GetValueOrDefault();

                return Request.KthePergjigje(RregjistrimeRepository.merrKodTvshNdermarrje(idndermarje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArtikullOseMakro2(JObject param)
        {
            try
            {
                string prefixText = param["prefixText"].Value<string>();
                int count = param["count"].Value<int>();
                string contextKey = param["contextKey"].Value<string>();
                int klasa = param["klasa"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheArtikullOseMakro2(prefixText, count, contextKey, klasa, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kthePershkrimArtikulliP(JObject param)
        {
            try
            {

                string prefixText = param["prefixText"].Value<string>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.kthePershkrimArtikulliP(prefixText, idPerdorues, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage ktheArtikujPerberesSipasDates(JObject param)
        {
            try
            {

                int id = param["id"].Value<int>();
                string data = param["data"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheArtikujPerberesSipasDates(id, data, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KontrolloDetajimLidhur(JObject param)
        {
            try
            {

                int idartikulli = param["idartikulli"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KontrolloDetajimLidhur(idartikulli));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KontrolloDetajimLidhurSipasLlojit(JObject param)
        {
            try
            {

                int idartikulli = param["idartikulli"].Value<int>();
                int lloji = param["lloji"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KontrolloDetajimLidhurSipasLlojit(idartikulli, lloji));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajNeSessionURL(JObject param)
        {
            try
            {

                string url = param["url"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ruajNeSessionURL(url, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrGjendje(JObject param)
        {
            try
            {
                string[] kodikf = param["kodikf"].ToObject<string[]>();
                DateTime datedok = param["datedok"].Value<DateTime>();
                int[] i = param["i"].ToObject<int[]>();
                int iddok = param["iddok"].Value<int>();
                string kodkonfigurim = param["kodkonfigurim"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.merrGjendje(kodikf, datedok, i, iddok, kodkonfigurim, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrKursetMonedhaveSipasDates(JObject param)
        {
            try
            {

                DateTime datedok = param["datedok"].Value<DateTime>();
                bool pershkrimi = param["pershkrimi"].Value<bool>();
                int llojkursi = param["llojkursi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrKursetMonedhaveSipasDates(datedok, pershkrimi, llojkursi, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage eshteAzhornimVeprimiFunditKlientFurnitor(JObject param)
        {
            try
            {

                int idKf = param["idKf"].Value<int>();
                DateTime dateSelektuar = param["dateSelektuar"].Value<DateTime>();
                string kodMonedhaKlFurn = param["kodMonedhaKlFurn"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.eshteAzhornimVeprimiFunditKlientFurnitor(idKf, dateSelektuar, kodMonedhaKlFurn, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraArtMeIDMeRecRes(JObject param)
        {
            try
            {

                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                string magazi = param["magazi"].Value<string>();
                int magazina2 = param["magazina2"].Value<int>();
                double sasia = param["sasia"].Value<double>();
                string sasiplanrec = param["sasiplanrec"].Value<string>();
                int idtrupiplanifikimi = param["idtrupiplanifikimi"].Value<int>();
                string sasiburimi = param["sasiburimi"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraArtMeIDMeRecRes(idja, rreshti, data, magazi, magazina2, sasia, sasiplanrec, idtrupiplanifikimi, sasiburimi, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrKostoMagazineKod(JObject param)
        {
            try
            {
                string recepturat = param["recepturat"].ToString();
                DateTime data = param["data"].Value<DateTime>().ToLocalTime();
                return Request.KthePergjigje(RregjistrimeRepository.merrKostoMagazineKod(recepturat, data, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheMonedheNdermarrje(JObject param)
        {
            try
            {

                return Request.KthePergjigje(RregjistrimeRepository.ktheMonedheNdermarrje(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheFatureZhdoganuar(JObject param)
        {
            try
            {

                string[] idt = param["idt"].ToObject<string[]>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheFatureZhdoganuar(idt));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheFaturaPerFleteDoganoreobj(JObject param)
        {
            try
            {

                string[] pars = param["pars"].ToObject<string[]>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheFaturaPerFleteDoganoreobj(pars));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kontrolloPrintuarKase(JObject param)
        {
            try
            {
                object[][] id = param["id"].ToObject<object[][]>();
                var lloji = param["lloji"].ToString();
                return Request.KthePergjigje(RregjistrimeRepository.kontrolloPrintuarKase(id, lloji));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kontrolloKonvertuarDheKase(JObject param)
        {
            try
            {
                object[][] id = param["id"].ToObject<object[][]>();
                var lloji = param["lloji"].ToString();
                string kodkonfig = param["kodkonfig"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idGjuha = param["idGjuha"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.kontrolloKonvertuarDheKase(id, lloji, kodkonfig, idNdermarrje, idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kontrolloAnulluar(JObject param)
        {
            try
            {
                object[] id = param["id"].ToObject<object[]>();
                return Request.KthePergjigje(RregjistrimeRepository.kontrolloAnulluar(id));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage janeUrdherShitje(JObject param)
        {
            try
            {

                int[] ids = param["ids"].ToObject<int[]>();
                int[] idNivele = param["idNivele"].ToObject<int[]>();
                return Request.KthePergjigje(RregjistrimeRepository.janeUrdherShitje(ids, idNivele));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage janeOferteShitje(JObject param)
        {
            try
            {

                int[] ids = param["ids"].ToObject<int[]>();
                int[] idNivele = param["idNivele"].ToObject<int[]>();
                return Request.KthePergjigje(RregjistrimeRepository.janeOferteShitje(ids, idNivele));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheVleratEShitjesPerFiskalizimin(JObject param)
        {
            try
            {

                int idKokaShitje = param["idKokaShitje"].ToObject<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheVleratEShitjesPerFiskalizimin(idKokaShitje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage janeOferteBlerje(JObject param)
        {
            try
            {

                int[] ids = param["ids"].ToObject<int[]>();
                int[] idNivele = param["idNivele"].ToObject<int[]>();
                return Request.KthePergjigje(RregjistrimeRepository.janeOferteBlerje(ids, idNivele));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheGjendjeVlefteSipasMagazines(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int iddetajim = param["iddetajim"].Value<int>();
                string magazina = param["magazina"].Value<string>();
                int idndermarje = param["idndermarje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheGjendjeVlefteSipasMagazines(idja, rreshti, data, iddetajim, magazina, idndermarje));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheIdKategoriDetajimi(JObject param)
        {
            try
            {
                string lloji = param["lloji"].Value<string>();
                int idArt = param["idArt"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheIdKategoriDetajimi(lloji, idArt));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKonfigurimRaporti(JObject param)
        {
            try
            {
                int idGjuha = param["idGjuha"].Value<int>();
                int idKonfigRaporti = param["idKonfigRaporti"].Value<int>();
                int idModuli = param["idModuli"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.ktheKonfigurimRaporti(idGjuha, idKonfigRaporti, idModuli));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage eshteDetajimLidhur(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.eshteDetajimLidhur(kodi, idNdermarrje, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
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
                bool merrFormatKursi = param["merrFormatKursi"].Value<bool>();
                int idGjuha = param["idGjuha"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                string llojVeprimi = param["llojVeprimi"].Value<string>();
                DateTime? dateDok = param["dateDok"] == null ? (DateTime?)null : param["dateDok"].Value<DateTime>();
                DateTime? dateDokDefault = param["dateDokDefault"] == null ? (DateTime?)null : param["dateDokDefault"].Value<DateTime>();
                //HttpStatusCode.Forbidden
                return Request.KthePergjigje(RregjistrimeRepository.ktheKonfigDB(idKomp, kodKonf, idNdermarrje, kodKontrollKlienti, idKlienti, merrFormatKursi, idGjuha, idPerdoruesi, llojVeprimi, dateDok, dateDokDefault));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ruajFushaAnketa(JObject param)
        {
            try
            {
                string vlera = param["vlera"].Value<string>();
                string fusha = param["fusha"].Value<string>();
                int index = param["index"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ruajFushaAnketa(vlera, fusha, index, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage MerrKonfigurimExporti(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.MerrKonfigurimExporti(id, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage MerrFiltraExporti(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.MerrFiltraExporti(id, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage MerrFormatImportiSipasKategorise(JObject param)
        {
            try
            {
                int idkategoria = param["idkategoria"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.MerrFormatImportiSipasKategorise(idkategoria, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage MerrKategoritePerImport(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idViti = param["idViti"].Value<int>();
                string komponente = param["komponente"].Value<string>();

                return Request.KthePergjigje(RregjistrimeRepository.MerrKategoritePerImport(idNdermarrje, idViti, idPerdoruesi, komponente));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrKonfigFormatNrTrupi(JObject param)
        {
            try
            {
                int idKokaFormatNr = param["idKokaFormatNr"].Value<int>();
                string idPerdoruesi = param["idPerdoruesi"].Value<string>();
                string idNdermarrje = param["idNdermarrje"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.merrKonfigFormatNrTrupi(idKokaFormatNr, idPerdoruesi, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage MerrProjekteTeGjeneruara(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.MerrProjekteTeGjeneruara(id, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrLlojeNgaKategoria(JObject param)
        {
            try
            {
                int[] kat = param["kat"].ToObject<int[]>();
                return Request.KthePergjigje(RregjistrimeRepository.merrLlojeNgaKategoria(kat, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKodPrindiGrupKF(JObject param)
        {
            try
            {
                string idja = param["idja"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKodPrindiGrupKF(idja));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kaVeprimeGrupimi(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.kaVeprimeGrupimi(id));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage MerrKonfigurimImporti(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.MerrKonfigurimImporti(id, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKodPrindiGrupArtikull(JObject param)
        {
            try
            {
                string idja = param["idja"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKodPrindiGrupArtikull(idja));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage kaVeprimeKodifikim(JObject param)
        {
            try
            {
                int iddokumenti = param["iddokumenti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.kaVeprimeKodifikim(iddokumenti, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheACListeKPFsh(JObject param)
        {
            try
            {
                string infixText = param["infixText"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheACListeKPFsh(infixText, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage eksitonLLogNeKetePrind(JObject param)
        {
            try
            {
                var col = param["col"].ToObject<DbCore.DbKontabiliteti.colTrupPasqyreFinaciare>();
                string lloj = param["lloj"].Value<string>();
                string prind = param["prind"].Value<string>();
                int index = param["index"].Value<int>();
                string kod = param["kod"].Value<string>();
                string tip = param["tip"].Value<string>();
                string gjendje = param["gjendje"].Value<string>();
                int idndermarje = param["idndermarje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.eksitonLLogNeKetePrind(col, lloj, prind, index, kod, tip, gjendje, idndermarje));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraKPF(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraKPF(idja, rreshti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraKPFMeKod(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraKPFMeKod(kodi, rreshti, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraLlogMeIDRow(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheVleraLlogMeIDRow(idja, rreshti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage KtheVleraLlogarishMeIDRow(JObject param)
        {
            try
            {
                string idLlogarish = param["idLlogarish"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheVleraLlogarishMeIDRow(idLlogarish.Substring(0, idLlogarish.Length - 1), rreshti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheVleraLlogMeKodRow(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheVleraLlogMeKodRow(kodi, rreshti, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheIdLlogNgaNumri(JObject param)
        {
            try
            {
                string nrLlogInv = param["nrLlogInv"].Value<string>();
                string nrLlogBle = param["nrLlogBle"].Value<string>();
                string nrLlogShit = param["nrLlogShit"].Value<string>();
                string nrLlogTret = param["nrLlogTret"].Value<string>();
                string nrLlogShpenz = param["nrLlogShpenz"].Value<string>();
                string nrLlogAmort = param["nrLlogAmort"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheIdLlogNgaNumri(nrLlogInv, nrLlogBle, nrLlogShit, nrLlogTret, nrLlogShpenz, nrLlogAmort, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheLlojeKurseshPerMonedhe(JObject param)
        {
            try
            {
                int id = param["id"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheLlojeKurseshPerMonedhe(id));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKodVlereMinimum(JObject param)
        {
            try
            {
                String vlera = param["vlera"].Value<String>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheIdKushtiMinimumShitje(vlera));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheVlereLlogariBuxheti(JObject param)
        {
            try
            {
                string vlera = param["vlera"].Value<String>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheIdKonfigAmbjentiSipasVleresSeKushtitMultiselectLupa(vlera));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheNiveleSipasKat(JObject param)
        {
            try
            {
                string kategoria = param["kategoria"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheNiveleSipasKat(kategoria, idNdermarrje, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrPershkrimMenyreMesazhi(JObject param)
        {
            try
            {
                int idmenyre = param["idmenyre"].Value<int>();
                int idGjuha = param["idGjuha"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrPershkrimMenyreMesazhi(idmenyre, idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrKVpjesetimKMK(JObject param)
        {
            try
            {
                string monedhakryesor = param["monedhakryesor"].Value<string>();
                string monedhalidhes = param["monedhalidhes"].Value<string>();
                string date = param["date"].Value<string>();
                double kursdoklidhes = param["kursdoklidhes"].Value<double>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrKVpjesetimKMK(monedhakryesor, monedhalidhes, date, kursdoklidhes, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheLlogariSipasKodit(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int idNdermarja = param["idNdermarja"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheLlogariSipasKodit(kodi, idNdermarja));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraAqtMeKodOseKodBar(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                string magazina = param["magazina"].Value<string>();
                int idndermarje = param["idndermarje"].Value<int>();
                int idperdoruesi = param["idperdoruesi"].Value<int>();
                bool rezerva = param["rezerva"].Value<bool>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraAqtMeKodOseKodBar(kodi, rreshti, data, magazina, idndermarje, idperdoruesi, rezerva));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheRowVleraAQTMeID(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int idmagazina = param["idmagazina"].Value<int>();
                int idndermarja = param["idndermarja"].Value<int>();
                bool rezerva = param["rezerva"].Value<bool>();
                bool plotesuarMagKoka = param["plotesuarMagKoka"].Value<bool>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheRowVleraAQTMeID(idja, rreshti, data, idmagazina, idndermarja, rezerva, plotesuarMagKoka));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheVleraArtRez(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                int iddetajim = param["iddetajim"].Value<int>();
                int idmag = param["idmag"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheVleraArtRez(idja, rreshti, data, iddetajim, idmag));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage mbushInfoArtikulliLidhurNew(JObject param)
        {
            try
            {
                int idja = param["idja"].Value<int>();
                string mag = param["mag"].Value<string>();
                DateTime data = param["data"].Value<DateTime>();
                string detajim = param["detajim"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                string modinfo = param["modinfo"].Value<string>();
                int idklient = param["idklient"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.mbushInfoArtikulliLidhurNew(idja, mag, data, detajim, rreshti, modinfo, idklient, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage callWsGetAutorizimeArtikulli(JObject param)
        {
            try
            {
                int idArtikulli = param["idArtikulli"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.callWsGetAutorizimeArtikulli(idArtikulli));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKodArtikulliSipasId(JObject param)
        {
            try
            {
                int idArtikulli = param["idArtikulli"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKodArtikulliSipasId(idArtikulli));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKlientFurnitorVeprimeKFMeIDRow(JObject param)
        {
            try
            {
                int IDkf = param["IDkf"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime date = param["date"].Value<DateTime>();
                int llojKursi = param["llojKursi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKlientFurnitorVeprimeKFMeIDRow(IDkf, rreshti, date, llojKursi, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheDisaKlientFurnitorVeprimeKFMeIDRow(JObject param)
        {
            try
            {
                string IDteKf = param["IDteKf"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime date = param["date"].Value<DateTime>();
                int llojKursi = param["llojKursi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheDisaKlientFurnitorVeprimeKFMeIDRow(IDteKf, rreshti, date, llojKursi, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage callWsGetAutorizimeSipasLlojitDheIdLidhese(JObject param)
        {
            try
            {
                int idLidhese = param["idLidhese"].Value<int>();
                string kodLloji = param["kodLloji"].Value<string>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.callWsGetAutorizimeSipasLlojitDheIdLidhese(idLidhese, kodLloji, idPerdorues));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheVlerenEShfaqesSeEinvoice(JObject param)
        {
            try
            {
                int idBanka = param["idBanka"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheVlerenEShfaqesSeEinvoice(idBanka));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrTipinEPerjashtimit(JObject param)
        {
            try
            {
                int idTakse = param["idTakse"].Value<int>();
                
                return Request.KthePergjigje(RregjistrimeRepository.merrTipinEPerjashtimit(idTakse));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrKodNjesieBiznesiDegeAdministrative(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                if(clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                    return Request.KthePergjigje(RregjistrimeRepository.merrKodNjesieBiznesiDegeAdministrative(idNdermarrje, kodi));
                else
                {
                    return Request.KthePergjigje(new { kodNjesieBiznesi = "" });
                }
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrTipiMagDheQyteti(JObject param)
        {
            try
            {
                int idNjesiAdm = param["idNjesi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrTipiMagDheQyteti(idNjesiAdm));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheKlientFurnitorVeprimeKFRow(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheKlientFurnitorVeprimeKFRow(kodi, rreshti, data, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage DergoNdryshimStatusiEinvoice(JObject param)
        {
            try
            {

                string eic = param["EIC"].Value<string>();
                string statusi = param["statusi"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>(); 

                bool pergjigje = RregjistrimeRepository.NdryshoMesazhEinvoice(eic, statusi, idNdermarrje);
                if (!pergjigje)
                {
                    return Request.KthePergjigje(false);
                }
                else
                    return Request.KthePergjigje(true);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }
        //pjesa qe shtova uneee
        public HttpResponseMessage merrEinvoiceEIC(JObject param)
        {
            try
            {

                string eic = param["EIC"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
                string[] pergjigje = RregjistrimeRepository.merrEinvoiceEIC(eic, nderm);
                return Request.KthePergjigje(pergjigje);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrQendraKosto(JObject param)
        {
            try
            {
                int lloji = param["lloji"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.merrQendraKosto(lloji, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrInfoComboLlogariByID(JObject param)
        {
            try
            {
                int idLlogari = param["idLlogari"].Value<int>();
                string kodKontrolli = param["kodKontrolli"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.merrInfoComboLlogariByID(idLlogari, kodKontrolli));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrInfoComboLlogariByKod(JObject param)
        {
            try
            {
                string kodLlogari = param["kodLlogari"].Value<string>();
                string kodKontrolli = param["kodKontrolli"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.merrInfoComboLlogariByKod(kodLlogari, kodKontrolli, Session));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrInfoComboSkemaArtikulliByID(JObject param)
        {
            try
            {
                int idSkema = param["idSkema"].Value<int>();
                string kodKontrolli = param["kodKontrolli"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.merrInfoComboSkemaArtikulliByID(idSkema, kodKontrolli));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage MerrIdKlientiByTakimi(JObject param)
        {
            try
            {
                int idTakimi = param["idTakimi"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.MerrIdKlientiByTakimi(idTakimi));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheIdSuperKategori(JObject param)
        {
            try
            {
                string idKategoria = param["idKategoria"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheIdSuperKategori(idKategoria));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheMarreveshjeKlienti(JObject param)
        {
            try
            {
                int idKlienti = param["idKlienti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheMarreveshjeKlienti(idKlienti));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheImazheArkive(JObject param)
        {
            try
            {
                int idEntitet = param["idEntitet"].Value<int>();
                int idKategoria = param["idKategoria"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheImazheArkive(idEntitet, idKategoria));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrTeDhenaKonfigurimiVeprimeBanka(JObject param)
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
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                string status = param["status"].Value<string>();
                int idkokashitje = param["idkokashitje"].Value<int>();
                int idlloji = param["idlloji"].Value<int>();
                int rreshti = param["rreshti"].Value<int>();
                DateTime data = param["data"].Value<DateTime>().ToLocalTime();
                int idArkaBankaLocalStorage = param["idArkaBankaLocalStorage"].Value<int>();
                int idArkaBankaFillestareLocalStorage = param["idArkaBankaFillestareLocalStorage"].Value<int>();
                int idKonfigurimi = param["idKonfigurimi"].Value<int>();
                string EmertimiKF = param["EmertimiKF"].Value<string>();
                bool LlojiKF = param["LlojiKF"].Value<bool>();
                DateTime dataKurs = param["dataKurs"].Value<DateTime>();
                return Request.KthePergjigje(RregjistrimeRepository.merrTeDhenaKonfigurimiVeprimeBanka(idKomp, idKonfigurimi, kodKonf, kodKontrolli, idObjekti, shtim, merrFormatKursi, merrGjitheKonf, idGjuha, Session, idNdermarrje, idPerdoruesi, status, idkokashitje, idlloji, rreshti, data, idArkaBankaLocalStorage, idArkaBankaFillestareLocalStorage, EmertimiKF, LlojiKF, dataKurs));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage ktheAplikohetTVSHNeTakseApoJo(JObject param)
        {
            try
            {
                string kodTakse = param["kodTakse"].Value<string>();
                int idNderm = param["idNderm"].Value<int>();
                int key = param["key"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheAplikohetTVSHNeTakseApoJo(kodTakse, idNderm, key));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage SaveFiles()
        {
            var content = Request.Content;
            // var conn = Request.Content.ReadAsStreamAsync().Result;
            //testc(content.ReadAsStringAsync().Result);

            var parma = System.Web.HttpContext.Current.Request.Params["qquuid"];
            var f = (System.Web.HttpContext.Current.Request.Files["qqfile"]);



            var vlerat = (new StreamReader((System.Web.HttpContext.Current.Request.Files["qqfile"]).InputStream)).ReadToEnd();
            //string jsonContent = content.ReadAsStringAsync().Result;
            var response = new { success = true };
            return Request.CreateResponse(
            System.Net.HttpStatusCode.OK,
            response,
            JsonMediaTypeFormatter.DefaultMediaType
        );

        }


        public HttpResponseMessage merrKursFatureOseAzhornimiPerIdDok(JObject param)
        {
            try
            {
                string ids = param["ids"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.merrKursFatureOseAzhornimiPerIdDok(ids));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage NgarkoFile()
        {

            try
            {
                var content = Request.Content;
                var idFile = System.Web.HttpContext.Current.Request.Params["qquuid"];
                var llojFile = System.Web.HttpContext.Current.Request.Params["lloji"];
                var f = (System.Web.HttpContext.Current.Request.Files["qqfile"]);
                if (f != null)
                    RregjistrimeRepository.ruajFileImportiCache(idFile, f, Session, llojFile);
                var response = new { success = true };
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, response, JsonMediaTypeFormatter.DefaultMediaType);
            }
            catch (Exception e)
            {
                var response = new { error = e.Message };
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, response, JsonMediaTypeFormatter.DefaultMediaType);
            }

        }


        [HttpDelete]
        public HttpResponseMessage FshiFile()
        {
            try
            {
                var idFile = Request.RequestUri.Segments[4];
                var llojFile = System.Web.HttpContext.Current.Request.Params["lloji"];
                RregjistrimeRepository.fshiFileImportiCache(idFile, llojFile, Session);
                var response = new { success = true };
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, response, JsonMediaTypeFormatter.DefaultMediaType);
            }
            catch (Exception e)
            {
                var response = new { error = e.Message };
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, response, JsonMediaTypeFormatter.DefaultMediaType);
            }
        }

        public HttpResponseMessage FshiDokument(JObject param)
        {
            try
            {
                int[] ids = param["ids"].ToObject<int[]>();
                string komponente = param["komponente"].Value<string>();
                string guidString = param["guidString"].Value<string>();
                string komponShitje_blerje = param["komponShitje_blerje"].Value<string>();
                string komponPerTedrejtat = param["komponPerTedrejtat"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.FshiDokument(Session, ids, komponente, guidString, komponShitje_blerje, komponPerTedrejtat));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        //  
        [HttpPost, HttpGet]
        public HttpResponseMessage RaporteMeFormatePrintimi(JObject param)
        {
            try
            {
                int idGjuha = param["idGjuha"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheRaportetMeFormatePrintimi(idGjuha));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage BejRivleresim(JObject param)
        {
            try
            {
                string guidString = param["guidString"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.BejRivleresim(Session, guidString));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheTeDhenaFaturashSipasId(JObject param)
        {
            try
            {
                int idKatDok = param["idKatDok"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idDokumenti = param["idDokumenti"].Value<int>();
                int idNiveli = param["idNiveli"].Value<int>();
                string faturat = param["faturat"].ToString();
                int idkonfigurim = param["idkonfigurim"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.ktheTeDhenaFaturashSipasId1(faturat, idKatDok, idPerdoruesi, idNdermarrje, idDokumenti, idNiveli, idkonfigurim));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage DizajneSipasRaportit(JObject param)
        {
            try
            {
                int idRaporti = param["idRaporti"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheDizajneSipasRaportit(idRaporti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage merrNdermarrjeTePalidhura(JObject param)
        {
            try
            {

                int idDesign = param["idDesign"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheNdermarrjetEPalidhura(idDesign));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpGet, HttpPost]
        public HttpResponseMessage lidhNdermarrjetEZgjedhuraMeFormatin(JObject param)
        {
            try
            {
                string idTeNdermarrjeve = param["ndermarrjet"].Value<string>();
                int idDesign = param["idDesign"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.LidhNdermarjetMeFormatin(idTeNdermarrjeve, idDesign));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }

        }

        [HttpPost, HttpGet]
        public HttpResponseMessage MerrNdermarrjetIdRoli(JObject param)
        {
            try
            {
                var idRoli = param["idRoli"].Value<int>();
                var guidString = param["guidString"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.MerrNdermarrjetIdRoli(Session, guidString, idRoli));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage RuajNdermarrjeRolNeSession(JObject param)
        {
            try
            {
                var selected = param["selected"].ToObject<string[]>();
                var guidString = param["guidString"].Value<string>();
                var idRoli = param["idRoli"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.RuajNdermarrjeRolNeSession(Session, guidString, idRoli, selected));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage ktheTeDhenaPerKlientFurnitor(JObject param)
        {
            try
            {
                int idobj = param["idObjekti"].Value<int>();
                string kodlloji = param["kodLloji"].Value<string>();
                int idkomp = param["idkomp"].Value<int>();
                string kodkonfig = param["kodkonfig"].ToString();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idGjuha = param["idGjuha"].Value<int>();

                return Request.KthePergjigje(RregjistrimeRepository.ktheTeDhenaPerKlientFurnitor(idobj, kodlloji, idkomp, kodkonfig, idNdermarrje, idGjuha,0));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage FshiDokumentMagazine(JObject param)
        {
            try
            {
                int[] ids = param["ids"].ToObject<int[]>();
                string komponente = param["komponente"].Value<string>();
                string guidString = param["guidString"].Value<string>();
                int periudhaIdViti = param["periudhaIdViti"].Value<int>();
                string periudhaDok = param["periudhaDok"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.FshiDokumentMagazine(Session, ids, komponente, guidString, periudhaIdViti, periudhaDok));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage kontrolloEkzistojneDokQePoKonvertohenSipasIdkoka(JObject param)
        {
            try
            {
                string idshtije = string.Join(",", param["idshitje"]);
                string idmag = string.Join(",", param["idmag"]);
                string idrez = string.Join(",", param["idrez"]);
                return Request.KthePergjigje(RregjistrimeRepository.kontrolloEkzistojneDokQePoKonvertohenSipasIdkoka(idshtije, idmag, idrez));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage KontrolloNqsKaTeDhenaNeGride(JObject param)
        {
            try
            {
                var llojiNv = param["llojiNv"].Value<string>();
                var index = param["index"].Value<int>();
                var idNdermarrjeVit = param["idNdermarrjeVit"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KrahasimiNqsGridaKaTeDhena(Session, index, llojiNv, idNdermarrjeVit));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage gjejRowSipasIdFaturaNgaDSGrides(JObject param)
        {
            try
            {

                var idndermarrje = param["idNdermarrje"].Value<int>();
                var komponente = param["komponente"].Value<string>();
                var idKatDokShitje = param["idKatDokShitje"].Value<int>();
                var idDokumenti = param["idDokumenti"].ToObject<string[]>();
                var pageId = param["pageId"].Value<string>();
                var keyFieldName = param["keyFieldName"].Value<string>();
                string fields = param["fields"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.gjejRowSipasIdFaturaNgaDSGrides(Session, idDokumenti, komponente, idndermarrje, idKatDokShitje, pageId, keyFieldName, fields));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage KaGaranciShitja(JObject param)
        {
            try
            {
                var idShitjeKoka = param["idShitjeKoka"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KaGaranciShitja(idShitjeKoka));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        public HttpResponseMessage RuajLidhjeDetajim(JObject param)
        {
            try
            {
                string kodartikulli = param["kodartikulli"].Value<string>();
                string kodi = param["kodi"].Value<string>();
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int lloji = param["lloji"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.RuajLidhjeDetajim(kodartikulli, kodi, idNdermarrje, lloji, idPerdorues));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage KtheKlientFurnitorSipasKoditLike(JObject param)
        {
            try
            {
                var kodiKlientFurnitor = param["kodiKlientFurnitor"].Value<string>();
                var kodModeli = param["kodModeli"].Value<string>();
                var idNdermarrje = param["idNdermarrje"].Value<int>();
                var idPerdoruesi = param["idPerdoruesi"].Value<int>();
                var idGjuha = param["idGjuha"].Value<int>();
                var idKlientFurnitorKryesor = param["idKlientFurnitorKryesor"].Value<int>();
                var idKonfigLupaKf = param["idKonfigLupaKf"].Value<int>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheKlientFurnitorSipasKoditLike(kodiKlientFurnitor, kodModeli, idNdermarrje, idPerdoruesi, idGjuha, idKlientFurnitorKryesor, idKonfigLupaKf));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage KtheKlientFurnitorSipasIdve(JObject param)
        {
            try
            {
                var ids = param["ids"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.KtheKlientFurnitorSipasIdve(ids));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage NgarkoMarreveshje(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                DateTime dtdok = param["dtdok"].Value<DateTime>().ToLocalTime();
                return Request.CreateResponse(RregjistrimeRepository.NgarkoMarreveshje(Session, idNdermarrje, dtdok));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }

        }
        public HttpResponseMessage KerkoMarreveshje(JObject param)
        {
            try
            {
                string idMarreveshje = param["idMarreveshje"].Value<string>();
                DateTime dtdok = param["dtdok"].Value<DateTime>().ToLocalTime();
                return Request.CreateResponse(RregjistrimeRepository.KerkoMarreveshje(idMarreveshje, dtdok));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }

        }

        public HttpResponseMessage CelDheLidhDetajimMeArtikull(JObject param)
        {
            try
            {
                int idArtikulli = param["idArtikulli"].Value<int>();
                int detajimPareApoDyte = param["detajimPareApoDyte"].Value<int>();
                int llojDetajimi = param["llojDetajimi"].Value<int>();
                int kategoriDetajimi = param["kategoriDetajimi"].Value<int>();
                string kodDetajimi = param["kodDetajimi"].Value<string>();

                return Request.CreateResponse(RregjistrimeRepository.CelDheLidhDetajimMeArtikull(idArtikulli, detajimPareApoDyte, llojDetajimi, kategoriDetajimi, kodDetajimi, Session));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }

        public HttpResponseMessage MerrArtikujAutoComplete(JObject param)
        {
            try
            {
                var kodi = param["kodi"].Value<string>();
                return Request.CreateResponse(RregjistrimeRepository.MerrArtikujAutoComplete(Session, kodi));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }

        public HttpResponseMessage MerrKodifikim1AutoComplete(JObject param)
        {
            try
            {
                var kodi = param["kodi"].Value<string>();
                return Request.CreateResponse(RregjistrimeRepository.MerrKodifikim1AutoComplete(Session, kodi));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }

        public HttpResponseMessage MerrNivelZbritjeAutoComplete(JObject param)
        {
            try
            {
                var kodi = param["kodi"].Value<string>();
                return Request.CreateResponse(RregjistrimeRepository.MerrNivelZbritjeAutoComplete(Session, kodi));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);

            }
        }

        public HttpResponseMessage MerrLimitet(JObject param)
        {
            try
            {
                var idKarta = param["idKarta"].Value<int>();
                return Request.CreateResponse(RregjistrimeRepository.MerrLimitet(Session, idKarta));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage VeprimeArkaBanka_DergoFatureMeEmail(JObject param)
        {
            try
            {
                string idsKokaDok = String.Join(",", param["idsKokaDok"]);
                return Request.CreateResponse(RregjistrimeRepository.VeprimeArkaBanka_DergoFatureMeEmail(idsKokaDok, Session));
            }
            catch(Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }


        [HttpGet, HttpPost]
        public HttpResponseMessage kthePiketNeModifikimTeVFONE(JObject param)
        {
            try
            {
                int idDok = param["idDok"].Value<int>();
                return Request.CreateResponse(RregjistrimeRepository.kthePiketNeModifikimTeVFONE(idDok, Session));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }


        [HttpGet, HttpPost]
        public HttpResponseMessage eshtePrindQenderKosto(JObject param)
        {
            try
            {
                int idNdermarrje = param["idNdermarrje"].Value<int>(); 
                string Kodi = param["Kodi"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.merrPrindQenderKosto(idNdermarrje, Kodi));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpGet, HttpPost]
        public HttpResponseMessage restoreDatabase(JObject param)
        {
            try
            {
                string prefix = param["uri"].Value<string>();
                return Request.KthePergjigje(RregjistrimeRepository.restoreDatabase(prefix));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpGet, HttpPost]
        public void destroySession(JObject param)
        {
            try
            {
                RregjistrimeRepository.logout(Session);
            }
            catch (Exception e)
            {
                //RregjistrimeRepository.logout(Session);
            }
        }
    }
}