using NLog;
using System;
using System.Data;

namespace DbCore.MbylljePeriudhe
{
    public abstract class PeriodSummaryDetail
    {
        public int IdPeriodSummaryDetail { get; set; }
        public int IdPeriodSummary { get; set; }
        public int IdModuli { get; set; }
        public int IdNdermarrje { get; set; }
        public int IdNdermarrjeVit { get; set; }
        public DateTime DtFundMuaji { get; set; }

        public PeriodSummaryDetail() { }

        public PeriodSummaryDetail(IDataRecord record) {
            Fill(record);
        }

        public void Fill(IDataRecord record) {
            IdPeriodSummaryDetail = !Convert.IsDBNull(record["IdPeriodSummaryDetail"]) ? Convert.ToInt32(record["IdPeriodSummaryDetail"]) : 0;
            IdPeriodSummary = !Convert.IsDBNull(record["IdPeriodSummary"]) ? Convert.ToInt32(record["IdPeriodSummary"]) : 0;
            IdModuli = !Convert.IsDBNull(record["IdModuli"]) ? Convert.ToInt32(record["IdModuli"]) : 0;
            IdNdermarrje = !Convert.IsDBNull(record["IdNdermarrje"]) ? Convert.ToInt32(record["IdNdermarrje"]) : 0;
            IdNdermarrjeVit = !Convert.IsDBNull(record["IdNdermarrjeVit"]) ? Convert.ToInt32(record["IdNdermarrjeVit"]) : 0;
            DtFundMuaji = !Convert.IsDBNull(record["DtFundMuaji"]) ? Convert.ToDateTime(record["DtFundMuaji"]) : new DateTime();
            FillObject(record);
        }

        public abstract void FillObject(IDataRecord record);
    }
}
