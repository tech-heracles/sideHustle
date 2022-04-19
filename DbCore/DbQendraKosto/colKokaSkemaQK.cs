using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaSkemaQK
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsAktiviteteKoka"/>
    public class colKokaSkemaQK: List<clsKokaSkemaQK>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKokaSkemaQK()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// merr skemat e ndermarjes 
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colKokaSkemaQK(int idndermarje)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mbushSkema(db.ktheGjitheSkematSipasNdermarjes(idndermarje));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKokaSkemaQK</param>
        public colKokaSkemaQK(IEnumerable<clsKokaSkemaQK> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKokaSkemaQK"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKokaSkemaQK this[int index]
        {
            get
            {
                return ((clsKokaSkemaQK)base[index]);
            }
        }

        /// <summary>
        /// merr skemen sipas id ne formen e data row
        /// </summary>
        /// <param name="idkoka"> id koka</param>
        /// <returns> kthen data row me kete koka</returns>
        public static DataRow merrSkemenSipasIdDR(int idkoka)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            DataRow dr = db.merrKokaSkemeSipasIdDR(idkoka);
            db.Dispose();
            return dr;            
        }

        /// <summary>
        /// merr skemen sipas ndermarje  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto skema</returns>
        public static DataTable merrSkemenNdermarjeDT(int idnderm)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            DataTable dt = db.merrKokaSkemaDT(idnderm);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// mbush te gjitha skemat sipas ndermarrjes 
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheSkematSipasNdermarjes(int idnder)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            bool mbush = mbushSkema(db.ktheGjitheSkematSipasNdermarjes(idnder));
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush te gjitha skemat sipas ndermarrjes like
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <param name="kodi">kodi</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheSkematSipasNdermarjesLike(int idnder,string kodi)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            bool mbush = mbushSkema(db.ktheGjitheSkematSipasNdermarjesLike(idnder, kodi));
            db.Dispose();
            return mbush;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit skema koka</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushSkema(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaSkemaQK skema = new clsKokaSkemaQK();
                    //skema.mbushSkeme(rreshti);
                    Add(new clsKokaSkemaQK(rreshti));
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
