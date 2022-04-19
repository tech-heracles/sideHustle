using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbTollona
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsBurime
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsArtikullTolloni"/>
    public class colArtikullTolloni : List<clsArtikullTolloni>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colArtikullTolloni()
        {
        }


        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsArtikullTolloni</param>
        public colArtikullTolloni(IEnumerable<clsArtikullTolloni> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsArtikullTolloni"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsArtikullTolloni this[int index]
        {
            get
            {
                return ((clsArtikullTolloni)base[index]);
            }
        }

        ///// <summary>
        ///// merr burimet sipas id ne formen e data row
        ///// </summary>
        ///// <param name="idburim"> id burimit</param>
        ///// <returns> kthen data row me kete burim</returns>
        //public static DataRow merrBurimSipasIdDR(int idburim)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //   DataRow dr = db.merrBurimSipasIdDR(idburim);
        //    db.Dispose();
        //    return dr;
        //}

        ///// <summary>
        ///// merr burim sipas ndermarje  ne forme data table
        ///// </summary>
        ///// <param name="idnderm"> idndermarje</param>
        ///// <returns> kthen data table me keto burime</returns>
        //public static DataTable merrBurimeNdermarjeDT(int idnderm)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    DataTable dt = db.merrBurimDT(idnderm);
        //    db.Dispose();
        //    return dt;
        //}

        ///// <summary>
        ///// merr burim sipas ndermarje aktive  ne forme data table
        ///// </summary>
        ///// <param name="idnderm"> idndermarje</param>
        ///// <returns> kthen data table me keto burime</returns>
        //public static DataTable merrBurimeNdermarjeDTAktive(int idnderm)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    DataTable dt = db.merrBurimDTAktive(idnderm);
        //    db.Dispose();
        //    return dt;
        //}
        //public static DataTable ktheGjitheBurimetSipasNdermarjesLikeDt(int idnderm, string kodi)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    DataTable dt = db.ktheGjitheBurimetSipasNdermarjesLikeDt(idnderm, kodi);
        //    db.Dispose();
        //    return dt;
        //}

        ///// <summary>
        ///// mbush te gjitha burimet sipas ndermarrjes 
        ///// </summary>
        ///// <param name="idnder">id e ndermarrjes</param>
        ///// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        //public bool mbushGjitheBurimetSipasNdermarjes(int idnder)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    bool mbush = mbushBurim(db.ktheGjitheBurimetSipasNdermarjes(idnder));
        //    db.Dispose();
        //    return mbush;
        //}

        ///// <summary>
        ///// mbush te gjitha burimet sipas ndermarrjes  aktive
        ///// </summary>
        ///// <param name="idnder">id e ndermarrjes</param>
        ///// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        //public bool mbushGjitheBurimeSipasNdermarjesAktive(int idnder)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    bool mbush = mbushBurim(db.ktheGjitheBurimetSipasNdermarjesAktiv(idnder));
        //    db.Dispose();
        //    return mbush;
        //}

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit burim</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushArtikull(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsArtikullTolloni artikull = new clsArtikullTolloni();
                    //artikull.mbushArtikull(rreshti);
                    Add(new clsArtikullTolloni(rreshti));
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
