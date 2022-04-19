using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.MbylljePeriudhe
{
    /// <summary>
    /// Factory class ProcesiFletaKontabelFactory => ProcesiAnalitikFletaKontabel
    /// </summary>
    public class PeriodSummaryFactoryKontabiliteti : PeriodSummaryFactory
    {
        #region public
        public PeriodSummaryFactoryKontabiliteti() : base() { }

        public override PeriodSummary GetPeriodSummary(int idNdermarrjeVit, int muaji)
        {
            PeriodSummary periodSummary = new PeriodSummaryKontabiliteti()
            {
                DbBuilder = DbBuilder,
                Logger = Logger
            };
            periodSummary.FillPeriodSummary(idNdermarrjeVit, muaji);
            return periodSummary;
        }
        public override PeriodSummary GetLastPeriodSummary(int idNdermarrjeVit)
        {
            PeriodSummary periodSummary = new PeriodSummaryKontabiliteti()
            {
                DbBuilder = DbBuilder,
                Logger = Logger
            };
            periodSummary.FillLastPeriodSummary(idNdermarrjeVit);
            return periodSummary;
        }

        public override PeriodSummary CreatePeriodSummary(int idProcess, int idNdermarrje, int idNdermarrjeVit, int idModuli, string pershkrimi, int muaji, DateTime dtFillimi, DateTime dtFundMuaji, Statusi statusi)
        {
            return new PeriodSummaryKontabiliteti()
            {
                DbBuilder = DbBuilder,
                Logger = Logger,
                IdProcess = idProcess,
                IdNdermarrje = idNdermarrje,
                IdNdermarrjeVit = idNdermarrjeVit,
                IdModuli = idModuli,
                Pershkrimi = pershkrimi,
                Muaji = muaji,
                DtFillimi = dtFillimi,
                DtFundMuaji = dtFundMuaji,
                Statusi = statusi
            };
        }

        public override void GeneratePeriodSummaryDetails(PeriodSummary periodSummary)
        {
            IList<IPeriodSummaryDetailKontabiliteti> previousDetails = GetLastPeriodSummaryDetails(periodSummary.IdNdermarrje);
            IList<IPeriodSummaryDetailKontabiliteti> currentDetails = GetAnaliticPeriodSummaryDetails(periodSummary.IdNdermarrje, periodSummary.DtFundMuaji);
            periodSummary.SetDetails(GeneratePeriodSummaryDetails(previousDetails, currentDetails, periodSummary));
        }

        public override void SavePeriodSummary(PeriodSummary procesiAnalitik)
        {
            procesiAnalitik.Save();
        }

        public override void DeletePeriodSummary(PeriodSummary procesiAnalitik)
        {
            procesiAnalitik.Delete();
        }

        #endregion

        #region private

        private PeriodSummaryDetailKontabiliteti CreatePeriodSummaryDetailKontabiliteti(IDataRecord record)
        {
            return new PeriodSummaryDetailKontabiliteti(record);
        }

        private IList<IPeriodSummaryDetailKontabiliteti> GetAnaliticPeriodSummaryDetails(int idNdermarrje, DateTime dtFundMuaji)
        {
            using (IDatabasePeriodClosing db = DbBuilder.CreateDatabasePeriodClosing())
                return db.GetAnaliticPeriodSummaryDetails<IPeriodSummaryDetailKontabiliteti>(idNdermarrje, dtFundMuaji, CreatePeriodSummaryDetailKontabiliteti).ToList();
        }

        private IList<IPeriodSummaryDetailKontabiliteti> GetLastPeriodSummaryDetails(int idNdermarrje)
        {
            using (IDatabasePeriodClosing db = DbBuilder.CreateDatabasePeriodClosing())
                return db.GetLastPeriodSummaryDetails<IPeriodSummaryDetailKontabiliteti>(idNdermarrje, CreatePeriodSummaryDetailKontabiliteti).ToList();
        }

        private IList<IPeriodSummaryDetailKontabiliteti> GeneratePeriodSummaryDetails(IList<IPeriodSummaryDetailKontabiliteti> previousDetails, IList<IPeriodSummaryDetailKontabiliteti> currentsDetails, PeriodSummary periodSummary)
        {
            List<IPeriodSummaryDetailKontabiliteti> details = new List<IPeriodSummaryDetailKontabiliteti>();
            IPeriodSummaryDetailKontabiliteti previousDetail = new PeriodSummaryDetailKontabiliteti();
            currentsDetails.ToList().ForEach(currentDetails =>
            {
                previousDetail = previousDetails.FirstOrDefault(x => x.IdLlogari == currentDetails.IdLlogari);
                if (previousDetail == null)
                    previousDetail = new PeriodSummaryDetailKontabiliteti();

                details.Add(new PeriodSummaryDetailKontabiliteti()
                {
                    IdNdermarrje = periodSummary.IdNdermarrje,
                    IdModuli = periodSummary.IdModuli,
                    IdNdermarrjeVit = periodSummary.IdNdermarrjeVit,
                    DtFundMuaji = periodSummary.DtFundMuaji,
                    IdMonedha = currentDetails.IdMonedha,
                    IdLlogari = currentDetails.IdLlogari,
                    VleraDebiAkumuluar = previousDetail.VleraDebiAkumuluar + currentDetails.VleraDebiAkumuluar,
                    VleraKrediAkumuluar = previousDetail.VleraKrediAkumuluar + currentDetails.VleraKrediAkumuluar,
                    VleraDebiMonBazeAkumuluar = previousDetail.VleraDebiMonBazeAkumuluar + currentDetails.VleraDebiMonBazeAkumuluar,
                    VleraKrediMonBazeAkumuluar = previousDetail.VleraKrediMonBazeAkumuluar + currentDetails.VleraKrediMonBazeAkumuluar
                });
            });
            details.AddRange(previousDetails.Where(p => !details.Any(p2 => p2.IdLlogari == p.IdLlogari)));
            details.ForEach(x => { x.DtFundMuaji = periodSummary.DtFundMuaji; });
            return details;
        }
        #endregion

        #region detailsReport
        public override DataTable GetPeriodSummaryDetailsReport(int idNdermarrje, int viti, int muaji)
        {
            using (IDatabasePeriodClosing db = DbBuilder.CreateDatabasePeriodClosing())
                return db.GetPeriodSummaryDetailsReport(idNdermarrje, viti, muaji);
        }
        #endregion

        #region deleteHistoric
        public override void DeleteHistoric(int idNdermarrje, int idNdermarrjeVit, string muajt)
        {
            using (IDatabasePeriodClosing db = DbBuilder.CreateDatabasePeriodClosing())
                db.DeleteHistoric(idNdermarrje, idNdermarrjeVit, muajt);
        }
        #endregion
    }
}
