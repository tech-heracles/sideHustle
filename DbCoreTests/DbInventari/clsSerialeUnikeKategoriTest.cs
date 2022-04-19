using DbCore;
using DbCore.DbInventari;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbCoreTests.DbInventari
{
    [TestClass]
    public class clsSerialeUnikeKategoriTest : FakeHttpContextBase
    {
        [TestMethod]
        public void ruajSerialetUnikeVodafoneAparate()
        {
            clsMesazh mesazh = new MesazhGabimi();
            clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori("APARATE", "Aparate Vodafone", "txt", "|", 3655, 564, 1, "", false);
            //kategori.shtoSerialUnik("IMEI");
            kategori.ColSerialeFusha.merrTegjithaFushat();         
            if (!kategori.ekziston())
                mesazh = kategori.Ruaj();
            else
                mesazh = new MesazhSuksesi();
            Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void ruajSerialetUnikeVodafoneKarta()
        {
            clsMesazh mesazh = new MesazhGabimi();
            clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori("KARTA", "Karta Vodafone", "txt", "|", 3655, 564, 1, "", false);
            //kategori.shtoSerialUnik("MSISDN");
            //kategori.shtoSerialUnik("SERIAL");
            colSerialeUnikeFusha colFushat = new colSerialeUnikeFusha();
            clsSerialeUnikeFusha fusha = new clsSerialeUnikeFusha();
            fusha.Fusha = "Serial 1";
            fusha.Emertimi = "SERIAL KARTA";
            fusha.MerrFusheSipasEmritFushes();
            colFushat.Add(fusha);
            kategori.ColSerialeFusha = colFushat;

            if (!kategori.ekziston())
                mesazh = kategori.Ruaj();
            else
                mesazh = new MesazhSuksesi();
            Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void ruajSerialetUnikeVodafoneTEST()
        {
            //clsMesazh mesazh = new MesazhGabimi();
            //clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori("TEST", "TEST", "txt", "|", 3655, 564, 1, true, "");
            //kategori.shtoSerialUnik("MSISDN");
            //kategori.shtoSerialUnik("SERIAL");
            //kategori.ColSerialeFusha.merrTegjithaFushat();
            //if (!kategori.ekziston())
            //    mesazh = kategori.Ruaj();
            //else
            //    mesazh = kategori.Modifiko();
            //Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void mbushGjitheKategoriVodafone()
        {
            colSerialeUnikeKategori kategori = new colSerialeUnikeKategori(564);
            Assert.IsNotNull(kategori);
        }

        //[TestMethod()]
        //public void shtoSerialNeKategori()
        //{
        //    clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori("APARATE", 564);
        //    kategori.ColSeriale.mbushSipasFormatit(kategori.ID);
        //    int nrParaShtimi = kategori.ColSeriale.Count;
        //    kategori.shtoSerialUnik("IMEI");
        //    Assert.AreEqual(nrParaShtimi, kategori.ColSeriale.Count, "Shtimi nuk duhet te ndodhte!");
        //}


        [TestMethod]
        public void shtoFushaNeKategori()
        {
            clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori("APARATE", 564);
            kategori.ColSerialeFusha.mbushFushaSipasKategorise(kategori.ID);
            Assert.IsNotNull(kategori);
        }

        //[TestMethod]
        //public void shtoSerialNeKategoriTrue()
        //{
        //    clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori("APARATE", 564);
        //    kategori.ColSeriale.mbushSipasFormatit(kategori.ID);
        //    int nrParaShtimi = kategori.ColSeriale.Count;
        //    kategori.shtoSerialUnik("MSISDN");
        //    Assert.AreEqual(nrParaShtimi + 1, kategori.ColSeriale.Count, "Shtimi duhet te ndodhte!");
        //}

        //[TestMethod]
        //public void hiqSerialNeKategori()
        //{
        //    clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori("APARATE", 564);
        //    kategori.ColSeriale.mbushSipasFormatit(kategori.ID);
        //    int nrParaShtimi = kategori.ColSeriale.Count;
        //    kategori.hiqSerialUnik("MSISDN");
        //    Assert.AreEqual(nrParaShtimi, kategori.ColSeriale.Count, "Heqja nuk duhet te ndodhte!");
        //}

        //[TestMethod]
        //public void hiqSerialNeKategoriTrue()
        //{
        //    clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori("APARATE", 564);
        //    kategori.ColSeriale.mbushSipasFormatit(kategori.ID);
        //    int nrParaShtimi = kategori.ColSeriale.Count;
        //    kategori.hiqSerialUnik("IMEI");
        //    Assert.AreEqual(nrParaShtimi - 1, kategori.ColSeriale.Count, "Heqja duhet te ndodhte!");
        //}

        [TestMethod]
        public void modifikoKategoriVodafone()
        {
            //clsMesazh mesazh = new MesazhGabimi();
            //clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori("TEST", 564);
            //kategori.ColSeriale.mbushSipasKategorise(kategori.ID);
            //kategori.hiqSerialUnik("MSISDN");
            //kategori.shtoSerialUnik("IMEI");
            //mesazh = kategori.Modifiko();
            //Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void fshiKategoriVodafone()
        {
            clsMesazh mesazh = new MesazhGabimi();
            clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori("TEST", 564);
            mesazh = kategori.Fshi();
            Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod, TestCategory("Gabim")]
        public clsSerialeUnikeKategori ktheKategoriSipasSerialitUnik(string serialKod, int idNdermarrje)
        {
            if (serialKod == "")
                serialKod = "356257071585331";
            if (idNdermarrje == 0)
                idNdermarrje = 564;

            int idFormati;
            colSerialeUnikeKategori serialeKategori = new colSerialeUnikeKategori(idNdermarrje);
            clsSerialeUnikeKategori kategori = serialeKategori.merrKategoriSipasSerialitUnik(serialKod, out idFormati);
            return kategori;
        }
    }
}
