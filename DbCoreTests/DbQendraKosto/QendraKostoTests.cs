using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbCore.DbQendraKosto;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbListPagesat;
using System.Collections.Generic;
using static DbCore.DbQendraKosto.clsKokaQendraKosto;

namespace DbCoreTests.DbQendraKosto
{
    [TestClass]
    public class QendraKostoTests : FakeHttpContextBase
    {
        [TestMethod]
        public void TestKtheVlereQKDheKurs()
        {
            Tuple<double, double> a = clsKokaQendraKosto.KtheVlereQKDheKurs(1, 1, 1, 10.12, 10.12, 1.0, 1.0);
            Assert.IsTrue(a.Item1 == 10.12);
            Assert.IsTrue(a.Item2 == 1.0);

            Tuple<double, double> b = clsKokaQendraKosto.KtheVlereQKDheKurs(1, 1, 2, 10.12, 20.0, 1.25, 1.27);
            Assert.IsTrue(b.Item1 == 10.12);
            Assert.IsTrue(b.Item2 == 1.25);

            Tuple<double, double> c = clsKokaQendraKosto.KtheVlereQKDheKurs(1, 2, 2, 10.12, 10.90, 1.30, 1.40);
            Assert.IsTrue(c.Item1 == 10.90);
            Assert.IsTrue(c.Item2 == 1.0);

            Tuple<double, double> d = clsKokaQendraKosto.KtheVlereQKDheKurs(1, 2, 3, 10.12, 10.90, 1.40, 1.50);
            Assert.IsTrue(d.Item1 == 10.90/1.50);
            Assert.IsTrue(d.Item2 == 1.50);
        }

        [TestMethod]
        public void TestKthePermbajtjeMesazhi()
        {
            string mesazhi1 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, true, 0, 1);
            string mesazhi12 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, true, 0, 2);
            string mesazhi13 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, true, 0, 3);
            string mesazhi14 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, true, 0, 4);
            string mesazhi15 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, true, 0, 5);
            string mesazhi16 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, true, 1, 1);
            string mesazhi17 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, true, 1, 2);
            string mesazhi18 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, true, 1, 3);
            string mesazhi19 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, true, 1, 4);
            string mesazhi20 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, true, 1, 5);

            string mesazhi21 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, false, 0, 1);
            string mesazhi22 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, false, 0, 2);
            string mesazhi23 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, false, 0, 3);
            string mesazhi24 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, false, 0, 4);
            string mesazhi25 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, false, 0, 5);
            string mesazhi26 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, false, 1, 1);
            string mesazhi27 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, false, 1, 2);
            string mesazhi28 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, false, 1, 3);
            string mesazhi29 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, false, 1, 4);
            string mesazhi30 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(false, false, 1, 5);

            string mesazhi31 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, false, 0, 1);
            string mesazhi32 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, false, 0, 2);
            string mesazhi33 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, false, 0, 3);
            string mesazhi34 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, false, 0, 4);
            string mesazhi35 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, false, 0, 5);
            string mesazhi36 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, true, 0, 1);
            string mesazhi37 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, true, 0, 2);
            string mesazhi38 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, true, 0, 3);
            string mesazhi39 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, true, 0, 4);
            string mesazhi40 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, true, 0, 5);

            string mesazhi41 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, false, 1, 1);
            string mesazhi42 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, true, 1, 1);
            string mesazhi43 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, false, 1, 5);
            string mesazhi44 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, true, 1, 5);

            Assert.IsTrue(mesazhi1 == "jo" && mesazhi12 == "jo" && mesazhi13 == "jo" && mesazhi14 == "jo" && mesazhi15 == "jo" && mesazhi16 == "jo" && mesazhi17 == "jo" && mesazhi18 == "jo" && mesazhi19 == "jo" && mesazhi20 == "jo" && mesazhi21 == "jo" && mesazhi22 == "jo" && mesazhi23 == "jo" && mesazhi24 == "jo" && mesazhi25 == "jo" && mesazhi26 == "jo" && mesazhi27 == "jo" && mesazhi28 == "jo" && mesazhi29 == "jo" && mesazhi30 == "jo" && mesazhi31 == "jo" && mesazhi32 == "jo" && mesazhi33 == "jo" && mesazhi34 == "jo" && mesazhi35 == "jo" && mesazhi36 == "jo" && mesazhi37 == "jo" && mesazhi38 == "jo" && mesazhi39 == "jo" && mesazhi40 == "jo" && mesazhi41 == "jo" && mesazhi42 == "jo" && mesazhi43 == "jo" && mesazhi44 == "jo");
            Assert.IsFalse(mesazhi1 == "Jo" && mesazhi12 == "Jo" && mesazhi13 == "Jo" && mesazhi14 == "Jo" && mesazhi15 == "Jo" && mesazhi16 == "Jo" && mesazhi17 == "Jo" && mesazhi18 == "Jo" && mesazhi19 == "Jo" && mesazhi20 == "Jo" && mesazhi21 == "Jo" && mesazhi22 == "Jo" && mesazhi23 == "Jo" && mesazhi24 == "Jo" && mesazhi25 == "Jo" && mesazhi26 == "Jo" && mesazhi27 == "Jo" && mesazhi28 == "Jo" && mesazhi29 == "Jo" && mesazhi30 == "Jo" && mesazhi31 == "Jo" && mesazhi32 == "Jo" && mesazhi33 == "Jo" && mesazhi34 == "Jo" && mesazhi35 == "Jo" && mesazhi36 == "Jo" && mesazhi37 == "Jo" && mesazhi38 == "Jo" && mesazhi39 == "Jo" && mesazhi40 == "Jo" && mesazhi41 == "Jo" && mesazhi42 == "Jo" && mesazhi43 == "Jo" && mesazhi44 == "Jo");
            Assert.IsFalse(mesazhi1 == "po" && mesazhi12 == "po" && mesazhi13 == "po" && mesazhi14 == "po" && mesazhi15 == "po" && mesazhi16 == "po" && mesazhi17 == "po" && mesazhi18 == "po" && mesazhi19 == "po" && mesazhi20 == "po" && mesazhi21 == "po" && mesazhi22 == "po" && mesazhi23 == "po" && mesazhi24 == "po" && mesazhi25 == "po" && mesazhi26 == "po" && mesazhi27 == "po" && mesazhi28 == "po" && mesazhi29 == "po" && mesazhi30 == "po" && mesazhi31 == "po" && mesazhi32 == "po" && mesazhi33 == "po" && mesazhi34 == "po" && mesazhi35 == "po" && mesazhi36 == "po" && mesazhi37 == "po" && mesazhi38 == "po" && mesazhi39 == "po" && mesazhi40 == "po" && mesazhi41 == "po" && mesazhi42 == "po" && mesazhi43 == "po" && mesazhi44 == "po");

            string mesazh45 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, true, 1, 2);
            string mesazh46 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, false, 1, 2);
            Assert.IsTrue(mesazh45 == "shfaqmesazh");
            Assert.IsFalse(mesazh45 == "jo");
            Assert.IsTrue(mesazh46 == "jo");
            Assert.IsFalse(mesazh46 == "shfaqmesazh");

            string mesazhi47 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, false, 1, 3);
            string mesazhi48 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, true, 1, 3);
            string mesazhi49 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, false, 1, 4);
            string mesazhi50 = clsKokaQendraKosto.KthePermbajtjePerShfaqjenEMesazhit(true, true, 1, 4);
            Assert.IsTrue(mesazhi47 == "shfaqmesazh" && mesazhi48 == "shfaqmesazh");
            Assert.IsFalse(mesazhi47 == "jo" && mesazhi48 == "jo");
            
            Assert.IsTrue(mesazhi49 == "shfaqlupe" && mesazhi50 == "shfaqlupe");
            Assert.IsFalse(mesazhi49 == "jo" && mesazhi50 == "jo");

        }

        [TestMethod]
        public void TestPercaktoIdDheLlojQendre()
        {
            //Kujdes! Radha e instruksione ka rendesi

            //arrange
            clsDegeAdministrative dega = new clsDegeAdministrative();
            dega.LlojQendre = 1;
            dega.QendraKostos = 11;
            dega.IdSkemaQendraKosto = 21;

            clsStrukturaAdministrative nenDep = new clsStrukturaAdministrative();
            nenDep.LlojQendre = 1;
            nenDep.QendraKostos = 12;
            nenDep.IdSkemaQendraKosto = 22;

            clsStrukturaAdministrative dep = new clsStrukturaAdministrative();
            dep.LlojQendre = 1;
            dep.QendraKostos = 13;
            dep.IdSkemaQendraKosto = 23;

            clsLlogari llog = new clsLlogari();
            llog.LlojQendre = 1;
            llog.QendraKostos = 14;
            llog.IdSkemaQendraKosto = 24;

            clsNjesiAdministrative mag = new clsNjesiAdministrative();
            mag.LlojQendre = 1;
            mag.QendraKostos = 15;
            mag.IdSkemaQendraKosto = 25;

            //act
            Tuple<int, int> pDega = clsKokaQendraKosto.PercaktoIdDheLlojQendre(1, 0, 0, llog, mag, dega, dep, nenDep);
            Tuple<int, int> pNenDep = clsKokaQendraKosto.PercaktoIdDheLlojQendre(2, 0, 0, llog, mag, dega, dep, nenDep);
            Tuple<int, int> pDep = clsKokaQendraKosto.PercaktoIdDheLlojQendre(3, 0, 0, llog, mag, dega, dep, nenDep);
            Tuple<int, int> pLlog = clsKokaQendraKosto.PercaktoIdDheLlojQendre(4, 0, 0, llog, mag, dega, dep, nenDep);
            Tuple<int, int> pMag = clsKokaQendraKosto.PercaktoIdDheLlojQendre(5, 0, 0, llog, mag, dega, dep, nenDep);

            //assert
            Assert.IsTrue(pDega.Item1 == 1 && pDega.Item2 == 11);
            Assert.IsTrue(pNenDep.Item1 == 1 && pNenDep.Item2 == 12);
            Assert.IsTrue(pDep.Item1 == 1 && pDep.Item2 == 13);
            Assert.IsTrue(pLlog.Item1 == 1 && pLlog.Item2 == 14);
            Assert.IsTrue(pMag.Item1 == 1 && pMag.Item2 == 15);
            
            //arrange
            dega.LlojQendre = 2;            
            nenDep.LlojQendre = 2;           
            dep.LlojQendre = 2;            
            llog.LlojQendre = 2;           
            mag.LlojQendre = 2;

            //act
            Tuple<int, int> pDega2 = clsKokaQendraKosto.PercaktoIdDheLlojQendre(1, 0, 0, llog, mag, dega, dep, nenDep);
            Tuple<int, int> pNenDep2 = clsKokaQendraKosto.PercaktoIdDheLlojQendre(2, 0, 0, llog, mag, dega, dep, nenDep);
            Tuple<int, int> pDep2 = clsKokaQendraKosto.PercaktoIdDheLlojQendre(3, 0, 0, llog, mag, dega, dep, nenDep);
            Tuple<int, int> pLlog2 = clsKokaQendraKosto.PercaktoIdDheLlojQendre(4, 0, 0, llog, mag, dega, dep, nenDep);
            Tuple<int, int> pMag2 = clsKokaQendraKosto.PercaktoIdDheLlojQendre(5, 0, 0, llog, mag, dega, dep, nenDep);

            //assert
            Assert.IsTrue(pDega2.Item1 == 2 && pDega2.Item2 == 21);
            Assert.IsTrue(pNenDep2.Item1 == 2 && pNenDep2.Item2 == 22);
            Assert.IsTrue(pDep2.Item1 == 2 && pDep2.Item2 == 23);
            Assert.IsTrue(pLlog2.Item1 == 2 && pLlog2.Item2 == 24);
            Assert.IsTrue(pMag2.Item1 == 2 && pMag2.Item2 == 25);
        }
        
        [TestMethod]
        public void TestMerrIdDheLlojQendre()
        {
            //Kujdes! Radha e instruksione ka rendesi

            clsDegeAdministrative dega = new clsDegeAdministrative();
            dega.LlojQendre = 1;
            dega.QendraKostos = 11;
            dega.IdSkemaQendraKosto = 21;

            clsStrukturaAdministrative nenDep = new clsStrukturaAdministrative();
            nenDep.LlojQendre = 1;
            nenDep.QendraKostos = 12;
            nenDep.IdSkemaQendraKosto = 22;

            clsStrukturaAdministrative dep = new clsStrukturaAdministrative();
            dep.LlojQendre = 1;
            dep.QendraKostos = 13;
            dep.IdSkemaQendraKosto = 23;

            clsLlogari llog = new clsLlogari();
            llog.LlojQendre = 1;
            llog.QendraKostos = 14;
            llog.IdSkemaQendraKosto = 24;

            clsNjesiAdministrative mag = new clsNjesiAdministrative();
            mag.LlojQendre = 1;
            mag.QendraKostos = 15;
            mag.IdSkemaQendraKosto = 25;

            clsKonfigurimQK konf = new clsKonfigurimQK();
            konf.Prioriteti1 = 1;
            konf.Prioriteti2 = 2;
            konf.Prioriteti3 = 3;
            konf.Prioriteti4 = 4;
            konf.Prioriteti5 = 5;

            int idqk = 0;
            int llojiqk = 1;

            clsKokaQendraKosto.MerrIdDheLlojQendre(konf, ref llojiqk, ref idqk, llog, mag, dega, dep, nenDep);
            Assert.IsTrue(llojiqk==1 && idqk==11);

            dega.QendraKostos = 0;
            clsKokaQendraKosto.MerrIdDheLlojQendre(konf, ref llojiqk, ref idqk, llog, mag, dega, dep, nenDep);
            Assert.IsTrue(llojiqk == 1 && idqk == 12);

            nenDep.QendraKostos = 0;
            clsKokaQendraKosto.MerrIdDheLlojQendre(konf, ref llojiqk, ref idqk, llog, mag, dega, dep, nenDep);
            Assert.IsTrue(llojiqk == 1 && idqk == 13);

            dep.QendraKostos = 0;
            clsKokaQendraKosto.MerrIdDheLlojQendre(konf, ref llojiqk, ref idqk, llog, mag, dega, dep, nenDep);
            Assert.IsTrue(llojiqk == 1 && idqk == 14);

            llog.QendraKostos = 0;
            clsKokaQendraKosto.MerrIdDheLlojQendre(konf, ref llojiqk, ref idqk, llog, mag, dega, dep, nenDep);
            Assert.IsTrue(llojiqk == 1 && idqk == 15);

            mag.QendraKostos = 0;
            clsKokaQendraKosto.MerrIdDheLlojQendre(konf, ref llojiqk, ref idqk, llog, mag, dega, dep, nenDep);
            Assert.IsTrue(llojiqk == 1 && idqk == 0);
        }

        [TestMethod]
        public void TestGrupoTrupatSipasEkzistencesSeLlogarive()
        {
            //arrange
            colTrupiQendraKosto QendraKostoVjeter = PergatitQkVjeter();
            List <QenderKostoTrupFK> QKperTRFKgrupuar = PergatitVlera();

            //act
            Tuple<List<QenderKostoTrupFK>, List<QenderKostoTrupFK>> llogarite = GrupoTrupatSipasEkzistencesSeLlogarive(QKperTRFKgrupuar, QendraKostoVjeter);

            //assert
            Assert.IsTrue(llogarite.Item1.FindIndex(x => x.IdLlogaria == 1) != -1);
            Assert.IsTrue(llogarite.Item1.FindIndex(x => x.IdLlogaria == 2) != -1);
            Assert.IsTrue(llogarite.Item1.FindIndex(x => x.IdLlogaria == 3) == -1);
            Assert.IsTrue(llogarite.Item1.FindIndex(x => x.IdLlogaria == 4) == -1);

            Assert.IsTrue(llogarite.Item1.Find(x => x.IdLlogaria == 1).VleftaDebiTrupiFleteKontabel == 200);
            Assert.IsTrue(llogarite.Item1.Find(x => x.IdLlogaria == 2).VleftaDebiTrupiFleteKontabel == 300);

            Assert.IsTrue(llogarite.Item2.FindIndex(x => x.IdLlogaria == 1) == -1);
            Assert.IsTrue(llogarite.Item2.FindIndex(x => x.IdLlogaria == 2) == -1);
            Assert.IsTrue(llogarite.Item2.FindIndex(x => x.IdLlogaria == 3) != -1);
            Assert.IsTrue(llogarite.Item2.FindIndex(x => x.IdLlogaria == 4) != -1);

            Assert.IsTrue(llogarite.Item2.Find(x => x.IdLlogaria == 3).VleftaDebiTrupiFleteKontabel == 100);
            Assert.IsTrue(llogarite.Item2.Find(x => x.IdLlogaria == 4).VleftaDebiTrupiFleteKontabel == 100);
            Assert.IsTrue(llogarite.Item2.Find(x => x.IdLlogaria == 5).VleftaDebiTrupiFleteKontabel == 100);
        }
               
        [TestMethod]
        public void TestLlogaritVleraTrupi()
        {
            //Kujdes! Radha e instruksione ka rendesi

            colTrupiQendraKosto col = new colTrupiQendraKosto();
            double perqindjambetur = 0;

            //pa objektiva

            //qendra e kostos, llogaria dhe ndermarrja kane te njejten monedhe
            clsKokaQendraKosto.LlogaritVleraTrupi(1, 1, "kodiMonedhaLlog", "nrLlogaria", "emerLlogaria1", 1, 1, "kodiQK", "pershkrimiQK", ref col, 1, 20, 10, 1, ref perqindjambetur, null, new List<double>(), new List<double>(), new List<int>(), 1, 0, false, false, 1);
            Assert.IsTrue(col.Count == 1 && col[0].VleftaLlog == 20 && col[0].VleftaMonBaze == 10 && col[0].VleftaQK == 20);

            //llogaria dhe ndermarrja kane te njejten monedhe, qendra e kostos ka monedhe tjeter
            //kursi = 2, kursi i trupit = 1
            clsKokaQendraKosto.LlogaritVleraTrupi(1, 1, "kodiMonedhaLlog", "nrLlogaria", "emerLlogaria1", 1, 2, "kodiQK", "pershkrimiQK", ref col, 1, 20, 10, 1, ref perqindjambetur, null, new List<double>(), new List<double>(), new List<int>(), 1, 0, false, false, 2);
            Assert.IsTrue(col.Count == 2 && col[1].VleftaLlog == 20 && col[1].VleftaMonBaze == 10 && col[1].VleftaQK == 5);

            //qendra e kostos dhe llogaria kane te njejten monedhe, te ndryshme nga ndermarrja
            //kursi = 1, kursi i trupit = 1
            clsKokaQendraKosto.LlogaritVleraTrupi(1, 1, "kodiMonedhaLlog", "nrLlogaria", "emerLlogaria1", 1, 1, "kodiQK", "pershkrimiQK", ref col, 1, 20, 10, 2, ref perqindjambetur, null, new List<double>(), new List<double>(), new List<int>(), 1, 0, false, false, 1);
            Assert.IsTrue(col.Count == 3 && col[2].VleftaLlog == 20 && col[2].VleftaMonBaze == 10 && col[2].VleftaQK == 20);

            //qendra e kostos dhe llogaria kane te njejten monedhe, te ndryshme nga ndermarrja
            //kursi = 2, kursi i trupit = 1
            clsKokaQendraKosto.LlogaritVleraTrupi(1, 1, "kodiMonedhaLlog", "nrLlogaria", "emerLlogaria1", 1, 1, "kodiQK", "pershkrimiQK", ref col, 1, 20, 10, 2, ref perqindjambetur, null, new List<double>(), new List<double>(), new List<int>(), 1, 0, false, false, 2);
            Assert.IsTrue(col.Count == 4 && col[3].VleftaLlog == 20 && col[3].VleftaMonBaze == 10 && col[3].VleftaQK == 20);

            //qendra e kostos dhe llogaria kane te njejten monedhe, te ndryshme nga ndermarrja 
            //kursi = 2, kursi i trupit = 2
            clsKokaQendraKosto.LlogaritVleraTrupi(1, 1, "kodiMonedhaLlog", "nrLlogaria", "emerLlogaria1", 1, 1, "kodiQK", "pershkrimiQK", ref col, 2, 20, 10, 2, ref perqindjambetur, null, new List<double>(), new List<double>(), new List<int>(), 1, 0, false, false, 2);
            Assert.IsTrue(col.Count == 5 && col[4].VleftaLlog == 20 && col[4].VleftaMonBaze == 10 && col[4].VleftaQK == 20);
            
            //qendra e kostos, llogaria dhe ndermarrja kane monedha te ndryshme
            //kursi = 1, kursi i trupit = 2
            clsKokaQendraKosto.LlogaritVleraTrupi(1, 1, "kodiMonedhaLlog", "nrLlogaria", "emerLlogaria1", 1, 2, "kodiQK", "pershkrimiQK", ref col, 1.2, 20, 10, 3, ref perqindjambetur, null, new List<double>(), new List<double>(), new List<int>(), 1, 0, false, false, 1.5);
            Assert.IsTrue(col.Count == 6 && col[5].VleftaLlog == 20 && col[5].VleftaMonBaze == 10 && col[5].VleftaQK < 7);

            //me objektiva
            //DbQendraKosto.colObjektivaKosto objektivat, List< double > vleratobjektiva, List<double> vleratobjektivamonbaze, List< int > idllogobj
            colObjektivaKosto objektivat = new colObjektivaKosto();
            clsObjektivaKosto o1 = new clsObjektivaKosto(); objektivat.Add(o1);
            clsObjektivaKosto o2 = new clsObjektivaKosto(); objektivat.Add(o2);
            clsObjektivaKosto o3 = new clsObjektivaKosto(); objektivat.Add(o3);
            clsObjektivaKosto o4 = new clsObjektivaKosto(); objektivat.Add(o4);
            List<double> vleratobjektiva = new List<double> { 11, 22, 33, 44 };
            List<double> vleratobjektivamonbaze = new List<double> { 10, 20, 30, 40 };
            List<int> idllogobj = new List<int> { 1, 2, 3, 4 };
            perqindjambetur = 100;

            //qendra e kostos, llogaria dhe ndermarrja kane te njejten monedhe
            clsKokaQendraKosto.LlogaritVleraTrupi(1, 1, "kodiMonedhaLlog", "nrLlogaria", "emerLlogaria1", 1, 1, "kodiQK", "pershkrimiQK", ref col, 1, 20, 10, 1, ref perqindjambetur, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, 1, 100, true, false, 1);
            Assert.IsTrue(col.Count == 7);
            Assert.IsTrue(col[6].VleftaLlog == 11 && col[6].VleftaMonBaze == 10 && col[6].VleftaQK == 10);

            //qendra e kostos dhe ndermarrja kane te njejten monedhe, ndryshe nga llogaria
            //kursi i trupi = 1
            clsKokaQendraKosto.LlogaritVleraTrupi(1, 2, "kodiMonedhaLlog", "nrLlogaria", "emerLlogaria1", 1, 1, "kodiQK", "pershkrimiQK", ref col, 1, 20, 10, 1, ref perqindjambetur, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, 1, 100, true, false, 1);
            Assert.IsTrue(col.Count == 8);
            Assert.IsTrue(col[7].VleftaLlog == 11 && col[7].VleftaMonBaze == 10 && col[7].VleftaQK == 10);

            //qendra e kostos dhe ndermarrja kane te njejten monedhe, ndryshe nga llogaria
            //kursi i trupi = 2
            clsKokaQendraKosto.LlogaritVleraTrupi(1, 2, "kodiMonedhaLlog", "nrLlogaria", "emerLlogaria1", 1, 1, "kodiQK", "pershkrimiQK", ref col, 2, 20, 10, 1, ref perqindjambetur, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, 1, 100, true, false, 1);
            Assert.IsTrue(col.Count == 9);
            Assert.IsTrue(col[8].VleftaLlog == 11 && col[8].VleftaMonBaze == 10 && col[8].VleftaQK == 10);

            //qendra e kostos dhe ndermarrja kane te njejten monedhe, ndryshe nga llogaria
            //kursi = 2
            clsKokaQendraKosto.LlogaritVleraTrupi(1, 2, "kodiMonedhaLlog", "nrLlogaria", "emerLlogaria1", 1, 1, "kodiQK", "pershkrimiQK", ref col, 1, 20, 10, 1, ref perqindjambetur, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, 1, 100, true, false, 2);
            Assert.IsTrue(col.Count == 10);
            Assert.IsTrue(col[9].VleftaLlog == 11 && col[9].VleftaMonBaze == 10 && col[9].VleftaQK == 10);
        }

        [TestMethod]
        public void TestKrijoTrupQKPerQKVjeterSipasSHDQKPMD()
        {
            //trupiqendravjeter Debi
            clsTrupiQendraKosto qkVjeter1 = new clsTrupiQendraKosto(0, 0, 1, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 1, "llogari", "pershkrimllog", 1, 10, 10, 10, "monedha", 1, "");
            QenderKostoTrupFK QKperTRFK1 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 10.00, 20.00 }, VleraMonBazeObjektiva = new List<double> { 10.00, 20.00 }, Objektivat = null };

            clsTrupiQendraKosto trqk = clsKokaQendraKosto.KrijoTrupQKPerQKVjeterSipasSHDQKPMD(QKperTRFK1, qkVjeter1, 1, 1, 1);

            Assert.IsTrue(trqk != null && trqk.DebiKredi == 1 && trqk.VleftaLlog == 10 && trqk.VleftaMonBaze == 10 && trqk.VleftaQK == 10);

            //trupiqendravjeter Kredi
            clsTrupiQendraKosto qkVjeter2 = new clsTrupiQendraKosto(0, 0, 1, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 1, "llogari", "pershkrimllog", 2, 10, 10, 10, "monedha", 1, "");
            QenderKostoTrupFK QKperTRFK2 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 10.00, 20.00 }, VleraMonBazeObjektiva = new List<double> { 10.00, 20.00 }, Objektivat = null };

            clsTrupiQendraKosto trqk2 = clsKokaQendraKosto.KrijoTrupQKPerQKVjeterSipasSHDQKPMD(QKperTRFK2, qkVjeter2, 1, 1, 1);

            Assert.IsTrue(trqk2 != null && trqk2.DebiKredi == 2 && trqk2.VleftaLlog == 10 && trqk2.VleftaMonBaze == 10 && trqk2.VleftaQK == 10);

        }

        private static clsTrupiQendraKosto KrijoTrupQKPPerDiferencenSipasSHDQKPMD(QenderKostoTrupFK tr, double _totaliVjeterLlog, double _totaliVjeterMonBaze, clsQendraKosto QKP, double vleraKursitQKP, int _idMonedheNderm)
        {
            double vleraQKP = (tr.VleftaDebiTrupiFleteKontabel - Math.Abs(_totaliVjeterLlog < 0 ? _totaliVjeterLlog : 0)) - (tr.VleftaKrediTrupiFleteKontabel - Math.Abs(_totaliVjeterLlog > 0 ? _totaliVjeterLlog : 0));
            double vleraMonBazeQKP = (tr.VleftaDebiMonBazeTrupiFleteKontabel - Math.Abs(_totaliVjeterMonBaze < 0 ? _totaliVjeterMonBaze : 0)) - (tr.VleftaKrediMonBazeTrupiFleteKontabel - Math.Abs(_totaliVjeterMonBaze > 0 ? _totaliVjeterMonBaze : 0));

            Tuple<double, double> vleraDheKursiQKP = KtheVlereQKDheKurs(tr.IdMonedha, QKP.IdMonedha, _idMonedheNderm, vleraQKP, vleraMonBazeQKP, tr.Kursi, vleraKursitQKP);

            clsTrupiQendraKosto trQKP = new clsTrupiQendraKosto(0, 0, QKP.Id, QKP.Kodi, QKP.Pershkrimi, 0, "", "", tr.IdLlogaria, tr.NrLlogari, tr.EmerLlogari, vleraMonBazeQKP > 0 ? 1 : 2, Math.Abs(vleraQKP), Math.Abs(vleraDheKursiQKP.Item1), Math.Abs(vleraMonBazeQKP), tr.KodiMonedha, tr.Kursi, "");

            return trQKP;
        }

        [TestMethod]
        public void TestKrijoTrupQKPPerDiferencenSipasSHDQKPMD()
        {
            //arrange
            clsQendraKosto qkp = new clsQendraKosto(0, "qkp", "qendra e kostos e pacaktuar", 1, true, 0, 0, 0, 0, 1, 1, null);
            QenderKostoTrupFK QKperTRFK1 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 1.00, 2.00 }, VleraMonBazeObjektiva = new List<double> { 1.00, 2.00 }, Objektivat = null };
            //act
            clsTrupiQendraKosto trqk1 = clsKokaQendraKosto.KrijoTrupQKPPerDiferencenSipasSHDQKPMD(QKperTRFK1, 100, 99, qkp, 1.2, 1);
            //assert
            Assert.IsTrue(trqk1 != null && trqk1.DebiKredi == 1 && trqk1.VleftaLlog == 200 && trqk1.VleftaMonBaze == 199 && trqk1.VleftaQK == 200);

            //arrange
            QenderKostoTrupFK QKperTRFK2 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 110, VleftaKrediTrupiFleteKontabel = 120, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 1.00, 2.00 }, VleraMonBazeObjektiva = new List<double> { 1.00, 2.00 }, Objektivat = null };
            //act
            clsTrupiQendraKosto trqk2 = clsKokaQendraKosto.KrijoTrupQKPPerDiferencenSipasSHDQKPMD(QKperTRFK2, 100, 99, qkp, 1.2, 1);
            //assert
            Assert.IsTrue(trqk2 != null && trqk2.DebiKredi == 1 && trqk2.VleftaLlog == 80 && trqk2.VleftaMonBaze == 89 && trqk2.VleftaQK == 80);

            //arrange
            QenderKostoTrupFK QKperTRFK3 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 2, KodiMonedha = "monedha", Kursi = 1.2, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 110, VleftaKrediTrupiFleteKontabel = 120, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 1.00, 2.00 }, VleraMonBazeObjektiva = new List<double> { 1.00, 2.00 }, Objektivat = null };
            //act
            clsTrupiQendraKosto trqk3 = clsKokaQendraKosto.KrijoTrupQKPPerDiferencenSipasSHDQKPMD(QKperTRFK3, 100, 99, qkp, 1.2, 1);
            //assert
            Assert.IsTrue(trqk3 != null && trqk3.DebiKredi == 1 && trqk3.VleftaLlog == 80 && trqk3.VleftaMonBaze == 89 && trqk3.VleftaQK == 89);

            //arrange
            QenderKostoTrupFK QKperTRFK4 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 2, KodiMonedha = "monedha", Kursi = 1.2, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 110, VleftaKrediTrupiFleteKontabel = 120, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 1.00, 2.00 }, VleraMonBazeObjektiva = new List<double> { 1.00, 2.00 }, Objektivat = null };
            //act
            clsTrupiQendraKosto trqk4 = clsKokaQendraKosto.KrijoTrupQKPPerDiferencenSipasSHDQKPMD(QKperTRFK4, 100, 99, qkp, 1.4, 3);
            //assert
            Assert.IsTrue(trqk4 != null && trqk4.DebiKredi == 1 && trqk4.VleftaLlog == 80 && trqk4.VleftaMonBaze == 89 && trqk4.VleftaQK < 65);

            //arrange
            QenderKostoTrupFK QKperTRFK5 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 2, KodiMonedha = "monedha", Kursi = 1.2, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 1100, VleftaKrediTrupiFleteKontabel = 1200, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 1.00, 2.00 }, VleraMonBazeObjektiva = new List<double> { 1.00, 2.00 }, Objektivat = null };
            //act
            clsTrupiQendraKosto trqk5 = clsKokaQendraKosto.KrijoTrupQKPPerDiferencenSipasSHDQKPMD(QKperTRFK5, 100, 99, qkp, 1.4, 3);
            //assert
            Assert.IsTrue(trqk5 != null && trqk5.DebiKredi == 2 && trqk5.VleftaLlog == 1000 && trqk5.VleftaMonBaze == 901 && trqk5.VleftaQK < 644);
        }

        [TestMethod]
        public void TestKrijoTrupQKDukeShperndareVleratNeQKEkzistuese()
        {
            //arrange
            clsTrupiQendraKosto qkVjeter1 = new clsTrupiQendraKosto(0, 0, 1, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 1, "llogari", "pershkrimllog", 1, 10, 10, 10, "monedha", 1, "");
            QenderKostoTrupFK QKperTRFK1 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 1.00, 2.00 }, VleraMonBazeObjektiva = new List<double> { 1.00, 2.00 }, Objektivat = null };
            //act
            clsTrupiQendraKosto trqk1 = clsKokaQendraKosto.KrijoTrupQKDukeShperndareVleratNeQKEkzistuese(QKperTRFK1, qkVjeter1, 1, 1, 1, 1);
            //assert
            Assert.IsTrue(trqk1 != null && trqk1.DebiKredi == 2 && trqk1.VleftaLlog == 1000 && trqk1.VleftaMonBaze == 1000 && trqk1.VleftaQK == 1000);

            //arrange
            clsTrupiQendraKosto qkVjeter2 = new clsTrupiQendraKosto(0, 0, 1, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 1, "llogari", "pershkrimllog", 2, 10, 10, 10, "monedha", 1, "");
            QenderKostoTrupFK QKperTRFK2 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 1.00, 2.00 }, VleraMonBazeObjektiva = new List<double> { 1.00, 2.00 }, Objektivat = null };
            //act
            clsTrupiQendraKosto trqk2 = clsKokaQendraKosto.KrijoTrupQKDukeShperndareVleratNeQKEkzistuese(QKperTRFK2, qkVjeter2, 1, 1, 1, 1);
            //assert
            Assert.IsTrue(trqk2 != null && trqk2.DebiKredi == 2 && trqk2.VleftaLlog == 1000 && trqk2.VleftaMonBaze == 1000 && trqk2.VleftaQK == 1000);

            //arrange
            clsTrupiQendraKosto qkVjeter3 = new clsTrupiQendraKosto(0, 0, 1, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 1, "llogari", "pershkrimllog", 2, 10, 10, 10, "monedha", 1, "");
            QenderKostoTrupFK QKperTRFK3 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 1.00, 2.00 }, VleraMonBazeObjektiva = new List<double> { 1.00, 2.00 }, Objektivat = null };
            //act
            clsTrupiQendraKosto trqk3 = clsKokaQendraKosto.KrijoTrupQKDukeShperndareVleratNeQKEkzistuese(QKperTRFK3, qkVjeter3, 1, 100, 100, 1);
            //assert
            Assert.IsTrue(trqk3 != null && trqk3.DebiKredi == 2 && trqk3.VleftaLlog == 10 && trqk3.VleftaMonBaze == 10 && trqk3.VleftaQK == 10);

            //arrange
            clsTrupiQendraKosto qkVjeter4 = new clsTrupiQendraKosto(0, 0, 1, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 1, "llogari", "pershkrimllog", 1, 10, 10, 10, "monedha", 1, "");
            QenderKostoTrupFK QKperTRFK4 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 1.00, 2.00 }, VleraMonBazeObjektiva = new List<double> { 1.00, 2.00 }, Objektivat = null };
            //act
            clsTrupiQendraKosto trqk4 = clsKokaQendraKosto.KrijoTrupQKDukeShperndareVleratNeQKEkzistuese(QKperTRFK4, qkVjeter4, 1, 50, 55, 1);
            //assert
            Assert.IsTrue(trqk4 != null && trqk4.DebiKredi == 2 && trqk4.VleftaLlog == 20 && trqk4.VleftaMonBaze == 20 && trqk4.VleftaQK == 20);

            //arrange
            clsTrupiQendraKosto qkVjeter5 = new clsTrupiQendraKosto(0, 0, 1, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 1, "llogari", "pershkrimllog", 1, 2, 3, 4, "monedha", 1, "");
            QenderKostoTrupFK QKperTRFK5 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 70, VleftaKrediTrupiFleteKontabel = 80, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 1.00, 2.00 }, VleraMonBazeObjektiva = new List<double> { 1.00, 2.00 }, Objektivat = null };
            //act
            clsTrupiQendraKosto trqk5 = clsKokaQendraKosto.KrijoTrupQKDukeShperndareVleratNeQKEkzistuese(QKperTRFK5, qkVjeter5, 1, 50, 55, 1);
            //assert
            Assert.IsTrue(trqk5 != null && trqk5.DebiKredi == 2 && trqk5.VleftaLlog == 1.6 && trqk5.VleftaMonBaze == 2.4 && trqk5.VleftaQK == 2.4);

            //arrange
            clsTrupiQendraKosto qkVjeter6 = new clsTrupiQendraKosto(0, 0, 1, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 1, "llogari", "pershkrimllog", 1, 10, 10, 10, "monedha", 1, "");
            QenderKostoTrupFK QKperTRFK6 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 2, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 1.00, 2.00 }, VleraMonBazeObjektiva = new List<double> { 1.00, 2.00 }, Objektivat = null };
            //act
            clsTrupiQendraKosto trqk6 = clsKokaQendraKosto.KrijoTrupQKDukeShperndareVleratNeQKEkzistuese(QKperTRFK6, qkVjeter6, 1.5, 50, 55, 3);
            //assert
            Assert.IsTrue(trqk6 != null && trqk6.DebiKredi == 2 && trqk6.VleftaLlog == 20 && trqk6.VleftaMonBaze == 20 && trqk6.VleftaQK < 14);
        }

        private static List<QenderKostoTrupFK> PergatitVlera()
        {
            List<QenderKostoTrupFK> QKperTRFKgrupuar = new List<QenderKostoTrupFK>();
            QenderKostoTrupFK t1 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 10.00, 20.00 }, VleraMonBazeObjektiva = new List<double> { 10.00, 20.00 }, Objektivat = null };
            QenderKostoTrupFK t2 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 1, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 10.00, 20.00 }, VleraMonBazeObjektiva = new List<double> { 10.00, 20.00 }, Objektivat = null };
            QenderKostoTrupFK t3 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 2, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 10.00, 20.00 }, VleraMonBazeObjektiva = new List<double> { 10.00, 20.00 }, Objektivat = null };
            QenderKostoTrupFK t4 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 2, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 10.00, 20.00 }, VleraMonBazeObjektiva = new List<double> { 10.00, 20.00 }, Objektivat = null };
            QenderKostoTrupFK t5 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 3, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 10.00, 20.00 }, VleraMonBazeObjektiva = new List<double> { 10.00, 20.00 }, Objektivat = null };
            QenderKostoTrupFK t6 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 4, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 10.00, 20.00 }, VleraMonBazeObjektiva = new List<double> { 10.00, 20.00 }, Objektivat = null };
            QenderKostoTrupFK t7 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 5, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 10.00, 20.00 }, VleraMonBazeObjektiva = new List<double> { 10.00, 20.00 }, Objektivat = null };
            QenderKostoTrupFK t8 = new QenderKostoTrupFK { IdQendra = 1, LlojiQk = 1, IdLlogaria = 2, NrLlogari = "1", EmerLlogari = "1", IdMonedha = 1, KodiMonedha = "monedha", Kursi = 1, VleftaDebiMonBazeTrupiFleteKontabel = 100, VleftaDebiTrupiFleteKontabel = 100, VleftaKrediMonBazeTrupiFleteKontabel = 0, VleftaKrediTrupiFleteKontabel = 0, IdLlogariObjektiv = new List<int> { 1, 2 }, VleraObjektiva = new List<double> { 10.00, 20.00 }, VleraMonBazeObjektiva = new List<double> { 10.00, 20.00 }, Objektivat = null };

            QKperTRFKgrupuar.Add(t1);
            QKperTRFKgrupuar.Add(t2);
            QKperTRFKgrupuar.Add(t3);
            QKperTRFKgrupuar.Add(t4);
            QKperTRFKgrupuar.Add(t5);
            QKperTRFKgrupuar.Add(t6);
            QKperTRFKgrupuar.Add(t7);
            QKperTRFKgrupuar.Add(t8);

            return QKperTRFKgrupuar;
        }

        private static colTrupiQendraKosto PergatitQkVjeter()
        {
            colTrupiQendraKosto QendraKostoVjeter = new colTrupiQendraKosto();
            clsTrupiQendraKosto tr1 = new clsTrupiQendraKosto(0, 0, 1, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 1, "llogari", "pershkrimllog", 1, 10, 10, 10, "monedha", 1, "");
            clsTrupiQendraKosto tr2 = new clsTrupiQendraKosto(0, 0, 2, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 2, "llogari", "pershkrimllog", 1, 20, 20, 20, "monedha", 1, "");
            clsTrupiQendraKosto tr3 = new clsTrupiQendraKosto(0, 0, 1, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 1, "llogari", "pershkrimllog", 1, 30, 30, 30, "monedha", 1, "");
            clsTrupiQendraKosto tr4 = new clsTrupiQendraKosto(0, 0, 2, "qendra", "pershkrimqendra", 0, "obj", "pershkrimobj", 2, "llogari", "pershkrimllog", 1, 40, 40, 40, "monedha", 1, "");

            QendraKostoVjeter.Add(tr1);
            QendraKostoVjeter.Add(tr2);
            QendraKostoVjeter.Add(tr3);
            QendraKostoVjeter.Add(tr4);

            return QendraKostoVjeter;
        }
    }
}
