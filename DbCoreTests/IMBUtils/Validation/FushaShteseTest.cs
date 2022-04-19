using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbCoreTests.IMBUtils.Validation
{
    [TestClass]
    public class FushaShteseTest
    {
        [TestMethod]
        public void RegexTestGjatsiFjale()
        {


            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch("sdaadsd", @"^(?!^.{5}).+"));
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch("s d a ", @"^(?!^.{5}).+"));
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch("", @"^(?!^.{5}).+"));
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch("      ", @"^(?!^.{5}).+"));
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch("    23", @"^(?!^.{5}).+"));
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch("", @"^(?!^.{5}).+"));
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(" k    ", @"^(?!^.{5}).+"));

        }
    }
}
