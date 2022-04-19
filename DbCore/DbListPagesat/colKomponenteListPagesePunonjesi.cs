using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Web;
using System.Globalization;
using System.Resources;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKomponenteListPagesePunonjesi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsKomponenteListPagesePunonjesi"/>
    public class colKomponenteListPagesePunonjesi : List<clsKomponenteListPagesePunonjesi>, IDataBaseReader
    {
        private List<int> _idPunnonjesish;
        private DateTime _dtAktivizimi;
        #region Konstruktoret

        public colKomponenteListPagesePunonjesi()
        {
        }

        /// <summary>
        /// konstruktroi qe implementon klasen baze duke i dhene madhesine e koleksionit
        /// </summary>
        /// <param name="capacity">madhesia e koleksionit</param>
        public colKomponenteListPagesePunonjesi(int capacity)
            : base(capacity)
        {

        }

        public colKomponenteListPagesePunonjesi(DateTime dtAktivizimi)
        {
            _dtAktivizimi = dtAktivizimi;
        }
        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKomponenteListPagesePunonjesi</param>
        public colKomponenteListPagesePunonjesi(IEnumerable<clsKomponenteListPagesePunonjesi> collection)
            : base(collection)
        {

        }
        /// <summary>
        /// kontrukutori me dy parametra
        /// merr komponente list pagesa te nje punonjesi ne nje date te caktuar
        /// </summary>
        /// <param name="idpunonjes"> idpunonjes</param>
        /// <param name="data">data</param>
        public colKomponenteListPagesePunonjesi(int idpunonjes, DateTime data)
        {
            using (var db = new clsDatabazeListPagesa())
                db.ktheKomponenteListPagesePunonjesSipasPunonjesitDheDates(idpunonjes, data, this);
        }
        public colKomponenteListPagesePunonjesi(int idpunonjes, DateTime data, string kodiKomp)
        {
            using (var db = new clsDatabazeListPagesa())
                db.ktheKomponenteListPagesePunonjesSipasPunonjesitDheDates(idpunonjes, data, kodiKomp, this);
        }
        /// <summary>
        /// kontrukutori me 3 parametra
        /// merr komponentet e list pageses te nje punonjesi sipas llojit te komponentes ne nje date te caktuar fillestare pra vlerat e komponenteve jane zero
        /// </summary>
        /// <param name="idndermarje"> idndermarje</param>
        /// <param name="lloji">lloji i komponentes</param>
        /// <param name="data">data</param>
        /// <see cref="LlojKomponentePage"/>
        public colKomponenteListPagesePunonjesi(bool lloji, DateTime data, int idndermarje)

        {
            using (var db = new clsDatabazeListPagesa())
            {
                db.ktheKomponenteListPagesePunonjesiFillestare(lloji, data, idndermarje, this);
            }

        }
        public colKomponenteListPagesePunonjesi(List<int> idPunonjesish, DateTime dtAktivizimi, LlojKomponentePage llojKomponente)
        {


        }
        internal clsMesazh modifikoKomponente(clsDatabazeListPagesa db, clsPunonjes punonjesvjeter, colKomponenteListPagesePunonjesi kompfundit, int idPunonjes, int idPerdoruesi, System.Resources.ResourceManager rm, CultureInfo ci)
        {
            clsMesazh mesazh = new clsMesazh();
            string fushatmod = "";
            string mesazhmod = "";
            bool modifikuar = false;
            var dataKomponentesh = colKomponenteListPagesePunonjesi.merrDataKomponente(idPunonjes, false);
            if (punonjesvjeter.OColKomponente.Count > 0)
            {

                mesazhmod = clsKomponenteListPagesePunonjesi.kontrolloKomp(this, punonjesvjeter.OColKomponente, out fushatmod, out modifikuar, db);
                foreach (clsKomponenteListPagesePunonjesi komponente in punonjesvjeter.OColKomponente)
                {
                    mesazh = komponente.fshi(db, idPerdoruesi);
                    if (!mesazh.Status)
                        return new clsMesazh(false, rm.GetString("msgPunonjesPayRoll", ci));
                }
            }
            else if (kompfundit.Count > 0)
            {
                mesazhmod = clsKomponenteListPagesePunonjesi.kontrolloKomp(this, kompfundit, out fushatmod, out modifikuar, db);
            }
            else
            {
                mesazhmod = "U shtua komponente listpagese punonjes!";
                modifikuar = true;
            }
            if (modifikuar)
            {
                mesazh = EshteModifikimVlefshem(idPunonjes, dataKomponentesh, ci, rm);
                if (!mesazh) return mesazh;
            }
            mesazh = Ruaj(idPunonjes, rm, ci);
            if (!mesazh) return mesazh;
            if (mesazhmod != "U modifikuan fushat per komponente listpagese:")
            {
                mesazh = clsPunonjes.ruajLog(idPunonjes, idPerdoruesi, 1, mesazhmod, fushatmod, db);
                if (!mesazh.Status)
                    return new clsMesazh(false, rm.GetString("msgPunonjesRuajFail", ci));
            }
            return new clsMesazh(true, "Komponentet e punonjesit u ruajten me sukses!");
        }
        #endregion

        #region Metoda Publike

        public new clsKomponenteListPagesePunonjesi this[int index]
        {
            get { return ((clsKomponenteListPagesePunonjesi)base[index]); }
        }

        public static List<PunonjesMeData> MerrPunonjesMeDataAktivimiKomponentesh(List<int> idPunonjesish, DateTime dtAktivizimi, int idNdermarrje)
        {
            using (var dbLp = new clsDatabazeListPagesa())
            {
                return new List<PunonjesMeData>(dbLp.MerrIdPunonjesishMeDataAktivizimi(idPunonjesish, dtAktivizimi, idNdermarrje));
            }
        }

        public static List<PunonjesMeSigurime> MerrPunonjesMeIdSigurimiSipasDateAktivizimi(List<int> idPunonjesish, DateTime dtAktivizimi)
        {
            using (var dbLp = new clsDatabazeListPagesa())
            {
                return new List<PunonjesMeSigurime>(dbLp.MerrIdPunonjesishMeIdSigurimiSipasDateAktivizimi(idPunonjesish, dtAktivizimi));
            }
        }

        public clsMesazh RiruajSipasDatave(List<int> idPunonjesish, DateTime dtAktivizimiSkema, int idNdermarrje, int idPerdoruesi)
        {
            try
            {
                //marrim punonjesit me te gjitha dt qe ka komponente
                var punonjesMeData = MerrPunonjesMeDataAktivimiKomponentesh(idPunonjesish, dtAktivizimiSkema, idNdermarrje);
                var dicPd = punonjesMeData.GroupBy(x => x.IdPunonjesi).ToDictionary(k => k.Key, v => v.ToList());

                List<PunonjesMeSigurime> punonjesMeSigurime = MerrPunonjesMeIdSigurimiSipasDateAktivizimi(idPunonjesish, dtAktivizimiSkema);
                PunonjesMeSigurime sig;
                PunonjesMeSigurime sigDefault = PunonjesMeSigurime.MerrSigurimeSipasDatesMeTeAfert(idNdermarrje, dtAktivizimiSkema);

                CancellationTokenSource tokenSource = new CancellationTokenSource();
                var options = new ParallelOptions
                {
#if DEBUG
                    MaxDegreeOfParallelism = 1
#else
                    MaxDegreeOfParallelism=100
#endif
                };
                var punonjesitEPerfunduar = new List<int>();

                var teModifikuar = new ConcurrentBag<int>();
                using (var scope = new MyTransactionScope())
                {
                    var currentContext = HttpContext.Current;
                    Parallel.ForEach(dicPd, options, (punonjesiMeDatat, loopState) =>
                    {
                        HttpContext.Current = currentContext;
                        if (!loopState.IsStopped)
                        {
                            try
                            {
                                foreach (var p in punonjesiMeDatat.Value)
                                {
                                    colKomponenteListPagesePunonjesi col;
                                    //RASTI 1 dtPunonjesi>dtskeme
                                    if (p.Data >= dtAktivizimiSkema)
                                    {
                                        col = new colKomponenteListPagesePunonjesi(p.Data);
                                        col.ktheKomponenteListPagesePunonjesiSipasPunonjesitDatesLlojitNdermarjes(true, p.Data, idNdermarrje, p.Data, p.IdPunonjesi);

                                    }
                                    else
                                    {
                                        //rasti kur data e aktivizimit te komponenteve te ndermarrjes  eshte me e madhe
                                        //se ajo e punonjesit
                                        col = new colKomponenteListPagesePunonjesi(dtAktivizimiSkema);
                                        col.ktheKomponenteListPagesePunonjesiSipasPunonjesitDatesLlojitNdermarjes(true, dtAktivizimiSkema, idNdermarrje, p.Data, p.IdPunonjesi);
                                        sig = punonjesMeSigurime.FirstOrDefault(x => x.IdPunonjesi == p.IdPunonjesi);
                                        if (sig == null || sig.IdSigurime == 0)
                                            sig = sigDefault;
                                        if (sig.IdSigurime > 0 && ((sig.IdPunonjesi != 0 && sig.DtAktivizimi != dtAktivizimiSkema) || sig.IdPunonjesi == 0))
                                        {
                                            clsSkemaSigurimi skem = new clsSkemaSigurimi(0, p.IdPunonjesi, sig.IdSigurime, dtAktivizimiSkema, idPerdoruesi);
                                            var mesazhi = skem.ruaj();
                                        }
                                    }
                                    ///TO DO ruajtja
                                    var mesazh = col.ModifikoKomponentetPerPunonjes(p.IdPunonjesi, idPerdoruesi);
                                    //TO DO merr rezultatin kush u modifikua e kush jo
                                }
                            }
                            catch (Exception ex)
                            {
                                ImbLogger.Error(ex);
                                loopState.Stop();
                                throw;
                            }
                        }
                    });
                    scope.Complete();
                }
                return new clsMesazh(true, "Ndryshimi u aplikua me sukses per te gjithe punonjesit");

            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new clsMesazh(false, ex.Message);
            }

        }

        private clsMesazh ModifikoKomponentetPerPunonjes(int idPunonjesi, int idPerdoruesi)
        {

            using (var dbLp = new clsDatabazeListPagesa())
            {
                var dt = this.ToDataTable("Edetyrueshme", "IdKomponenteListPagesePunonjes", "IdPerdoruesi", "IdPunonjes", "IdKomponentePage", "DtAktivizimi", "Edukshme", "Njesia", "VleraDefault");
                return dbLp.ModifikoKomponentetPerPunonjes(_dtAktivizimi, idPunonjesi, idPerdoruesi, dt);
            }
        }

        public static DataTable merrKomponenteListPagesePunonjesiPerExport(int idndermarje)
        {
            using (clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa())
            {
                return dbKodifikimKF.merrKomponenteListPagesePunonjesiPerExport(idndermarje);
            }

        }
        /// <summary>
        /// merr datat ne te cilat kemi komponente list pagesa
        /// </summary>
        /// <param name="idpunonjesi">id e punonjes</param>
        /// <returns>liste me datat</returns>
        public static List<string> merrDataKomponenteListPagesa(int idpunonjesi, bool klonim)
        {
            return merrDataKomponente(idpunonjesi, klonim)?.OrderByDescending(x => x).Select(dt => dt.ToShortDateString()).ToList();
        }
        public static List<DateTime> merrDataKomponente(int idpunonjesi, bool klonim)
        {
            var list = new List<DateTime>();
            using (var db = new clsDatabazeListPagesa())
            {
                var dt = db.merrDataKomponenteListPageseSipasIdPunonjes(idpunonjesi);
                foreach (DataRow rreshti in dt.Rows)
                {
                    list.Add(DateTime.Parse(rreshti["DTAKTIVIZIMI"].ToString()).Date);
                    if (klonim) break;
                }
            }
            return list;
        }
        /// <summary>
        /// kthen nje kolekosion me komponente listpagese sipas idpunonjesi dates se aktivizimit dates se ndryshimit ndermarjes dhe llojit
        /// </summary>
        /// <param name="lloji"> lloji i komponentes</param>
        /// <param name="data">data e aktivzimit</param>
        /// <param name="idndermarje">id e nderamarjes</param>
        /// <param name="dtndryshimi"> data e ndryshimit</param>
        /// <param name="idpunonjes">id e punonjesit</param>
        /// <see cref="LlojKomponentePage"/>
        public void ktheKomponenteListPagesePunonjesiSipasPunonjesitDatesLlojitNdermarjes(bool lloji, DateTime data, int idndermarje, DateTime dtndryshimi, int idpunonjes)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheKomponenteListPagesePunonjesiSipasPunonjesitDatesLlojitNdermarjes(lloji, data, idndermarje, dtndryshimi, idpunonjes, this);
            }
        }
        public void ktheKomponenteListPagesePunonjesiSipasPunonjesitDatesMeTeFundit(DateTime data, int idpunonjes)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheKomponenteListPagesePunonjesiSipasPunonjesitDatesMeTeFundit(data, idpunonjes, this);
            }
        }
        public static List<string> ktheKomponenteListPagesePunonjesiTeDetyrueshme(DateTime data, int idpunonjes)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                return db.ktheKomponenteListPagesePunonjesiTeDetyrueshme(data, idpunonjes);
            }
        }

        public void Mbush(IDataRecord record)
        {
            var komp = new clsKomponenteListPagesePunonjesi(record);
            //bere me qellim per te marr nr rendor
            if (komp.IdKomponenteListPagesePunonjes == 0) komp.IdKomponenteListPagesePunonjes = Count + 1;
            Add(komp);

        }

        public colKomponenteListPagesePunonjesi Clone()
        {
            var clonedKompLp = new colKomponenteListPagesePunonjesi { Capacity = Count };
            ForEach(komp => clonedKompLp.Add(komp.Clone()));
            return clonedKompLp;
        }
        public bool Equals(colKomponenteListPagesePunonjesi colKomp)
        {
            foreach (var komp in this)
            {
                var pagaPerkatese = colKomp.FirstOrDefault(newKomp => komp.IdKomponentePage == newKomp.IdKomponentePage);
                if (pagaPerkatese.VleraDefault != komp.VleraDefault || pagaPerkatese.Edetyrueshme != komp.Edetyrueshme)
                    return false;
            }
            return true;
        }

        public clsMesazh Ruaj(int idPunonjes, System.Resources.ResourceManager rm, CultureInfo ci)
        {
            foreach (var komponente in this)
            {
                komponente.IdPunonjes = idPunonjes;
                var mesazh = komponente.ruaj();
                if (!mesazh) return new MesazhGabimi(rm.GetString("msgPunonjesPayRoll", ci));
            }
            return new MesazhSuksesi();
        }
        public clsMesazh EshteModifikimVlefshem(int idPunonjesi, List<DateTime> dataKomponentesh, System.Globalization.CultureInfo ci, ResourceManager rm)
        {
            //marrim te paren ne radhe

            var dtAktivizimi = this[0].DtAktivizimi;
            string listpagesa = rm.GetString("komplistepagesa", ci);
            return Utils.validoVlefshmerineEModifikimitTeKomponenteve(dtAktivizimi, idPunonjesi, dataKomponentesh, listpagesa, ci, rm);
        }
        #endregion
    }
}
