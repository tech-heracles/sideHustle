using DbCore.DbRegjistrim;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.DbShare;
using DbCore.DbKontabiliteti;
using DbCore.DbInventari;

namespace DbCoreTests.Receta
{
    [TestClass]
    public class clsKokaRecetaOptikeTest : FakeHttpContextBase
    {
        private clsKonfigurimAmbjenti konfigAmbienti;
        private clsNivelRegjistrimi nivelRegjistrimi;
        private clsKlientFurnitor klienti1;
        private clsKlientFurnitor klienti2;
        private clsKlientFurnitor klienti3;
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idNderVit;
        private int idRaportDesign;
        private int idKonfigAmbjenti;
        private int idArtikullDjathtas;
        private int idArtikullMajtas;

        public clsKokaRecetaOptikeTest()
        {

            idPerdoruesi = 3655;
            idNdermarrje = 564;
            idNderVit = 21291;//id e vitit 2017 per ndermarrjen i2
            konfigAmbienti = new clsKonfigurimAmbjenti() { KodKonfigAmbjente = "RO", IdNdermarje = idNdermarrje };

            idKonfigAmbjenti = konfigAmbienti.merrSipasKodit().IdKonfigAmbjente;
            nivelRegjistrimi = new clsNivelRegjistrimi("RO", idNdermarrje);
            var klientet = new colKlienteFurnitore();
            klientet.mbushKlienteFurnitoreNdermarrjes(idNdermarrje);
            klienti1 = klientet.Where(x => x.LlojiKF == true).FirstOrDefault();
            klienti2 = klientet.Where(x => x.LlojiKF == false).FirstOrDefault();// rasti kur jane vetem furnitor
            klienti3 = klientet.Where(x => x.LlojiKF == true).Last();
            klienti3.AktivKF = false;
            idRaportDesign = new clsRaportDesign("fatura_ReceteOptike", idNdermarrje).IdRaportDesign;
            var artikujt = new colArtikujt();
            artikujt.merrSipasArtikujNdermarrjesAndAutorizimeLike(idNdermarrje, idPerdoruesi, "");
            idArtikullDjathtas = artikujt.First().IdArtikulli;
            idArtikullMajtas = artikujt.Last().IdArtikulli;
          
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void RuajRecetaTest()
        {
            var trupiSyri = krijoRecetaOptikeSyri();
            var trupiPunime = krijoRecetaOptike();

            var koka = new clsKokaRecetaOptike
            {
                NrDok = "1000",
                IdKlient = klienti1.IdKlientFurnitor,
                IdKrijuesi = idPerdoruesi,
                IdNdermarrje = idNdermarrje,
                IdNderVit = idNderVit,
                IdKonfigAmbjente = idKonfigAmbjenti,
                IdNivel = nivelRegjistrimi.IdNivel,
                DtDok = DateTime.Today,
                DtRegj = DateTime.Today,
                IdPerdoruesi = idPerdoruesi,
                IdRaportDesign = idRaportDesign,
                ColTrupiPunime = trupiPunime,
                ColTrupiSyri = trupiSyri
            }; 

            //rasti kur subjekti eshte furnitor
            var koka2 = new clsKokaRecetaOptike
            {
                NrDok = "1001",
                IdKlient = klienti2.IdKlientFurnitor,
                IdKrijuesi = idPerdoruesi,
                IdNdermarrje = idPerdoruesi,
                IdNderVit = idNderVit,
                IdKonfigAmbjente = idKonfigAmbjenti,
                IdNivel = nivelRegjistrimi.IdNivel,
                DtDok = DateTime.Today,
                DtRegj = DateTime.Today,
                IdPerdoruesi = idPerdoruesi,
                IdRaportDesign = idRaportDesign,
                ColTrupiPunime = trupiPunime,
                ColTrupiSyri = trupiSyri
            };

            //rasti kur klienti nuk eshte aktiv
            var koka3 = new clsKokaRecetaOptike
            {
                NrDok = "1003",
                IdKlient = klienti3.IdKlientFurnitor,
                IdKrijuesi = idPerdoruesi,
                IdNdermarrje = idNdermarrje,
                IdNderVit = idNderVit,
                IdKonfigAmbjente = konfigAmbienti.IdKonfigAmbjente,
                IdNivel = nivelRegjistrimi.IdNivel,
                DtDok = DateTime.Today,
                DtRegj = DateTime.Today,
                IdPerdoruesi = idPerdoruesi,
                IdRaportDesign = idRaportDesign,
                ColTrupiPunime = trupiPunime,
                ColTrupiSyri = trupiSyri
            };


            var mesazh = koka.Ruaj();
            var mesazh2 = koka2.Ruaj();
            var mesazh3 = koka3.Ruaj();

            Assert.IsTrue(mesazh.Status);
            Assert.IsTrue(!mesazh2.Status);
            Assert.IsTrue(!mesazh3.Status);
        }

        private colTrupiRecetaOptikePunime krijoRecetaOptike()
        {
            var col = new colTrupiRecetaOptikePunime
            {
                new clsTrupiRecetaOptikePunime
                {
                    IdArtikulliSyriDjathte = idArtikullDjathtas,
                    IdArtikulliSyriMajte = idArtikullMajtas,
                    ShenimeSyriDjathte = "shenime syri i dajathte",
                    ShenimeSyriMajte = "shenime syri i majte"
                }
            };
            return col;
        }
        private colTrupiRecetaOptikeSyri krijoRecetaOptikeSyri()
        {
          
            return new colTrupiRecetaOptikeSyri();
        }


        [TestMethod]
        public void ModifikoRecetaTest()
        {
            var recetat = new colKokaRecetaOptike(idNdermarrje);

            var kokaEkzistuese = new clsKokaRecetaOptike(recetat.Last().IdKoka);
         
            var mesazh = kokaEkzistuese.Modifiko();

            Assert.IsTrue(mesazh.Status);

        }

        [TestMethod]
        public void FshiRecetaTest()
        {
            var recetat = new colKokaRecetaOptike(idNdermarrje);
            var koka = new clsKokaRecetaOptike(recetat[recetat.Count - 1].IdKoka);

            var mesazh = koka.Fshi();

            Assert.IsTrue(mesazh.Status);
        }
    }
}
