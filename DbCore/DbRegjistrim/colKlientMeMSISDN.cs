using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.DbInventari;

namespace DbCore.DbRegjistrim
{
    public class colKlientMeMSISDN : List<clsKlientMeMSISDN>, IDataBaseReader
    {

        public colKlientMeMSISDN()
        {

        }

        public void Mbush(IDataRecord record)
        {
            this.Add(new clsKlientMeMSISDN(record));
        }

        public static DataTable merrPromocioneTePerdoruraSipasNdermarrjes(int idNdermarrje)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return db.merrPromocioneMSISDNSipasNdermarrjes(idNdermarrje);

        }


    }
}
