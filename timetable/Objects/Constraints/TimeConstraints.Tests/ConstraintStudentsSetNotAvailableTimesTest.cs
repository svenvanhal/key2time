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
	public class ConstraintStudentsSetNotAvailableTimesTest
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
				new School_Lookup_Class{ClassID = 4, ClassName = "testGrade"},
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
			ConstraintStudentsSetNotAvailableTimes constraint = new ConstraintStudentsSetNotAvailableTimes();
			Assert.AreEqual(constraint.ToXelement().Name.ToString(), "ConstraintStudentsSetNotAvailableTimes");
		}


		[Test]
		public void CreateTest()
		{
			ConstraintStudentsSetNotAvailableTimes constraint = new ConstraintStudentsSetNotAvailableTimes();
			ConstraintStudentsSetNotAvailableTimes constraintTest = new ConstraintStudentsSetNotAvailableTimes { day = (Days)2, students = "testGrade", hour = 3 };
			ConstraintStudentsSetNotAvailableTimes constraintTest2 = new ConstraintStudentsSetNotAvailableTimes { day = (Days)3, students = "testGrade", hour = 3 };


			XElement[] result = constraint.Create(test.Object);
			Assert.AreEqual(1, result.Where(item => item.ToString().Equals(constraintTest.ToXelement().ToString())).Count());
			Assert.AreEqual(0, result.Where(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())).Count());

		}
	}


}
