using AlphaWeb.Core.Messages;
using DbCore.IMBUtils.DataBase;
using System;
using System.Collections.Generic;
using NLog;
using AlphaWeb.Core.Interfaces;
using AlphaWeb.Core.Interfaces.Localization;
using DbCore.MbylljePeriudhe.Interfaces;

namespace DbCore.MbylljePeriudhe
{    

    public class Process : IProcess
    {
        public bool Cancelled { get; set; }
        public ITaskManager TaskManager { get; set; }
        public string ServerName { get; set; }
        public IMessagesResource Messages { get; set; }
        public ILogger Logger { get; set; }
        public IDbBuilder DbBuilder { get; set; }
        public IMesazhBuilder MesazhBuilder { get; set; }
        public ICollection<PeriodSummary> PeriodSummaries { get; set; }
        public Modul Modul { get; set; }
        public IList<int> Months { get; set; }
        public PeriodSummaryFactory PeriodSummaryFactory { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public int IdProcess { get; set; }
        public int IdModuli { get; set; }
        public int IdNdermarrje { get; set; }
        public int IdNdermarrjeVit { get; set; }
        public int IdKrijuesi { get; set; }
        public DateTime DtFillimi { get; set; }
        public DateTime DtMbarimi { get; set; }
        public Statusi Statusi { get; set; }
        public PcAction Action { get; set; }
        public IClosedPeriod LastClosedPeriod { get; set; }


        public Process()
        {
            Cancelled = false;
        }

        public IMesazh Start()
        {
            if (ProcesetContainer.Instance.GetProcesi(ServerName, IdNdermarrje) != null)
                return MesazhBuilder.CreateMesazhGabimi(Messages.Get("MP_msgProcessStartedAlready"));

            ProcesetContainer.Instance.SetProcess(this);
            switch (Action)
            {
                case PcAction.MbyllPeriudhe:
                    TaskManager.CreateNewTask(CreatePeriodClosing, Cleanup);
                    break;
                case PcAction.AnulloMbyllje:
                    TaskManager.CreateNewTask(DeletePeriodClosing, Cleanup);
                    break;
                case PcAction.FshiHistorik:
                    TaskManager.CreateNewTask(DeleteHistoric, Cleanup);
                    break;
            }
            return MesazhBuilder.CreateMesazhSuksesi(Messages.Get("MP_msgProcessStarted"));
        }

        public IMesazh Cancel()
        {
            IMesazh mesazh;
            if (!Cancelled)
            {
                Cancelled = true;
                mesazh = MesazhBuilder.CreateMesazhSuksesi(Messages.Get("MP_msgProcessStoped"));
            }
            else
                mesazh = MesazhBuilder.CreateMesazhSuksesi(Messages.Get("MP_msgProcessFinished"));
            LastClosedPeriod.LastClosedDate = new DateTime(CurrentYear, CurrentMonth, DateTime.DaysInMonth(CurrentYear, CurrentMonth), 23, 59, 59).AddMonths(Action == PcAction.AnulloMbyllje ? -1 : 0);
            ProcesetContainer.Instance.DeleteProcess(this);
            return mesazh;
        }

        #region private
        private void Cleanup()
        {
            LastClosedPeriod.LastClosedDate = new DateTime(CurrentYear, CurrentMonth, DateTime.DaysInMonth(CurrentYear, CurrentMonth), 23, 59, 59).AddMonths(Action == PcAction.AnulloMbyllje ? -1 : 0);
            ProcesetContainer.Instance.DeleteProcess(this);
        }

        private void CreatePeriodClosing()
        {
            try
            {
                Save();
                bool valid = false;
                foreach (int month in Months)
                {
                    if (Cancelled)
                        break;

                    CurrentMonth = month;

                    DateTime dtFundMuaji = new DateTime(CurrentYear, CurrentMonth, DateTime.DaysInMonth(CurrentYear, CurrentMonth), 23, 59, 59);
                    PeriodSummary periodSummary = PeriodSummaryFactory.CreatePeriodSummary(IdProcess, IdNdermarrje, IdNdermarrjeVit, IdModuli, Modul.ToString(), month, DateTime.Now, dtFundMuaji, Statusi.InProcess);
                    if (!valid)
                    {
                        valid = IsValid(periodSummary);
                        if (!valid)
                            continue;
                    }

                    PeriodSummaries.Add(periodSummary);
                    PeriodSummaryFactory.GeneratePeriodSummaryDetails(periodSummary);
                    periodSummary.DtMbarimi = DateTime.Now;
                    periodSummary.Statusi = Statusi.Done;
                    PeriodSummaryFactory.SavePeriodSummary(periodSummary);
                    PeriodSummaries.Remove(periodSummary);
                }
                DtMbarimi = DateTime.Now;
                Statusi = Statusi.Done;
                Update();
            }
            catch(Exception ex)
            {
                DtMbarimi = DateTime.Now;
                Statusi = Statusi.Error;
                Update();
                Logger.Error($"Gabim gjate mbylljes se muajit: {CurrentMonth}/{CurrentYear} - (IdNdermarrje: {IdNdermarrje}, IdPerdoruesi: {IdKrijuesi}, Veprimi: {Action.ToString()}) - {ex.Message}");
            }
        }

        private bool IsValid(PeriodSummary periodSummary)
        {
            PeriodSummary lastPeriodSummary = PeriodSummaryFactory.GetLastPeriodSummary(IdNdermarrjeVit);
            DateTime previousMonth = periodSummary.DtFundMuaji.AddMonths(-1);
            if (lastPeriodSummary.IdPeriodSummary > 0 && lastPeriodSummary.DtFundMuaji.Month != previousMonth.Month && lastPeriodSummary.DtFundMuaji.Year != previousMonth.Year)
                return false;

            PeriodSummary existentPeriodSummary = PeriodSummaryFactory.GetPeriodSummary(IdNdermarrjeVit, CurrentMonth);
            if (existentPeriodSummary.IdPeriodSummary > 0)
                return false;

            return true;
        }

        private void DeletePeriodClosing()
        {
            try
            {
                Save();
                foreach (int month in Months)
                {
                    if (Cancelled)
                        break;

                    CurrentMonth = month;
                    PeriodSummary procesiAnalitik = PeriodSummaryFactory.GetPeriodSummary(IdNdermarrjeVit, month);
                    if (procesiAnalitik.IdPeriodSummary <= 0)
                        continue;

                    PeriodSummaries.Add(procesiAnalitik);
                    PeriodSummaryFactory.DeletePeriodSummary(procesiAnalitik);
                    PeriodSummaries.Remove(procesiAnalitik);
                }
                DtMbarimi = DateTime.Now;
                Statusi = Statusi.Done;
                Update();
            }
            catch (Exception ex)
            {
                DtMbarimi = DateTime.Now;
                Statusi = Statusi.Error;
                Update();
                Logger.Error($"Gabim gjate anullimit te muajit: {CurrentMonth}/{CurrentYear} - (IdNdermarrje: {IdNdermarrje}, IdPerdoruesi: {IdKrijuesi}, Veprimi: {Action.ToString()}) - {ex.Message}");
            }
        }

        private void DeleteHistoric()
        {
            try
            {
                Save();
                PeriodSummaryFactory.DeleteHistoric(IdNdermarrje, IdNdermarrjeVit, string.Join(",",Months));
                DtMbarimi = DateTime.Now;
                Statusi = Statusi.Done;
                Update();
            }
            catch (Exception ex)
            {
                DtMbarimi = DateTime.Now;
                Statusi = Statusi.Error;
                Update();
                Logger.Error($"Gabim gjate fshirjes se historikut: {CurrentYear} - (IdNdermarrje: {IdNdermarrje}, IdPerdoruesi: {IdKrijuesi}, Veprimi: {Action.ToString()}) - {ex.Message}");
            }
        }

        private void Save()
        {
            using (IMyTransactionScope scope = DbBuilder.CreateTransactionScope())
            {
                SaveToDb();
                scope.Complete();
            }
        }

        private void Update()
        {
            using (IMyTransactionScope scope = DbBuilder.CreateTransactionScope())
            {
                UpdateToDb();
                scope.Complete();
            }
        }

        private void SaveToDb()
        {
            using (IDatabasePeriodClosingCommon db = DbBuilder.CreateDatabasePeriodClosingCommon())
                db.SaveProcess(this);
        }

        private void UpdateToDb()
        {
            using (IDatabasePeriodClosingCommon db = DbBuilder.CreateDatabasePeriodClosingCommon())
                db.UpdateProcess(this);
        }
        #endregion
    }
}
