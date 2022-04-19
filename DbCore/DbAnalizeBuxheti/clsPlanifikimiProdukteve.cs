using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsPlanifikimiProdukteve
    {
        #region atributet
        public int PPID { get; set; }

        public int RreshtiId { get; set; }

        public string NjesiaId { get; set; }

        public decimal SasiorPlus1 { get; set; }

        public decimal VlerorPlus1 { get; set; }

        public decimal SasiorPlus2 { get; set; }

        public decimal VlerorPlus2 { get; set; }

        public decimal SasiorPlus3 { get; set; }

        public decimal VlerorPlus3 { get; set; }
        
        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public int IdStatusDok { get; set; }

        public int IdNdermarrje { get; set; }

        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public string Emertimi { get; set; }

        public int IdNdermVit { get; set; }

        #endregion atributet

        public clsPlanifikimiProdukteve() { }

        public clsPlanifikimiProdukteve(int pPId, int rreshtiId, string njesiaId, decimal sasiorPlus1, decimal vlerorPlus1, decimal sasiorPlus2, decimal vlerorPlus2, decimal sasiorPlus3, decimal vlerorPlus3, int idStatusDok, int idNdermarrje, int idModifikuesi, int idNdermVit)
        {
            PPID = pPId;
            RreshtiId = rreshtiId;
            NjesiaId = njesiaId;
            SasiorPlus1 = sasiorPlus1;
            VlerorPlus1 = vlerorPlus1;
            SasiorPlus2 = sasiorPlus2;
            VlerorPlus2 = vlerorPlus2;
            SasiorPlus3 = sasiorPlus3;
            VlerorPlus3 = vlerorPlus3;
            IdStatusDok = idStatusDok;
            IdModifikuesi = idModifikuesi;
            IdNdermarrje = idNdermarrje;
            IdNdermVit = idNdermVit;
        }

        public static clsPlanifikimiProdukteve Krijo(IDataRecord record)
        {
            return new clsPlanifikimiProdukteve
            {
                PPID = !Convert.IsDBNull(record["PPID"]) ? Convert.ToInt32(record["PPID"]) : 0,
                RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0,
                NjesiaId = !Convert.IsDBNull(record["NJESIAID"]) ? Convert.ToString(record["NJESIAID"]) : "",
                SasiorPlus1 = !Convert.IsDBNull(record["SASIOR_PLUS1"]) ? Convert.ToDecimal(record["SASIOR_PLUS1"]) : 0,
                VlerorPlus1 = !Convert.IsDBNull(record["VLEROR_PLUS1"]) ? Convert.ToDecimal(record["VLEROR_PLUS1"]) : 0,
                SasiorPlus2 = !Convert.IsDBNull(record["SASIOR_PLUS2"]) ? Convert.ToDecimal(record["SASIOR_PLUS2"]) : 0,
                VlerorPlus2 = !Convert.IsDBNull(record["VLEROR_PLUS2"]) ? Convert.ToDecimal(record["VLEROR_PLUS2"]) : 0,
                SasiorPlus3 = !Convert.IsDBNull(record["SASIOR_PLUS3"]) ? Convert.ToDecimal(record["SASIOR_PLUS3"]) : 0,
                VlerorPlus3 = !Convert.IsDBNull(record["VLEROR_PLUS3"]) ? Convert.ToDecimal(record["VLEROR_PLUS3"]) : 0,
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0,
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
                Emertimi = !Convert.IsDBNull(record["EMERTIMI"]) ? Convert.ToString(record["EMERTIMI"]) : "",
                IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0,
            };
        }

        public clsMesazh Modifiko()
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit");

            try
            {
                dbAB.beginTransaksion();
                mesazhi = dbAB.fshiUpdateStatusDokPlanifikimiProdukteve(PPID, IdModifikuesi);
                if (mesazhi.Status)
                {
                    int pPID = -1;
                    mesazhi = dbAB.RuajPlanifikimiProdukteve(out pPID, RreshtiId, SasiorPlus1, VlerorPlus1,
                        SasiorPlus2, VlerorPlus2, SasiorPlus3,
                        VlerorPlus3, IdNdermarrje, IdModifikuesi, IdNdermVit);

                    if (mesazhi.Status)
                    {
                        PPID = pPID;
                        dbAB.commitTransaksion();
                    }
                    else
                        dbAB.rollbackTransaksion();
                }
                else
                    dbAB.rollbackTransaksion();


            }
            catch (Exception err)
            {
                dbAB.rollbackTransaksion();
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te produkteve \n" + err.Message);
            }
            return mesazhi;
        }


        /// <summary>
        /// fshin nga tabela rreshtin e caktuar
        /// </summary>
        /// <param name="rreshtiID"></param>
        /// <param name="dbAB"></param>
        /// <returns></returns>
        internal static clsMesazh Fshi(int rreshtiID, clsDatabaseAnalizeBuxheti dbAB)
        {
            return dbAB.fshiRreshtNgaPlanifikimiProdukteve(rreshtiID);
        }
    }
}
