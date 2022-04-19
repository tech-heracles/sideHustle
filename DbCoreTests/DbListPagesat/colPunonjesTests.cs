using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbCore.DbListPagesat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace DbCoreTests
{
    [TestClass()]
    public class colPunonjesTests : FakeHttpContextBase
    {

        [TestMethod()]
        public void colPunonjesTest()
        {
            colPunonjes punonjes = new colPunonjes(1112);
            Assert.IsTrue(true);
        }
    }
}