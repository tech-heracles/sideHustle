using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
    public class colTrupiErrorImporti : System.Collections.Generic.List<clsTrupiErrorImporti>
    {
        #region Metoda Publike

        public new clsTrupiErrorImporti this[int index]
        {
            get { return ((clsTrupiErrorImporti)base[index]); }
        }

        public static DataTable ktheErrorImportiSipasIdKoka(int idKoka)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.ktheErrorImportiSipasIdKoka(idKoka);
            }
        }


        #endregion

        #region Metoda Private

        public bool mbushErrorImporti(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsTrupiErrorImporti(rreshti));
            }
            return true;
        }
        public bool mbushErrorImportiNgaProgrami(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsTrupiErrorImporti info = new clsTrupiErrorImporti();
                    info.mbushErrorImportiNgaProg(rreshti);
                    Add(info);
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }
        public bool mbushErrorImportiNgaAmbienti(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                clsTrupiErrorImporti info = new clsTrupiErrorImporti();
                info.mbushErrorImportiNgaAmbienti(rreshti);
                Add(info);
            }
            return true;
        }

        #endregion
    }
}
