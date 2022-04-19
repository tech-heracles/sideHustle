using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsShperndarjeShpenzimeFaturat
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colShperndarjeShpenzimeFaturat : System.Collections.Generic.List<clsShperndarjeShpenzimeFaturat>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeFaturat"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsShperndarjeShpenzimeFaturat this[int index]
        {
            get { return ((clsShperndarjeShpenzimeFaturat)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeFaturat"/> 
        /// </summary>
        private bool mbushShperndarjeShpezimeFaturat(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsShperndarjeShpenzimeFaturat(rreshti));
            }
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushShperndarjeShpezimeFaturat(DataTable dt)", true)]
        public colShperndarjeShpenzimeFaturat mbushArrayListShperndarjeShpenzimeFaturat(DataSet ds)
        {
            colShperndarjeShpenzimeFaturat faturat = new colShperndarjeShpenzimeFaturat();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsShperndarjeShpenzimeFaturat fatura = new clsShperndarjeShpenzimeFaturat();

                fatura.IdTrupi = int.Parse(rreshti[0].ToString());
                fatura.IdKoka = int.Parse(rreshti[1].ToString());
                fatura.IdFatura = int.Parse(rreshti[2].ToString());
                faturat.Add(fatura);
            }
            return faturat;
        }


    }
}
