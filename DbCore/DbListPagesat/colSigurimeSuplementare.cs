using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{ 
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsSigurimeSuplementare
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsSigurimeSuplementare"/>
    public class colSigurimeSuplementare : List<clsSigurimeSuplementare>
    {


        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colSigurimeSuplementare()
        {
        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// merr sigurimet suplementare sipas ndermarjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colSigurimeSuplementare(int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushSigurim(db.ktheGjitheSigurimeSuplementareSipasNdermarrjes(idndermarje));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsSigurimeSuplementare</param>
        public colSigurimeSuplementare(IEnumerable<clsSigurimeSuplementare> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsSigurimeSuplementare"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsSigurimeSuplementare this[int index]
        {
            get
            {
                return ((clsSigurimeSuplementare)base[index]);
            }
        }



        /// <summary>
        /// mbush te gjitha sigurime sipas ndermarrjes  
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheSigurimeSuplementareSipasNdermarjes(int idnder)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushSigurim(db.ktheGjitheSigurimeSuplementareSipasNdermarrjes(idnder));
            db.Dispose();
            return mbush;
        }
        /// <summary>
        /// mbush te gjitha sigurimet suplementare sipas ndermarrjes  dhe dates
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <param name="dtaktivizimi"> data e aktivizimit</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheSigurimeSuplementareSipasNdermarjesDheDates(int idnder, DateTime dtaktivizimi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushSigurim(db.ktheGjitheSigurimeSuplementareSipasNdermarrjesDheDates(idnder, dtaktivizimi));
            db.Dispose();
            return mbush;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit Sigurime suplementare</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushSigurim(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsSigurimeSuplementare kat = new clsSigurimeSuplementare();
                    //kat.mbushSigurim(rreshti);
                    Add(new clsSigurimeSuplementare(rreshti));
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
