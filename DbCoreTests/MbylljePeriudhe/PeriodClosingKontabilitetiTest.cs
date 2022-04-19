using DbCore;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.MbylljePeriudhe;
using DbCoreTests.Fake;
using DbCoreTests.Fake.MbylljePeriudhe;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCoreTests.MbylljePeriudhe
{
    [TestClass]
    public class PeriodClosingKontabilitetiTest
    {
        string _serverName = "unitTester";
        private int _idNdermarrje = 0;
        private int _idNdermarrjeVit = 0;
        private int _viti = 2019;
        private int _idPerdoruesi = 0;
        private int _idModuli = 0;
        private Modul _moduli = Modul.M_KONTABILITETI;

        /// </summary>
        [TestMethod]
        public void MbylljePeriudheTest()
        {
            CreateProcessTest(PcAction.MbyllPeriudhe);
            DeleteProcessTest(PcAction.AnulloMbyllje);
        }

        private void CreateProcessTest(PcAction action)
        {
            IDbBuilder dbBuilder = new FakeDbBuilder() { _moduli = _moduli };
            ILogger logger = new FakeImbLogger();

            List<int> months = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
            if (action == PcAction.AnulloMbyllje)
                months.Reverse();

            List<PeriodSummaryDetailKontabiliteti> first = FakePeriodSummaryDetailKontabilitetiData.GenerateRandom(100, new DateTime(2018, 12, 31, 23, 59, 59));
            FakePeriodSummaryDetailKontabilitetiData._lastPeriodSummaryDetails = first.ToDataTable();
            DateTime lastClosedDate = new DateTime(_viti, months.Last(), DateTime.DaysInMonth(_viti, months.Last()), 23, 59, 59).AddMonths(action == PcAction.AnulloMbyllje ? -1 : 0);
            Process procesi = new Process()
            {
                Messages = new FakeMessages(),
                TaskManager = new FakeTastManager(),
                ServerName = _serverName,
                DbBuilder = dbBuilder,
                MesazhBuilder = new FakeMesazhBuilder(),
                Logger = logger,
                PeriodSummaryFactory = PeriodClosing.CreateFactory(_moduli, dbBuilder),
                Modul = _moduli,
                Months = months,
                IdNdermarrje = _idNdermarrje,
                IdModuli = _idModuli,
                IdKrijuesi = _idPerdoruesi,
                IdNdermarrjeVit = _idNdermarrjeVit,
                Statusi = Statusi.InProcess,
                DtFillimi = DateTime.Now,
                Action = action,
                PeriodSummaries = new List<PeriodSummary>(),
                CurrentMonth = months.First(),
                CurrentYear = _viti,
                LastClosedPeriod = new ClosedPeriod() { ServerName = _serverName, CompanyId = _idNdermarrje, Modul = _moduli, LastClosedDate = lastClosedDate }
            };

            IMesazh mesazh = procesi.Start();
            Assert.IsTrue(mesazh.Status);
            Assert.IsTrue(procesi.LastClosedPeriod.LastClosedDate == new DateTime(2019, 12, 31, 23, 59, 59));
            DataRow last = FakePeriodSummaryDetailKontabilitetiData._lastPeriodSummaryDetails.AsEnumerable().Where(r => r.Field<int>("IdLlogari") == 100).FirstOrDefault();
            Assert.IsTrue(Convert.ToDateTime(last["DtFundMuaji"]) == new DateTime(2019, 12, 31, 23, 59, 59));
            Assert.IsTrue(Convert.ToInt32(last["VleraKrediAkumuluar"]) == 20 + (months.Count * 20));
            Assert.IsFalse(Convert.ToInt32(last["VleraKrediAkumuluar"]) == (months.Count * 20));

        }

        private void DeleteProcessTest(PcAction action)
        {
            IDbBuilder dbBuilder = new FakeDbBuilder() { _moduli = _moduli };
            ILogger logger = new FakeImbLogger();

            List<int> months = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
            if (action == PcAction.AnulloMbyllje)
                months.Reverse();

            List<PeriodSummaryDetailKontabiliteti> first = FakePeriodSummaryDetailKontabilitetiData.GenerateRandom(100, new DateTime(2018, 12, 31, 23, 59, 59));
            FakePeriodSummaryDetailKontabilitetiData._lastPeriodSummaryDetails = first.ToDataTable();

            DateTime lastClosedDate = new DateTime(_viti, months.Last(), DateTime.DaysInMonth(_viti, months.Last()), 23, 59, 59).AddMonths(action == PcAction.AnulloMbyllje ? -1 : 0);
            Process procesi = new Process()
            {
                Messages = new FakeMessages(),
                TaskManager = new FakeTastManager(),
                ServerName = _serverName,
                DbBuilder = dbBuilder,
                MesazhBuilder = new FakeMesazhBuilder(),
                Logger = logger,
                PeriodSummaryFactory = PeriodClosing.CreateFactory(_moduli, dbBuilder),
                Modul = _moduli,
                Months = months,
                IdNdermarrje = _idNdermarrje,
                IdModuli = _idModuli,
                IdKrijuesi = _idPerdoruesi,
                IdNdermarrjeVit = _idNdermarrjeVit,
                Statusi = Statusi.InProcess,
                DtFillimi = DateTime.Now,
                Action = action,
                PeriodSummaries = new List<PeriodSummary>(),
                CurrentMonth = months.First(),
                CurrentYear = _viti,
                LastClosedPeriod = new ClosedPeriod() { ServerName = _serverName, CompanyId = _idNdermarrje, Modul = _moduli, LastClosedDate = lastClosedDate }
            };

            IMesazh mesazh = procesi.Start();
            Assert.IsTrue(mesazh.Status);
            Assert.IsTrue(FakePeriodSummaryDetailKontabilitetiData._lastPeriodSummaryDetails.Rows.Count == 0);

        }
    }
}