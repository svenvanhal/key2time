using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using NUnit.Framework;
using Timetabling.Algorithms.FET;

namespace Timetabling.Tests.Algorithms.FET
{

    internal class FetProcessBuilderExposer : FetProcessBuilder
    {
        public FetProcessBuilderExposer(string executableLocation = null, IFileSystem fileSystem = null) : base(executableLocation, fileSystem) { }
        public new string GetArgument(string name) => base.GetArgument(name);
        public new void SetArgument(string name, string value) => base.SetArgument(name, value);
        public new ProcessStartInfo CreateStartInfo() => base.CreateStartInfo();
    }

    [TestFixture]
    internal class FetProcessBuilderTest
    {

        [Test]
        public void ConstructorTest()
        {
            var fpb = new FetProcessBuilder("ExecutableLocation");
            var expected = "ExecutableLocation";

            Assert.AreEqual(expected, fpb.ExecutableLocation);
        }

        [Test]
        public void CreateProcessTest()
        {
            var fpb = new FetProcessBuilder();
            var proc = fpb.CreateProcess();
            Assert.IsInstanceOf<Process>(proc);
        }

        [Test]
        public void SetInputFileTest()
        {
            var mockFs = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { "TestFile", new MockFileData("") },
            });

            var fpb = new FetProcessBuilderExposer(null, mockFs);
            var expected = "TestFile";

            fpb.SetInputFile(expected);

            Assert.AreEqual(expected, fpb.GetArgument("inputfile"));
        }

        [Test]
        public void SetInputFileMissingTest()
        {
            var mockFs = new MockFileSystem(new Dictionary<string, MockFileData>());
            var fpb = new FetProcessBuilderExposer(null, mockFs);
            Assert.Throws<FileNotFoundException>(() => fpb.SetInputFile("DoesNotExist"));
        }

        [Test]
        public void SetOutputDirTest()
        {
            var mockFs = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"OutputDir/TestFile", new MockFileData("") },
            });

            var fpb = new FetProcessBuilderExposer(null, mockFs);
            var expected = "OutputDir";

            fpb.SetOutputDir(expected);

            Assert.AreEqual(expected, fpb.GetArgument("outputdir"));
        }

        [Test]
        public void SetOutputDirMissingTest()
        {
            var mockFs = new MockFileSystem(new Dictionary<string, MockFileData>());
            var fpb = new FetProcessBuilderExposer(null, mockFs);
            Assert.Throws<DirectoryNotFoundException>(() => fpb.SetOutputDir("DoesNotExist"));
        }

        [Test]
        public void SetTimeoutTest()
        {
            var fpb = new FetProcessBuilderExposer();

            fpb.SetTimeout(259);

            var expected = "259";
            Assert.AreEqual(expected, fpb.GetArgument("timelimitseconds"));
        }

        [Test]
        public void SetInvalidTimeoutTest()
        {
            var fpb = new FetProcessBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => fpb.SetTimeout(0));
        }

        [Test]
        public void SetLanguageTest()
        {
            var fpb = new FetProcessBuilderExposer();

            fpb.SetLanguage(FetLanguage.Dutch);

            var expected = FetLanguage.Dutch.ToString();
            Assert.AreEqual(expected, fpb.GetArgument("language"));
        }

        [Test]
        public void SetLanguageNullTest()
        {
            var fpb = new FetProcessBuilderExposer();
            Assert.Throws<ArgumentNullException>(() => fpb.SetLanguage(null));
        }

        [Test]
        public void DebugOnTest()
        {
            var fpb = new FetProcessBuilderExposer();
            var expected = "true";

            fpb.Debug(true);

            Assert.AreEqual(expected, fpb.GetArgument("verbose"));
        }

        [Test]
        public void DebugOffTest()
        {
            var fpb = new FetProcessBuilderExposer();
            var expected = "false";

            fpb.Debug(false);

            Assert.AreEqual(expected, fpb.GetArgument("verbose"));
        }

        [Test]
        public void GetSetArgumentTest()
        {
            var fpb = new FetProcessBuilderExposer();
            var expectedKey = "key";
            var expectedValue = "value";

            fpb.SetArgument(expectedKey, expectedValue);

            Assert.AreEqual(expectedValue, fpb.GetArgument(expectedKey));
        }

        [Test]
        public void RemoveArgumentTest()
        {
            var fpb = new FetProcessBuilderExposer();
            var expectedKey = "key";
            var oldValue = "value";

            fpb.SetArgument(expectedKey, oldValue);
            fpb.SetArgument(expectedKey, null);

            Assert.Throws<KeyNotFoundException>(() => fpb.GetArgument(expectedKey));
        }

        [Test]
        public void CreateStartInfoTest()
        {
            var expected = "ExecutableLocation";
            var fpb = new FetProcessBuilderExposer(expected);

            var startInfo = fpb.CreateStartInfo();

            Assert.IsNotNull(startInfo);
            Assert.AreEqual(expected, startInfo.FileName);
        }

    }
}