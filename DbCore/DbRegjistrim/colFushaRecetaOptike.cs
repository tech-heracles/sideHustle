using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbRegjistrim
{
    public class colFushaRecetaOptike : List<clsFushaRecetaOptike>, IDataBaseReader
    {
        public colFushaRecetaOptike()
        {
            using (var db = new clsDatabaseRegjistrim())
                db.merrFushaRecetaOptike(this);
        }
        public void Mbush(IDataRecord record)
        {
            Add(new clsFushaRecetaOptike(record));
        }
    }
}
