using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsPBuxheti3Vjecar
    {
        #region atributet
        public int PB3VId { get; set; }

        public int RreshtiId { get; set; }

        public decimal BuxhetiParaardhes { get; set; }

        public decimal TeArdhuratParaardhes { get; set; }

        public decimal BuxhetiAktual { get; set; }

        public decimal TeArdhuratAktual { get; set; }

        public decimal BuxhetiPlus1 { get; set; }

        public decimal TeArdhuratPlus1 { get; set; }

        public decimal BuxhetiPlus2 { get; set; }

        public decimal TeArdhuratPlus2 { get; set; }

        public decimal BuxhetiPlus3 { get; set; }

        public decimal TeArdhuratPlus3 { get; set; }

        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public int IdStatusDok { get; set; }

        public int IdNdermarrje { get; set; }

        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public string Emertimi { get; set; }

        public int IdNdermVit { get; set; }
        #endregion atributet

        public clsPBuxheti3Vjecar() { }

        public clsPBuxheti3Vjecar(int pB3VId, int rreshtiId, decimal buxhetiParaardhes, decimal teArdhuratParaardhes, decimal buxhetiAktual, decimal teArdhuratAktual, decimal buxhetiPlus1, decimal teArdhuratPlus1, decimal buxhetiPlus2, decimal teArdhuratPlus2, decimal buxhetiPlus3, decimal teArdhuratPlus3, int idStatusDok, int idNdermarrje, int idModifikuesi, int idNdermVit)
        {
            PB3VId = pB3VId;
            RreshtiId = rreshtiId;
            BuxhetiParaardhes = buxhetiParaardhes;
            TeArdhuratParaardhes = teArdhuratParaardhes;
            BuxhetiAktual = buxhetiAktual;
            TeArdhuratAktual = teArdhuratAktual;
            BuxhetiPlus1 = buxhetiPlus1;
            TeArdhuratPlus1 = teArdhuratPlus1;
            BuxhetiPlus2 = buxhetiPlus2;
            TeArdhuratPlus2 = teArdhuratPlus2;
            BuxhetiPlus3 = buxhetiPlus3;
            TeArdhuratPlus3 = teArdhuratPlus3;
            IdStatusDok = idStatusDok;
            IdNdermarrje = idNdermarrje;
            IdModifikuesi = IdModifikuesi;
            IdNdermVit = idNdermVit;
        }

        public static clsPBuxheti3Vjecar Krijo(IDataRecord record)
        {
            return new clsPBuxheti3Vjecar
            {
                PB3VId = !Convert.IsDBNull(record["PB3VID"]) ? Convert.ToInt32(record["PB3VID"]) : 0,
                RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0,
                BuxhetiParaardhes = !Convert.IsDBNull(record["BUXHETI_PARAARDHES"]) ? Convert.ToDecimal(record["BUXHETI_PARAARDHES"]) : 0,
                TeArdhuratParaardhes = !Convert.IsDBNull(record["TEARDHURAT_PARAARDHES"]) ? Convert.ToDecimal(record["TEARDHURAT_PARAARDHES"]) : 0,
                BuxhetiAktual = !Convert.IsDBNull(record["BUXHETI_AKTUAL"]) ? Convert.ToDecimal(record["BUXHETI_AKTUAL"]) : 0,
                TeArdhuratAktual = !Convert.IsDBNull(record["TEARDHURAT_AKTUAL"]) ? Convert.ToDecimal(record["TEARDHURAT_AKTUAL"]) : 0,
                BuxhetiPlus1 = !Convert.IsDBNull(record["BUXHETI_PLUS1"]) ? Convert.ToDecimal(record["BUXHETI_PLUS1"]) : 0,
                TeArdhuratPlus1 = !Convert.IsDBNull(record["TEARDHURAT_PLUS1"]) ? Convert.ToDecimal(record["TEARDHURAT_PLUS1"]) : 0,
                BuxhetiPlus2 = !Convert.IsDBNull(record["BUXHETI_PLUS2"]) ? Convert.ToDecimal(record["BUXHETI_PLUS2"]) : 0,
                TeArdhuratPlus2 = !Convert.IsDBNull(record["TEARDHURAT_PLUS2"]) ? Convert.ToDecimal(record["TEARDHURAT_PLUS2"]) : 0,
                BuxhetiPlus3 = !Convert.IsDBNull(record["BUXHETI_PLUS3"]) ? Convert.ToDecimal(record["BUXHETI_PLUS3"]) : 0,
                TeArdhuratPlus3 = !Convert.IsDBNull(record["TEARDHURAT_PLUS3"]) ? Convert.ToDecimal(record["TEARDHURAT_PLUS3"]) : 0,
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
                mesazhi = dbAB.fshiUpdateStatusDokPBuxheti3Vjecar(PB3VId, IdModifikuesi);
                if (mesazhi.Status)
                {
                    int pB3VId = -1;
                    mesazhi = dbAB.RuajPBuxheti3Vjecar(out pB3VId, RreshtiId, BuxhetiParaardhes, TeArdhuratParaardhes, BuxhetiAktual,
                        TeArdhuratAktual, BuxhetiPlus1, TeArdhuratPlus1,
                        BuxhetiPlus2, TeArdhuratPlus2, BuxhetiPlus3, TeArdhuratPlus3, IdNdermarrje, IdModifikuesi, IdNdermVit);

                    if (mesazhi.Status)
                    {
                        PB3VId = pB3VId;
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
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te projekt buxhetit \n" + err.Message);
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
            return dbAB.fshiRreshtNgaPbuxhet3Vjecar(rreshtiID);
        }
    }
}
