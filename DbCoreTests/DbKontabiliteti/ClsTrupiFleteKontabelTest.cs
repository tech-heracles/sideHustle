using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DbCore.DbKontabiliteti;

namespace DbCoreTests.DbKontabiliteti
{
    [TestClass]
    public class ClsTrupiFleteKontabelTest : FakeHttpContextBase
    {
        [TestMethod]
        public void ValidoLlogarineTest()
        {
            try
            {
                var obj = new PrivateObject(new clsTrupiFleteKontabel());
                object[] objects = { new clsLlogari
                {
                    IdLlogari = 87918,
                    Aktiv = true
                }};
                obj.Invoke("ValidoLlogarine", objects);
            }
            catch (Exception myEx)
            {
                Assert.Fail($"Nuk duhet te hidhte exception {myEx}");
            }
        }

        [TestMethod]
        public void ValidoMonedhenTest()
        {
            try
            {
                var obj = new PrivateObject(new clsTrupiFleteKontabel());
                const string monedha = "LEK";
                object[] objects = { monedha, 564 };
                obj.Invoke("ValidoMonedhen", objects);
            }
            catch (Exception myEx)
            {
                Assert.Fail($"Nuk duhet te hidhte exception {myEx}");
            }
        }
    }
}
