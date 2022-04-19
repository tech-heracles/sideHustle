using DbCore;
using DbCore.IMBUtils.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DbCoreTests.IMBUtils.Validation
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void RemoveSpacesTest()
        {
            string text_1 = " alphaWeb ";
            string text_2 = "alpha  Web ";
            string text_3 = "232 alpha web  no spaces  ";
            string text_4 = " alpha \n  web";

            Assert.AreEqual(text_1.RemoveSpaces(), "alphaWeb");
            Assert.AreEqual(text_2.RemoveSpaces(), "alpha  Web");
            Assert.AreEqual(text_3.RemoveSpaces(), "232 alpha web  no spaces");
            Assert.AreEqual(text_4.RemoveSpaces(), "alpha   web");
        }
    }
}