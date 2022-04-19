using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKarta
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKarta : System.Collections.Generic.List<clsKarta>
    {
        #region Ctor

        public colKarta() { }

        public colKarta(int idndermarje)
        {
            using (var db = new clsDatabaseRegjistrim())
                Mbush(db.MerrKarteSipasNdermarrjes(idndermarje));
        }

        public colKarta(int idNdermarrje, int idKlient)
        {
            using (var db = new clsDatabaseRegjistrim())
                Mbush(db.MerrKarteSipasKlientit(idNdermarrje, idKlient));
        } 

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKarta"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKarta this[int index] => base[index];

        public static DataTable MerrKartatSipasNdermarrjes(int idNdermarrje)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrKarteSipasNdermarrjes(idNdermarrje);
        }

        public static DataTable MerrKartatSipasNdermarrjesAll(int idNdermarrje)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrKarteSipasNdermarrjesAll(idNdermarrje);
        }

        public static DataTable MerrKartatSipasKlientit(int idNdermarrje, int idKlient)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrKarteSipasKlientit(idNdermarrje, idKlient);
        }

        public static DataTable MerrKartatSipasNdermarrjesPerDhurata(int idNdermarrje)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrKarteSipasNdermarrjesDtSmall(idNdermarrje);
        }

        public static DataTable KtheKarteSipasNdermMeFilter(string filter, long startIndex, long endIndex, int idnderm, int idKlient)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrKarteSipasNdermarrjesMeFilter(filter, startIndex, endIndex, idnderm, idKlient);
        }

        public static DataTable MerrKartaSipasNdermarrjesPerEksport(int idnderm)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrKartaSipasNdermarrjesPerEksport(idnderm);
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera nga nje stored procedure.
        /// </summary>
        /// <param name="dt">Merr si parameter nje DataTable.</param>
        private void Mbush(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsKarta(rreshti));
            }
        }

        #endregion
    }
}