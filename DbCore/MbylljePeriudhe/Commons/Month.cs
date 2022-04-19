using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using System;
using System.Data;

namespace DbCore.MbylljePeriudhe
{
    public class Month : IDataBaseReader
    {
        public int IdMuaji { get; set; }
        public string KodiMuaji { get; set; }
        public int IdNdermarrjeVit { get; set; }
        public int IdNdermarrje { get; set; }
        public bool Disabled { get; set; }
        public IDbBuilder DbBuilder { get; private set; }

        public Month()
        {
        }

        public Month(IDbBuilder dbBuilder)
        {
            DbBuilder = dbBuilder;
        }

        private Month(IDataRecord record)
        {
            Mbush(record);
        }

        private Month CreateMonth(IDataRecord record)
        {
            return new Month(record);
        }

        public void Mbush(IDataRecord record)
        {
            IdMuaji = !Convert.IsDBNull(record["IdMuaji"]) ? Convert.ToInt32(record["IdMuaji"]) : 0;
            KodiMuaji = !Convert.IsDBNull(record["KodiMuaji"]) ? Convert.ToString(record["KodiMuaji"]) : String.Empty;
            IdNdermarrjeVit = !Convert.IsDBNull(record["IdNdermarrjeVit"]) ? Convert.ToInt32(record["IdNdermarrjeVit"]) : 0;
            IdNdermarrje = !Convert.IsDBNull(record["IdNdermarrje"]) ? Convert.ToInt32(record["IdNdermarrje"]) : 0;
        }

        public void GetLastClosedMonth(int idNdermarrje, int idNdermarrjeVit, int idModuli)
        {
            using (IDatabasePeriodClosingCommon db = DbBuilder.CreateDatabasePeriodClosingCommon())
                db.GetLastClosedMonth(idNdermarrje, idNdermarrjeVit, idModuli, this);
        }
    }
}
