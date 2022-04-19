using DbCore.DbInventari;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Text.RegularExpressions;

namespace DbCoreTests.DbInventari
{
    [TestClass]
    public class clsSerialeUnikeMagazinaTest : FakeHttpContextBase
    {
        [TestMethod]
        public void skanimAparatiPerVeprimMagazine()
        {
            string skanimAparati = "356257071585331";
            int idNdermarrje = 564;
            clsSerialeUnikeKategoriTest testSkanimAparati = new clsSerialeUnikeKategoriTest();
            clsSerialeUnikeKategori kategori = testSkanimAparati.ktheKategoriSipasSerialitUnik(skanimAparati, idNdermarrje);
            Assert.AreEqual(kategori.Kategori, "APARATE", "Kategoria duhet te ishte Aparate!");
        }

        [TestMethod]
        public void krijimTrupiNgaBlerjaAparate()
        {
            string serialiKryesore = "356257071585331", serialiDytesor = "";
            string artikulli = "set", artikullSet = "1", cardSerialNo = "", phoneSerialNo = "", usercode = "", airtime = "", shitBatch = "", batchPerPack = "", cardsPerBatch = "", cardPartNo = "";
            int idNdermarrje = 564;
            int idTrupi = 3964, idKoka = 50982, idLlojDokumentiMagazine = 1;
            float cmimi = 500;
            colSerialeUnikeMagazina serialetNeMagazine = new colSerialeUnikeMagazina();
            colSerialeUnikeKategori serialeKategori = new colSerialeUnikeKategori(idNdermarrje);
            KrijuesKategoriSerialesh.MbushKategoriSerialesh(serialeKategori, serialetNeMagazine, idNdermarrje, new clsArtikulli(artikulli,564),new clsArtikulli(artikullSet, 564), true, 0, false, serialiKryesore);
            serialetNeMagazine.shtoParametraMagazine(idTrupi, idKoka, idLlojDokumentiMagazine, cmimi);
            serialetNeMagazine.Ruaj();
        }
        [TestMethod]
        public void KrijoTrupSerialeshPerGride()
        {
            //var ser = new colSerialeUnikeMagazina();
            //ser.Add(new clsSerialeUnikeKarta(564, 2, "222222222222222222", new clsArtikulli(762788), null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));
            //ser.Add(new clsSerialeUnikeKarta(564, 2, "222222222222222223", new clsArtikulli(762788), null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));

            //var trupi = ser.NdertoTrupinEGrides(564);
            //var expected = new
            //{
            //    Seriali = "222222222222222222",
            //    Artikulli = "KARTE1",
            //    Seti = "",
            //    Kategoria = "KARTA",
            //    Sasia = 1
            //};
            //},
            //    new
            //    {
            //        Seriali = "222222222222222222",
            //        Artikulli = "KARTE1",
            //        Seti = "",
            //        Kategoria = "KARTA",
            //        Sasia = 1
            //    }
            //];
           // Assert.AreEqual(trupi, expected);
        }


        [TestMethod]
        public void KontrolliFifoPerSerialeUnike()
        {
            //string serial1 = clsSerialeUnikeMagazina.MerrSerialinEPareNeRradhe(762788, true, DateTime.Now, 564);
            //string serial2 = clsSerialeUnikeMagazina.MerrSerialinEPareNeRradhe(762788, true, new DateTime(2017, 8, 3), 564);
            //string serial3 = clsSerialeUnikeMagazina.MerrSerialinEPareNeRradhe(762788, false, new DateTime(2017, 8, 3), 564);
            //Assert.AreEqual(serial1, "111111111111111112");
            //Assert.AreEqual(serial2, "111111111111111112");
            //Assert.AreEqual(serial3, "0695215742");
        }
        [TestMethod]
        public void KontrolloMatchRegex()
        {
            string s = "0695215741";
            string s2 = "06952157411";
            string zerogjashtenente = @"^069\d{7}$";
            string pesembedhjeteshifror = @"^\d{15}$";
            string packSerialNumberRegEx = @"^\s*(\d{6}-00(0[0-9]|10)-00(0[0-9]|10))\z";

            Assert.IsTrue(Regex.Match(s, zerogjashtenente).Success);
            Assert.IsFalse(Regex.Match(s2, zerogjashtenente).Success);
            Assert.IsTrue(Regex.Match("012345678910112", pesembedhjeteshifror).Success);
            Assert.IsFalse(Regex.Match("0123456789101122323", pesembedhjeteshifror).Success);
            Assert.IsTrue(Regex.Match("111111-0001-0009", packSerialNumberRegEx).Success);
            Assert.IsFalse(Regex.Match("111111-0001-0009-Nedjan", packSerialNumberRegEx).Success);
            Assert.IsFalse(Regex.Match("111111-0001-000956", packSerialNumberRegEx).Success);

        }
        [TestMethod]
        public void Compute()
        {
            DataTable dt = new DataTable();
            var result = dt.Compute("13 > 12", "");
            Assert.IsTrue((bool)result);
            try
            {
                dt.Compute("13 < nedjan", "");
                Assert.Fail();
            }
            catch(Exception ex)
            {
                
            }

            Assert.IsFalse((bool)dt.Compute("13 < 12", ""));
            
        }

        [TestMethod]
        public void TestoValidimSeriali()
        {
            colSerialeUnikeKategori serialeKategori = new colSerialeUnikeKategori(564);//duhet kaluar si parameter nga ku do therritet metoda qe mos mbushet nga databaza aq here sa seriale kemi
            int idFormati;
            clsSerialeUnikeKategori kategoriSeriali = serialeKategori.merrKategoriSipasSerialitUnik("123456789101213", out idFormati);
            Assert.AreEqual(kategoriSeriali.Kategori, "APARATE");
            try
            {
                serialeKategori.merrKategoriSipasSerialitUnik("1234567891012136", out idFormati);
                Assert.Fail();
            }catch(Exception ex)
            {

            }
            Assert.AreEqual(serialeKategori.merrKategoriSipasSerialitUnik("123456789101213123", out idFormati).Kategori, "KARTA");
        }
    }
}
