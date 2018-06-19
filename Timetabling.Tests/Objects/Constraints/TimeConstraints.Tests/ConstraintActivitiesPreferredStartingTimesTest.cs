using NUnit.Framework;
using System.Linq;
using Moq;
using Timetabling.DB;
using System.Collections.Generic;
using System.Data.Entity;
using Timetabling.Objects.Constraints.TimeConstraints;
using Timetabling.Objects;

namespace Timetabling.Tests.Objects.Constraints.TimeConstraints.Tests
{
    [TestFixture()]
    internal class ConstraintActivitiesPreferredStartingTimesTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            var data = new List<TimeOffTable>{
                new TimeOffTable{ItemId = 4, Day = 2, LessonIndex = 3, ItemType = 2},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<TimeOffTable>>();
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataAc = new List<ActivityGroupModel>{
                new ActivityGroupModel{ ClassId = 1, SubjectId = 4,  TeacherId = 0},
            }.AsQueryable();

            var mockSetAc = new Mock<DbSet<ActivityGroupModel>>();
            mockSetAc.As<IQueryable<ActivityGroupModel>>().Setup(m => m.Provider).Returns(dataAc.Provider);
            mockSetAc.As<IQueryable<ActivityGroupModel>>().Setup(m => m.Expression).Returns(dataAc.Expression);
            mockSetAc.As<IQueryable<ActivityGroupModel>>().Setup(m => m.ElementType).Returns(dataAc.ElementType);
            mockSetAc.As<IQueryable<ActivityGroupModel>>().Setup(m => m.GetEnumerator()).Returns(dataAc.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Tt_TimeOff).Returns(mockSet.Object);
            mockDB.Setup(item => item.tt_ActitvityGroup).Returns(mockSetAc.Object);

            test = mockDB;

        }

        [Test()]
        public void TestConstruct()

        {
            ConstraintActivitiesPreferredStartingTimes constraint = new ConstraintActivitiesPreferredStartingTimes();
            Assert.AreEqual("ConstraintActivitiesPreferredStartingTimes", constraint.ToXelement().Name.ToString());
        }


        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintActivitiesPreferredStartingTimes();
            var constraintTest = new ConstraintActivitiesPreferredStartingTimes { DaysList = { (Days)2 }, SubjectID = 4, HoursList = { 3 }, NumberOfHours = 1 };
            var constraintTest2 = new ConstraintActivitiesPreferredStartingTimes { DaysList = { (Days)3, (Days)3, (Days)2 }, SubjectID = 4, HoursList = { 3, 4, 5 }, NumberOfHours = 3 };

            var result = constraint.Create(test.Object);

            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }
}
