using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsGaranciArtikulli
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colGaranciArtikulli : System.Collections.Generic.List<clsGaranciArtikulli>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colGaranciArtikulli()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes se  shitjes</param>
        public colGaranciArtikulli(int id)
        {
            using (var db = new clsDatabaseRegjistrim())
                Mbush(db.ktheGaranciArtikulliSipasKoka(id));
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsGaranciArtikulli"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsGaranciArtikulli this[int index] => base[index];

        #endregion

        #region Metoda Private

        private void Mbush(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsGaranciArtikulli(rreshti));
            }
        }

        #endregion
    }
}
