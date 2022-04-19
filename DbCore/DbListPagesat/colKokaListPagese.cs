using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaListPagese
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsKokaListPagese"/>
    public class colKokaListPagese : List<clsKokaListPagese>
    {

        #region Konstruktor

        /// <summary>
        /// konstrukturi pa parametra
        /// </summary>
        public colKokaListPagese()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// merr gjithe dokumentat e list pageses te nje ndermarje viti
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        //public colKokaListPagese(int idNdermVit)
        //{
        //    clsDatabazeListPagesa db = new clsDatabazeListPagesa();
        //    mbushKokat(db.ktheGjitheKokaListPagese(idNdermVit), db);
        //    db.Dispose();
        //}

        /// <summary>
        ///  konstruktori qe implementon klasen bazekoleksion me clsSkemaSigurimi
        /// </summary>
        /// <param name="collection">koleksion me clsKokaListPagese</param>
        public colKokaListPagese(IEnumerable<clsKokaListPagese> collection)
            : base(collection)
        {

        }




        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKokaListPagese"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaListPagese this[int index]
        {
            get { return ((clsKokaListPagese)base[index]); }
        }

        /// <summary>
        /// merr list pagesat  sipas idnderviti dhe autorizimeve te perdoruesit ne forme data table
        /// </summary>
        /// <param name="idndermvit">id qe lidh nje ndermarje me nje vit</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <returns> kthen nje data table me listpagesat e kesaj ndermarje viti</returns>
        public static DataTable merrKokaListPagesaDT(int idndermvit, int idperdoruesi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrKokaListPageseDT(idndermvit,idperdoruesi);
            db.Dispose();
            return dt;            
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="clsKokaListPagese"/> 
        /// </summary>
        /// <param name="dt">data table me te dhenat e tipit koka list pagesa</param>
        /// <param name="db"> clsDatabazeListPagesa qe perdoret ne rastet e transaksioneve</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        //private bool mbushKokat(DataTable dt, clsDatabazeListPagesa db)
        //{
        //    foreach (DataRow rreshti in dt.Rows)
        //        Add(new clsKokaListPagese(rreshti, db));
        //    return true;
        //}

        #endregion


    }
}
