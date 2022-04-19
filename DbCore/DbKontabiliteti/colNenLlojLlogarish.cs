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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsNenLlojLlogarish
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colNenLlojLlogarish : System.Collections.Generic.List<clsNenLlojLlogarish>
    {
        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsNenLlojLlogarish"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        #region Metoda Publike

        public new clsNenLlojLlogarish this[int index]
        {
            get { return ((clsNenLlojLlogarish)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsNenLlojLlogarish"/> 
        /// </summary>
        private bool mbushNenLlojLlog(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsNenLlojLlogarish nenlloji = new clsNenLlojLlogarish();
                    //nenlloji.mbushNenLlojLlogarish(rreshti);
                    Add(new clsNenLlojLlogarish(rreshti));
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
        [Obsolete("Perdor: bool mbushNenLlojLlog(DataTable dt)", true)]
        public colNenLlojLlogarish mbushArrayListNenLlojLlogarish(DataSet ds)
        {// metoda per te mbushur nje arraylist me llogari nga nje dataset
            colNenLlojLlogarish nenllojellogarish = new colNenLlojLlogarish();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsNenLlojLlogarish nenlloji = new clsNenLlojLlogarish();
                nenlloji.IdLlojLlogarie = rreshti[0].ToString();
                nenlloji.IdNenLlojLlogarie = int.Parse(rreshti[1].ToString());
                nenlloji.KodNenLlojLlogarie = rreshti[2].ToString();
                nenlloji.PershkrimNenLlojLlogarie = rreshti[3].ToString();

                nenllojellogarish.Add(nenlloji);
            }
            return nenllojellogarish;
        }
        
    }
}
