using System.IO;
using NUnit.Framework;
using Timetabling.Algorithms.FET;

namespace Timetabling.Tests.Algorithms.FET
{
    internal class FetConfigTest
    {

        [Test]
        public void GetSettingTest()
        {
            var expected = "TestValue";
            Assert.AreEqual(expected, FetConfig.GetSetting("TestSetting"));
        }

        [Test]
        public void DefaultsTest()
        {
            var expectedFetLanguage = FetLanguage.US_English;
            var expectedTimeout = 0;

            var expectedFetExecutablePath = @"lib/fet/fet-cl";
            var expectedFetWorkingDir = Path.Combine(Path.GetTempPath(), "timetabling");

            Assert.AreEqual(expectedFetLanguage, FetConfig.GetFetLanguage());
            Assert.AreEqual(expectedTimeout, FetConfig.GetTimeout());

            Assert.IsTrue(Path.GetFullPath(expectedFetExecutablePath).Equals(Path.GetFullPath(FetConfig.GetFetExecutableLocation())));
            Assert.IsTrue(Path.GetFullPath(expectedFetWorkingDir).Equals(Path.GetFullPath(FetConfig.GetFetWorkingDir())));
        }

    }
}
