using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbInventari
{
    public class clsLlojElementiPerIntegrim
    {
        public int Id { get; set; }
        public string Emertimi { get; set; }
        public string Emertimi_eng { get; set; }

        public static clsLlojElementiPerIntegrim Krijo(IDataRecord record)
        {
            return new clsLlojElementiPerIntegrim
            {
                Id = !Convert.IsDBNull(record["ID"]) ? Convert.ToInt32(record["ID"]) : 0,
                Emertimi_eng = !Convert.IsDBNull(record["EMERTIMI_eng"]) ? Convert.ToString(record["EMERTIMI_eng"]) : String.Empty,
                Emertimi = !Convert.IsDBNull(record["EMERTIMI"]) ? Convert.ToString(record["EMERTIMI"]) : String.Empty,
            };
        }

    }
}
