using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using System.Collections.Generic;
using System.Data;

namespace DbCore.MbylljePeriudhe
{
    /// <summary>
    /// Concrete class ProcesiAnalitikFletaKontabel
    /// </summary>
    public class PeriodSummaryKontabiliteti : PeriodSummary
    {
        public IList<IPeriodSummaryDetailKontabiliteti> Details { get; set; }
        public PeriodSummaryKontabiliteti() : base() { }

        public override void SetDetails<T>(IList<T> details)
        {
            Details = details as IList<IPeriodSummaryDetailKontabiliteti>;
        }

        public override void FillPeriodSummary(int idNdermarrjeVit, int muaji)
        {
            using (IDatabasePeriodClosingCommon db = DbBuilder.CreateDatabasePeriodClosingCommon())
                db.GetPeriodSummary<PeriodSummaryKontabiliteti>(idNdermarrjeVit, muaji, Fill);
        }
        public override void FillLastPeriodSummary(int idNdermarrjeVit)
        {
            using (IDatabasePeriodClosingCommon db = DbBuilder.CreateDatabasePeriodClosingCommon())
                db.GetLastPeriodSummary<PeriodSummaryKontabiliteti>(idNdermarrjeVit, Fill);
        }

        public override void Save()
        {
            using (IMyTransactionScope scope = DbBuilder.CreateTransactionScope())
            {
                SaveToDb();
                if(Details.Count > 0)
                {
                    Details.ForEach(x => { x.IdPeriodSummary = IdPeriodSummary; });
                    SaveDetailsToDb();
                }
                SaveToHistoric(1);

                scope.Complete();
            }
        }

        public override void Delete()
        {
            using (IMyTransactionScope scope = DbBuilder.CreateTransactionScope())
            {
                SaveToHistoric(2);
                DeleteDetailsFromDb();
                DeleteFromDb();

                scope.Complete();
            }
        }


        private void SaveToDb()
        {
            using (IDatabasePeriodClosingCommon db = DbBuilder.CreateDatabasePeriodClosingCommon())
                db.SavePeriodSummary(this);
        }

        private void SaveDetailsToDb()
        {
            DataTable details = Details.ToDataTable("IdPeriodSummaryDetail", "IdPeriodSummary", "IdModuli", "IdNdermarrje", "IdNdermarrjeVit", "DtFundMuaji", "IdMonedha", "IdLlogari", "VleraDebiAkumuluar", "VleraKrediAkumuluar", "VleraDebiMonBazeAkumuluar", "VleraKrediMonBazeAkumuluar"); 
            using (IDatabasePeriodClosing db = DbBuilder.CreateDatabasePeriodClosing())
                db.SavePeriodSummaryDetail(details);
        }

        private void SaveToHistoric(int status)
        {
            using (IDatabasePeriodClosingCommon db = DbBuilder.CreateDatabasePeriodClosingCommon())
                db.SavePeriodSummaryToHistorik(this, status);
        }

        private void DeleteFromDb()
        {
            using (IDatabasePeriodClosingCommon db = DbBuilder.CreateDatabasePeriodClosingCommon())
                db.DeletePeriodSummary(this);
        }
        private void DeleteDetailsFromDb()
        {
            using (IDatabasePeriodClosing db = DbBuilder.CreateDatabasePeriodClosing())
                db.DeletePeriodSummaryDetail(IdPeriodSummary);
        }
    }
}
