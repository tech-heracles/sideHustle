using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
    public class colDiteLeje : System.Collections.Generic.List<clsDiteLeje>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsDiteLeje"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsDiteLeje this[int index]
        {
            get { return ((clsDiteLeje)base[index]); }
        }


        /// <summary>
        /// mbush gjithe kodifikimet e artikullit sipas ndermarrjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public bool ktheGjitheDiteLejeSipasNdermarrjes(int idndermarje)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            bool sukses = mbushDiteLeje(dbKodifikimKF.ktheGjitheDiteLejeSipasNdermarrjes(idndermarje));
            dbKodifikimKF.Dispose();
            return sukses;
        }
        public static DataTable ktheGjitheDiteLejeExport(int idndermarje)
        {
            using (clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa())
            {
                return dbKodifikimKF.merrDiteLejePerExport(idndermarje);
            }

        }
        public bool ktheGjitheDiteLejeSipasPunonjesit(int idpunonjesi)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            bool sukses = mbushDiteLeje(dbKodifikimKF.ktheGjitheDiteLejeSipasPunonjesit(idpunonjesi));
            dbKodifikimKF.Dispose();
            return sukses;
        }
        public static decimal ktheGjitheDiteLejeSipasPunonjesitDhePeriudhes(int idpunonjesi, string muaji, int viti, int idKokaLp)
        {
            using (clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa())
            {
                decimal ore = dbKodifikimKF.ktheGjitheDiteLejeSipasPunonjesitDhePeriudhes(idpunonjesi, muaji, viti, idKokaLp);
                return ore;
            }
        }
        public static Dictionary<int, decimal> KtheGjitheDiteLejeSipasPunonjesveDhePeriudhes(List<int> punonjesitIDs, string muaji, int viti, int idKokaLp)
        {
            using (clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa())
            {
                return dbKodifikimKF.ktheGjitheDiteLejeSipasPunonjesveDhePeriudhes(punonjesitIDs, muaji, viti, idKokaLp);

            }
        }
        #endregion


        #region Metoda Private

        private bool mbushDiteLeje(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsDiteLeje grupKF = new clsDiteLeje();
                //grupKF.mbushDiteLeje(rreshti);
                this.Add(new clsDiteLeje(rreshti));
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

