﻿using Moq;
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
	public class ConstraintStudentsSetMaxHoursContinuouslyTest
	{
		Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
			var data = new List<tt_GradeLesson>{
				new tt_GradeLesson{gradeId = 0, numberOfLessons = 3},
            }.AsQueryable();

			var mockSet = new Mock<DbSet<tt_GradeLesson>>();
			mockSet.As<IQueryable<tt_GradeLesson>>().Setup(m => m.Provider).Returns(data.Provider);
			mockSet.As<IQueryable<tt_GradeLesson>>().Setup(m => m.Expression).Returns(data.Expression);
			mockSet.As<IQueryable<tt_GradeLesson>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockSet.As<IQueryable<tt_GradeLesson>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


			var dataGrade = new List<School_Lookup_Grade>{
				new School_Lookup_Grade{GradeID = 0, GradeName = "testGrade"},
            }.AsQueryable();

			var mockSetGrade = new Mock<DbSet<School_Lookup_Grade>>();
			mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Provider).Returns(dataGrade.Provider);
			mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Expression).Returns(dataGrade.Expression);
			mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.ElementType).Returns(dataGrade.ElementType);
			mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.GetEnumerator()).Returns(dataGrade.GetEnumerator());
            var mockDB = new Mock<DataModel>();
			mockDB.Setup(item => item.tt_GradeLesson).Returns(mockSet.Object);
			mockDB.Setup(item => item.School_Lookup_Grade).Returns(mockSetGrade.Object);


            
			test = mockDB;

        }
		[Test()]
		public void TestConstruct()

		{
			ConstraintStudentsSetMaxHoursContinuously constraint = new ConstraintStudentsSetMaxHoursContinuously {numberOfHours = 1, gradeName = "test" };
			Assert.AreEqual(constraint.weight, 100);
			Assert.AreEqual(constraint.gradeName, "test");
			Assert.AreEqual(constraint.numberOfHours, 1);
		}

		[Test()]
		public void TestToXElement()

		{
			ConstraintStudentsSetMaxHoursContinuously constraint = new ConstraintStudentsSetMaxHoursContinuously {numberOfHours = 1, gradeName ="test" };
			Assert.AreEqual("<ConstraintStudentsSetMaxHoursContinuously>"+System.Environment.NewLine+"  <Weight_Percentage>100</Weight_Percentage>"+System.Environment.NewLine+"  <Maximum_Hours_Continuously>1</Maximum_Hours_Continuously>" + System.Environment.NewLine + "  <Students>test</Students>" + System.Environment.NewLine + "</ConstraintStudentsSetMaxHoursContinuously>", constraint.ToXelement().ToString());
		}

        [Test]
		public void CreateTest(){
			ConstraintStudentsSetMaxHoursContinuously constraint = new ConstraintStudentsSetMaxHoursContinuously();
			ConstraintStudentsSetMaxHoursContinuously constraintTest = new ConstraintStudentsSetMaxHoursContinuously { gradeName = "testGrade", numberOfHours = 3 };
			ConstraintStudentsSetMaxHoursContinuously constraintTest2 = new ConstraintStudentsSetMaxHoursContinuously { gradeName = "testGrade2", numberOfHours = 3 };


			XElement[] result = constraint.Create(test.Object);
			Assert.AreEqual(result.Where(item => item.ToString().Equals(constraintTest.ToXelement().ToString())).Count(), 1);
			Assert.AreEqual(result.Where(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())).Count(), 0);
            
		}
	}

}
