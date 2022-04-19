using System;
using System.Data;
using static System.Convert;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne Maturimet
    ///  (Te dhenat  merren nga tabela : T_MATURIMI)
    /// </summary>
    public class clsMaturimi
    {
        #region Kontruktoret

        /// <summary>
        /// konstruktore me 1 parameter
        /// </summary>
        /// <param name="id">id e maturimit</param>
        public clsMaturimi(int id)
        {
            using (var databaseInventari = new clsDatabaseInventari())
                Mbush(databaseInventari.MerrMaturim(id));
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsMaturimi()
        {
        }

        public clsMaturimi(DataRow rreshti)
        {
            Mbush(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdMaturimi { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin e maturimit.
        /// </summary>
        public string KodMaturimi { get; set; }

        /// <summary>
        /// Kthen/Vendos pershkrimin e maturimit.
        /// </summary>
        public string PershkrimMaturimi { get; set; }

        /// <summary>
        /// Kthen/Vendos llojin e maturimit.
        /// </summary>
        public bool LlojMaturimi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e dates se fillimit te periudhes se maturimit.
        /// </summary>
        public DateFillimiMaturiteti IdDateFillimi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e periudhes.
        /// </summary>
        public PeriudheMaturiteti IdPeriudha { get; set; }

        /// <summary>
        /// Kthen/Vendos percaktimin e maturimit.
        /// </summary>
        public int PercaktimMaturimi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi { get; set; }

        /// <summary>
        /// Kthen/Vendos Id-ne statusit te dokumentit.
        /// </summary>
        public int IdStatusDok { get; set; }

        public DateTime DtKrijimi { get; private set; }

        public DateTime DtModifikimi { get; private set; }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan maturimin.
        /// </summary>
        /// <returns></returns>
        public void Ruaj()
        {
            using (var scope = new MyTransactionScope())
            {
                using (var databaseInventari = new clsDatabaseInventari())
                    IdMaturimi = databaseInventari.RuajMaturim(KodMaturimi, PershkrimMaturimi, LlojMaturimi, IdDateFillimi, IdPeriudha, PercaktimMaturimi, IdPerdoruesi, IdNdermarje, IdStatusDok);

                scope.Complete();
            }
        }

        /// <summary>
        /// Modifikon maturimin
        /// </summary>
        public void Modifiko()
        {
            using (var scope = new MyTransactionScope())
            {
                using (var databaseInventari = new clsDatabaseInventari())
                    databaseInventari.ModifikoMaturim(IdMaturimi, KodMaturimi, PershkrimMaturimi, LlojMaturimi, IdDateFillimi, IdPeriudha, PercaktimMaturimi, IdPerdoruesi, IdNdermarje, IdStatusDok);

                scope.Complete();
            }
        }

        /// <summary>
        /// Fshin objektin maturim ne tabelen perkatese ne databaze.
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Fshi()
        {
            using (var databaseInventari = new clsDatabaseInventari())
                return databaseInventari.FshiMaturim(IdMaturimi, IdPerdoruesi);
        }

        /// <summary>
        /// Merr nje rresht maturimi dhe perdoret per te shtuar/modifikuar nje rresht ne gride.
        /// </summary>
        /// <param name="idmaturimi">The idmaturimi.</param>
        /// <returns></returns>
        public static DataRow MerrAfatMaturimiDr(int idmaturimi)
        {
            using (var databaseInventari = new clsDatabaseInventari())
                return databaseInventari.MerrMaturimDr(idmaturimi);
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id maturimit sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kod">kodi i artikullit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>id e maturimit</returns>
        public static int KtheIdMaturimi(string kod, int idndermarje)
        {
            using (var databaseInventari = new clsDatabaseInventari())
                return databaseInventari.MerrIdMaturim(kod, idndermarje);
        }

        /// <summary>
        /// Kontrollon nqs ekziston maturimi.
        /// </summary>
        /// <param name="kod">The kod.</param>
        /// <param name="idnderm">The idnderm.</param>
        /// <returns></returns>
        public static bool Ekziston(string kod, int idnderm)
        {
            using (var databaseInventari = new clsDatabaseInventari())
                return databaseInventari.EkzistonMaturim(kod, idnderm);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// Mbushja e maturimit nga databaza
        /// </summary>
        /// <param name="dataRow">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal void Mbush(DataRow dataRow)
        {
            if (dataRow != null)
            {
                try
                {
                    IdMaturimi = int.Parse(dataRow["IDMATURIMI"].ToString());
                    KodMaturimi = dataRow["KODMATURIMI"].ToString();
                    PershkrimMaturimi = dataRow["PERSHKRIMMATURIMI"].ToString();
                    LlojMaturimi = bool.Parse(dataRow["LLOJMATURIMI"].ToString());
                    IdDateFillimi = (DateFillimiMaturiteti)int.Parse(dataRow["IDDATEFILLIMI"].ToString());
                    IdPeriudha = (PeriudheMaturiteti)int.Parse(dataRow["IDPERIUDHA"].ToString());
                    PercaktimMaturimi = int.Parse(dataRow["PERCAKTIMMATURIMI"].ToString());
                    IdPerdoruesi = int.Parse(dataRow["IDPERDORUESI"].ToString());
                    IdNdermarje = int.Parse(dataRow["IDNDERMARJE"].ToString());
                    IdStatusDok = !IsDBNull(dataRow["IDSTATUSDOK"])
                        ? ToInt32(dataRow["IDSTATUSDOK"])
                        : 0;
                    DtKrijimi = !IsDBNull(dataRow["DTKRIJIMI"])
                        ? ToDateTime(dataRow["DTKRIJIMI"])
                        : DateTime.MinValue;
                    DtModifikimi = !IsDBNull(dataRow["DTMODIFIKIMI"])
                        ? ToDateTime(dataRow["DTMODIFIKIMI"])
                        : DateTime.MinValue;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se maturimit nga db-ja");
                }
            }
        }

        #endregion
    }
}
