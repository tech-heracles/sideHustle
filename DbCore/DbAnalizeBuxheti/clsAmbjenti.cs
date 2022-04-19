using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
  public  class clsAmbjenti
    {
        #region atributet
        public int IdAmbjenti { get; set; }

        public string Kodi { get; set; }

        public string Pershkrim { get; set; }

        //public int IdKrijuesi { get; set; }

        //public int IdModifikuesi { get; set; }

        //public int IdStatusDok{get; set;}

        //public DateTime? DtKrijimi { get; set; }

        //public DateTime? DtModifikimi { get; set; }
        #endregion atributet
        public clsAmbjenti() { }

        public clsAmbjenti(int idAmbjenti, string kodi, string pershkrim)
        {
            IdAmbjenti = idAmbjenti;
            Kodi = kodi;
            Pershkrim = pershkrim;
            //IdStatusDok = idStatusDok;
            //IdModifikuesi = idModifikuesi;
        }

        /// <summary>
        /// krijon nje objekt  clsAmbjenti,
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public static clsAmbjenti Krijo(IDataRecord record)
        {
            return new clsAmbjenti
            {
                IdAmbjenti = !Convert.IsDBNull(record["IDAMBJENTI"]) ? Convert.ToInt32(record["IDAMBJENTI"]) : 0,
                Kodi = !Convert.IsDBNull(record["KODI"]) ? Convert.ToString(record["KODI"]) : "",
                Pershkrim = !Convert.IsDBNull(record["PERSHKRIMI"]) ? Convert.ToString(record["PERSHKRIMI"]) : ""
                //IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                //IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                //IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                //DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
                //DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
                
            };
        }


    }
}
