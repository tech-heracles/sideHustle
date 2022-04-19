using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbCore.IMBUtils.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCoreTests.IMBUtils.Validation
{
    [TestClass()]
    public class CodeProviderTests
    {
        [TestMethod()]
        public void emerIVlefshemTest()
        {

            Assert.IsTrue(CodeProvider.emerIVlefshemVariable("aaa"));
            Assert.IsTrue(CodeProvider.emerIVlefshemVariable("aaa_aa"));
            Assert.IsTrue(CodeProvider.emerIVlefshemVariable("_aaa"));
            Assert.IsTrue(CodeProvider.emerIVlefshemVariable("_1aa"));
            Assert.IsTrue(CodeProvider.emerIVlefshemVariable("aaa11"));
            Assert.IsTrue(CodeProvider.emerIVlefshemVariable("_aaa11AA_"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa "));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa+"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa-"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa/"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa\\"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa?"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa!"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa*"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa<"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa>"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa,"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa."));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa;"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa:"));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa\""));
            Assert.IsTrue(!CodeProvider.emerIVlefshemVariable("aaa\'"));
        }
    }
}