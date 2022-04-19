using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsLlojLlogarish
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colLlojLlogarish : System.Collections.Generic.List<clsLlojLlogarish>
    {
        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsLlojLlogarish"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        #region Metoda Publike

        public new clsLlojLlogarish this[int index]
        {
            get { return ((clsLlojLlogarish)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsLlojLlogarish"/> 
        /// </summary>
        private bool mbushLlojLlogarish(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojLlogarish lloji = new clsLlojLlogarish();
                    //lloji.mbushLlojLlogarish(rreshti);
                    Add(new clsLlojLlogarish(rreshti));
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
        [Obsolete("Perdor: bool mbushLlojLlogarish(DataTable dt)", true)]
        public colLlojLlogarish mbushArrayListNenLlojLlogarish(DataSet ds)
        {// metoda per te mbushur nje arraylist me llogari nga nje dataset
            colLlojLlogarish llojellogarish = new colLlojLlogarish();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlojLlogarish lloji = new clsLlojLlogarish();
                lloji.IdLlojLlogarie = int.Parse(rreshti[0].ToString());
                lloji.KodLlojLlogarie = rreshti[1].ToString();
                lloji.PershkrimLlojLlogarie = rreshti[2].ToString();

                llojellogarish.Add(lloji);
            }
            return llojellogarish;
        }
        
    }
}
