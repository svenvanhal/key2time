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
            var expectedFetExecutablePath = @"lib/fet/fet-cl";
            var expectedFetLanguage = FetLanguage.US_English;
            var expectedFetWorkingDir = @"%TEMP%/Timetabling";
            var expectedTimeout = 0;

            Assert.AreEqual(expectedFetExecutablePath, FetConfig.GetFetExecutableLocation());
            Assert.AreEqual(expectedFetLanguage, FetConfig.GetFetLanguage());
            Assert.AreEqual(expectedFetWorkingDir, FetConfig.GetFetWorkingDir());
            Assert.AreEqual(expectedTimeout, FetConfig.GetTimeout());
        }

    }
}
