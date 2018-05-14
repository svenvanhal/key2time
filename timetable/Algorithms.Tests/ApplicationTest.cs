using NUnit.Framework;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace Timetable.Algorithm.Tests
{

    class FetAlgorithmTest
    {

        readonly string fetPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Util.GetAppSetting("FetBinaryLocation"));

        [Test]
        public void IntegrationTest()
        {

            // Instantiate FET algorithm and run on Hopwood test file
            FetAlgorithm fet = new FetAlgorithm(fetPath);
            Timetable result = fet.Run("testdata/fet/United-Kingdom/Hopwood/Hopwood.fet");

            Assert.IsNotNull(result);
        }

        [Test]
        public void NoExecutableProvidedTest()
        {

            // Instantiate FET algorithm with a null argument
            FetAlgorithm fet = new FetAlgorithm(null);

            // Should throw a InvalidOperationException
            Assert.Throws<InvalidOperationException>(() => fet.Run(""));

        }

        [Test]
        public void ExecutableNotFoundTest()
        {

            // Instantiate FET algorithm with a non existing executable
            FetAlgorithm fet = new FetAlgorithm("non_existing_file");

            // Should throw a Win32Exception
            Assert.Throws<Win32Exception>(() => fet.Run(""));

        }

    }
}
