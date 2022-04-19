using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsNjesiProdhimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colNjesiProdhimi : System.Collections.Generic.List<clsNjesiProdhimi>
    {

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsPikeShitjeFurnizimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsNjesiProdhimi this[int index]
        {
            get { return ((clsNjesiProdhimi)base[index]); }
        }

        public static DataTable merrNjesiProdhimiSipasNdermarrjeDt(int idnderm)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataTable dt = dbRegj.merrNjesiProdhimiSipasNdermarrjeDt(idnderm);
            dbRegj.Dispose();
            return dt;
        }

        public static DataTable merrNjesiProdhimiSipasNdermarrjeAktiveDt(int idnderm)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataTable dt = dbRegj.merrNjesiProdhimiSipasNdermarrjeAktiveDt(idnderm);
            dbRegj.Dispose();
            return dt;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsPikeShitjeFurnizimi"/> 
        /// </summary>
        private bool mbushNjesiteEProdhimit(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsNjesiProdhimi njesiProdhimi = new clsNjesiProdhimi();
                    //njesiProdhimi.mbushNjesiProdhimi(rreshti);
                    Add(new clsNjesiProdhimi(rreshti));
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
