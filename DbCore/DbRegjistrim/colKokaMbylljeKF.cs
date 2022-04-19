using System.Collections.Generic;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colKokaMbylljeKF : List<clsKokaMbylljeKF>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKokaMbylljeKF"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaMbylljeKF this[int index] => base[index];
        
        public static DataTable MerrMbylljeKfdt(int idndermvit, int idperdoruesi)
        {
            using (var dbartikuj = new clsDatabaseRegjistrim())
                return dbartikuj.merrMbylljeKFDT(idndermvit, idperdoruesi);
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="clsKokaMbylljeKF"/> 
        /// </summary>
        private void MbushMbylljeFkKoka(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsKokaMbylljeKF(rreshti));
            }
        }

        #endregion
    }
}
