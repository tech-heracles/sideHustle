using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbCore.DbRegjistrim;

namespace DbCoreTest
{
    [TestClass]
    public class clsKlientMeMSISDNTest
    {
        [TestMethod]
        public void ValidoKlientMeMsisdn()
        {
            var klient1 = new clsKlientMeMSISDN()
            {
                Msisdn = "355669234567",
                KodiFitues = "YRHAO",
                StatusPerdorimi = false

            };
            var klient2 = new clsKlientMeMSISDN()
            {
                Msisdn = "",
                KodiFitues = "YRHAO",
                StatusPerdorimi = false

            };
            var klient3 = new clsKlientMeMSISDN()
            {
                Msisdn = "355669234567",
                KodiFitues = "YRHAO",
                StatusPerdorimi = true

            };
            Assert.IsTrue(klient1.Valido().Status);
            Assert.IsFalse(klient2.Valido().Status);
            Assert.IsFalse(klient3.Valido().Status);

        }

        [TestMethod]
        public void ValidoMsisdnTeReTest()
        {
            var klient1 = new clsKlientMeMSISDN()
            {
                MsisdnERe = ""
            };


            var klient2 = new clsKlientMeMSISDN()
            {
                MsisdnERe = "qwrtgg"
            };


            var klient3 = new clsKlientMeMSISDN()
            {
                MsisdnERe = "355695215741"
            };

            var klient4 = new clsKlientMeMSISDN()
            {
                MsisdnERe = "3556952165741"
            };


            var klient5 = new clsKlientMeMSISDN()
            {
                MsisdnERe = "06952165741"
            };
            Assert.IsTrue(klient3.ValidoMsisdnTeRe().Status);
            Assert.IsFalse(klient1.ValidoMsisdnTeRe().Status);
            Assert.IsFalse(klient2.ValidoMsisdnTeRe().Status);
            Assert.IsFalse(klient4.ValidoMsisdnTeRe().Status);
            Assert.IsFalse(klient5.ValidoMsisdnTeRe().Status);
        }
    }
}
