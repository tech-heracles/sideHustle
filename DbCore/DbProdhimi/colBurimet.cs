using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsBurime
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsBurime"/>
    public class colBurimet : List<clsBurime>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colBurimet()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// merr burimet te ndermarjes 
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colBurimet(int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushBurim(db.ktheGjitheBurimetSipasNdermarjes(idndermarje));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsBurime</param>
        public colBurimet(IEnumerable<clsBurime> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsBurime"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsBurime this[int index]
        {
            get
            {
                return ((clsBurime)base[index]);
            }
        }

        /// <summary>
        /// merr burimet sipas id ne formen e data row
        /// </summary>
        /// <param name="idburim"> id burimit</param>
        /// <returns> kthen data row me kete burim</returns>
        public static DataRow merrBurimSipasIdDR(int idburim)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataRow dr = db.merrBurimSipasIdDR(idburim);
            db.Dispose();
            return dr;
        }

        /// <summary>
        /// merr burim sipas ndermarje  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto burime</returns>
        public static DataTable merrBurimeNdermarjeDT(int idnderm)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.merrBurimDT(idnderm);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// merr burim sipas ndermarje aktive  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto burime</returns>
        public static DataTable merrBurimeNdermarjeDTAktive(int idnderm)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.merrBurimDTAktive(idnderm);
            db.Dispose();
            return dt;
        }
        public static DataTable ktheGjitheBurimetSipasNdermarjesLikeDt(int idnderm, string kodi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.ktheGjitheBurimetSipasNdermarjesLikeDt(idnderm, kodi);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// mbush te gjitha burimet sipas ndermarrjes 
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheBurimetSipasNdermarjes(int idnder)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushBurim(db.ktheGjitheBurimetSipasNdermarjes(idnder));
            db.Dispose();
            return mbush;            
        }

        /// <summary>
        /// mbush te gjitha burimet sipas ndermarrjes  aktive
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheBurimeSipasNdermarjesAktive(int idnder)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushBurim(db.ktheGjitheBurimetSipasNdermarjesAktiv(idnder));
            db.Dispose();
            return mbush;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit burim</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushBurim(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsBurime burim = new clsBurime();
                    //burim.mbushBurim(rreshti);
                    Add(new clsBurime(rreshti));
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
