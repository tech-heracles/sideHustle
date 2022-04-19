using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTipeKontrate
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsTipeKontrate"/>
    public class colTipeKontrate : List<clsTipeKontrate>
    {
        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colTipeKontrate()
        {

        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// merr tipet e kontrates ne baze te ndermarjes
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        public colTipeKontrate(int idnderm)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            mbushTipeKontrate(data.ktheGjitheTipeKontrateSipasNdermarjes(idnderm));
            data.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection"> koleksion me clsTipeKontrate</param>
        public colTipeKontrate(IEnumerable<clsTipeKontrate> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// kthen objektin <see cref="clsTipeKontrate"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTipeKontrate this[int index]
        {
            get { return ((clsTipeKontrate)base[index]); }
        }
        #endregion

        #region Metoda Private
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt"> data table me te dhenat e tipit tip kontrate</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushTipeKontrate(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTipeKontrate tipeKontrate = new clsTipeKontrate();
                    //tipeKontrate.mbushTipKontrate(rreshti);
                    Add(new clsTipeKontrate(rreshti));
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
