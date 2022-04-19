using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbCore.DbRegjistrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore;
using DbCore.DbInventari;
using DbCore.DbAdmin;

namespace DbCoreTests.DbRegjistrim
{
    [TestClass]
    public class clsKokaShitjeTest
    {
        [TestMethod]
        public void GjeneroGaranciTest()
        {
            var art = new clsArtikulli
            {
                IdArtikulli = 56,
                Njesi1Artikulli = 21,
                Njesi2Artikulli = 36,
                KoeficientArtikulli = 100,
                IdllojGarancie = 45
            };
            clsKokaShitje koka = new clsKokaShitje
            {
                IdNivel = 0,
                DtDok = DateTime.Now,
                EmerKlienti = "nedjan",
                Kontakti = "45",
                IdNdermarrje = 564,
                IdStatusDok = 1,
                ColGaranci = new colGaranciArtikulli()
                
            };
            koka.OColTrupiShitje = new colTrupiShitje();
            koka.OColTrupiShitje.Add(new clsTrupiShitje
            {
                IdLlojVeprimi = 1,
                IdNjesia = 21,
                Sasia = 1,
                KodDetajim1 = "test",
                Element = art
            });

            var seriale = new colSerialeUnikeMagazina();
            seriale.Add(new clsSerialeUnikeAparate(0, 0, 0, "4545", art, null, true, 0));
            var seriale2 = new colSerialeUnikeMagazina();
            seriale2.Add(new clsSerialeUnikeAparate(0, 0, 0, "4545", art, null, true, 0));
            seriale2.Add(new clsSerialeUnikeAparate(0, 0, 0, "4546", art, null, true, 0));
            seriale2.Add(new clsSerialeUnikeKarta(0, 0, 0, "4545543", art, null, "", "", "", "", "", true, 0));
            var serialeKarta = new colSerialeUnikeMagazina();
            serialeKarta.Add(new clsSerialeUnikeKarta(0,0,0, "4545543",art,null,"","","","","",true,0));
            var serialeBosh = new colSerialeUnikeMagazina();


            Assert.IsTrue(koka.GjeneroGaranci(seriale, "nedjan").Status);
            Assert.IsTrue(koka.ColGaranci[0].Detajimi == "test");
            Assert.IsTrue(koka.ColGaranci.Count == 1);

            koka.ColGaranci.Clear();

            koka.EmerKlienti = "";
            Assert.IsFalse(koka.GjeneroGaranci(seriale, "nedjan").Status);
            koka.Kontakti = "";
            Assert.IsFalse(koka.GjeneroGaranci(seriale, "nedjan").Status);
            koka.IdStatusDok = 0;
            Assert.IsTrue(koka.GjeneroGaranci(seriale, "nedjan").Status);

            koka.IdStatusDok = 1;
            koka.EmerKlienti = "test";
            koka.Kontakti = "test";

            koka.OColTrupiShitje[0].IdNjesia = 36;
            Assert.IsTrue(koka.GjeneroGaranci(seriale, "nedjan").Status);
            Assert.IsTrue(koka.ColGaranci[0].Detajimi == "test");
            Assert.IsTrue(koka.ColGaranci.Count == 100);
            koka.ColGaranci.Clear();


            koka.OColTrupiShitje[0].IdNjesia = 21;
            koka.OColTrupiShitje[0].KodDetajim1 = null;

            Assert.IsTrue(koka.GjeneroGaranci(seriale, "nedjan").Status);
            Assert.IsTrue(koka.ColGaranci[0].Detajimi == "4545");
            Assert.IsTrue(koka.ColGaranci.Count == 1);
            koka.ColGaranci.Clear();

            koka.OColTrupiShitje[0].Sasia = 2;
            Assert.IsTrue(koka.GjeneroGaranci(seriale2, "nedjan").Status);
            Assert.IsTrue(koka.ColGaranci[0].Detajimi == "4545");
            Assert.IsTrue(koka.ColGaranci.Count == 2);
            koka.ColGaranci.Clear();

            koka.OColTrupiShitje[0].Sasia = 1;
            koka.OColTrupiShitje[0].KodDetajim1 = null;
            Assert.IsFalse(koka.GjeneroGaranci(null, "nedjan").Status);



        }
    }
}
