using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsAdresaKlientFurnitor
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colAdresatKlientFurnitor : System.Collections.Generic.List<clsAdresaKlientFurnitor>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parameter
        /// </summary>
        public colAdresatKlientFurnitor()
        {
        }

        /// <summary>
        /// konstruktore me 1 parameter
        /// </summary>
        /// <param name="id">id e klient furnitorit</param>
        public colAdresatKlientFurnitor(int id)
        {
            using (var dbAdresaKlientFurn = new clsDatabaseKontabilitet())
                MbushAdresatKlientFurnitor(dbAdresaKlientFurn.ktheAdreseSipasIdKlientFurnitor(id));
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsAdresaKlientFurnitor"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsAdresaKlientFurnitor this[int index] => base[index];

        #endregion

        #region Metoda Internal

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        private void MbushAdresatKlientFurnitor(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsAdresaKlientFurnitor(rreshti));
            }
        }

        #endregion
    }
}
