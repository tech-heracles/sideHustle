using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbRegjistrim
{
    public class clsFushaRecetaOptike : IDataBaseReader
    {
        private int idFusha;
        private string pershkrimi;

        public clsFushaRecetaOptike(IDataRecord record)
        {
            Mbush(record);
        }

        public int IdFusha { get { return idFusha; } }
        public string Pershkrimi { get { return pershkrimi; } }
        public void Mbush(IDataRecord record)
        {
            int.TryParse(record["ID"].ToString(), out idFusha);
            pershkrimi = record["PERSHKRIMI"].ToString();
        }
    }
}
