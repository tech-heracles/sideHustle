using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.DbListPagesat;
namespace DbCoreTests.DbListPagesat
{
    [TestClass]
    public class KomponenteListpageseTest:FakeHttpContextBase
    {

        [TestMethod]
        public void ValidoData()
        {
            var data = new List<DateTime>
            {
                new DateTime(2017,2,1),
                new DateTime(2017,2,2),
                new DateTime(2016,12,1),
                new DateTime(2016,1,2),
                new DateTime(2017, 1, 1)
            };
            var dtAktivizimi = new DateTime(2017, 1, 1);
            var dtPasardhesePritur = new DateTime(2017, 2, 1);
            Assert.AreEqual(dtPasardhesePritur, DbCore.DbListPagesat.Utils.MerrDatenMeTeAfertPasardhese(dtAktivizimi, data));
        }

    }
}
