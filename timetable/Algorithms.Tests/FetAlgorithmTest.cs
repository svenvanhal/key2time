using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Timetabling.Helper;
using Timetabling.Resources;

namespace Timetabling.Algorithm.Tests
{

    // TODO: write tests for FET-CL output (catch different error codes)
    class FetAlgorithmTest
    {

        readonly string fetPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Util.GetAppSetting("FetBinaryLocation"));

        [Test]
        public void IntegrationTest()
        {

            // Instantiate FET algorithm and run on Hopwood test file
            FetAlgorithm fet = new FetAlgorithm(fetPath);
            fet.Initialize("testdata/fet/United-Kingdom/Hopwood/Hopwood.fet");
            fet.Run();

            var result = fet.GetResult();

            Assert.IsNotNull(result);
        }

        [Test]
        public void NoExecutableProvidedTest()
        {

            // Instantiate FET algorithm with a null argument
            FetAlgorithm fet = new FetAlgorithm(null);

            // Should throw a InvalidOperationException
            Assert.Throws<InvalidOperationException>(() => fet.Run());

        }

        [Test]
        public void ExecutableNotFoundTest()
        {

            // Instantiate FET algorithm with a non existing executable
            FetAlgorithm fet = new FetAlgorithm("non_existing_file");

            // Should throw a Win32Exception
            Assert.Throws<Win32Exception>(() => fet.Run());

        }

        [Test]
        public void ArgumentTestGetSet()
        {

            // Instantiate FET algorithm
            FetAlgorithm fet = new FetAlgorithm(null);

            fet.SetArgument("testargument", "testvalue");
            var expected = "testvalue";

            // Should throw a Win32Exception
            Assert.AreEqual(expected, fet.GetArgument("testargument"));

        }

        [Test]
        public void ArgumentTestRemove()
        {

            // Instantiate FET algorithm
            FetAlgorithm fet = new FetAlgorithm(null);

            // Add argument
            fet.SetArgument("testargument", "testvalue");
            Assert.AreEqual("testvalue", fet.GetArgument("testargument"));

            // Remove argument
            fet.SetArgument("testargument", null);
            Assert.Throws<KeyNotFoundException>(() => fet.GetArgument("testargument"));

        }

        [Test]
        public void InitializeTestRefreshId()
        {

            // Instantiate FET algorithm
            FetAlgorithm fet = new FetAlgorithm(null);

            // Initialize twice
            fet.Initialize("path_to_inputfile");
            var pre_id = fet.CurrentRunIdentifier;
            fet.Initialize("path_to_inputfile");

            // Check if identifier is refreshed
            Assert.AreNotEqual(pre_id, fet.CurrentRunIdentifier);

        }

        [Test]
        public void InitializeTestInputfileArgument()
        {

            var inputfile = "path_to_inputfile";

            // Instantiate FET algorithm
            FetAlgorithm fet = new FetAlgorithm(null);

            // Initialize
            fet.Initialize(inputfile);

            // Check if the inputfile is set correctly
            Assert.AreEqual(inputfile, fet.GetArgument("inputfile"));

        }

        [Test]
        public void InitializeTestOutputdirExists()
        {

            var inputfile = "path_to_inputfile";

            // Instantiate FET algorithm
            FetAlgorithm fet = new FetAlgorithm(null);

            // Initialize
            fet.Initialize(inputfile);

            // Check if the output dir exists
            Assert.IsTrue(Directory.Exists(fet.GetArgument("outputdir")));

        }

    }
}
