using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbCore.DbRegjistrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore;
using DbCore.DbInventari;

namespace DbCoreTests.DbRegjistrim
{
    [TestClass]
    public class colKokaMagazinaTests
    {
        [TestMethod]
        public void TestoBashkimTrupi()
        {
            var data = DateTime.Now;
            var art1 = new clsArtikulli
            {
                IdArtikulli = 1,
                IdFormatSeriali = 1
            };
            var art2 = new clsArtikulli
            {
                IdArtikulli = 2,
                IdFormatSeriali = 1
            };
            var art3 = new clsArtikulli
            {
                IdArtikulli = 3,
                IdFormatSeriali = 0
            };
            colSerialeUnikeKategori kategori = new colSerialeUnikeKategori();
            clsSerialeUnikeKategori kategoria = new clsSerialeUnikeKategori
            {
                ID = 1,
                Kategori = "APARATE",
                ColFormateSeriali = new colSerialeUnikeFormate()
            };
            kategoria.ColFormateSeriali.Add(new clsSerialeUnikeFormate
            {
                ID = 1,
                IdKategoriSeriali = 1,
            });
            kategori.Add(kategoria);

            colTrupiMagazina col = new colTrupiMagazina();
            col.Add(new clsTrupiMagazina
            {
                IdArtikulli = 1,
                KodiArtikull = "1",
                PershkrimArtikull = "1",
                IdLlojVeprimi = 1,
                IdNjesia = 1,
                IdKokaMagazina = 0,
                Koeficenti = 1,
                Shenja = 1,
                IdMag = 1,
                Data = data,
                IdArtikullSet = 0,
                IdBarkodi = 0,
                Sasia = 1,
                Cmimi = 3,
                Vlefta = 3,
                Shenime = "1",
                Element = art1
            });
            col.Add(new clsTrupiMagazina
            {
                IdArtikulli = 1,
                KodiArtikull = "1",
                PershkrimArtikull = "1",
                IdLlojVeprimi = 1,
                IdNjesia = 1,
                IdKokaMagazina = 0,
                Koeficenti = 1,
                Shenja = 1,
                IdMag = 1,
                Data = data,
                IdArtikullSet = 0,
                IdBarkodi = 0,
                Sasia = 1,
                Cmimi = 5,
                Vlefta = 5,
                Shenime = "2",
                Element = art1

            });

            colTrupiMagazina col2 = new colTrupiMagazina();
            col2.Add(new clsTrupiMagazina
            {
                IdArtikulli = 1,
                KodiArtikull = "1",
                PershkrimArtikull = "1",
                IdLlojVeprimi = 1,
                IdNjesia = 1,
                IdKokaMagazina = 0,
                Koeficenti = 1,
                Shenja = 1,
                IdMag = 1,
                Data = data,
                IdArtikullSet = 0,
                IdBarkodi = 0,
                Sasia = 1,
                Cmimi = 3,
                Vlefta = 3,
                Shenime = "1",
                Element = art1
            });
            col2.Add(new clsTrupiMagazina
            {
                IdArtikulli = 1,
                KodiArtikull = "1",
                PershkrimArtikull = "1",
                IdLlojVeprimi = 1,
                IdNjesia = 1,
                IdKokaMagazina = 0,
                Koeficenti = 1,
                Shenja = 1,
                IdMag = 2,
                Data = data,
                IdArtikullSet = 0,
                IdBarkodi = 0,
                Sasia = 1,
                Cmimi = 5,
                Vlefta = 5,
                Shenime = "2",
                Element = art1

            });
            col2.Add(new clsTrupiMagazina
            {
                IdArtikulli = 2,
                KodiArtikull = "2",
                PershkrimArtikull = "2",
                IdLlojVeprimi = 1,
                IdNjesia = 1,
                IdKokaMagazina = 0,
                Koeficenti = 1,
                Shenja = 1,
                IdMag = 1,
                Data = data,
                IdArtikullSet = 0,
                IdBarkodi = 0,
                Sasia = 1,
                Cmimi = 5,
                Vlefta = 5,
                Shenime = "2",
                Element = art2

            });
            col2.Add(new clsTrupiMagazina
            {
                IdArtikulli = 3,
                KodiArtikull = "2",
                PershkrimArtikull = "2",
                IdLlojVeprimi = 1,
                IdNjesia = 1,
                IdKokaMagazina = 0,
                Koeficenti = 1,
                Shenja = 1,
                IdMag = 1,
                Data = data,
                IdArtikullSet = 0,
                IdBarkodi = 0,
                Sasia = 1,
                Cmimi = 5,
                Vlefta = 5,
                Shenime = "2",
                Element = art3

            });


            col.BashkoTrupin(kategori);
            col2.BashkoTrupin(kategori);




            Assert.IsTrue(col.Count == 1);
            Assert.IsTrue(col[0].Sasia == 2 && col[0].Vlefta == 8);
            Assert.IsTrue(col[0].Shenime == "1");
            Assert.IsTrue(col2.Count == 3);
            Assert.IsTrue(col2[0].IdMag == 1);
        }
    }
}
