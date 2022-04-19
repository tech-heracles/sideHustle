using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.Types;

namespace DbCoreTests.UtilsTest
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void TestGetVlereDefault()
        {
            Assert.IsTrue(!Converter.MerrVlereOseDefault<bool>(""));
            Assert.IsTrue(!Converter.MerrVlereOseDefault<bool>("false"));
            Assert.IsTrue(Converter.MerrVlereOseDefault<bool>("true"));
            Assert.IsTrue(Converter.MerrVlereOseDefault<int>("1") == 1);
            Assert.IsTrue(Converter.MerrVlereOseDefault<decimal>("1.1") == 1.1M);
            Assert.IsTrue(Converter.MerrVlereOseDefault<decimal>("") == 0M);
            Assert.IsTrue(Converter.MerrVlereOseDefault<decimal>("null") == 0M);
            Assert.IsTrue(Converter.MerrVlereOseDefault<decimal>("undefined") == 0M);
            Assert.IsTrue(Converter.MerrVlereOseDefault<decimal>("NaN") == 0M);
        }

    }
}
