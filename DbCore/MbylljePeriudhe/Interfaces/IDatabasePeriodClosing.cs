using System;
using System.Collections.Generic;
using System.Data;

namespace DbCore.MbylljePeriudhe
{
    public interface IDatabasePeriodClosing: IDisposable
    {
        void SavePeriodSummaryDetail(DataTable details);
        void DeletePeriodSummaryDetail(int idPeriodSummary);
        IEnumerable<T> GetLastPeriodSummaryDetails<T>(int idNdermarrje, Func<IDataRecord, T> funcToFill);
        IEnumerable<T> GetAnaliticPeriodSummaryDetails<T>(int idNdermarrje, DateTime dtFundMuaji, Func<IDataRecord, T> funcToFill);
        DataTable GetPeriodSummaryDetailsReport(int idNdermarrje, int viti, int muaji);
        void DeleteHistoric(int idNdermarrje, int idNdermarrjeVit, string muajt);
    }
}
