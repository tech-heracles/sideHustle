using DbCore.MbylljePeriudhe;

namespace DbCore.IMBUtils.DataBase
{
    public interface IDbBuilder
    {
        IDatabasePeriodClosing CreateDatabasePeriodClosing();
        IDatabasePeriodClosingCommon CreateDatabasePeriodClosingCommon();
        IMyTransactionScope CreateTransactionScope();
    }
}
