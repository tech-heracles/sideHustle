using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Collections;

namespace DbCore.DbAdmin
{
    public class colInfoTrupi : System.Collections.Generic.List<clsInfoTrupi>
    {
        #region Metoda Publike

        public new clsInfoTrupi this[int index]
        {
            get { return ((clsInfoTrupi)base[index]); }
        }


        /// <summary>
        /// Kthen trupin e info artikulli ne varesi te id se kokes
        /// </summary>
        /// <param name="idKoka"></param>
        /// <returns></returns>
        public static colInfoTrupi merrInfoTrupiSipasIdKoka(int idKoka)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            colInfoTrupi trupi = new colInfoTrupi();
            trupi.mbushColInfoTrupi(dbAdmin.merrInfoTrupiSipasIdKoka(idKoka));
            dbAdmin.Dispose();
            return trupi;
        }
        /// <summary>
        /// merr trupin e infos se artikullit sipas idkokes dhe ne do merren kolonat e dukshme apo te padukshmet
        /// </summary>
        /// <param name="idkoka">idkoka</param>
        /// <param name="visible">visible</param>
        /// <param name="indermarje">id e ndermarjes</param>
        /// <returns>kthen koleksionin e trupit me kolonat e dukshme apo te padukshme</returns>
        public static colInfoTrupi merrInfoSipasIdKokaDheVisible(int idkoka, bool visible, int indermarje)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            colInfoTrupi trupi = new colInfoTrupi();
            trupi.mbushColInfoTrupi(dbAdmin.merrInfoTrupiSipasIdKokaDheVisible(idkoka,visible, indermarje));
            dbAdmin.Dispose();
            return trupi;
        }
        
        /// <summary>
        /// merr trupin e infos se artikullit sipas idkokes dhe ne do merren kolonat e dukshme apo te padukshmet, ne ndryshim nga merrInfoSipasIdKokaDheVisible(int idkoka, bool visible, int indermarje) ky funksion merr edhe sasine dhe koston per detajimin e dyte.
        /// </summary>
        /// <param name="idkoka">idkoka</param>
        /// <param name="visible">visible</param>
        /// <param name="indermarje">id e ndermarjes</param>
        /// <returns>kthen koleksionin e trupit me kolonat e dukshme apo te padukshme</returns>
        public static colInfoTrupi merrInfoSipasIdKokaDheVisibleNew(int idkoka, bool visible, int indermarje)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            colInfoTrupi trupi = new colInfoTrupi();
            trupi.mbushColInfoTrupi(dbAdmin.merrInfoTrupiSipasIdKokaDheVisibleNew(idkoka,visible, indermarje));
            dbAdmin.Dispose();
            return trupi;
        }
     
        #endregion

        #region Metoda Private

        private bool mbushColInfoTrupi(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsInfoTrupi info = new clsInfoTrupi();
                    //info.mbushInfoTrupi(rreshti);
                    Add(new clsInfoTrupi(rreshti));
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
