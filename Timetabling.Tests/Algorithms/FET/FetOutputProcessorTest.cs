using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using NUnit.Framework;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using Timetabling.Algorithms.FET;
using Timetabling.Objects;
using Timetabling.Resources;

namespace Timetabling.Tests.Algorithms.FET
{

    [TestFixture]
    internal class FetOutputProcessorTest
    {

        internal class FetOutputProcessorExposer : FetOutputProcessor
        {
            public FetOutputProcessorExposer(string inputName, string outputDir) : base(inputName, outputDir, new FileSystem()) { }
            public new Timetable XmlToTimetable(Stream fileStream, IDictionary<int, Activity> activities) => base.XmlToTimetable(fileStream, activities);
            public new List<string> ParseSoftConflicts(StreamReader reader) => base.ParseSoftConflicts(reader);
            public new void ParseMetaLine(string line, Timetable tt) => base.ParseMetaLine(line, tt);
            public new void AddMetadata(Timetable tt) => base.AddMetadata(tt);
        }

        [Test]
        public void GetTimetableTest()
        {

            var testDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata/fet/United-Kingdom/Hopwood/output.xml");
            var testData = File.ReadAllBytes(testDataPath);

            // Create dummy FET output
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"Hopwood\Hopwood_activities.xml", new MockFileData(testData) },
                { @"Hopwood\Hopwood_data_and_timetable.fet", new MockFileData("Original FET file") },
                { @"Hopwood\Hopwood_soft_conflicts.txt", new MockFileData("Soft conflicts in solution") }
            });

            // Run
            var fop = new FetOutputProcessor("Hopwood", fileSystem.Directory.GetCurrentDirectory(), fileSystem);

            var tt = fop.GetTimetable(null);

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

            var fop = new FetOutputProcessorExposer("", "");

            // Create output file stream
            var testDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata/fet/United-Kingdom/Hopwood/output.xml");
            using (var outputFileStream = File.OpenRead(testDataPath))
            {
                // Deserialize XML
                Assert.DoesNotThrow(() => fop.XmlToTimetable(outputFileStream, null));
            }

        }

        [Test]
        public void XmlToTimetableActivitiesTest()
        {

            var fop = new FetOutputProcessorExposer("", "");

            var expected = new Activity {Id = 3};
            var activities = new Dictionary<int, Activity> {{ 3, expected }};

            // Create output file stream
            var testDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata/fet/United-Kingdom/Hopwood/output.xml");
            using (var outputFileStream = File.OpenRead(testDataPath))
            {
                var tt = fop.XmlToTimetable(outputFileStream, activities);

                // Find activity in timetable output and check that it's associated resource matches
                Assert.AreEqual(expected, tt.Activities.Find(i => i.Id == "3").Resource);
            }

        }

        [Test]
        public void CorruptXmlToTimetableTest()
        {

            const string contents = "Hey this is not XML!";

            var fop = new FetOutputProcessorExposer("", "");
            var byteArray = Encoding.UTF8.GetBytes(contents);

            // Create output file stream
            using (var outputFileStream = new MemoryStream(byteArray))
            {
                // Deserialize XML
                Assert.Throws<InvalidOperationException>(() => fop.XmlToTimetable(outputFileStream, null));
            }

        }

        [Test]
        public void EmptyXmlToTimetableTest()
        {

            var testDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "empty.xml");
            var testData = File.ReadAllBytes(testDataPath);

            // Create dummy FET output
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"Hopwood\Hopwood_activities.xml", new MockFileData(testData) }
            });

            // Run
            var fop = new FetOutputProcessor("Hopwood", fileSystem.Directory.GetCurrentDirectory(), fileSystem);
            var tt = fop.GetTimetable(null);

            // Check that we have found all 163 activities
            Assert.AreEqual(0, tt.Activities.Count);

        }

        [Test]
        public void InvalidXmlToTimetableTest()
        {

            var testDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "invalid.xml");
            var testData = File.ReadAllBytes(testDataPath);

            // Create dummy FET output
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"Hopwood\Hopwood_activities.xml", new MockFileData(testData) }
            });

            // Run
            var fop = new FetOutputProcessor("Hopwood", fileSystem.Directory.GetCurrentDirectory(), fileSystem);

            // Invalid XML throws InvalidOperationException
            Assert.Throws<InvalidOperationException>(() => fop.GetTimetable(null));

        }

        [Test]
        public void ParseSoftConflictsTest()
        {
            var testline = "\r\n\r\nTest soft conflict warning\r\n";
            var expected = new List<string> { "Test soft conflict warning" };

            var fop = new FetOutputProcessorExposer("", "");

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(testline)))
            using (var reader = new StreamReader(stream))
            {
                Assert.AreEqual(expected, fop.ParseSoftConflicts(reader));
            }
        }

        [Test]
        public void ParseSoftConflictsTtNullTest()
        {
            var fop = new FetOutputProcessorExposer("", "");
            Assert.DoesNotThrow(() => { fop.AddMetadata(null); });
        }

        [Test]
        public void ParseSoftConflictsEndTest()
        {
            var testline = "\r\nEnd of file.\r\nTest soft conflict warning\r\n";
            var expected = new List<string>();

            var fop = new FetOutputProcessorExposer("", "");

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(testline)))
            using (var reader = new StreamReader(stream))
            {
                Assert.AreEqual(expected, fop.ParseSoftConflicts(reader));
            }
        }

        [Test]
        public void ParseMetaLineTest()
        {
            var tt = new Timetable();
            var fop = new FetOutputProcessorExposer("", "");

            var input_1 = "Warning! Only 197 out of 479 activities placed!";
            uint expected_1 = 197;

            var input_2 = "Total conflicts: 5640361.15";
            double expected_2 = 5640361.15d;

            fop.ParseMetaLine(input_1, tt);
            Assert.AreEqual(expected_1, tt.PlacedActivities);

            fop.ParseMetaLine(input_2, tt);
            Assert.AreEqual(expected_2, tt.ConflictWeight);
        }

    }
}
