using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{
    public class clsServerConnectionString : IDataBaseReader
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public clsServerConnectionString()
        {

        }
        internal clsServerConnectionString(IDataRecord record)
        {
            Mbush(record);
        }
        public void Mbush(IDataRecord record)
        {
            Id = int.Parse(record["Id"]?.ToString());
            Name = record["name"]?.ToString();
            ConnectionString = record["CONNECTIONSTRING"].ToString();
        }
    }
}
