using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
   public class colBankaPunonjes: List<clsBankaPunonjes>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parameter
        /// </summary>
        public colBankaPunonjes()
        {
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsBankaPunonjes</param>
        public colBankaPunonjes(IEnumerable<clsBankaPunonjes> collection)
            : base(collection)
        {

        }
        /// <summary>
        /// kontrukutori me 1 parametra
        /// merr punesimet e nje punonjesi
        /// </summary>
        /// <param name="idpunonjes"> idpunonjes</param>
        public colBankaPunonjes(int idpunonjes)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushBanka(db.ktheBankaPunonjesSipasIdPuneonjesi(idpunonjes));
            db.Dispose();
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsBankaPunonjes"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsBankaPunonjes this[int index]
        {
            get { return ((clsBankaPunonjes)base[index]); }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt"> data table me te dhenat e tipit punesim</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushBanka(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsBankaPunonjes skema = new clsBankaPunonjes();
                    //skema.mbushBanka(rreshti);
                    Add(new clsBankaPunonjes(rreshti));
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
