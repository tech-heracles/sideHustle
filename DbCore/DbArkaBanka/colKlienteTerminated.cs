using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbArkaBanka
{
    public class colKlienteTerminated : List<string>, IDataBaseReader
    {
        public colKlienteTerminated()
        {
            using (var db = new clsDatabaseArkaBanka())
            {
                db.merrKlienteTerminated(this);
            }
        }
        public void Mbush(IDataRecord record)
        {
            this.Add(record["CUSTOMER_ID"].ToString());
        }
    }
}
