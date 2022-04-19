using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
  public  class colVendndodhjet: List<clsVendndodhjet>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colVendndodhjet()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// merr burimet te ndermarjes 
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colVendndodhjet(int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushVendndodhjet(db.ktheGjitheVendndodhjetSipasNdermarjes(idndermarje));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsVendndodhjet</param>
        public colVendndodhjet(IEnumerable<clsVendndodhjet> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsVendndodhjet"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsVendndodhjet this[int index]
        {
            get
            {
                return ((clsVendndodhjet)base[index]);
            }
        }

        /// <summary>
        /// merr burimet sipas id ne formen e data row
        /// </summary>
        /// <param name="id"> id burimit</param>
        /// <returns> kthen data row me kete burim</returns>
        public static DataRow merrVendndodhjetSipasIdDR(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataRow dr = db.merrVendndodhjetSipasIdDR(id);
            db.Dispose();
            return dr;
        }

        /// <summary>
        /// merr burim sipas ndermarje  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto burime</returns>
        public static DataTable merrVendndodhjetDT(int idnderm)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrVendndodhjetDT(idnderm);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// merr burim sipas ndermarje aktive  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto burime</returns>
        public static DataTable merrVendndodhjetDTAktive(int idnderm)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrVendndodhjetDTAktive(idnderm);
            db.Dispose();
            return dt;
        }


        /// <summary>
        /// mbush te gjitha burimet sipas ndermarrjes 
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool ktheGjitheVendndodhjetSipasNdermarjes(int idnder)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushVendndodhjet(db.ktheGjitheVendndodhjetSipasNdermarjes(idnder));
            db.Dispose();
            return mbush;            
        }

        /// <summary>
        /// mbush te gjitha burimet sipas ndermarrjes  aktive
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool ktheGjitheVendndodhjetSipasNdermarjesAktiv(int idnder)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushVendndodhjet(db.ktheGjitheVendndodhjetSipasNdermarjesAktiv(idnder));
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
        private bool mbushVendndodhjet(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsVendndodhjet vendndodhje = new clsVendndodhjet();
                    //vendndodhje.mbushVendndodhjet(rreshti);
                    Add(new clsVendndodhjet(rreshti));
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
