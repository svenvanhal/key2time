using Moq;
using NUnit.Framework;
using System;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects.Tests
{
    [TestFixture()]
    public class RoomsListTest
    {
		XElement test;

        [SetUp]
        public void Init()
        {

            var mockDB = new Mock<DataModel>();
			var list = new RoomsList(mockDB.Object);

            test = list.GetList();

        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual(test.Name.ToString(), "Rooms_List");
        }

    }
}
