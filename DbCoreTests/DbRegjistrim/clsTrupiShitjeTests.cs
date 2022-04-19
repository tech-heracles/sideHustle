using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbCore.DbRegjistrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore;

namespace DbCoreTests.DbRegjistrim
{
    [TestClass]
    public class clsTrupiShitjeTests
    {
        [TestMethod]
        public void KontrolloZbritjeAnalitikeTest()
        {

            var trupiShitje = new clsTrupiShitje() { Zbritje = 150 };

            var obj = new PrivateObject(trupiShitje);

            try
            {
                obj.Invoke("KontrolloZbritjeAnalitike", new[] { "Jo" });
                Assert.Fail("Duhet te hidhte exception sepse vlera eshte e pa lejuar!");
            }
            catch (MyException myEx)
            {
                Assert.IsTrue(true);
            }

        }

        [TestMethod]
        public void KontrolloZbritjeAnalitikeTest1()
        {

            var trupiShitje = new clsTrupiShitje() { Zbritje = -10 };

            var obj = new PrivateObject(trupiShitje);

            try
            {
                obj.Invoke("KontrolloZbritjeAnalitike", new[] { "Jo" });
                Assert.Fail("Duhet te hidhte exception sepse vlera eshte e pa lejuar!");
            }
            catch (MyException myEx)
            {
                Assert.IsTrue(true);
            }

        }

        [TestMethod]
        public void KontrolloZbritjeAnalitikeTest2()
        {
            var trupiShitje = new clsTrupiShitje() { Zbritje = 50 };

            var obj = new PrivateObject(trupiShitje);

            try
            {
                obj.Invoke("KontrolloZbritjeAnalitike", new[] { "Jo" });
            }
            catch (MyException myEx)
            {
                Assert.Fail($"Nuk duhet te hidhte exception {myEx.ToString()}");
            }
        }

        [TestMethod]
        public void KontrolloZbritjeAnalitikeTest3()
        {
            var trupiShitje = new clsTrupiShitje() { Zbritje = 120 };

            var obj = new PrivateObject(trupiShitje);

            try
            {
                obj.Invoke("KontrolloZbritjeAnalitike", new[] { "Po" });
                Assert.Fail("Duhet te hidhte exception sepse vlera eshte e pa lejuar!");
            }
            catch (MyException myEx)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void KontrolloZbritjeAnalitikeTest4()
        {
            var trupiShitje = new clsTrupiShitje() { Zbritje = -120 };

            var obj = new PrivateObject(trupiShitje);

            try
            {
                obj.Invoke("KontrolloZbritjeAnalitike", new[] { "Po" });
                Assert.Fail("Duhet te hidhte exception sepse vlera eshte e pa lejuar!");
            }
            catch (MyException myEx)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void KontrolloZbritjeAnalitikeTest5()
        {
            var trupiShitje = new clsTrupiShitje() { Zbritje = 50 };

            var obj = new PrivateObject(trupiShitje);

            try
            {
                obj.Invoke("KontrolloZbritjeAnalitike", new[] { "Po" });
            }
            catch (MyException myEx)
            {
                Assert.Fail($"Nuk duhet te hidhte exception {myEx.ToString()}");
            }
        }
    }
}