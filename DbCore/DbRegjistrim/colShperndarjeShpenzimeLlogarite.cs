using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colShperndarjeShpenzimeLlogarite : System.Collections.Generic.List<clsShperndarjeShpenzimeLlogarite>
    {   /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeLlogarite"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsShperndarjeShpenzimeLlogarite this[int index]
        {
            get { return ((clsShperndarjeShpenzimeLlogarite)base[index]); }
        }

        public bool merrLlogariSipasKokes(int idkoka)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool mbush = mbushShperndarjeShpezimeLlogarite(db.ktheShperndarjeShpenzimeshLlogariSipasKokes(idkoka));
            db.Dispose();
            return mbush;
        }

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeLlogarite"/> 
        /// </summary>
        private bool mbushShperndarjeShpezimeLlogarite(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsShperndarjeShpenzimeLlogarite llogari = new clsShperndarjeShpenzimeLlogarite();
                    //llogari.mbushShperndarjeShpenzLlog(rreshti);
                    Add(new clsShperndarjeShpenzimeLlogarite(rreshti));
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
