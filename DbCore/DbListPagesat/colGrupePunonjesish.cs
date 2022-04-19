using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsGrupPunonjesish
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsGrupPunonjesish"/>
    public class colGrupePunonjesish : List<clsGrupPunonjesish>
    {
        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colGrupePunonjesish()
        {

        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// merr grupet e punonjesve te nje ndermarje
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        public colGrupePunonjesish(int idnderm)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            mbushGrupePunonjesish(data.ktheGjitheGrupetPunonjesishSipasNdermarjes(idnderm));
            data.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsGrupPunonjesish</param>
        public colGrupePunonjesish(IEnumerable<clsGrupPunonjesish> collection)
            : base(collection)
        {
            
        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// kthen objektin <see cref="clsGrupPunonjesish"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsGrupPunonjesish this[int index]
        {
            get { return ((clsGrupPunonjesish)base[index]); }
        }


        #endregion

        #region Metoda Private
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipi grup punonjes</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushGrupePunonjesish(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupPunonjesish grupPunonjesish = new clsGrupPunonjesish();
                    //grupPunonjesish.mbushGrupPunonjesish(rreshti);
                    Add(new clsGrupPunonjesish(rreshti));
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
