using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <inheritdoc />
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKontaktiKlientFurnitor
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKontaktiKlientFurnitor : System.Collections.Generic.List<clsKontaktiKlientFurnitor>
    {
        #region Konstruktoret

        /// <inheritdoc />
        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colKontaktiKlientFurnitor()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e klientit</param>
        public colKontaktiKlientFurnitor(int id)
        {
            using (var dbKontaktet = new clsDatabaseKontabilitet())
                MbushListKontaktesh(dbKontaktet.ktheKontaktSipasIdKlientFurnitor(id));
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsKontaktiKlientFurnitor"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKontaktiKlientFurnitor this[int index] => base[index];
        
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsKontaktiKlientFurnitor"/> 
        /// </summary>
        private void MbushListKontaktesh(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsKontaktiKlientFurnitor(rreshti));
            }
        }

        #endregion
    }
}


