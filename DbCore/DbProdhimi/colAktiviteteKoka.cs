using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsAktiviteteKoka
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsAktiviteteKoka"/>
    public class colAktiviteteKoka : List<clsAktiviteteKoka>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colAktiviteteKoka()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// merr aktivitetet e ndermarjes 
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colAktiviteteKoka(int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushAktivitet(db.ktheGjitheAktivitetetSipasNdermarjes(idndermarje));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsAktiviteteKoka</param>
        public colAktiviteteKoka(IEnumerable<clsAktiviteteKoka> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsAktiviteteKoka"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsAktiviteteKoka this[int index]
        {
            get
            {
                return ((clsAktiviteteKoka)base[index]);
            }
        }

        /// <summary>
        /// merr aktivitetin sipas id ne formen e data row
        /// </summary>
        /// <param name="idkoka"> id koka</param>
        /// <returns> kthen data row me kete koka</returns>
        public static DataRow merrAktivitetSipasIdDR(int idkoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataRow dr = db.merrAktivitetSipasIdDR(idkoka);
            db.Dispose();
            return dr;            
        }

        /// <summary>
        /// merr aktivitet sipas ndermarje  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto aktiviteti</returns>
        public static DataTable merrAktivitetetNdermarjeDT(int idnderm)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.merrAktiviteteDT(idnderm);
            db.Dispose();
            return dt;
        }
        public static DataTable merrAktiviteteSipasBurimitDT(int idnderm, int idburimi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.merrAktiviteteSipasBurimitDT(idnderm, idburimi);
            db.Dispose();
            return dt;
        }
        /// <summary>
        /// mbush te gjitha aktivitetet sipas ndermarrjes 
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheAktivitetetSipasNdermarjes(int idnder)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushAktivitet(db.ktheGjitheAktivitetetSipasNdermarjes(idnder));
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush te gjitha aktivitetet sipas ndermarrjes like
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <param name="kodi">kodi</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheAktivitetetSipasNdermarjesLike(int idnder,string kodi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushAktivitet(db.ktheGjitheAktivitetetSipasNdermarjesLike(idnder, kodi));
            db.Dispose();
            return mbush;
        }
        public static DataTable ktheGjitheAktivitetetSipasNdermarjesLikeDt(int idnder, string kodi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable mbush =db.ktheGjitheAktivitetetSipasNdermarjesLikeDt(idnder, kodi);
            db.Dispose();
            return mbush;
        }
        public static DataTable ktheGjitheAktivitetetSipasNdermarjesDheBurimiLikeDt(int idnder, string kodi, int idburimi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable mbush = db.ktheGjitheAktivitetetSipasNdermarjesDheBurimiLikeDt(idnder, kodi, idburimi);
            db.Dispose();
            return mbush;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit aktivitete koka</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushAktivitet(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsAktiviteteKoka aktivitet = new clsAktiviteteKoka();
                    //aktivitet.mbushAktivitet(rreshti);
                    Add(new clsAktiviteteKoka(rreshti));
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
