using NLog;
using DbCore.IMBUtils.DataBase;
using System;
using System.Data;

namespace DbCore.MbylljePeriudhe
{
    public abstract class PeriodSummaryFactory
    {
        public ILogger Logger { get; set; }
        public IDbBuilder DbBuilder { get; set; }

        public abstract PeriodSummary CreatePeriodSummary(int idProcess, int idNdermarrje, int idNdermarrjeVit, int idModuli, string pershkrimi, int muaji, DateTime dtFillimi, DateTime dtFundMuaji, Statusi statusi);
        public abstract void GeneratePeriodSummaryDetails(PeriodSummary periodSummary);
        public abstract void SavePeriodSummary(PeriodSummary periodSummary);
        public abstract void DeletePeriodSummary(PeriodSummary periodSummary);
        public abstract PeriodSummary GetPeriodSummary(int idNdermarrjeVit, int muaji);
        public abstract PeriodSummary GetLastPeriodSummary(int idNdermarrjeVit);
        public abstract DataTable GetPeriodSummaryDetailsReport(int idNdermarrje, int viti, int muaji);
        public abstract void DeleteHistoric(int idNdermarrje, int idNdermarrjeVit, string muajt);
    }
}
