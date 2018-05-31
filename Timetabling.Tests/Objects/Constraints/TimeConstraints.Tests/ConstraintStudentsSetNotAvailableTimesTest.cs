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
            var data = new List<Tt_TimeOff>{
                new Tt_TimeOff{ItemId = 4, day = 2, lessonIndex = 3, ItemType = 3},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Tt_TimeOff>>();
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataClass = new List<School_Lookup_Class>{
                new School_Lookup_Class{ClassID = 4, ClassName = "testGrade", timeOffConstraint = 50},
            }.AsQueryable();

            var mockSetClass = new Mock<DbSet<School_Lookup_Class>>();
            mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Provider).Returns(dataClass.Provider);
            mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Expression).Returns(dataClass.Expression);
            mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.ElementType).Returns(dataClass.ElementType);
            mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.GetEnumerator()).Returns(dataClass.GetEnumerator());

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
            var constraintTest = new ConstraintStudentsSetNotAvailableTimes { Days = { (Days)2 }, Students = "testGrade", Hours = { 3 },NumberOfHours =1 ,weight = 50 };
            var constraintTest2 = new ConstraintStudentsSetNotAvailableTimes { Days = { (Days)2, (Days)2 }, Students = "testGrade", Hours = { 3,2 }, NumberOfHours = 2, weight = 50 };

            var result = constraint.Create(test.Object);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
           Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }

}
