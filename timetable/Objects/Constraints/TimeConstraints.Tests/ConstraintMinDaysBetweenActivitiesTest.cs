using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects.Constraints.TimeConstraints.Tests
{
	public class ConstraintMinDaysBetweenActivitiesTest
	{
		Mock<DataModel> test;

		[SetUp]
		public void Init()
		{
			var data = new List<School_TeacherClass_Subjects>{
				new School_TeacherClass_Subjects{ ClassID = 1, SubjectID = 1,  TeacherID = 0},
				new School_TeacherClass_Subjects{ ClassID = 2, SubjectID = 0,  TeacherID = 4},
			}.AsQueryable();

			var mockSet = new Mock<DbSet<School_TeacherClass_Subjects>>();
			mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.Provider).Returns(data.Provider);
			mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.Expression).Returns(data.Expression);
			mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			var data2 = new List<School_Lookup_Class>{
				new School_Lookup_Class{ClassName = "test", ClassID = 1, GradeID = 60},
				new School_Lookup_Class{ClassName = "test2", ClassID = 2, GradeID = 60},
			}.AsQueryable();

			var mockSet2 = new Mock<DbSet<School_Lookup_Class>>();
			mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Provider).Returns(data2.Provider);
			mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Expression).Returns(data2.Expression);
			mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.ElementType).Returns(data2.ElementType);
			mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.GetEnumerator()).Returns(data2.GetEnumerator());

			var data3 = new List<Subject_SubjectGrade>{
				new Subject_SubjectGrade{GradeID = 60, NumberOfLlessonsPerWeek = 4,NumberOfLlessonsPerDay =1,  SubjectID =1
				}
			}.AsQueryable();

			var mockSet3 = new Mock<DbSet<Subject_SubjectGrade>>();
			mockSet3.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.Provider).Returns(data3.Provider);
			mockSet3.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.Expression).Returns(data3.Expression);
			mockSet3.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.ElementType).Returns(data3.ElementType);
			mockSet3.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.GetEnumerator()).Returns(data3.GetEnumerator());

			var mockDB = new Mock<DataModel>();
			mockDB.Setup(item => item.School_TeacherClass_Subjects).Returns(mockSet.Object);
			mockDB.Setup(item => item.School_Lookup_Class).Returns(mockSet2.Object);
			mockDB.Setup(item => item.Subject_SubjectGrade).Returns(mockSet3.Object);

			test = mockDB;
		}

		[Test()]
		public void TestConstruct()

		{
			ConstraintMinDaysBetweenActivities constraint = new ConstraintMinDaysBetweenActivities();
			Assert.AreEqual("ConstraintMinDaysBetweenActivities", constraint.ToXelement().Name.ToString());
		}


		[Test]
		public void CreateNumberOfActivitiesTest()
		{
			ConstraintMinDaysBetweenActivities constraint = new ConstraintMinDaysBetweenActivities();


			var result = constraint.Create(test.Object);
			Assert.AreEqual("4", result.First().Elements("Number_of_Activities").First().Value);
			Assert.AreNotEqual("6", result.First().Elements("Number_of_Activities").First().Value);

		}

		[Test]
        public void CreateNumberOfActivityElementsTest()
        {
            ConstraintMinDaysBetweenActivities constraint = new ConstraintMinDaysBetweenActivities();


            var result = constraint.Create(test.Object);

			   
			Assert.AreEqual(4, result.First().Elements("Activity_Id").Count());

        }
	}

}
