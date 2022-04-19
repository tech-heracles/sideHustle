using DbCore;
using DbCore.DbInventari;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DbCoreTests.DbInventari
{
    [TestClass]
    public class clsSerialeUnikeFushaTest : FakeHttpContextBase
    {

        [TestMethod]
        public void mbushGjitheFushaImporti()
        {
            try
            {
                colSerialeUnikeFusha fusha = new colSerialeUnikeFusha();
                fusha.merrTegjithaFushat();
            } catch (Exception ex)
            {
                Assert.Fail();
            }
        }
        

        [TestMethod]
        public void merrFusheImportiSipasEmrit()
        {
            clsSerialeUnikeFusha fusha = new clsSerialeUnikeFusha();
            fusha.Fusha = "Serial 1";
            fusha.MerrFusheSipasEmritFushes();
            Assert.IsNotNull(fusha.ID);
        }
    }
}
