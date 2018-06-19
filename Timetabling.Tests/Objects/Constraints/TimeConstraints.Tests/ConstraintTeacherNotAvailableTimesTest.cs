using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using Timetabling.DB;
using Timetabling.Objects;
using Timetabling.Objects.Constraints.TimeConstraints;

namespace Timetabling.Tests.Objects.Constraints.TimeConstraints
{
    [TestFixture()]
    internal class ConstraintTeacherSetNotAvailableTimesTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            var data = new List<TimeOffTable>{
                new TimeOffTable{ItemId = 4, Day = 2, LessonIndex = 3, ItemType = 1},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<TimeOffTable>>();
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TimeOffTable>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataEmp = new List<EmployeeModel>{
                new EmployeeModel{EmployeeId = 4, IsActive = true, TimeOffConstraint=50, IsTeacher =true},
            }.AsQueryable();

            var mockSetEmp = new Mock<DbSet<EmployeeModel>>();
            mockSetEmp.As<IQueryable<EmployeeModel>>().Setup(m => m.Provider).Returns(dataEmp.Provider);
            mockSetEmp.As<IQueryable<EmployeeModel>>().Setup(m => m.Expression).Returns(dataEmp.Expression);
            mockSetEmp.As<IQueryable<EmployeeModel>>().Setup(m => m.ElementType).Returns(dataEmp.ElementType);
            mockSetEmp.As<IQueryable<EmployeeModel>>().Setup(m => m.GetEnumerator()).Returns(dataEmp.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Tt_TimeOff).Returns(mockSet.Object);
            mockDB.Setup(item => item.HR_MasterData_Employees).Returns(mockSetEmp.Object);

            test = mockDB;

        }

        [Test()]
        public void TestConstruct()

        {
            ConstraintTeacherNotAvailableTimes constraint = new ConstraintTeacherNotAvailableTimes();
            Assert.AreEqual( "ConstraintTeacherNotAvailableTimes",constraint.ToXelement().Name.ToString());
        }


        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintTeacherNotAvailableTimes();
            var constraintTest = new ConstraintTeacherNotAvailableTimes { DaysList = { (Days)2 }, Teacher = 4, HoursList = { 3 }, weight = 50, NumberOfHours = 1 };
            var constraintTest2 = new ConstraintTeacherNotAvailableTimes { DaysList = { (Days)3, (Days)3, (Days)2 }, Teacher = 4, HoursList = { 3, 4, 5 }, weight = 30, NumberOfHours = 3 };

            var result = constraint.Create(test.Object);
          
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));

            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }
}
