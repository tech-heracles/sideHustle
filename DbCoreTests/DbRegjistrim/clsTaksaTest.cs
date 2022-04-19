using DbCore.DbRegjistrim;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DbCoreTests.Fake;

namespace DbCore.DbRegjistrim.Tests
{
    [TestClass()]
    public class clsTaksaTest
    {
        [TestMethod()]
        public void validoTakseNdermAktiveTestMeTakseNdermJoAktive()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { TakseNdermarje = true, Aktiv = false };

            //act
            bool statusi = taksa.validoTakseNdermAktive().Status;

            //assert
            Assert.AreEqual(statusi, false);
        }

        [TestMethod()]
        public void validoTakseNdermAktiveTestMeTakseNdermAktive()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { TakseNdermarje = true, Aktiv = true };

            //act
            bool statusi = taksa.validoTakseNdermAktive().Status;

            //assert
            Assert.AreEqual(statusi, true);
        }

        [TestMethod()]
        public void validoTakseNdermAktiveTestMeTakseJoNderm()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { TakseNdermarje = false};

            //act
            bool statusi = taksa.validoTakseNdermAktive().Status;

            //assert
            Assert.AreEqual(statusi, true);
        }


        [TestMethod()]
        public void validoFurnizimeZeroTestMeFurnizim0Check()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { FurnizimeZero = true, Njesia = "Perqindje", NormaPerqindje = 0, EPerjashtuar = false };

            //act
            bool statusi = taksa.validoFurnizimeZero().Status;

            //assert
            Assert.AreEqual(statusi, true);
        }

        [TestMethod()]
        public void validoFurnizimeZeroTestMeFurnizim0JoCheck()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { FurnizimeZero = false };

            //act
            bool statusi = taksa.validoFurnizimeZero().Status;

            //assert
            Assert.AreEqual(statusi, true);
        }

        [TestMethod()]
        public void validoFurnizimeZeroTestMeFurnizim0CheckMeVlere()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { FurnizimeZero = true, Njesia = "Vlere" };

            //act
            bool statusi = taksa.validoFurnizimeZero().Status;

            //assert
            Assert.AreEqual(statusi, false);
        }

        [TestMethod()]
        public void validoFurnizimeZeroTestMeFurnizim0CheckMePerqindjeJoZero()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { FurnizimeZero = true, Njesia = "Perqindje", NormaPerqindje = 5 };

            //act
            bool statusi = taksa.validoFurnizimeZero().Status;

            //assert
            Assert.AreEqual(statusi, false);
        }

        [TestMethod()]
        public void validoFurnizimeZeroTestMeFurnizim0CheckEPerjashtuar()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { FurnizimeZero = true, EPerjashtuar = true };

            //act
            bool statusi = taksa.validoFurnizimeZero().Status;

            //assert
            Assert.AreEqual(statusi, false);
        }

        [TestMethod()]
        public void validoTakseNdermMeLlojTreTestMeTaksNdermLlojTre()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { TakseNdermarje = true, IdLlojTakse = 3 };

            //act
            bool statusi = taksa.validoTakseNdermMeLlojTre().Status;

            //assert
            Assert.AreEqual(statusi, false);
        }

        [TestMethod()]
        public void validoTakseNdermMeLlojTreTestMeTaksJoNderm()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { TakseNdermarje = false };

            //act
            bool statusi = taksa.validoTakseNdermMeLlojTre().Status;

            //assert
            Assert.AreEqual(statusi, true);
        }

        [TestMethod()]
        public void validoTakseNdermMeLlojTreTestMeTaksNdermLlojJoTre()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { TakseNdermarje = true, IdLlojTakse = 5 };

            //act
            bool statusi = taksa.validoTakseNdermMeLlojTre().Status;

            //assert
            Assert.AreEqual(statusi, true);
        }

        [TestMethod()]
        public void validoTaksePaTvshMeLlojNjeTestPaTvshMeLlojNje()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { KodTaksa = "Pa TVSH", IdLlojTakse = 1 };

            //act
            bool statusi = taksa.validoTaksePaTvshMeLlojNje("Pa TVSH").Status;

            //assert
            Assert.AreEqual(statusi, false);
        }

        [TestMethod()]
        public void validoTaksePaTvshMeLlojNjeTestPaTvshMeLlojJoNje()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { KodTaksa = "Pa TVSH", IdLlojTakse = 5 };

            //act
            bool statusi = taksa.validoTaksePaTvshMeLlojNje("Pa TVSH").Status;

            //assert
            Assert.AreEqual(statusi, true);
        }

        [TestMethod()]
        public void validoTaksePaTvshMeLlojNjeTestJoPaTvsh()
        {
            //arrange
            clsTaksa taksa = new clsTaksa(new FakeMessages()) { KodTaksa = "Muco" };

            //act
            bool statusi = taksa.validoTaksePaTvshMeLlojNje("Pa TVSH").Status;

            //assert
            Assert.AreEqual(statusi, true);
        }
        
    }
}