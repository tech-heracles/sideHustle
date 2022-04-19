using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbCore.DbKontabiliteti;

namespace DbCoreTests.DbKontabiliteti
{
    [TestClass]
    public class clsKlientfurnitorTest: FakeHttpContextBase
    {
        [TestMethod]
        public void EshteNdermarrjeOwnNdermarrjaEKlientit()
        {
          //  Assert.IsTrue(clsKlientFurnitor.eshteNdermarrjeKlientiOwn(101251));
            Assert.IsFalse(clsKlientFurnitor.eshteNdermarrjeKlientiOwn(101212));
            Assert.IsFalse(clsKlientFurnitor.eshteNdermarrjeKlientiOwn(1644532));
        }
    }
}
