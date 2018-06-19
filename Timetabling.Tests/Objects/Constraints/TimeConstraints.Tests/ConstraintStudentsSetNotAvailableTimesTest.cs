using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using Timetabling.DB;
using Timetabling.Objects;
using Timetabling.Objects.Constraints.TimeConstraints;

namespace Timetabling.Tests.Objects.Constraints.TimeConstraints.Tests
{
    [TestFixture()]
    internal class ConstraintStudentsSetNotAvailableTimesTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            var data = new List<TimeOffTable>{
                new TimeOffTable{ItemId = 4, Day = 2, LessonIndex = 3, ItemType = 3},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<TimeOffTable>>();
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataClass = new List<LookupClassModel>{
                new LookupClassModel{ClassId = 4, ClassName = "testGrade", TimeOffConstraint = 100, IsActive = true},
            }.AsQueryable();

            var mockSetClass = new Mock<DbSet<LookupClassModel>>();
            mockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.Provider).Returns(dataClass.Provider);
            mockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.Expression).Returns(dataClass.Expression);
            mockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.ElementType).Returns(dataClass.ElementType);
            mockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.GetEnumerator()).Returns(dataClass.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Tt_TimeOff).Returns(mockSet.Object);
            mockDB.Setup(item => item.School_Lookup_Class).Returns(mockSetClass.Object);

            test = mockDB;
        }

        [Test()]
        public void TestConstruct()
        {
            var constraint = new ConstraintStudentsSetNotAvailableTimes();
            Assert.AreEqual("ConstraintStudentsSetNotAvailableTimes", constraint.ToXelement().Name.ToString());
        }

        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintStudentsSetNotAvailableTimes();
            var constraintTest = new ConstraintStudentsSetNotAvailableTimes { DaysList = { (Days)2 }, Students = "testGrade", HoursList = { 3 },NumberOfHours =1 ,weight = 100 };
            var constraintTest2 = new ConstraintStudentsSetNotAvailableTimes { DaysList = { (Days)2, (Days)2 }, Students = "testGrade", HoursList = { 3,2 }, NumberOfHours = 2, weight = 100 };

            var result = constraint.Create(test.Object);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
           Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }

}
