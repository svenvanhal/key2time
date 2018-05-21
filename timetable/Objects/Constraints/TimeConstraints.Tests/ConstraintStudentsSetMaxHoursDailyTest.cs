using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects.Constraints.TimeConstraints.Tests
{
	[TestFixture()]
	public class ConstraintStudentsSetMaxHoursDailyTest
	{
		Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
			var data = new List<Tt_GradeLesson>{
				new Tt_GradeLesson{gradeId = 0, numberOfLessons = 10},
            }.AsQueryable();

			var mockSet = new Mock<DbSet<Tt_GradeLesson>>();
			mockSet.As<IQueryable<Tt_GradeLesson>>().Setup(m => m.Provider).Returns(data.Provider);
			mockSet.As<IQueryable<Tt_GradeLesson>>().Setup(m => m.Expression).Returns(data.Expression);
			mockSet.As<IQueryable<Tt_GradeLesson>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockSet.As<IQueryable<Tt_GradeLesson>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


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
			ConstraintStudentsSetMaxHoursDaily constraint = new ConstraintStudentsSetMaxHoursDaily ();
			Assert.AreEqual( "ConstraintStudentsSetMaxHoursDaily", constraint.ToXelement().Name.ToString());
		}


		[Test]
		public void CreateTest()
		{
			ConstraintStudentsSetMaxHoursDaily constraint = new ConstraintStudentsSetMaxHoursDaily();
			ConstraintStudentsSetMaxHoursDaily constraintTest = new ConstraintStudentsSetMaxHoursDaily { maxHoursDaily = 10, gradeName = "testGrade" };
			ConstraintStudentsSetMaxHoursDaily constraintTest2 = new ConstraintStudentsSetMaxHoursDaily { maxHoursDaily = 10, gradeName = "falseGrade" };


			XElement[] result = constraint.Create(test.Object);
			Assert.AreEqual(1,result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
			Assert.AreEqual(0,result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
            
		}
	}

}
