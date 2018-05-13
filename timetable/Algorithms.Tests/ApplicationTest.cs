using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace Timetable.Algorithm.Tests
{

    class FetAlgorithmTest
    {

        [OneTimeSetUp]
        public void ChangeCwd()
        {

            // Change current working directory
            Directory.SetCurrentDirectory(TestContext.CurrentContext.TestDirectory);
        }

        [Test]
        public void IntegrationTest()
        {

            // Instantiate FET algorithm
            FetAlgorithm fet = new FetAlgorithm(Util.GetAppSetting("FetAlgorithmExecutableLocation"));

            // Run algorithm on Hopwood test file
            Timetable result = fet.Run("testdata/fet/United-Kingdom/Hopwood/Hopwood.fet");

            Assert.IsNotNull(result);
        }

    }
}
