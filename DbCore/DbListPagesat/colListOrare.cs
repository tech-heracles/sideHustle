using System;
using System.Collections.Generic;
using System.Data;

namespace DbCore.DbListPagesat
{
    public class colListOrare : List<clsListOrare>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsListOrare"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsListOrare this[int index] => base[index];

        public colListOrare(int idNdermarrje) : base(new clsDatabazeListPagesa().ktheGjitheListOrariSipasNdermarrjes(idNdermarrje)) { }

        public static DataTable ktheGjitheListOrariExport(int idndermarje, DateTime datafillimi, DateTime datembarimi)
        {
            using (clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa())
            {
                return dbKodifikimKF.merrListOrarePerExport(idndermarje, datafillimi, datembarimi);
            }

        }
        public bool ktheGjitheListOrariSipasPunonjesit(int idpunonjesi)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            bool sukses = mbushListOrari(dbKodifikimKF.ktheGjitheListOrariSipasPunonjesit(idpunonjesi));
            dbKodifikimKF.Dispose();
            return sukses;
        }
     
        #endregion


        #region Metoda Private

        private bool mbushListOrari(DataTable dt)
        {

            foreach (DataRow rreshti in dt.Rows)
            {

                Add(new clsListOrare(rreshti));
            }

            return true;
        }

        #endregion

        public static DataTable ktheGjitheListOrariSipasPunonjesitSipasPeriudhes(int idPunonjes, DateTime datafill, DateTime datembar)
        {
           return new clsDatabazeListPagesa().ktheGjitheListOrariSipasPunonjesitSipasPeriudhes(idPunonjes, datafill, datembar);
        }
        public static DataTable ktheGjitheListOrariSipasPunonjesveSipasPeriudhes(List<int> idPunonjesish, DateTime datafill, DateTime datembar)
        {
            return new clsDatabazeListPagesa().ktheGjitheListOrariSipasPunonjesveSipasPeriudhes(idPunonjesish, datafill, datembar);
        }
    }
}

