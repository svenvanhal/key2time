using NUnit.Framework;
using Timetabling.Objects;
using System.Linq;
using System.Xml.Linq;

namespace Timetabling.Tests.Objects
{
    [TestFixture()]
    internal class DaysListTest
    {
        XElement test;

        [SetUp]
        public void Init()
        {
            var TestDataModel = new TestDataModel(); 
            var list = new DaysList(TestDataModel.MockDataModel.Object);
            test = list.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Days_List", test.Name.ToString());
        }


        [Test]
        public void NumberOfDaysElementTest()
        {

            Assert.AreEqual("7", test.Elements("Number_of_Days").First().Value);
        }

        [Test]
        public void NumberOfDaysTest()
        {
            Assert.AreEqual(7, test.Elements("Day").Count());
        }

        [Test]
        public void WeekDayTest()
        {
            Assert.AreEqual(1, test.Elements("Day").Elements("Name").Count(item => item.Value.Equals(Days.Tuesday.ToString())));
        }
    }
}
