using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsInventarizim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colInventarizim : System.Collections.Generic.List<clsInventarizim>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsInventarizim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsInventarizim this[int index]
        {
            get { return ((clsInventarizim)base[index]); }
        }

        /// <summary>
        /// mbush gjithe inventarizimet
        /// </summary>
        /// <returns>kthen true nese mbushja eshte kryer me sukses, ne te kundert kthen false</returns>
        public bool mbushGjitheInventarizime()
        {
            clsDatabaseRegjistrim dbInventarizim = new clsDatabaseRegjistrim();
            bool mbush = mbushInventarizime(dbInventarizim.ktheGjitheInventarizime());
            dbInventarizim.Dispose();
            return mbush;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsInventarizim"/> 
        /// </summary>
        private bool mbushInventarizime(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsInventarizim inv = new clsInventarizim();
                    //inv.mbushInventar(rreshti);
                    Add(new clsInventarizim(rreshti));
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
        [Obsolete("Perdor: bool mbushInventarizime(DataTable dt)", true)]
        public colInventarizim mbushArrayListInventarizim(DataSet ds)
        {
            colInventarizim invt = new colInventarizim();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsInventarizim inv = new clsInventarizim();
                inv.IdInventarizimi = int.Parse(rreshti[0].ToString());
                inv.Kodi = rreshti[1].ToString();
                inv.Pershkrimi = rreshti[2].ToString();
                invt.Add(inv);
            }
            return invt;
        }
    }
}
