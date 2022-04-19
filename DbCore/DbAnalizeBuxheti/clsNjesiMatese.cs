using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsNjesiMatese
    {
        #region atributet
        public int IdNjesia { get; set; }
        public string Kodi { get; set; }
        public string Pershkrimi { get; set; }
        #endregion atributet

        public clsNjesiMatese() { }

        public clsNjesiMatese(int idNjesia, string kodi, string pershkrimi)
        {
            IdNjesia = idNjesia;
            Kodi = kodi;
            Pershkrimi = pershkrimi;
        }

        public static clsNjesiMatese Krijo(IDataRecord record)
        {
            return new clsNjesiMatese
            {
                IdNjesia = !Convert.IsDBNull(record["IDNJESIA"]) ? Convert.ToInt32(record["IDNJESIA"]) : 0,
                Kodi = !Convert.IsDBNull(record["KODI"]) ? Convert.ToString(record["KODI"]) : String.Empty,
                Pershkrimi = !Convert.IsDBNull(record["PERSHKRIMI"]) ? Convert.ToString(record["PERSHKRIMI"]) : String.Empty
                
                
            };
        }
    }
}
