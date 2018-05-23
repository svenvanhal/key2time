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

            var fakeInterface = new Mock<FetProcessInterface>(fpb.CreateProcess())
            {
                CallBase = true
            };

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
            // TODO: implement
        }

    }
}
