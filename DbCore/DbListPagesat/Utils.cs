using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web.SessionState;
using DbCore.IMBUtils.Types;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///     kjo klase eshte pergjegjese per funksione te cilat lidhen me modulin e listpagesave dhe sherbejne si funksione
    ///     ndihmse  brenda klasave te ketij Moduli
    ///     KUJDES nuk lejohet te vendosen metoda qe bejne lexime ne DB,per leximin ne db jane pergjegjese klasat perkatese
    /// </summary>
    public class Utils
    {
        private const string VleratEMuajveSuffix = "colMuajiListpagesa";
        private const string VleratEMuajveCopySuffix = "colMuajiListpagesaCopy";
        public static Dictionary<int, Dictionary<string, colKomponenteMuaji>> MerrGjitheVleratPerMuajt(HttpSessionState session)
        {
            var obj = mySessionObjects.MerrNgaSession<Dictionary<int, Dictionary<string, colKomponenteMuaji>>>(session, VleratEMuajveSuffix);
            if (obj == null)
                return new Dictionary<int, Dictionary<string, colKomponenteMuaji>>();
            return KlonoVlera(obj);
        }
        public static Dictionary<int, Dictionary<string, colKomponenteMuaji>> MerrGjitheVleratPerMuajtOrigjinale(HttpSessionState session)
        {
            var obj = mySessionObjects.MerrNgaSession<Dictionary<int, Dictionary<string, colKomponenteMuaji>>>(session, VleratEMuajveCopySuffix) ?? new Dictionary<int, Dictionary<string, colKomponenteMuaji>>();
            return obj;
        }
        public static void RuajGjitheVleratColKomponenteMuaji(HttpSessionState session, Dictionary<int, Dictionary<string, colKomponenteMuaji>> teGjitha)
        {
            mySessionObjects.RuajNeSession(session, teGjitha, VleratEMuajveSuffix);
        }
        public static void RuajGjitheVleratColKomponenteMuajiCopy(HttpSessionState session, Dictionary<int, Dictionary<string, colKomponenteMuaji>> teGjitha)
        {
            mySessionObjects.RuajNeSession(session, teGjitha, VleratEMuajveCopySuffix);
        }
        public static void HiqVleratNgaSessioni(HttpSessionState session)
        {
            mySessionObjects.hiqObjectNeSesion(session, VleratEMuajveSuffix);
            mySessionObjects.hiqObjectNeSesion(session, VleratEMuajveCopySuffix);
        }


        /// <summary>
        ///     ruan ne session vlerat e punonjesit per komponente muaji
        /// </summary>
        /// <param name="session"></param>
        /// <param name="vlerat"></param>
        /// <param name="idPunonjesi"></param>
        public static void RuajVleratMujoreTeNjePunonjesiNeSession(HttpSessionState session, Dictionary<string, colKomponenteMuaji> vlerat, int idPunonjesi)
        {
            var teGjithevlerat = MerrGjitheVleratPerMuajt(session);
            teGjithevlerat[idPunonjesi] = vlerat;
            RuajGjitheVleratColKomponenteMuaji(session, teGjithevlerat);
        }
        public static void RuajVleratMujoreTeNjePunonjesiNeSessionOrigjinale(HttpSessionState session, int idPunonjesi)
        {
            var teGjithevlerat = MerrGjitheVleratPerMuajt(session);
            RuajGjitheVleratColKomponenteMuajiCopy(session, teGjithevlerat);
        }

        private static Dictionary<int, Dictionary<string, colKomponenteMuaji>> KlonoVlera(Dictionary<int, Dictionary<string, colKomponenteMuaji>> teGjithevlerat)
        {
            var tegjithaVleratCopy = new Dictionary<int, Dictionary<string, colKomponenteMuaji>>(teGjithevlerat.Count);
            foreach (var punonjesiMeVlera in teGjithevlerat)
            {
                tegjithaVleratCopy[punonjesiMeVlera.Key] = new Dictionary<string, colKomponenteMuaji>(punonjesiMeVlera.Value.Count);
                foreach (var item in punonjesiMeVlera.Value)
                {
                    tegjithaVleratCopy[punonjesiMeVlera.Key][item.Key] = item.Value.Clone();
                }
            }
            return tegjithaVleratCopy;
        }

        public static Dictionary<string, colKomponenteMuaji> MerrVleratMujoreTeNjePunonjesi(HttpSessionState session, int idPunonjesi)
        {
            var vlerat = MerrGjitheVleratPerMuajt(session);
            Dictionary<string, colKomponenteMuaji> objTmp;
            return vlerat.TryGetValue(idPunonjesi, out objTmp) ? objTmp : new Dictionary<string, colKomponenteMuaji>();
        }
        public static Dictionary<string, colKomponenteMuaji> MerrVleratMujoreTeNjePunonjesiOrigjinale(HttpSessionState session, int idPunonjesi)
        {
            var vlerat = MerrGjitheVleratPerMuajtOrigjinale(session);
            Dictionary<string, colKomponenteMuaji> objTmp;
            return vlerat.TryGetValue(idPunonjesi, out objTmp) ? objTmp : new Dictionary<string, colKomponenteMuaji>();
        }

        /// <summary>
        ///     kthen nje colKomponente muaji nga sessioni
        /// </summary>
        /// <param name="idPunonjesi"></param>
        /// <param name="kodKomponente"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static colKomponenteMuaji MerrColKomponenteMuaji(int idPunonjesi, string kodKomponente, HttpSessionState session)
        {
            var vlerat = MerrVleratMujoreTeNjePunonjesi(session, idPunonjesi);
            colKomponenteMuaji kompTmp;
            return vlerat.TryGetValue(kodKomponente, out kompTmp) ? kompTmp : new colKomponenteMuaji();
        }

        public static void HiqKomponenteMuajiNgaSessioniPerPunonjes(int idPunonjes, HttpSessionState session)
        {
            var teGjitha = MerrGjitheVleratPerMuajt(session);
            if (teGjitha.ContainsKey(idPunonjes))
                teGjitha.Remove(idPunonjes);
            RuajGjitheVleratColKomponenteMuaji(session, teGjitha);
        }

        /// <summary>
        ///     ruan ne session colkompmuaji ne session per punonjesin e dhene
        /// </summary>
        /// <param name="idPunonjesi"></param>
        /// <param name="kompMuaji"></param>
        /// <param name="kodKomp"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static void VendosColKomponentePerPunonjesDheKomponente(int idPunonjesi, colKomponenteMuaji kompMuaji, string kodKomp, HttpSessionState session)
        {
            var vlerat = MerrVleratMujoreTeNjePunonjesi(session, idPunonjesi);
            vlerat[kodKomp] = kompMuaji;
            RuajVleratMujoreTeNjePunonjesiNeSession(session, vlerat, idPunonjesi);
        }

        public static int BusinessDaysInMonth(int viti, int muaji)
        {
            return BusinessDaysInMonth(viti, muaji, 1, DateTime.DaysInMonth(viti, muaji));
        }

        public static int BusinessDaysInMonth(int viti, int muaji, int dtFillimi, int dtMbarimi)
        {
            DayOfWeek[] weekends = { DayOfWeek.Saturday, DayOfWeek.Sunday };
            var businessDaysInMonth = Enumerable.Range(dtFillimi, dtMbarimi).Where(d => !weekends.Contains(new DateTime(viti, muaji, d).DayOfWeek));
            return businessDaysInMonth.Count();
        }

        public static DateTime MerrDatenFunditTeMuajit(int viti, int muaji)
        {
            return new DateTime(viti, muaji, DateTime.DaysInMonth(viti, muaji));
        }


        /// <summary>
        ///     nga gjithe komponentet e pages per te gjithe punonjesit kthen nje colKomponentePage ne baze te id-ve
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="colKompPage"></param>
        /// <returns></returns>
        public static colKomponentePage MerrKomponentePage(List<int> ids, colKomponentePage colKompPage)
        {
            var kompPage = new colKomponentePage();
            kompPage.AddRange(ids.Select(id => colKompPage.Find(x => x.IdKomponentePage == id).Clone()));

            return kompPage;
        }


        /// <summary>
        ///     merr si parameter numrat personal dhe nje collection me punonjesit dhe kthen id e tyre
        /// </summary>
        /// <param name="personalNrs"></param>
        /// <param name="punonjesit"></param>
        /// <returns></returns>
        public static List<int> MerrIdPunonjesishSipasNumravePersonal(string[] personalNrs, colPunonjes punonjesit)
        {
            var punonjesIds = new List<int>(personalNrs.Length);
            punonjesIds.AddRange(personalNrs.Select(t => punonjesit.Find(x => x.NrPersonal == t).IdPunonjes));
            return punonjesIds;
        }

        /// <summary>
        ///kthen gjithe kodet e komponenteve te mundshme per kete listpagese
        /// </summary>
        /// <param name="arrkodi"></param>
        /// <param name="colpaga"></param>
        public static string[] MerrArrayMeKodeKomponenteTeRejaDheTeVjetra(string[] arrkodi, colKompListPagese colpaga)
        {
            return arrkodi.Union(colpaga.Select(x => x.KodKomponente), new FuncEqualityComparer<string>((a, b) => a == b)).ToArray();
        }

        public static string[] KtheArrayMeKodeKomponentesh(colKompListPagese colFillestare)
        {
            var count = colFillestare.Count;
            var arrKodi = new string[count];
            for (var j = 0; j < count; j++)
            {
                arrKodi[j] = colFillestare[j].KodKomponente;
            }
            return arrKodi;
        }


        public static Dictionary<int, colOreShtese> GrupoKomponenteTeImportuaraSipasMuajve(int muajiLp, int vitiLp, colOreShtese komponenteTeImportuara, clsKomponentePage komp)
        {
            komponenteTeImportuara.ForEach(checkVlera =>
            {
                if (checkVlera.Muaji == 0)
                    checkVlera.Muaji = muajiLp;
                if (checkVlera.Viti == 0)
                    checkVlera.Viti = vitiLp;
            });
            return komponenteTeImportuara.GroupBy(x => x.Muaji).ToDictionary(v => v.Key, v => new colOreShtese(v));//.Where(k => k.KodiKomponentes != komp.Kodi)
        }
        public static clsMesazh validoVlefshmerineEModifikimitTeKomponenteve(DateTime dtAktivizimi, int idPunonjesi, List<DateTime> dataKomponentesh, string llojKomp, System.Globalization.CultureInfo ci, ResourceManager rm)
        {

            DateTime dtPasardhese = MerrDatenMeTeAfertPasardhese(dtAktivizimi, dataKomponentesh);
            if (!clsKokaListPagese.ekzistonListePagesaPerPunonjes(dtAktivizimi, dtPasardhese, idPunonjesi))
                return new MesazhSuksesi();
            string mesazh;
            mesazh = rm.GetString("msgKomponenteModifikim", ci);
            mesazh = mesazh.Replace("#llojKomp#", llojKomp);
            return new MesazhGabimi(mesazh);
        }

        public static DateTime MerrDatenMeTeAfertPasardhese(DateTime dtAktivizimi, List<DateTime> dataKomponentesh)
        {
            DateTime dtPasardhese = dataKomponentesh.OrderBy(x => x).FirstOrDefault(x => x > dtAktivizimi);
            if (DateTimeUtil.EshteNullOrDefault(dtPasardhese))
            {
                dtPasardhese = DateTime.Now.AddYears(1000);
            }
            return dtPasardhese;
        }
    }
}