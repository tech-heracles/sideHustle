using DbCore.MbylljePeriudhe;

namespace DbCore.IMBUtils.DataBase
{
    public class DbBuilder : IDbBuilder
    {
        public DbBuilder() { }
        public IDbData DbData { get; private set; }
        private Modul Moduli { get; set; }

        public DbBuilder(IDbData dbData)
        {
            DbData = dbData;
        }
        public DbBuilder(IDbData dbData, Modul moduli)
        {
            DbData = dbData;
            Moduli = moduli;
        }        

        public IMyTransactionScope CreateTransactionScope()
        {
            return new MyTransactionScope();
        }

        public IDatabasePeriodClosing CreateDatabasePeriodClosing()
        {
            switch (Moduli)
            {
                case Modul.M_KONTABILITETI:
                    return new DatabasePeriodClosingKontabiliteti(DbData);
                default:
                    return null;
            }
        }

        public IDatabasePeriodClosingCommon CreateDatabasePeriodClosingCommon()
        {
            return new DatabasePeriodClosingCommon(DbData);
        }
    }
}
