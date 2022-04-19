using AlphaWeb.Core.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbDashboard
{
    public class colDashboard : List<clsDashboard>, IDataBaseReader
    {
        public colDashboard()
        {

        }

        public colDashboard(int idPerdoruesi)
        {
            using (clsDatabaseDashboard db = new clsDatabaseDashboard())
                db.GetDashboardsByUser(idPerdoruesi, this);
        }

        public void Mbush(IDataRecord record) => Add(new clsDashboard(record));
    }
}
