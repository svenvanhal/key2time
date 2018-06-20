using Moq;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using Timetabling.DB;
using System.Data.Entity;
using Timetabling.Objects.Constraints.TimeConstraints;
using Timetabling.Objects;

namespace Timetabling.Tests.Objects.Constraints.TimeConstraints.Tests
{
    [TestFixture()]
    internal class ConstraintPeriodSectionTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            var data = new List<LookupGradeModel>{
                new LookupGradeModel{ GradeName = "test", GradeId = 0, IsActive = true, StageId = 1},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<LookupGradeModel>>();
            mockSet.As<IQueryable<LookupGradeModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<LookupGradeModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<LookupGradeModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<LookupGradeModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var dataStage = new List<LookupStageModel>{
                new LookupStageModel{IsActive = true, SectionId = 2, StageId = 1
                },
            }.AsQueryable();

            var mockSetStage = new Mock<DbSet<LookupStageModel>>();
            mockSetStage.As<IQueryable<LookupStageModel>>().Setup(m => m.Provider).Returns(dataStage.Provider);
            mockSetStage.As<IQueryable<LookupStageModel>>().Setup(m => m.Expression).Returns(dataStage.Expression);
            mockSetStage.As<IQueryable<LookupStageModel>>().Setup(m => m.ElementType).Returns(dataStage.ElementType);
            mockSetStage.As<IQueryable<LookupStageModel>>().Setup(m => m.GetEnumerator()).Returns(dataStage.GetEnumerator());

            var dataWeekend = new List<SectionWeekendModel>{
                new SectionWeekendModel{DayIndex = 0, DayName = "Sunday", SectionId =2},
            }.AsQueryable();

            var mockSetWeekend = new Mock<DbSet<SectionWeekendModel>>();
            mockSetWeekend.As<IQueryable<SectionWeekendModel>>().Setup(m => m.Provider).Returns(dataWeekend.Provider);
            mockSetWeekend.As<IQueryable<SectionWeekendModel>>().Setup(m => m.Expression).Returns(dataWeekend.Expression);
            mockSetWeekend.As<IQueryable<SectionWeekendModel>>().Setup(m => m.ElementType).Returns(dataWeekend.ElementType);
            mockSetWeekend.As<IQueryable<SectionWeekendModel>>().Setup(m => m.GetEnumerator()).Returns(dataWeekend.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Weekends).Returns(mockSetWeekend.Object);
            mockDB.Setup(item => item.GradesLookup).Returns(mockSet.Object);
            mockDB.Setup(item => item.StagesLookup).Returns(mockSetStage.Object);

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
            var constraintTest = new ConstraintPeriodSection { DaysList = new List<Days> { 0 }, NumberOfHours = 9, Students = "test" };
            var constraintTestFail = new ConstraintPeriodSection { DaysList = new List<Days> { 0 }, NumberOfHours = 9, Students = "not" };

            var result = constraint.Create(test.Object);

            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTestFail.ToXelement().ToString())));
        }
    }

}
