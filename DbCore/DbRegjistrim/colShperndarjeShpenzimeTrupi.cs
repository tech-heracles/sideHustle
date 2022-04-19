using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{ 
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsShperndarjeShpenzimeTrupi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colShperndarjeShpenzimeTrupi : System.Collections.Generic.List<clsShperndarjeShpenzimeTrupi>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colShperndarjeShpenzimeTrupi()
        { 
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idShperndarjeShpenzimeshKoka">id e kokes se shperndarje shpenzimeve</param>
        public colShperndarjeShpenzimeTrupi(int idShperndarjeShpenzimeshKoka)
        {
            clsDatabaseRegjistrim dbShperndShpenTrupi = new clsDatabaseRegjistrim();
            mbushShperndarjeShpezimeTrupat(dbShperndShpenTrupi.ktheShperndarjeShpenzimeshTrupiSipasKokes(idShperndarjeShpenzimeshKoka));
            dbShperndShpenTrupi.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeTrupi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsShperndarjeShpenzimeTrupi this[int index]
        {
            get { return ((clsShperndarjeShpenzimeTrupi)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeTrupi"/> 
        /// </summary>
        private bool mbushShperndarjeShpezimeTrupat(DataTable dt)
        {
                foreach (DataRow rreshti in dt.Rows)
                {
                    Add(new clsShperndarjeShpenzimeTrupi(rreshti));
                }
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushShperndarjeShpezimeTrupat(DataTable dt)", true)]
        public colShperndarjeShpenzimeTrupi mbushArrayListShperndarjeShpenzimeTrupi(DataSet ds)
        {
            colShperndarjeShpenzimeTrupi trupat = new colShperndarjeShpenzimeTrupi();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsShperndarjeShpenzimeTrupi trupi = new clsShperndarjeShpenzimeTrupi();

                trupi.IdTrupi = int.Parse(rreshti[0].ToString());
                trupi.IdKoka = int.Parse(rreshti[1].ToString());
                trupi.IdFatura= int.Parse(rreshti[2].ToString());
                trupi.NrDok = rreshti[3].ToString();
                trupi.DtDok = DateTime.Parse(rreshti[4].ToString());
                trupi.LlojDok = int.Parse(rreshti[5].ToString());
                trupi.OColTrupiFaturat = new colShperndarjeShpenzimeTrupiFaturat();
                trupat.Add(trupi);
            }
            return trupat;
        }
    }
}
