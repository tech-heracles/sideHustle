using System;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne dhuratat e klientit
    ///  (Te dhenat  merren nga tabela : T_Dhurata)
    /// </summary>
    public class Dhurata
    {
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdDhurata { get; set; }

        public int IdKarta { get; set; }

        public int IdKategoriDhurate { get; set; }

        public int PikeDebit { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e krijuesit.
        /// </summary>
        public int IdKrijues { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes.
        /// </summary>
        public int IdNdermarrje { get; set; }

        public int IdNdermarrjeVit { get; set; }

        /// <summary>
        /// Kthen/Vendos daten e krijimit.
        /// </summary>
        public DateTime DtKrijimi { get; set; }

        #endregion

        #region Konstruktoret

        public Dhurata() { }

        public Dhurata(int idKarta, int idkategoriDhurate, int pikeTeZbritura, int idKrijues, int idNdermarrje, int idNderVit)
        {
            IdKarta = idKarta;
            IdKategoriDhurate = idkategoriDhurate;
            PikeDebit = pikeTeZbritura;
            IdNdermarrjeVit = idNderVit;
            IdNdermarrje = idNdermarrje;
            IdKrijues = idKrijues;
        }

        #endregion

        #region Metoda Publike

        public void Ruaj()
        {
            using (var scope = new MyTransactionScope())
            {
                using (var db = new clsDatabaseRegjistrim())
                    IdDhurata = db.RuajDhurata(IdKarta, IdKategoriDhurate, PikeDebit, IdNdermarrje, IdKrijues, IdNdermarrjeVit);

                scope.Complete();
            }
        }

        #endregion

        internal void Ruaj(clsDatabaseRegjistrim databaseRegjistrim)
        {
            IdDhurata = databaseRegjistrim.RuajDhurata(IdKarta, IdKategoriDhurate, PikeDebit, IdNdermarrje, IdKrijues, IdNdermarrjeVit);
        }

        internal void ModifikoPikeFillestareKarte(clsDatabaseRegjistrim databaseRegjistrim)
        {
            databaseRegjistrim.ModifikoPikeFillestareKarte(IdKarta, PikeDebit);
        }
    }
}