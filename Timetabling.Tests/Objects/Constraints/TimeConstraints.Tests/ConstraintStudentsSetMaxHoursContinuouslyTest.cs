using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using Timetabling.DB;
using Timetabling.Objects.Constraints.TimeConstraints;

namespace Timetabling.Tests.Objects.Constraints.TimeConstraints.Tests
{
    [TestFixture()]
    internal class ConstraintStudentsSetMaxHoursContinuouslyTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            var data = new List<GradeLessonModel>{
                new GradeLessonModel{GradeId = 0, NumberOfLessons = 3},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<GradeLessonModel>>();
            mockSet.As<IQueryable<GradeLessonModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<GradeLessonModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<GradeLessonModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<GradeLessonModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var dataGrade = new List<School_Lookup_Grade>{
                new School_Lookup_Grade{GradeID = 0, GradeName = "testGrade"},
            }.AsQueryable();

            var mockSetGrade = new Mock<DbSet<School_Lookup_Grade>>();
            mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Provider).Returns(dataGrade.Provider);
            mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Expression).Returns(dataGrade.Expression);
            mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.ElementType).Returns(dataGrade.ElementType);
            mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.GetEnumerator()).Returns(dataGrade.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Tt_GradeLesson).Returns(mockSet.Object);
            mockDB.Setup(item => item.School_Lookup_Grade).Returns(mockSetGrade.Object);

            test = mockDB;
        }

        [Test()]
        public void TestConstruct()
        {
            var constraint = new ConstraintStudentsSetMaxHoursContinuously { NumberOfHours = 1, GradeName = "test" };
            Assert.AreEqual(100, constraint.weight);
            Assert.AreEqual("test", constraint.GradeName);
            Assert.AreEqual(1, constraint.NumberOfHours);
        }

        [Test()]
        public void TesTtoXElement()
        {
            var constraint = new ConstraintStudentsSetMaxHoursContinuously { NumberOfHours = 1, GradeName = "test" };
            Assert.AreEqual("<ConstraintStudentsSetMaxHoursContinuously>" + System.Environment.NewLine + "  <Weight_Percentage>100</Weight_Percentage>" + System.Environment.NewLine + "  <Maximum_Hours_Continuously>1</Maximum_Hours_Continuously>" + System.Environment.NewLine + "  <Students>test</Students>" + System.Environment.NewLine + "</ConstraintStudentsSetMaxHoursContinuously>", constraint.ToXelement().ToString());
        }

        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintStudentsSetMaxHoursContinuously();
            var constrainTtest = new ConstraintStudentsSetMaxHoursContinuously { GradeName = "testGrade", NumberOfHours = 3 };
            var constrainTtest2 = new ConstraintStudentsSetMaxHoursContinuously { GradeName = "testGrade2", NumberOfHours = 3 };

            var result = constraint.Create(test.Object);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constrainTtest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constrainTtest2.ToXelement().ToString())));
        }
    }

}
