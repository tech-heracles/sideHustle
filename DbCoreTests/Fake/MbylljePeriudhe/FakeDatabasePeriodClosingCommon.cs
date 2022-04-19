using AlphaWeb.Core.Interfaces.Data;
using DbCore.MbylljePeriudhe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCoreTests.Fake.MbylljePeriudhe
{
    public class FakeDatabasePeriodClosingCommon : IDatabasePeriodClosingCommon
    {
        public void DeletePeriodSummary(PeriodSummary periodSummary)
        {
        }

        public void Dispose()
        {
        }

        public DataTable GetAllPeriodSummaryByIdNdermarrje(int idNdermarrje)
        {
            return new DataTable();
        }

        public DateTime GetLastClosedDate(int companyId, Modul modul)
        {
            return new DateTime();
        }

        public void GetLastClosedMonth(int idNdermarrje, int idNdermarrjeVit, int idModuli, IDataBaseReader objectToFill)
        {

        }

        public void GetLastPeriodSummary<T>(int idNdermarrjeVit, Action<IDataRecord> functToFill)
        {

        }

        public void GetPeriodSummary<T>(int idNdermarrjeVit, int muaji, Action<IDataRecord> funcToFill)
        {
            var row = FakePeriodSummaryDetailKontabilitetiData._lastPeriodSummaryDetails.AsEnumerable()
                                                                .Where(r => r.Field<int>("IdNdermarrjeVit") == idNdermarrjeVit
                                                                            && r.Field<DateTime>("DtFundMuaji").Month == muaji).FirstOrDefault();
            if (row == null)
                return;

            List<PeriodSummary> periodSummaries = new List<PeriodSummary>();
            periodSummaries.Add(new PeriodSummaryKontabiliteti()
             {
                 IdPeriodSummary = Convert.ToInt32(row["IdPeriodSummary"])
             });

            IDataReader dataReader = periodSummaries.AsDataReader(x => new
            {
                x.IdPeriodSummary,
                x.IdProcess,
                x.IdModuli,
                x.IdNdermarrje,
                x.IdNdermarrjeVit,
                x.Pershkrimi,
                x.Muaji,
                x.DtFundMuaji,
                x.DtFillimi,
                x.DtMbarimi,
                x.Statusi
            } );

            while (dataReader.Read())
            {
                funcToFill(dataReader);
                break;
            }
        }

        public void SavePeriodSummary(PeriodSummary periodSummary)
        {
            periodSummary.IdPeriodSummary = periodSummary.DtFundMuaji.Month;
        }

        public void SavePeriodSummaryToHistorik(PeriodSummary periodSummary, int llojVeprimi)
        {

        }

        public void SaveProcess(IProcess process)
        {

        }

        public void UpdateProcess(IProcess process)
        {

        }
    }
}
