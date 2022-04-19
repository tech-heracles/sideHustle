using AlphaWeb.Core.Interfaces.Data;
using System;
using System.Data;

namespace DbCore.MbylljePeriudhe
{
    public interface IDatabasePeriodClosingCommon: IDisposable
    {
        void GetLastClosedMonth(int idNdermarrje, int idNdermarrjeVit, int idModuli, IDataBaseReader objectToFill);
        void SaveProcess(IProcess process);
        void UpdateProcess(IProcess process);
        void SavePeriodSummary(PeriodSummary periodSummary);
        void DeletePeriodSummary(PeriodSummary periodSummary);
        void SavePeriodSummaryToHistorik(PeriodSummary periodSummary, int llojVeprimi);
        void GetPeriodSummary<T>(int idNdermarrjeVit, int muaji, Action<IDataRecord> funcToFill);
        void GetLastPeriodSummary<T>(int idNdermarrjeVit, Action<IDataRecord> functToFill);
        DataTable GetAllPeriodSummaryByIdNdermarrje(int idNdermarrje);
        DateTime GetLastClosedDate(int companyId, Modul modul);
    }
}
