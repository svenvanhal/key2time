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
	public class ConstraintTeacherSetNotAvailableTimesTest
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
				new HR_MasterData_Employees{EmployeeID = 4, IsActive = true, timeOffConstraint=50},
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
			Assert.AreEqual(constraint.ToXelement().Name.ToString(), "ConstraintTeacherNotAvailableTimes");
		}


		[Test]
		public void CreateTest()
		{
			ConstraintTeacherNotAvailableTimes constraint = new ConstraintTeacherNotAvailableTimes();
			ConstraintTeacherNotAvailableTimes constraintTest = new ConstraintTeacherNotAvailableTimes { day = (Days)2, teacher = 4, hour = 3 , weight = 50};
			ConstraintTeacherNotAvailableTimes constraintTest2 = new ConstraintTeacherNotAvailableTimes { day = (Days)3, teacher = 4, hour = 3 , weight = 30};


			XElement[] result = constraint.Create(test.Object);
			Assert.AreEqual(1, result.Where(item => item.ToString().Equals(constraintTest.ToXelement().ToString())).Count());
			Assert.AreEqual(0, result.Where(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())).Count());

		}
	}


}
