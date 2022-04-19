using System;
using System.Data;

namespace DbCore.MbylljePeriudhe
{
    public class PeriodSummaryDetailKontabiliteti : PeriodSummaryDetail, IPeriodSummaryDetailKontabiliteti
    {
        public int IdMonedha { get; set; }
        public int IdLlogari { get; set; }
        public decimal VleraDebiAkumuluar { get; set; }
        public decimal VleraKrediAkumuluar { get; set; }
        public decimal VleraDebiMonBazeAkumuluar { get; set; }
        public decimal VleraKrediMonBazeAkumuluar { get; set; }

        public PeriodSummaryDetailKontabiliteti(): base() { }
        public PeriodSummaryDetailKontabiliteti(IDataRecord record): base(record) { }
        

        public override void FillObject(IDataRecord record)
        {
            IdMonedha = !Convert.IsDBNull(record["IdMonedha"]) ? Convert.ToInt32(record["IdMonedha"]) : 0;
            IdLlogari = !Convert.IsDBNull(record["IdLlogari"]) ? Convert.ToInt32(record["IdLlogari"]) : 0;
            VleraDebiAkumuluar = !Convert.IsDBNull(record["VleraDebiAkumuluar"]) ? Convert.ToDecimal(record["VleraDebiAkumuluar"]) : 0;
            VleraKrediAkumuluar = !Convert.IsDBNull(record["VleraKrediAkumuluar"]) ? Convert.ToDecimal(record["VleraKrediAkumuluar"]) : 0;
            VleraDebiMonBazeAkumuluar = !Convert.IsDBNull(record["VleraDebiMonBazeAkumuluar"]) ? Convert.ToDecimal(record["VleraDebiMonBazeAkumuluar"]) : 0;
            VleraKrediMonBazeAkumuluar = !Convert.IsDBNull(record["VleraKrediMonBazeAkumuluar"]) ? Convert.ToDecimal(record["VleraKrediMonBazeAkumuluar"]) : 0;
        }

    }
}
