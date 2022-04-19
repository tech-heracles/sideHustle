using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbArkaBanka
{
    public class colOpsionePagese : List<clsOpsionePagese>
    {
        #region Metoda Publike

        public colOpsionePagese(LlojVeprimi lloji)
        {
            merrGjitheOpsionePagese(Convert.ToInt32(lloji));
        }

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsLlojTakse"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsOpsionePagese this[int index]
        {
            get { return base[index]; }
        }
        /// <summary>
        /// mbush gjithe llojet e taksave
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool merrGjitheOpsionePagese(int lloji)
        {
            clsDatabaseArkaBanka db = new clsDatabaseArkaBanka();
            bool sukses = mbushPagese(db.merrGjitheOpsionePagese(lloji));
            db.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsLlojTakse"/> 
        /// </summary>
        private bool mbushPagese(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsOpsionePagese(rreshti));
            }
            return true;
        }

        #endregion
    }
}
