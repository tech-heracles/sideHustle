using DbCore;
using DbCore.DbInventari;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbCoreTests.DbInventari
{
    [TestClass]
    public class clsSerialeUnikeFormateTest : FakeHttpContextBase
    {
        [TestMethod]
        public void ruajSerialetUnikeVodafoneAparate()
        {
            clsMesazh mesazh = new MesazhGabimi();
            clsSerialeUnikeFormate format = new clsSerialeUnikeFormate("APARATE", "Aparate Vodafone", 10, 3655, 564, 1, true);
            format.shtoSerialUnik("IMEI");   
            if (!format.ekziston())
                mesazh = format.Ruaj();
            else
                mesazh = new MesazhSuksesi();
            Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void ruajSerialetUnikeVodafoneKarta()
        {
            clsMesazh mesazh = new MesazhGabimi();
            clsSerialeUnikeFormate format = new clsSerialeUnikeFormate("KARTA", "Karta Vodafone", 2, 3655, 564, 1, true);
            format.shtoSerialUnik("MSISDN");
            format.shtoSerialUnik("SERIAL");

            if (!format.ekziston())
                mesazh = format.Ruaj();
            else
                mesazh = new MesazhSuksesi();
            Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void ruajSerialetUnikeVodafoneTEST()
        {
            clsMesazh mesazh = new MesazhGabimi();
            clsSerialeUnikeFormate format = new clsSerialeUnikeFormate("TEST", "TEST", 1, 3655, 564, 1, true);
            format.shtoSerialUnik("MSISDN");
            format.shtoSerialUnik("SERIAL");
            if (!format.ekziston())
                mesazh = format.Ruaj();
            else
                mesazh = format.Modifiko();
            Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void mbushGjitheFormateVodafone()
        {
            colSerialeUnikeFormate format = new colSerialeUnikeFormate(564);
            Assert.IsNotNull(format);
        }

        [TestMethod()]
        public void shtoSerialNeFormat()
        {
            clsSerialeUnikeFormate format = new clsSerialeUnikeFormate("APARATE", 564);
            format.ColSeriale.mbushSipasFormatit(format.ID);
            int nrParaShtimi = format.ColSeriale.Count;
            format.shtoSerialUnik("IMEI");
            Assert.AreEqual(nrParaShtimi, format.ColSeriale.Count, "Shtimi nuk duhet te ndodhte!");
        }

        [TestMethod]
        public void shtoSerialNeFormatTrue()
        {
            clsSerialeUnikeFormate format = new clsSerialeUnikeFormate("APARATE", 564);
            format.ColSeriale.mbushSipasFormatit(format.ID);
            int nrParaShtimi = format.ColSeriale.Count;
            format.shtoSerialUnik("MSISDN");
            Assert.AreEqual(nrParaShtimi + 1, format.ColSeriale.Count, "Shtimi duhet te ndodhte!");
        }

        [TestMethod]
        public void hiqSerialNeFormat()
        {
            clsSerialeUnikeFormate format = new clsSerialeUnikeFormate("APARATE", 564);
            format.ColSeriale.mbushSipasFormatit(format.ID);
            int nrParaShtimi = format.ColSeriale.Count;
            format.hiqSerialUnik("MSISDN");
            Assert.AreEqual(nrParaShtimi, format.ColSeriale.Count, "Heqja nuk duhet te ndodhte!");
        }

        [TestMethod]
        public void hiqSerialNeFormatTrue()
        {
            clsSerialeUnikeFormate format = new clsSerialeUnikeFormate("APARATE", 564);
            format.ColSeriale.mbushSipasFormatit(format.ID);
            int nrParaShtimi = format.ColSeriale.Count;
            format.hiqSerialUnik("IMEI");
            Assert.AreEqual(nrParaShtimi - 1, format.ColSeriale.Count, "Heqja duhet te ndodhte!");
        }

        [TestMethod]
        public void modifikoFormatVodafone()
        {
            //clsMesazh mesazh = new MesazhGabimi();
            //clsSerialeUnikeFormate format = new clsSerialeUnikeFormate("TEST", 564);
            //format.ColSeriale.mbushSipasFormatit(format.ID);
            //format.hiqSerialUnik("MSISDN");
            //format.shtoSerialUnik("IMEI");
            //mesazh = format.Modifiko();
            //Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void fshiFormatVodafone()
        {
            clsMesazh mesazh = new MesazhGabimi();
            clsSerialeUnikeFormate format = new clsSerialeUnikeFormate("TEST", 564);
            mesazh = format.Fshi();
            Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        //[TestMethod,TestCategory("Gabim")]
        //public clsSerialeUnikeFormate ktheFormatSipasSerialitUnik(string serialKod, int idNdermarrje)
        //{
        //    if (serialKod == "") serialKod = "356257071585331";
        //    if (idNdermarrje == 0) idNdermarrje = 564;

        //    colSerialeUnikeFormate serialeFormat = new colSerialeUnikeFormate(idNdermarrje);
        //    clsSerialeUnikeFormate formati = serialeFormat.merrFormatSipasSerialitUnik(serialKod);
        //    return formati;
        //}
    }
}
