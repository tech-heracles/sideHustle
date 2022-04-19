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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsLidhesPerKontroll
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colLidhesPerKontroll : System.Collections.Generic.List<clsLidhesPerKontroll>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colLidhesPerKontroll()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        public colLidhesPerKontroll(int idNivel)
        {
            clsDatabaseRegjistrim dbLidhes = new clsDatabaseRegjistrim();
            mbushLidhesetPerKontroll(dbLidhes.ktheLidhesPerKontrollSipasIDNiveli(idNivel));
            dbLidhes.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsLidhesPerKontroll"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsLidhesPerKontroll this[int index]
        {
            get { return ((clsLidhesPerKontroll)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsTabPerKontroll"/> 
        /// </summary>
        private bool mbushLidhesetPerKontroll(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLidhesPerKontroll lidhes = new clsLidhesPerKontroll();
                    //lidhes.mbushLidhes(rreshti);
                    Add(new clsLidhesPerKontroll(rreshti));
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
        [Obsolete("Perdor: bool mbushLidhesetPerKontroll(DataTable dt)", true)]
        public colLidhesPerKontroll mbushArrayListLidhesPerKontroll(DataSet ds)
        {
            colLidhesPerKontroll lidhesPerKontroll = new colLidhesPerKontroll();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLidhesPerKontroll lidhes = new clsLidhesPerKontroll();

                lidhes.Id = int.Parse(rreshti[0].ToString());
                lidhes.IdNivel = int.Parse(rreshti[1].ToString());
                lidhes.IdTabPerKontroll = int.Parse(rreshti[2].ToString());

                lidhesPerKontroll.Add(lidhes);
            }
            return lidhesPerKontroll;
        }
    }
}
