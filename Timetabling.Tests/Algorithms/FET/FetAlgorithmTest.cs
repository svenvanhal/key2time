using NUnit.Framework;
using System;
using System.IO;
using Timetabling.Algorithms.FET;
using Timetabling.Exceptions;
using Timetabling.Helper;
using Timetabling.Resources;

namespace Timetabling.Tests.Algorithms.FET
{

    internal class FetAlgorithmExposer : FetAlgorithm
    {
        public FetAlgorithmExposer(CommandLineArguments args = null) : base(args)
        {
        }

        public void Initialize(string input) => base.Initialize(input);
        public void Run() => base.Run();
        public Timetable GetResult() => base.GetResult();

    }

    [TestFixture]
    public class FetAlgorithmTest : FetAlgorithm
    {

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
