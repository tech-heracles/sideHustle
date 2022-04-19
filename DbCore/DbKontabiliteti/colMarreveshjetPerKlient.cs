using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <inheritdoc />
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsMarreveshjePerKlient
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colMarreveshjetPerKlient : System.Collections.Generic.List<clsMarreveshjePerKlient>
    {
        #region Metoda Publike

        public new clsMarreveshjePerKlient this[int index] => base[index];

        /// <summary>
        /// mbush vetem marreveshjet e selektuara per kete klient
        /// </summary>
        /// <param name="idKlient">id e klientit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public void MbushVetemMarreveshjetEKlientit(int idKlient)
        {
            using (var dbKont = new clsDatabaseKontabilitet())
                MbushMarreveshjePerKlient(dbKont.ktheVetemMarreveshjeKlienti(idKlient));
        }

        public static clsMesazh FshiMarreveshjetEKlientit(int idKlient, clsDatabaseKontabilitet dbKont)
        {
            return dbKont.fshiMarreveshjeKlienti(idKlient);
        }

        #endregion

        #region Metoda Private


        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        private void MbushMarreveshjePerKlient(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsMarreveshjePerKlient(rreshti));
            }
        }

        #endregion
    }
}