using System.IO.Abstractions;
using NUnit.Framework;
using Timetabling.Algorithms.FET;

namespace Timetabling.Tests
{
    [TestFixture]
    public class TimetableGeneratorTest
    {

        [Test]
        public void Test()
        {

            var gklsdg = new FetProcessBuilder("Test", new FileSystem());

        }

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