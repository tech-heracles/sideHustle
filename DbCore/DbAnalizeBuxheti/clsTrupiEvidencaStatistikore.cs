using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsTrupiEvidencaStatistikore
    {
        public int IdTrupiDok { get; set; }
        public int IdKokaDok { get; set; }
        public int RreshtiId { get; set; }
        public int NumriGjithsej { get; set; }
        public int NumriPerfunduar { get; set; }
        public int IdStatusDok { get; set; }
        public int IdKrijuesi { get; set; }
        public int IdModifikuesi { get; set; }
        public DateTime? DtKrijimi { get; set; }
        public DateTime? DtModifikimi { get; set; }


        public static clsTrupiEvidencaStatistikore Krijo(IDataRecord record)
        {

            return new clsTrupiEvidencaStatistikore
            {
                IdTrupiDok = !Convert.IsDBNull(record["IDTRUPIDOK"]) ? Convert.ToInt32(record["IDTRUPIDOK"]) : 0,
                IdKokaDok = !Convert.IsDBNull(record["IDKOKADOK"]) ? Convert.ToInt32(record["IDKOKADOK"]) : 0,
                RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0,
                NumriGjithsej = !Convert.IsDBNull(record["NUMRI_GJITHSEJ"]) ? Convert.ToInt32(record["NUMRI_GJITHSEJ"]) : 0,
                NumriPerfunduar = !Convert.IsDBNull(record["NUMRI_PERFUNDUAR"]) ? Convert.ToInt32(record["NUMRI_PERFUNDUAR"]) : 0,
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null
            };


        }

        internal clsMesazh Ruaj(clsDatabaseAnalizeBuxheti dbAB)
        {
            int idTrupi = -1;
            clsMesazh mesazh = dbAB.RuajTrupEvidencaStatistikore(out idTrupi, IdKokaDok, RreshtiId, NumriGjithsej, NumriPerfunduar, IdStatusDok, IdKrijuesi);
            if (mesazh.Status)
                IdTrupiDok = idTrupi;
            return mesazh;
        }
    }
}
