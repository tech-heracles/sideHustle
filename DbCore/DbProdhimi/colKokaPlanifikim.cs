using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaPlanifikim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokaPlanifikim : List<clsKokaPlanifikim>
    {

        #region Konstruktor

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKokaPlanifikim()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colKokaPlanifikim(int idNdermVit)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushKokaPlanifikim(db.ktheGjitheKokaPlanifikim(idNdermVit), db);
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKokaPlanifikim</param>
        public colKokaPlanifikim(IEnumerable<clsKokaPlanifikim> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// merr gjithe planifikimet sipas inderviti dhe autorizimeve ne forme data table
        /// </summary>
        /// <param name="idndermvit">id e ndermarje vitit</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <returns>data table me keto te dhena</returns>
        public static DataTable merrKokaPlanifikimDT(int idndermvit, int idperdoruesi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.merrKokaPlanifikimDT(idndermvit, idperdoruesi);
            db.Dispose();
            return dt;            
        }

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbProdhimi.clsKokaPlanifikim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaPlanifikim this[int index]
        {
            get { return ((clsKokaPlanifikim)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <param name="dt">data table me te dhenat e tipit koka planifikim</param>
        /// <returns>true ose false ne se coleksioni u mbush ne rregull</returns>
        private bool mbushKokaPlanifikim(DataTable dt, clsDatabazeProdhimi db)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaPlanifikim koka = new clsKokaPlanifikim();
                    //koka.mbushKokaPlanifikim(rreshti, db);
                    Add(new clsKokaPlanifikim(rreshti, db));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion


    }
}
