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
			var data = new List<tt_GradeLesson>{
				new tt_GradeLesson{gradeId = 0, numberOfLessons = 10},
            }.AsQueryable();

			var mockSet = new Mock<DbSet<tt_GradeLesson>>();
			mockSet.As<IQueryable<tt_GradeLesson>>().Setup(m => m.Provider).Returns(data.Provider);
			mockSet.As<IQueryable<tt_GradeLesson>>().Setup(m => m.Expression).Returns(data.Expression);
			mockSet.As<IQueryable<tt_GradeLesson>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockSet.As<IQueryable<tt_GradeLesson>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			var dataGrade = new List<School_Lookup_Grade>{
				new School_Lookup_Grade{GradeID = 0, GradeName = "testGrade", StageID = 6},
            }.AsQueryable();

			var mockSetGrade = new Mock<DbSet<School_Lookup_Grade>>();
			mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Provider).Returns(dataGrade.Provider);
			mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Expression).Returns(dataGrade.Expression);
			mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.ElementType).Returns(dataGrade.ElementType);
			mockSetGrade.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.GetEnumerator()).Returns(dataGrade.GetEnumerator());
         
			var dataStage = new List<School_Lookup_Stage>{
				new School_Lookup_Stage{StageID = 6, SectionID = 5
				},
            }.AsQueryable();
            
			var mockSetStage = new Mock<DbSet<School_Lookup_Stage>>();
			mockSetStage.As<IQueryable<School_Lookup_Stage>>().Setup(m => m.Provider).Returns(dataStage.Provider);
			mockSetStage.As<IQueryable<School_Lookup_Stage>>().Setup(m => m.Expression).Returns(dataStage.Expression);
			mockSetStage.As<IQueryable<School_Lookup_Stage>>().Setup(m => m.ElementType).Returns(dataStage.ElementType);
			mockSetStage.As<IQueryable<School_Lookup_Stage>>().Setup(m => m.GetEnumerator()).Returns(dataStage.GetEnumerator());
          


			var dataSection = new List<tt_SectionLessonConfiguration>{
				new tt_SectionLessonConfiguration{ sectionId = 5, lessonPerDay = 4
                },
            }.AsQueryable();
            
			var mockSetSection = new Mock<DbSet<tt_SectionLessonConfiguration>>();
			mockSetSection.As<IQueryable<tt_SectionLessonConfiguration>>().Setup(m => m.Provider).Returns(dataSection.Provider);
			mockSetSection.As<IQueryable<tt_SectionLessonConfiguration>>().Setup(m => m.Expression).Returns(dataSection.Expression);
			mockSetSection.As<IQueryable<tt_SectionLessonConfiguration>>().Setup(m => m.ElementType).Returns(dataSection.ElementType);
			mockSetSection.As<IQueryable<tt_SectionLessonConfiguration>>().Setup(m => m.GetEnumerator()).Returns(dataSection.GetEnumerator());
          
			var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.tt_GradeLesson).Returns(mockSet.Object);
            mockDB.Setup(item => item.School_Lookup_Grade).Returns(mockSetGrade.Object);
			mockDB.Setup(item => item.tt_SectionLessonConfiguration).Returns(mockSetSection.Object);
			mockDB.Setup(item => item.School_Lookup_Stage).Returns(mockSetStage.Object);

			test = mockDB;

        }
		[Test()]
		public void TestConstruct()

		{
			ConstraintStudentsSetMaxHoursDaily constraint = new ConstraintStudentsSetMaxHoursDaily ();
			Assert.AreEqual(constraint.ToXelement().Name.ToString(), "ConstraintStudentsSetMaxHoursDaily");
		}


		[Test]
		public void CreateTest()
		{
			ConstraintStudentsSetMaxHoursDaily constraint = new ConstraintStudentsSetMaxHoursDaily();
			ConstraintStudentsSetMaxHoursDaily constraintTest = new ConstraintStudentsSetMaxHoursDaily { maxHoursDaily = 4, gradeName = "testGrade" };
			ConstraintStudentsSetMaxHoursDaily constraintTest2 = new ConstraintStudentsSetMaxHoursDaily { maxHoursDaily = 10, gradeName = "falseGrade" };


			XElement[] result = constraint.Create(test.Object);
			Assert.AreEqual(1, result.Where(item => item.ToString().Equals(constraintTest.ToXelement().ToString())).Count());
			Assert.AreEqual(0, result.Where(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())).Count());
            
		}
	}

}
