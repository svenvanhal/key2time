using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Objects.Constraints.SpaceConstraints.Tests
{
    [TestFixture()]
    public class ConstraintRoomNotAvailableTimesTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            var data = new List<Tt_TimeOff>{
                new Tt_TimeOff{ItemId = 4, day = 2, lessonIndex = 3, ItemType = 4},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Tt_TimeOff>>();
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataEmp = new List<School_BuildingsUnits>{
                new School_BuildingsUnits{ID = 4, IsActive = true},
            }.AsQueryable();

            var mockSetEmp = new Mock<DbSet<School_BuildingsUnits>>();
            mockSetEmp.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.Provider).Returns(dataEmp.Provider);
            mockSetEmp.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.Expression).Returns(dataEmp.Expression);
            mockSetEmp.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.ElementType).Returns(dataEmp.ElementType);
            mockSetEmp.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.GetEnumerator()).Returns(dataEmp.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Tt_TimeOff).Returns(mockSet.Object);
            mockDB.Setup(item => item.School_BuildingsUnits).Returns(mockSetEmp.Object);

            test = mockDB;

        }

        [Test()]
        public void TestConstruct()

        {
            ConstraintRoomNotAvailableTimes constraint = new ConstraintRoomNotAvailableTimes();
            Assert.AreEqual(constraint.ToXelement().Name.ToString(), "ConstraintRoomNotAvailableTimes");
        }


        [Test]
        public void CreateTest()
        {
            ConstraintRoomNotAvailableTimes constraint = new ConstraintRoomNotAvailableTimes();
            ConstraintRoomNotAvailableTimes constraintTest = new ConstraintRoomNotAvailableTimes { day = (Days)2, room = 4, hour = 3 };
            ConstraintRoomNotAvailableTimes constraintTest2 = new ConstraintRoomNotAvailableTimes { day = (Days)3, room = 4, hour = 3 };


            XElement[] result = constraint.Create(test.Object);
            Assert.AreEqual(1, result.Where(item => item.ToString().Equals(constraintTest.ToXelement().ToString())).Count());
            Assert.AreEqual(0, result.Where(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())).Count());

        }
    }


}
