namespace DbCore.MbylljePeriudhe
{
    public interface IPeriodSummaryDetailKontabiliteti: IPeriodSummaryDetail
    {
        int IdMonedha { get; set; }
        int IdLlogari { get; set; }
        decimal VleraDebiAkumuluar { get; set; }
        decimal VleraKrediAkumuluar { get; set; }
        decimal VleraDebiMonBazeAkumuluar { get; set; }
        decimal VleraKrediMonBazeAkumuluar { get; set; }
    }
}
