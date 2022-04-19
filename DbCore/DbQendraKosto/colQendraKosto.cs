using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsQendraKosto
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsQendraKosto"/>
    public class colQendraKosto : List<clsQendraKosto>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colQendraKosto()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// merr strukturat administrative te ndermarjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colQendraKosto(int idndermarje):this(idndermarje,new clsDatabaseQendraKosto())
        {
            
        }
        public colQendraKosto(int idndermarje, clsDatabaseQendraKosto db)
        {
                mbushQendraKosto(db.ktheGjitheQendraKostoSipasNdermarjes(idndermarje));
            
        }
        /// <summary>
        ///  konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsStrukturaAdministrative </param>
        public colQendraKosto(IEnumerable<clsQendraKosto> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsQendraKosto"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsQendraKosto this[int index]
        {
            get
            {
                return ((clsQendraKosto)base[index]);
            }
        }

        /// <summary>
        /// merr qendren sipas id ne forme data row
        /// </summary>
        /// <param name="id"> id qendra</param>
        /// <returns> kthen data row me kete strukture</returns>
        public static DataRow merrQendraKostoDR(int id)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return db.merrQendraKostoDR(id);
            }
        }

        /// <summary>
        /// merr qendrat sipas ndermarje ne forme data table 
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto qendra</returns>
        public static DataTable merrQendraKostoDT(int idnderm)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return db.merrQendraKostoDT(idnderm);
            }
        }

        /// <summary>
        /// mbush te gjitha qendrat sipas ndermarrjes
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheQendraKostoSipasNdermarjes(int idnder)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return mbushGjitheQendraKostoSipasNdermarjes(idnder, db);
            }
        }
        public bool mbushGjitheQendraKostoSipasNdermarjes(int idnder, clsDatabaseQendraKosto db)
        {
            return mbushQendraKosto(db.ktheGjitheQendraKostoSipasNdermarjes(idnder));
        }
        /// <summary>
        /// mbush te gjitha qendrat sipas ndermarrjes aktiv
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheQendraKostoSipasNdermarjesAktiv(int idnder)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return mbushQendraKosto(db.ktheGjitheQendraKostoSipasNdermarjesAktiv(idnder));
            }
        }

        /// <summary>
        /// mbush te gjitha qendrat prind sipas ndermarrjes
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheQendraKostoPrindiSipasNdermarjes(int idnder)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return mbushQendraKosto(db.ktheGjitheQendraKostoPrindiSipasNdermarjes(idnder));
            }
        }
        /// <summary>
        /// mbush te gjitha qendrat prind sipas ndermarrjes aktiv
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheQendraKostoPrindiSipasNdermarjesAktiv(int idnder)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return mbushQendraKosto(db.ktheGjitheQendraKostoPrindiSipasNdermarjesAktiv(idnder));
            }
        }
        public bool ktheGjitheQendraKostoPrindiNiveli1SipasNdermarjesAktiv(int idnder)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return mbushQendraKosto(db.ktheGjitheQendraKostoPrindiNiveli1SipasNdermarjesAktiv(idnder));
            }
        }
        /// <summary>
        /// mbush te gjitha qendrat bij sipas ndermarrjes aktiv
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheQendraKostoBijSipasNdermarjesAktiv(int idnder)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return mbushQendraKosto(db.ktheGjitheQendraKostoBijSipasNdermarjesAktiv(idnder));
            }
        }
        public static bool mbushGjitheQendraKostoPrindJoFundoreSipasNdermarjesAktiv(int idnder, string Kodi)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return db.ktheGjitheQendraKostoPrindJoFundoreSipasNdermarjesAktiv(idnder, Kodi);
            }
        }
        /// <summary>
        /// merr qendrat bij te nje qendre  prind
        /// </summary>
        /// <param name="idprindi">id e prindit</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushQendraSipasPrindit(int idprindi)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return mbushQendraKosto(db.ktheQendraKostoSipasPrindit(idprindi));
            }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt"> data table me te dhenat e tipit qendra kosto</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushQendraKosto(DataTable dt)
        {
            //try
            //{

            foreach (DataRow rreshti in dt.Rows)
            {
                //clsQendraKosto qendra = new clsQendraKosto();
                //qendra.mbushQendraKosto(rreshti);
                Add(new clsQendraKosto(rreshti));
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
