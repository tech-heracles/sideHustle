using DbCore;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DbCoreTests.DbInventari
{
    [TestClass]
    public class clsArtikulliTest:FakeHttpContextBase
    {

        [TestMethod]
        public void ktheTVSHTest()
        {
          
            //duhet te rregullohet
            var aiQeDuhet = new ComboListTvsh[13];
            var llojtvsh = KonfigurimTVSHGjateRregj.Undefined;
            int idNdermarrje = 564;
            int idPerdoruesi = 3655;
            var idKF = 569487;
            clsTaksa taksendermarje = new clsTaksa();
            taksendermarje.mbushTakseDefaultNdermarrje(idNdermarrje);
            colTaksa taksaNdermarrje = new colTaksa(idNdermarrje, LlojTakse.Nivel_Tvsh, idPerdoruesi);
            var kf = new clsKlientFurnitor(idKF);
            clsTaksa taksaKF = new clsTaksa(kf.IdTvsh);
            var art = new clsArtikulli(572480);
            var comboListTvsh = art.ktheTvsh(llojtvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
            Assert.AreEqual(aiQeDuhet.Length, comboListTvsh.Length);
        }
        [TestMethod]
        public void MerrTvshSugjeruarTest()
        {
            return;
            //duhet te rregullohet
            ComboListTvsh tvshEPritshmeArtikullin = new ComboListTvsh
            {
                caktuar = "art",
                norma = 20.0000000000M,
                text = "Nivel 20",
                value = 1387
            };

            ComboListTvsh tvshEPritshmeKlienti = new ComboListTvsh
            {
                caktuar = "nderm",
                norma = 20.0000000000M,
                text = "Nivel 20",
                value = 1387
            };


            ComboListTvsh pritetPaAsnjeTVSH = new ComboListTvsh
            {
                caktuar = "nderm",
                norma = 20.0000000000M,
                text = "Nivel 20",
                value = 1387
            };

            Assert.AreEqual(MerrTvshSipasArtikullit(), tvshEPritshmeArtikullin);
            Assert.AreEqual(MerrTvshSipasKlientit(), tvshEPritshmeKlienti);
            Assert.AreEqual(MerrPaAsnjeTVSH(), pritetPaAsnjeTVSH);


        }

        private static ComboListTvsh MerrTvshSipasArtikullit()
        {
            var art = new clsArtikulli(572480);
            var obj = new PrivateObject(art);
            var llojtvsh = KonfigurimTVSHGjateRregj.Sipas_Artikullit;
            int idNdermarrje = 564;
            int idPerdoruesi = 3655;
            var idKF = 569487;

            clsTaksa taksendermarje = new clsTaksa();
            taksendermarje.mbushTakseDefaultNdermarrje(idNdermarrje);
            colTaksa taksaNdermarrje = new colTaksa(idNdermarrje, LlojTakse.Nivel_Tvsh, idPerdoruesi);
            var kf = new clsKlientFurnitor(idKF);
            clsTaksa taksaKF = new clsTaksa(kf.IdTvsh);
            return (ComboListTvsh)obj.Invoke("MerrTvshTeSugjeruar", llojtvsh, taksendermarje, taksaKF);
        }

        private static ComboListTvsh MerrTvshSipasKlientit()
        {
            var art = new clsArtikulli(572480);
            var obj = new PrivateObject(art);
            var llojtvsh = KonfigurimTVSHGjateRregj.Sipas_Klientit;
            int idNdermarrje = 564;
            int idPerdoruesi = 3655;
            var idKF = 569487;

            clsTaksa taksendermarje = new clsTaksa();
            taksendermarje.mbushTakseDefaultNdermarrje(idNdermarrje);
            colTaksa taksaNdermarrje = new colTaksa(idNdermarrje, LlojTakse.Nivel_Tvsh, idPerdoruesi);
            var kf = new clsKlientFurnitor(idKF);
            clsTaksa taksaKF = new clsTaksa(kf.IdTvsh);
            return (ComboListTvsh)obj.Invoke("MerrTvshTeSugjeruar", llojtvsh, taksendermarje, taksaKF);
        }

        private static ComboListTvsh MerrPaAsnjeTVSH()
        {
            var art = new clsArtikulli(572480);
            var obj = new PrivateObject(art);
            var llojtvsh = KonfigurimTVSHGjateRregj.Undefined;
            int idNdermarrje = 564;
            int idPerdoruesi = 3655;
            var idKF = 569487;

            clsTaksa taksendermarje = new clsTaksa();
            taksendermarje.mbushTakseDefaultNdermarrje(idNdermarrje);
            colTaksa taksaNdermarrje = new colTaksa(idNdermarrje, LlojTakse.Nivel_Tvsh, idPerdoruesi);
            var kf = new clsKlientFurnitor(idKF);
            clsTaksa taksaKF = new clsTaksa(kf.IdTvsh);
            return (ComboListTvsh)obj.Invoke("MerrTvshTeSugjeruar", llojtvsh, taksendermarje, taksaKF);
        }





        [TestMethod]
        public void MundTePorositetTest()
        {
            clsArtikulli artikulli = new clsArtikulli
            {
                KodArtikulli = "KOD",
                StokuMaxVfOne = 5
            };
            clsArtikulli artikulli_blere_nga_dealer = new clsArtikulli
            {
                KodArtikulli = "KOD_blere_nga_dealer",
                StokuMaxVfOne = 5,
                IRezervueshem = true
            };

            Assert.IsFalse(artikulli.MundTePorositet(0, 0, 0, 0, false, 34, true, true));
            Assert.IsFalse(artikulli_blere_nga_dealer.MundTePorositet(0, 0, 0, 0, false, 34, true, true));

            artikulli.IRezervueshem = true;
            Assert.IsFalse(artikulli.MundTePorositet(0, 5, 0, 0, false, 34, true, false));

            //rastet per bazaar
            Assert.IsFalse(artikulli.MundTePorositet(0, 0, 0, 0, false, 0, true, true));
            Assert.IsFalse(artikulli.MundTePorositet(0, 0, 0, 0, false, -5, true, true));
            Assert.IsTrue(artikulli.MundTePorositet(0, 0, 0, 0, false, 5, true, true));
            Assert.IsTrue(artikulli.MundTePorositet(-5, 0, 0, 0, false, 5, true, true));
            Assert.IsFalse(artikulli.MundTePorositet(5, 0, 0, 0, false, 5, true, true));
            Assert.IsTrue(artikulli.MundTePorositet(0, 0, 0, 0, true, 5, true, true));

            //rastet per vfone
            Assert.IsFalse(artikulli.MundTePorositet(6, 0, 0, 0, true, 5, false, true));
            Assert.IsFalse(artikulli.MundTePorositet(0, 0, 0, 0, true, 5, false, true));
            Assert.IsFalse(artikulli.MundTePorositet(0, 0, -9, 0, true, 5, false, true));
            Assert.IsTrue(artikulli.MundTePorositet(0, 0, 3, 3, true, 5, false, true));
            Assert.IsFalse(artikulli.MundTePorositet(0, 0, 5, 5, true, 5, false, true));
            Assert.IsFalse(artikulli.MundTePorositet(0, 0, 7, 7, true, 5, false, true));

            //te tjera
            Assert.IsTrue(artikulli.MundTePorositet(0, 0, 7, 7, false, 5, false, false));
            Assert.IsTrue(artikulli.MundTePorositet(-10, 0, 7, 7, false, 5, false, false));
            Assert.IsFalse(artikulli.MundTePorositet(5, 0, 7, 7, false, 5, false, false));
            Assert.IsTrue(artikulli.MundTePorositet(5, 0, 7, 7, true, 5, false, false));


            Assert.IsFalse(artikulli.MundTePorositet(0, 5, 7, 0, true, 5, false, true));
        }


    }

}

