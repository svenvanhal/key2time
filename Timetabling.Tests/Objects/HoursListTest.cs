using NUnit.Framework;
using Moq;
using Timetabling.Objects;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Tests.Objects
{
    [TestFixture()]
    internal class HoursListTest
    {
        XElement test;

        [SetUp]
        public void Init()
        {
            var mockDB = new Mock<DataModel>();
            var list = new HoursList(mockDB.Object);
            test = list.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Hours_List", test.Name.ToString());
        }


        [Test]
        public void NumberOfDaysElementTest()
        {

            Assert.AreEqual("9", test.Elements("Number_of_Hours").First().Value);
        }
    }
}
