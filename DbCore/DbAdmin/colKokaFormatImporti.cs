using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace DbCore.DbAdmin
{
    public class colKokaFormatImporti : System.Collections.Generic.List<clsKokaFormatImporti>
    {
        #region Metoda Publike

        public new clsKokaFormatImporti this[int index]
        {
            get { return ((clsKokaFormatImporti)base[index]); }
        }

        public static colKokaFormatImporti ktheFormatImportiNdermarrjes(int idNdermarja)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            colKokaFormatImporti modelet = new colKokaFormatImporti();
            modelet.mbushFormatImporti(dbAdmin.merrGjitheFormatImportiNdermarrjes(idNdermarja));
            dbAdmin.Dispose();
            return modelet;
        }
        public static colKokaFormatImporti ktheFormatImportiSipasKategorise(int idNdermarja, int idkategori)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            colKokaFormatImporti modelet = new colKokaFormatImporti();
            modelet.mbushFormatImporti(dbAdmin.merrGjitheFormatImportiNdermarjesDheKategorise(idNdermarja, idkategori));
            dbAdmin.Dispose();
            return modelet;
        }
        /// <summary>
        /// Kthen nje datatable me te formatet e importit dt sipas te drejtave
        /// </summary>
        /// <param name="idNdermarje"></param>
        /// <param name="idViti"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="komponente"></param>
        /// <returns></returns>
        public static DataTable ktheFormatImportiDTSipasTeDrejtave(int idNdermarje, int idViti, int idPerdoruesi, string komponente)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DataTable result = dbAdmin.merrFormatImportiDTSipasTeDrejtave(idNdermarje, idViti, idPerdoruesi, komponente);
            dbAdmin.Dispose();
            return result;
        }

        /// <summary>
        /// Kthen nje datatable me te gjitha formatet e importit sipas kategorise dt
        /// </summary>
        /// <param name="idNdermarje"></param>
        /// <returns></returns>
        public static DataTable ktheFormatImportiSipasKategoriseDT(int idNdermarje, int lloji)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DataTable result = dbAdmin.merrFormatImportiSipasKategoriseDT(idNdermarje, lloji);
            dbAdmin.Dispose();
            return result;
        }
        #endregion
        public static DataTable MerrFormatetsipasNdermarjesDheKategorise(int idNdermarja, int idkategori)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            colKokaFormatImporti modelet = new colKokaFormatImporti();
            DataTable result = dbAdmin.merrGjitheFormatImportiNdermarjesDheKategorise(idNdermarja, idkategori);
            dbAdmin.Dispose();
            return result;
        }
        #region Metoda Private

        private bool mbushFormatImporti(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaFormatImporti info = new clsKokaFormatImporti();
                    //info.mbushFormatImporti(rreshti);
                    Add(new clsKokaFormatImporti(rreshti));
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
