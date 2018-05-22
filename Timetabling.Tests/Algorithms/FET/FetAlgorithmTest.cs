using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Timetabling.Algorithms.FET;
using Timetabling.Exceptions;
using Timetabling.Helper;

namespace Timetabling.Tests.Algorithms.FET
{

    [TestFixture]
    public class FetAlgorithmTest : FetAlgorithm
    {

        public FetAlgorithmTest() : base(null)
        {
        }

        [Test]
        public void IntegrationTest()
        {

            // Instantiate FET algorithm and run on Hopwood test file
            var fet = new FetAlgorithm();

            Assert.DoesNotThrow(() => fet.Execute("testIdentifier",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "United-Kingdom", "Hopwood", "Hopwood.fet")));
        }

        [Test]
        public void ConstructorTestMissingArgs()
        {

            var fet = new FetAlgorithm();

            // Check that the inner argument store is created when no addition arguments are passed in the constructor
            Assert.DoesNotThrow(() => fet.SetArgument("name", "value"));

        }

        [Test]
        public void ConstructorTestArgs()
        {

            var fet = new FetAlgorithm(new CommandLineArguments
            {
                { "key", "value" }
            });

            // Check the value is accessible
            Assert.AreEqual("value", fet.GetArgument("key"));
        }

        [Test]
        public void ArgumentTestGetSet()
        {

            // Instantiate FET algorithm
            var fet = new FetAlgorithm();

            fet.SetArgument("testargument", "testvalue");
            var expected = "testvalue";

            // Should throw a Win32Exception
            Assert.AreEqual(expected, fet.GetArgument("testargument"));

        }

        [Test]
        public void ArgumentTestRemove()
        {

            // Instantiate FET algorithm
            var fet = new FetAlgorithm();

            // Add argument
            fet.SetArgument("testargument", "testvalue");
            Assert.AreEqual("testvalue", fet.GetArgument("testargument"));

            // Remove argument
            fet.SetArgument("testargument", null);
            Assert.Throws<KeyNotFoundException>(() => fet.GetArgument("testargument"));

        }



        [Test]
        public void InitializeTestInputfileArgument()
        {

            var inputfile = "path_to_inputfile";

            // Instantiate FET algorithm
            var fet = new FetAlgorithmTest();

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
            var fet = new FetAlgorithmTest();

            // TODO: this test is currently useless, has to be refactored after FetAlgorithm is changed to accept db input source.
            // This statement is now executed in FetAlgorithm.Execute();
            fet.OutputDir = Util.CreateTempFolder("testIdentifier");

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
            var fet = new FetAlgorithmTest();

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
            var fet = new FetAlgorithm(new CommandLineArguments
            {
                {"timelimitseconds", "1" }
            });

            // Italy 2007 difficult usually takes more than one seconds
            var ex = Assert.Throws<AlgorithmException>(() => fet.Execute("testIdentifier",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "Italy", "2007", "difficult", "highschool-Ancona.fet")));

            Assert.AreEqual("No timetable is generated: the FET output file could not be found.", ex.Message);

            // Check that no input is generated
            // TODO: better test / implement this
            Assert.That(ex.InnerException, Is.TypeOf<FileNotFoundException>());
        }

        [Test]
        public void RunTestInvalidFetFile()
        {
            var fet = new FetAlgorithm();
            var ex = Assert.Throws<AlgorithmException>(() => fet.Execute("testIdentifier", 
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "activities_missing.fet")));

            Assert.AreEqual("Could not execute FET algorithm.", ex.Message);
        }

        [Test]
        public void RunTestInvalidNoFetFileExtension()
        {
            var fet = new FetAlgorithm();
            Assert.Throws<AlgorithmException>(() => fet.Execute("testIdentifier",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "books.xml")));
        }

    }
}
