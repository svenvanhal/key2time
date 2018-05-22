using NUnit.Framework;

namespace Timetabling.Tests
{
    [TestFixture]
    public class TimetableGeneratorTest
    {

        [Test]
        public void RefreshIdTest()
        {

            var ttg = new TimetableGenerator();

            var previousIdentifier = ttg.CurrentRunIdentifier;
            ttg.RefreshIdentifier();

            // Check if identifier is refreshed
            Assert.AreNotEqual(previousIdentifier, ttg.CurrentRunIdentifier);

        }

    }
}