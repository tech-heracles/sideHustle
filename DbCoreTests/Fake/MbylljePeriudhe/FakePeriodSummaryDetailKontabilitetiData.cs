using DbCore.MbylljePeriudhe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCoreTests.Fake.MbylljePeriudhe
{
    public static class FakePeriodSummaryDetailKontabilitetiData
    {
        public static DataTable _lastPeriodSummaryDetails;
        
        /// <summary>
        /// Generates random values. After last item, another item with static values of 20 is added;
        /// </summary>
        /// <param name="nr">nr of records</param>
        /// <param name="dtFundMuaji">Data e Fundit e Muajit</param>
        /// <returns>List<PeriodSummaryDetailKontabiliteti></returns>
        public static List<PeriodSummaryDetailKontabiliteti> GenerateRandom(int nr, DateTime dtFundMuaji)
        {
            Random random = new Random();
            var list = new List<PeriodSummaryDetailKontabiliteti>();
            for (int i = 0; i <= nr; i++)
            {
                list.Add(new PeriodSummaryDetailKontabiliteti()
                {
                    IdPeriodSummaryDetail = i,
                    IdPeriodSummary = dtFundMuaji.Month,
                    IdModuli = 0,
                    IdNdermarrje = 0,
                    IdNdermarrjeVit = 0,
                    DtFundMuaji = dtFundMuaji,
                    IdMonedha = 0,
                    IdLlogari = i,
                    VleraDebiAkumuluar = i == nr ? 20 : GetRandomNumber(0, 500000),
                    VleraKrediAkumuluar = i == nr ? 20 : GetRandomNumber(0, 500000),
                    VleraDebiMonBazeAkumuluar = i == nr ? 20 : GetRandomNumber(0, 500000),
                    VleraKrediMonBazeAkumuluar = i == nr ? 20 : GetRandomNumber(0, 500000)
                });
            }
            return list;
        }

        private static decimal GetRandomNumber(double min, double max)
        {
            Random random = new Random();
            return Convert.ToDecimal(random.NextDouble() * (max - min) + min);
        }
    }
}
