using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{ 
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsAmbjent
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colAmbjent : System.Collections.Generic.List<clsAmbjent>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsLlojBuxheti"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsAmbjent this[int index]
        {
            get { return ((clsAmbjent)base[index]); }
        }

        /// <summary>
        /// mbush gjithe llojet e buxheteve
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushGjitheAmbjente(int idllojlicenca, int idGjuha, bool ambjentpermobile)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            bool sukses = mbushAmbjente(dbAdmin.ktheGjitheAmbjente(idllojlicenca, idGjuha, ambjentpermobile));
            dbAdmin.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsLlojBuxheti"/> 
        /// </summary>
        private bool mbushAmbjente(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsAmbjent llojBuxheti = new clsAmbjent();
                    //llojBuxheti.mbushAmbjent(rreshti);
                    Add(new clsAmbjent(rreshti)); 
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
