using DbCore.DbAdmin;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.MbylljePeriudhe
{
    public static class PeriodClosing
    {
        public static IEnumerable<Month> GetMonths()
        {
            return CreateMonths(0, PcAction.Undefined, 0, false, 0);
        }
        public static IEnumerable<Month> GetMonths(int idNdermarrje, int idNdermarrjeVit, int idModuli, PcAction action)
        {

            int monthId = 0;
            bool disabledAll = false;
            colNdermarrjeVitet years = new colNdermarrjeVitet();
            years.mbushGjitheViteELidhuraMeNdermarrje(idNdermarrje);
            int selectedIdNdermarrjeVit = idNdermarrjeVit;

            if (action == PcAction.FshiHistorik)
                selectedIdNdermarrjeVit = idNdermarrjeVit < 1 ? years.FirstOrDefault().IdNderViti : idNdermarrjeVit;
            else
                monthId = GetLastClosedMonthOfYear(idNdermarrje, idNdermarrjeVit, idModuli, action, ref disabledAll, years, ref selectedIdNdermarrjeVit);

            return CreateMonths(idNdermarrje, action, monthId, disabledAll, selectedIdNdermarrjeVit);
        }

        private static int GetLastClosedMonthOfYear(int idNdermarrje, int idNdermarrjeVit, int idModuli, PcAction action, ref bool disabledAll, colNdermarrjeVitet years, ref int selectedIdNdermarrjeVit)
        {
            IDbBuilder dbBuilder = new DbBuilder(new DbData());
            Month month = new Month(dbBuilder);
            month.GetLastClosedMonth(idNdermarrje, idNdermarrjeVit, idModuli);
            int monthId = month.IdMuaji;
            if (selectedIdNdermarrjeVit == 0)
            {
                selectedIdNdermarrjeVit = GetSelectedYear(action, years, month, monthId);
                monthId = selectedIdNdermarrjeVit != month.IdNdermarrjeVit ? 0 : monthId;
            }
            else
            {
                Month lastClosedMonth = new Month(dbBuilder);
                lastClosedMonth.GetLastClosedMonth(idNdermarrje, 0, idModuli);
                disabledAll = GetSelectedYear(action, years, lastClosedMonth, lastClosedMonth.IdMuaji) != selectedIdNdermarrjeVit;
            }

            return monthId;
        }

        private static int GetSelectedYear(PcAction action, colNdermarrjeVitet years, Month month, int monthId)
        {
            int selectedIdNdermarrjeVit = month.IdNdermarrjeVit;
            switch (monthId)
            {
                case 0:
                    selectedIdNdermarrjeVit = years.FirstOrDefault().IdNderViti;
                    break;
                case 12:
                    int indexOfYear = years.FindIndex(x => x.IdNderViti == month.IdNdermarrjeVit);
                    if (years.Count == indexOfYear + 1 || action == PcAction.AnulloMbyllje)
                        break;
                    selectedIdNdermarrjeVit = years[indexOfYear + 1].IdNderViti;
                    break;
            }

            return selectedIdNdermarrjeVit;
        }

        private static List<Month> CreateMonths(int idNdermarrje, PcAction action, int monthId, bool disabledAll, int selectedIdNdermarrjeVit)
        {
            List<Month> muajt = new List<Month>();
            for (int i = 1; i <= 12; i++)
            {
                bool mbyllur = i <= monthId;
                muajt.Add(new Month()
                {
                    IdMuaji = i,
                    IdNdermarrje = idNdermarrje,
                    IdNdermarrjeVit = selectedIdNdermarrjeVit,
                    KodiMuaji = $"{ MessagesResource.Messages[$"muaji_" + i] } {(mbyllur ? MessagesResource.Messages["MP_lblMbyllur"] : "")}",
                    Disabled = ((action == PcAction.MbyllPeriudhe && mbyllur) || (action == PcAction.AnulloMbyllje && !mbyllur) || disabledAll) && action != PcAction.FshiHistorik
                });
            }

            return muajt;
        }

        public static PeriodSummaryFactory CreateFactory(Modul moduli, IDbBuilder dbBuilder)
        {
            switch (moduli)
            {
                case Modul.M_KONTABILITETI:
                    return CreatePeriodSummaryFactoryKontabiliteti(dbBuilder);
                default:
                    return null;
            }
        }

        private static PeriodSummaryFactory CreatePeriodSummaryFactoryKontabiliteti(IDbBuilder dbBuilder)
        {
            return new PeriodSummaryFactoryKontabiliteti()
            {
                DbBuilder = dbBuilder,
                Logger = ImbLogger.MbylljePeriudheLogger
            };
        }

        public static DataTable GetAllPeriodSummaryByIdNdermarrje(IDbBuilder dbBuilder, int idNdermarrje)
        {
            using (var db = dbBuilder.CreateDatabasePeriodClosingCommon())
                return db.GetAllPeriodSummaryByIdNdermarrje(idNdermarrje);
        }

        public static bool IsPeriodClosed(DateTime dateToCheck, string serverName, int companyId, KategoriDokumenti kategoriDokumenti, int idKonfigAmbjente)
        {
            List<Modul> modulsToCheck = new List<Modul>();
            switch (kategoriDokumenti)
            {
                case KategoriDokumenti.FleteKontabel:
                case KategoriDokumenti.RivleresimInventari:
                    modulsToCheck.Add(Modul.M_KONTABILITETI);
                    break;
                case KategoriDokumenti.Shitje:
                case KategoriDokumenti.Blerje:
                case KategoriDokumenti.VeprimeArke:
                case KategoriDokumenti.VeprimeBanke:
                case KategoriDokumenti.Magazina:
                case KategoriDokumenti.ShperndarjeShpenzimesh:
                case KategoriDokumenti.FleteDoganore:
                case KategoriDokumenti.LidhjeDokumentash:
                case KategoriDokumenti.AzhornimKlientFurnitor:
                case KategoriDokumenti.VeprimeKlientFurnitor:
                case KategoriDokumenti.ListPagesa:
                case KategoriDokumenti.EkzekutimProdhimi:
                case KategoriDokumenti.MbylljeKF:
                case KategoriDokumenti.Amortizimi:
                case KategoriDokumenti.RivleresimeAmortizimi:
                case KategoriDokumenti.NdryshimSasiCmim:
                    string atlernative = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "GJK").ToLower();
                    if (!string.IsNullOrEmpty(atlernative) && atlernative != "jo")
                        modulsToCheck.Add(Modul.M_KONTABILITETI);
                    break;
            }

            bool isPeriodClosed = false;
            foreach (Modul modul in modulsToCheck)
            {
                isPeriodClosed = ProcesetContainer.Instance.IsPeriodClosed(new ClosedPeriod() { LastClosedDate = dateToCheck, ServerName = serverName, CompanyId = companyId, Modul = modul });
                if (isPeriodClosed)
                    return isPeriodClosed;
            }
            return isPeriodClosed;
        }
    }
}
