using NUnit.Framework;
using Timetabling.Algorithms.FET;
using Timetabling.Config;

namespace Timetabling.Tests.Config
{
    internal class TimetablingConfigTest
    {

        [Test]
        public void GetSettingTest()
        {
            var expected = "TestValue";
            Assert.AreEqual(expected, TimetablingConfig.GetSetting("TestSetting"));
        }

        [Test]
        public void DefaultsTest()
        {
            var expectedFetExecutablePath = @"lib/fet/fet-cl";
            var expectedFetLanguage = FetLanguage.US_English;
            var expectedFetWorkingDir = @"%TEMP%/Timetabling";
            var expectedTimeout = 0;

            Assert.AreEqual(expectedFetExecutablePath, TimetablingConfig.GetFetExecutableLocation());
            Assert.AreEqual(expectedFetLanguage, TimetablingConfig.GetFetLanguage());
            Assert.AreEqual(expectedFetWorkingDir, TimetablingConfig.GetFetWorkingDir());
            Assert.AreEqual(expectedTimeout, TimetablingConfig.GetTimeout());
        }

    }
}
