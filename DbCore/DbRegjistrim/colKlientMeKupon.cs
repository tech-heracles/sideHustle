using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DbCore.IMBUtils.Security;

namespace DbCore.DbRegjistrim
{
    public class colKlientMeKupon : List<clsKlientMeKupon>
    {




        public static DataTable MerrSipasNdermarrjesPerExport(int idNdermarrje)
        {
            //int idmeme = idNdermarrje;
            //if (!DbAdmin.clsNdermarrje.eshtePrind(idNdermarrje))
            //    idmeme = DbAdmin.clsNdermarrje.ktheIdNdermarrjeMeme(idNdermarrje);

            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                DataTable dt = db.MerrKlientMeKuponSipasNdermarrjesRoot(DbAdmin.clsNdermarrje.ktheIdNdermarrjeMeme());
                foreach (DataRow dr in dt.Rows)
                {
                    dr["KodKuponi"] = RijndaelSimple.DecryptDDString(dr["KodKuponi"].ToString());
                }
                return dt;
            }

        }
    }
}
