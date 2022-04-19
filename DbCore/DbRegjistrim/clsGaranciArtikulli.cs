using System;
using System.Data;
using static System.Convert;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne garancine e artikujve
    ///  (Te dhenat  merren nga tabela : T_GARANCIAART)
    /// </summary>
    public class clsGaranciArtikulli
    {
        #region Ctor

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsGaranciArtikulli()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id </param>
        public clsGaranciArtikulli(int id)
        {
            using (var db = new clsDatabaseRegjistrim())
                Mbush(db.ktheGaranciArtikulliSipasId(id));
        }

        public clsGaranciArtikulli(string kodi, string detajim)
        {
            using (var db = new clsDatabaseRegjistrim())
                Mbush(db.ktheGaranciArtikulliSipasKoditOseDetajimit(kodi, detajim));
        }

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idgarancia"></param>
        /// <param name="kodi"></param>
        /// <param name="idnivel"></param>
        /// <param name="dtdok"></param>
        /// <param name="krijuesi"></param>
        /// <param name="kodklienti"></param>
        /// <param name="kontakti"></param>
        /// <param name="idartikulli"></param>
        /// <param name="detajim"></param>
        /// <param name="idmagazina"></param>
        /// <param name="idndermarje"></param>
        /// <param name="idkokashitje"></param>
        public clsGaranciArtikulli(int idgarancia, string kodi, int idnivel, DateTime dtdok, string krijuesi, string kodklienti, string kontakti, int idartikulli, string detajim, int idmagazina, int idndermarje, int idkokashitje)
        {
            IdGarancia = idgarancia;
            Kodi = kodi;
            IdNiveli = idnivel;
            DtDok = dtdok;
            Krijuesi = krijuesi;
            EmerKlienti = kodklienti;
            Kontakti = kontakti;
            IdArtikulli = idartikulli;
            Detajimi = detajim;
            IdMagazina = idmagazina;
            IdNdermarje = idndermarje;
            IdKokaShitje = idkokashitje;
        }

        internal clsGaranciArtikulli(DataRow rreshti)
        {
            Mbush(rreshti);
        }

        #endregion

        #region Properties

        public DateTime DtMbarimi { get; private set; }

        public string Dyqani { get; private set; }

        public string Loan { get; private set; }

        /// <summary>
        /// id e garancise
        /// </summary>
        public int IdGarancia { get; set; }

        /// <summary>
        /// id e artikullit te garancise
        /// </summary>
        public int IdArtikulli { get; set; }

        /// <summary>
        /// id e detajimit te artikullit
        /// </summary>
        public string Detajimi { get; set; }

        /// <summary>
        /// data e dokumentit te shitjes
        /// </summary>
        public DateTime DtDok { get; set; }

        /// <summary>
        /// id e kokes se shitjes
        /// </summary>
        public int IdKokaShitje { get; set; }

        /// <summary>
        /// id e krijuesit
        /// </summary>
        public string Krijuesi { get; set; }

        /// <summary>
        /// id e magazines
        /// </summary>
        public int IdMagazina { get; set; }

        /// <summary>
        /// id e ndermarjes
        /// </summary>
        public int IdNdermarje { get; set; }

        /// <summary>
        /// id e nivelit te dokumentit
        /// </summary>
        public int IdNiveli { get; set; }

        public string IMEI { get; private set; }

        /// <summary>
        /// nr automatik unik qe identifikon nje garanci
        /// </summary>
        public string Kodi { get; set; }

        /// <summary>
        /// emri i klientit te garancise
        /// </summary>
        public string EmerKlienti { get; set; }

        public string Kohezgjatje { get; private set; }

        public string Kompania { get; private set; }

        /// <summary>
        /// nr i kontaktit te klientit
        /// </summary>
        public string Kontakti { get; set; }

        public string LlojGaranci { get; private set; }

        public string Niveli { get; private set; }

        public string Produkti { get; private set; }

        public string Shites { get; private set; }
        #endregion

        #region Public Methods

        public void Ruaj(clsDatabaseRegjistrim databaseRegjistrim)
        {
            databaseRegjistrim.RuajGaranciArtikulli(IdNiveli, DtDok, Krijuesi, EmerKlienti, Kontakti, IdArtikulli, Detajimi, IdMagazina, IdNdermarje, IdKokaShitje);
        }

        #endregion

        #region Private Methods

        private void Mbush(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    IdGarancia = !IsDBNull(db["IDGARANCIA"])
                        ? ToInt32(db["IDGARANCIA"])
                        : 0;
                    IdNiveli = !IsDBNull(db["IDNIVELI"])
                        ? ToInt32(db["IDNIVELI"])
                        : 0;
                    Krijuesi = db["KRIJUESI"].ToString();
                    EmerKlienti = db["EMERKLIENTI"].ToString();
                    Kodi = db["KODI"].ToString();
                    Kontakti = db["KONTAKTI"].ToString();
                    IdArtikulli = !IsDBNull(db["IDARTIKULLI"])
                        ? ToInt32(db["IDARTIKULLI"])
                        : 0;
                    Detajimi = db["DETAJIMI"].ToString();
                    IdMagazina = !IsDBNull(db["IDMAGAZINA"])
                        ? ToInt32(db["IDMAGAZINA"])
                        : 0;
                    IdNdermarje = !IsDBNull(db["IDNDERMARJE"])
                        ? ToInt32(db["IDNDERMARJE"])
                        : 0;
                    Loan = db["Loan"].ToString();
                    IdKokaShitje = !IsDBNull(db["IDKOKASHITJE"])
                        ? ToInt32(db["IDKOKASHITJE"])
                        : 0;
                    DtDok = !IsDBNull(db["DTDOK"])
                        ? ToDateTime(db["DTDOK"])
                        : DateTime.MinValue;
                    Niveli = db["Niveli"].ToString();
                    Shites = db["Shites"].ToString();
                    Produkti = db["Produkti"].ToString();
                    Dyqani = db["Dyqani"].ToString();
                    Kompania = db["Kompania"].ToString();
                    IMEI = db["IMEI"].ToString();
                    Kohezgjatje = db["Kohezgjatje"].ToString();
                    LlojGaranci = db["LlojiGarancise"].ToString();
                    DtMbarimi = !IsDBNull(db["DtMbarimi"])
                        ? ToDateTime(db["DtMbarimi"])
                        : DateTime.MinValue;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se garancise  nga db-ja");
                }
            }
        }

        #endregion
    }
}
