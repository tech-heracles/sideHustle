using DbCore.MbylljePeriudhe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCoreTests.Fake.MbylljePeriudhe
{
    class FakeDatabasePeriodClosingKontabiliteti : IDatabasePeriodClosing
    {
        public void DeleteHistoric(int idNdermarrje, int idNdermarrjeVit, string muajt)
        {

        }

        public void DeletePeriodSummaryDetail(int idPeriodSummary)
        {
            FakePeriodSummaryDetailKontabilitetiData._lastPeriodSummaryDetails.AcceptChanges();
            var rows = FakePeriodSummaryDetailKontabilitetiData._lastPeriodSummaryDetails.AsEnumerable().Where(r => r.Field<int>("IdPeriodSummary") == idPeriodSummary);
            foreach (var row in rows)
                row.Delete();
            FakePeriodSummaryDetailKontabilitetiData._lastPeriodSummaryDetails.AcceptChanges();
        }

        public void Dispose()
        {

        }

        public IEnumerable<T> GetAnaliticPeriodSummaryDetails<T>(int idNdermarrje, DateTime dtFundMuaji, Func<IDataRecord, T> funcToFill)
        {
            IDataReader dataReader = FakePeriodSummaryDetailKontabilitetiData.GenerateRandom(100, dtFundMuaji).AsDataReader(x => new
            {
                x.IdPeriodSummaryDetail,
                x.IdPeriodSummary,
                x.IdModuli,
                x.IdNdermarrje,
                x.IdNdermarrjeVit,
                x.DtFundMuaji,
                x.IdMonedha,
                x.IdLlogari,
                x.VleraDebiAkumuluar,
                x.VleraKrediAkumuluar,
                x.VleraDebiMonBazeAkumuluar,
                x.VleraKrediMonBazeAkumuluar,
            });

            while (dataReader.Read())
            {
                yield return funcToFill(dataReader);
            }
        }

        public IEnumerable<T> GetLastPeriodSummaryDetails<T>(int idNdermarrje, Func<IDataRecord, T> funcToFill)
        {
            DataTableReader reader = FakePeriodSummaryDetailKontabilitetiData._lastPeriodSummaryDetails.CreateDataReader();
            while (reader.Read())
            {
                yield return funcToFill(reader);
            }
        }

        public DataTable GetPeriodSummaryDetailsReport(int idNdermarrje, int viti, int muaji)
        {
            return new DataTable();
        }

        public void SavePeriodSummaryDetail(DataTable details)
        {
            FakePeriodSummaryDetailKontabilitetiData._lastPeriodSummaryDetails = details;
        }
    }
}
