using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using Moq;
using Timetabling.Algorithms.FET;
using Timetabling.Exceptions;
using Timetabling.Helper;
using Timetabling.Resources;

namespace Timetabling.Tests.Algorithms.FET
{

    public class FetAlgorithmExposer : FetAlgorithm
    {
        public new void Initialize(string input) => base.Initialize(input);
        public new void Run() => base.Run();
        public new Timetable GetResult() => base.GetResult();
    }

    [TestFixture]
    public class FetAlgorithmTest
    {

        [Test]
        public void InputPropertiesTest()
        {
            var expectedFile = "DummyInputFile.fet";
            var expectedName = "DummyInputFile";

            var algo = new FetAlgorithm
            {
                InputFile = expectedFile,
            };

            Assert.AreEqual(expectedFile, algo.InputFile);
            Assert.AreEqual(expectedName, algo.InputName);

        }

        [Test]
        public void ExecuteTest()
        {

            var algo = new FetAlgorithm();

            // Run algorithm on test data
            var result = algo.Execute("testIdentifier",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "United-Kingdom", "Hopwood", "Hopwood.fet"));

            // Verify that the result is not null
            Assert.IsNotNull(result);
        }

        [Test]
        public void InitializeTest()
        {
            var algo = new FetAlgorithmExposer
            {
                Identifier = "testIdentifier"
            };

            var expected = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "United-Kingdom", "Hopwood", "Hopwood.fet");
            var preOutputDir = algo.OutputDir;

            algo.Initialize(expected);

            Assert.AreEqual(expected, algo.InputFile);
            Assert.AreNotEqual(preOutputDir, algo.OutputDir);
            Assert.IsNotNull(algo.ProcessInterface);
        }

        [Test]
        public void RunTest()
        {
            var algo = new FetAlgorithmExposer();

            // Setup process builder
            var fpb = new FetProcessBuilder(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib", "fet", "fet-cl"));
            fpb.SetInputFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "United-Kingdom", "Hopwood", "Hopwood.fet"));
            fpb.SetOutputDir(Util.CreateTempFolder("testIdentifier"));

            var fakeInterface = new Mock<FetProcessInterface>(fpb.CreateProcess());

            algo.ProcessInterface = fakeInterface.Object;

            // Run algorithm
            algo.Run();

            // Verify the required methods are called
            fakeInterface.Verify(mock => mock.StartProcess(), Times.Once);
            fakeInterface.Verify(mock => mock.TerminateProcess(), Times.Once);

        }

        [Test]
        public void GetResultTest()
        {
            throw new NotImplementedException();
        }

        //[Test]
        //public void IntegrationTest()
        //{

        //    // Instantiate FET algorithm and run on Hopwood test file
        //    var fet = new FetAlgorithm();

        //    Assert.DoesNotThrow(() => fet.Execute("testIdentifier",
        //        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "United-Kingdom", "Hopwood", "Hopwood.fet")));
        //}

        //[Test]
        //public void ConstructorTestMissingArgs()
        //{

        //    var fet = new FetAlgorithm();

        //    // Check that the inner argument store is created when no addition arguments are passed in the constructor
        //    //Assert.DoesNotThrow(() => fet.SetArgument("name", "value"));

        //}

        //[Test]
        //public void ConstructorTestArgs()
        //{

        //    var fet = new FetAlgorithm(new CommandLineArguments
        //    {
        //        {"key", "value"}
        //    });

        //    // Check the value is accessible
        //    //Assert.AreEqual("value", fet.GetArgument("key"));
        //}


        //[Test]
        //public void InitializeTestInputfileArgument()
        //{

        //    var inputfile = "path_to_inputfile";

        //    // Instantiate FET algorithm
        //    var fet = new FetAlgorithmExposer();

        //    // Initialize
        //    fet.Initialize(inputfile);

        //    // Check if the inputfile is set correctly
        //    //Assert.AreEqual(inputfile, fet.GetArgument("inputfile"));

        //}

        //[Test]
        //[MaxTime(5000)] // Safe margin
        //public void RunTestTimelimitExceeded()
        //{

        //    // Instantiate FET algorithm
        //    var fet = new FetAlgorithm(new CommandLineArguments
        //    {
        //        {"timelimitseconds", "1" }
        //    });

        //    // Italy 2007 difficult usually takes more than one seconds
        //    var ex = Assert.Throws<AlgorithmException>(() => fet.Execute("testIdentifier",
        //        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "Italy", "2007", "difficult", "highschool-Ancona.fet")));

        //    Assert.AreEqual("No timetable is generated: the FET output file could not be found.", ex.Message);

        //    // Check that no input is generated
        //    // TODO: better test / implement this
        //    Assert.That(ex.InnerException, Is.TypeOf<FileNotFoundException>());
        //}

        //[Test]
        //public void RunTestInvalidFetFile()
        //{
        //    var fet = new FetAlgorithm();
        //    var ex = Assert.Throws<AlgorithmException>(() => fet.Execute("testIdentifier", 
        //        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "activities_missing.fet")));

        //    Assert.AreEqual("Could not execute FET algorithm.", ex.Message);
        //}

        //[Test]
        //public void RunTestInvalidNoFetFileExtension()
        //{
        //    var fet = new FetAlgorithm();
        //    Assert.Throws<AlgorithmException>(() => fet.Execute("testIdentifier",
        //        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "books.xml")));
        //}

    }
}
