using System;
using System.Data;
using DbCore.DbInventari;
using DbCore.IMBUtils.Messages;
using static System.Convert;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqsojne limitin e kartes se klientit
    /// (Te dhenat  merren nga tabela : T_LimitKarta)
    /// </summary>
    public class clsLimitKarta
    {
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLimiti { get; set; }

        /// <summary>
        /// Kthen/Vendos idKarta.
        /// </summary>
        public int IdKarta { get; set; }

        /// <summary>
        /// Kthen/Vendos lloji 0-Artikull 1-Grupim Artikulli.
        /// </summary>
        public int Lloji { get; set; }

        /// <summary>
        /// Gets or sets the identifier artikull.
        /// </summary>
        /// <value>
        /// The identifier artikull.
        /// </value>
        public int IdArtikull { get; set; }

        /// <summary>
        /// Kthen/Vendos limitin e sasise.
        /// </summary>
        public double LimitSasi { get; set; }

        /// <summary>
        /// Kthen/Vendos limitin ne vlere.
        /// </summary>
        public double LimitVlere { get; set; }

        /// <summary>
        /// Kthen/Vendos data Fillimit.
        /// </summary>
        public DateTime DtFillimi { get; set; }

        /// <summary>
        /// Kthen/Vendos dataMbarimit.
        /// </summary>
        public DateTime DtMbarimi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e kategorise se zbritjes.
        /// </summary>
        public int IdNivelZbritje { get; set; }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsLimitKarta(int idKarta, int lloji, string kodi, double limitSasi, double limitVlere, DateTime dtFillimi, DateTime dtMbarimi, string kodNivelZbritje, int idNdermarrje)
        {
            IdKarta = idKarta;
            Lloji = lloji;
            IdArtikull = lloji == 0
                ? clsArtikulli.ktheIdArtikulli(kodi, idNdermarrje)
                : clsKodifikimArtikulli.MerrIdKodifikimi(kodi, idNdermarrje);
            LimitSasi = limitSasi;
            LimitVlere = limitVlere;
            DtFillimi = dtFillimi;
            DtMbarimi = dtMbarimi;
            IdNivelZbritje = kodNivelZbritje != string.Empty
                ? clsNivelZbritje.ktheIdNivelZbritje(kodNivelZbritje, idNdermarrje)
                : 0;

            Valido();
        }

        #endregion

        #region Metoda Publike


        /// <summary>
        /// Metode qe merr limitet sipas kartes. Perdoret per te mbushur griden e limiteve.
        /// </summary>
        /// <param name="idKarta">Identifikuesi i karts.</param>
        /// <returns></returns>
        public static DataTable MerrLimitetSipasKartes(int idKarta)
        {
            using (var databaseRegjistrim = new clsDatabaseRegjistrim())
                return databaseRegjistrim.MerrLimitetSipasKartes(idKarta);
        }

        public static double MerrLimitSasi(int idArtikull, int idKarta, DateTime dataShitjes)
        {
            using (var databaseRegjistrim = new clsDatabaseRegjistrim())
                return databaseRegjistrim.MerrLimitSasiSipasIdArtikulli(idArtikull, idKarta, dataShitjes);
        }

        public static double MerrLimitVlere(int idArtikull, int idKarta, DateTime dataShitjes)
        {
            using (var databaseRegjistrim = new clsDatabaseRegjistrim())
                return databaseRegjistrim.merrLimitVlereSipasIdArtikulli(idArtikull, idKarta, dataShitjes);
        }

        public static double MerrLimitVlereMbetur(int idArtikull, int idKarta, DateTime dataShitjes)
        {
            using (var databaseRegjistrim = new clsDatabaseRegjistrim())
                return databaseRegjistrim.merrLimitVlereMbeturSipasIdArtikulli(idArtikull, idKarta, dataShitjes);
        }

        public static double MerrLimitSasiMbetur(int idArtikull, int idKarta, DateTime dataShitjes)
        {
            using (var databaseRegjistrim = new clsDatabaseRegjistrim())
                return databaseRegjistrim.merrLimitSasiMbeturSipasIdArtikulli(idArtikull, idKarta, dataShitjes);
        }

        #endregion

        #region Metoda Internal

        internal void Ruaj(clsDatabaseRegjistrim db)
        {
            if (EkzistonLimit(db))
                throw new MyException("Ekziston nje limit per kete artikull brenda intervalit!");

            IdLimiti = db.RuajLimitKarta(IdKarta, Lloji, IdArtikull, DtFillimi, DtMbarimi, LimitSasi, LimitVlere, IdNivelZbritje);
        }

        internal static void FshiSipasKartes(clsDatabaseRegjistrim db, int idKarta)
        {
            db.FshiLimitetSipasKartes(idKarta);
        }

        /// <summary>
        /// Metode per mbushjen e automjetit nga databaza.
        /// </summary>
        /// <param name="dbDataRow">Si parameter merr nje DataRow.</param>
        internal void Mbush(DataRow dbDataRow)
        {
            if (dbDataRow == null)
                return;
            try
            {
                IdLimiti = !IsDBNull(dbDataRow["IDLIMITI"])
                    ? ToInt32(dbDataRow["IDLIMITI"])
                    : 0;
                IdKarta = !IsDBNull(dbDataRow["IDKARTA"])
                    ? ToInt32(dbDataRow["IDKARTA"])
                    : 0;
                Lloji = !IsDBNull(dbDataRow["LLOJI"])
                    ? ToInt32(dbDataRow["LLOJI"])
                    : 0;
                IdArtikull = !IsDBNull(dbDataRow["IDARTIKULL"])
                    ? ToInt32(dbDataRow["IDARTIKULL"])
                    : 0;
                DtFillimi = !IsDBNull(dbDataRow["DTFILLIMI"])
                    ? ToDateTime(dbDataRow["DTFILLIMI"])
                    : DateTime.MinValue;
                DtMbarimi = !IsDBNull(dbDataRow["DTMBARIMI"])
                    ? ToDateTime(dbDataRow["DTMBARIMI"])
                    : DateTime.MinValue;
                IdNivelZbritje = !IsDBNull(dbDataRow["IDKATEGORIZB"])
                    ? ToInt32(dbDataRow["IDKATEGORIZB"])
                    : 0;
                LimitSasi = !IsDBNull(dbDataRow["LIMITSASI"])
                    ? ToDouble(dbDataRow["LIMITSASI"])
                    : 0;
                LimitVlere = !IsDBNull(dbDataRow["LIMITVLERE"])
                    ? ToDouble(dbDataRow["LIMITVLERE"])
                    : 0;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se automjetit nga db-ja");
            }
        }

        #endregion

        #region Metoda Private

        private void Valido()
        {
            if (IdArtikull <= 0)
            {
                throw new MyException(MessagesResource.Messages["LimitKarta.ValidimKodi"]);
            }

            if (IdNivelZbritje < 0)
            {
                throw new MyException(MessagesResource.Messages["LimitKarta.ValidimZbritjeAnalitike"]);
            }

            if (LimitVlere == 0 && LimitSasi == 0)
            {
                throw new MyException(MessagesResource.Messages["LimitKarta.ValidimLimiteZero"]);
            }

            if (LimitVlere != 0 && LimitSasi != 0)
            {
                throw new MyException(MessagesResource.Messages["LimitKarta.ValidimLimiteJoZero"]);
            }

            if (DtFillimi == DateTime.MinValue)
            {
                throw new MyException(MessagesResource.Messages["LimitKarta.ValidimDtFillimi"]);
            }

            if (DtMbarimi == DateTime.MinValue)
            {
                throw new MyException(MessagesResource.Messages["LimitKarta.ValidimDtMbarimi"]);
            }

            if (DtFillimi > DtMbarimi)
            {
                throw new MyException(MessagesResource.Messages["LimitKarta.ValidimDtFillimiDtMbarimi"]);
            }
        }

        private bool EkzistonLimit(clsDatabaseRegjistrim db)
        {
            return db.EkzistonLimitiPerKarten(IdKarta, IdArtikull, Lloji, DtFillimi);
        }

        #endregion
    }
}
