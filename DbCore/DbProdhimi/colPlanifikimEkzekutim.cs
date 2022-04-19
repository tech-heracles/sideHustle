using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsPlanifikimEkzekutim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsPlanifikimEkzekutim"/>
    public class colPlanifikimEkzekutim : List<clsPlanifikimEkzekutim>
    {

        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colPlanifikimEkzekutim()
        {
        }

        /// <summary>
        /// konstruktori me nje parameter merr dokumentat e planifikimit te ketij dokumenti ekzekutimi
        /// </summary>
        /// <param name="idekzekutimi">id ekzekutimi</param>
        public colPlanifikimEkzekutim(int idekzekutimi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushPlanifikimEkzekutim(db.ktheDokPlanifikimiTeDokEkzekutimi(idekzekutimi));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsPlanifikimEkzekutim</param>
        public colPlanifikimEkzekutim(IEnumerable<clsPlanifikimEkzekutim> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsPlanifikimEkzekutim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsPlanifikimEkzekutim this[int index]
        {
            get { return ((clsPlanifikimEkzekutim)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit planifikim ekzekutim</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushPlanifikimEkzekutim(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsPlanifikimEkzekutim planekze = new clsPlanifikimEkzekutim();
                    //planekze.mbushPlanifikimEkzekutim(rreshti);
                    Add(new clsPlanifikimEkzekutim(rreshti));
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
