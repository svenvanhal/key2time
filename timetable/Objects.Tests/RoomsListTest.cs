using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
			var data = new List<School_BuildingsUnits>{
				new School_BuildingsUnits{ID = 0,  IsActive = true},
				new School_BuildingsUnits{ID = 1, IsActive = false},

            }.AsQueryable();

			var mockSet = new Mock<DbSet<School_BuildingsUnits>>();
			mockSet.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.Provider).Returns(data.Provider);
			mockSet.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.Expression).Returns(data.Expression);
			mockSet.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockSet.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            
            var mockDB = new Mock<DataModel>();
			mockDB.Setup(item => item.School_BuildingsUnits).Returns(mockSet.Object);
			var list = new RoomsList(mockDB.Object);
			list.Create();
            test = list.GetList();

        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual(test.Name.ToString(), "Rooms_List");
        }

		[Test]
        public void RoomRightTest()
        {
			Console.Write(test);
            Assert.AreEqual(1, test.Elements("Room").Count());

        }
    }
}
