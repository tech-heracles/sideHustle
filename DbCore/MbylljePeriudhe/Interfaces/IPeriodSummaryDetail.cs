using System;

namespace DbCore.MbylljePeriudhe
{
    public interface IPeriodSummaryDetail
    {
        int IdPeriodSummaryDetail { get; set; }
        int IdPeriodSummary { get; set; }
        int IdModuli { get; set; }
        int IdNdermarrje { get; set; }
        int IdNdermarrjeVit { get; set; }
        DateTime DtFundMuaji { get; set; }
    }
}
