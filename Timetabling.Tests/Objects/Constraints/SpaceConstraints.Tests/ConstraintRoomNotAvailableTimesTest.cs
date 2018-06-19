using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using Timetabling.DB;
using Timetabling.Objects;
using Timetabling.Objects.Constraints.SpaceConstraints;

namespace Timetabling.Tests.Objects.Constraints.SpaceConstraints.Tests
{
    [TestFixture()]
    internal class ConstraintRoomNotAvailableTimesTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            var data = new List<TimeOffTable>{
                new TimeOffTable{ItemId = 4, Day = 2, LessonIndex = 3, ItemType = 4},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<TimeOffTable>>();
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataEmp = new List<BuildingModel>{
                new BuildingModel{ID = 4, IsActive = true},
            }.AsQueryable();

            var mockSetEmp = new Mock<DbSet<BuildingModel>>();
            mockSetEmp.As<IQueryable<BuildingModel>>().Setup(m => m.Provider).Returns(dataEmp.Provider);
            mockSetEmp.As<IQueryable<BuildingModel>>().Setup(m => m.Expression).Returns(dataEmp.Expression);
            mockSetEmp.As<IQueryable<BuildingModel>>().Setup(m => m.ElementType).Returns(dataEmp.ElementType);
            mockSetEmp.As<IQueryable<BuildingModel>>().Setup(m => m.GetEnumerator()).Returns(dataEmp.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Tt_TimeOff).Returns(mockSet.Object);
            mockDB.Setup(item => item.School_BuildingsUnits).Returns(mockSetEmp.Object);

            test = mockDB;
        }

        [Test()]
        public void TestConstruct()
        {
            var constraint = new ConstraintRoomNotAvailableTimes();
            Assert.AreEqual("ConstraintRoomNotAvailableTimes", constraint.ToXelement().Name.ToString());
        }


        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintRoomNotAvailableTimes();
            var constraintTest = new ConstraintRoomNotAvailableTimes { Day = (Days)2, Room = 4, Hour = 3 };
            var constraintTest2 = new ConstraintRoomNotAvailableTimes { Day = (Days)3, Room = 4, Hour = 3 };

            var result = constraint.Create(test.Object);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }

}
