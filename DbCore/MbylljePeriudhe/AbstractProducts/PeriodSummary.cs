using NLog;
using DbCore.IMBUtils.DataBase;
using System;

using System.Data;
using System.Collections.Generic;

namespace DbCore.MbylljePeriudhe
{
    public abstract class PeriodSummary
    {
        public int IdPeriodSummary { get; set; }
        public int IdProcess { get; set; }
        public int IdModuli { get; set; }
        public int IdNdermarrje { get; set; }
        public int IdNdermarrjeVit { get; set; }
        public string Pershkrimi { get; set; }
        public int Muaji { get; set; }
        public DateTime DtFundMuaji { get; set; }
        public DateTime DtFillimi { get; set; }
        public DateTime DtMbarimi { get; set; }
        public Statusi Statusi { get; set; }
        public IDbBuilder DbBuilder { get; set; }
        public ILogger Logger { get; set; }


        public abstract void Save();
        public abstract void Delete();
        public abstract void SetDetails<T>(IList<T> details);

        public PeriodSummary()
        {

        }

        public abstract void FillPeriodSummary(int idNdermarrjeVit, int muaji);
        public abstract void FillLastPeriodSummary(int idNdermarrjeVit);

        public void Fill(IDataRecord record)
        {
            IdPeriodSummary = !Convert.IsDBNull(record["IdPeriodSummary"]) ? Convert.ToInt32(record["IdPeriodSummary"]) : 0;
            IdProcess = !Convert.IsDBNull(record["IdProcess"]) ? Convert.ToInt32(record["IdProcess"]) : 0;
            IdModuli = !Convert.IsDBNull(record["IdModuli"]) ? Convert.ToInt32(record["IdModuli"]) : 0;
            IdNdermarrje = !Convert.IsDBNull(record["IdNdermarrje"]) ? Convert.ToInt32(record["IdNdermarrje"]) : 0;
            IdNdermarrjeVit = !Convert.IsDBNull(record["IdNdermarrjeVit"]) ? Convert.ToInt32(record["IdNdermarrjeVit"]) : 0;
            Pershkrimi = !Convert.IsDBNull(record["Pershkrimi"]) ? Convert.ToString(record["Pershkrimi"]) : String.Empty;
            Muaji = !Convert.IsDBNull(record["Muaji"]) ? Convert.ToInt32(record["Muaji"]) : 0;
            DtFundMuaji = !Convert.IsDBNull(record["DtFundMuaji"]) ? Convert.ToDateTime(record["DtFundMuaji"]) : new DateTime();
            DtFillimi = !Convert.IsDBNull(record["DtFillimi"]) ? Convert.ToDateTime(record["DtFillimi"]) : new DateTime();
            DtMbarimi = !Convert.IsDBNull(record["DtMbarimi"]) ? Convert.ToDateTime(record["DtMbarimi"]) : new DateTime();
            Statusi = !Convert.IsDBNull(record["Statusi"]) ? (Statusi)Convert.ToInt32(record["Statusi"]) : 0;
        }
    }
}
