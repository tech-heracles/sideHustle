using DbCore;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbListPagesat;
using DbCore.DbQendraKosto;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using DbCore.DbRegjistrim;
using System.Resources;
using DbCore.DbListPagesat.Helpers;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using System.Reflection;
using DbCore.IMBUtils.Messages;
using Newtonsoft.Json;
using DbCore.IMBUtils.DataBase;

namespace RestApi.WebAPI.Models
{
    /// <summary>
    ///     ne kete klase ndodhen metoda te cilat kryejne llogaritje per interes te ws te modulit te listpagesave
    /// </summary>
    public class ListPagesaRepository
    {
        #region PUBLIC API

        public static colKompListPagese merrVleratPerPunonjes(DateTime data, int idNdermarrje, int idPunonjes, bool heraPare, HttpSessionState session)
        {
            colKompListPagese colPerPunonjes = null;

            if (!heraPare)
                colPerPunonjes = (colKompListPagese)mySessionObjects.merrObjectNgaSesioni(session, "colListPagesa");

            if (colPerPunonjes != null) return colPerPunonjes;
            colPerPunonjes = new colKompListPagese(true, data, idNdermarrje, idPunonjes);
            mySessionObjects.ruajObjectNeSesion(session, colPerPunonjes, "colListPagesa");

            return colPerPunonjes;
        }

        public static object LlogaritPagePerPunonjesPerMuajin(int idpunonjes, DateTime data, string[] arrkodi, decimal[] arrparam, bool[] arrmosndrysho, decimal[] arrvlera, bool llogariDP, int muaji, bool ditemuajindryshueshme, string kodi, bool vjenNgaMuajt, bool ditemuaji, bool pagemuaji, int muajitjeter, int vititjeter, bool merrimporte, string kodimporti, bool vjenNgaKomponentja, bool newrecord, bool llogaritDiteLejeNgaImporti, HttpSessionState Session, int idNdermarrjeVit, int idGjuha, int idNdermarrje, int idKokaLp, bool merrVlereDefault)
        {
            DateTime datePage = pagemuaji ? Utils.MerrDatenFunditTeMuajit(vititjeter, muajitjeter) : data;
            if ((muaji != muajitjeter || data.Year != vititjeter) && ditemuaji)
            {
                data = Utils.MerrDatenFunditTeMuajit(vititjeter, muajitjeter);
                muaji = muajitjeter;
            }
            var result = LlogaritPagePerPunonjes(idpunonjes, data, datePage, arrkodi, arrparam, arrmosndrysho, arrvlera, llogariDP, muaji, ditemuajindryshueshme, kodi, vjenNgaMuajt, ditemuaji, pagemuaji, muajitjeter, vititjeter, merrimporte, kodimporti, vjenNgaKomponentja, newrecord, llogaritDiteLejeNgaImporti, Session, idNdermarrjeVit, idGjuha, idNdermarrje, idKokaLp, merrVlereDefault, false);
            return new
            {
                Mesazhet = result.mesazhetPerPunonjes,
                Komponentet = result == null || result.data == null ? new colKomponentePage() : result.data.ColKomponente
            };
        }

        /// <summary>
        /// llogarit pagen per nje punonjes ne rastin kur eshte hapur lupa e komponenteve
        /// </summary>
        /// <param name="idpunonjes"></param>
        /// <param name="data"></param>
        /// <param name="arrkodi"></param>
        /// <param name="arrparam"></param>
        /// <param name="arrmosndrysho"></param>
        /// <param name="arrvlera"></param>
        /// <param name="llogariDP"></param>
        /// <param name="muaji"></param>
        /// <param name="ditemuajindryshueshme"></param>
        /// <param name="kodi"></param>
        /// <param name="vjenNgaMuajt"></param>
        /// <param name="ditemuaji"></param>
        /// <param name="pagemuaji"></param>
        /// <param name="muajitjeter"></param>
        /// <param name="vititjeter"></param>
        /// <param name="merrimporte"></param>
        /// <param name="kodimporti"></param>
        /// <param name="vjenNgaKomponentja"></param>
        /// <param name="newrecord"></param>
        /// <param name="llogaritDiteLejeNgaImporti"></param>
        /// <param name="session"></param>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="idGjuhe"></param>
        /// <param name="nrpersonal"></param>
        /// <param name="emri"></param>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idKokaLp">id e dokumentit te LP</param>
        /// <returns></returns>
        public static dynamic LlogaritPagePerPunonjes(int idpunonjes, DateTime data, DateTime datePage, string[] arrkodi, decimal[] arrparam, bool[] arrmosndrysho, decimal[] arrvlera, bool llogariDP, int muaji, bool ditemuajindryshueshme, string kodi, bool vjenNgaMuajt, bool ditemuaji, bool pagemuaji, int muajitjeter, int vititjeter, bool merrimporte, string kodimporti, bool vjenNgaKomponentja, bool newrecord, bool llogaritDiteLejeNgaImporti, HttpSessionState session, int idNdermarrjeVit, int idGjuha, int idNdermarrje, int idKokaLp, bool merrVlereDefault, bool buttonClickNgaKomponente)
        {
            var mesazhetPerPunonjes = new Dictionary<string, string>(1);
            object result = null;
            decimal diteTeHarxhura = 0;
            clsNdermarrje ndermarrje = null;

            var punfundit = new clsPunesim();

            colKomponenteNr allKompNrPunonjesi = null;
            clsSigurimet sigurimet = null;
            clsSkemaSigurimi skemasig = null;
            clsMonedha monNder = null;
            clsKurset kursinderm = null;
            colTatimet tatimet = new colTatimet();
            clsKurset kursiMonedhesSePunonjesit = null;
            decimal diteLeje = 0;
            colOreShtese allOreShtesePunonjesi = null;
            List<PunonjesMeData> punonjesMeData = null;
            Dictionary<DateTime, colKompListPagese> colPagaMeData = null;
            colPagaShtesa pagaShtesaMeData = null;
            DataTable listorare = null;
            colKompListPagese colkomplistpage = null;

            var punonjesi = new clsPunonjes(idpunonjes);
            var currentContext = HttpContext.Current;
            try
            {
                Parallel.Invoke(new ParallelOptions { MaxDegreeOfParallelism = 2 }, () =>
                     {
                         HttpContext.Current = currentContext;
                         allOreShtesePunonjesi = new colOreShtese(new List<int> { punonjesi.IdPunonjes }, data.Month, data.Year, idKokaLp);
                         punonjesMeData = MerrPunonjesMeData(data, new List<int> { idpunonjes }, allOreShtesePunonjesi, pagemuaji, datePage, muajitjeter, vititjeter);

                         ndermarrje = new clsNdermarrje(idNdermarrje);
                     }, () =>
                     {
                         HttpContext.Current = currentContext;
                         allKompNrPunonjesi = new colKomponenteNr(new List<int> { punonjesi.IdPunonjes }, data.Month, data.Year, idKokaLp);
                         diteTeHarxhura = clsKompListPagese.merrDiteTeHarxhuara(idpunonjes, data, idNdermarrjeVit);
                     });

                Parallel.Invoke(new ParallelOptions { MaxDegreeOfParallelism = 5 }, () =>
                {
                    HttpContext.Current = currentContext;
                    punfundit.merrPunesimFundit(punonjesi.IdPunonjes, data);
                    skemasig = new clsSkemaSigurimi(); skemasig.merrSipasIdPunonjesDheDataDeri(punonjesi.IdPunonjes, data);
                    sigurimet = new clsSigurimet(skemasig.IdSigurimi);
                    colkomplistpage = new colKompListPagese(idpunonjes, data, idNdermarrje);
                }, () =>
                {
                    HttpContext.Current = currentContext;
                    monNder = new clsMonedha(); monNder.mbushMonedhen("LEK", idNdermarrje); //kursi i lekut ne kete ndermarje
                    kursinderm = new clsKurset(monNder.IdMonedha, data);
                    tatimet.merrTatimeNdermarjeSipasDatesDeri(idNdermarrje, data);
                    kursiMonedhesSePunonjesit = new clsKurset(punonjesi.IdMonedha, data);
                    diteLeje = colDiteLeje.ktheGjitheDiteLejeSipasPunonjesitDhePeriudhes(idpunonjes, muaji.ToString(), data.Year, idKokaLp);

                    //nqs e kemi te chekuar llogarit nga list oraret e importuara marrim te dhenat e importuara per muajin e zgjdhur tek listpagesat
                    if (punonjesi.LlogaritNgaListorare)
                        listorare = colListOrare.ktheGjitheListOrariSipasPunonjesitSipasPeriudhes(punonjesi.IdPunonjes, new DateTime(data.Year, muaji, 1), new DateTime(data.Year, muaji, DateTime.DaysInMonth(data.Year, muaji)));//hiqet
                }, () =>
                {
                    HttpContext.Current = currentContext;
                    colPagaShtesa.merrPagaShtesaSipasPunonjesveDheDatesMeTeFundit(1, punonjesMeData).TryGetValue(idpunonjes, out pagaShtesaMeData);
                    if (pagaShtesaMeData == null) return; //to log
                    var kompPageIds = pagaShtesaMeData.Select(x => x.IdKomponentePage).Distinct().ToList();
                    var colKompPage = new colKomponentePage(kompPageIds);
                    pagaShtesaMeData.AsParallel().ForAll(x => x.KomponentePage = colKompPage.FirstOrDefault(komp => x.IdKomponentePage == komp.IdKomponentePage)?.Clone());
                }, () =>
                {
                    HttpContext.Current = currentContext;
                    var dicColPaga = colKompListPagese.MerrDicKomponenteshPerPunonjesitSipasDataveBasicAsParallel(true, data, idNdermarrje, punonjesMeData, true, 4);
                    if (!dicColPaga.TryGetValue(idpunonjes, out colPagaMeData))
                        return;
                    var allKompLp = colPagaMeData.SelectMany(x => x.Value).ToList();
                    var kompPageIds = allKompLp.Select(x => x.IdKomponentePage).Distinct().ToList();
                    var colKompPage = new colKomponentePage(kompPageIds);
                    allKompLp.AsParallel().ForAll(x => x.komponentePage = colKompPage.FirstOrDefault(komp => x.IdKomponentePage == komp.IdKomponentePage)?.Clone());

                    colKompListPagese colpaga;
                    if (!colPagaMeData.TryGetValue(data.Date, out colpaga))
                        throw new MyException($"Per kete punonjes nuk ka komponente listpagese per daten {data.Date}");
                    arrkodi = Utils.MerrArrayMeKodeKomponenteTeRejaDheTeVjetra(arrkodi, colpaga);
                    Array.Resize(ref arrparam, arrkodi.Length);
                    Array.Resize(ref arrmosndrysho, arrkodi.Length);
                    Array.Resize(ref arrvlera, arrkodi.Length);
                });
                if(punfundit.IdPunesim == 0)
                {
                    ImbLogger.Error($"Nuk ka nje punesim aktiv per punonjesin {punonjesi.NrPersonal} ne daten {data}");
                    throw new MyException(MessagesResource.Messages["msgPunonjesNukMundTeBeniVeprime"]);
                }
                if (colPagaMeData == null || colPagaMeData.Count == 0 || colkomplistpage == null || colkomplistpage.Count == 0)
                {
                    throw new MyException("nuk ka komponente per punonjesin {0} ne daten {1}", punonjesi.NrPersonal, data);
                }

                //nese e rishton pastro vlerat e vjetra
                var vleraMujore = (!vjenNgaKomponentja && newrecord) ? new Dictionary<string, colKomponenteMuaji>() : Utils.MerrVleratMujoreTeNjePunonjesi(session, idpunonjes);
                var muajTeLlogaritur = new colMuajTeLlogariturTrack
                {
                    new MuajTeLlogariturTrack  { muaji=muaji,  llogaritur=false  }
                };

                result = llogaritPage(arrvlera, arrparam, arrkodi, arrmosndrysho, data,datePage, llogariDP, punonjesi.NrPersonal, $"{punonjesi.Emer}  {punonjesi.Mbiemer}", muaji, ditemuajindryshueshme, kodi, vjenNgaMuajt, ditemuaji, pagemuaji, muajitjeter, vititjeter, merrimporte, kodimporti, vjenNgaKomponentja, newrecord, llogaritDiteLejeNgaImporti, idNdermarrje, kursinderm, monNder, punonjesi, tatimet, ndermarrje, skemasig, sigurimet, colPagaMeData, colkomplistpage, punfundit, diteTeHarxhura, kursiMonedhesSePunonjesit, listorare, allOreShtesePunonjesi, allKompNrPunonjesi, pagaShtesaMeData, idGjuha, diteLeje, ref vleraMujore, muajTeLlogaritur, merrVlereDefault, data, false, buttonClickNgaKomponente, 0, 0, null);
                if (!vjenNgaMuajt)
                    Utils.RuajVleratMujoreTeNjePunonjesiNeSession(session, vleraMujore, idpunonjes);
            }
            catch (Exception ex)
            {

                mesazhetPerPunonjes.Add(punonjesi.NrPersonal, ex.Message);
                ImbLogger.Error(ex);
            }

            return new
            {
                data = result,
                mesazhetPerPunonjes
            };
        }

        public static clsMesazh DiscardNdryshimetPerMuajt(HttpSessionState session, int idPunonjesi)
        {
            var vleratOrigjinale = Utils.MerrVleratMujoreTeNjePunonjesiOrigjinale(session, idPunonjesi);
            Utils.RuajVleratMujoreTeNjePunonjesiNeSession(session, vleratOrigjinale, idPunonjesi);
            return new MesazhSuksesi("Ndryshimet u kthyen mbrapsh me sukses!");
        }
        public static clsMesazh RuajNdryshimetPerMuajt(HttpSessionState session, int idPunonjesi)
        {
            Utils.RuajVleratMujoreTeNjePunonjesiNeSessionOrigjinale(session, idPunonjesi);
            return new MesazhSuksesi("Ndryshimet u ruajten me sukses!");
        }
        /// <summary>
        /// metode e cila ben llogaritjet e pages per punonejsit e zjedhur nga lupa
        /// </summary>
        /// <param name="data"></param>
        /// <param name="DPZero"></param>
        /// <param name="nrDitesh"></param>
        /// <param name="idpunonjesish"></param>
        /// <param name="llogariDP"></param>
        /// <param name="muaji"></param>
        /// <param name="ditemuajindryshueshme"></param>
        /// <param name="kodi"></param>
        /// <param name="vjenNgaMuajt"></param>
        /// <param name="ditemuaji"></param>
        /// <param name="pagemuaji"></param>
        /// <param name="muajitjeter"></param>
        /// <param name="vititjeter"></param>
        /// <param name="merrimporte"></param>
        /// <param name="kodimporti"></param>
        /// <param name="vjenNgaKomponentja"></param>
        /// <param name="newrecord"></param>
        /// <param name="llogaritDiteLejeNgaImporti"></param>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static object LlogaritPagePerShumePunonjes(DateTime data, bool DPZero, double nrDitesh, List<int> idpunonjesish, bool llogariDP, int muaji, bool ditemuajindryshueshme, string kodi, bool vjenNgaMuajt, bool ditemuaji, bool pagemuaji, int muajitjeter, int vititjeter, bool merrimporte, string kodimporti, bool vjenNgaKomponentja, bool newrecord, bool llogaritDiteLejeNgaImporti, int idKokaLp, bool merrVlereDefault, int idNdermarrje, int idGjuha)
        {

            colPunonjes punonjesit = null;
            colKompListPagese colFillestare = null;
            string[] arrKodi = null;
            var currentContext = HttpContext.Current;

            Parallel.Invoke(() =>
            {
                HttpContext.Current = currentContext;
                colFillestare = new colKompListPagese(true, data, idNdermarrje, 0);
                arrKodi = Utils.KtheArrayMeKodeKomponentesh(colFillestare);
            },
            () =>
            {
                HttpContext.Current = currentContext;
                punonjesit = new colPunonjes(idpunonjesish, false, data);
            });

            var rezultati = LlogaritPerShumePunonjes(data, DPZero, nrDitesh, idpunonjesish, punonjesit, llogariDP, muaji, ditemuajindryshueshme, kodi, vjenNgaMuajt, ditemuaji, pagemuaji, muajitjeter, vititjeter, merrimporte, kodimporti, vjenNgaKomponentja, newrecord, llogaritDiteLejeNgaImporti, idNdermarrje, colFillestare, null, arrKodi, false, idGjuha, idKokaLp, null, true, merrVlereDefault);

            return new
            {
                colKompListPagesa = colFillestare,
                data = rezultati.Item1, //te dhenat e llogaritura
                mesazhetPerPunonjes = rezultati.Item2
            };
        }

        public static bool KaTeDhenaPerTeMarre(int id)
        {
            return clsKokaListPagese.KaTeDhenaPerTeMarre(id);
        }

        public static decimal merrVleraDefault(int muaj, int vit, int idpunonjes, string kod, bool heraPare, HttpSessionState Session, int idNdermarrje)
        {
            var colpaga = merrVleratPerPunonjes(new DateTime(vit, muaj, DateTime.DaysInMonth(vit, muaj)), idNdermarrje, idpunonjes, heraPare, Session);

            var first = colpaga.FirstOrDefault(x => x.KodKomponente == kod);
            return first?.Vlera ?? 0;
        }

        /// <summary>
        /// metoda e cila kryen rillogaritje te vlerave per komponentet e punonjesve qe jane ne trupin e nje LP
        /// </summary>
        /// <param name="colFillestareNgaTrupiLP"></param>
        /// <param name="DPZero"></param>
        /// <param name="data"></param>
        /// <param name="llogariDP"></param>
        /// <param name="personalNrs"></param>
        /// <param name="muaji"></param>
        /// <param name="ditemuajindryshueshme"></param>
        /// <param name="kodi"></param>
        /// <param name="vjenNgaMuajt"></param>
        /// <param name="ditemuaji"></param>
        /// <param name="pagemuaji"></param>
        /// <param name="muajitjeter"></param>
        /// <param name="vititjeter"></param>
        /// <param name="merrimporte"></param>
        /// <param name="kodimporti"></param>
        /// <param name="vjenNgaKomponentja"></param>
        /// <param name="newrecord"></param>
        /// <param name="llogaritDiteLejeNgaImporti"></param>
        /// <param name="idGjuha"></param>
        /// <param name="Session"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static object RimerrVlera(Dictionary<int, colKompListPagese> colFillestareNgaTrupiLP, bool DPZero, DateTime data, bool llogariDP, string[] personalNrs, int muaji, bool ditemuajindryshueshme, string kodi, bool vjenNgaMuajt, bool ditemuaji, bool pagemuaji, int muajitjeter, int vititjeter, bool merrimporte, string kodimporti, bool vjenNgaKomponentja, bool newrecord, bool llogaritDiteLejeNgaImporti, colPunonjes colPunonjesit, List<int> idPunonjesish, int idNdermarrje, int idGjuha, int idKokaLp, colPunesim punesimet, bool listPageseERe)
        {
            var rezultati = LlogaritPerShumePunonjes(data, DPZero, 0, idPunonjesish, colPunonjesit, llogariDP, muaji, ditemuajindryshueshme, kodi, vjenNgaMuajt, ditemuaji, pagemuaji, muajitjeter, vititjeter, merrimporte, kodimporti, vjenNgaKomponentja, newrecord, llogaritDiteLejeNgaImporti, idNdermarrje, null, colFillestareNgaTrupiLP, null, true, idGjuha, idKokaLp, punesimet, listPageseERe, false);
            return new
            {
                data = rezultati.Item1, //te dhenat e llogaritura
                mesazhetPerPunonjes = rezultati.Item2
            };
        }

        internal static bool fshiRaportNgaSessioni(HttpSessionState session)
        {
            return mySessionObjects.fshiRaportNgaSessioni(session);
        }

        public static clsGrupimeLocaleGlobale MerrPrindGrupimi(int idgrupilocal)
        {
            var gr = new clsGrupimeLocaleGlobale(idgrupilocal);
            var grprindi = new clsGrupimeLocaleGlobale(gr.IdPrindi);
            return grprindi;
        }

        public static clsQendraKosto MerrPrindQK(int idqk)
        {
            var qk = new clsQendraKosto(idqk);
            var qkprindi = new clsQendraKosto(qk.IdPrindi);
            return qkprindi;
        }

        public static colKategoriPage merrKategoriPage(int idllojpage, int idndermarje)
        {
            var col = new colKategoriPage(idndermarje, idllojpage);
            col.Insert(0, new clsKategoriPage());
            return col;
        }

        public static colSigurimeSuplementare merrSigurimeSuplementare(DateTime date, int idndermarje)
        {
            date = date.ToLocalTime();
            var col = new colSigurimeSuplementare();
            col.mbushGjitheSigurimeSuplementareSipasNdermarjesDheDates(idndermarje, date);
            return col;
        }

        public static colKomponentePage llogaritVlera(decimal[] arrvlera, decimal[] arrparam, string tipi, DateTime data, int idsigurimi, int idmonedha, HttpSessionState session, int idndermarje, int idPunonjes)
        {
            data = data.ToLocalTime();
            var colpaga = mySessionObjects.merrPageShteseNgaSesioni(session);
            var colkom = new colKomponentePage();
            var kompDitePune = new clsKomponentePage("DM", idndermarje, data);
            var vleraOd = 0 == idPunonjes ? 8 : clsKomponenteListPagesePunonjesi.MerrVlereDefaultPerKomponente("OD", idPunonjes, data);
            for (var i = 0; i < colpaga.Count; i++)
            {
                var komp = clsKomponentePage.MerrSipasIdNgaStaticCache(colpaga[i].IdKomponentePage);
                if (komp.Njesi == 2 && komp.ParamKodi != "")
                {
                    komp.Formula = ZevendesoParameter(komp.Formula, komp.ParamKodi, arrparam[i].ToString());
                }
                colkom.Add(komp);
            }
            var pbindex = -1;
            var pnindex = -1;
            var psindex = -1;
            decimal tb = 0;
            for (var i = 0; i < colkom.Count; i++)
            {
                if (colkom[i].Kodi == "PB")
                    pbindex = i;
                else if (colkom[i].Kodi == "PN")
                    pnindex = i;
                else if (colkom[i].Kodi == "PS")
                    psindex = i;
                else if (colkom[i].Kodi == "TB")
                    tb = arrvlera[i];
                else if (colkom[i].Kodi == "PNETO" && arrvlera[i] != 0)
                {
                    var pb = llogaritPageBazeNgaNeto(arrvlera[i], data, idsigurimi, idmonedha, idndermarje);
                    if (psindex >= 0)
                        colkom[psindex].Formula = pb.ToString();
                    if (pbindex >= 0)
                        colkom[pbindex].Formula = (pb - tb).ToString();
                    if (pnindex >= 0)
                    {
                        if (tipi == "Mujore")
                            colkom[pnindex].Formula = (pb - tb).ToString();
                        else if (tipi == "Ditore")
                        {
                            colkom[pnindex].Formula = ((pb - tb) / int.Parse(kompDitePune.Formula)).ToString();
                        }
                        else
                        {
                            colkom[pnindex].Formula = ((pb - tb) / (vleraOd * int.Parse(kompDitePune.Formula))).ToString();
                        }
                    }
                }
            }

            for (var i = 0; i < colkom.Count; i++)
            {
                if (colkom[i].Njesi != 2)
                    formula(colkom, colkom[i].Kodi, arrvlera[i].ToString(),false, null, 0, 0);
                else formula(colkom, colkom[i].Kodi, string.Format((string)"({0})", (object)colkom[i].Formula), false, null, 0, 0);
                if (colkom[i].Kodi == "PNETO" && arrvlera[i] != 0)
                {
                    continue;
                }
            }
            if (tipi == "Mujore")
                formula(colkom, "LP", "1", false, null, 0, 0);
            else if (tipi == "Ditore")
            {
                formula(colkom, "LP", kompDitePune.Formula, false, null, 0, 0);
            }
            else
            {
                formula(colkom, "LP", $"{vleraOd}*{kompDitePune.Formula}", false, null, 0, 0);
            }
            return colkom;
        }

        public static decimal llogaritPageBazeNgaNeto(decimal vlera, DateTime data, int idsigurimi, int idmonedha, int idndermarje)
        {
            //gjejme kursin e monedhes
            decimal normaFaktike = 1;
            decimal kursimon = 1;
            var kursi = new clsKurset(idmonedha, data);
            if (kursi.VleraKursi != 0)
                kursimon = Convert.ToDecimal((double)kursi.VleraKursi);
            //gjejme skemen e sigurimit dhe tatimet qe do perdoren per kete punonjes
            var sigurim = new clsSigurimet(idsigurimi);
            var tatimet = new colTatimet();
            tatimet.merrTatimeNdermarjeSipasDatesDeri(idndermarje, data);
            var vleratkufi = new List<decimal>
            {
                sigurim.PagaMin,
                sigurim.PagaMax,
                sigurim.PageMinShen,
                sigurim.PageMaxShen
            }; //ruajme vlerat kufi ne nje array
            if (tatimet.Count > 0)
                vleratkufi.Add(tatimet[0].Min);
            vleratkufi.AddRange(tatimet.Select(tatim => tatim.Max));

            vleratkufi.Sort(); //i rendisim

            ///gjejme pagen neto per cdo vlere kufi

            for (var i = 0; i < vleratkufi.Count; i++)
            {
                if (vleratkufi[i] / kursimon == 0) continue;
                var sigurimeshendetsore = ((vleratkufi[i] / kursimon < sigurim.PageMinShen / kursimon ? sigurim.PageMinShen / kursimon : vleratkufi[i] / kursimon) > sigurim.PageMaxShen / kursimon ? sigurim.PageMaxShen / kursimon : (vleratkufi[i] / kursimon < sigurim.PageMinShen / kursimon ? sigurim.PageMinShen / kursimon : vleratkufi[i] / kursimon)) * sigurim.SigShenPun / 100; //sigurimet shendetsore
                var sigurimeshoqerore = ((vleratkufi[i] / kursimon < sigurim.PagaMin / kursimon ? sigurim.PagaMin / kursimon : vleratkufi[i] / kursimon) > sigurim.PagaMax / kursimon ? sigurim.PagaMax / kursimon : (vleratkufi[i] / kursimon < sigurim.PagaMin / kursimon ? sigurim.PagaMin / kursimon : vleratkufi[i] / kursimon)) * sigurim.SigShoqPun / 100; //sigurimet shoqerore
                decimal vleratatime = 0;
                var k = 0;
                for (k = 0; k < tatimet.Count; k++) //tatimet
                {
                    if (tatimet[k].Min / kursimon <= vleratkufi[i] / kursimon && tatimet[k].Max / kursimon >= vleratkufi[i] / kursimon)
                    {
                        decimal P = vleratkufi[i] / kursimon;
                        string pString = "P";
                        string formula = tatimet[k].Norma;
                        formula = formula.Replace(pString, P.ToString());
                        //vleratatime = (vleratkufi[i] / kursimon);
                        DataTable dt = new DataTable();
                        decimal vleraTatime = (decimal)dt.Compute(formula, "");
                        normaFaktike = vleratatime / P;
                        break;
                        //if (tatimet[k].Menyra == 1) //totale
                        //{
                        //    vleratatime += ((vleratkufi[i] / kursimon) * tatimet[k].Norma / 100);
                        //}
                        //else //progresive
                        //{
                        //    vleratatime = (vleratkufi[i] / kursimon - (tatimet[k].Min - 1) / kursimon) * tatimet[k].Norma / 100;
                        //    for (var j = k - 1; j >= 0; j--)
                        //    {
                        //        vleratatime += ((tatimet[j].Max + 1 - tatimet[j].Min) / kursimon) * tatimet[j].Norma / 100;
                        //    }
                        //}
                        //break;
                    }
                }

                var paganeto = vleratkufi[i] / kursimon - sigurimeshendetsore - sigurimeshoqerore - vleratatime;

                if (paganeto < vlera) continue;

                if (paganeto == vlera) //nqs jane te barabarta eshte vlera kufi dhe e kthejme kete
                    return vleratkufi[i] / kursimon;
                var paganetoak = vlera;
                decimal pjesetimi = 1;
                decimal vlerafill = 0;
                var vlerambarim = vleratkufi[i] / kursimon;
                if (i > 0)
                    vlerafill = vleratkufi[i - 1] / kursimon;
                if (vlerafill >= sigurim.PageMaxShen / kursimon) //nqs vlera e kufirit te poshtem e kalon sigurimin max merret sigurimi max dhe i shtohet pages neto
                    paganetoak += ((sigurim.PageMaxShen / kursimon) * sigurim.SigShenPun / 100);
                else if (vlerambarim <= sigurim.PageMinShen / kursimon) //nqs vlera e kufirit te siperm eshte me e vogel se sigurimi min meret sigurimi min dhe i shtohet pages neto
                    paganetoak += ((sigurim.PageMinShen / kursimon) * sigurim.SigShenPun / 100);
                else // prn shuma qe do dale do pjesetohet me (1-sigurimin shendetesor)
                {
                    pjesetimi -= sigurim.SigShenPun / 100;
                }
                if (vlerafill >= sigurim.PagaMax / kursimon) //nqs vlera e kufirit te poshtem e kalon sigurimin max merret sigurimi max dhe i shtohet pages neto
                    paganetoak += ((sigurim.PagaMax / kursimon) * sigurim.SigShoqPun / 100);
                else if (vlerambarim <= sigurim.PagaMin / kursimon) //nqs vlera e kufirit te siperm eshte me e vogel se sigurimi min meret sigurimi min  dhe i shtohet pages neto
                    paganetoak += ((sigurim.PagaMin / kursimon) * sigurim.SigShoqPun / 100);
                else // prn shuma qe do dale do pjesetohet me (1-sigurimin shoqeror)
                {
                    pjesetimi -= sigurim.SigShoqPun / 100;
                }

                if (tatimet[k].Menyra == 1) //shuma do pjestohet me (1-tatimin e nivleit)
                {
                    pjesetimi -= normaFaktike / 100;
                }
                else
                {
                    // duke qene se formuala eshte (pb-(tmin-1))* norme dhe pb e kemi te panjojtur gjejme pjesen per tmin dhe ia zbresim pagesneto. pjesa e pb shkon tek pjestimi
                    var vleramin = ((tatimet[k].Min - 1) / kursimon) * normaFaktike / 100;
                    paganetoak -= vleramin;
                    pjesetimi -= normaFaktike / 100;

                    ///pjesa e nivele te meposhtme nuk ka variabla qe nuk dihen prandaj i shtohen direkt pages neto
                    for (var j = k - 1; j >= 0; j--)
                    {
                        paganetoak += ((tatimet[j].Max - tatimet[j].Min + 1) / kursimon) * normaFaktike / 100;
                    }
                }
                var pagabruto = paganetoak / pjesetimi;
                return pagabruto;
            }
            return 0;
        }

        public static clsMesazh AplikoNdryshiminEKomponentvePerPunonjesitSipasDates(List<int> idpunonjesish, DateTime data, LlojKomponentePage lloji, int idNdermarrje, int idPerdoruesi)
        {
            switch (lloji)
            {
                case LlojKomponentePage.ListPagese:
                    var kompLp = new colKomponenteListPagesePunonjesi();
                    kompLp.RiruajSipasDatave(idpunonjesish, data, idNdermarrje, idPerdoruesi);
                    break;

                case LlojKomponentePage.Page:
                    throw new NotImplementedException("TODO GETSON pak me vone per komponente page ");

                default:
                    throw new MyException($"Lloji {lloji.ToString()} eshte i pa percaktuar!");
            }
            return new clsMesazh(true, "Ndryshimi u aplikua me sukses!");
        }
        internal static string PastroSession(HttpSessionState session)
        {
            Utils.HiqVleratNgaSessioni(session);
            return "ok";
        }

        public static int MerrIdPunonjesi(string nrPersonal, int idNdermarrje)
        {
            clsPunonjes pun = new clsPunonjes(nrPersonal, idNdermarrje);
            return pun.IdPunonjes;
        }

        public static object[] merrDataKomponenteListPagesaDheShtesaPaga(int idpunonjes, bool klonim)
        {
            var result = new object[3];
            result[0] = colKomponenteListPagesePunonjesi.merrDataKomponenteListPagesa(idpunonjes, klonim);
            result[1] = colPagaShtesa.merrDataPagaShtesa(idpunonjes, klonim);
            if (((List<string>)result[1]).Count > 0)
                result[2] = new clsBankaPunonjes(idpunonjes, DateTime.Parse(((List<string>)result[1])[0]));
            else result[2] = new clsBankaPunonjes();
            return result;
        }

        public static object[] merrSkemaSigurimi(DateTime date, string datendryshimi, int idpunonjes, int idndermarje)
        {
            date = date.ToLocalTime();
            var result = new object[2];
            var col = new colSigurimet(idndermarje, date);
            result[0] = col;
            if (datendryshimi != "")
            {
                var skema = new clsSkemaSigurimi(idpunonjes, Convert.ToDateTime(datendryshimi));
                result[1] = skema;
            }
            else result[1] = null;
            return result;
        }

        public static object[] kontrolloUsername(string username, int nr, int idndermarje)
        {
            var ekziston = clsPunonjes.ekzistonUsername(username, idndermarje);
            var result = new object[2];
            result[0] = !ekziston;
            result[1] = nr;
            return result;
        }

        #endregion PUBLIC API

        #region metoda private

        /// <summary>
        /// kryen llogaritjet per punonjes
        /// </summary>
        /// <param name="arrvlera"></param>
        /// <param name="arrparam"></param>
        /// <param name="arrkodi"></param>
        /// <param name="arrmosndrysho"></param>
        /// <param name="data"></param>
        /// <param name="llogariDP"></param>
        /// <param name="nrpersonal"></param>
        /// <param name="emri"></param>
        /// <param name="muaji"></param>
        /// <param name="ditemuajindryshueshme"></param>
        /// <param name="kodi"></param>
        /// <param name="vjenNgaMuajt"></param>
        /// <param name="ditemuaji"></param>
        /// <param name="pagemuaji"></param>
        /// <param name="muajitjeter"></param>
        /// <param name="vititjeter"></param>
        /// <param name="merrimporte"></param>
        /// <param name="kodimporti"></param>
        /// <param name="vjenNgaKomponentja"></param>
        /// <param name="newrecord"></param>
        /// <param name="llogaritDiteLejeNgaImporti"></param>
        /// <param name="idndermarje"></param>
        /// <param name="kursinderm"></param>
        /// <param name="monedhaNder"></param>
        /// <param name="punonjesi"></param>
        /// <param name="tatimet"></param>
        /// <param name="ndermarrje"></param>
        /// <param name="skemasig"></param>
        /// <param name="sigurimet"></param>
        /// <param name="colpaga">jane komponentet e mundshme te punonjesit (nese do te regjistrohej ne kete date do kishte keto),  jane edhe komponentet e padukshme sepse duhen ne llogaritje por te aktivizuara</param>
        /// <param name="colkomplistpage"> jane komp qe jane ne kartelen e punonjesit dhe jane te dukshme,dhe te aktivizuara ne daten e dhene</param>
        /// <param name="punfundit"></param>
        /// <param name="colKompPage"></param>
        /// <param name="Session"></param>
        /// <param name="diteTeHarxhuara"></param>
        /// <param name="kursiMonedhesSePunonjesit"></param>
        /// <param name="listorare"></param>
        /// <param name="allOreShtesePunonjesi"></param>
        /// <param name="allKompNrPunonjesi"></param>
        /// <param name="pagaShtesaPunonjesi"></param>
        /// <param name="idGjuha1"></param>
        /// <param name="idGjuha"></param>
        /// <param name="diteLeje"></param>
        /// <param name="vleraMujore"></param>
        /// <returns></returns>
        private static dynamic llogaritPage(decimal[] arrvlera, decimal[] arrparam, string[] arrkodi, bool[] arrmosndrysho, DateTime data,DateTime datePage, bool llogariDP, string nrpersonal, string emri, int muaji, bool ditemuajindryshueshme, string kodi, bool vjenNgaMuajt, bool ditemuaji, bool pagemuaji, int muajitjeter, int vititjeter, bool merrimporte, string kodimporti, bool vjenNgaKomponentja, bool newrecord, bool llogaritDiteLejeNgaImporti, int idndermarje, clsKurset kursinderm, clsMonedha monedhaNder, clsPunonjes punonjesi, colTatimet tatimet, clsNdermarrje ndermarrje, clsSkemaSigurimi skemasig, clsSigurimet sigurimet, Dictionary<DateTime, colKompListPagese> colpagaMeData, colKompListPagese colkomplistpage, clsPunesim punfundit, decimal diteTeHarxhuara, clsKurset kursiMonedhesSePunonjesit, DataTable listorare, colOreShtese allOreShtesePunonjesi, colKomponenteNr allKompNrPunonjesi, colPagaShtesa pagaShtesaPunonjesi, int idGjuha, decimal diteLeje, ref Dictionary<string, colKomponenteMuaji> vleraMujore, colMuajTeLlogariturTrack muajTeLlogaritur, bool merrVlereDefault, DateTime dtListpagese, bool dukeMarreImporte, bool buttonClickNgaKomponente, decimal diteMuajiLp, decimal pageBazeMuajLp, colKomponentePage komponenteLp)
        {
            
            var od = "";
            var ppspershendetsore = "PPP";
            if (kursiMonedhesSePunonjesit.VleraKursi == 0) kursiMonedhesSePunonjesit.VleraKursi = 1;
            if (kursinderm.VleraKursi == 0) kursinderm.VleraKursi = 1;

            var indexPp = -1;
            var vleraDp = "0";
            var first = true;
            if (punonjesi.LlogaritNgaListorare && merrimporte && kodimporti.EqualsAnyIgnoreCase("DLK", "") && diteLeje > 0)
            {
                llogariDP = VendosDiteLejeNgaImportiNeseKa(arrvlera, arrkodi, diteLeje);
            }
            //me duhet nje list e tille per te mbajtur te gjitha id e komponenteve qe kane si parameter DP
            var listeidDp = new List<int>();
            var colkom = new colKomponentePage { Capacity = arrkodi.Length };
            //merr collecitonin me komponentet e LP te kesaj date
            colKompListPagese colpaga = null;
            if (!colpagaMeData.TryGetValue(data.Date, out colpaga)) throw new MyException($"nuk u jane marr nga db komponentet e LP per daten {data.Date}", false);

            if (!dukeMarreImporte)
            {
                diteMuajiLp = Convert.ToDecimal(PercaktoDm(colpaga.Find(x => x.KodKomponente == "DM")?.komponentePage.Formula, ditemuajindryshueshme, muaji, dtListpagese, ditemuaji, muajitjeter, vititjeter));
                pageBazeMuajLp = LlogaritPageBaze(data, datePage, pagemuaji, muajitjeter, vititjeter, kursiMonedhesSePunonjesit, pagaShtesaPunonjesi);
                komponenteLp = new colKomponentePage(colpaga.Select(x => x.komponentePage.Clone()));
            }


            //var colKompPage = new colKomponentePage(colpaga.Select(x => x.komponentePage));
            if (merrimporte && !dukeMarreImporte)
            {
                for (int i = 0, count = colpaga.Count; i < count; i++)
                {
                    if (colpaga[i].KodKomponente.EqualsAnyIgnoreCase("PPS", "PT") || colpaga[i].Njesia == 1) 
                        vleraMujore[colpaga[i].KodKomponente] = new colKomponenteMuaji();

                    if (allOreShtesePunonjesi != null && allOreShtesePunonjesi.Exists( x => x.KodiKomponentes == colpaga[i].KodKomponente))
                    {
                        for (int m = 0; m < arrkodi.Length; m++)
                        {
                            if (arrkodi[m] != colpaga[i].KodKomponente) continue;
                            arrparam[m] = 0;
                        }
                    }
                }
            }


            for (int i = 0, count = colpaga.Count; i < count; i++)
            {
                var paga = colpaga[i];
                var kompPage = paga.komponentePage; //colKompPage.Find(x => x.IdKomponentePage == paga.IdKomponentePage);
                if (kompPage.Kodi == "PP")
                {
                    od = kompPage.ParamEmri.IndexOf("ore", StringComparison.InvariantCultureIgnoreCase) == -1 ? "1" : "OD";
                }
                for (var m = 0; m < arrkodi.Length; m++)
                    if (arrkodi[m] == kompPage.Kodi)
                    {

                        //nese komponentja nuk eshte tabelare ose nuk ka te checkuar llogarit gjthm bejme vlerat 0
                        if (!(colkomplistpage.Find(x => x.KodKomponente == kompPage.Kodi) != null || (kompPage.Kodi.EqualsAnyIgnoreCase("PPS", "SPS", "SPD", "SNS", "SND", "SP", "SN", "TP", "PT") || kompPage.LlogaritGjithmone)))
                        {
                            arrparam[m] = 0;
                            arrvlera[m] = 0;
                            kompPage.Formula = "0";
                            kompPage.ParamKodi = "";
                            break;
                        }


                        //nese eshte hera e pare qe ketij punonjesi po i llogaritet paga ath,vlera duhet te merret nga vlera default e komponentes se punonjesit.
                        if (merrVlereDefault) arrvlera[m] = paga.Vlera;

                        //if (arrvlera[m] == 0 && paga.Vlera != 0)
                        //    arrvlera[m] = paga.Vlera;
                        //nese vlera eshte 0 dhe ekziston vlera default ath vendos ate (old)

                        #region merrimporte

                        if (punonjesi.LlogaritNgaListorare && merrimporte && (kodimporti == kompPage.Kodi || kodimporti == "") && (allKompNrPunonjesi != null || allOreShtesePunonjesi != null))
                        {
                            merrImporte(kompPage, m, arrvlera, arrparam, arrkodi, arrmosndrysho, data, llogariDP, nrpersonal, emri, muaji, ditemuajindryshueshme, llogaritDiteLejeNgaImporti, idndermarje, kursinderm, monedhaNder, punonjesi, tatimet, ndermarrje, skemasig, sigurimet, colpagaMeData, colkomplistpage, punfundit, allOreShtesePunonjesi, allKompNrPunonjesi, kursiMonedhesSePunonjesit, listorare, diteTeHarxhuara, pagaShtesaPunonjesi, idGjuha, diteLeje, ref vleraMujore, muajTeLlogaritur, buttonClickNgaKomponente, diteMuajiLp, pageBazeMuajLp, komponenteLp);
                        }

                        #endregion merrimporte

                        #region llogarit nga listoraret

                        if (punonjesi.LlogaritNgaListorare && arrparam[m] == 0 && listorare.Rows.Count > 0)
                        //nqs parametri nuk ka nje vlere pra nuk eshte shenuar me dore llogarisim vleren e tij sipas te dhenave te importuara, nqs ka vlere nuk e llogarisim per te evituar llogaritjet e perseritura disa here per te njejten gje
                        {
                            llogaritNgaListorare(kompPage, m, arrparam, listorare, idndermarje, data);
                        }

                        #endregion llogarit nga listoraret

                        if (kompPage.Njesi != (int)NjesiPagese.Nr) //&& komp.ParamKodi != ""
                        {
                            if (kompPage.ParamKodi == "DP" && first)
                            {
                                vleraDp = LlogaritDPFillestare(arrparam, data, muaji, ditemuajindryshueshme, ditemuaji, muajitjeter, vititjeter, m, punfundit, arrmosndrysho[m] || buttonClickNgaKomponente); //
                            }
                            if (kompPage.ParamKodi == "DP" && llogariDP && first)
                            {
                                indexPp = i;
                                if(!dukeMarreImporte)
                                    vleraDp = KrijoFormulePerDp(kompPage, ndermarrje, colpaga, llogaritDiteLejeNgaImporti);
                                first = false;
                            }
                            else if (!(kompPage.ParamKodi == "DP" && llogariDP) && (!string.IsNullOrEmpty(kompPage.ParamKodi) && (kompPage.Tipi == 1 || kompPage.Tipi == 2)))
                            {

                                colKomponenteMuaji colMuaji;
                                if ((vjenNgaMuajt && kompPage.Kodi != kodi) || !vleraMujore.TryGetValue(kompPage.Kodi, out colMuaji))
                                    colMuaji = new colKomponenteMuaji();
                                LlogaritVleraNgaParametri(arrvlera, arrparam, arrmosndrysho, muaji, kodi, vjenNgaMuajt, colMuaji, kompPage, m);
                            }
                            //nese ka pp dhe ka me shume komp se indexi i pp
                            if (indexPp != -1 && colkom.Count > indexPp && kompPage.ParamKodi != "" && !dukeMarreImporte)
                            {
                                vleraDp = ZevendesoFormulenDp(arrparam, colkom, indexPp, vleraDp, listeidDp, kompPage, m);
                            }
                            if (kompPage.ParamKodi == "DP" && !first)
                            {
                                //zevendeson formulen e komponenteve qe kane si parameter DP
                                kompPage.Formula = ZevendesoParameter(kompPage.Formula, kompPage.ParamKodi, vleraDp);
                                kompPage.ParamKodi = vleraDp;
                                //ruan ne list indexin e kesaj komponenteje qe ka si param dp
                                listeidDp.Add(i);
                                // komp.Formula = zevendesoParameter(komp.Formula, komp.ParamKodi, arrparam[i].ToString());
                            }
                        }

                        if ((merrimporte || dukeMarreImporte) && kompPage.Kodi == "CATERING")
                        {
                            if (dukeMarreImporte && muajitjeter != dtListpagese.Month)
                                kompPage.Formula = "0";
                        }

                        if (kompPage.Formula.Contains("DP"))
                        {
                            //zevendeso DP tek formula nese e permban ate
                            kompPage.Formula = ZevendesoParameter(kompPage.Formula, "DP", vleraDp);
                        }
                        if (kompPage.Njesi != (int)NjesiPagese.Nr )
                        {
                            ZevendesoFormulePerKomponenteTabelare(data, muaji, ditemuajindryshueshme, ditemuaji, muajitjeter, vititjeter, kompPage, ref ppspershendetsore, colkom, kursinderm, sigurimet, diteTeHarxhuara);
                        }
                        if (kompPage.Kodi == "OD")
                        {
                            //nese vlera eshte 0 merr vleren default te OD
                            if (arrvlera[m] == 0)
                                arrvlera[m] = colpaga.Find(x => x.KodKomponente == "OD").Vlera;

                            if (od == "OD")
                                od = arrvlera[m].ToString();
                        }
                        
                        break;
                    }

                colkom.Add(kompPage);
            }

            LlogaritPagaShtesa(data, datePage, pagemuaji, muajitjeter, vititjeter, colkom, kursiMonedhesSePunonjesit, pagaShtesaPunonjesi, dukeMarreImporte && dtListpagese.Month != muajitjeter, komponenteLp, diteMuajiLp, pageBazeMuajLp); 
                      
            if (dukeMarreImporte && dtListpagese.Month == muajitjeter )
            {
                var arrVleraTotal = new decimal[arrvlera.Length];
                for (var i = 0; i < arrvlera.Length; i++)
                    arrVleraTotal[i] = arrvlera[i];

                if (allKompNrPunonjesi != null)
                {
                    LlogaritComponenteMuajiVlerePasImporti(ref arrVleraTotal, arrkodi, colkom, allKompNrPunonjesi);
                }
                var cateringFormulaOrigjinale = komponenteLp.Find(x => x.Kodi == "CATERING");
                LlogaritKomponenteCatering(arrparam, vleraMujore, arrkodi, colkom.Find(x=> x.Kodi == "CATERING"), dtListpagese.Month, cateringFormulaOrigjinale?.Formula);
                ZevendesoParametraMeVleraPerKomponentet(arrVleraTotal, arrparam, arrkodi, llogariDP, colkom, indexPp, listeidDp, false, diteMuajiLp, pageBazeMuajLp, komponenteLp);
            }  
            else
                ZevendesoParametraMeVleraPerKomponentet(arrvlera, arrparam, arrkodi, llogariDP, colkom, indexPp, listeidDp, dukeMarreImporte && dtListpagese.Month != muajitjeter, diteMuajiLp, pageBazeMuajLp, komponenteLp);

            //ne rast se ssp dhe ssn jane inaktive i zevendesojme me 0 qe te mos ngelet tek formula dhe te japin error
            formula(colkom, "SSP", "0", false, komponenteLp, diteMuajiLp, pageBazeMuajLp);
            formula(colkom, "SSN", "0", false, komponenteLp, diteMuajiLp, pageBazeMuajLp);
            
            LlogaritKomponenteMuaji(arrvlera, arrparam, arrkodi, muajitjeter, vititjeter, colkom, vleraMujore, idGjuha, vjenNgaKomponentja, vjenNgaMuajt, dtListpagese, dukeMarreImporte, merrimporte);

            if (!dukeMarreImporte)
                ZevendesoFormulePerKomponenteTabelarePerMaxMinSigurime(colkom, kursinderm, sigurimet);
           
            EvaluateAllTabularComponents(colkom, arrvlera, arrparam, arrkodi);
            LlogaritTatiminMbiPage(tatimet, kursinderm, colkom.Find(x => x.Kodi == "TP"), colkom.Find(x => x.Kodi == "PT"));

            return new
            {
                Emri = emri,
                NrPersonal = nrpersonal,
                ColKomponente = colkom,
                Tatimet = tatimet,
                Kursi = kursiMonedhesSePunonjesit,
                KursiNdermarrjes = kursinderm,
                Departamenti = punfundit.Departament,
                VleraParam = arrparam,
                Vlerat = arrvlera,
                OrePuneNeDite = od,
                NrKomponenteve = colkomplistpage.Count,
                Kodet = arrkodi
            };
        }

        private static void LlogaritTatiminMbiPage(colTatimet tatimet, clsKurset kursinderm, clsKomponentePage tatimMbiPagen, clsKomponentePage pagaPerTatim)
        {
            if (tatimMbiPagen == null || pagaPerTatim == null) return;
            int count = tatimet.Count;
            decimal kursimon = (decimal)kursinderm.VleraKursi;
            decimal paga = Convert.ToDecimal(pagaPerTatim.Formula);
            decimal tatimi = 0;

            for (int i = count - 1; i >= 0; i--)
            {
                var pagaQePoTatohet = paga;
                if (tatimet[i].Min / kursimon <= pagaQePoTatohet / kursimon && tatimet[i].Max / kursimon >= pagaQePoTatohet / kursimon)
                {
                    decimal P = pagaQePoTatohet / kursimon;
                    string pString = "P";
                    string formula = tatimet[i].Norma;
                    formula = formula.Replace(pString, P.ToString());
                    //vleratatime = (vleratkufi[i] / kursimon);
                    DataTable dt = new DataTable();
                    decimal vleraTatime = (decimal)dt.Compute(formula, "");
                    tatimi = vleraTatime;
                    break;
                }
                //if (paga < tatimet[i].Min * (decimal)kursinderm.VleraKursi)
                //    continue;
                ////decimal P = vleratkufi[i] / kursimon;
                ////string pString = "P";
                ////string formula = tatimet[k].Norma;
                ////formula = formula.Replace(pString, P.ToString());
                //////vleratatime = (vleratkufi[i] / kursimon);
                ////DataTable dt = new DataTable();
                ////decimal vleraTatime = (decimal)dt.Compute(formula, "");
                ////normaFaktike = vleratatime / P;
                ////break;

                //var pagaQePoTatohet = paga - tatimet[i - 1].Max * (decimal)kursinderm.VleraKursi;
                //paga -= pagaQePoTatohet;
                //tatimi += pagaQePoTatohet * tatimet[i].Norma / 100;

            }

            //if (tatimet[0].Menyra == 1)
            //{

            //    //decimal P = vleratkufi[i] / kursimon;
            //    //string pString = "P";
            //    //string formula = tatimet[k].Norma;
            //    //formula = formula.Replace(pString, P.ToString());
            //    ////vleratatime = (vleratkufi[i] / kursimon);
            //    //DataTable dt = new DataTable();
            //    //decimal vleraTatime = (decimal)dt.Compute(formula, "");
            //    //normaFaktike = vleratatime / P;
            //    //break;
            //    tatimi = paga * tatimet[0].Norma / 100;
            //} else
            //{
               
            //}

            tatimMbiPagen.Formula = tatimi.ToString();
        }

        private static void LlogaritKomponenteCatering(decimal[] arrparam, Dictionary<string, colKomponenteMuaji> vleraMujore, string[] arrkodi, clsKomponentePage catering, int muajListpagese, string formula)
        {
            if (catering == null)
                return;
            catering.Formula = formula;
            for (int i = 0; i < arrkodi.Length; i++)
            {
                if(arrkodi[i].EqualsAnyIgnoreCase("SICKLEAVES", "UNPAIDLEAVES", "PP"))
                {
                    colKomponenteMuaji colMuaji = null;
                    if (!vleraMujore.TryGetValue(arrkodi[i], out colMuaji))
                    {
                        colMuaji = new colKomponenteMuaji();
                    }
                    var parameterTotal = (colMuaji.Exists(x => x.Muaji == muajListpagese) ? colMuaji.Sum(x => x.VleraParam).ToString() : (arrparam[i] + colMuaji.Sum(x => x.VleraParam)).ToString());
                    if(arrkodi[i] == "SICKLEAVES")
                        catering.Formula = ZevendesoParameter(catering.Formula, "SLDAYS", parameterTotal);
                    else if(arrkodi[i] == "UNPAIDLEAVES")
                        catering.Formula = ZevendesoParameter(catering.Formula, "ULDAYS", parameterTotal);
                    else
                        catering.Formula = ZevendesoParameter(catering.Formula, "DP", arrparam[i].ToString());

                }
            }
        }

        private static void ShtoKateringNeKomponentetPerkatese(clsKomponentePage catering, colKomponentePage colkom, Dictionary<string, colKomponenteMuaji> vleraMujore, colKomponentePage komponenteLp, int idgjuha)
        {
            if (catering == null)
                return;

            foreach(var komponente in komponenteLp)
            {
                if (komponente.Njesi != 1)
                    continue;
                var charIndex = komponente.Formula.IndexOf(catering.Kodi);
                if (charIndex == -1)
                    continue;
                decimal vlera = LlogaritVlerenSipasFormules(0, 0, idgjuha, catering);
                colKomponenteMuaji colmuaji;
                if (!vleraMujore.TryGetValue(komponente.Kodi, out colmuaji))
                    colmuaji = new colKomponenteMuaji();

                if (charIndex > 0 && komponente.Formula[charIndex - 1] == '-')
                    vlera *= -1;

            }
        }

        private static string PercaktoDm(string formula, bool ditemuajindryshueshme, int muaji, DateTime data, bool ditemuaji, int muajitjeter, int vititjeter)
        {
            if (ditemuajindryshueshme)
            {
                var mu = muaji;
                var vi = data.Year;
                if (ditemuaji)
                {
                    mu = muajitjeter;
                    vi = vititjeter;
                }
                formula = Utils.BusinessDaysInMonth(vi, mu).ToString();
            }
            return formula;
        }
        private static void LlogaritComponenteMuajiVlerePasImporti(ref decimal[] arrvlera, string[] arrkodi, colKomponentePage colkom, colKomponenteNr allKompNrPunonjesi)
        {
            foreach (var komponente in colkom) 
            {
                for (var m = 0; m < arrkodi.Length; m++)
                {
                    if (arrkodi[m] != komponente.Kodi)
                        continue;
                    if (komponente.Njesi == 0)
                    {
                        var col = allKompNrPunonjesi.Where(x => x.KodiKomponentes == komponente.Kodi);
                        if (col != null && col.Count() > 0)
                        {
                            arrvlera[m] = col.Sum(x => x.Vlera);
                        }
                        continue;
                    }
                }
            }
        }

        private static void EvaluateAllTabularComponents(colKomponentePage colkom, decimal[] arrvlera, decimal[] arrparam, string[] arrkodi)
        {
            foreach (var col in colkom.Where(x => x.Kodi.EqualsAnyIgnoreCase("PT", "PPS") || x.Njesi == 1))
            {
                for(int i = 0; i < arrkodi.Length; i++)
                {
                    if (arrkodi[i] != col.Kodi) continue;
                    var vlera = LlogaritVlerenSipasFormules(arrparam[i], arrvlera[i], 0, col);
                    arrvlera[i] = vlera;
                    col.Formula = vlera.ToString();

                }
            }
            
        }

        private static void merrImporte(clsKomponentePage komp, int m, decimal[] arrvlera, decimal[] arrparam, string[] arrkodi, bool[] arrmosndrysho, DateTime data, bool llogariDP, string nrpersonal, string emri, int muaji, bool ditemuajindryshueshme, bool llogaritDiteLejeNgaImporti, int idndermarrje, clsKurset kursiNder, clsMonedha mon, clsPunonjes punonjesi, colTatimet tatimet, clsNdermarrje ndermarrje, clsSkemaSigurimi skemasig, clsSigurimet sigurimet, Dictionary<DateTime, colKompListPagese> colpaga, colKompListPagese colkomplistpage, clsPunesim punfundit, colOreShtese allOreShtesePunonjesi, colKomponenteNr allKompNrPunonjesi, clsKurset kursiMonedhesSePunonjesit, DataTable listOrare, decimal diteTeHarxhuara, colPagaShtesa pagaShtesaPunonjesi, int idGjuha, decimal diteLeje, ref Dictionary<string, colKomponenteMuaji> vleraMujore, colMuajTeLlogariturTrack muajTeLlogaritur, bool buttonClickNgaKomponente, decimal diteMuajiLp, decimal pageBazeMuajLp, colKomponentePage komponenteLp)
        {
            if (komp.Njesi == 1)
                return;
            colKomponenteMuaji colmuaji;
            if (!vleraMujore.TryGetValue(komp.Kodi, out colmuaji))
            {
                colmuaji = new colKomponenteMuaji();
            }           

            switch (komp.Njesi)
            {
                case 2:

                    if (allOreShtesePunonjesi == null || !allOreShtesePunonjesi.Exists(x => x.KodiKomponentes == komp.Kodi)) break;

                    //#bug muaji i njejte ne dy vite te ndryshme 

                    arrparam = LlogaritImporteFormule(komp, m, arrvlera, arrparam, arrkodi, arrmosndrysho, data, llogariDP, nrpersonal, emri, muaji, ditemuajindryshueshme, llogaritDiteLejeNgaImporti, idndermarrje, kursiNder, mon, punonjesi, tatimet, ndermarrje, skemasig, sigurimet, colmuaji, colpaga, colkomplistpage, punfundit, allOreShtesePunonjesi, allKompNrPunonjesi, kursiMonedhesSePunonjesit, listOrare, diteTeHarxhuara, pagaShtesaPunonjesi, idGjuha, diteLeje, ref vleraMujore, muajTeLlogaritur, buttonClickNgaKomponente, diteMuajiLp, pageBazeMuajLp, komponenteLp);

                    arrparam[m] = colmuaji.Sum(x => x.VleraParam);
                    arrvlera[m] = colmuaji.Sum(x => x.Vlera);

                    break;

                case 0:

                    if (allKompNrPunonjesi == null) break;

                    var dimport = new colKomponenteNr(allKompNrPunonjesi.Where(x => x.KodiKomponentes == komp.Kodi));

                    if (dimport.Count > 0) //marrim vlerat per muajt e tjere dhe ruhen ne sesion si komponente muaj
                    {
                        colmuaji = LlogaritKomponenteImportiVlere(komp, m, data, muaji, dimport, colmuaji);

                        arrvlera[m] = colmuaji.Sum(x => x.Vlera);
                    }

                    break;
            }
            vleraMujore[komp.Kodi] = colmuaji;
        }

        private static decimal[] LlogaritImporteFormule(clsKomponentePage komp, int m, decimal[] arrvlera, decimal[] arrparam, string[] arrkodi, bool[] arrmosndrysho, DateTime data, bool llogariDP, string nrpersonal, string emri, int muaji, bool ditemuajindryshueshme, bool llogaritDiteLejeNgaImporti, int idndermarrje, clsKurset kursiNder, clsMonedha mon, clsPunonjes punonjesi, colTatimet tatimet, clsNdermarrje ndermarrje, clsSkemaSigurimi skemasig, clsSigurimet sigurimet, colKomponenteMuaji colmuaji, Dictionary<DateTime, colKompListPagese> colpaga, colKompListPagese colkomplistpage, clsPunesim punfundit, colOreShtese allOreShtesePunonjesi, colKomponenteNr allKompNrPunonjesi, clsKurset kursiMonedhesSePunonjesit, DataTable listOrare, decimal diteTeHarxhuara, colPagaShtesa pagaShtesaPunonjesi, int idGjuha, decimal diteLeje, ref Dictionary<string, colKomponenteMuaji> vleraMujore, colMuajTeLlogariturTrack muajTeLlogaritur, bool buttonClickNgaKomponente, decimal diteMuajiLp, decimal pageBazeMuajLp, colKomponentePage komponenteLp)
        {

            var importeTeGrupuaraSipasMuajit = Utils.GrupoKomponenteTeImportuaraSipasMuajve(muaji, data.Year, allOreShtesePunonjesi, komp);

            foreach (var kompImportuar in importeTeGrupuaraSipasMuajit)
            {
                var vitiKomponentesImportuar = kompImportuar.Value.FirstOrDefault().Viti;
                var muajiKomponentesImportuar = kompImportuar.Key;

                var muajiPerTuLlogaritur = muajTeLlogaritur.FirstOrDefault(muji => muji.muaji == muajiKomponentesImportuar);

                if (muajiPerTuLlogaritur == null)
                {
                    //nese muaji po vjen nga importi atehere e shtojme ne track-un e llogaritjes per muajt 
                    muajiPerTuLlogaritur = new MuajTeLlogariturTrack
                    {
                        muaji = muajiKomponentesImportuar,
                        llogaritur = false
                    };
                    muajTeLlogaritur.Add(muajiPerTuLlogaritur);
                }

                if (muaji == muajiKomponentesImportuar && vitiKomponentesImportuar == data.Year)
                {
                    //nese muaji i  vlerave te importuara eshte i njejte me muajin e listpageses
                    if (!muajiPerTuLlogaritur.llogaritur)
                    {
                        //var arrParaMuajiAktual = new decimal[arrparam.Length];
                        arrparam = ZevendesoParametratSipasImportit(arrkodi, arrparam, kompImportuar.Value);
                        //shenojme si te llogaritur kete muaj
                        muajiPerTuLlogaritur.llogaritur = true;

                        llogaritPage(arrvlera, arrparam, arrkodi, arrmosndrysho, data,data, llogariDP, nrpersonal, emri, muaji, ditemuajindryshueshme, komp.Kodi, true, komp.AplikoDiteMuaji, komp.AplikoPageMuaji, muajiKomponentesImportuar, vitiKomponentesImportuar, false, "", true, false, llogaritDiteLejeNgaImporti, idndermarrje, kursiNder, mon, punonjesi, tatimet, ndermarrje, skemasig, sigurimet, colpaga, colkomplistpage, punfundit, diteTeHarxhuara, kursiMonedhesSePunonjesit, listOrare, allOreShtesePunonjesi, allKompNrPunonjesi, pagaShtesaPunonjesi, idGjuha, diteLeje, ref vleraMujore, muajTeLlogaritur, false, data, true, buttonClickNgaKomponente, diteMuajiLp, pageBazeMuajLp, komponenteLp);
                    }
                    if (kompImportuar.Value.Exists(x => x.KodiKomponentes == komp.Kodi)) {
                        var kompLpSipasKoditDheDates = MerrKomponentePageNgaKomponentetEPages(arrkodi[m], colpaga, data.Date);
                        colmuaji = LlogaritColComponenteMuaji(komp, kompLpSipasKoditDheDates, m, colmuaji, vitiKomponentesImportuar, muajiKomponentesImportuar, arrparam[m]);
                    }
                }
                else
                {
                    //kryehet llogaritja per kompoentet e importuara te nje muaji te kaluar
                    // per muajt e tjere zerohen vlerat dhe merret paga ose ditet sipas kushteve

                    var arrvleramuajitjeter = new decimal[arrvlera.Length];
                    var arrparammuajitjeter = new decimal[arrparam.Length];

                    arrparammuajitjeter = ZevendesoParametratSipasImportit(arrkodi, arrparammuajitjeter, kompImportuar.Value);

                    var dataFunditMuajiTjeter = Utils.MerrDatenFunditTeMuajit(vitiKomponentesImportuar, muajiKomponentesImportuar);

                    if (!muajiPerTuLlogaritur.llogaritur)
                    {
                        //shenojme si te llogaritur kete muaj
                        muajiPerTuLlogaritur.llogaritur = true;
                        llogaritPage(arrvleramuajitjeter, arrparammuajitjeter, arrkodi, arrmosndrysho, dataFunditMuajiTjeter,dataFunditMuajiTjeter, llogariDP, nrpersonal, emri, muajiKomponentesImportuar, ditemuajindryshueshme, komp.Kodi, true, komp.AplikoDiteMuaji, komp.AplikoPageMuaji, muajiKomponentesImportuar, vitiKomponentesImportuar, false, "", true, false, llogaritDiteLejeNgaImporti, idndermarrje, kursiNder, mon, punonjesi, tatimet, ndermarrje, skemasig, sigurimet, colpaga, colkomplistpage, punfundit, diteTeHarxhuara, kursiMonedhesSePunonjesit, listOrare, allOreShtesePunonjesi, allKompNrPunonjesi, pagaShtesaPunonjesi, idGjuha, diteLeje, ref vleraMujore, muajTeLlogaritur, true, data, true, buttonClickNgaKomponente, diteMuajiLp, pageBazeMuajLp, komponenteLp);
                    }
                    if (kompImportuar.Value.Exists(x => x.KodiKomponentes == komp.Kodi)) {
                        var kompLpSipasKoditDheDates = MerrKomponentePageNgaKomponentetEPages(arrkodi[m], colpaga, dataFunditMuajiTjeter);
                        colmuaji = LlogaritColComponenteMuaji(komp, kompLpSipasKoditDheDates, m, colmuaji, vitiKomponentesImportuar, muajiKomponentesImportuar, arrparammuajitjeter[m]);
                    }

                }
            }
            return arrparam;
        }



        private static colKomponenteMuaji LlogaritColComponenteMuaji(clsKomponentePage komp, clsKompListPagese kompLp, int m, colKomponenteMuaji colmuaji, int vitiKomponentesImportuar, int muajiKomponentesImportuar, decimal vleraParamMuaji)
        {
            using (var dt = new DataTable())
            {
                decimal vlera = 0;
                try
                {
                    vlera = decimal.Parse(dt.Compute(kompLp.komponentePage.Formula, "").ToString());
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex, $"Nuk mund te vleresohet formula per komponenten {kompLp.komponentePage.Kodi}...Formula {kompLp.komponentePage.Formula}");
                    throw new MyException($"Nuk mund te vleresohet formula per komponenten {kompLp.komponentePage.Kodi}");
                }
                var km = new clsKomponenteMuaji(0, m, muajiKomponentesImportuar, vitiKomponentesImportuar, vleraParamMuaji, vlera, komp.Kodi, komp.Pershkrimi, komp.ParamEmri, komp.Njesi, komp.Tipi);
                if (km.Vlera != 0 || km.VleraParam != 0)
                {
                    colmuaji.RemoveAll(x => x.Muaji == km.Muaji && x.Viti == km.Viti);
                    colmuaji.ShtoMeIdNegative(km);
                }
            }
            return colmuaji;
        }

        private static clsKompListPagese MerrKomponentePageNgaKomponentetEPages
            (string kodi, Dictionary<DateTime, colKompListPagese> colpaga, DateTime dataFunditMuajit)
        {
            colKompListPagese colpagadates = null;

            if (!colpaga.TryGetValue(dataFunditMuajit, out colpagadates)) throw new MyException($"nuk u jane marr nga db komponentet e LP per daten {dataFunditMuajit}", false);

            var kompPage = colpagadates.Find(x => x.KodKomponente == kodi);
            return kompPage;
        }

        /// <summary>
        /// BLACKBOX
        /// </summary>
        /// <param name="komp"></param>
        /// <param name="m"></param>
        /// <param name="arrparam"></param>
        /// <param name="listorare"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="data"></param>
        private static void llogaritNgaListorare(clsKomponentePage komp, int m, decimal[] arrparam, DataTable listorare, int idNdermarrje, DateTime data)
        {
            var drs = listorare.Select("kodi='" + komp.Kodi + "'");
            // zgjedhim ato listorare qe simboli i tyre prek kete komponente
            decimal dite = 0;
            decimal ore = 0;
            if (drs.Length > 0)
            {
                //i grupojme rreshtat sipas simbolit
                var aaa = drs.GroupBy(x => x.Field<string>("Simboli"));
                if (!komp.ParamEmri.Contains("ore") && !komp.ParamEmri.Contains("Ore"))
                //nqs komponentja nuk eshte me baze ore atehere totali do dali si shumator i sa here perseritet secili simbol shumezim koeficientin e secilit simbol
                {
                    for (var key = 0; key < aaa.Count(); key++)
                    //per cdo simbol bejme koeficient simboli shumezim sa here perseritet simboli
                    {
                        dite += Convert.ToDecimal((object)aaa.ElementAt(key).First()["KoeficientSimboli"]) * aaa.ElementAt(key).Count();
                    }
                    //dhe vleren e gjetur ia kalojme parametrit ose formules
                    if (komp.Njesi == 2) arrparam[m] = dite; //nqs eshte formule te dhenat shkojne tek parametri
                    else if (komp.Njesi == 0) komp.Formula = (dite.ToString()); //prn nqs eshte numer tek formula
                }
                else
                {
                    ///nqs komponentja eshte me baze orare
                    ///marrim oret ditore te punes te konfiguruara ne komponentet e listpageses
                    var kompore = new clsKomponentePage("OD", idNdermarrje, data);
                    for (var key = 0; key < aaa.Count(); key++)
                    {
                        ///per cdo simbol marrim oren e fillimit dhe te mbarimit te simbolit
                        var elementi = aaa.ElementAt(key);
                        var orefillimi = elementi.First()["OREFILLIMI"].ToString();
                        var orembarimi = elementi.First()["OREMBARIMI"].ToString();
                        var OF = Convert.ToDateTime((string)orefillimi);
                        var OM = Convert.ToDateTime((string)orembarimi);
                        if (orefillimi == "00:00:00" && orembarimi == "00:00:00")
                            //nqs nuk ka ore fillimi dhe mbarimi atehere marrim per baze Ore pune ne dite dhe e shumezojme me koeficientin dhe me sa here perseritet simboli
                            ore += Convert.ToDecimal((object)elementi.First()["KoeficientSimboli"]) * elementi.Count() * Convert.ToDecimal((string)kompore.Formula);
                        else
                        {
                            //nqs kemi te caktuar ore fillimi dhe ore mbarimi
                            //shikojme nqs ndonje nga ditet eshte feste
                            //nqs po atehere oret e simbolit i shumezojme me koeficientin e festes
                            var diteFeste = elementi.Where(x => x.Field<string>("EshteFeste") == "Po");
                            for (var keyb = 0; keyb < diteFeste.Count(); keyb++)
                            {
                                var elem = diteFeste.ElementAt(keyb);
                                var orefeste = Convert.ToDecimal((OM - OF).TotalHours);
                                if (orefeste < 0)
                                    //nqs ora e mbarimit eshte me e vogel se e fillimit pra dita tjeter i shtojme 24 ore
                                    orefeste = 24 + orefeste;
                                ore += Convert.ToDecimal((object)elem["KoeficientFeste"]) * orefeste;
                            }
                            ///gjejme ditet e tjera qe nuk jane feste
                            var oreJoFeste = elementi.Where(x => x.Field<string>("EshteFeste") == "Jo");
                            ///i grupojme sipas diteve te javes
                            var ditejave = oreJoFeste.GroupBy(x => x.Field<string>("dita"));

                            for (var keyb = 0; keyb < ditejave.Count(); keyb++)
                            {
                                ///per cdo dite marrim konfigurimet e oreve per ate dite
                                decimal oreditajava = 0;
                                var elementidite = ditejave.ElementAt(keyb).First();
                                var configurime = colTrupiKonfigListOrari.ktheTrupiKonfigListOrariSipasDites(idNdermarrje, elementidite["dita"].ToString());
                                if (OF > OM)
                                //nqs ora e fillimit me e madhe se ora e mbarimit pra dita tjeter e ndajme intervalin ne dy
                                {
                                    var of1 = OF; ///ora e fillimit deri ne mesnate
                                    var om1 = DateTime.Parse("00:00:00");
                                    var of2 = DateTime.Parse("00:00:00"); //nga mesnata deri ne oren e mbarimit
                                    var om2 = OM;
                                    for (var oredite = 0; oredite < configurime.Rows.Count; oredite++)
                                    {
                                        //per cdo konfigurim marrim oren e fillimit dhe oren e mbarimit
                                        var konfOF = Convert.ToDateTime((string)configurime.Rows[oredite]["OREFILLIMI"].ToString());
                                        var konfOM = Convert.ToDateTime((string)configurime.Rows[oredite]["OREMBARIMI"].ToString());
                                        if (konfOF <= of2 && konfOM > of2) //ora e fillimit i perket ketij brezi
                                        {
                                            if (konfOM >= om2)
                                                //nqs ora e mbarimit te simbolit me e vogel se ora e mbarimit te brezit atehere marrim oren e mbarimit te simbolit
                                                oreditajava += Convert.ToDecimal((om2 - of2).TotalHours) * Convert.ToDecimal((object)configurime.Rows[oredite]["KOEFICIENTI"]);
                                            else
                                            //prn marrim oren e mbarimit te brezit dhe pastaj kete ore ia kalojme ores se fillimit per te gjetur nivelin tjeter
                                            {
                                                oreditajava += Convert.ToDecimal((konfOM - of2).TotalHours) * Convert.ToDecimal((object)configurime.Rows[oredite]["KOEFICIENTI"]);
                                                of2 = konfOM;
                                            }
                                        }
                                        if (konfOF <= of1 && (konfOM == DateTime.Parse("00:00:00") || konfOM > of1))
                                        //ora e fillimit i perket ketij brezi
                                        {
                                            if (om1 != DateTime.Parse("00:00:00") && konfOM >= om1)
                                                //nqs ora e mbarimit nuk eshte mesnata
                                                oreditajava += Convert.ToDecimal((om1 - of1).TotalHours) * Convert.ToDecimal((object)configurime.Rows[oredite]["KOEFICIENTI"]);
                                            else
                                            {
                                                if (konfOM == DateTime.Parse("00:00:00"))
                                                //nqs eshte mesnata i shtojme 24 ore diferences
                                                {
                                                    oreditajava += (24 + Convert.ToDecimal((konfOM - of1).TotalHours)) * Convert.ToDecimal((object)configurime.Rows[oredite]["KOEFICIENTI"]);
                                                }
                                                else
                                                    oreditajava += Convert.ToDecimal((konfOM - of1).TotalHours) * Convert.ToDecimal((object)configurime.Rows[oredite]["KOEFICIENTI"]);
                                                of1 = konfOM;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    var of1 = OF;
                                    var om1 = OM;
                                    for (var oredite = 0; oredite < configurime.Rows.Count; oredite++)
                                    {
                                        var konfOF = Convert.ToDateTime((string)configurime.Rows[oredite]["OREFILLIMI"].ToString());
                                        var konfOM = Convert.ToDateTime((string)configurime.Rows[oredite]["OREMBARIMI"].ToString());
                                        if (konfOF <= of1 && (konfOM == DateTime.Parse("00:00:00") || konfOM > of1))
                                        //ora e fillimit i perket ketij brezi
                                        {
                                            if (om1 != DateTime.Parse("00:00:00") && konfOM >= om1)
                                                //nqs ora e mbarimit nuk eshte mesnata
                                                oreditajava += Convert.ToDecimal((om1 - of1).TotalHours) * Convert.ToDecimal((object)configurime.Rows[oredite]["KOEFICIENTI"]);
                                            else
                                            {
                                                if (konfOM == DateTime.Parse("00:00:00"))
                                                //nqs ora e mbarimit te konfigurimit eshte mesnata i shtojme 24 ore diferences
                                                {
                                                    oreditajava += (24 + Convert.ToDecimal((konfOM - of1).TotalHours)) * Convert.ToDecimal((object)configurime.Rows[oredite]["KOEFICIENTI"]);
                                                }
                                                else
                                                    oreditajava += Convert.ToDecimal((konfOM - of1).TotalHours) * Convert.ToDecimal((object)configurime.Rows[oredite]["KOEFICIENTI"]);
                                                of1 = konfOM;
                                            }
                                        }
                                    }
                                }

                                ore += oreditajava * ditejave.ElementAt(keyb).Count();
                            }
                        }
                    }
                    switch (komp.Njesi)
                    {
                        case 2:
                            arrparam[m] = ore; //nqs eshte formule te dhenat shkojne tek parametri
                            break;

                        case 0:
                            komp.Formula = (ore.ToString(CultureInfo.InvariantCulture)); //prn nqs eshte numer tek formula
                            break;
                    }
                }
            }
        }

        private static string ZevendesoFormulenDp(decimal[] arrparam, colKomponentePage colkom, int indexPP, string vleraDP, List<int> listeidDp, clsKomponentePage kompPage, int m)
        {
            //zevendesojme formulen me parametrat per PP
            colkom[indexPP].Formula = ZevendesoParameter(colkom[indexPP].Formula, kompPage.ParamKodi, arrparam[m].ToString());
            colkom[indexPP].ParamKodi = ZevendesoParameter(colkom[indexPP].ParamKodi, kompPage.ParamKodi, arrparam[m].ToString());

            vleraDP = ZevendesoParameter(vleraDP, kompPage.ParamKodi, arrparam[m].ToString());
            //zevendeso DP tek komponentet e tjera qe e kane si parameter ate
            foreach (var ind in listeidDp.Where(ind => colkom.Count > ind))
            {
                colkom[ind].Formula = ZevendesoParameter(colkom[ind].Formula, kompPage.ParamKodi, arrparam[m].ToString());
                colkom[ind].ParamKodi = ZevendesoParameter(colkom[ind].ParamKodi, kompPage.ParamKodi, arrparam[m].ToString());
            }

            return vleraDP;
        }

        private static string KrijoFormulePerDp(clsKomponentePage kompPage, clsNdermarrje ndermarrje, colKompListPagese colpaga, bool llogaritDiteLejeNgaImporti)
        {
            var kaDr = colpaga.Exists(c => c.KodKomponente == "PR" || c.Emerparam.EqualsIgnoreCase("Dite Raporti"));
            var kaDrn = colpaga.Exists(c => c.KodKomponente == "PRN" || c.Emerparam.IndexOf("Dite Raporti Mbi 12", StringComparison.InvariantCultureIgnoreCase) > -1);
            var formulaDp = "";
            if (ndermarrje.Lloji == 2)
            {
                var form = "(DM";
                if (kaDr)
                    form += "-DR";
                if (kaDrn)
                    form += "-DRN";
                form += ")";
                kompPage.Formula = ZevendesoParameter(kompPage.Formula, kompPage.ParamKodi, form);
                kompPage.ParamKodi = form;
                formulaDp = form;
            }
            else
            {
                var form = llogaritDiteLejeNgaImporti ? "(DM-DLK" : "(DM";
                if (kaDr)
                    form += "-DR";
                if (kaDrn)
                    form += "-DRN";
                form += ")";
                kompPage.Formula = ZevendesoParameter(kompPage.Formula, kompPage.ParamKodi, form);
                kompPage.ParamKodi = form;
                formulaDp = form;
            }
            return formulaDp;
        }

        /// <summary>
        ///     merr ditet e lejes nga importi
        /// </summary>
        /// <param name="arrvlera"></param>
        /// <param name="arrkodi"></param>
        /// <returns></returns>
        private static bool VendosDiteLejeNgaImportiNeseKa(decimal[] arrvlera, string[] arrkodi, decimal diteleje)
        {
            var llogariDp = false;
            for (var m = 0; m < arrkodi.Length; m++)
                if (arrkodi[m] == "DLK")
                {
                    if (diteleje != 0)
                        llogariDp = true;
                    arrvlera[m] = diteleje;
                    break;
                }
            return llogariDp;
        }

        private static string LlogaritDPFillestare(decimal[] arrparam, DateTime data, int muaji, bool ditemuajindryshueshme, bool ditemuaji, int muajitjeter, int vititjeter, int m, clsPunesim punfundit, bool mosNdrysho)
        {
            string vleraDP = arrparam[m].ToString(CultureInfo.InvariantCulture);

            //kjo eshte shtuar per vodafone-in qe te llogaritet automatikisht dp sipas diteve qe ka muaji
            //mosndrysho eshte rasti kur perdoruesi vendos vleren 0 ne gride me dore (vodafone i shenon me 0 dite pune punonjesit qe jane me leje lindje)
            //ditemuajindryshuemshme nese vlera e diteve te punes duhet te llogaritet ne baze te diteve te muajit
            //
            if (mosNdrysho || !ditemuajindryshueshme || arrparam[m] != 0)
                return vleraDP;

            var mu = muaji;
            var vi = data.Year;
            if (ditemuaji)
            {
                mu = muajitjeter;
                vi = vititjeter;
            }
            var datefillimi = 1;//nese ka date fillimi > se dt e fillimit te muajit ath merr ate si dtFillimi else merr daten 1
            if ((punfundit.DtFillimi > new DateTime(vi, mu, 1)))
            {
                clsPunesim punparafundit = new clsPunesim();
                punparafundit.merrPunesimParaFundit(punfundit.IdPunonjes, punfundit.DtFillimi);
                if (punparafundit.DtFillimi > DateTime.MinValue)
                    datefillimi = (punparafundit.DtFillimi > new DateTime(vi, mu, 1)) ? punparafundit.DtFillimi.Day : punfundit.DtFillimi.Day;
                else
                    datefillimi = punfundit.DtFillimi.Day;
            }

            //nese ka dtLargimi perpara dates se fundit te muajit merr dtLargimi else nr e diteve qe ka muaji
            var daysInMonth = (punfundit.DtLargimi > new DateTime(1970, 01, 01) && punfundit.DtLargimi < Utils.MerrDatenFunditTeMuajit(vi, mu)) ? punfundit.DtLargimi.Value.Day : DateTime.DaysInMonth(vi, mu);

            arrparam[m] = Utils.BusinessDaysInMonth(vi, mu, datefillimi, daysInMonth - datefillimi + 1);

            vleraDP = arrparam[m].ToString(CultureInfo.InvariantCulture);
            return vleraDP;
        }

        private static void LlogaritVleraNgaParametri(decimal[] arrvlera, decimal[] arrparam, bool[] arrmosndrysho, int muaji, string kodi, bool vjenNgaMuajt, colKomponenteMuaji colkompmuaj, clsKomponentePage kompPage, int m)
        {
            if (colkompmuaj.Count > 0 && kompPage.ParamKodi != "")
            {
                if ("PP" == kompPage.Kodi && vjenNgaMuajt)
                    kompPage.Formula = ZevendesoParameter(kompPage.Formula, kompPage.ParamKodi, arrparam[m].ToString());
                //perllogaritet kur eshte zgjedhur nje komponente dhe eshte hapur lupa e muajve
                else if (kodi == kompPage.Kodi && vjenNgaMuajt)
                    kompPage.Formula = ZevendesoParameter(kompPage.Formula, kompPage.ParamKodi, arrparam[m].ToString());
                //nese ke vetem nje muaj dhe eshte muaji i LP llogarit direkt vleren qe ka komponentja,nuk me interson se cfare ka tek komponente muaji
                else if (colkompmuaj.Count == 1 && colkompmuaj[0].Muaji == muaji)
                    kompPage.Formula = ZevendesoParameter(kompPage.Formula, kompPage.ParamKodi, arrparam[m].ToString());
                //rasti kur ka shume muaj ose muaj te meparshem nuk duhet ta llogarisi por te perdori vleren qe ka :)
                //perjashtim ben rasti kur kur perdoruesi ka vendosur vleren 0
                else if(arrparam[m] != 0)
                    kompPage.Formula = arrvlera[m].ToString();
            }
            //rasti kur ske kompMuaji por eshte vetem parametri
            else if (kompPage.ParamKodi != "")
                kompPage.Formula = ZevendesoParameter(kompPage.Formula, kompPage.ParamKodi, arrparam[m].ToString());
            //rasti kur perdoruesi e ka modifikuar vleren me dore,mos llogarit por perdor ate vlere
            else if (arrmosndrysho[m])
                kompPage.Formula = arrvlera[m].ToString();
        }
        /// <summary>
        /// zevendeson komponentet qe marrin vlerat nga tabela te tjera ku jane konfiguruar si psh sigurimet
        /// </summary>
        /// <param name="data"></param>
        /// <param name="muaji"></param>
        /// <param name="ditemuajindryshueshme"></param>
        /// <param name="ditemuaji"></param>
        /// <param name="muajitjeter"></param>
        /// <param name="vititjeter"></param>
        /// <param name="kompPage"></param>
        /// <param name="ppspershendetsore"></param>
        /// <param name="colkom"></param>
        /// <param name="kursinderm"></param>
        /// <param name="sigurimet"></param>
        /// <param name="diteTeHarxhuara"></param>
        private static void ZevendesoFormulePerKomponenteTabelare(DateTime data, int muaji, bool ditemuajindryshueshme, bool ditemuaji, int muajitjeter, int vititjeter, clsKomponentePage kompPage, ref string ppspershendetsore, colKomponentePage colkom, clsKurset kursinderm, clsSigurimet sigurimet, decimal diteTeHarxhuara)
        {
            switch (kompPage.Kodi)
            {
                case "PPS":
                    //ppspershendetsore = string.Format("(iif({1}>{0}, {0}, iif({1}<{2},{2}, {1} ) ))", sigurimet.PageMaxShen / Convert.ToDecimal(kursinderm.VleraKursi), kompPage.Formula, sigurimet.PageMinShen / Convert.ToDecimal(kursinderm.VleraKursi));
                    //formula(colkom, "PPP", $"({ppspershendetsore})");
                    //kompPage.Formula = ppspershendetsore;
                    //break;

                case "SPS":
                    //kompPage.Formula = string.Format("((iif(PPS>{0},{0},PPS))*{1}/100)", sigurimet.PagaMax / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigShoqPun);
                    //break;

                case "SPD":
                    //kompPage.Formula = string.Format("((iif({2}>{0},{0},{2}))*{1}/100)", sigurimet.PageMaxShen / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigShenPun, ppspershendetsore);
                    //break;

                case "SNS":
                    //kompPage.Formula = string.Format("((iif(PPS>{0},{0},PPS))*{1}/100)", sigurimet.PagaMax / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigShoqNder);
                    //break;

                case "SND":
                    //kompPage.Formula = string.Format("((iif({2}>{0},{0},{2}))*{1}/100)", sigurimet.PageMaxShen / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigShenNder, ppspershendetsore);
                    //break;

                case "SSP":
                    //kompPage.Formula = string.Format("((iif(PPS>{0},{0},PPS))*{1}/100)", sigurimet.PagaMax / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigSupPun);
                    //break;

                case "SSN":
                    //kompPage.Formula = string.Format("((iif(PPS>{0},{0},PPS))*{1}/100)", sigurimet.PagaMax / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigSupNder);
                    //break;

                case "SP":
                    //kompPage.Formula = "SPS+SPD+SSP";
                    //break;

                case "SN":
                    //kompPage.Formula = "SNS+SND+SSN";
                    //break;
                    break;

                case "DLM":
                    kompPage.Formula = $"{kompPage.Formula}-{diteTeHarxhuara}";
                    break;

                case "TP":
                    kompPage.Formula = "PT";
                    break;

                case "DM":
                    kompPage.Formula = PercaktoDm(kompPage.Formula, ditemuajindryshueshme, muaji, data, ditemuaji, muajitjeter, vititjeter);
                    break;

                    //case "OD":
                    //    if (result[8].ToString() == "OD")
                    //        result[8] = zevendesoParameter(result[8].ToString(), "OD", komp.Formula);
                    //    break;
            }
            //return ppspershendetsore;
        }

        private static void ZevendesoFormulePerKomponenteTabelarePerMaxMinSigurime(colKomponentePage colkom, clsKurset kursinderm, clsSigurimet sigurimet)
        {
            var pps = colkom.Find(x => x.Kodi == "PPS");
            string ppspershendetsore = string.Format("(iif({1}>{0}, {0}, iif({1}<{2},{2}, {1} ) ))", sigurimet.PageMaxShen / Convert.ToDecimal(kursinderm.VleraKursi), pps.Formula, sigurimet.PageMinShen / Convert.ToDecimal(kursinderm.VleraKursi));
            pps.Formula = string.Format("(iif({1}>{0}, {0}, iif({1}<{2},{2}, {1} ) ))", sigurimet.PagaMax / Convert.ToDecimal(kursinderm.VleraKursi), pps.Formula, sigurimet.PagaMin / Convert.ToDecimal(kursinderm.VleraKursi));

           

            formula(colkom, "PPP", $"({ppspershendetsore})", false, null, 0, 0);
            string formulaSPS = "0", formulaSPD = "0", formulaSNS = "0", formulaSND = "0", formulaSSP = "0", formulaSSN = "0";

            foreach (var kompPage in colkom)
            {
                switch (kompPage.Kodi)
                {
                    case "SPS":
                        formulaSPS = string.Format("((iif({2}>{0},{0},{2}))*{1}/100)", sigurimet.PagaMax / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigShoqPun, pps.Formula);
                        kompPage.Formula = formulaSPS;
                        break;

                    case "SPD":

                        formulaSPD = string.Format("((iif({2}>{0},{0},{2}))*{1}/100)", sigurimet.PageMaxShen / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigShenPun, ppspershendetsore);
                        kompPage.Formula = formulaSPD;
                        break;

                    case "SNS":
                        formulaSNS = string.Format("((iif({2}>{0},{0},{2}))*{1}/100)", sigurimet.PagaMax / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigShoqNder, pps.Formula);
                        kompPage.Formula = formulaSNS;
                        break;

                    case "SND":
                        formulaSND = string.Format("((iif({2}>{0},{0},{2}))*{1}/100)", sigurimet.PageMaxShen / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigShenNder, ppspershendetsore);
                        kompPage.Formula = formulaSND;
                        break;

                    case "SSP":
                        formulaSSP = string.Format("((iif({2}>{0},{0},{2}))*{1}/100)", sigurimet.PagaMax / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigSupPun, pps.Formula);
                        kompPage.Formula = formulaSSP;
                        break;

                    case "SSN":
                        formulaSSN = string.Format("((iif({2}>{0},{0},{2}))*{1}/100)", sigurimet.PagaMax / Convert.ToDecimal(kursinderm.VleraKursi), sigurimet.SigSupNder, pps.Formula);
                        kompPage.Formula = formulaSSN;
                        break;
                }
            }
            var SP = colkom.Find(x => x.Kodi == "SP");
            SP.Formula = formulaSPS + " + " + formulaSPD + " + " + formulaSSP;
            var SN = colkom.Find(x => x.Kodi == "SN");
            SN.Formula = formulaSNS + " + " + formulaSND + " + " + formulaSSN;
        }

        private static void ZevendesoParametraMeVleraPerKomponentet(decimal[] arrvlera, decimal[] arrparam, string[] arrkodi, bool llogariDP, colKomponentePage colkom, int indexPP, List<int> listeidDP, bool dukeMarreImporte, decimal diteMuajiLp, decimal pageBazeMuajLp, colKomponentePage komponenteLp)
        {
            foreach (var komponente in colkom)
            {
                for (var m = 0; m < arrkodi.Length; m++)
                {
                    if (arrkodi[m] != komponente.Kodi)
                        continue;

                    if (komponente.Njesi == 0)
                    {
                        formula(colkom, komponente.Kodi, arrvlera[m].ToString(), dukeMarreImporte, komponenteLp, diteMuajiLp, pageBazeMuajLp);
                        if (indexPP != -1)
                        {
                            colkom[indexPP].ParamKodi = ZevendesoParameter(colkom[indexPP].ParamKodi, komponente.Kodi, arrvlera[m].ToString());
                            foreach (var ind in listeidDP.Where(ind => colkom.Count > ind))
                            {
                                colkom[ind].ParamKodi = ZevendesoParameter(colkom[ind].ParamKodi, komponente.Kodi, arrvlera[m].ToString());
                            }
                        }
                    }
                    else
                    {
                        if (!(komponente.ParamKodi.ContainsAnyIgnoreCase("+", "-")) && komponente.ParamKodi != "" && (komponente.Kodi != "PP" || !llogariDP))
                            formula(colkom, komponente.ParamKodi, arrparam[m].ToString(), dukeMarreImporte, komponenteLp, diteMuajiLp, pageBazeMuajLp);
                        
                        formula(colkom, komponente.Kodi, $"({komponente.Formula})", dukeMarreImporte, komponenteLp, diteMuajiLp, pageBazeMuajLp);

                        if (indexPP != -1)
                        {
                            colkom[indexPP].ParamKodi = ZevendesoParameter(colkom[indexPP].ParamKodi, komponente.Kodi, $"({komponente.Formula})");
                            foreach (var ind in listeidDP.Where(ind => colkom.Count > ind))
                            {
                                colkom[ind].ParamKodi = ZevendesoParameter(colkom[ind].ParamKodi, komponente.Kodi, $"({komponente.Formula})");
                            }
                        }
                    }

                    break;
                }
            }
        }
        /// <summary>
        /// Ketu behen zevendesimet e komponenteve te pages PP,PB etc
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pageMuaji"></param>
        /// <param name="muajiTjeter"></param>
        /// <param name="vitiTjeter"></param>
        /// <param name="colkom"></param>
        /// <param name="kursi"></param>
        /// <param name="pagaShtesaPunonjesi"></param>
        private static void LlogaritPagaShtesa(DateTime data, DateTime datePage, bool pageMuaji, int muajiTjeter, int vitiTjeter, colKomponentePage colkom, clsKurset kursi, colPagaShtesa pagaShtesaPunonjesi, bool dukeMarreImporte, colKomponentePage komponenteLp, decimal diteMuajiLp, decimal pageBazeMuajLp)
        {
            data = pageMuaji ? Utils.MerrDatenFunditTeMuajit(vitiTjeter, muajiTjeter) : datePage;

            //kujdes per kete rast si date page eshte perdorur data e aktivizimit
            var pagaShtesaPunonjesiSipasDates = new colPagaShtesa(pagaShtesaPunonjesi.Where(x => x.DtAktivizimi.Date == data.Date));
            if (pagaShtesaPunonjesiSipasDates.Count == 0)
                throw new MyException($"Nuk jane marr nga db komponentet e pagashtesa per daten {data.Date}", false);
            foreach (var pagaShtesa in pagaShtesaPunonjesiSipasDates)
            {
                if (pagaShtesa.KomponentePage.Kodi.EqualsAnyIgnoreCase("SF", "SHK", "SUP", "SD", "SV"))
                    formula(colkom, pagaShtesa.KomponentePage.Kodi, $"({pagaShtesa.Vlera})", false, null, 0, 0);
                else
                    formula(colkom, pagaShtesa.KomponentePage.Kodi, $"({pagaShtesa.Vlera * Convert.ToDecimal(kursi.VleraKursi)})", dukeMarreImporte, komponenteLp, diteMuajiLp, pageBazeMuajLp);
            }
        }
        private static decimal LlogaritPageBaze(DateTime data, DateTime datePage, bool pageMuaji, int muajiTjeter, int vitiTjeter, clsKurset kursi, colPagaShtesa pagaShtesaPunonjesi)
        {
            data = pageMuaji ? Utils.MerrDatenFunditTeMuajit(vitiTjeter, muajiTjeter) : datePage;

            //kujdes per kete rast si date page eshte perdorur data e aktivizimit
            var pagaShtesaPunonjesiSipasDates = new colPagaShtesa(pagaShtesaPunonjesi.Where(x => x.DtAktivizimi.Date == data.Date));
            if (pagaShtesaPunonjesiSipasDates.Count == 0)
                throw new MyException($"Nuk jane marr nga db komponentet e pagashtesa per daten {data.Date}", false);
            var pagaShtesaPB = pagaShtesaPunonjesiSipasDates.Find(x => x.KomponentePage.Kodi == "PB");

            if (pagaShtesaPB == null)
                throw new MyException($"Mungon komponentja PB nga pagashtesa per daten {data.Date}", false);
            return pagaShtesaPB.Vlera * Convert.ToDecimal(kursi.VleraKursi);
        }
        private static void LlogaritKomponenteMuaji(decimal[] arrvlera, decimal[] arrparam, string[] arrkodi, int muajiTjeter, int vitiTjeter, colKomponentePage colkom, Dictionary<string, colKomponenteMuaji> vleraMujore, int idGjuha, bool vjenNgaKomponentja, bool vjenNgaMuajt, DateTime dtListpagese, bool dukeImportuar, bool merrimporte)
        {

            foreach (var komponente in colkom.Where(komponente =>  !komponente.Kodi.EqualsAnyIgnoreCase("OD", "DM", "DL", "DLK", "DLM"))) //, "PT","PPS"
            {
                for (var m = 0; m < arrkodi.Length; m++)
                {
                    if (arrkodi[m] != komponente.Kodi)
                        continue;
                    colKomponenteMuaji colmuaji;
                    if (!vleraMujore.TryGetValue(komponente.Kodi, out colmuaji))
                        colmuaji = new colKomponenteMuaji();
                    var vleraParametrit = arrparam[m];
                    var vleraVlera = arrvlera[m];

                    //ne rast se plotesojme nga lupa e muajve vlerat per dy ose me shume muaj ath mos rillogarit vlerat nga komponentja,
                    //nese eshte plotesuar vlera per nje muaj te ndryshem nga muaji i komponentes mos rillogarit..
                    //else rillogarit nga komponentja :)
                    
                    if (komponente.Kodi.EqualsAnyIgnoreCase("PPS", "PT") || komponente.Njesi == 1)
                    {
                        if (merrimporte && !dukeImportuar && colmuaji.Count > 0) // && komponente.Kodi != "TP"
                        {
                            if (colmuaji.Exists(x => x.Viti == vitiTjeter && x.Muaji == muajiTjeter))
                            {
                                var vleraTabelare = colmuaji.Sum(x => x.Vlera);
                                komponente.Formula = vleraTabelare.ToString();
                            }
                        }
                    }
                    

                    if (komponente.Njesi == 2 && !komponente.Kodi.EqualsAnyIgnoreCase("PPS", "PT") && LlogaritKomponenteMuajiFormule(colmuaji, muajiTjeter, vitiTjeter, vleraParametrit, vjenNgaKomponentja, vjenNgaMuajt, komponente, vleraMujore, dtListpagese, arrparam, m, dukeImportuar)) 
                        continue;

                    if (komponente.Njesi == 0 && LlogaritKomponenteMuajiNumerike(colmuaji, muajiTjeter, vitiTjeter, vleraVlera, vjenNgaKomponentja, vjenNgaMuajt, komponente, vleraMujore, dtListpagese, arrvlera, m, dukeImportuar))
                        continue;
                    
                    
                    var vlera = LlogaritVlerenSipasFormules(vleraParametrit, arrvlera[m], idGjuha, komponente);
                    if (vlera == 0 && vleraParametrit == 0)
                    {
                        //nese vlera eshte vendosur me dore 0,ath pastrojme vlerat per muajt
                        if (vjenNgaKomponentja && !vjenNgaMuajt)
                            vleraMujore[komponente.Kodi] = new colKomponenteMuaji();
                        break;
                    }

                    var km = new clsKomponenteMuaji(0, m, muajiTjeter, vitiTjeter, vleraParametrit, vlera, komponente.Kodi, komponente.Pershkrimi, komponente.ParamEmri, komponente.Njesi, komponente.Tipi);
                    if (km.Vlera != 0 || km.VleraParam != 0)
                    {
                        if ((komponente.Kodi.EqualsAnyIgnoreCase("PPS", "PT") || komponente.Njesi == 1) && merrimporte && !dukeImportuar && colmuaji.Count > 0)
                            colmuaji.RemoveAll(x => x.Muaji != muajiTjeter);
                        
                        colmuaji.RemoveAll(x => x.Muaji == km.Muaji && x.Viti == km.Viti);
                        colmuaji.ShtoMeIdNegative(km);
                        vleraMujore[komponente.Kodi] = colmuaji;
                    }
                    break;
                }
            }
        }
        private static bool LlogaritKomponenteMuajiFormule(colKomponenteMuaji colmuaji, int muajiTjeter, int vitiTjeter, decimal vleraParametrit, bool vjenNgaKomponentja, bool vjenNgaMuajt, clsKomponentePage komponente, Dictionary<string, colKomponenteMuaji> vleraMujore, DateTime dtListpagese, decimal[] arrparam, int indexArrParam, bool dukeImportuar)
        {
            //Nese jemi ne muajin listpageses (colmuaji.Count == 0) ose ka vetem nje komponente qe eshte po ne ate muaj nuk vijojme me tutje
            if (colmuaji.Count == 0 || (colmuaji.Count == 1 && colmuaji.Exists(x => x.Muaji == muajiTjeter && x.Viti == vitiTjeter)))
            return false;

            if (vleraParametrit == 0 && vjenNgaKomponentja && !vjenNgaMuajt)
            {
                //nese perdoruesi vendos vleren 0 ath pastro gjithe vlerat per muajt
                komponente.Formula = "0";
                colmuaji = new colKomponenteMuaji();
                vleraMujore[komponente.Kodi] = colmuaji;
            }

           if (!dukeImportuar && MuajiINjejteMeMuajinEListpageses(muajiTjeter, vitiTjeter, dtListpagese))
           {
                //nese totali i vlerave te parametrave per muajt eshte != vlera qe vendos perdoruesi,kthejve vleren mbrapsht per te shmangur inkosistencen
                //kjo vlen vetem per rastin qe po vendoset vlera per muajin e listpageses,pasi aty mund te ndryshohet vlera e parametrit
                var paramTotal = colmuaji.Sum(x => x.VleraParam);
                if (paramTotal != vleraParametrit)
                    arrparam[indexArrParam] = paramTotal;
            }
            return true;
        }

        private static bool LlogaritKomponenteMuajiNumerike(colKomponenteMuaji colmuaji, int muajiTjeter, int vitiTjeter, decimal vlera, bool vjenNgaKomponentja, bool vjenNgaMuajt, clsKomponentePage komponente, Dictionary<string, colKomponenteMuaji> vleraMujore, DateTime dtListpagese, decimal[] arrVlera, int indexArrVlera, bool dukeImportuar)
        {
            //Nese jemi ne muajin listpageses (colmuaji.Count == 0) ose ka vetem nje komponente qe eshte po ne ate muaj nuk vijojme me tutje
            if (colmuaji.Count == 0 || (colmuaji.Count == 1 && colmuaji.Exists(x => x.Muaji == muajiTjeter && x.Viti == vitiTjeter)))
                return false;

            if (vlera == 0 && vjenNgaKomponentja && !vjenNgaMuajt)
            {
                colmuaji = new colKomponenteMuaji();
                vleraMujore[komponente.Kodi] = colmuaji;
            }

            if (!dukeImportuar && MuajiINjejteMeMuajinEListpageses(muajiTjeter, vitiTjeter, dtListpagese))
            {
                var vleraTotal = colmuaji.Sum(x => x.Vlera);
                if (vleraTotal != vlera)
                {
                    colmuaji = new colKomponenteMuaji();
                    vleraMujore[komponente.Kodi] = colmuaji;
                    arrVlera[indexArrVlera] = vlera;
                }
            }
            return true;                
        }

        private static bool MuajiINjejteMeMuajinEListpageses(int muajiTjeter, int vitiTjeter, DateTime dtListpagese)
        {
            return (muajiTjeter == dtListpagese.Month && vitiTjeter == dtListpagese.Year);
        }

        private static decimal LlogaritVlerenSipasFormules(decimal parametri, decimal vleraVjeter, int idGjuha, clsKomponentePage komponente)
        {
            decimal vlera;
            using (var dt = new DataTable())
            {
                try
                {
                    vlera = komponente.Formula != "" ? Convert.ToDecimal(dt.Compute(komponente.Formula, "")) : vleraVjeter;
                }
                catch (Exception ex)
                {
                    ImbLogger.LogErrorWebApi(ex.ToString());
                    if (idGjuha == 0)
                        throw new MyException($"Nuk mund te vlersohet formula {komponente.Formula} per komponenten {komponente.Kodi}", false);
                    throw new MyException($"Cannot evaluate the formula { komponente.Formula} for the component {komponente.Kodi}", false);
                }
            }

            return vlera;
        }

        /// <summary>
        ///     llogarit komponenet e importit te tipit vlere
        /// </summary>
        /// <param name="komp"></param>
        /// <param name="m"></param>
        /// <param name="arrvlera"></param>
        /// <param name="data"></param>
        /// <param name="muaji"></param>
        /// <param name="dimport"></param>
        /// <param name="colmuaji"></param>
        private static colKomponenteMuaji LlogaritKomponenteImportiVlere(clsKomponentePage komp, int m, DateTime data, int muaji, colKomponenteNr dimport, colKomponenteMuaji colmuaji)
        {
            foreach (var dr in dimport)
            {
                var muajiKomponentesImportuar = (dr.Muaji == 0 ? muaji : dr.Muaji);
                var vitiKomponentesImportuar = (dr.Viti == 0 ? data.Year : dr.Viti);

                var km = new clsKomponenteMuaji(0, m, muajiKomponentesImportuar, vitiKomponentesImportuar, 0, dr.Vlera, komp.Kodi, komp.Pershkrimi, komp.ParamEmri, komp.Njesi, komp.Tipi);

                colmuaji.RemoveAll(x => x.Muaji == km.Muaji && x.Viti == km.Viti);
                colmuaji.ShtoMeIdNegative(km);
            }
            return colmuaji;
        }
        private static decimal[] ZevendesoParametratSipasImportit(string[] arrKodi, decimal[] arrParam, colOreShtese importet)
        {
            foreach (var komponenteImporti in importet)
            {
                var index = Array.IndexOf(arrKodi, komponenteImporti.KodiKomponentes);
                if (index == -1) throw new MyException($"Komponentja {komponenteImporti.KodiKomponentes} nuk gjendet tek arrKodi");

                arrParam[index] = komponenteImporti.Totali;

            }
            return arrParam;
        }
        /// <summary>
        /// Zevendeson Komponenten param ne cdo komponente ku gjendet si pjese e formules me vleren perkatese.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="param"></param>
        /// <param name="vlera"></param>
        /// <param name="merrImporte"></param>
        /// <param name="colLp"></param>
        /// <param name="diteMuajiTeListepageses"></param>
        /// <param name="pageBazeLp"></param>
        private static void formula(colKomponentePage col, string param, string vlera, bool merrImporte, colKomponentePage colLp, decimal diteMuajiTeListepageses, decimal pageBazeLp)
        {
            foreach (var komponentePage in col.Where(komponentePage => komponentePage.Njesi != 0))
            {

                if (merrImporte && param.EqualsAnyIgnoreCase("PB", "DM"))
                {
                    var komp = colLp.Find(x => x.Kodi == komponentePage.Kodi);
                    if (param == "PB" && !komp.AplikoPageMuaji)
                        komponentePage.Formula = ZevendesoParameter(komponentePage.Formula, param, pageBazeLp.ToString());
                    else if (param == "DM" && !komp.AplikoDiteMuaji)
                        komponentePage.Formula = ZevendesoParameter(komponentePage.Formula, param, diteMuajiTeListepageses.ToString());
                    else
                        komponentePage.Formula = ZevendesoParameter(komponentePage.Formula, param, vlera);
                    continue;
                }


                komponentePage.Formula = ZevendesoParameter(komponentePage.Formula, param, vlera);
            }
        }

        private static string ZevendesoParameter(string formula, string parameter, string vlera)
        {
            var pattern = $@"\b{parameter}\b";
            var rgx = new Regex(pattern);
            formula = rgx.Replace(formula, vlera);
            return formula;
        }

        /// <summary>
        /// kryen llogaritjet ne parallel per nje list me punonjes
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dpZero"></param>
        /// <param name="nrDitesh"></param>
        /// <param name="idpunonjesish"></param>
        /// <param name="llogariDP"></param>
        /// <param name="muaji"></param>
        /// <param name="ditemuajindryshueshme"></param>
        /// <param name="kodi"></param>
        /// <param name="vjenNgaMuajt"></param>
        /// <param name="ditemuaji"></param>
        /// <param name="pagemuaji"></param>
        /// <param name="muajitjeter"></param>
        /// <param name="vititjeter"></param>
        /// <param name="merrimporte"></param>
        /// <param name="kodimporti"></param>
        /// <param name="vjenNgaKomponentja"></param>
        /// <param name="newrecord"></param>
        /// <param name="llogaritDiteLejeNgaImporti"></param>
        /// <param name="session"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="colFillestare"></param>
        /// <param name="arrKode"></param>
        /// <param name="mon"></param>
        /// <param name="kursinderm"></param>
        /// <param name="tatimet"></param>
        /// <param name="ndermarrje"></param>
        /// <param name="punonjesit"></param>
        /// <param name="dicColpaga"></param>
        /// <param name="dicColkomplistpage"></param>
        /// <param name="colPunesimeFundit"></param>
        /// <param name="colKompPageTeGjitha"></param>
        /// <param name="mesazhetPerPunonjes"></param>
        /// <param name="colFillestareNgaTrupiLp"></param>
        /// <param name="dicDiteTeHarxhuara"></param>
        /// <param name="colSigurime"></param>
        /// <param name="colSkemaSigurimesh"></param>
        /// <param name="kurset"></param>
        /// <param name="listOraret"></param>
        /// <param name="allOreShtesePunonjesish"></param>
        /// <param name="allKompNrPunonjesish"></param>
        /// <param name="dicPagaShtesa"></param>
        /// <param name="rillogarit"></param>
        /// <param name="idGjuha"></param>
        /// <param name="arrKodi"></param>
        /// <param name="colSkemaSigurimi"></param>
        /// <param name="dicDiteLeje"></param>
        /// <returns></returns>
        private static Tuple<List<object>, Dictionary<string, string>> LlogaritPagenPerPunonjesParallel(DateTime data, bool dpZero, double nrDitesh, List<int> idpunonjesish, bool llogariDP, int muaji, bool ditemuajindryshueshme, string kodi, bool vjenNgaMuajt, bool ditemuaji, bool pagemuaji, int muajitjeter, int vititjeter, bool merrimporte, string kodimporti, bool vjenNgaKomponentja, bool newrecord, bool llogaritDiteLejeNgaImporti, HttpContext context, int idNdermarrje, colKompListPagese colFillestare, string[] arrKode, clsMonedha mon, clsKurset kursinderm, colTatimet tatimet, clsNdermarrje ndermarrje, colPunonjes punonjesit, Dictionary<int, Dictionary<DateTime, colKompListPagese>> dicColpaga, Dictionary<int, colKompListPagese> dicColkomplistpage, colPunesim colPunesimeFundit, Dictionary<string, string> mesazhetPerPunonjes, Dictionary<int, colKompListPagese> colFillestareNgaTrupiLp, Dictionary<int, decimal> dicDiteTeHarxhuara, colSigurimet colSigurime, colSkemaSigurimi colSkemaSigurimesh, colKurset kurset, DataTable listOraret, Dictionary<int, colOreShtese> allOreShtesePunonjesish, Dictionary<int, colKomponenteNr> allKompNrPunonjesish, Dictionary<int, colPagaShtesa> dicPagaShtesa, bool rillogarit, int idGjuha, Dictionary<int, decimal> dicDiteLeje, bool kaListOrare, bool listPageseRe, bool merrVlereDefault)
        {
            ImbLogger.Info($"Lista e punonjesve qe po u llogaritet paga jane : {string.Join(",", punonjesit.Select(x => x.NrPersonal))}");
            var count = idpunonjesish.Count;
            var results = new ConcurrentBag<Tuple<int, object>>();
            var vleratMujorePerPunonjesDheKomponente = new cdPunonjesitMeVleratPerMuajt(context.Session);
            //  perdoret per te bere debug me lehtesi me nje thread
            var options = new ParallelOptions
            {
                //TODO beforeCOMMIT beje parallel
                MaxDegreeOfParallelism = count //beje 1 kur je ne debug / count ne release
            };
            try
            {
                Parallel.ForEach(punonjesit, options, punonjesi =>
                 {
                     try
                     {
                         HttpContext.Current = context;
                         if (listPageseRe && !punonjesi.Aktiv)
                         {
                             //nese eshte listpagese e re dhe punonjesi nuk eshte aktiv mos i llogarit te dhenat dhe mos shtohet ne vlerat e kthyera
                             punonjesi.Errors.AppendLine("Punonjesi nuk u shtua ne listapgese pasi nuk eshte me aktiv!");
                             return;
                         }

                         Dictionary<DateTime, colKompListPagese> colpaga = null;
                         if (!dicColpaga.TryGetValue(punonjesi.IdPunonjes, out colpaga))
                         {
                             punonjesi.Errors.AppendLine("Nuk ka komponente aktive per kete punonjes!");
                             return;
                         }
                         colKompListPagese colPagaSipasDates = null;
                         if (!colpaga.TryGetValue(data.Date, out colPagaSipasDates))
                         {
                             punonjesi.Errors.AppendLine($"Nuk u gjet asnje komponente listpagese per daten {data.Date.ToString("dd/MM/yyyy")} ");
                             return;
                         }
                         colKompListPagese colkomplistpage = null;
                         if (!dicColkomplistpage.TryGetValue(punonjesi.IdPunonjes, out colkomplistpage))
                         {
                             punonjesi.Errors.AppendLine($"Nuk u gjet asnje komponente listpagese e dukshme");
                             return;
                         }
                         string[] arrKodi = null;
                         if (arrKode != null)
                         {
                             arrKodi = new string[arrKode.Length];
                             Array.Copy(arrKode, arrKodi, arrKode.Length);
                         }
                         decimal[] arrvlera = null;
                         decimal[] arrvleraParam = null;
                         bool[] arrMosNdrysho = null;

                         if (rillogarit)
                         {
                             colKompListPagese colKompNgaTrupi = null;
                             if (!colFillestareNgaTrupiLp.TryGetValue(punonjesi.IdPunonjes, out colKompNgaTrupi))
                             {
                                 punonjesi.Errors.AppendLine("nuk u gjeten kompListpagese ne trupin e dokumentit per rillogaritje");
                                 return;
                             }
                             MbushArrayKomponenteshMeVlera(colKompNgaTrupi, colPagaSipasDates, dpZero, ref arrKodi, ref arrvleraParam, ref arrvlera, ref arrMosNdrysho, nrDitesh, true);
                         }
                         else
                             MbushArrayKomponenteshMeVlera(colFillestare, colPagaSipasDates, dpZero, ref arrKodi, ref arrvleraParam, ref arrvlera, ref arrMosNdrysho, nrDitesh, false);

                         var skemasig = colSkemaSigurimesh.FirstOrDefault(x => x.IdPunonjes == punonjesi.IdPunonjes);
                         if (skemasig == null || skemasig.IdSkemaSig == 0)
                         {
                             punonjesi.Errors.AppendLine("mungon skema e sigurimit");
                             return;
                         }

                         var sigurimet = colSigurime.FirstOrDefault(x => x.IdSigurime == skemasig.IdSigurimi);
                         if (sigurimet == null || sigurimet.IdSigurime == 0)
                         {
                             punonjesi.Errors.AppendLine("mungojne  sigurimit per punonjesin");
                             return;
                         }
                         var punfundit = colPunesimeFundit.FirstOrDefault(x => x.IdPunonjes == punonjesi.IdPunonjes);
                         if (punfundit == null)
                         {
                             punonjesi.Errors.AppendLine("mungon nje  punesim aktiv per punonjesin");
                             return;
                         }

                         decimal diteTeHarxhuara;
                         dicDiteTeHarxhuara.TryGetValue(punonjesi.IdPunonjes, out diteTeHarxhuara);

                         decimal diteLeje;
                         dicDiteLeje.TryGetValue(punonjesi.IdPunonjes, out diteLeje);

                         var kursiMonedhesSePunonjesit = kurset.FirstOrDefault(x => x.IdMonedha == punonjesi.IdMonedha) ?? new clsKurset { VleraKursi = 1 };

                         colOreShtese allOreShtesePunonjesi;
                         allOreShtesePunonjesish.TryGetValue(punonjesi.IdPunonjes, out allOreShtesePunonjesi);

                         colKomponenteNr allKompNrPunonjesi;
                         allKompNrPunonjesish.TryGetValue(punonjesi.IdPunonjes, out allKompNrPunonjesi);

                         colPagaShtesa pagaShtesaPunonjesi = null;
                         if (!dicPagaShtesa.TryGetValue(punonjesi.IdPunonjes, out pagaShtesaPunonjesi))
                         {
                             punonjesi.Errors.AppendLine("nuk jane marr komponentet e pagashtesa");
                             return;
                         }

                         //per tu hequr,aktualisht meqe hr nuk e perdor eshte vendosur ky kusht,qe te kalohet si param datatable bosh
                         var listorarePunonjesi = kaListOrare ? listOraret.Select("IDPUNONJESI=" + punonjesi.IdPunonjes).GetDataTable(listOraret) : listOraret;
                         var vleraMujore = (!vjenNgaKomponentja && newrecord) ? new Dictionary<string, colKomponenteMuaji>() : vleratMujorePerPunonjesDheKomponente.MerrVleratENjePunonjesi(punonjesi.IdPunonjes);

                         var muajTeLlogaritur = new colMuajTeLlogariturTrack { new MuajTeLlogariturTrack { muaji = muaji, llogaritur = false } };
                         var vleratELlogaritura = llogaritPage(arrvlera, arrvleraParam, arrKodi, arrMosNdrysho, data, data,llogariDP, punonjesi.NrPersonal, $"{punonjesi.Emer} {punonjesi.Mbiemer}", muaji, ditemuajindryshueshme, kodi, vjenNgaMuajt, ditemuaji, pagemuaji, muajitjeter, vititjeter, merrimporte, kodimporti, vjenNgaKomponentja, newrecord, llogaritDiteLejeNgaImporti, idNdermarrje, kursinderm, mon, punonjesi, tatimet, ndermarrje, skemasig, sigurimet, colpaga, colkomplistpage, punfundit, diteTeHarxhuara, kursiMonedhesSePunonjesit, listorarePunonjesi, allOreShtesePunonjesi, allKompNrPunonjesi, pagaShtesaPunonjesi, idGjuha, diteLeje, ref vleraMujore, muajTeLlogaritur, merrVlereDefault, data, false, false, 0, 0, null);

                         vleratMujorePerPunonjesDheKomponente.RuajVleratENjePunonjesiNeSession(vleraMujore, punonjesi.IdPunonjes);

                         results.Add(new Tuple<int, object>(punonjesi.IdPunonjes, vleratELlogaritura));
                     }
                     catch (Exception ex)
                     {
                         punonjesi.Errors.AppendLine(ex.Message);
                     }
                 });

                vleratMujorePerPunonjesDheKomponente.RuajGjitheVleratColKomponente();
                // results.CompleteAdding();
                ImbLogger.Info("Blloku i llogaritjes se pagave ne parallel perfundoi me sukses!");
            }
            catch (Exception ex)
            {
                ImbLogger.Error($"BLLOKU parallel :{ex.ToString()}");
            }
            var taskLogger = Task.Factory.StartNew(() =>
               {
                   foreach (var punonjes in punonjesit)
                   {
                       if (punonjes.Errors.Length == 0)
                           ImbLogger.LogErrorWebApi("paga u llogarit me sukses per punonjesin id:{0} nrPersonal : {1}!", punonjes.IdPunonjes, punonjes.NrPersonal);
                       else
                           ImbLogger.LogErrorWebApi("Nuk u llogarit paga per punonjesin id:{0} nrPersonal : {1} errors: {2}!", punonjes.IdPunonjes, punonjes.NrPersonal, punonjes.Errors.ToString());
                   }
               });

            //renditen sipas nr rendor dhe id  se punonjesit
            var teRenditura = (from lp in results
                               join p in punonjesit on lp.Item1 equals p.IdPunonjes
                               orderby p.NrRendor, p.IdPunonjes
                               select lp.Item2).ToList();
            foreach (var p in punonjesit.Where(p => p.Errors.Length > 0))
            {
                mesazhetPerPunonjes[p.NrPersonal] = p.Errors.ToString();
            }
            return new Tuple<List<object>, Dictionary<string, string>>(teRenditura, mesazhetPerPunonjes);
        }

        private static double MerrDitePuneNgaTrupiLp(colKompListPagese colFillestareNgaTrupiLp)
        {
            var ditePune = colFillestareNgaTrupiLp.FirstOrDefault(x => x.KodKomponente == "PP");
            return ditePune != null ? (double)ditePune.VleraParam : 0;
        }

        /// <summary>
        ///   kryen llogaritjet e komponenteve per punonjesit ne baze te komponenteve qe ai ka ne trupin e lp,ose kur punonjesit jane zgjedhur nga lupa duke u bazuar tek komponentet e mundshme
        /// </summary>
        /// <param name="data"></param>
        /// <param name="DPZero"></param>
        /// <param name="nrDitesh"></param>
        /// <param name="idpunonjesish"></param>
        /// <param name="llogariDP"></param>
        /// <param name="muaji"></param>
        /// <param name="ditemuajindryshueshme"></param>
        /// <param name="kodi"></param>
        /// <param name="vjenNgaMuajt"></param>
        /// <param name="ditemuaji"></param>
        /// <param name="pagemuaji"></param>
        /// <param name="muajitjeter"></param>
        /// <param name="vititjeter"></param>
        /// <param name="merrimporte"></param>
        /// <param name="kodimporti"></param>
        /// <param name="vjenNgaKomponentja"></param>
        /// <param name="newrecord"></param>
        /// <param name="llogaritDiteLejeNgaImporti"></param>
        /// <param name="Session"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="colFillestare"></param>
        /// <param name="colFillestareNGaTrupiLp"></param>
        /// <param name="arrKodi"></param>
        /// <param name="rillogarit"></param>
        /// <returns></returns>
        private static Tuple<List<object>, Dictionary<string, string>> LlogaritPerShumePunonjes(DateTime data, bool DPZero, double nrDitesh, List<int> punonjesIds, colPunonjes punonjesit, bool llogariDP, int muaji, bool ditemuajindryshueshme, string kodi, bool vjenNgaMuajt, bool ditemuaji, bool pagemuaji, int muajitjeter, int vititjeter, bool merrimporte, string kodimporti, bool vjenNgaKomponentja, bool newrecord, bool llogaritDiteLejeNgaImporti, int idNdermarrje, colKompListPagese colFillestare, Dictionary<int, colKompListPagese> colFillestareNGaTrupiLp, string[] arrKodi, bool rillogarit, int idGjuha, int idKokaLp, colPunesim colPunesimeFundit, bool listPageseRe, bool merrVlereDefault)
        {
            var mon = new clsMonedha(); mon.mbushMonedhen("LEK", idNdermarrje); //kursi i lekut ne kete ndermarje
            var kursinderm = new clsKurset(mon.IdMonedha, data);
            var ndermarrje = new clsNdermarrje(idNdermarrje);
            var myPunonjesCount = punonjesIds.Count;

            var kodeUnik = rillogarit ? colFillestareNGaTrupiLp.SelectMany(x => x.Value).Select(x => x.KodKomponente).Distinct().ToArray() : arrKodi;
            var currentContext = HttpContext.Current;
            DataTable listOraret = null;
            var tatimet = new colTatimet();
            colSigurimet colSigurime = null;
            colSkemaSigurimi colSkemaSigurimesh = null;
            colKurset kurset = null;
            Dictionary<int, decimal> dicDiteTeHarxhuara = null;
            Dictionary<int, colKompListPagese> dicColkomplistpage = null;
            Dictionary<int, Dictionary<DateTime, colKompListPagese>> dicColPaga = null;
            Dictionary<int, colKomponenteNr> dicKompNrPunonjesish = null;
            Dictionary<int, colPagaShtesa> dicPagaShtesa = null;
            Dictionary<int, decimal> dicDiteLeje = null;

            var allOreShtesePunonjesish = new colOreShtese(punonjesIds, data.Month, data.Year, idKokaLp).GrupoOreShteseSipasPunonjesit();
            var importet = allOreShtesePunonjesish.SelectMany(x => x.Value);
            var punonjesMeData = MerrPunonjesMeData(data, punonjesIds, importet, pagemuaji, data, muajitjeter, vititjeter);
            var kaListOrare = false;
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = myPunonjesCount // 1 ne debugg / myPunonjesCount ne release
            };
            ExecutionContext.SuppressFlow();
            Parallel.Invoke(options, () =>
            {
                try
                {
                    HttpContext.Current = currentContext;
                    dicColPaga = colKompListPagese.MerrDicKomponenteshPerPunonjesitSipasDataveBasicAsParallel(true, data, idNdermarrje, punonjesMeData, true, 16);
                    var allKompLp = new colKompListPagese(dicColPaga.SelectMany(x => x.Value).SelectMany(x => x.Value));
                    var kompPageIds = allKompLp.Select(x => x.IdKomponentePage).Distinct().ToList();
                    var colKompPage = new colKomponentePage(kompPageIds);
                    allKompLp.AsParallel().ForAll(x => x.komponentePage = colKompPage.FirstOrDefault(komp => x.IdKomponentePage == komp.IdKomponentePage)?.Clone());
                }
                catch (Exception ex)
                {
                    ImbLogger.LogErrorWebApi("Error Mbushja e dicColPaga ex {0}:", ex.ToString());
                }

            }, () =>
            {
                HttpContext.Current = currentContext;
                dicColkomplistpage = colKompListPagese.MerrListKomponenteshPerPunonjesitBasicAsParallel(punonjesIds, data, 4);
            }, () =>
            {
                HttpContext.Current = currentContext;
                dicDiteTeHarxhuara = clsKompListPagese.merrDiteTeHarxhuara(punonjesIds, data, idNdermarrje);
            }, () =>
            {
                HttpContext.Current = currentContext;
                colSigurime = new colSigurimet(idNdermarrje);
                if (colPunesimeFundit == null)
                    colPunesimeFundit = colPunesim.MerrPunesimTeFunditPerPunonjesit(punonjesIds, data);
            }, () =>
            {
                HttpContext.Current = currentContext;
                colSkemaSigurimesh = new colSkemaSigurimi(punonjesIds, data);
            }, () =>
            {
                HttpContext.Current = currentContext;
                kurset = new colKurset(idNdermarrje, data, new clsDatabaseAdmin());
            }, () =>
            {
                HttpContext.Current = currentContext;
                listOraret = colListOrare.ktheGjitheListOrariSipasPunonjesveSipasPeriudhes(punonjesIds, new DateTime(data.Year, muaji, 1), new DateTime(data.Year, muaji, DateTime.DaysInMonth(data.Year, muaji)));
                kaListOrare = listOraret.Rows.Count != 0;
            }, () =>
            {
                HttpContext.Current = currentContext;
                dicKompNrPunonjesish = new colKomponenteNr(punonjesIds, data.Month, data.Year, idKokaLp).GrupoOreShteseSipasPunonjesit();
            }, () =>
            {
                HttpContext.Current = currentContext;
                dicPagaShtesa = colPagaShtesa.merrPagaShtesaSipasPunonjesveDheDatesMeTeFundit(myPunonjesCount, punonjesMeData);
                var allKompLp = new colPagaShtesa(dicPagaShtesa.SelectMany(x => x.Value));
                var kompPageIds = allKompLp.Select(x => x.IdKomponentePage).Distinct().ToList();
                var colKompPage = new colKomponentePage(kompPageIds);
                allKompLp.AsParallel().ForAll(x => x.KomponentePage = colKompPage.FirstOrDefault(komp => x.IdKomponentePage == komp.IdKomponentePage)?.Clone());
            }, () =>
            {
                HttpContext.Current = currentContext;
                tatimet.merrTatimeNdermarjeSipasDatesDeri(idNdermarrje, data);
            }, () =>
            {
                HttpContext.Current = currentContext;
                dicDiteLeje = colDiteLeje.KtheGjitheDiteLejeSipasPunonjesveDhePeriudhes(punonjesIds, muaji.ToString(), data.Year, idKokaLp);
            });

            var mesazhetPerPunonjes = new Dictionary<string, string>(punonjesIds.Count);

            var rezultati = LlogaritPagenPerPunonjesParallel(data, DPZero, nrDitesh, punonjesIds, llogariDP, muaji, ditemuajindryshueshme, kodi, vjenNgaMuajt, ditemuaji, pagemuaji, muajitjeter, vititjeter, merrimporte, kodimporti, vjenNgaKomponentja, newrecord, llogaritDiteLejeNgaImporti, currentContext, idNdermarrje, colFillestare, kodeUnik, mon, kursinderm, tatimet, ndermarrje, punonjesit, dicColPaga, dicColkomplistpage, colPunesimeFundit, mesazhetPerPunonjes, colFillestareNGaTrupiLp, dicDiteTeHarxhuara, colSigurime, colSkemaSigurimesh, kurset, listOraret, allOreShtesePunonjesish, dicKompNrPunonjesish, dicPagaShtesa, rillogarit, idGjuha, dicDiteLeje, kaListOrare, listPageseRe, merrVlereDefault);

            return rezultati;
        }

        /// <summary>
        ///kthen nje list me kombinimin punonjes-Date per te kerkuar ne db te gjitha komponentet e lp per punononjesit per muajt ku ka te dhena
        /// </summary>
        /// <param name="data"></param>
        /// <param name="punonjesIds"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<PunonjesMeData> MerrPunonjesMeData(DateTime data, List<int> punonjesIds, IEnumerable<clsOreShtese> dt, bool pageMuaji, DateTime datePage, int muajiTjeter, int vitiTjeter)
        {
            //krijojme nje colleciton me <punonjes,data> per te marre te gjithe komponentet e listpageses qe kane punonjesit.
            var punonjesMeData = dt.Select(x => new PunonjesMeData
            {
                //marrim datat per te gjithe punonjesit ne varesi te vlerave te importuara nese kane
                IdPunonjesi = x.IdPunonjesi,
                Data = new DateTime(x.Viti, x.Muaji, DateTime.DaysInMonth(x.Viti, x.Muaji))
            }).Union(punonjesIds.Select(x => new PunonjesMeData
            {
                //lista e punonjesve me daten qe po behet LP
                IdPunonjesi = x,
                Data = data.Date
            }));

            if (pageMuaji && (data.Month != muajiTjeter || vitiTjeter != data.Year))
            {
                punonjesMeData = punonjesMeData.Union(punonjesIds.Select(x => new PunonjesMeData
                {
                    IdPunonjesi = x,
                    Data = Utils.MerrDatenFunditTeMuajit(vitiTjeter, muajiTjeter)
                }));
            }
            else
            {
                punonjesMeData = punonjesMeData.Union(punonjesIds.Select(x => new PunonjesMeData
                {
                    IdPunonjesi = x,
                    Data = datePage
                }));
            }
            return punonjesMeData.Distinct(new FuncEqualityComparer<PunonjesMeData>((a, b) => a.Data == b.Data && a.IdPunonjesi == b.IdPunonjesi)).ToList();
        }

        /// <summary>
        ///     mbush array me kodet,parametrat dhe dhe vlerat ne
        /// </summary>
        /// <param name="colFillestare"></param>
        /// <param name="colPaga"></param>
        /// <param name="DPZero"></param>
        /// <param name="arrKodi"></param>
        /// <param name="arrVleraParam"></param>
        /// <param name="arrvlera"></param>
        /// <param name="arrMosNdrysho"></param>
        /// <param name="rillogaritje"></param>
        /// <param name="nrDitesh"></param>
        private static void MbushArrayKomponenteshMeVlera(colKompListPagese colFillestare, colKompListPagese colPaga, bool DPZero, ref string[] arrKodi, ref decimal[] arrVleraParam, ref decimal[] arrvlera, ref bool[] arrMosNdrysho, double nrDitesh, bool rillogaritje)
        {
            var nrKomponentesh = colFillestare.Count;
            if (rillogaritje)
            {
                arrKodi = new string[nrKomponentesh];
                if (!DPZero)
                    nrDitesh = MerrDitePuneNgaTrupiLp(colFillestare);
            }

            arrMosNdrysho = new bool[nrKomponentesh];
            arrvlera = new decimal[nrKomponentesh];
            arrVleraParam = new decimal[nrKomponentesh];

            for (var k = 0; k < nrKomponentesh; k++)
            {
                var kompTmp = colFillestare[k];
                if (kompTmp.KodKomponente == "PP")
                {
                    if (rillogaritje && DPZero)
                        arrVleraParam[k] = 0;
                    else arrVleraParam[k] = (decimal)nrDitesh;
                }
                else
                    arrVleraParam[k] = kompTmp.VleraParam;

                arrvlera[k] = kompTmp.Vlera;
                if ((rillogaritje && DPZero) || !rillogaritje)
                    kompTmp.Modifikuar = false;
                if (rillogaritje)
                    arrKodi[k] = kompTmp.KodKomponente;
                arrMosNdrysho[k] = (rillogaritje && !DPZero) && !arrKodi[k].EqualsAnyIgnoreCase("PPS", "SPS", "SPD", "SNS", "SND", "SP", "SN", "TP", "PT");
            }
            
            arrKodi = Utils.MerrArrayMeKodeKomponenteTeRejaDheTeVjetra(arrKodi, colPaga);
            Array.Resize(ref arrVleraParam, arrKodi.Length);
            Array.Resize(ref arrvlera, arrKodi.Length);
            Array.Resize(ref arrMosNdrysho, arrKodi.Length);
        }
        
        public static AutoCompleteItem[] ktheACListePunonjesish(string kodi, int idNdermarrje)
        {
            //int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DataTable tmpTable = DbCore.DbListPagesat.colPunonjes.ktheACListePunonjesishLikeKodiEmerMbiemer(kodi, idNdermarrje);
            tmpTable.Columns["des"].ColumnName = "desc";
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return autoCompleteItem;
        }
        #endregion metoda private

        #region TEST METHODS



        private static Tuple<clsMesazh, colPunonjes> colPunonjesTest(int idNdermarje)
        {
            colPunonjes punonjesit = new colPunonjes();
            try
            {
                punonjesit = new colPunonjes(idNdermarje);
                if (punonjesit.Count == 0)
                {
                    return new Tuple<clsMesazh, colPunonjes>(new clsMesazh(TipMesazhi.Informim, "Nuk ka punonjes"), punonjesit);
                }
            }
            catch (Exception e)
            {
                return new Tuple<clsMesazh, colPunonjes>(new clsMesazh(TipMesazhi.Gabim, e.Message), punonjesit);
            }
            return new Tuple<clsMesazh, colPunonjes>(new clsMesazh(TipMesazhi.Sukses, "Punonjesit u morren me sukses"), punonjesit);
        }

        private static Tuple<clsMesazh, colPunesim> colPunesimeTest(int idpunonjes, string nrPersonal)
        {
            colPunesim punesime = new colPunesim();
            try
            {
                punesime = new colPunesim(idpunonjes);
                if (punesime.Count == 0)
                {
                    return new Tuple<clsMesazh, colPunesim>(new clsMesazh(TipMesazhi.Gabim, "punonjesi {0} Nuk ka punesime", nrPersonal), punesime);
                }
            }
            catch (Exception e)
            {
                return new Tuple<clsMesazh, colPunesim>(new clsMesazh(TipMesazhi.Gabim, "punonjesi :{0} gabimi :{}", nrPersonal, e.Message), punesime);
            }
            return new Tuple<clsMesazh, colPunesim>(new clsMesazh(TipMesazhi.Sukses, "Punonjesi {0} punesimet u morren me sukses"), punesime);
        }

        private static Tuple<clsMesazh, colKompListPagese> colKomponenteListpageseTest(int idPunonjes, string nrPersonal)
        {
            colKompListPagese punesime = new colKompListPagese();
            try
            {
                punesime = new colKompListPagese();
                if (punesime.Count == 0)
                {
                    return new Tuple<clsMesazh, colKompListPagese>(new clsMesazh(TipMesazhi.Gabim, "punonjesi {0} Nuk ka punesime", nrPersonal), punesime);
                }
            }
            catch (Exception e)
            {
                return new Tuple<clsMesazh, colKompListPagese>(new clsMesazh(TipMesazhi.Gabim, "punonjesi :{0} gabimi :{}", nrPersonal, e.Message), punesime);
            }
            return new Tuple<clsMesazh, colKompListPagese>(new clsMesazh(TipMesazhi.Sukses, "Punonjesi {0} punesimet u morren me sukses"), punesime);
        }

        #endregion TEST METHODS

        public static AutoCompleteItem[] ktheACListeKonfigurimeshUrdherPagese(string infixText, int lloj, int idNdermarrje)
        {
            DataTable tmpTable = DbCore.DbArkaBanka.colKonfigUrdherPagese.colKonfigUrdherPageseNew(idNdermarrje, lloj, infixText);
            tmpTable.Columns["des"].ColumnName = "desc";
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return autoCompleteItem;
        }

        

        public static AutoCompleteItem[] ktheACListeLlogarishBeginWith(string infixText, int idPerdoruesi, int idNderrmarje)
        {
            DataTable tmpTable = new DataTable();
            tmpTable = DbCore.DbKontabiliteti.colLlogarite.merrLLogariteNdermarrjesAndAutorizimeLikeNew(idNderrmarje, idPerdoruesi, infixText);
            //DataTable tmpTable = DbCore.DbKontabiliteti.colLlogarite.merrLLogariteNdermarrjesAndAutorizimeLikeNew(idNderrmarje, idPerdoruesi, infixText);
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];

            tmpTable.Columns["des"].ColumnName = "desc";

            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return autoCompleteItem;
        }

        public static DbCore.DbArkaBanka.clsKonfigUrdherPagese ktheVleraKonfigMeID(int idja)
        {
            DbCore.DbArkaBanka.clsKonfigUrdherPagese konfig = new DbCore.DbArkaBanka.clsKonfigUrdherPagese(idja);
            return konfig.Id > 0 ? konfig : null;
        }

        public static DbCore.DbArkaBanka.clsKonfigUrdherPagese ktheVleraKonfigMeKod(string kodi, int lloji, int idNderrmarje)
        {
            DbCore.DbArkaBanka.clsKonfigUrdherPagese konfig = new DbCore.DbArkaBanka.clsKonfigUrdherPagese(kodi, idNderrmarje, lloji);
            return konfig.Id > 0 ? konfig : null;
        }

        public static Object kthePunonjesMeKodRow(string nrPersonal, int idNdermarrje, int rreshti)
        {
            clsPunonjes pun = new clsPunonjes(nrPersonal, idNdermarrje);
            if (pun.IdPunonjes == 0)
                return new { idRreshti = rreshti, punonjesi = pun, llogaria = new clsLlogari() };
            clsLlogari llogariaPunonjesit = new clsLlogari(pun.IdLlogari);
            return new { idRreshti = rreshti, punonjesi = pun, llogaria = llogariaPunonjesit };
        }

       
        
        public static Object kthePunonjesMeIdRow(int idPunonjes, int rreshti)
        {
            clsPunonjes pun = new clsPunonjes(idPunonjes);
            if (pun.IdPunonjes == 0)
                return new { idRreshti = rreshti, punonjesi = pun, llogaria = new clsLlogari() };
            clsLlogari llogariaPunonjesit = new clsLlogari(pun.IdLlogari);
            return new { idRreshti = rreshti, punonjesi = pun, llogaria = llogariaPunonjesit };
        }

        public static Object KthePunonjesitMeIdRow(string idPunonjesish, int rreshti)
        {
            List<Object> result = new List<Object>();
            string[] idTe = idPunonjesish.Split(',');
            for (int i = 0; i < idTe.Length; i++)
            {
                if (idTe[i] != "")
                {
                    int idPun = Convert.ToInt32(idTe[i]);
                    clsPunonjes pun = new clsPunonjes(idPun);
                    clsLlogari llogariPunonjesi = new clsLlogari();
                    if (pun.IdPunonjes != 0)
                    {
                        llogariPunonjesi = new clsLlogari(pun.IdLlogari);
                        result.Add(new { punonjesi = pun, llogaria = llogariPunonjesi });
                    }
                }
            }
            return new { result = result, rreshti = rreshti };
        }

        public static clsLlogari KtheVleraLlogMeID(int idja)
        {
            clsLlogari llogaria = new clsLlogari(idja);
            return llogaria.IdLlogari > 0 ? llogaria : null;
        }

       
        public static clsLlogari ktheVleraLlogMeKod(string kodi, int idNderrmarje)
        {
            //clsLlogari llogaria = new clsLlogari(kodi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            clsLlogari llogaria = new clsLlogari();
            llogaria.merrLlogariAktiveSipasKodit(kodi, idNderrmarje);
            return llogaria.IdLlogari > 0 ? llogaria : null;
        }

        public static clsBankaPunonjes merrBankaPunonjes(int idpunonjes, DateTime data)
        {
            data = data.ToLocalTime();
            return new clsBankaPunonjes(idpunonjes, data);
        }
        public static object MerrTrupDokumentiListpagese(int idKoka, int idGjuha, int muaji, bool rimerrVlera, HttpSessionState session, bool llogaritDiteLejeNgaImporti, bool hiqPunonjesTelarguarKlonim, DateTime cmbData)
        {
            ImbLogger.Info("Filloi Leximi Vlerave ");
            var koka = new clsKokaListPagese(idKoka, true);
            var lpPerRillogaritje = koka.OColTrupi.MerrTrupDokumentiListPagese(koka.DtDok, session, hiqPunonjesTelarguarKlonim, cmbData);

            object rezultatIRillogaritur = null;
            if (rimerrVlera)
            {

                ImbLogger.Info("Filloi Rillogaritja : " + DateTime.Now);
                rezultatIRillogaritur = RimerrVlera(lpPerRillogaritje.Item4, false, koka.DtDok, false, null, muaji, true, "PP", false, false, false, muaji, koka.DtDok.Year, true, "", false, false, llogaritDiteLejeNgaImporti, lpPerRillogaritje.Item2, lpPerRillogaritje.Item1, koka.IdNdermarje, idGjuha, koka.IdKoka, lpPerRillogaritje.Item3, false);
                ImbLogger.Info("Mbaroi Rillogaritja : " + DateTime.Now);

            }
            ImbLogger.Info("Mbaroi Leximi i vlerave");
            return new
            {
                TeRillogaritura = rezultatIRillogaritur,
                TrupiDok = lpPerRillogaritje.Item5
            };

        }
        public static object RuajRegjistrimListPagese(HttpSessionState Session, string veprimi, string shtimModifikim, string dtDok, string dtRegjistrimi, string kontabilizim, string idNgaQueryString, int IdEtapa, string ServerUrl, string IdKonfigurimi, string KodKonfigurimi, string Vlefta, string Vlefta2, string NrDok,
            int Muaji, int IdMonedha, string KodMonedha, string Kursi, string Shenime, string GridDataObject, string HfKomp, int IdSkema, string Lidhur, string lblStatusAprovimi, string nrAutoNrDok,
            string hfStatus1, string pergjigja, int idGjuha, int idPerdoruesi, int idNdermarrjeVit, int idViti, int idNdermarrje, int idDepartamenti, int idNenDepartamenti, string departamenti, string nendepartamenti, string hfArkiva)
        {
            int Kontabilizim;
            int.TryParse(kontabilizim, out Kontabilizim);
            int IdNgaQueryString;
            int.TryParse(idNgaQueryString, out IdNgaQueryString);

            Dictionary<string, object> arkiva = JsonConvert.DeserializeObject <Dictionary<string, object>>(hfArkiva);

            ListePagesaRegjistrues lpRegjistrues = new ListePagesaRegjistrues
            {
                Veprimi = veprimi,
                ShtimModifikim = shtimModifikim,
                DtDok = Convert.ToDateTime(dtDok),
                DtRegjistrimi = Convert.ToDateTime(dtRegjistrimi),
                Rm = MessagesResource.CurrentResourceManager,
                Ci = MessagesResource.KtheCultureInfo(idGjuha),
                IdGjuha = idGjuha,
                IdPerdoruesi = idPerdoruesi,
                IdNdermarrjeVit = idNdermarrjeVit,
                IdViti = idViti,//#bug nese skadon sessioni jep error
                IdNdermarrje = idNdermarrje,
                Periudha = mySessionObjects.merrPeriudheKontabel(Session),
                IdDepartamenti = idDepartamenti,
                IdNenDepartamenti = idNenDepartamenti,
                Kontabilizim = Kontabilizim,
                IdNgaQueryString = IdNgaQueryString,
                IdEtapa = IdEtapa,
                ServerUrl = ServerUrl,
                IdKonfigurimi = IdKonfigurimi,
                KodKonfigurimi = KodKonfigurimi,
                Vlefta = Convert.ToDecimal(Vlefta),
                Vlefta2 = Convert.ToDecimal(Vlefta2),
                NrDok = NrDok,
                Muaji = Muaji,
                IdMonedha = IdMonedha,
                KodMonedha = KodMonedha,
                Kursi = Convert.ToDecimal(Kursi),
                Shenime = Shenime,
                GridDataObject = GridDataObject,
                HfKomp = HfKomp,
                IdSkema = IdSkema,
                Lidhur = Lidhur,
                lblStatusAprovimi = lblStatusAprovimi,
                nrAutoNrDok = nrAutoNrDok,
                departamenti = departamenti,
                nendepartamenti = nendepartamenti,
                HfArkiva = arkiva
            };

            return lpRegjistrues.Ruaj(Session, hfStatus1, pergjigja);
        }

        public static object FshiDokument(HttpSessionState Session, int[] ids, string guidString)
        {
            CultureInfo cultinf = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            clsMesazh mesazh = new clsMesazh();
            List<clsKokaListPagese> teFshire = new List<clsKokaListPagese>();
            List<string> tePaFshire = new List<string>(), 
                teLidhur = new List<string>(), 
                periudheKycur = new List<string>(), 
                procesAprovimi = new List<string>(),
                closedPeriod = new List<string>();
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            foreach (int id in ids)
            {
                clsKokaListPagese clsKoka = new clsKokaListPagese(Convert.ToInt32(id), false);
                bool lidhur = dbAdmin.eshteDokumentiILidhur(clsKoka.IdKoka, clsKoka.IdNivel, "T_KOKALISTPAGESE", "IDKOKA");
                if (lidhur)
                {
                    teLidhur.Add(clsKoka.NrDok);
                    continue;
                }
                if (clsKoka.StatusAprovimi != ((StatusAprovimi.Undefined)) && clsKoka.StatusAprovimi != ((StatusAprovimi.Aprovuar)))
                {
                    if (clsKoka.StatusAprovimi == (StatusAprovimi.Refuzuar))
                    {
                        DbCore.DbShare.clsKusht kusht = new DbCore.DbShare.clsKusht(clsKoka.IdKonfigAmbjente, "ZSP");

                        clsTrupiSkemaWorkFlow trup = new clsTrupiSkemaWorkFlow();
                        trup.merrTrupSipasKokesDhePerdoruesit(kusht.Vlera, idPerdoruesi);
                        if (trup.Niveli != 1)
                        {
                            procesAprovimi.Add(clsKoka.NrDok);
                            continue;
                        }
                    }
                    else
                    {
                        procesAprovimi.Add(clsKoka.NrDok);
                        continue;
                    }
                }
                bool ekycur = clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DtDok, idNdermarrje);
                if (ekycur)
                {
                    periudheKycur.Add(clsKoka.NrDok);
                    continue;
                }


                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DtDok, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.ListPagesa, clsKoka.IdKonfigAmbjente))
                {
                    closedPeriod.Add(clsKoka.NrDok);
                    continue;
                }

                clsKoka.IdPerdoruesi = idPerdoruesi;
                mesazh = clsKoka.fshi();
                if (mesazh.Status)
                    teFshire.Add(clsKoka);
                else
                    tePaFshire.Add(clsKoka.NrDok);
            }
            dbAdmin.Dispose();
            mySessionObjects.RemoveGridRowsInSessionById("GrideListepagese", guidString, "IdKoka", teFshire.Select(ks => ks.IdKoka).ToList());
            Tuple<string, string, string> mesazhetInformuese = clsFunksione.MesazhetInformuese(teFshire.Select(ks => ks.NrDok).ToList(), teLidhur, new List<string>(), periudheKycur, new List<string>(), procesAprovimi, tePaFshire, new List<string>(), new List<string>(), new List<string>(), new List<string>(), new Dictionary<string, List<string>>(), closedPeriod, rm, cultinf, new List<string>());
            return new
            {
                mesazhGabim = mesazhetInformuese.Item1,
                mesazhSukses = mesazhetInformuese.Item2,
                deletedKeys = teFshire.Select(ks => ks.IdKoka).ToArray()
            };            
        }

        public static object merrTeDhenaPerPunonjesDheListePagesatEShtesaPaga(int idkomp, string kodkonfi, int idpunonjes, int idndermarje, int gjuhe, bool klonim)
        {
            var result = merrDataKomponenteListPagesaDheShtesaPaga(idpunonjes, klonim);
            string lidhur = clsFunksione.eshteLidhur(idkomp, kodkonfi,Convert.ToString(idpunonjes), idndermarje, gjuhe);           
            string lidhurPunMeVepArkeBanke = clsPunonjes.eshteLidhurPunMeVepArkeBanke(idpunonjes).ToString();

            return new { lidhur = lidhur, result = result, lidhurPunMeVepArkeBanke = lidhurPunMeVepArkeBanke };
   

        }
    }
}