using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsUrdherPorosiPlanifikim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsUrdherPorosiPlanifikim"/>
    public class colUrdherPorosiPlanifikim : System.Collections.Generic.List<clsUrdherPorosiPlanifikim>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colUrdherPorosiPlanifikim()
        {
        }
       /// <summary>
       /// konstruktori me nje parameter
       /// </summary>
       /// <param name="idplanifikimi">id e planifikimit</param>
        public colUrdherPorosiPlanifikim(int idplanifikimi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushUrdherPorosiPlanifikim(db.ktheDokUrdherPorosiTePlanifikimir(idplanifikimi));
            db.Dispose();
        }
        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsUrdherPorosiPlanifikim</param>
        public colUrdherPorosiPlanifikim(IEnumerable<clsUrdherPorosiPlanifikim> collection)
            : base(collection)
        {
            
        }
         
        #endregion

        #region Metoda Publike
        /// <summary>
        /// kthen objektin <see cref="clsUrdherPorosiPlanifikim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsUrdherPorosiPlanifikim this[int index]
        {
            get { return ((clsUrdherPorosiPlanifikim)base[index]); }
        }



        #endregion

        #region Metoda Private
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit urdher porosi planifikim</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushUrdherPorosiPlanifikim(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsUrdherPorosiPlanifikim upp = new clsUrdherPorosiPlanifikim();
                    //upp.mbushUrdherPorosiPlanifikim(rreshti);
                    Add(new clsUrdherPorosiPlanifikim(rreshti));
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

