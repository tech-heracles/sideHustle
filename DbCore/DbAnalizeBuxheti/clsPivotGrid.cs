using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsPivotGrid
    {
        public int IdKolona { get; set; }
        public string EmriKolones { get; set; }
        public string Pershkrimi { get; set; }
        public string Tipi { get; set; }
        public string Zona { get; set; }    
        public int IdKomponente { get; set; }
        public int Indexi { get; set; }

        public static IEnumerable<clsPivotGrid> MerrKolonaPerPivotGrid(int idKomponente)
        {
            using (var databaseAnalizeBuxheti = new clsDatabaseAnalizeBuxheti())
                return databaseAnalizeBuxheti.MerrKolonaPerPivotGrid(idKomponente);
        }

        internal static clsPivotGrid Krijo(System.Data.IDataRecord record)
        {
            return new clsPivotGrid
            {
                IdKolona = !Convert.IsDBNull(record["IDKOLONA"]) ? Convert.ToInt32(record["IDKOLONA"]) : 0,
                EmriKolones = !Convert.IsDBNull(record["EMRIKOLONES"]) ? Convert.ToString(record["EMRIKOLONES"]) : string.Empty
,
                Pershkrimi = !Convert.IsDBNull(record["PERSHKRIMI"]) ? Convert.ToString(record["PERSHKRIMI"]) : string.Empty,
                Zona = !Convert.IsDBNull(record["ZONA"]) ? Convert.ToString(record["ZONA"]) : string.Empty,
                IdKomponente = !Convert.IsDBNull(record["IDKOMPONENTE"]) ? Convert.ToInt32(record["IDKOMPONENTE"]) : 0,
                Indexi = !Convert.IsDBNull(record["INDEXI"]) ? Convert.ToInt16(record["INDEXI"]) : 0,
                Tipi=!Convert.IsDBNull(record["TIPI"])?Convert.ToString(record["TIPI"]):string.Empty
            };
        }        
    }
}
