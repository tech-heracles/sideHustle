using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsLlojDifekti
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colLlojDifekti : System.Collections.Generic.List<clsLlojDifekti>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colLlojDifekti()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter integer
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        public colLlojDifekti(int idnder)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            mbushLlojDifekti(db.ktheGjithellojdifektiSipasNdermarrjes(idnder));
            db.Dispose();

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsLlojDifekti"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsLlojDifekti this[int index]
        {
            get { return ((clsLlojDifekti)base[index]); }
        }


        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsLlojDifekti"/> 
        /// </summary>
        private bool mbushLlojDifekti(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojDifekti llojdifekti = new clsLlojDifekti();
                    //llojdifekti.mbushLlojDifekti(rreshti);
                    this.Add(new clsLlojDifekti(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
       
    }
}
