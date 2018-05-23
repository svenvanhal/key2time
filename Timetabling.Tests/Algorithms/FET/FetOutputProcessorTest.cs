using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using Timetabling.Algorithms.FET;
using Timetabling.Exceptions;

namespace Timetabling.Tests.Algorithms.FET
{

    [TestFixture]
    internal class FetOutputProcessorTest
    {

        [Test]
        public void GetTimetableTest()
        {

            var testDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata/fet/United-Kingdom/Hopwood/output.xml");
            var testData = File.ReadAllBytes(testDataPath);

            // Create dummy FET output
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"Hopwood_activities.xml", new MockFileData(testData) },
                { @"Hopwood_data_and_timetable.fet", new MockFileData("Original FET file") },
                { @"Hopwood_soft_conflicts.txt", new MockFileData("Soft conflicts in solution") }
            });

            // Run
            var fop = new FetOutputProcessor("Hopwood", fileSystem.Directory.GetCurrentDirectory(), fileSystem);

            var tt = fop.GetTimetable();

            // Check that we have found all 163 activities
            Assert.AreEqual(163, tt.Activities.Count);

        }

        [Test]
        public void CleanupOutputDirTest()
        {

            // Create dummy FET output
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"outputdir/Hopwood_activities.xml", new MockFileData("Generated timetable") },
                { @"outputdir/Hopwood_data_and_timetable.fet", new MockFileData("Original FET file") },
                { @"outputdir/Hopwood_soft_conflicts.txt", new MockFileData("Soft conflicts in solution") },
            });

            var fopDir = fileSystem.Path.Combine(fileSystem.Directory.GetCurrentDirectory(), "outputdir");
            var fop = new FetOutputProcessor("Hopwood", fopDir, fileSystem);

            Assert.IsTrue(fileSystem.Directory.Exists(fopDir));

            // Run
            fop.CleanupOutputDir();

            // Check that we have found all 163 activities
            Assert.IsFalse(fileSystem.Directory.Exists(fopDir));

        }

        [Test]
        public void XmlToTimetableTest()
        {

            var fop = new FetOutputProcessor("", "");

            // Create output file stream
            var testDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata/fet/United-Kingdom/Hopwood/output.xml");
            using (var outputFileStream = File.OpenRead(testDataPath))
            {
                // Deserialize XML
                Assert.DoesNotThrow(() => fop.XmlToTimetable(outputFileStream));
            }

        }

        [Test]
        public void CorruptXmlToTimetableTest()
        {

            const string contents = "Hey this is not XML!";

            var fop = new FetOutputProcessor("", "");
            var byteArray = Encoding.UTF8.GetBytes(contents);

            // Create output file stream
            using (var outputFileStream = new MemoryStream(byteArray))
            {
                // Deserialize XML
                var ex = Assert.Throws<AlgorithmException>(() => fop.XmlToTimetable(outputFileStream));
                Assert.That(ex.InnerException, Is.TypeOf<InvalidOperationException>());
            }

        }

        [Test]
        public void EmptyXmlToTimetableTest()
        {

            var testDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata/fet/United-Kingdom/Hopwood/empty.xml");
            var testData = File.ReadAllBytes(testDataPath);

            // Create dummy FET output
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"Hopwood_activities.xml", new MockFileData(testData) }
            });

            // Run
            var fop = new FetOutputProcessor("Hopwood", fileSystem.Directory.GetCurrentDirectory(), fileSystem);

            var tt = fop.GetTimetable();

            // Check that we have found all 163 activities
            Assert.AreEqual(0, tt.Activities.Count);

        }

        [Test]
        public void InvalidXmlToTimetableTest()
        {

            var testDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata/fet/United-Kingdom/Hopwood/invalid.xml");
            var testData = File.ReadAllBytes(testDataPath);

            // Create dummy FET output
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"Hopwood_activities.xml", new MockFileData(testData) }
            });

            // Run
            var fop = new FetOutputProcessor("Hopwood", fileSystem.Directory.GetCurrentDirectory(), fileSystem);

            // Check that we have found all 163 activities
            var ex = Assert.Throws<AlgorithmException>(() => fop.GetTimetable());
            Assert.That(ex.InnerException, Is.TypeOf<InvalidOperationException>());

        }

    }
}
