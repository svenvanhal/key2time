using Moq;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using Timetabling.DB;
using System.Data.Entity;
using Timetabling.Objects.Constraints.TimeConstraints;

namespace Timetabling.Tests.Objects.Constraints.TimeConstraints.Tests
{
    [TestFixture()]
    internal class ConstraintPeriodSectionTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            var data = new List<School_Lookup_Grade>{
                new School_Lookup_Grade{ GradeName = "test", GradeID = 0, IsActive = true, StageID = 1},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<School_Lookup_Grade>>();
            mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var dataStage = new List<School_Lookup_Stage>{
                new School_Lookup_Stage{IsActive = true, SectionID = 2, StageID = 1
                },
            }.AsQueryable();

            var mockSetStage = new Mock<DbSet<School_Lookup_Stage>>();
            mockSetStage.As<IQueryable<School_Lookup_Stage>>().Setup(m => m.Provider).Returns(dataStage.Provider);
            mockSetStage.As<IQueryable<School_Lookup_Stage>>().Setup(m => m.Expression).Returns(dataStage.Expression);
            mockSetStage.As<IQueryable<School_Lookup_Stage>>().Setup(m => m.ElementType).Returns(dataStage.ElementType);
            mockSetStage.As<IQueryable<School_Lookup_Stage>>().Setup(m => m.GetEnumerator()).Returns(dataStage.GetEnumerator());

            var dataWeekend = new List<Section_WeekEnd>{
                new Section_WeekEnd{dayIndex = 0, dayName = "Sunday", sectionId =2},
            }.AsQueryable();

            var mockSetWeekend = new Mock<DbSet<Section_WeekEnd>>();
            mockSetWeekend.As<IQueryable<Section_WeekEnd>>().Setup(m => m.Provider).Returns(dataWeekend.Provider);
            mockSetWeekend.As<IQueryable<Section_WeekEnd>>().Setup(m => m.Expression).Returns(dataWeekend.Expression);
            mockSetWeekend.As<IQueryable<Section_WeekEnd>>().Setup(m => m.ElementType).Returns(dataWeekend.ElementType);
            mockSetWeekend.As<IQueryable<Section_WeekEnd>>().Setup(m => m.GetEnumerator()).Returns(dataWeekend.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Section_WeekEnd).Returns(mockSetWeekend.Object);
            mockDB.Setup(item => item.School_Lookup_Grade).Returns(mockSet.Object);
            mockDB.Setup(item => item.School_Lookup_Stage).Returns(mockSetStage.Object);

            test = mockDB;
        }

        [Test()]
        public void TestConstruct()
        {
            var constraint = new ConstraintPeriodSection();
            Assert.AreEqual("ConstraintStudentsSetNotAvailableTimes", constraint.ToXelement().Name.ToString());
        }


        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintPeriodSection();
            var constraintTest = new ConstraintPeriodSection { days = new List<int> { 0 }, numberOfHours = 8, students = "test" };
            var constraintTestFail = new ConstraintPeriodSection { days = new List<int> { 0 }, numberOfHours = 8, students = "not" };

            var result = constraint.Create(test.Object);

            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTestFail.ToXelement().ToString())));
        }
    }

}
