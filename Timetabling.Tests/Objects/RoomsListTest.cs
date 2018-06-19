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
            var data = new List<BuildingModel>{
                new BuildingModel{ID = 0,  IsActive = true},
                new BuildingModel{ID = 1, IsActive = false},

            }.AsQueryable();

            var mockSet = new Mock<DbSet<BuildingModel>>();
            mockSet.As<IQueryable<BuildingModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<BuildingModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<BuildingModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<BuildingModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Buildings).Returns(mockSet.Object);
            var list = new RoomsList(mockDB.Object);
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

            Assert.AreEqual(1, test.Elements("Room").Count());

        }
    }
}
