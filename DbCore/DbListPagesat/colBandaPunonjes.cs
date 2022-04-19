using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
   public class colBandaPunonjes : List<clsBandaPunonjes>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parameter
        /// </summary>
        public colBandaPunonjes()
        {
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsBandaPunonjes</param>
        public colBandaPunonjes(IEnumerable<clsBandaPunonjes> collection)
            : base(collection)
        {

        }
        /// <summary>
        /// kontrukutori me 1 parametra
        /// merr punesimet e nje punonjesi
        /// </summary>
        /// <param name="idpunonjes"> idpunonjes</param>
        public colBandaPunonjes(int idpunonjes)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                mbushBandaPunonjes(db.ktheBandaPunonjesiSipasIdPunonjesi(idpunonjes));
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsQendraKostoPunesimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsBandaPunonjes this[int index]
        {
            get { return ((clsBandaPunonjes)base[index]); }
        }

       public static DataTable ktheBandaPunonjesi(int idpunonjes)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
              return db.ktheBandaPunonjesiSipasIdPunonjesi(idpunonjes);
            }
       }
        public static DataTable ktheBandaNdermarrjesAndAutorizimeDTExport(int idnderm)
        {
            using (clsDatabazeListPagesa dbartikuj = new clsDatabazeListPagesa())
            {
                DataTable tabela = dbartikuj.ktheBandaNdermarrjesAndAutorizimeDTExport(idnderm);
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
        private bool mbushBandaPunonjes(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsQendraKostoPunesimi skema = new clsQendraKostoPunesimi();
                    //skema.mbushQendraKosto(rreshti);
                    Add(new clsBandaPunonjes(rreshti));
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
