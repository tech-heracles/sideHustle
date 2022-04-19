using System.Collections.Generic;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colTrupiPolitikeKarta : List<clsTrupiPolitikeKarta>
    {
        #region Ctor

        public colTrupiPolitikeKarta() { }

        public colTrupiPolitikeKarta(int idPolitike)
        {
            using (var data = new clsDatabaseRegjistrim())
                Mbush(data.MerrTrupiPolitikeSipasIdPolitike(idPolitike));
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsTrupiPolitikeKarta"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTrupiPolitikeKarta this[int index] => base[index];
        
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
                Add(new clsTrupiPolitikeKarta(rreshti));
            }
        }

        #endregion
    }
}
