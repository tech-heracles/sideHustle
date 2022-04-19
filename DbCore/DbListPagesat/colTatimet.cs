using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTatime
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsTatime"/>
    public class colTatimet : List<clsTatime>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colTatimet()
        {
        }

        /// <summary>
        /// konstruktori me 1 parameter
        /// merr tatimet e nje ndermarje
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colTatimet(int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushTatimet(db.ktheGjitheTatimeSipasNdermarjes(idndermarje));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori me 2 parametra
        /// merr tatimet e nje ndermarje ne nje date te caktuar aktivizimi
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="data"> data e aktivizimit</param>
        public colTatimet(int idndermarje, DateTime data)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushTatimet(db.ktheGjitheTatimeSipasNdermarjesDheDates(idndermarje, data));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection"> koleksion me clsTatime</param>
        public colTatimet(IEnumerable<clsTatime> collection)
            : base(collection)
        {
            
        }
         
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsTatime"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTatime this[int index]
        {
            get
            {
                return ((clsTatime)base[index]);
            }
        }

        /// <summary>
        /// merr tatime sipas id ne forme data row
        /// </summary>
        /// <param name="idnderm">idndermarje</param>
        /// <param name="idtatime"> id tatime</param>
        /// <returns> kthen data row me kete tatim</returns>
        public static DataRow merrTatimeSipasNdermarjesDR(int idnderm, int idtatime)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataRow dr = db.merrTatimeSipasNdermarjesDR(idnderm, idtatime);
            db.Dispose();
            return dr;            
        }

        /// <summary>
        /// merr tatime sipas ndermarje  ne forme datatable
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto tatime</returns>
        public static DataTable merrTatimeNdermarjeDT(int idnderm)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrTatimeDT(idnderm);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// merr tatime sipas ndermarje  dhe dates ne forme datatable
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <param name="data">data e aktivizimit</param>
        /// <returns> kthen data table me keto tatime</returns>
        public static DataTable merrTatimeNdermarjeDTSipasDates(int idnderm, DateTime data)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrTatimeNdermarjeDTSipasDates(idnderm, data);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// merr tatime me te fundit te ndermarjes ne kete date 
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <param name="data">data e aktivizimit</param>
        /// <returns> kthen nje koleksion me keto tatime </returns>
        public  void merrTatimeNdermarjeSipasDatesDeri(int idnderm, DateTime data)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushTatimet( db.ktheGjitheTatimeSipasNdermarjesDheDatesDeri(idnderm, data));
            db.Dispose();
        }

        /// <summary>
        /// mbush te gjitha tatimet sipas ndermarrjes 
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheTatimeSipasNdermarjes(int idnder)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushTatimet(db.ktheGjitheTatimeSipasNdermarjes(idnder));
            db.Dispose();
            return mbush;            
        }

        /// <summary>
        /// merr datat ne te cilat kemi tatime per kete ndermarje
        /// </summary>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <returns>liste me datat</returns>
        public static List<string> merrDataKomponente(int idnderm)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            List<string> list = new List<string>();
            DataTable dt = db.merrDataTatimeSipasNdermarjes(idnderm);
            foreach (DataRow rreshti in dt.Rows)
            {
                list.Add(DateTime.Parse(rreshti["Date"].ToString()).ToShortDateString());
            }
            db.Dispose();
            return list;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhenat e tipit tatime</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushTatimet(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTatime tatime = new clsTatime();
                    //tatime.mbushTatime(rreshti);
                    Add(new clsTatime(rreshti));
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