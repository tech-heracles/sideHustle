using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbListPagesat
{
   public class PunonjesMeData
    {
        private int _idPunonjesi;
        private DateTime _data;
        public PunonjesMeData() { }
        public PunonjesMeData(IDataRecord record)
        {
            
            int.TryParse(record["idPunonjesi"].ToString(),out _idPunonjesi);
            DateTime.TryParse(record["Data"].ToString(), out _data);
        }


        public DateTime Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
            }
        }

        public int IdPunonjesi
        {
            get
            {
                return _idPunonjesi;
            }

            set
            {
                _idPunonjesi = value;
            }
        }

        public static PunonjesMeData Krijo(IDataRecord record)
        {
            return new PunonjesMeData(record);
        }

    }
}
