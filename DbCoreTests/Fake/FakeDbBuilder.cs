using DbCore.IMBUtils.DataBase;
using DbCore.MbylljePeriudhe;
using DbCoreTests.Fake.MbylljePeriudhe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCoreTests.Fake
{
    public class FakeDbBuilder : IDbBuilder
    {
        public Modul _moduli;
        public IDatabasePeriodClosing CreateDatabasePeriodClosing()
        {
            switch (_moduli)
            {
                case Modul.M_KONTABILITETI:
                    return new FakeDatabasePeriodClosingKontabiliteti();
                default:
                    return null;
            }
        }

        public IDatabasePeriodClosingCommon CreateDatabasePeriodClosingCommon()
        {
            return new FakeDatabasePeriodClosingCommon();
        }

        public IMyTransactionScope CreateTransactionScope()
        {
            return new FakeMyTransactionScope();
        }


    }
}
