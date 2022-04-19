using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{ 
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKategoriPage
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsKategoriPage"/>
    public class colKategoriPage: List<clsKategoriPage>
    {


        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKategoriPage()
        {
        }

        /// <summary>
        /// konstruktori me 2 parametra
        /// merr kategorite e pagese te ndermarjes sipas tipit
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="tipi"> tipi i kategorise</param>
        /// <see cref="LlojPagese"/>
        public colKategoriPage(int idndermarje, int tipi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushKategori(db.ktheGjitheKategoriPageSipasNdermarrjesDheTipit(idndermarje, tipi));
            db.Dispose();
        }
       
        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection"> koleksion me clsKategoriPage</param>
        public colKategoriPage(IEnumerable<clsKategoriPage> collection)
            : base(collection)
        {
            
        }

        /// <summary>
        /// konstruktroi qe implementon klasen baze duke i dhene madhesine e koleksionit
        /// </summary>
        /// <param name="capacity">madhesia e koleksionit</param>
        public colKategoriPage(int capacity)
            : base(capacity)
        {
            
        }
         
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKategoriPage"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKategoriPage this[int index]
        {
            get
            {
                return ((clsKategoriPage)base[index]);
            }
        }

      
       
        /// <summary>
        /// mbush te gjitha kategorite sipas ndermarrjes  dhe tipit
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <param name="tipi"> tipi i pageses</param>
        /// <see cref="LlojPagese"/>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKategoriPageSipasNdermarjesDheTipit(int idnder, int tipi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushKategori(db.ktheGjitheKategoriPageSipasNdermarrjesDheTipit(idnder, tipi));
            db.Dispose();
            return mbush;            
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt"> data table me te dhena te tipit kategori page</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushKategori(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKategoriPage kat = new clsKategoriPage();
                    //kat.mbushKategori(rreshti);
                    Add(new clsKategoriPage(rreshti));
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
