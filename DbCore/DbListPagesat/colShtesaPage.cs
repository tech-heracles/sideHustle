using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{ 
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsShtesaPage
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsShtesaPage"/>
    public class colShtesaPage : List<clsShtesaPage>
    {


        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colShtesaPage()
        {
        }
        /// <summary>
        /// konstruktori me 2 parametra
        /// merr shtesa page sipas ndermarjes dhe tipit
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="tipi">tipi</param>
        /// <see cref="TipeShtesaPage"/>
        public colShtesaPage(int idndermarje, int tipi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushShtesa(db.ktheGjitheShtesaPageSipasNdermarrjesDheTipit(idndermarje, tipi));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsShtesaPage</param>
        public colShtesaPage(IEnumerable<clsShtesaPage> collection)
            : base(collection)
        {

        }

        /// <summary>
        /// konstruktroi qe implementon klasen baze duke i dhene madhesine e koleksionit
        /// </summary>
        /// <param name="capacity">madhesia e koleksionit</param>
        public colShtesaPage(int capacity)
            : base(capacity)
        {
            
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsShtesaPage"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsShtesaPage this[int index]
        {
            get
            {
                return ((clsShtesaPage)base[index]);
            }
        }



        /// <summary>
        /// mbush te gjitha shtesate sipas ndermarrjes  dhe tipit
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <param name="tipi">tipi i shteses se pages</param>
        /// <see cref="TipeShtesaPage"/>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheShtesaPageSipasNdermarjesDheTipit(int idnder, int tipi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushShtesa(db.ktheGjitheShtesaPageSipasNdermarrjesDheTipit(idnder, tipi));
            db.Dispose();
            return mbush;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit Shtesa page</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushShtesa(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsShtesaPage kat = new clsShtesaPage();
                    //kat.mbushShtesa(rreshti);
                    Add(new clsShtesaPage(rreshti));
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
