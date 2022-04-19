using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsTrupiPasqyraOrganike
    {



        public int IdTrupiDok { get; set; }
        public int IdKokaDok { get; set; }
        public int IdProfesioni { get; set; }
        public decimal VleraFakt { get; set; }
        public decimal VleraPlan { get; set; }
        public int IdStatusDok { get; set; }
        public int IdKrijuesi { get; set; }
        public int IdModifikuesi { get; set; }
        public DateTime? DtKrijimi { get; set; }
        public DateTime? DtModifikimi { get; set; }
        public int TotaliFemra { get; set; }
        public int TotaliMeshkuj { get; set; }


        public static clsTrupiPasqyraOrganike Krijo(IDataRecord record)
        {

            return new clsTrupiPasqyraOrganike
            {
                IdTrupiDok = !Convert.IsDBNull(record["IDTRUPIDOK"]) ? Convert.ToInt32(record["IDTRUPIDOK"]) : 0,
                IdKokaDok = !Convert.IsDBNull(record["IDKOKADOK"]) ? Convert.ToInt32(record["IDKOKADOK"]) : 0,
                IdProfesioni = !Convert.IsDBNull(record["IDPROFESIONI"]) ? Convert.ToInt32(record["IDPROFESIONI"]) : 0,
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
                VleraFakt = !Convert.IsDBNull(record["VLERAFAKT"]) ? Convert.ToDecimal(record["VLERAFAKT"]) : 0,
                VleraPlan = !Convert.IsDBNull(record["VLERAPLAN"]) ? Convert.ToDecimal(record["VLERAPLAN"]) : 0,
                TotaliFemra = !Convert.IsDBNull(record["TOTALIFEMRA"]) ? Convert.ToInt32(record["TOTALIFEMRA"]) : 0,
                TotaliMeshkuj = !Convert.IsDBNull(record["TOTALIMESHKUJ"]) ? Convert.ToInt32(record["TOTALIMESHKUJ"]) : 0,
            };


        }





        internal clsMesazh Ruaj(clsDatabaseAnalizeBuxheti dbAB)
        {
            int idTrupi = -1;
            clsMesazh mesazh = dbAB.RuajTrupPasqyraOrganike(out idTrupi, IdKokaDok, IdProfesioni, VleraFakt, VleraPlan, IdStatusDok, IdKrijuesi, TotaliFemra, TotaliMeshkuj);
            if (mesazh.Status)
                IdTrupiDok = idTrupi;
            return mesazh;
        }
    }
}
