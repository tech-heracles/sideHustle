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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsStatusRiparimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public  class colStatusRiparimi: System.Collections.Generic.List<clsStatusRiparimi>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colStatusRiparimi()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter integer
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        public colStatusRiparimi(int idnder, int idperdoruesi)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            mbushStatusRiparimi(db.ktheGjitheStatusRiparimeSipasNdermarrjes(idnder, idperdoruesi));
            db.Dispose();

        }
        public colStatusRiparimi(int idnder)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            mbushStatusRiparimi(db.ktheGjitheStatusRiparimeSipasNdermarrjesPaAutorizime(idnder));
            db.Dispose();

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsStatusRiparimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsStatusRiparimi this[int index]
        {
            get { return ((clsStatusRiparimi)base[index]); }
        }


        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsStatusRiparimi"/> 
        /// </summary>
        private bool mbushStatusRiparimi(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsStatusRiparimi statusriparimi = new clsStatusRiparimi();
                    //statusriparimi.mbushStatusRiparime(rreshti);
                    this.Add(new clsStatusRiparimi(rreshti));
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