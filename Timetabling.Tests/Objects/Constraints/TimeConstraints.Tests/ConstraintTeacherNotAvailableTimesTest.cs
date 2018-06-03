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
            var data = new List<Tt_TimeOff>{
                new Tt_TimeOff{ItemId = 4, day = 2, lessonIndex = 3, ItemType = 1},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Tt_TimeOff>>();
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataEmp = new List<HR_MasterData_Employees>{
                new HR_MasterData_Employees{EmployeeID = 4, IsActive = true, timeOffConstraint=50, IsTeacher =true},
            }.AsQueryable();

            var mockSetEmp = new Mock<DbSet<HR_MasterData_Employees>>();
            mockSetEmp.As<IQueryable<HR_MasterData_Employees>>().Setup(m => m.Provider).Returns(dataEmp.Provider);
            mockSetEmp.As<IQueryable<HR_MasterData_Employees>>().Setup(m => m.Expression).Returns(dataEmp.Expression);
            mockSetEmp.As<IQueryable<HR_MasterData_Employees>>().Setup(m => m.ElementType).Returns(dataEmp.ElementType);
            mockSetEmp.As<IQueryable<HR_MasterData_Employees>>().Setup(m => m.GetEnumerator()).Returns(dataEmp.GetEnumerator());

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
            var constraintTest = new ConstraintTeacherNotAvailableTimes { days = { (Days)2 }, teacher = 4, hours = { 3 }, weight = 50, numberOfHours = 1 };
            var constraintTest2 = new ConstraintTeacherNotAvailableTimes { days = { (Days)3, (Days)3, (Days)2 }, teacher = 4, hours = { 3, 4, 5 }, weight = 30, numberOfHours = 3 };

            var result = constraint.Create(test.Object);
          
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }
}
