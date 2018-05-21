using Moq;
using NUnit.Framework;
using Timetabling.Algorithms;

namespace Timetabling.Tests
{
    [TestFixture]
    public class ApplicationTest
    {

        [Test]
        public void RefreshIdTest()
        {

            var ttg = new TimetableGenerator();

            var pre_id = ttg.CurrentRunIdentifier;
            ttg.RefreshIdentifier();

            // Check if identifier is refreshed
            Assert.AreNotEqual(pre_id, ttg.CurrentRunIdentifier);

        }

    }
}