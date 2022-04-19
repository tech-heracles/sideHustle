using AlphaWeb.Core.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbDashboard
{
    public class colDashboardDatasource : List<clsDashboardDatasource>, IDataBaseReader
    {
        public colDashboardDatasource(int idPerdoruesi, int idNdermarrje, int idViti)
        {
            using (clsDatabaseDashboard db = new clsDatabaseDashboard())
                db.GetDashboardDatasources(idPerdoruesi, idNdermarrje, idViti, this);
        }
        public void Mbush(IDataRecord record) => Add(new clsDashboardDatasource(record));
    }
}
