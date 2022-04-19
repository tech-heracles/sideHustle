using DbCore;
using DbCore.DbInventari;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbCoreTests.DbInventari
{
    [TestClass]
    public class clsSerialeUnikeTest : FakeHttpContextBase
    {
        [TestMethod]
        public void ruajSerialetUnikeAparateVodafone()
        {
            clsMesazh mesazh = new MesazhGabimi();
            clsSerialeUnike aparateVod = new clsSerialeUnike("IMEI", "Aparate Vodafone", enumSerialeUnike_Lloje.NUMERIKE, "=15", @"\d{15}", 3655, 564,1, true, true);
            if (!aparateVod.ekziston())
                mesazh = aparateVod.Ruaj();
            else
                mesazh = new MesazhSuksesi();
            Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void ruajSerialetUnikeKartaMSISDNVodafone()
        {
            clsMesazh mesazh = new MesazhGabimi();
            clsSerialeUnike kartaMSISDNVod = new clsSerialeUnike("MSISDN", "Karta Vodafone MSISDN", enumSerialeUnike_Lloje.NUMERIKE, "=10", @"069\d{7}", 3655, 564,1, true, false);
            if (!kartaMSISDNVod.ekziston())
                mesazh = kartaMSISDNVod.Ruaj();
            else
                mesazh = new MesazhSuksesi();
            Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void ruajSerialetUnikeKartaSerialeVodafone()
        {
            clsMesazh mesazh = new MesazhGabimi();
            clsSerialeUnike kartaSerialeVod = new clsSerialeUnike("SERIAL", "Karta Vodafone Serial", enumSerialeUnike_Lloje.NUMERIKE, "=18", @"\d{18}", 3655, 564,1, true, true);
            if (!kartaSerialeVod.ekziston())
                mesazh = kartaSerialeVod.Ruaj();
            else
                mesazh = new MesazhSuksesi();
            Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void ruajSerialetUnikeRingarkuesVodafone()
        {
            clsMesazh mesazh = new MesazhGabimi();
            clsSerialeUnike ringarkuesVod = new clsSerialeUnike("PACKSERIALNO", "Karta Vodafone Serial", enumSerialeUnike_Lloje.NUMERIKE, "=18", @"^\s*(\d{6}-00(0[0-9]|10)-00(0[0-9]|10))\z", 3655, 564,1, true, true);
            if (!ringarkuesVod.ekziston())
                mesazh = ringarkuesVod.Ruaj();
            else
                mesazh = new MesazhSuksesi();
            Assert.AreEqual(mesazh.Status, true, mesazh.PershkrimMesazhi);
        }

        [TestMethod]
        public void ekzistonSerialetUnikeVodafone()
        {
            clsSerialeUnike test = new clsSerialeUnike("TEST", "Karta Vodafone Serial", enumSerialeUnike_Lloje.NUMERIKE, "=18", @"^\s*(\d{6}-00(0[0-9]|10)-00(0[0-9]|10))\z", 3655, 564,1, true, true);
            Assert.IsFalse(test.ekziston());
        }

        [TestMethod]
        public void mbushGjitheSerialetUnikeVodafone()
        {
            colSerialeUnike seriale = new colSerialeUnike(564);
            Assert.IsNotNull(seriale);
        }
    }
}
