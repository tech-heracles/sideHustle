using DbCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbCoreTests.IMBUtils
{
    [TestClass]
    public class clsFunksioneTests
    {
        [TestMethod]
        public void GetKomponenteTest()
        {
            string komponente1 = @"/Shto_RegjistrimDokumentash.aspx?scopeID=ee8ab097-ca76-487d-b5b3-c289bc76a978&shitje_blerje=shitje&shtim_modifikim=shtim";
            string komponente2 = @"/Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&shtim_modifikim=shtim&scopeID=ee8ab097-ca76-487d-b5b3-c289bc76a978";
            string komponente3 = @"/Shto_RegjistrimDokumentash.aspx?scopeID=ee8ab097-ca76-487d-b5b3-c289bc76a978";
            string komponente4 = @"/Shto_RegjistrimDokumentash.aspx";
            string komponente5 = @"/Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje";
            string komponente6 = @"/Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&shtim_modifikim=shtim";

            string expected = "Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje";
            string expected2 = "Shto_RegjistrimDokumentash.aspx";
            Assert.AreEqual(clsFunksione.GetKomponente(komponente1, true), expected);
            Assert.AreEqual(clsFunksione.GetKomponente(komponente2, true), expected);
            Assert.AreEqual(clsFunksione.GetKomponente(komponente3, true), expected2);
            Assert.AreEqual(clsFunksione.GetKomponente(komponente4, true), expected2);
            Assert.AreEqual(clsFunksione.GetKomponente(komponente5, true), expected);
            Assert.AreEqual(clsFunksione.GetKomponente(komponente6, true), expected);


            Assert.AreEqual(clsFunksione.GetKomponente(komponente1, false), expected2);
            Assert.AreEqual(clsFunksione.GetKomponente(komponente2, false), expected2);
            Assert.AreEqual(clsFunksione.GetKomponente(komponente3, false), expected2);
            Assert.AreEqual(clsFunksione.GetKomponente(komponente4, false), expected2);
            Assert.AreEqual(clsFunksione.GetKomponente(komponente5, false), expected2);
            Assert.AreEqual(clsFunksione.GetKomponente(komponente6, false), expected2);

            try
            {
                clsFunksione.GetKomponente("", false);
                Assert.Fail("Duhet te deshtonte kur url eshte bosh");

                clsFunksione.GetKomponente("Shto_RegjistrimDokumentash.aspx", false);
                Assert.Fail("Duhet te deshtonte kur url nuk nis me //");
            }
            catch
            {
            }
        }

        [TestMethod()]
        public void validoKtheVlereBool()
        {
            string vlera = "po";
            string vlera2 = "checked";
            string vlera3 = "true";
            string vlera4 = "jo";
            
            Assert.AreEqual(clsFunksione.ktheVlereBool(vlera), true);
            Assert.AreEqual(clsFunksione.ktheVlereBool(vlera2), true);
            Assert.AreEqual(clsFunksione.ktheVlereBool(vlera3), true);
            Assert.AreEqual(clsFunksione.ktheVlereBool(vlera4), false);
        }
    }
}
