using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore;
using DbCore.IMBUtils.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbCoreTests.UtilsTest
{
    [TestClass]
    public class PasswordTest
    {
        [TestMethod]
        public void IsPasswordGeneratedOk()
        {
            int posProblematik;
            Assert.IsTrue(clsStrongPassword.IsDangerousString("yo1h<naG", out posProblematik));
        }
        [TestMethod]
        public void MerrPassTeVlefshem()
        {

            var pass = clsStrongPassword.GjeneroPasswordTeVlefshem(8, 1, 1, 1);
            Assert.AreEqual(8, pass.Length);
        }
    }
}
