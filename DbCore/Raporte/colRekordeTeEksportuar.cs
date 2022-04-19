using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.Extensions;

namespace DbCore.Raporte
{
    class colRekordeTeEksportuar
    {
        

        public static DataTable merrKolonaPerTuEksportuar(DataTable dt)
        {
            using (DbCore.DbShare.clsDatabaseShare dbShare = new DbShare.clsDatabaseShare())
            {               
                return dbShare.merrRekordePerTuEksportuar(dt.ToDataTable("IDSHITJEKOKA", "IDSHITJETRUPI", "SASIAEPAKONVERTUAR", "DTMODIFIKIMI", "TIPI"));
            }
        }

        public static void updateRekordeTeEksportuar(DataTable dt, DbShare.clsDatabaseShare dbShare)
        {
            dbShare.updateRekordeTeEksportuar(dt.ToDataTable("IDSHITJEKOKA", "IDSHITJETRUPI", "SASIAEPAKONVERTUAR", "DTMODIFIKIMI", "TIPI"));
        }

        public static DataTable merrRekordeTeDetyrueshme(DbShare.clsDatabaseShare dbShare)
        {
            return dbShare.merrRekordeTeDetyrueshme();
        }
    }
}
