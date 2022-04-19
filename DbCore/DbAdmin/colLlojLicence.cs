using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colLlojLicence : System.Collections.Generic.List<clsLlojLicence>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsLlojTakse"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsLlojLicence this[int index]
        {
            get { return ((clsLlojLicence)base[index]); }
        }
        /// <summary>
        /// mbush gjithe llojet e taksave
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGjitheLlojeLicence()
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool sukses = mbushLlojLicence(db.ktheGjitheLlojeLicence());
            db.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsLlojTakse"/> 
        /// </summary>
        private bool mbushLlojLicence(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojLicence llojlicence = new  clsLlojLicence ();
                    //llojlicence.mbushLlojLicence(rreshti);
                    Add(new clsLlojLicence(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion
    }
}
