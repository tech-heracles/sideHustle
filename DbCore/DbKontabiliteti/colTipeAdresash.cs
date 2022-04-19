using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <inheritdoc />
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTipAdrese
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTipeAdresash : System.Collections.Generic.List<clsTipAdrese>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsTipAdrese"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTipAdrese this[int index] => base[index];

        /// <summary>
        /// mbush te gjitha tipet e adresave
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public void MbushGjitheTipetAdresave()
        {
            using (var dbTipeAdresash = new clsDatabaseKontabilitet())
                MbushTipeAdresash(dbTipeAdresash.ktheGjitheTipetAdresave());
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        private void MbushTipeAdresash(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsTipAdrese(rreshti));
            }
        }

        #endregion
    }
}

