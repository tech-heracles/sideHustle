using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbArkaBanka
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje konfigurim udher pagese
    ///  (Te dhenat  merren nga tabela : T_konfigurdherpagese)
    /// </summary>
    public class clsKonfigUrdherPagese
    {
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se konfigurim udher pagese nga db-ja";
        private const string gabimEkzistencial = "ekziston nje grup me kete kod. Ju lutem shenoni nje tjeter!";
         private const string gabimEkzistencialTitulli = "ekziston nje titull me kete kod. Ju lutem shenoni nje tjeter!";
         private const string gabimEkzistencialKapitull = "ekziston nje kapitull me kete kod. Ju lutem shenoni nje tjeter!";

        #region Atributet

        private int id;
        private string kodi;
        private string pershkrimi;
        private int lloji;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKonfigUrdherPagese(int id, string kodi, string pershkrim,int lloji, int idperdoruesi, int idndermarje, int idstatusdok)
        {
           this. id = id;
            this.kodi = kodi;
            this.pershkrimi = pershkrim;
            this.lloji = lloji;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idndermarje;
            idStatusDok = idstatusdok;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKonfigUrdherPagese(string kodi, string pershkrim,int lloji, int idperdoruesi, int idndermarje, int idstatusdok)
        {
            this.kodi = kodi;
            this.pershkrimi = pershkrim;
            this.lloji = lloji;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idndermarje;
            idStatusDok = idstatusdok;
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i grupit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <param name="lloji">lloji</param>
        public clsKonfigUrdherPagese(string kodi, int idnderm, int lloji)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            mbushKonfigUrdherPagese(data.ktheKonfigUrdherPageseSipasKoditDheLlojit(kodi, idnderm, lloji));
            data.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e grupit</param>
        public clsKonfigUrdherPagese(int id)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            mbushKonfigUrdherPagese(data.merrKonfigUrdherPageseSipasId(id));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKonfigUrdherPagese()
        {
        }

        public clsKonfigUrdherPagese(DataRow rreshti)
        {
            
            mbushKonfigUrdherPagese(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodin .
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin .
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }
        /// <summary>
        /// kthen/vendos lloji
        /// <example>1-grup, 2-titull,3-kapitull</example>
        /// </summary>
        public int Lloji
        {
            get
            {
                return lloji;
            }
            set
            {
                lloji = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe celi
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos id e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        /// <summary>
        /// kthen vendos id e statusit te dokumentit
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        /// <summary>
        /// kthen  dt e krijimit 
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        /// <summary>
        /// kthen daten e modifikimit
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e konfig urdher pagese ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbRegjistrime.clsDatabaseArkaBanka.ruajGrupPunonjesish"/> 
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseArkaBanka db = new clsDatabaseArkaBanka();
            if (db.ekzistonKonfigUrdherPagese(kodi, idNdermarje,lloji))
                switch (lloji)
                {
                    case 1:
                        return new clsMesazh(false, gabimEkzistencial);
                    case 2:
                         return new clsMesazh(false, gabimEkzistencialTitulli);
                    case 3:
                        return new clsMesazh(false, gabimEkzistencialKapitull);
                    default:
                        break;
                } 
            clsMesazh u_ruajt = db.ruajKonfigUrdherPagese(out id, Kodi, Pershkrimi,Lloji, IdPerdoruesi, IdNdermarje, idStatusDok);
            db.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e konfig urdher pagese ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbRegjistrime.clsDatabaseArkaBanka.modifikoGrupPunonjesish"/> 
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh u_modifikua = data.modifikoKonfigUrdherPagese(Id, Kodi, Pershkrimi,Lloji, IdPerdoruesi, IdNdermarje, idStatusDok);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e Konfig urdherPagese ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.fshiGrupPunonjesish"/> 
        /// </summary>
        public clsMesazh fshi(int id, int idperdoruesi)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh u_fshi = data.fshiKonfigUrdherPageseStatus(id, idperdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// kontrollon nese ka veprime ke kete konfig
        /// </summary>
        /// <returns></returns>
        public bool kaVeprime()
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            bool kaVeprime = data.kaVeprime(id);
            data.Dispose();
            return kaVeprime;
        }

        /// <summary>
        /// kontrollon nese eksiton nje grup punonjesish me kete kodi
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">ndermarja</param>
        /// <param name="lloji">lloji</param>
        /// <returns></returns>
        public static bool ekziston(string kodi, int idndermarje, int lloji)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            bool ekziston = data.ekzistonKonfigUrdherPagese(kodi, idndermarje, lloji);
            data.Dispose();
            return ekziston;
        }
        #endregion

        #region Metoda Internal
        /// <summary>
        /// mbush grupin e punonjesve nga databaza
        /// </summary>
        /// <param name="dbDataRowKonfig"></param>
        /// <returns></returns>
        internal bool mbushKonfigUrdherPagese(DataRow dbDataRowKonfig)
        {
            if (dbDataRowKonfig != null)
            {
                try
                {
                    int.TryParse(dbDataRowKonfig["ID"].ToString(), out id);
                    kodi = dbDataRowKonfig["KODI"].ToString();
                    pershkrimi = dbDataRowKonfig["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowKonfig["LLOJI"].ToString(), out lloji);
                    int.TryParse(dbDataRowKonfig["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowKonfig["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowKonfig["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowKonfig["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKonfig["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(gabimNeTeDhena);
                }
            }
            else
                return false;
        }

        #endregion
    }
}

