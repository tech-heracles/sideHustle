using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaRiparime
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
  public  class colKokaRiparime: List<clsKokaRiparime>
    {

        #region Konstruktor

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKokaRiparime()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colKokaRiparime(int idNdermVit)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushKokaRiparime(db.ktheGjitheKokaRiparime(idNdermVit), db);
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKokaRiparime</param>
        public colKokaRiparime(IEnumerable<clsKokaRiparime> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// merr gjithe riparimet sipas inderviti dhe autorizimeve ne forme data table
        /// </summary>
        /// <param name="idndermvit">id e ndermarje vitit</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <returns>data table me keto te dhena</returns>
        public static DataTable merrKokaRiparimeDT(int idndermvit, int idperdoruesi)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            DataTable dt = db.merrKokaRiparimeDT(idndermvit, idperdoruesi);
            db.Dispose();
            return dt;            
        }
        public static DataTable ktheHistorikuDT(int idkoka)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            DataTable dt = db.ktheHistorikuDT(idkoka);
            db.Dispose();
            return dt;            
        }

        /// <summary>
        /// kthen objektin <see cref="clsKokaRiparime"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaRiparime this[int index]
        {
            get { return ((clsKokaRiparime)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <param name="dt">data table me te dhenat e tipit koka riparim</param>
        /// <returns>true ose false ne se coleksioni u mbush ne rregull</returns>
        private bool mbushKokaRiparime(DataTable dt, clsDatabaseRegjistrim db)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaRiparime koka = new clsKokaRiparime();
                    //koka.mbushKokaRiparime(rreshti, db);
                    Add(new clsKokaRiparime(rreshti, db));
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
