using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;
using Timetabling.Objects;

namespace Timetabling.Tests.Objects
{
    [TestFixture()]
    internal class RoomsListTest
    {
        XElement test;

        [SetUp]
        public void Init()
        {
            var TestDataModel = new TestDataModel();
            var list = new RoomsList(TestDataModel.MockDataModel.Object);
            test = list.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Rooms_List", test.Name.ToString());
        }

        [Test]
        public void RoomRightTest()
        {

            Assert.AreEqual(2, test.Elements("Room").Count());

        }
    }
}
