using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne sigurime suplementare
    ///  (Te dhenat  merren nga tabela : T_SIGURIMESUPLEMENTARE)
    /// </summary>
    public class clsSigurimeSuplementare
    {
        public static string mbushjeSukses = "Sigurimi u mbush me sukses";
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se sigurimeve suplementare nga db-ja";
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        private const string gabimEkzistence = "Ekziston nje sigurim ne kete kod. Ju lutem zgjidhni nje kod tjeter!";
        #region Atribute

        private int idSigurimeSuplementare;
        private string kodi;
        private string grupi;
        private decimal perqindja;
        private DateTime dtAktivizimi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string germa;
        private DataRow rreshti;

        
        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsSigurimeSuplementare()
        {
        }

        /// <summary>
        /// kontruktori me parametra
        /// </summary>
        /// <param name="idsigurim"> id e sigurimit</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="grupi">grupi</param>
        /// <param name="perqindja">perqindja  </param>
        /// <param name="dtaktivizimi"> dt e aktivizimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka bere veprimin</param>
        /// <param name="idNdermarje"> id e ndermarjes </param>
        /// <param name="idStatusDok"> id e status dok 0-draft,1-ruajtur 2-fshire</param>
        public clsSigurimeSuplementare(int idsigurim, string kodi, string grupi, decimal perqindja, DateTime dtaktivizimi, int idPerdoruesi, int idNdermarje, int idStatusDok, string germa)
        {
            this.idSigurimeSuplementare = idsigurim;
            this.kodi = kodi;
            this.grupi = grupi;
            this.perqindja = perqindja;

            this.dtAktivizimi = dtaktivizimi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;
            this.germa = germa;
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="kodi">kodi i shtesase</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <param name="tipi">tipi i pages</param>
        public clsSigurimeSuplementare(string kodi, int idNderm)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            if (!mbushSigurim(db.merrSigurimeSuplementareSipasKodit(kodi, idNderm )).Status)
                idSigurimeSuplementare = -1;
            db.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idsugurim">id sigurime</param>
        public clsSigurimeSuplementare(int idsugurim)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushSigurim(db.merrSigurimeSuplementare(idsugurim));
            db.Dispose();
        }

        public clsSigurimeSuplementare(DataRow rreshti)
        {
            
            mbushSigurim(rreshti);
        }
        #endregion

        #region Properties

        /// <summary>
        /// id e shtesa page
        /// </summary>
        public int IdSigurimeSuplementare
        {
            get
            {
                return idSigurimeSuplementare;
            }
            set
            {
                idSigurimeSuplementare = value;
            }
        }
        /// <summary>
        /// kodi 
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
        /// perqindja 
        /// </summary>
        public decimal Perqindja
        {
            get
            {
                return perqindja;
            }
            set
            {
                perqindja = value;
            }
        }

        /// <summary>
        /// dt e aktivizizmit
        /// </summary>
        public DateTime DtAktivizimi
        {
            get
            {
                return dtAktivizimi;
            }
            set
            {
                dtAktivizimi = value;
            }
        }
        /// <summary>
        /// grupi
        /// </summary>
        public string Grupi
        {
            get
            {
                return grupi;
            }
            set
            {
                grupi = value;
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

        /// <summary>
        /// Kthen/Vendos germen
        /// </summary>
        public string Germa
        {
            get { return germa; }
            set { germa = value; }
        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// ruan shtesa e pagese
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo sigurimi </returns>
        public clsMesazh ruaj()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            clsMesazh mesazh = new clsMesazh();
            int idshtesa = 0;
            if (db.ekzistonSigurimeSuplementare(kodi, idNdermarje))
                return new clsMesazh(false, gabimEkzistence);
            mesazh = db.ruajSigurimeSuplementare(out idshtesa, kodi, grupi, dtAktivizimi, perqindja, idPerdoruesi, idNdermarje, idStatusDok, germa);
            idSigurimeSuplementare = idshtesa;
            db.Dispose();
            return mesazh;
        }



        /// <summary>
        /// modifikon shtesane
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo sigurimi</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mesazh = db.modifikoSigurimeSuplementare(idSigurimeSuplementare, kodi, grupi, dtAktivizimi, perqindja, idPerdoruesi, idNdermarje, idStatusDok, germa);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin sigurim ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            clsMesazh u_fshi = db.fshiSigurimeSuplementare(idSigurimeSuplementare, idPerdoruesi);
            db.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin sigurim nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        public void merr()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            db.merrSigurimeSuplementare(idSigurimeSuplementare);
            db.Dispose();
        }


        /// <summary>
        /// Merr datatable sigurim  te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje datatable me te gjithe sigurimet e kesaj ndermarje</returns>
        public DataTable merriTeGjithe()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.ktheGjitheSigurimeSuplementareSipasNdermarrjes(idNdermarje);
            db.Dispose();
            return dt;            
        }

        /// <summary>
        /// kontrollon nese ekziston sigurimi me kete kod ne kete ndermarje 
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonSigurimeSuplementare(string kodi, int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool ekziston = db.ekzistonSigurimeSuplementare(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }
        /// <summary>
        /// kontrollon ka veprime me kete sigurimi
        /// </summary>

        /// <param name="idsigurimi">idsigurimi</param>
        /// 
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool kaVeprime(int idsigurimi,int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool kaVeprime = db.kaVeprimeSigurimeSuplementare(idsigurimi, idndermarje);
            db.Dispose();
            return kaVeprime;            
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush sigurimin me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushSigurim(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDSIGSUPLEMENTARE"].ToString(), out idSigurimeSuplementare);
                    kodi = dbDataRow["KODI"].ToString();
                    decimal.TryParse(dbDataRow["PERQINDJA"].ToString(), out perqindja);
                    grupi = dbDataRow["GRUPI"].ToString();
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(dbDataRow["DTAKTIVIZIMI"].ToString(), out dtAktivizimi);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    germa = Convert.ToString(dbDataRow["GERMA"]);

                    return new clsMesazh(true, clsSigurimeSuplementare.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsSigurimeSuplementare.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsSigurimeSuplementare.drbosh);
        }


        #endregion
    }
}

