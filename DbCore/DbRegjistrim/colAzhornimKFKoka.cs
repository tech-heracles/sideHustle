using System.Data;

namespace DbCore.DbRegjistrim
{

    public class colAzhornimKFKoka : System.Collections.Generic.List<clsAzhornimKFKoka>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsAzhornimKFKoka"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsAzhornimKFKoka this[int index] => base[index];

        public static DataTable MerrAzhornimKfdt(int idndermvit, int idperdoruesi)
        {
            using (var dbartikuj = new clsDatabaseRegjistrim())
                return dbartikuj.merrAzhornimKFDT(idndermvit, idperdoruesi);
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="clsAzhornimKFKoka"/> 
        /// </summary>
        private void MbushAzhornimetKfKoka(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsAzhornimKFKoka(rreshti));
            }
        }

        #endregion
    }
}
