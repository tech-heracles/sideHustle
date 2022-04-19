using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbCore.DbKontabiliteti;
using System;
using System.Collections.Generic;
using DbCore.DbQendraKosto;
using DbCore.DbShare;

namespace DbCoreTests.DbKontabiliteti
{
    [TestClass]
    public class ClsKokaFleteKontabelTest
    {
        [TestMethod]
        public void MerrFleteSipasIdGjeneruesDheKonfigGjeneruesTest()
        {
            const int iddok = 29222;
            const int idkonf = 16937;
            var kokaFleteKontabel = new clsKokaFleteKontabel();
            Assert.IsTrue(kokaFleteKontabel.MerrFleteSipasIdGjeneruesDheKonfigGjenerues(iddok, idkonf, new clsDatabaseKontabilitet()));
        }

        [TestMethod]
        public void KrijoFleteTest()
        {
            const int idMondedha = 2895;
            const int idNdermarrje = 564;
            const int idGjuha = 0;
            string shfaqmesazhapolupe;
            const int statusDokumenti = 1;
            const int idNdervit = 21293;
            const string nrDokumenti = "A11A11";
            const string nrReference = "B11B11";
            var dtDok = DateTime.Today;
            var dtRegj = DateTime.Today;
            const int iDKonfigAmbient = 47274;
            const int idGrupKontabilizimi = 0;
            const string pershkrimi = "Flete Kontabel";
            const int idPerdorues = 3655;
            var colTrupi = new colTrupatFletetKontabel
            {
                new clsTrupiFleteKontabel(0, 0, 109, pershkrimi, idMondedha, 1, 100, 0, "LEK", "", 100, 0)
            };
            const int idPeriudha = 36586;
            var konfig = new clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod("FleteKontabel", idNdermarrje);
            var kokaQendraKosto = clsKokaQendraKosto.KrijoQenderRe(konfig, colTrupi, new colObjektivaKosto(), new List<double>(), new List<double>(), new List<int>(), statusDokumenti, out shfaqmesazhapolupe, idNdermarrje, idGjuha, "shtim", 0, pershkrimi, dtDok, DateTime.Today, idNdervit, idPerdorues, nrDokumenti);
            var koka = new clsKokaFleteKontabel();
            var mesazh = koka.KrijoFlete(1, idNdervit, nrDokumenti, nrReference, dtDok, dtRegj, iDKonfigAmbient, idGrupKontabilizimi,
                pershkrimi, idPerdorues, colTrupi, true, 5, 5, idPeriudha, idNdermarrje, kokaQendraKosto);
            Assert.IsTrue(mesazh.Status);
        }

        [TestMethod]
        public void EshteILidhurTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void MerrIdsDokLidhurTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GjeneroNrReferenceTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RuajTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void FshiFleteKontabelTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ModifikoFleteKontabelGrupKontabilizimiTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GjeneroKontabilizimFleteAzhornimTest()
        {
            Assert.Fail();
        }
    }
}
