using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using AlphaWeb.Core.SharedKernel;
using DbCore.IMBUtils.Logging;
using Newtonsoft.Json;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje konfigurim per nje numer
    ///  automatik.(Te dhenat  merren nga tabela : T_NRAUTOM)
    /// </summary>
    public class clsNrAutom
    {
        #region Atribute

        private int idNrAutom;
        private String kodiNrAutom;
        private String emertimiNrAutom;
        private Int64 fillonNrAutom;
        private Int64 mbaronNrAutom;
        private Int64 hapiNrAutom;
        private int drejtimiNrAutom;
        private DateTime ngaDataNrAutom;
        private DateTime deriMeNrAutom;
        private String majtasNrAutom;
        private String djathtasNrAutom;
        private int kategoriaNrAutom;
        private int periudhaNrAutom;
        private int gjatesiaNrAutom;

        private int idNdermarja;
        private int viti;
        //private int idNderViti;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int lajmeroPerparaNrFundit;
        private Int64 interval;
        private colNrAutomatikFundit oColNrAutoFundit;
        private DataRow rreshti;
        private clsDatabaseAdmin data;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsNrAutom(int idnrautom, String kodinrautom, String emertiminrautom,
                             int fillonnrautom,
                             int mbaronnrautom, int hapinrautom,
                             int drejtiminrautom, DateTime ngadatanrautom,
                             DateTime derimenrautom, String majtasnrautom, String djathtasnrautom, int kategoranrautom,
                             int periudhanrautom, int gjatesianrautom,
                             int idndermarja, int vit, int idperdoruesi, int idstatusdok, int lajmeroPerparaNrFundit, Int64 interval)
        {
            idNrAutom = idnrautom;
            kodiNrAutom = kodinrautom;
            emertimiNrAutom = emertiminrautom;
            fillonNrAutom = fillonnrautom;
            mbaronNrAutom = mbaronnrautom;
            hapiNrAutom = hapinrautom;
            drejtimiNrAutom = drejtiminrautom;
            ngaDataNrAutom = ngadatanrautom;
            deriMeNrAutom = derimenrautom;
            majtasNrAutom = majtasnrautom;
            djathtasNrAutom = djathtasnrautom;
            kategoriaNrAutom = kategoranrautom;
            periudhaNrAutom = periudhanrautom;
            gjatesiaNrAutom = gjatesianrautom;
            idNdermarja = idndermarja;
            viti = vit;
            //idNderViti = idndermvit;
            idPerdoruesi = idperdoruesi;
            idStatusDok = idstatusdok;
            this.interval = interval;
            this.lajmeroPerparaNrFundit = lajmeroPerparaNrFundit;
            //clsNrAutomatikFundit nr = new clsNrAutomatikFundit(idnrautom, ngadatanrautom, fillonnrautom, idperdoruesi, idndermarja, idstatusdok);
            oColNrAutoFundit = new colNrAutomatikFundit();
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsNrAutom(String kodinrautom, String emertiminrautom,
                             int fillonnrautom,
                             int mbaronnrautom, int hapinrautom,
                             int drejtiminrautom, DateTime ngadatanrautom,
                             DateTime derimenrautom, String majtasnrautom, String djathtasnrautom, int kategoranrautom,
                             int periudhanrautom, int gjatesianrautom,
                             int idndermarja, int vit, int idperdoruesi, int idstatusdok, int lajmeroPerparaNrFundit, Int64 interval
                        )
        {
            kodiNrAutom = kodinrautom;
            emertimiNrAutom = emertiminrautom;
            fillonNrAutom = fillonnrautom;
            mbaronNrAutom = mbaronnrautom;
            hapiNrAutom = hapinrautom;
            drejtimiNrAutom = drejtiminrautom;
            ngaDataNrAutom = ngadatanrautom;
            deriMeNrAutom = derimenrautom;
            majtasNrAutom = majtasnrautom;
            djathtasNrAutom = djathtasnrautom;
            kategoriaNrAutom = kategoranrautom;
            periudhaNrAutom = periudhanrautom;
            gjatesiaNrAutom = gjatesianrautom;
            idNdermarja = idndermarja;
            viti = vit;
            //idNderViti = idndermvit;
            idPerdoruesi = idperdoruesi;
            idStatusDok = idstatusdok;
            this.interval = interval;
            this.lajmeroPerparaNrFundit = lajmeroPerparaNrFundit;
        }

        public clsNrAutom(int id)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushNumerAuto(data.merrNrAutom(id), data);
            }
        }

        public clsNrAutom(clsDatabaseAdmin data, int id)
        {
            mbushNumerAuto(data.merrNrAutom(id), data);
        }

        public clsNrAutom(string kod, int idndermarje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushNumerAuto(data.merrNrAutom(kod, idndermarje), data);
            }
        }

        public clsNrAutom(string kod, int idndermarje, clsDatabaseAdmin data)
        {
            mbushNumerAuto(data.merrNrAutom(kod, idndermarje), data);
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsNrAutom()
        {
        }

        public clsNrAutom(DataRow rreshti, clsDatabaseAdmin data)
        {
            
            mbushNumerAuto(rreshti, data);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdNrAutom
        {
            get { return idNrAutom; }
            set { idNrAutom = value; }
        }

        public Int64 Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos kodin e numrit automatik.
        /// </summary>
        public String KodiNrAutom
        {
            get { return kodiNrAutom; }
            set { kodiNrAutom = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e numrit automatik.
        /// </summary>
        public String EmertimiNrAutom
        {
            get { return emertimiNrAutom; }
            set { emertimiNrAutom = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren me te cilin do filloj ky numer automatik. (Pra pjesen fikse te fillimit)
        /// </summary>
        public Int64 FillonNrAutom
        {
            get { return fillonNrAutom; }
            set { fillonNrAutom = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren me te cilin do mbaroje ky numer automatik. (Pra pjesen fikse te mbarimit)
        /// </summary>
        public Int64 MbaronNrAutom
        {
            get { return mbaronNrAutom; }
            set { mbaronNrAutom = value; }
        }


        /// <summary>
        /// Kthen/Vendos statusin qe tregon nese numrat qe do gjenerohen nga ky konfigurim do jene duke u rritur
        /// apo ne zbritje.
        /// </summary>
        public int DrejtimiNrAutom
        {
            get { return drejtimiNrAutom; }
            set { drejtimiNrAutom = value; }
        }

        /// <summary>
        /// Kthen/Vendos se me sa do rritet apo do zvogelohet numri pasardhes qe gjenerohet duke u bazuar tek ky 
        /// konfigurim.
        /// </summary>
        public Int64 HapiNrAutom
        {
            get { return hapiNrAutom; }
            set { hapiNrAutom = value; }
        }

        /// <summary>
        /// Kthen/Vendos stringun me te cilin do filloj ky numer automatik. (Pra pjesen fikse te fillimit)
        /// </summary>
        public DateTime NgaDataNrAutom
        {
            get { return ngaDataNrAutom; }
            set { ngaDataNrAutom = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten nga e cila ky konfigurim do filloj te jete aktiv.
        /// </summary>
        public DateTime DeriMeNrAutom
        {
            get { return deriMeNrAutom; }
            set { deriMeNrAutom = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten deri kur ky konfigurim do vazhdoje te jete aktiv.
        /// </summary>
        public String MajtasNrAutom
        {
            get { return majtasNrAutom; }
            set { majtasNrAutom = value; }
        }


        public String DjathtasNrAutom
        {
            get { return djathtasNrAutom; }
            set { djathtasNrAutom = value; }
        }


        public int KategoriaNrAutom
        {
            get { return kategoriaNrAutom; }
            set { kategoriaNrAutom = value; }
        }

        public int PeriudhaNrAutom
        {
            get { return periudhaNrAutom; }
            set { periudhaNrAutom = value; }
        }

        public int GjatesiaNrAutom
        {
            get { return gjatesiaNrAutom; }
            set { gjatesiaNrAutom = value; }
        }

        //public int IdNderViti
        //{
        //    get { return idNderViti; }
        //    set { idNderViti = value; }
        //}
        public int IdNdermarja
        {
            get { return idNdermarja; }
            set { idNdermarja = value; }
        }
        public int Viti
        {
            get { return viti; }
            set { viti = value; }
        }
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }
        /// <summary>
        /// Kthen / Vendos nr e dok te fundit kur nr automatik eshte drejt fundit. Persh nese eshte 10, ath per 10 dok e fundit do lajmerohet perdoruesi qe po perfundojne nr e dok
        /// </summary>
        public int LajmeroPerparaNrFundit
        {
            get { return lajmeroPerparaNrFundit; }
            set { lajmeroPerparaNrFundit = value; }
        }
        public colNrAutomatikFundit OColNrAutoFundit
        {
            get { return oColNrAutoFundit; }
            set { oColNrAutoFundit = value; }
        }
        #endregion

        #region Metoda Publike

        public static string merrVlerenNrAutomatik(int idNrAuto, DateTime date)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return merrVlerenNrAutomatik(idNrAuto, date, dbAdmin);
            }
        }

        public static string merrVlerenNrAutomatik(int idNrAuto, DateTime date, clsDatabaseAdmin dbAdmin)
        {
            return merrVlerenObjektNrAutomatik(idNrAuto, date, dbAdmin)["vleraPasardhese"].ToString();
        }

        public static Dictionary<string, Object> merrVlerenObjektNrAutomatik(int idNrAuto, DateTime date, clsDatabaseAdmin dbAdmin)
        {
            DbCore.DbAdmin.clsNrAutom nrAutom = DbCore.DbAdmin.clsNrAutom.merrNumrinAutomatikSipasId(idNrAuto, dbAdmin);
            return merrVlerenNrAutomatik(nrAutom, date, dbAdmin);
        }

        public static Dictionary<string, Object> merrVlerenNrAutomatik(clsNrAutom nrAutom, DateTime date)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return merrVlerenNrAutomatik(nrAutom, date, dbAdmin);
            }
        }
        
        public static Dictionary<string, Object> merrVlerenNrAutomatik(clsNrAutom nrAutom, DateTime date, clsDatabaseAdmin dbAdmin)
        {
            Dictionary<string, Object> rezultati = new Dictionary<string, object>();
            bool eshteAktivNrAutomatik = nrAutom.eshteAktivNrAutomatik(date, dbAdmin);
            rezultati.Add("eshteAktivNrAutomatik", eshteAktivNrAutomatik);
            if (eshteAktivNrAutomatik)
                rezultati.Add("vleraPasardhese", nrAutom.ktheVlerenParsardheseNrAutomatik(date, eshteAktivNrAutomatik, dbAdmin));
            else
                rezultati.Add("vleraPasardhese", String.Empty);
            return rezultati;
        }
       
        public static bool ekzistonNrAuto(string kodi, int idNdermarrje)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.ekzistonNrAutomatik(kodi, idNdermarrje);
            }
        }

        public static bool ekzistonNrAuto(string kodi, int idNdermarrje, clsDatabaseAdmin dbAdmin)
        {
            return dbAdmin.ekzistonNrAutomatik(kodi, idNdermarrje);
        }

        public static bool eshteLidhurMeAtributeTrupiPerMobile(int idNrAuto)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.eshteILidhurMeAtributeTrupiPerMobile(idNrAuto);
            }
        }

        public static int ktheIntervalSipasId(int idNrAuto)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.ktheIntervalSipasId(idNrAuto);
            }
        }

        public static string ktheKodNrAutoSipasId(int idNrAuto)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.ktheKodNrAutoSipasId(idNrAuto);
            }
        }


        public clsMesazh ruaj()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh;
            dbAdmin.beginTransaksion();
            try
            {
                if (ekzistonNrAuto(this.KodiNrAutom, this.idNdermarja, dbAdmin))
                {
                    return new clsMesazh(false, "Ekziston nje numer automatik me kete kod!");
                }
                int id = 0;
                mesazh = dbAdmin.ruajNrAutom(out id, this.KodiNrAutom, this.EmertimiNrAutom, this.FillonNrAutom, this.MbaronNrAutom, this.HapiNrAutom,
                            this.DrejtimiNrAutom, this.NgaDataNrAutom, this.DeriMeNrAutom, this.MajtasNrAutom, this.DjathtasNrAutom, this.KategoriaNrAutom, this.PeriudhaNrAutom,
                            this.GjatesiaNrAutom, this.IdNdermarja, this.Viti, this.IdPerdoruesi, this.idStatusDok, this.LajmeroPerparaNrFundit, this.interval);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                this.IdNrAutom = id;
                dbAdmin.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh mesazh;
            //data.krijoManager();
            data.beginTransaksion();
            try
            {
                mesazh = data.modifikoNrAutom(this.IdNrAutom, this.KodiNrAutom, this.EmertimiNrAutom, this.FillonNrAutom, this.MbaronNrAutom, this.HapiNrAutom, this.DrejtimiNrAutom, this.NgaDataNrAutom, this.DeriMeNrAutom, this.MajtasNrAutom, this.DjathtasNrAutom, this.KategoriaNrAutom, this.PeriudhaNrAutom, this.GjatesiaNrAutom, this.IdPerdoruesi, this.idStatusDok, this.LajmeroPerparaNrFundit, this.interval);
                if (!mesazh.Status)
                {
                    data.rollbackTransaksion();
                    return mesazh;
                }
                foreach (clsNrAutomatikFundit nr in this.oColNrAutoFundit)
                {
                    mesazh = nr.modifiko(data);
                    if (!mesazh.Status)
                    {
                        data.rollbackTransaksion();
                        return mesazh;
                    }
                }
                data.commitTransaksion();
                return mesazh;
            }
            catch (Exception ce)
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public static DataRow merrNrAutoSipasID(int id)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrNrAutom(id);
            }
        }

        public static clsNrAutom merrNumrinAutomatikSipasId(int id)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return merrNumrinAutomatikSipasId(id, data);
            }
        }

        public static clsNrAutom merrNumrinAutomatikSipasId(int id, clsDatabaseAdmin data)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merrNumrinAutomatikSipasId id = {id}");
            clsNrAutom nrAutomatik = new clsNrAutom();
            nrAutomatik.mbushNumerAuto(data.merrNrAutom(id), data);
            ImbLogger.LogTraceShitje($"Mbaroi metoda merrNumrinAutomatikSipasId id = {id}");
            return nrAutomatik;
        }

        /// <summary>
        /// Per daten qe i kalohet si parameter, kthen fillim e periudhes se numrit automatik se ciles ajo i perket
        /// </summary>
        /// <param name="dtAktuale"></param>
        /// <returns></returns>
        public DateTime ktheFillimPeriudheSipasDates(DateTime dtAktuale)
        {
            if (this.PeriudhaNrAutom == 1)
            {
                return dtAktuale;
            }
            //Nqs periudha eshte pa limit : Ne tabelen nrAutomatikFundit ka vetem nje rekord per kete nr automatik
            //Pra ne kete rast duhet te marrim kete numer te vetem
            else if (this.PeriudhaNrAutom == 4)
            {
                return this.NgaDataNrAutom;
            }
            else
            {
                //Nqs numri automatik ka periudhe mujore apo vjetore do kerkojme per vlera brenda periudhave te caktuara
                DateTime dtKerkimit;
                DateTime dtKerkimitF;
                //DateTime dtKerkimitM;
                //Nqs eshte periudha mujore do kerkojme per intervalin nje mujor te numrit automatik, se ciles i perket data aktuale
                if (this.PeriudhaNrAutom == 2)
                {
                    try
                    {
                        dtKerkimit = new DateTime(dtAktuale.Year, dtAktuale.Month, this.NgaDataNrAutom.Day);
                        if (this.NgaDataNrAutom.Day <= dtAktuale.Day)
                        {
                            dtKerkimitF = dtKerkimit;
                        }
                        else
                        {
                            dtKerkimitF = dtKerkimit.AddMonths(-1);
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        dtKerkimitF = new DateTime(this.NgaDataNrAutom.Year, this.NgaDataNrAutom.Month, this.NgaDataNrAutom.Day);
                    }
                }
                //Nqs eshte periudha vjetore do kerkojme per intervalin vjetor te numrit automatik, se ciles i perket data aktuale
                else
                {
                    try
                    {
                        dtKerkimit = new DateTime(dtAktuale.Year, this.NgaDataNrAutom.Month, this.NgaDataNrAutom.Day);
                        if (this.NgaDataNrAutom.DayOfYear <= dtAktuale.DayOfYear)
                        {
                            dtKerkimitF = dtKerkimit;
                        }
                        else
                        {
                            dtKerkimitF = dtKerkimit.AddYears(-1);
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        dtKerkimitF = new DateTime(this.NgaDataNrAutom.Year, this.NgaDataNrAutom.Month, this.NgaDataNrAutom.Day);
                    }
                }
                return dtKerkimitF;
            }
        }

        /// <summary>
        /// Per daten qe i kalohet si parameter, kthen mbarimin e periudhes se numrit automatik se ciles ajo i perket
        /// </summary>
        /// <param name="dtAktuale"></param>
        /// <returns></returns>
        public DateTime ktheMbarimPeridheSipasDates(DateTime dtAktuale)
        {
            if (this.PeriudhaNrAutom == 1)
            {
                return dtAktuale.AddDays(1);
            }
            //Nqs periudha eshte pa limit : Ne tabelen nrAutomatik te fundit ka vetem nje rekord per kete nr automatik
            //Pra ne kete rast duhet te marrim kete numer te vetem
            else if (this.PeriudhaNrAutom == 4)
            {
                return new DateTime(2222, 01, 01);
            }
            else
            {
                //Nqs numri automatik ka periudhe mujore apo vjetore do kerkojme per vlera brenda periudhave te caktuara
                DateTime dtKerkimit;
                DateTime dtKerkimitM;
                //Nqs eshte periudha mujore do kerkojme per intervalin nje mujor te numrit automatik, se ciles i perket data aktuale
                if (this.PeriudhaNrAutom == 2)
                {
                    int nrDiteshNeMuaj = DateTime.DaysInMonth(dtAktuale.Year, dtAktuale.Month);
                    int diteNga;
                    if (this.NgaDataNrAutom.Day > nrDiteshNeMuaj)
                        diteNga = nrDiteshNeMuaj;
                    else diteNga = this.NgaDataNrAutom.Day;
                    dtKerkimit = new DateTime(dtAktuale.Year, dtAktuale.Month, diteNga);

                    if (this.NgaDataNrAutom.Day <= dtAktuale.Day)
                    {
                        dtKerkimitM = dtKerkimit.AddMonths(1);
                    }
                    else
                    {
                        dtKerkimitM = dtKerkimit;
                    }
                }
                //Nqs eshte periudha vjetore do kerkojme per intervalin vjetor te numrit automatik, se ciles i perket data aktuale
                else
                {
                    try
                    {
                        dtKerkimit = new DateTime(dtAktuale.Year, this.NgaDataNrAutom.Month, this.NgaDataNrAutom.Day);
                        if (this.NgaDataNrAutom.DayOfYear <= dtAktuale.DayOfYear)
                        {
                            dtKerkimitM = dtKerkimit.AddYears(1);
                        }
                        else
                        {
                            dtKerkimitM = dtKerkimit;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        dtKerkimitM = new DateTime(this.NgaDataNrAutom.Year, this.NgaDataNrAutom.Month, this.NgaDataNrAutom.Day);
                    }
                }
                return dtKerkimitM;
            }
        }

        /// <summary>
        /// Fshirja e numrit automatik : Fillimisht fshihen numrat automatik te fundit te tij, e me pas vete numri
        /// </summary>
        /// <returns></returns>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh;
            dbAdmin.beginTransaksion();
            try
            {
                mesazh = dbAdmin.fshiGjithNrAutoFunditSipasNrAuto(this.IdNrAutom);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                mesazh = dbAdmin.fshiNrAutomStatus(this.IdNrAutom, this.IdPerdoruesi);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                dbAdmin.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Gjenerimi i numrit automatik per here te pare, per cdo fillim periudhe
        /// </summary>
        /// <returns></returns>
        public string gjeneroPerHereTePareNumrinAutomatik()
        {
            string s = "";
            int gjatesiaEZeroveQeDoShtohen = 0;
            if (this.GjatesiaNrAutom != 0)
            {
                gjatesiaEZeroveQeDoShtohen = this.GjatesiaNrAutom - this.MajtasNrAutom.Length - this.DjathtasNrAutom.Length
                                         - this.fillonNrAutom.ToString().Length;
            }
            string stringuMeZero = "";
            for (int i = 0; i < gjatesiaEZeroveQeDoShtohen; i++)
            {
                stringuMeZero = stringuMeZero + "0";
            }
            s = this.MajtasNrAutom + stringuMeZero + this.fillonNrAutom + this.DjathtasNrAutom;
            return s;
        }

        /// <summary>
        /// Per numrin automatik te dhene gjenerohet vlera pasardhese, ne varesi te vleres korrente qe i kalohet si paramter
        /// </summary>
        /// <param name="numriFundit"></param>
        /// <returns></returns>
        public string gjeneroNumrinAutomatikPasardhes(string numriFundit)
        {
            ImbLogger.LogTraceShitje("Filloi metoda gjenero gjeneroNumrinAutomatikPasardhes");
            string numriPasardhes = "";
            Int64 numriPasardhesSiNumer;
            string numriMesit = "";
            int gjatesiaEZeroveQeDoShtohen = 0;
            Int64 numriFunditSiNumer = Int64.Parse(numriFundit);
            int k = (int)DrejtimiNumraveAutomatike.Rrites;
            if (this.DrejtimiNrAutom == k)
            {
                numriPasardhesSiNumer = numriFunditSiNumer + this.HapiNrAutom;
            }
            else
            {
                numriPasardhesSiNumer = numriFunditSiNumer - this.HapiNrAutom;
            }
            if (this.GjatesiaNrAutom != 0)
            {
                gjatesiaEZeroveQeDoShtohen = this.GjatesiaNrAutom - this.MajtasNrAutom.Length - this.DjathtasNrAutom.Length - numriPasardhesSiNumer.ToString().Length;
            }
            string stringuMeZero = "";
            for (int i = 0; i < gjatesiaEZeroveQeDoShtohen; i++)
            {
                stringuMeZero = stringuMeZero + "0";
            }
            numriMesit = stringuMeZero + numriPasardhesSiNumer.ToString();
            numriPasardhes = this.MajtasNrAutom + numriMesit + this.DjathtasNrAutom;
            ImbLogger.LogTraceShitje($"Mbaroi metoda gjeneroNumrinAutomatikPasardhes dhe ktheu numriPasardhes:{numriPasardhes}");
            return numriPasardhes;
        }

        /// <summary>
        /// Therret funksionin ktheNrAutomatikFundit(DateTime dtAktuale, clsDatabaseAdmin db) pasi krijon nje objekt te tipit clsDatabaseAdmin
        /// </summary>
        /// <param name="dtAktuale"></param>
        /// <returns></returns>
        public DbCore.DbAdmin.clsNrAutomatikFundit ktheNrAutomatikFundit(DateTime dtAktuale)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            DbCore.DbAdmin.clsNrAutomatikFundit nrFundit = ktheNrAutomatikFundit(dtAktuale, db);
            db.Dispose();
            return nrFundit;
        }

        //NUK PERDOREJ
        ///// <summary>
        ///// Kthen numrin e fundit automatik qe eshte ruajtur ne DB per numrin autmatik, per peridhen 
        ///// se ciles i perket data qe i kalohet si paramater
        ///// </summary>
        ///// <param name="dtAktuale"></param>
        ///// <returns></returns>
        //public DbCore.DbAdmin.NrAuto merrVlerenNrAutomatik(string kodKontrolli, int idNrAuto, DateTime date)
        //{
        //    date = date.ToLocalTime();
        //    string vleraPasardhese = DbCore.DbAdmin.clsNrAutom.merrVlerenNrAutomatik(idNrAuto, date);
        //    return new DbCore.DbAdmin.NrAuto() { kodKontrolli = kodKontrolli, idNrAuto = idNrAuto, vlereNrAuto = vleraPasardhese };
        //    // return new string[] { kodKontrolli, idNrAuto.ToString(), vleraPasardhese, "false" };
        //}

        public DbCore.DbAdmin.clsNrAutomatikFundit ktheNrAutomatikFundit(DateTime dtAktuale, clsDatabaseAdmin db)
        {
            if (this.OColNrAutoFundit.Count == 0)
                return null;
            if (this.PeriudhaNrAutom == 4)
                return DbCore.DbAdmin.colNrAutomatikFundit.merrNrAutoFunditSipasNrAuto(this.IdNrAutom, db).First();

            DateTime dtKerkimitF = this.ktheFillimPeriudheSipasDates(dtAktuale).Date;
            DateTime dtKerkimitM = this.ktheMbarimPeridheSipasDates(dtAktuale).Date;

            //if (dtKerkimitF != null)
            //    dtKerkimitF = dtKerkimitF.Date;
            //if (dtKerkimitM != null)
            //    dtKerkimitM = dtKerkimitM.Date;

            try
            {
                return OColNrAutoFundit.Where(nr => nr.Data >= dtKerkimitF && nr.Data < dtKerkimitM).OrderByDescending(nr => nr.IdNrFunditAutomatik).FirstOrDefault();//TOCHECK KELVIN
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Per numrin autmatik te dhene, tkhen vleren pasardhese te tij, per periudhen se ciles i perket data qe i kalohet si parameter
        /// </summary>
        /// <param name="dtAktuale"></param>
        /// <returns></returns>
        public String ktheVlerenParsardheseNrAutomatik(DateTime dtAktuale)
        {
            //Kontrollohet nese ky numer automatik po perdoret per here te pare ose jo


            //Nese ka nje numer autmatik per kete date, gjenerohet vlera pasardhese   
            //Me kete kusht kontrollojme nese nuk ka mbaruar afati i perdorimit te numrit automatik
            //Nese ka mbaruar atehre do kthehet vetem nje vlere boshe, pra kontolli nuk do vazhdoj te jete me automatik
            //por lidhja e tij me nr automatik do mbetet nese behen shtime me data brenda afatit te perdorimit te nr automatik
            //if (!this.eshteAktivNrAutomatik(dtAktuale,null)) //kur nuk eshte aktiv 
            if (!this.eshteAktivNrAutomatik(dtAktuale)) //kur nuk eshte aktiv 
                return "";
            return ktheVlerenParsardheseNrAutomatik(dtAktuale, true);
        }

        public String ktheVlerenParsardheseNrAutomatik(DateTime dtAktuale, bool eshteAktivNrAutomatik)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return ktheVlerenParsardheseNrAutomatik(dtAktuale, eshteAktivNrAutomatik, dbAdmin);
            }
        }

        public String ktheVlerenParsardheseNrAutomatik(DateTime dtAktuale, bool eshteAktivNrAutomatik, clsDatabaseAdmin dbAdmin)
        {
            if (!eshteAktivNrAutomatik) //kur nuk eshte aktiv 
                return "";
            if (this.OColNrAutoFundit.Count == 0) // kur e kam per here te pare
                return this.gjeneroPerHereTePareNumrinAutomatik();
            //DbCore.DbAdmin.clsNrAutomatikFundit nrFundit = this.ktheNrAutomatikFundit(dtAktuale, null);
            DbCore.DbAdmin.clsNrAutomatikFundit nrFundit = this.ktheNrAutomatikFundit(dtAktuale, dbAdmin);
            return ktheVlerenParsardheseNrAutomatik(dtAktuale, eshteAktivNrAutomatik, nrFundit);
        }
        //bere overloaded metoda qe te ul aksesesin ne databaze ne rastin e serialeve
        public String ktheVlerenParsardheseNrAutomatik(DbCore.DbAdmin.clsNrAutomatikFundit nrFundit,DateTime dtAktuale, bool eshteAktivNrAutomatik, clsDatabaseAdmin dbAdmin)
        {
            if (!eshteAktivNrAutomatik) //kur nuk eshte aktiv 
                return "";
            if (this.OColNrAutoFundit.Count == 0) // kur e kam per here te pare
                return this.gjeneroPerHereTePareNumrinAutomatik();
            return ktheVlerenParsardheseNrAutomatik(dtAktuale, eshteAktivNrAutomatik, nrFundit);
        }
        public String ktheVlerenParsardheseNrAutomatik(DateTime dtAktuale, bool eshteAktivNrAutomatik, DbCore.DbAdmin.clsNrAutomatikFundit nrFundit)
        {
            ImbLogger.LogTraceShitje("Filloi metoda ktheVlerenPasaArdheseNrAutomatik");
            if (!eshteAktivNrAutomatik) //kur nuk eshte aktiv 
                return "";
            if (this.OColNrAutoFundit.Count == 0) // kur e kam per here te pare
                return this.gjeneroPerHereTePareNumrinAutomatik();
            //DbCore.DbAdmin.clsNrAutomatikFundit nrFundit = this.ktheNrAutomatikFundit(dtAktuale, null);            
            if (nrFundit == null) //kur e kam per here te pare per periudhen ku ndodhet data e dhene 
                return this.gjeneroPerHereTePareNumrinAutomatik();

            int karakteremajtas = this.MajtasNrAutom.Length; //rastet e tjera
            int karakteredjathtas = nrFundit.Vlera.Length - this.DjathtasNrAutom.Length - karakteremajtas;
            string vlera = nrFundit.Vlera.Substring(karakteremajtas, karakteredjathtas);
            string vlerapasardhese = this.gjeneroNumrinAutomatikPasardhes(vlera);
            if (this.gjatesiaNrAutom != 0)
            {
                if (this.gjatesiaNrAutom >= vlerapasardhese.Length)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi metoda ktheVlerenPasArdhese dhe ktheu vleren =>{vlerapasardhese}");
                    return vlerapasardhese;
                }
                else
                {
                    ImbLogger.LogTraceShitje($"Mbaroi metoda ktheVlerenPasArdhese dhe ktheu vleren =>{""}");
                    return "";
                }
            }
            else
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda ktheVlerenPasArdhese dhe ktheu vleren =>{vlerapasardhese}");
                return vlerapasardhese;
            } 
        }

        /// <summary>
        /// Kjo metode perdoret per te kontrolluar nese nje numer automatik vazhdon te jete aktiv apo jo, ne varesi te dates dhe vleres se tij te fundit
        /// Metoda kthen true nese eshte aktiv dhe false ne te kundert
        /// </summary>
        /// <param name="idNrAutom"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool eshteAktivNrAutomatik(DateTime data)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool sukses = eshteAktivNrAutomatik(data, db);
            db.Dispose();
            return sukses;
        }

        public static bool kontrolloEshteNrAutoDrejtFundit(int idNrAuto, DateTime data, ref long countNrFundit)
        {
            clsNrAutom nrAutom = clsNrAutom.merrNumrinAutomatikSipasId(idNrAuto);
            return kontrolloEshteNrAutoDrejtFundit(nrAutom, data, ref countNrFundit);
        }

        public static bool kontrolloEshteNrAutoDrejtFundit(clsNrAutom nrAutom, DateTime data, ref long countNrFundit)
        {            
            if (nrAutom.MbaronNrAutom == 0 || nrAutom.LajmeroPerparaNrFundit == 0)
                return false;
            int k = (int)DrejtimiNumraveAutomatike.Rrites;
            DbCore.DbAdmin.clsNrAutomatikFundit nrFundit = nrAutom.ktheNrAutomatikFundit(data);
            if (nrFundit != null)
            {
                if (nrAutom.DrejtimiNrAutom == k)
                    countNrFundit = (nrAutom.MbaronNrAutom - nrFundit.ktheVlerenNumerikeFundit(nrFundit.Vlera, nrAutom)) / nrAutom.HapiNrAutom;
                else
                    countNrFundit = (nrFundit.ktheVlerenNumerikeFundit(nrFundit.Vlera, nrAutom) - nrAutom.MbaronNrAutom) / nrAutom.HapiNrAutom;
                return countNrFundit <= nrAutom.LajmeroPerparaNrFundit;
            }
            else
            {
                countNrFundit = ((nrAutom.MbaronNrAutom - nrAutom.FillonNrAutom) / nrAutom.HapiNrAutom);
                return countNrFundit <= nrAutom.LajmeroPerparaNrFundit;
            }
        }

        /// <summary>
        /// Kjo metode perdoret per te kontrolluar nese nje numer automatik vazhdon te jete aktiv apo jo, ne varesi te dates dhe vleres se tij te fundit
        /// Metoda kthen true nese eshte aktiv dhe false ne te kundert
        /// </summary>
        /// <param name="idNrAutom"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool eshteAktivNrAutomatik(DateTime data, clsDatabaseAdmin db)
        {
            ImbLogger.LogTraceShitje("Filloi metoda per kontrollin nqs nje numer automatik eshte aktiv ose jo!");
            bool isValid = true;
            int k = (int)DrejtimiNumraveAutomatike.Rrites;
            Int64 vleraPasardhese;
            if (this.MbaronNrAutom != 0)
            {
                DbCore.DbAdmin.clsNrAutomatikFundit nrFundit = this.ktheNrAutomatikFundit(data, db);
                if (nrFundit != null)
                {
                    if (this.DrejtimiNrAutom == k)
                    {
                        vleraPasardhese = nrFundit.ktheVlerenNumerikeFundit(nrFundit.Vlera, this) + this.HapiNrAutom;
                        isValid = isValid && (vleraPasardhese <= this.MbaronNrAutom);

                    }
                    else
                    {

                        vleraPasardhese = nrFundit.ktheVlerenNumerikeFundit(nrFundit.Vlera, this) - this.HapiNrAutom;
                        isValid = isValid && (vleraPasardhese >= this.MbaronNrAutom);
                    }
                }
            }

            //if (this.PeriudhaNrAutom == 3) 
            //    isValid = isValid && this.NgaDataNrAutom.Date <= data.Date;
            //else
            isValid = isValid && (this.NgaDataNrAutom.Date <= data.Date) && (this.DeriMeNrAutom.Date >= data.Date);
            ImbLogger.LogTraceShitje($"Filloi metoda per kontrollin nqs nje numer automatik eshte aktiv ose jo mbaroi dhe ktheu :{isValid}");
            return isValid;     
        }

        /// <summary>
        /// kontrollon nese nr automatik eshte ndryshuar gjate kohes qe nga hapja e dokumentit deri ne ruajtjen e tij
        /// </summary>
        /// <param name="nrAuto"> nr auto</param>
        /// <param name="data">data</param>
        /// <param name="dbAdmin">db</param>
        /// <returns> kthen nr auton me nr e ri</returns>
        public NrAuto kontrolloNrAutomatik(clsDatabaseAdmin dbAdmin, NrAuto nrAuto, DateTime data)
        {
            ImbLogger.LogTraceShitje("Filloi metoda kontrolloNrAutomatik");
            bool eshteAktivNrAutomatik = this.eshteAktivNrAutomatik(data, dbAdmin);
            if (!eshteAktivNrAutomatik)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda kontrolloNrAutomatik dhe ktheu nrautomatik :{nrAuto}");
                return nrAuto;
            }
            DbCore.DbAdmin.clsNrAutomatikFundit nrFundit = this.ktheNrAutomatikFundit(data, dbAdmin);
            if (nrFundit == null)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda kontrolloNrAutomatik dhe ktheu nrautomatik :{nrAuto}");
                return nrAuto;
            }
            string vleraPasardheseNrAutomatik = this.ktheVlerenParsardheseNrAutomatik(data, eshteAktivNrAutomatik, nrFundit);
            if (nrAuto.vlereNrAuto.Equals(nrFundit.Vlera) //kane vlere te njejte
                || nrFundit.eshteNrParaardhes(nrAuto.vlereNrAuto, this) || vleraPasardheseNrAutomatik.Equals(nrAuto.vlereNrAuto))
            {
                if (!vleraPasardheseNrAutomatik.Equals(nrAuto.vlereNrAuto))
                    nrAuto.pershkrimMesazhi = nrAuto.kodKontrolli + "=" + vleraPasardheseNrAutomatik;
                else nrAuto.pershkrimMesazhi = "";
                //Nqs vlera e textbox-it == vleren e nr te fundit automatik athere do ruhet me vleren pasardhese
                nrAuto.vlereNrAuto = vleraPasardheseNrAutomatik;
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda kontrolloNrAutomatik dhe ktheu nrautomatik :{nrAuto}");
            return nrAuto;
        }

        /// <summary>
        /// kontrollon te gjithe numrat automatik te faqes nqs jane zene keta numra
        /// </summary>
        /// <param name="hfregjistrime">hidden fieldi me te dhenat</param>
        /// <param name="data">data</param>
        /// <returns> kthe nje liste me nr auto me numrat e fundit me te cilet duhet te ruhet dokumenti</returns>
        public static List<NrAuto> kontrollogjithenumrat(clsDatabaseAdmin dbAdmin, IDictionary<string, object> hfregjistrime, DateTime data)
        {
            var list = new List<NrAuto>();
            foreach (KeyValuePair<string, object> elem in hfregjistrime)
            {
                var nrAuto = NrAuto.LexoNrAuto(elem.Value.ToString());
                if (nrAuto.isModified)
                    continue;
                var nrauto = new clsNrAutom(dbAdmin, nrAuto.idNrAuto);
                nrAuto = nrauto.kontrolloNrAutomatik(dbAdmin, nrAuto, data);
                list.Add(nrAuto);
            }
            return list;
        }

        public static bool EshteNrAutoILidhur(int idNrAuto)
        {
            using (var dbAdmin = new clsDatabaseAdmin())
                return dbAdmin.eshteILidhurNrAutomatik(idNrAuto);
        }

        #endregion

        #region Metoda Internal

        internal bool mbushNumerAuto(DataRow dbDataRowNumerAuto, clsDatabaseAdmin db)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushNumerAuto!");
            if (dbDataRowNumerAuto != null)
            {
                try
                {
                    int.TryParse(dbDataRowNumerAuto["IDNRAUTOM"].ToString(), out idNrAutom);
                    kodiNrAutom = dbDataRowNumerAuto["KODINRAUTOM"].ToString();
                    emertimiNrAutom = dbDataRowNumerAuto["EMERTIMINRAUTOM"].ToString();
                    Int64.TryParse(dbDataRowNumerAuto["FILLONNRAUTOM"].ToString(), out fillonNrAutom);
                    Int64.TryParse(dbDataRowNumerAuto["MBARONNRAUTOM"].ToString(), out mbaronNrAutom);
                    Int64.TryParse(dbDataRowNumerAuto["HAPINRAUTOM"].ToString(), out hapiNrAutom);
                    int.TryParse(dbDataRowNumerAuto["DREJTIMINRAUTOM"].ToString(), out drejtimiNrAutom);
                    DateTime.TryParse(dbDataRowNumerAuto["NGADATA"].ToString(), out ngaDataNrAutom);
                    DateTime.TryParse(dbDataRowNumerAuto["DERIME"].ToString(), out deriMeNrAutom);
                    majtasNrAutom = dbDataRowNumerAuto["MAJTASNRAUTOM"].ToString();
                    djathtasNrAutom = dbDataRowNumerAuto["DJATHTASNRAUTOM"].ToString();
                    int.TryParse(dbDataRowNumerAuto["KATEGORIA"].ToString(), out kategoriaNrAutom);
                    int.TryParse(dbDataRowNumerAuto["PERIUDHANRAUTOM"].ToString(), out periudhaNrAutom);
                    int.TryParse(dbDataRowNumerAuto["GJATESIANRAUTOM"].ToString(), out gjatesiaNrAutom);
                    int.TryParse(dbDataRowNumerAuto["IDNDERMARJE"].ToString(), out idNdermarja);
                    int.TryParse(dbDataRowNumerAuto["VITI"].ToString(), out viti);
                    //int.TryParse(dbDataRowNumerAuto["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowNumerAuto["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowNumerAuto["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowNumerAuto["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNumerAuto["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    Int64.TryParse(dbDataRowNumerAuto["INTERVALI"].ToString(), out interval);
                    int.TryParse(dbDataRowNumerAuto["LAJMEROPARANRFUNDIT"].ToString(), out lajmeroPerparaNrFundit);
                    oColNrAutoFundit = DbCore.DbAdmin.colNrAutomatikFundit.merrNrAutoFunditSipasNrAuto(idNrAutom, db);
                    ImbLogger.LogTraceShitje("Mbaroi metoda mbushNumerAuto!");
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se nr automatik nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se nr automatik nga db-ja");
                }
            }
            else
            {
                ImbLogger.LogTraceShitje("Mbaroi metoda mbushNumerAuto!");
                return false;
            }
                
        }

        #endregion
    }
}