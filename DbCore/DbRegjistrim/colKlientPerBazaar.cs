using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace DbCore.DbRegjistrim
{
    public class colKlientPerBazaar : List<clsKlientPerBazaar>
    {

        public colKlientPerBazaar()
        {

        }
        public static DataTable MerrSipasNdermarrjesPerExport(int idNdermarrje)
        {

            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return db.MerrKlientPerBazaarSipasNdermarrjes(DbAdmin.clsNdermarrje.ktheIdNdermarrjeMeme());

        }


    }
}
