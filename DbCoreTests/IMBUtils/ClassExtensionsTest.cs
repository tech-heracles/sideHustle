using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbCore.IMBUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.DbInventari;

namespace DbCoreTests.ClassExtensions
{
    [TestClass()]
    public class ClassExtensionsTest
    {
        [TestMethod()]
        public void TestEmptyObject()
        {
            clsArtikulli artikull = new clsArtikulli();
            artikull.EmptyObject();
            Assert.AreEqual(artikull.KodArtikulli,string.Empty);
        }
    }
}