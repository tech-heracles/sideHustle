using System.Collections.Generic;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsMaturimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colMaturimet : List<clsMaturimi>
    {
        #region Ctor

        /// <summary>
        /// konstruktore pa parametra
        /// </summary>
        public colMaturimet()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// konstruktore me 2 parametra
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        public colMaturimet(int idnderm)
        {
            using (var databaseInventari = new clsDatabaseInventari())
                Mbush(databaseInventari.MerrMaturimeSipasNdermarrjes(idnderm));
        }

        /// <inheritdoc />
        /// <summary>
        /// konstruktore me 3 parametra
        /// </summary>
        /// <param name="lloji">lloji i maturimit</param>
        /// <param name="idNdermarrje">id e ndermarrjes vit</param>
        public colMaturimet(bool lloji, int idNdermarrje)
        {
            using (var databaseInventari = new clsDatabaseInventari())
                Mbush(databaseInventari.MerrMaturimKlientiOseFurnitori(lloji, idNdermarrje));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// kthen objektin <see cref="clsMaturimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsMaturimi this[int index] => base[index];

        /// <summary>
        /// Merr nje table per te mbushur griden.
        /// </summary>
        /// <param name="idndermarrje">The idndermarrje.</param>
        /// <returns></returns>
        public static DataTable MerrAfatMaturimiSipasNdermDt(int idndermarrje)
        {
            using (var databaseInventari = new clsDatabaseInventari())
                return databaseInventari.MerrMaturimSipasNdermarrjesDt(idndermarrje);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="clsMaturimi"/> 
        /// </summary>
        private void Mbush(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsMaturimi(rreshti));
            }
        }

        #endregion
    }
}
