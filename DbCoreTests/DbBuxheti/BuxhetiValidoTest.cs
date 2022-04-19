using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbCore.DbBuxheti;
using System;
using DbCoreTests.Fake;
using System.Collections.Generic;
using System.Data;
using DbCore;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCoreTests.Buxheti.Valido
{
    [TestClass()]
    public class BuxhetiValidoTest : FakeHttpContextBase
    {
        
        [TestMethod()]
        public void TestValidoVleraAnalizeBuxhetimi()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();
            //arrange
            var kategoriBuxhetimi0 = new ClsBKategoriBuxhetimi(new FakeMessages(), 0, "6581235451", "Paga", "", 564, 0, 1, null, null, 0, true, true);
            var kategoriBuxhetimi1 = new ClsBKategoriBuxhetimi(new FakeMessages(), 0, "6581 235451", "Paga", "", 564, 0, 1, null, null, 0, true, true);
            var kategoriBuxhetimi2 = new ClsBKategoriBuxhetimi(new FakeMessages(), 0, "6581235451  ", "Paga", "", 564, 0, 1, null, null, 0, true, true);
            var kategoriBuxhetimi3 = new ClsBKategoriBuxhetimi(new FakeMessages(), 0, "65812;;;;35451", "Paga", "", 564, 0, 1, null, null, 0, true, true);
            var kategoriBuxhetimi4 = new ClsBKategoriBuxhetimi(new FakeMessages(), 0, "6581235451", "Paga", "", 564, 0, 2, null, 5, 0, true, true);
            var kategoriBuxhetimi5 = new ClsBKategoriBuxhetimi(new FakeMessages(), 0, "6581235451", "Paga", "", 564, 0, 2, null, Convert.ToDecimal(0.5), 0, true, true);
            var kategoriBuxhetimi6 = new ClsBKategoriBuxhetimi(new FakeMessages(), 0, "6581235451", "Paga;'", "", 564, 0, 2, null, Convert.ToDecimal(0.5), 0, true, true);
            var kategoriBuxhetimi7 = new ClsBKategoriBuxhetimi(new FakeMessages(), 0, "6581235451", "Paga", "Paga;'", 564, 0, 2, null, Convert.ToDecimal(0.5), 0, true, true);
            var kategoriBuxhetimi8 = new ClsBKategoriBuxhetimi(new FakeMessages(), 0, "", "Paga", "", 564, 0, 1, null, null, 0, true, true);
            var kategoriBuxhetimi9 = new ClsBKategoriBuxhetimi(new FakeMessages(), 0, "6581235451", "", "", 564, 0, 1, null, null, 0, false, true);
            //assert
            Assert.AreEqual(kategoriBuxhetimi0.ValidoProperty().Status, true);
            Assert.AreEqual(kategoriBuxhetimi1.ValidoProperty().Status, false);
            Assert.AreEqual(kategoriBuxhetimi2.ValidoProperty().Status, false);
            Assert.AreEqual(kategoriBuxhetimi3.ValidoProperty().Status, false);
            Assert.AreEqual(kategoriBuxhetimi4.ValidoProperty().Status, false);
            Assert.AreEqual(kategoriBuxhetimi5.ValidoProperty().Status, true);
            Assert.AreEqual(kategoriBuxhetimi6.ValidoProperty().Status, false);
            Assert.AreEqual(kategoriBuxhetimi7.ValidoProperty().Status, false);
            Assert.AreEqual(kategoriBuxhetimi8.ValidoProperty().Status, false);
            Assert.AreEqual(kategoriBuxhetimi9.ValidoProperty().Status, true);
        }

        [TestMethod]
        public void TestValidoFshirjeAnalizeBuxhetimi()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();

            ClsBKategoriBuxhetimi kategoriBuxheti = new ClsBKategoriBuxhetimi();

            clsMesazh a = kategoriBuxheti.LejoFshirje(new FakeMessages(), true);
            clsMesazh b = kategoriBuxheti.LejoFshirje(new FakeMessages(), false);
            Assert.IsTrue(a.Status == false);
            Assert.IsTrue(b.Status == true);
        }
                        
        [TestMethod]
        public void TestValidoVleraKomponenteKalkuluese()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();

            clsMesazh b = ClsBKomponente.ValidoKomponente(new FakeMessages(), 0, "", "", 1, 1, true, null, null, null, false, true, 1, true, new clsMesazh(true));
            Assert.IsTrue(b.Status == true);
            //me te njejtin kod si nje ekzistues
            clsMesazh a = ClsBKomponente.ValidoKomponente(new FakeMessages(), 0, "", "", 1, 1, true, null, null, null, true, true, 1, true, new clsMesazh(true));
            Assert.IsTrue(a.Status == false);
            //kodi permban hapesira
            clsMesazh c = ClsBKomponente.ValidoKomponente(new FakeMessages(), 0, " ", "", 1, 1, true, null, null, null, true, true, 1, true, new clsMesazh(true));
            Assert.IsTrue(c.Status == false);
            //kodi permban karaktere speciale
            clsMesazh d = ClsBKomponente.ValidoKomponente(new FakeMessages(), 0, "123?.", "", 1, 1, true, null, null, null, true, true, 1, true, new clsMesazh(true));
            Assert.IsTrue(d.Status == false);
            //kodi permban thonjeza
            clsMesazh e = ClsBKomponente.ValidoKomponente(new FakeMessages(), 0, "gf'", "", 1, 1, true, null, null, null, true, true, 1, true, new clsMesazh(true));
            Assert.IsTrue(e.Status == false);
            //pershkimi permban hapesira
            clsMesazh f = ClsBKomponente.ValidoKomponente(new FakeMessages(), 0, "", " ", 1, 1, true, null, null, null, true, true, 1, true, new clsMesazh(true));
            Assert.IsTrue(f.Status == false);
            //pershkimi permban karaktere speciale
            clsMesazh g = ClsBKomponente.ValidoKomponente(new FakeMessages(), 0, "", "123?.", 1, 1, true, null, null, null, true, true, 1, true, new clsMesazh(true));
            Assert.IsTrue(g.Status == false);
            //pershkimi permban thonjeza
            clsMesazh h = ClsBKomponente.ValidoKomponente(new FakeMessages(), 0, "", "123'", 1, 1, true, null, null, null, true, true, 1, true, new clsMesazh(true));
            Assert.IsTrue(h.Status == false);
            //jane plotesuar vlerat max dhe min por mungon lloji i kufizimit
            clsMesazh i = ClsBKomponente.ValidoKomponente(new FakeMessages(), 0, "kodi", "pershkrimi", 1, 1, true, 1, 2, null, true, true, 1, true, new clsMesazh(true));
            Assert.IsTrue(i.Status == false);
            //vlera min eshte me e madhe se max
            clsMesazh o = ClsBKomponente.ValidoKomponente(new FakeMessages(), 0, "kodi", "pershkrimi", 1, 1, true, 2, 1, 1, true, true, 1, true, new clsMesazh(true));
            Assert.IsTrue(o.Status == false);
            //njesia e komponentes eshte formule dhe formula nuk eshte e sakte
            clsMesazh p = ClsBKomponente.ValidoKomponente(new FakeMessages(), 0, "kodi", "pershkrimi", 2, 1, true, 1, 2, 1, true, true, 1, true, new clsMesazh(false));
            Assert.IsTrue(p.Status == false);
            //nese po modifikohet duke u bere joaktive dhe eshte perdorur me pare ne formule
            clsMesazh t = ClsBKomponente.ValidoKomponente(new FakeMessages(), 1, "kodi", "pershkrimi", 2, 1, false, 1, 2, 1, true, true, 1, true, new clsMesazh(true));
            Assert.IsTrue(t.Status == false);
            //po tentohet të modifikohet një komponente ekzistuese duke iu ndryshuar tipi dhe është përdorur më parë në formulë ose në veprime
            clsMesazh r = ClsBKomponente.ValidoKomponente(new FakeMessages(), 1, "kodi", "pershkrimi", 2, 1, true, 1, 2, 1, true, true, 2, true, new clsMesazh(true));
            Assert.IsTrue(r.Status == false);

            //to continue
        }

        [TestMethod]
        public void TestValidoFshirjeKomponenteKalkuluese()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();

            clsMesazh a = ClsBKomponente.ValidoFshirje(new FakeMessages(), true, true, true);
            Assert.IsTrue(a.Status == false);

            clsMesazh b = ClsBKomponente.ValidoFshirje(new FakeMessages(), false, true, true);
            Assert.IsTrue(b.Status == false);

            clsMesazh c = ClsBKomponente.ValidoFshirje(new FakeMessages(), false, false, true);
            Assert.IsTrue(c.Status == false);

            clsMesazh d = ClsBKomponente.ValidoFshirje(new FakeMessages(), false, false, false);
            Assert.IsTrue(d.Status == true);            
        }

        [TestMethod()]
        public void TestValidoVleraLlojBuxheti()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();
            //arrange
            var buxheti0 = new ClsBLlojBuxheti(new FakeMessages(), 0, "6581235451", "Buxhet", 564);
            var buxheti1 = new ClsBLlojBuxheti(new FakeMessages(), 0, "6581 235451", "Buxhet", 564);
            var buxheti2 = new ClsBLlojBuxheti(new FakeMessages(), 0, "6581235451  ", "", 564);
            var buxheti3 = new ClsBLlojBuxheti(new FakeMessages(), 0, "65812;;;;35451", "Buxhet", 564);
            var buxheti4 = new ClsBLlojBuxheti(new FakeMessages(), 0, "6581235451", "Buxhe;'][t", 564);
            var buxheti5 = new ClsBLlojBuxheti(new FakeMessages(), 0, "", "Buxhet", 564);
            var buxheti6 = new ClsBLlojBuxheti(new FakeMessages(), 0, "6581235451", "", 564);

            //assert
            Assert.AreEqual(buxheti0.ValidoProperty().Status, true);
            Assert.AreEqual(buxheti1.ValidoProperty().Status, false);
            Assert.AreEqual(buxheti2.ValidoProperty().Status, false);
            Assert.AreEqual(buxheti3.ValidoProperty().Status, false);
            Assert.AreEqual(buxheti4.ValidoProperty().Status, false);
            Assert.AreEqual(buxheti5.ValidoProperty().Status, false);
            Assert.AreEqual(buxheti6.ValidoProperty().Status, false);
        }

        [TestMethod]
        public void TestValidoFshirjeLlojBuxheti()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();

            ClsBLlojBuxheti llojBuxheti = new ClsBLlojBuxheti(new FakeMessages());

            clsMesazh a = llojBuxheti.LejoFshirje(new FakeMessages(), true);
            clsMesazh b = llojBuxheti.LejoFshirje(new FakeMessages(), false);
            Assert.IsTrue(a.Status == false);
            Assert.IsTrue(b.Status == true);
        }
        
        [TestMethod]
        public void TestValidoSintakseNumerDokumentaBuxhetimi()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();
            //arrange
            var buxheti1 = new ClsBKokaBuxheti(0, 0, 0, "1", new DateTime(), 0, 0, 0, 0, 0, "", new DateTime(), new DateTime(), 0, 0, 0, 0, 0, "PB", 0, "", 0, 170, 0, 0, 0, DbCore.DbRegjistrim.StatusAprovimi.Undefined, "", 0, 0);
            var buxheti2 = new ClsBKokaBuxheti(0, 0, 0, "", new DateTime(), 0, 0, 0, 0, 0, "", new DateTime(), new DateTime(), 0, 0, 0, 0, 0, "PB", 0, "", 0, 170, 0, 0, 0, DbCore.DbRegjistrim.StatusAprovimi.Undefined, "", 0, 0);

            //assert
            var validim = new ClsBValidimBuxheti();
            Assert.IsTrue(validim.ValidoSintakseNrDok(new FakeMessages(), buxheti1.NrDok).Status);
            Assert.IsFalse(validim.ValidoSintakseNrDok(new FakeMessages(), buxheti2.NrDok).Status);
        }
        
        [TestMethod]
        public void TestKontrolloTotalePerDokumentBuxheti()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();
            ClsBValidimBuxheti buxhetiValidim = new ClsBValidimBuxheti();
            ClsBKokaBuxheti objBuxheti = new ClsBKokaBuxheti();

            objBuxheti.Totali = 1;
            objBuxheti.TotaliPaTvsh = 0;
            clsMesazh m1 = buxhetiValidim.kontrolloTotaletDokumentBuxheti(objBuxheti, 1, 0);
            clsMesazh m2 = buxhetiValidim.kontrolloTotaletDokumentBuxheti(objBuxheti, 1, 1);
            clsMesazh m5 = buxhetiValidim.kontrolloTotaletDokumentBuxheti(objBuxheti, 0, 1);
            objBuxheti.Totali = 0;
            clsMesazh m3 = buxhetiValidim.kontrolloTotaletDokumentBuxheti(objBuxheti, 0, 0);

            
            Assert.IsTrue(m1.Tipi == TipMesazhi.Gabim);
            Assert.IsTrue(m1.PershkrimMesazhi == "Totali i vleres me tvsh ne trup te dokumentin nuk eshte i barabarte me totalin e vleres me tvsh ne fund te dokumentit.");

            Assert.IsTrue(m2.Tipi == TipMesazhi.Gabim);
            Assert.IsTrue(m2.PershkrimMesazhi == "Totali i vleres pa tvsh ne trup te dokumentin nuk eshte i barabarte me totalin e vleres pa tvsh ne fund te dokumentit.");

            Assert.IsTrue(m3.Status == true);
            Assert.IsFalse(m3.PershkrimMesazhi == "Totali i vleres pa tvsh ne trup te dokumentin nuk eshte i barabarte me totalin e vleres pa tvsh ne fund te dokumentit.");

            Assert.IsTrue(m5.Tipi == TipMesazhi.Sukses);
            Assert.IsTrue(m5.PershkrimMesazhi != "Totali i vleres me tvsh ne trup te dokumentin nuk eshte i barabarte me shumen e totalit te vleres me tvsh me vleren e tvsh ne fund te dokumentit.");

        }

        [TestMethod]
        public void TestKontrolloTotalePerMiratimPlanifikimBuxheti()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();

            clsMesazh a = ClsBValidimBuxheti.kontrolloTotaletMiratimPlanifikimKtheMesazh(1, 1, 1, 1, 0);
            Assert.IsTrue(a.PershkrimMesazhi != "Buxheti i planifikuar ne koken e dokumentit nuk eshte i barabarte me totalin e planifikuar te rreshtave te trupit.");

            clsMesazh b = ClsBValidimBuxheti.kontrolloTotaletMiratimPlanifikimKtheMesazh(1, 2, 1, 1, 0);
            Assert.IsTrue(b.PershkrimMesazhi == "Buxheti i planifikuar ne koken e dokumentit nuk eshte i barabarte me totalin e planifikuar te rreshtave te trupit.");

            clsMesazh c = ClsBValidimBuxheti.kontrolloTotaletMiratimPlanifikimKtheMesazh(0, 0, 0, 0, 0);
            Assert.IsTrue(c.PershkrimMesazhi == "Kontrollet u kaluan me sukses.");

            clsMesazh d = ClsBValidimBuxheti.kontrolloTotaletMiratimPlanifikimKtheMesazh(2, 2, 1, 1, 0);
            Assert.IsTrue(d.PershkrimMesazhi == "Buxheti i planifikuar ne koken e dokumentit nuk eshte i barabarte me totalin e planifikuar te rreshtave te trupit.");

            clsMesazh e = ClsBValidimBuxheti.kontrolloTotaletMiratimPlanifikimKtheMesazh(2, 0, 1, 0, 0);
            Assert.IsTrue(e.PershkrimMesazhi == "Buxheti faktik ne koken e dokumentit nuk eshte i barabarte me totalin faktik te rreshtave te trupit.");
        }

        [TestMethod]
        public void TestKontrolloTotaleDokumentiSipasKushteve()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();

            clsMesazh a = ClsBValidimBuxheti.KtheMesazhSipasKushtit("negative", "", "", 0, false, false, false, 10, 10, 10);
            Assert.IsTrue(a.PershkrimMesazhi == "Totali i vlerave te trupit te dokumentit duhet te jete negativ");
            clsMesazh b = ClsBValidimBuxheti.KtheMesazhSipasKushtit("negative", "", "", -1, false, false, false, 10, 10, 10);
            Assert.IsTrue(b.PershkrimMesazhi == "Trupi i dokumentit nuk mund te kete rreshta me vlere zero");

            clsMesazh c = ClsBValidimBuxheti.KtheMesazhSipasKushtit("pozitive", "", "", 1, false, false, false, 10, 10, 10);
            Assert.IsTrue(c.PershkrimMesazhi == "Trupi i dokumentit nuk mund te kete rreshta me vlere zero");
            clsMesazh d = ClsBValidimBuxheti.KtheMesazhSipasKushtit("pozitive", "", "", 0, false, false, false, 10, 10, 10);
            Assert.IsTrue(d.PershkrimMesazhi == "Totali i vlerave te trupit te dokumentit duhet te jete pozitiv");

            clsMesazh e = ClsBValidimBuxheti.KtheMesazhSipasKushtit("0", "", "", 0, false, false, false, 10, 10, 10);
            Assert.IsTrue(e.PershkrimMesazhi == "Trupi i dokumentit nuk mund te kete rreshta me vlere zero");
            clsMesazh f = ClsBValidimBuxheti.KtheMesazhSipasKushtit("0", "", "", 1, false, false, false, 10, 10, 10);
            Assert.IsTrue(f.PershkrimMesazhi == "Totali i vlerave te trupit te dokumentit duhet te jete zero");
            clsMesazh g = ClsBValidimBuxheti.KtheMesazhSipasKushtit("0", "", "", 0, false, false, true, 10, 10, 10);
            Assert.IsTrue(g.PershkrimMesazhi == string.Empty);

            clsMesazh i = ClsBValidimBuxheti.KtheMesazhSipasKushtit("", "negative", "", 0, false, false, false, 10, 10, 10);
            Assert.IsTrue(i.PershkrimMesazhi == "Trupi i dokumentit duhet te kete vetem rreshta me vlera negative");
            clsMesazh j = ClsBValidimBuxheti.KtheMesazhSipasKushtit("", "negative", "", 0, true, false, false, 10, 10, 10);
            Assert.IsTrue(j.PershkrimMesazhi == string.Empty);

            clsMesazh l = ClsBValidimBuxheti.KtheMesazhSipasKushtit("", "pozitive", "", 0, false, false, false, 10, 10, 10);
            Assert.IsTrue(l.PershkrimMesazhi == "Trupi i dokumentit duhet te kete vetem rreshta me vlera pozitive");
            clsMesazh m = ClsBValidimBuxheti.KtheMesazhSipasKushtit("", "pozitive", "", 0, false, true, false, 10, 10, 10);
            Assert.IsTrue(m.PershkrimMesazhi == string.Empty);

            clsMesazh n = ClsBValidimBuxheti.KtheMesazhSipasKushtit("", "", "", 0, false, false, false, 10, 10, 10);
            Assert.IsTrue(n.PershkrimMesazhi == "Trupi i dokumentit nuk mund te kete rreshta me vlere zero");
            clsMesazh v = ClsBValidimBuxheti.KtheMesazhSipasKushtit("", "", "", 0, false, false, true, 10, 10, 10);
            Assert.IsTrue(v.PershkrimMesazhi == string.Empty);

            clsMesazh x = ClsBValidimBuxheti.KtheMesazhSipasKushtit("", "", "po", 0, false, false, true, 10, 10, 9);
            Assert.IsTrue(x.PershkrimMesazhi == "Trupi i dokumentit duhet te kete te njejten analize ne te gjithe rreshtat.");
            clsMesazh z = ClsBValidimBuxheti.KtheMesazhSipasKushtit("", "", "po", 0, false, false, true, 10, 10, 10);
            Assert.IsTrue(z.PershkrimMesazhi == string.Empty);

            clsMesazh q = ClsBValidimBuxheti.KtheMesazhSipasKushtit("", "", "jo", 0, false, false, true, 10, 10, 9);
            Assert.IsTrue(q.PershkrimMesazhi == "Trupi i dokumentit nuk duhet te kete te njejten analize ne disa rreshta.");
            clsMesazh w = ClsBValidimBuxheti.KtheMesazhSipasKushtit("", "", "jo", 0, false, false, true, 10, 10, 10);
            Assert.IsTrue(w.PershkrimMesazhi == string.Empty);
        }
        
        [TestMethod]
        public void TestKontrolloVleratSipasMuajve()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();

            var buxhetiValidim = new ClsBValidimBuxheti();
            var muajiA1 = new ClsBMuajt(); muajiA1.Janar = (decimal)41.6666666666667 + (decimal)41.6666666666667;
            var muajiA2 = new ClsBMuajt(); muajiA2.Janar = (decimal)83.3333333333333;
            var muajiB1 = new ClsBMuajt(); muajiB1.Shkurt = (decimal)41.66665 + (decimal)41.6666666666667;
            var muajiB2 = new ClsBMuajt(); muajiB2.Shkurt = (decimal)83.3333333333333;

            Assert.IsTrue(buxhetiValidim.kontrolloVleratMuajve(muajiA1, muajiA2, "Bukur").Status);
            Assert.IsFalse(buxhetiValidim.kontrolloVleratMuajve(muajiB1, muajiB2, "JoBukur").Status);
        }
        
        [TestMethod]
        public void TestRefuzoVeprimFshirjeModifikimDokumentiSipasStatusitTeTij()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();

            clsMesazh a = ClsBValidimBuxheti.RefuzoVepriminSipasStatusit(1, 1);
            clsMesazh b = ClsBValidimBuxheti.RefuzoVepriminSipasStatusit(1, 8);
            clsMesazh c = ClsBValidimBuxheti.RefuzoVepriminSipasStatusit(1, 9);
            clsMesazh d = ClsBValidimBuxheti.RefuzoVepriminSipasStatusit(1, 10);
            clsMesazh e = ClsBValidimBuxheti.RefuzoVepriminSipasStatusit(2, 1);
            clsMesazh f = ClsBValidimBuxheti.RefuzoVepriminSipasStatusit(2, 8);
            clsMesazh g = ClsBValidimBuxheti.RefuzoVepriminSipasStatusit(2, 9);
            clsMesazh h = ClsBValidimBuxheti.RefuzoVepriminSipasStatusit(2, 10);
            clsMesazh i = ClsBValidimBuxheti.RefuzoVepriminSipasStatusit(3, 1);

            Assert.IsTrue(a.PershkrimMesazhi == "Dokumenti eshte ruajtuar ndaj nuk mund te modifikohet.");
            Assert.IsTrue(b.PershkrimMesazhi == "Dokumenti eshte refuzuar ndaj nuk mund te modifikohet.");
            Assert.IsTrue(c.PershkrimMesazhi == "Dokumenti eshte postuar ndaj nuk mund te modifikohet.");
            Assert.IsTrue(d.PershkrimMesazhi == string.Empty);
            Assert.IsTrue(e.PershkrimMesazhi == "Dokumenti eshte ruajtuar ndaj nuk mund te fshihet.");
            Assert.IsTrue(f.PershkrimMesazhi == "Dokumenti eshte refuzuar ndaj nuk mund te fshihet.");
            Assert.IsTrue(g.PershkrimMesazhi == "Dokumenti eshte postuar ndaj nuk mund te fshihet.");
            Assert.IsTrue(h.PershkrimMesazhi == string.Empty);
            Assert.IsTrue(i.PershkrimMesazhi == "Dokumenti eshte ruajtuar ndaj nuk mund te modifikohet.");
        }
        
        [TestMethod]
        public void TestValidoLejimPostimDokumenti()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();
            
            clsMesazh a = ClsBValidimBuxheti.ValidoPostim(0, 0, 0);
            Assert.IsTrue(a.PershkrimMesazhi == "Nuk ka asnje dokument per postim.");
            
            clsMesazh b = ClsBValidimBuxheti.ValidoPostim(1, 8, 0);
            Assert.IsTrue(b.PershkrimMesazhi == "Ky dokument eshte refuzuar dhe nuk mund te postohet.");
            
            clsMesazh c = ClsBValidimBuxheti.ValidoPostim(1, 9, 0);
            Assert.IsTrue(c.PershkrimMesazhi == "Ky dokument eshte postuar njehere.");
            
            clsMesazh f = ClsBValidimBuxheti.ValidoPostim(1, 10, 172);
            Assert.IsTrue(f.PershkrimMesazhi == "Nuk mund te postohen dokumenta me status jo draft.");

            clsMesazh g = ClsBValidimBuxheti.ValidoPostim(1, 10, 123);
            Assert.IsTrue(g.PershkrimMesazhi == string.Empty);

            clsMesazh d = ClsBValidimBuxheti.ValidoPostim(1, 0, 1);
            Assert.IsTrue(d.PershkrimMesazhi == string.Empty);
            
            clsMesazh e = ClsBValidimBuxheti.ValidoPostim(1, 0, 170);
            Assert.IsTrue(e.PershkrimMesazhi == "Nuk mund te postohen dokumenta me status draft.");

            var buxheti1 = new ClsBKokaBuxheti(0, 0, 0, "1", new DateTime(), 0, 0, 1, 0, 0, "", new DateTime(), new DateTime(), 0, 0, 0, 0, 0, "PB", 0, "", 0, 170, 0, 0, 0, DbCore.DbRegjistrim.StatusAprovimi.Undefined, "", 0, 0);
            var buxheti2 = new ClsBKokaBuxheti(2, 0, 0, "2", new DateTime(), 0, 0, 0, 0, 0, "", new DateTime(), new DateTime(), 0, 0, 0, 0, 0, "PB", 0, "", 0, 170, 0, 0, 0, DbCore.DbRegjistrim.StatusAprovimi.Undefined, "", 0, 0);
            var buxheti3 = new ClsBKokaBuxheti(3, 0, 0, "3", new DateTime(), 0, 0, 9, 0, 0, "", new DateTime(), new DateTime(), 0, 0, 0, 0, 0, "PB", 0, "", 0, 170, 0, 0, 0, DbCore.DbRegjistrim.StatusAprovimi.Undefined, "", 0, 0);
            var buxheti4 = new ClsBKokaBuxheti(4, 0, 0, "4", new DateTime(), 0, 0, 1, 0, 0, "", new DateTime(), new DateTime(), 0, 0, 0, 0, 0, "PB", 0, "", 0, 170, 0, 0, 0, DbCore.DbRegjistrim.StatusAprovimi.Undefined, "", 0, 0);

            Assert.IsFalse(buxheti1.ValidoPostim().Status);
            Assert.IsFalse(buxheti2.ValidoPostim().Status);
            Assert.IsFalse(buxheti3.ValidoPostim().Status);
            Assert.IsTrue(buxheti4.ValidoPostim().Status);
        }

        [TestMethod]
        public void GjeneroFormuleSakte()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();

            var colKomponente = bejGatiKomponentePerTestim();
            var colKomponenteVlera = bejGatiKomponenteVleraPerTestim();
            var komponenteGjeneruar = new ColBKomponenteVlere();
            var komponenteFillestare = new ClsBKomponente(12, 0, 0, 0, null, null, "K", "0.5*KH+0.5KP", (int)EnumBTipKomponente.Llogaritese, (int)EnumBNjesiKomponente.Formule, 0, "0.5*KH+0.5*KP", null, null, null, true);
            var dt = new DataTable();
            try
            {
                var formula = ClsBKomponente.gjeneroKomponenteNgaFormula(komponenteFillestare, "0.5*KH+0.5*KP", 101, colKomponente, colKomponenteVlera, ref komponenteGjeneruar, 3655);
                var vlera = ClsBKomponente.KtheSipasVlereMinMax(Convert.ToDecimal(formula), komponenteFillestare.VleraMin, komponenteFillestare.VleraMax, komponenteFillestare.LlojKufizimi);
                Assert.IsTrue((Convert.ToDecimal(vlera) - (decimal)0.9111) < (decimal)0.001);
            }
            catch (MyException msg)
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public void GjeneroFormuleGabim()
        {
            ImbLogger.BuxhetimiLogger = new FakeImbLogger();

            var colKomponente = bejGatiKomponentePerTestim();
            var colKomponenteVlera = bejGatiKomponenteVleraPerTestimGabim();
            var komponenteGjeneruar = new ColBKomponenteVlere();
            var komponenteFillestare = new ClsBKomponente(12, 0, 0, 0, null, null, "K", "0.5*KH+0.5KP", (int)EnumBTipKomponente.Llogaritese, (int)EnumBNjesiKomponente.Formule, 0, "0.5*KH+0.5*KP", null, null, null, true);
            var dt = new DataTable();
            try
            {
                var formula = ClsBKomponente.gjeneroKomponenteNgaFormula(komponenteFillestare, "0.5*KH+0.5*KP", 101, colKomponente, colKomponenteVlera, ref komponenteGjeneruar, 3655);
                var vlera = ClsBKomponente.KtheSipasVlereMinMax(Convert.ToDecimal(formula), komponenteFillestare.VleraMin, komponenteFillestare.VleraMax, komponenteFillestare.LlojKufizimi);
                Assert.IsTrue((Convert.ToDecimal(vlera) - (decimal)0.9111) < (decimal)0.001);
            }
            catch (MyException msg)
            {
                Assert.IsFalse(false);
            }
        }

        private static ColBKomponente bejGatiKomponentePerTestim()
        {
            var colKomponente = new ColBKomponente();
            colKomponente.Add(new ClsBKomponente(1, 0, 0, 0, null, null, "T1", "0.5", (int)EnumBTipKomponente.Komponente, (int)EnumBNjesiKomponente.Numer, 0, "0.5", 1, 2, 1, true));
            colKomponente.Add(new ClsBKomponente(2, 0, 0, 0, null, null, "T2", "2", (int)EnumBTipKomponente.Komponente, (int)EnumBNjesiKomponente.Numer, 1, "2", 0, 1, 2, true));
            colKomponente.Add(new ClsBKomponente(3, 0, 0, 0, null, null, "T3", "3", (int)EnumBTipKomponente.Komponente, (int)EnumBNjesiKomponente.Numer, 2, "3", 0, 2, 2, true));
            colKomponente.Add(new ClsBKomponente(4, 0, 0, 0, null, null, "K1", "(KK+KD)/KD", (int)EnumBTipKomponente.Llogaritese, (int)EnumBNjesiKomponente.Formule, 0, "(KK+KD)/KD", null, null, null, true));
            colKomponente.Add(new ClsBKomponente(5, 0, 0, 0, null, null, "K2", "T1/T2", (int)EnumBTipKomponente.Llogaritese, (int)EnumBNjesiKomponente.Formule, 0, "T1/T2", null, null, null, true));
            colKomponente.Add(new ClsBKomponente(6, 0, 0, 0, null, null, "K3", "T1+T2", (int)EnumBTipKomponente.Llogaritese, (int)EnumBNjesiKomponente.Formule, 0, "T1+T2/T3", null, null, null, true));
            colKomponente.Add(new ClsBKomponente(7, 0, 0, 0, null, null, "P", "", (int)EnumBTipKomponente.Komponente, (int)EnumBNjesiKomponente.Tabelare, 0, "", null, null, null, true));
            colKomponente.Add(new ClsBKomponente(8, 0, 0, 0, null, null, "KK", "", (int)EnumBTipKomponente.Komponente, (int)EnumBNjesiKomponente.Tabelare, 0, "", null, null, null, true));
            colKomponente.Add(new ClsBKomponente(9, 0, 0, 0, null, null, "KD", "", (int)EnumBTipKomponente.Komponente, (int)EnumBNjesiKomponente.Tabelare, 0, "", null, null, null, true));
            colKomponente.Add(new ClsBKomponente(10, 0, 0, 0, null, null, "KP", "P*1.1", (int)EnumBTipKomponente.Llogaritese, (int)EnumBNjesiKomponente.Formule, 0, "P*1.1", null, null, null, true));
            colKomponente.Add(new ClsBKomponente(11, 0, 0, 0, null, null, "KH", "(K1+K2+K3)/3", (int)EnumBTipKomponente.Llogaritese, (int)EnumBNjesiKomponente.Formule, 0, "(K1+K2+K3)/3", null, null, null, true));
            colKomponente.Add(new ClsBKomponente(12, 0, 0, 0, null, null, "K", "0.5*KH+0.5KP", (int)EnumBTipKomponente.Llogaritese, (int)EnumBNjesiKomponente.Formule, 0, "0.5*KH+0.5*KP", null, null, null, true));

            return colKomponente;
        }

        private static ColBKomponenteVlere bejGatiKomponenteVleraPerTestim()
        {
            var colKomponente = new ColBKomponenteVlere();
            colKomponente.Add(new ClsBKomponenteVlere(0, 0, 0, 0, 0, null, null, 7, 101, 1, 1, 2, 1, true, "101", "P"));
            colKomponente.Add(new ClsBKomponenteVlere(0, 0, 0, 0, 0, null, null, 8, 101, 1, 1, 1, 2, true, "101", "KK"));
            colKomponente.Add(new ClsBKomponenteVlere(0, 0, 0, 0, 0, null, null, 9, 101, 1, 0, 2, 2, true, "101", "KD"));
            return colKomponente;
        }

        private static ColBKomponenteVlere bejGatiKomponenteVleraPerTestimGabim()
        {
            var colKomponente = new ColBKomponenteVlere();
            colKomponente.Add(new ClsBKomponenteVlere(0, 0, 0, 0, 0, null, null, 7, 101, 1, 1, 2, 1, true, "101", "P"));
            colKomponente.Add(new ClsBKomponenteVlere(0, 0, 0, 0, 0, null, null, 8, 101, 1, 1, 1, 2, true, "101", "KK"));
            colKomponente.Add(new ClsBKomponenteVlere(0, 0, 0, 0, 0, null, null, 9, 101, 0, 0, 2, 2, true, "101", "KD"));
            return colKomponente;
        }

        private static ColBKomponenteLidhje bejGatiKomponenteLidhjePerTestim()
        {
            var colKomponente = new ColBKomponenteLidhje();
            colKomponente.Add(new ClsBKomponenteLidhje(1, 7, 101, "101", "101", 1, "", ""));
            colKomponente.Add(new ClsBKomponenteLidhje(1, 8, 101, "101", "101", 2, "", ""));
            colKomponente.Add(new ClsBKomponenteLidhje(1, 9, 101, "101", "101", 3, "", ""));
            return colKomponente;
        }     
        
    }
}