using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
   public class colQendraKostoPunonjes : List<clsQendraKostoPunonjes>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parameter
        /// </summary>
        public colQendraKostoPunonjes()
        {
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsQendraKostoPunesimi</param>
        public colQendraKostoPunonjes(IEnumerable<clsQendraKostoPunonjes> collection)
            : base(collection)
        {

        }
        /// <summary>
        /// kontrukutori me 1 parametra
        /// merr punesimet e nje punonjesi
        /// </summary>
        /// <param name="idpunonjes"> idpunonjes</param>
        public colQendraKostoPunonjes(int idpunonjes)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                mbushQendraPunonjes(db.ktheQendraKostoPunonjesSipasIdPunonjesi(idpunonjes));
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsQendraKostoPunonjes"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsQendraKostoPunonjes this[int index]
        {
            get { return ((clsQendraKostoPunonjes)base[index]); }
        }

       public static DataTable ktheQendraPunonjesi(int idpunonjes)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
              return db.ktheQendraKostoPunonjesSipasIdPunonjesi(idpunonjes);
            }
       }
        public static DataTable ktheQendraKostoNdermarrjesAndAutorizimeDTExport(int idnderm)
        {
            using (clsDatabazeListPagesa dbartikuj = new clsDatabazeListPagesa())
            {
                DataTable tabela = dbartikuj.ktheQendraKostoNdermarrjesAndAutorizimeDTExport(idnderm);
                return tabela;
            }
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt"> data table me te dhenat e tipit punesim</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushQendraPunonjes(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsQendraKostoPunesimi skema = new clsQendraKostoPunesimi();
                    //skema.mbushQendraKosto(rreshti);
                    Add(new clsQendraKostoPunonjes(rreshti));
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
