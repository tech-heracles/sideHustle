using DbCore.DbAsete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCoreTests.DbListPagesat
{
    [TestClass()]
    public class clsRezervaNormaAmortizimiTest:FakeHttpContextBase
    {
        [TestMethod()]
        public void clsRezervaNormaAmortizimi()
        {
            clsNormaAmortizimiRezerva rezerva = new clsNormaAmortizimiRezerva();
          //  rezerva.merrArtikullNormaAmortizimiSipasIDArtikullStandart(598609, 412, DateTime.Now);
        }
    }
}
