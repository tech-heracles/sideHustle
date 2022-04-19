using AlphaWeb.Core.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbDashboard
{

    public class clsDashboardDatasource : IDataBaseReader
    {
        private IDataRecord record;

        public int Id { get; set; }
        public int IdKomponente { get; set; }
        public string SpName { get; set; }

        public clsDashboardDatasource() { }

        public clsDashboardDatasource(IDataRecord record) => Mbush(record);

        public void Mbush(IDataRecord record)
        {
            Id = !Convert.IsDBNull(record["ID"]) ? Convert.ToInt32(record["ID"]) : 0;
            IdKomponente = !Convert.IsDBNull(record["IDKOMPONENTE"]) ? Convert.ToInt32(record["IDKOMPONENTE"]) : 0;
            SpName = !Convert.IsDBNull(record["SPNAME"]) ? Convert.ToString(record["SPNAME"]) : String.Empty;
        }
    }
}
