using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne sigurimet
    ///  (Te dhenat  merren nga tabela : T_SIGURIME)
    /// </summary>
    public class clsSigurimet
    {
        public static string mbushjeSukses = "Komponentja u mbush me sukses";
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se sigurimeve nga db-ja";
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        #region Atribute

        private int idSigurime;
        private string kodi;
        private DateTime data;
        private decimal pagaMin;
        private decimal pagaMax;
        private decimal sigShoqPun;
        private decimal sigShenPun;
        private decimal sigShoqNder;
        private decimal sigShenNder;
        private decimal total;
        private int model;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idKonfig;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private decimal pageMinShen;
        private decimal pageMaxShen;
        private decimal sigSupPun;
        private decimal sigSupNder;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsSigurimet()
        {
        }

        /// <summary>
        /// kontruktori me parametra
        /// </summary>
        /// <param name="idSigurime"> id e sigurimeve</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="data">data e aktivizimit</param>
        /// <param name="pagaMin">paga min </param>
        /// <param name="pagaMax"> paga max</param>
        /// <param name="sigShoqPun"> sig shoq pun</param>
        /// <param name="sigShenPun"> sig shen pun</param>
        /// <param name="sigShoqNder">sig shoq ndermarje</param>
        /// <param name="sigShenNder"> sig shen ndermarje</param>
        /// <param name="total">totali </param>
        /// <param name="model"> modeli 0 default 1-default i ndermarjes qe nuk mund te fshihet 2- nga perdoruesi qe mund te fshihet</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka bere veprimin</param>
        /// <param name="idNdermarje"> id e ndermarjes </param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok"> id e status dok 0-draft,1-ruajtur 2-fshire</param>
        public clsSigurimet(int idSigurime, string kodi, DateTime data, decimal pagaMin, decimal pagaMax, decimal sigShoqPun, decimal sigShenPun, decimal sigShoqNder, decimal sigShenNder, decimal total, int model, int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok, decimal pagaMinShen, decimal pagaMaxShen, decimal sigsuppun, decimal sigsupnder)
        {
            this.idSigurime = idSigurime;
            this.kodi = kodi;
            this.data = data;
            this.pagaMin = pagaMin;
            this.pagaMax = pagaMax;
            this.sigShoqPun = sigShoqPun;
            this.sigShenPun = sigShenPun;
            this.sigShoqNder = sigShoqNder;
            this.sigShenNder = sigShenNder;
            this.total = total;
            this.model = model;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idKonfig = idKonfig;
            this.idStatusDok = idStatusDok;
            this.pageMinShen = pagaMinShen;
            this.pageMaxShen = pagaMaxShen;
            this.sigSupPun = sigsuppun;
            this.sigSupNder = sigsupnder;
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="kodi">kodi i sigurimeve</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <param name="data">data e aktivizimit</param>
        public clsSigurimet(string kodi, int idNderm, DateTime data)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            if (!mbushSigurime(db.ktheSigurimeSipasKoditDheDates(kodi, idNderm, data)).Status)
                idSigurime = -1;
            db.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idSigurime">id sigurime</param>
        public clsSigurimet(int idSigurime)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushSigurime(db.ktheSigurime(idSigurime));
            db.Dispose();
        }
        public clsSigurimet(int idSigurime, clsDatabazeListPagesa db)
        {
            mbushSigurime(db.ktheSigurime(idSigurime));

        }

        public clsSigurimet(DataRow rreshti)
        {

            mbushSigurime(rreshti);
        }
        #endregion

        #region Properties

        /// <summary>
        /// id e sigurimeve
        /// </summary>
        public int IdSigurime
        {
            get
            {
                return idSigurime;
            }
            set
            {
                idSigurime = value;
            }
        }
        /// <summary>
        /// kodi i komponentes
        /// </summary>
        public string Kodi
        {
            get
            {
                return kodi;
            }
            set
            {
                kodi = value;
            }
        }
        /// <summary>
        /// paga minimale
        /// </summary>
        public decimal PagaMin
        {
            get
            {
                return pagaMin;
            }
            set
            {
                pagaMin = value;
            }
        }
        /// <summary>
        /// paga maksimale
        /// </summary>
        public decimal PagaMax
        {
            get
            {
                return pagaMax;
            }
            set
            {
                pagaMax = value;
            }
        }
        /// <summary>
        /// paga minimale e sigurimeve shendetsore
        /// </summary>
        public decimal PageMinShen
        {
            get
            {
                return pageMinShen;
            }
            set
            {
                pageMinShen = value;
            }
        }
        /// <summary>
        /// paga maksimale e sigurimeve shendetsore
        /// </summary>
        public decimal PageMaxShen
        {
            get
            {
                return pageMaxShen;
            }
            set
            {
                pageMaxShen = value;
            }
        }
        /// <summary>
        /// sigurimet shoqerore te punonjesit
        /// </summary>
        public decimal SigShoqPun
        {
            get
            {
                return sigShoqPun;
            }
            set
            {
                sigShoqPun = value;
            }
        }
        /// <summary>
        /// sigurimet shendetsor te punonjesit
        /// </summary>
        public decimal SigShenPun
        {
            get
            {
                return sigShenPun;
            }
            set
            {
                sigShenPun = value;
            }
        }
        /// <summary>
        /// sigurimet shoqerore ndermarje
        /// </summary>
        public decimal SigShoqNder
        {
            get
            {
                return sigShoqNder;
            }
            set
            {
                sigShoqNder = value;
            }
        }
        /// <summary>
        /// sigurimet shendetore te ndermarjes
        /// </summary>
        public decimal SigShenNder
        {
            get
            {
                return sigShenNder;
            }
            set
            {
                sigShenNder = value;
            }
        }
        public decimal SigSupNder
        {
            get
            {
                return sigSupNder;
            }
            set
            {
                sigSupNder = value;
            }
        }
        public decimal SigSupPun
        {
            get
            {
                return sigSupPun;
            }
            set
            {
                sigSupPun = value;
            }
        }
        /// <summary>
        /// totali i sigurimeve
        /// </summary>
        public decimal Total
        {
            get
            {
                return total;
            }
        }
        /// <summary>
        /// modeli
        /// <example> 0- modelet e sistemit qe perdoren per ndermarjet e reja 1- modelet e ndermarjes qe nuk fshihen 2- modelet e krijuar nga perdoruesi</example>
        /// </summary>
        public int Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }
        /// <summary>
        /// data e aktivizimit
        /// </summary>
        public DateTime Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        /// <summary>
        /// id e perdoruesit qe ka kryer veprimin
        /// </summary>
        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }
        /// <summary>
        /// id e ndermarjes 
        /// </summary>
        public int IdNdermarje
        {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje = value;
            }
        }
        /// <summary>
        /// id e konfigurimit te dokumentit
        /// </summary>
        public int IdKonfig
        {
            get
            {
                return idKonfig;
            }
            set
            {
                idKonfig = value;
            }
        }
        /// <summary>
        /// id e status te dok
        /// <example> 0 draft, 1-ruajtur,2 -fshire</example>
        /// </summary>
        public int IdStatusDok
        {
            get
            {
                return idStatusDok;
            }
            set
            {
                idStatusDok = value;
            }
        }
        /// <summary>
        /// data e krijimit te kompoentes
        /// </summary>
        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }
        }
        /// <summary>
        /// data e modifikimi te fundit te komponentes
        /// </summary>
        public DateTime DtModifikimi
        {
            get
            {
                return dtModifikimi;
            }

        }
        #endregion

        #region Metoda Publike
        /// <summary>
        /// ruan sigurimin
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo sigurimi apo jo</returns>
        public clsMesazh ruaj()
        {
            clsMesazh mesazh = new clsMesazh();
            int idsigurime = 0;
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            if (db.ekzistonSigurime(kodi, idNdermarje))
                return new clsMesazh(false, MessagesResource.Messages["msgEkzistenceSigurimiMeKodin"]);
            mesazh = db.ruajSigurime(out idsigurime, kodi, data, pagaMin, pagaMax, sigShoqPun, sigShenPun, sigShoqNder, sigShenNder, model, idKonfig, idPerdoruesi, idNdermarje, idStatusDok, pageMinShen, pageMaxShen, sigSupPun, sigSupNder);
            idSigurime = idsigurime;
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// ruan sigurime default
        /// </summary>
        /// <param name="idndermarje"> ndermarja</param>
        /// <param name="idperdoruesi">perdoruesi</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo sigurimi</returns>
        public static clsMesazh ruajDefault(int idndermarje, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mesazh = db.ruajDefaultSigurime(idperdoruesi, idndermarje);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// modifikon sigurime
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo sigurimi</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mesazh = db.modifikoSigurime(idSigurime, kodi, data, pagaMin, pagaMax, sigShoqPun, sigShenPun, sigShoqNder, sigShenNder, model, idKonfig, idPerdoruesi, idNdermarje, idStatusDok, pageMinShen, pageMaxShen, sigSupPun, sigSupNder);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin Sigurime ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            clsMesazh u_fshi = db.fshiSigurimeStatus(idSigurime, idPerdoruesi);
            db.Dispose();
            return u_fshi;
        }


        /// <summary>
        /// merr objektin sigurime sipas kodi dhe ndermarjes dhe dates
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="idndermarje">ndermarja</param>
        /// <param name="data">data</param>
        public static void merrSigurimeSipasKoditDheDates(string kodi, int idndermarje, DateTime data)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            db.ktheSigurimeSipasKoditDheDates(kodi, idndermarje, data);
            db.Dispose();
        }

        public void ktheSigurimeSipasDatesMeTeAfert(int idndermarje, DateTime data)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                mbushSigurime(db.ktheSigurimeSipasDatesMeTeAfert(idndermarje, data));
        }

        public void KtheSigurimeSipasDatesMeTeAfertPunonjes(int idndermarje, DateTime data, int idPunonjes)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                mbushSigurime(db.KtheSigurimeSipasDatesMeTeAfertPunonjes(idndermarje, data, idPunonjes));
        }

        public void ktheSigurimeSipasDatesMeTeAfert(int idndermarje, DateTime data, clsDatabazeListPagesa db)
        {
            mbushSigurime(db.ktheSigurimeSipasDatesMeTeAfert(idndermarje, data));
        }
  

        public clsMesazh RuajSipasDates(int idPunonjes, DateTime dtAktivizimi)
        {
            int idskemasig;
            using (var db = new clsDatabazeListPagesa())
                return db.ruajSkeme(out idskemasig, idPunonjes, IdSigurime, dtAktivizimi, idPerdoruesi);
        }

        /// <summary>
        /// kontrollon nese ekziston sigurimi me kete kod ne kete ndermarje ne kete date
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <param name="data">data</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonSigurimi(string kodi, int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool ekziston = db.ekzistonSigurime(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush sigurime me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushSigurime(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDSIGURIME"].ToString(), out idSigurime);
                    kodi = dbDataRow["KODI"].ToString();
                    decimal.TryParse(dbDataRow["PAGEMIN"].ToString(), out pagaMin);
                    decimal.TryParse(dbDataRow["PAGEMAX"].ToString(), out pagaMax);
                    decimal.TryParse(dbDataRow["SIGSHOQPUN"].ToString(), out sigShoqPun);
                    decimal.TryParse(dbDataRow["SIGSHOQNDER"].ToString(), out sigShoqNder);
                    decimal.TryParse(dbDataRow["SIGSHENPUN"].ToString(), out sigShenPun);
                    decimal.TryParse(dbDataRow["SIGSHENNDER"].ToString(), out sigShenNder);
                    decimal.TryParse(dbDataRow["TOTAL"].ToString(), out total);
                    DateTime.TryParse(dbDataRow["DATA"].ToString(), out data);
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["MODEL"].ToString(), out model);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    decimal.TryParse(dbDataRow["PAGEMINSHEN"].ToString(), out pageMinShen);
                    decimal.TryParse(dbDataRow["PAGEMAXSHEN"].ToString(), out pageMaxShen);
                    decimal.TryParse(dbDataRow["SIGSUPPUN"].ToString(), out sigSupPun);
                    decimal.TryParse(dbDataRow["SIGSUPNDER"].ToString(), out sigSupNder);
                    return new clsMesazh(true, clsSigurimet.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsSigurimet.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsSigurimet.drbosh);
        }

        /// <summary>
        /// mbush sigurimet kur te dhenat vijne nga dt e grides
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns>kthen nese mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushSigurimetNgaDT(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IdSigurime"].ToString(), out idSigurime);
                    kodi = dbDataRow["Kodi"].ToString();
                    decimal.TryParse(dbDataRow["PagaMin"].ToString(), out pagaMin);
                    decimal.TryParse(dbDataRow["PagaMax"].ToString(), out pagaMax);
                    decimal.TryParse(dbDataRow["SigShoqPun"].ToString(), out sigShoqPun);
                    decimal.TryParse(dbDataRow["SigShoqNder"].ToString(), out sigShoqNder);
                    decimal.TryParse(dbDataRow["SigShenPun"].ToString(), out sigShenPun);
                    decimal.TryParse(dbDataRow["SigShenNder"].ToString(), out sigShenNder);
                    decimal.TryParse(dbDataRow["Total"].ToString(), out total);
                    DateTime.TryParse(dbDataRow["Data"].ToString(), out data);
                    int.TryParse(dbDataRow["IdPerdoruesi"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["Model"].ToString(), out model);
                    int.TryParse(dbDataRow["IdNdermarje"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IdKonfig"].ToString(), out idKonfig);
                    int.TryParse(dbDataRow["IdStatusDok"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DtKrijimi"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DtModifikimi"].ToString(), out dtModifikimi);

                    return new clsMesazh(true, clsSigurimet.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsSigurimet.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsSigurimet.drbosh);
        }

        #endregion
    }
}
