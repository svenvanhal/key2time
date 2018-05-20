using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Timetabling.Exceptions;
using Timetabling.Helper;

namespace Timetabling.Algorithms.Tests
{

    [TestFixture(null, null)]
    public class FetAlgorithmTest : FetAlgorithm
    {

        readonly string fetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Util.GetAppSetting("FetBinaryLocation"));

        [Test]
        public void IntegrationTest()
        {

            // Instantiate FET algorithm and run on Hopwood test file
            var fet = new FetAlgorithm(fetPath);

            Assert.DoesNotThrow(() => fet.Execute(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata/fet/United-Kingdom/Hopwood/Hopwood.fet")));
        }

        [Test]
        public void ConstructorTestMissingArgs()
        {

            var fet = new FetAlgorithm(fetPath);

            // Check that the inner argument store is created when no addition arguments are passed in the constructor
            Assert.DoesNotThrow(() => fet.SetArgument("name", "value"));

        }

        [Test]
        public void ConstructorTestArgs()
        {

            var fet = new FetAlgorithm(fetPath, new CommandLineArguments
            {
                { "key", "value" }
            });

            // Check the value is accessible
            Assert.AreEqual("value", fet.GetArgument("key"));
        }

        [Test]
        public void NoExecutableProvidedTest()
        {

            // Instantiate FET algorithm with a null argument
            var fet = new FetAlgorithmTest(null);

            // Should throw a InvalidOperationException
            var ex = Assert.Throws<AlgorithmException>(() => fet.Run());
            Assert.That(ex.InnerException, Is.TypeOf<InvalidOperationException>());

        }

        [Test]
        public void ExecutableNotFoundTest()
        {

            // Instantiate FET algorithm with a non existing executable
            var fet = new FetAlgorithmTest("non_existing_file");

            // Should throw a Win32Exception
            var ex = Assert.Throws<AlgorithmException>(() => fet.Run());
            Assert.That(ex.InnerException, Is.TypeOf<Win32Exception>());

        }

        [Test]
        public void ArgumentTestGetSet()
        {

            // Instantiate FET algorithm
            var fet = new FetAlgorithm(null);

            fet.SetArgument("testargument", "testvalue");
            var expected = "testvalue";

            // Should throw a Win32Exception
            Assert.AreEqual(expected, fet.GetArgument("testargument"));

        }

        [Test]
        public void ArgumentTestRemove()
        {

            // Instantiate FET algorithm
            var fet = new FetAlgorithm(null);

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
            var fet = new FetAlgorithmTest(null);

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
            var fet = new FetAlgorithmTest(null);

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
            var fet = new FetAlgorithmTest(null);

            // Initialize
            fet.Initialize(inputfile);

            // Check if the output dir exists
            Assert.IsTrue(Directory.Exists(fet.GetArgument("outputdir")));

        }

        [Test]
        public void RunTestInputFileMissing()
        {

            var inputfile = "non_existing_file";

            // Instantiate FET algorithm
            var fet = new FetAlgorithmTest(fetPath);

            // Initialize
            fet.Initialize(inputfile);

            // Should throw a nested AlgorithmException
            var ex = Assert.Throws<AlgorithmException>(() => fet.Run());
            Assert.That(ex.InnerException, Is.TypeOf<AlgorithmException>());

        }

        [Test]
        [MaxTime(5000)] // Safe margin
        public void RunTestTimelimitExceeded()
        {

            // Instantiate FET algorithm
            var fet = new FetAlgorithm(fetPath, new CommandLineArguments
            {
                {"timelimitseconds", "1" }
            });

            // Italy 2007 difficult usually takes more than one seconds
            var ex = Assert.Throws<AlgorithmException>(() => fet.Execute(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata/fet/Italy/2007/difficult/highschool-Ancona.fet")));

            // Check that no input is generated
            // TODO: better test / implement this
            Assert.That(ex.InnerException, Is.TypeOf<FileNotFoundException>());
        }

        [Test]
        public void RunTestInvalidFetFile()
        {
            var fet = new FetAlgorithm(fetPath);
            Assert.Throws<AlgorithmException>(() => fet.Execute("testdata/fet/activities_missing.fet"));
        }

        [Test]
        public void RunTestInvalidNoFetFileExtension()
        {
            var fet = new FetAlgorithm(fetPath);
            Assert.Throws<AlgorithmException>(() => fet.Execute("testdata/books.xml"));
        }

        public FetAlgorithmTest(string executableLocation, CommandLineArguments args = null) : base(executableLocation, args)
        {
        }
    }
}
