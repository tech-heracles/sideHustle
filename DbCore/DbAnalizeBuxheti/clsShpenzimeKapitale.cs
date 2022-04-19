using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsShpenzimeKapitale
    {
        #region atributet
        public int ShKId { get; set; }

        public int RreshtiId { get; set; }

        public decimal TotalParardhes { get; set; }

        public decimal PritshmiAktual { get; set; }

        public decimal ShpenzKapitalePatrupezuarArdhme { get; set; }

        public decimal ShpenzKapitaleTrupezuarArdhme { get; set; }

        public decimal TransferimKapitalArdhme { get; set; }

        public decimal TotaliArdhme { get; set; }

        public decimal ParashikimTotaliPlus2 { get; set; }

        public decimal ParashikimTotaliPlus3 { get; set; }

        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public int IdStatusDok { get; set; }

        public int IdNdermarrje { get; set; }

        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public string Emertimi { get; set; }

        public int IdNdermVit { get; set; }
        #endregion atributet

        public clsShpenzimeKapitale() { }

        public clsShpenzimeKapitale(int shKId, int rreshtiId, decimal totalParardhes, decimal pritshmiAktual, decimal shpenzKapitalePatrupezuarArdhme, decimal shpenzKapitaleTrupezuarArdhme, decimal transferimKapitalArdhme, decimal totaliArdhme, decimal parashikimTotaliPlus2, decimal parashikimTotaliPlus3, int idStatusDok, int idNdermarrje, int idModifikuesi, int idNdermVit) {
            ShKId = shKId;
            RreshtiId = rreshtiId;
            TotalParardhes = totalParardhes;
            PritshmiAktual = pritshmiAktual;
            ShpenzKapitalePatrupezuarArdhme = shpenzKapitalePatrupezuarArdhme;
            ShpenzKapitaleTrupezuarArdhme = shpenzKapitaleTrupezuarArdhme;
            TransferimKapitalArdhme = transferimKapitalArdhme;
            TotaliArdhme = totaliArdhme;
            ParashikimTotaliPlus2 = parashikimTotaliPlus2;
            ParashikimTotaliPlus3 = parashikimTotaliPlus3;
            IdStatusDok = idStatusDok;
            IdNdermarrje = idNdermarrje;
            IdModifikuesi = IdModifikuesi;
            IdNdermVit = idNdermVit;
        }

        public static clsShpenzimeKapitale Krijo(IDataRecord record)
        {
            return new clsShpenzimeKapitale
            {
                ShKId = !Convert.IsDBNull(record["SHKID"]) ? Convert.ToInt32(record["SHKID"]) : 0,
                RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0,
                TotalParardhes = !Convert.IsDBNull(record["TOTAL_PARARDHES"]) ? Convert.ToDecimal(record["TOTAL_PARARDHES"]) : 0,
                PritshmiAktual = !Convert.IsDBNull(record["PRITSHMI_AKTUAL"]) ? Convert.ToDecimal(record["PRITSHMI_AKTUAL"]) : 0,
                ShpenzKapitalePatrupezuarArdhme = !Convert.IsDBNull(record["SHPENZ_KAPITALE_PATRUPEZUAR_ARDHME"]) ? Convert.ToDecimal(record["SHPENZ_KAPITALE_PATRUPEZUAR_ARDHME"]) : 0,
                ShpenzKapitaleTrupezuarArdhme = !Convert.IsDBNull(record["SHPENZ_KAPITALE_TRUPEZUAR_ARDHME"]) ? Convert.ToDecimal(record["SHPENZ_KAPITALE_TRUPEZUAR_ARDHME"]) : 0,
                TransferimKapitalArdhme = !Convert.IsDBNull(record["TRANSFERIM_KAPITAL_ARDHME"]) ? Convert.ToDecimal(record["TRANSFERIM_KAPITAL_ARDHME"]) : 0,
                TotaliArdhme = !Convert.IsDBNull(record["TOTALI_ARDHME"]) ? Convert.ToDecimal(record["TOTALI_ARDHME"]) : 0,
                ParashikimTotaliPlus2 = !Convert.IsDBNull(record["PARASHIKIM_TOTALI_PLUS2"]) ? Convert.ToDecimal(record["PARASHIKIM_TOTALI_PLUS2"]) : 0,
                ParashikimTotaliPlus3 = !Convert.IsDBNull(record["PARASHIKIM_TOTALI_PLUS3"]) ? Convert.ToDecimal(record["PARASHIKIM_TOTALI_PLUS3"]) : 0,
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
                mesazhi = dbAB.fshiUpdateStatusDokShpenzimeKapitale(ShKId, IdModifikuesi);
                if (mesazhi.Status)
                {
                    int shKId = -1;
                    mesazhi = dbAB.RuajShpenzimeKapitale(out shKId, RreshtiId, TotalParardhes, PritshmiAktual, ShpenzKapitalePatrupezuarArdhme,
                        ShpenzKapitaleTrupezuarArdhme, TransferimKapitalArdhme, TotaliArdhme,
                        ParashikimTotaliPlus2, ParashikimTotaliPlus3, IdNdermarrje, IdModifikuesi, IdNdermVit);

                    if (mesazhi.Status)
                    {
                        ShKId = shKId;
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
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit \n" + err.Message);
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
            return dbAB.fshiRreshtNgaShpenzimeKapitale(rreshtiID);
        }
    }
}
