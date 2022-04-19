using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{ 
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsSigurimet
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsSigurimet"/>
    public class colSigurimet : List<clsSigurimet>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colSigurimet()
        {
        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// merr sigurimet e nje ndermarje
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colSigurimet(int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushSigurime(db.ktheGjitheSigurimetSipasNdermarjes(idndermarje));
            db.Dispose();
        }
        /// <summary>
        /// konstruktori me 2 parametra
        /// merr sigurimet e nje ndermarje ne nje date te caktuar aktivizimi
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="data"> data e aktivizimit</param>
        public colSigurimet(int idndermarje, DateTime data)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushSigurime(db.ktheGjitheSigurimetSipasNdermarjesDheDates(idndermarje, data));
            db.Dispose();
        }
        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsSigurimet</param>
        public colSigurimet(IEnumerable<clsSigurimet> collection)
            : base(collection)
        {
            
        }
         
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsSigurimet"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsSigurimet this[int index]
        {
            get
            {
                return ((clsSigurimet)base[index]);
            }
        }

        /// <summary>
        /// merr sigurime sipas id ne forme data row
        /// </summary>
        /// <param name="idnderm">idndermarje</param>
        /// <param name="idsigurime"> id sigurime</param>
        /// <returns> kthen data row me kete sigurime</returns>
        public static DataRow merrSigurimeSipasNdermarjesDR(int idnderm, int idsigurime)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataRow dr = db.merrSigurimeSipasNdermarjesDR(idnderm, idsigurime);
            db.Dispose();
            return dr;
        }

        /// <summary>
        /// merr sigurime sipas ndermarje ne forme data table 
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto sigurime</returns>
        public static DataTable merrSigurimeNdermarjeDT(int idnderm)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrSigurimeDT(idnderm);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// merr sigurime sipas ndermarje  dhe dates ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <param name="data">data e aktivizimit</param>
        /// <returns> kthen data table me keto sigurime</returns>
        public static DataTable merrSigurimeNdermarjeDTSipasDates(int idnderm,  DateTime data)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrSigurimeNdermarjeDTSipasDates(idnderm, data);
            db.Dispose();
            return dt;            
        }

        /// <summary>
        /// mbush te gjitha sigurimet sipas ndermarrjes 
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheSigurimetSipasNdermarjes(int idnder)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushSigurime(db.ktheGjitheSigurimetSipasNdermarjes(idnder));
            db.Dispose();
            return mbush;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt"> data table me te dhena te tipit sigurime</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushSigurime(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsSigurimet sig = new clsSigurimet();
                    //sig.mbushSigurime(rreshti);
                    Add(new clsSigurimet(rreshti));
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
