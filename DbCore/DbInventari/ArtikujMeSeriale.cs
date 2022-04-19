using System.Collections.Generic;
using System.Data;

namespace DbCore.DbInventari
{
    public class ArtikujMeSeriale
    {
        public string Artikulli { get; set; }
        public string ArtikulliSet { get; set; }
        public string Magazina { get; set; }
        public string SerialiUnikKryesor { get; set; }
        public string SerialiUnikDytesor { get; set; }

        public string GetValue(DataRow row, Dictionary<string, string> fushaSerialesh, string fusha)
        {
            return fushaSerialesh.ContainsKey(fusha) ? row[fushaSerialesh[fusha]].ToString() : "";
        }
    }
}
