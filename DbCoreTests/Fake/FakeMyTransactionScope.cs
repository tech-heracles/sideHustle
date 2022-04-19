using DbCore.IMBUtils.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCoreTests.Fake
{
    public class FakeMyTransactionScope : IMyTransactionScope
    {
        public void Complete()
        {

        }

        public void Dispose()
        {

        }
    }
}
