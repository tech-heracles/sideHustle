using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace DbCore.DbAdmin
{
    public class colInfoKoka : System.Collections.Generic.List<clsInfoKoka>
    {
        #region Metoda Publike 

        public new clsInfoKoka this[int index]
        {
            get { return ((clsInfoKoka)base[index]); }
        }

        public static colInfoKoka ktheInfoNdermarrjes(int idNdermarja)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            colInfoKoka modelet = new colInfoKoka();
            modelet.mbushColInfoKoka(dbAdmin.merrGjitheInfoKokaNdermarrjes(idNdermarja));
            dbAdmin.Dispose();
            return modelet;
        }
        public static colInfoKoka ktheInfoNdermarrjesDheLlojit(int idNdermarja, int lloji)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            colInfoKoka modelet = new colInfoKoka();
            modelet.mbushColInfoKoka(dbAdmin.merrGjitheInfoKokaNdermarrjesSipasLlojit(idNdermarja,lloji));
            dbAdmin.Dispose();
            return modelet;
        }
        #endregion

        #region Metoda Private

        private bool mbushColInfoKoka(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsInfoKoka info = new clsInfoKoka();
                    //info.mbushInfoKoka(rreshti);
                    Add(new clsInfoKoka(rreshti));
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
