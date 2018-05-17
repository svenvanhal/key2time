using NUnit.Framework;
using System;
using Moq;
using System.Data.Entity;
using Timetable.timetable.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Timetable.timetable.DB.Tests
{
	[TestFixture()]
	public class YearsListTest
	{

		XElement test;

		[SetUp]
		public void Init()
		{
			var data = new List<School_Lookup_Grade>{
				new School_Lookup_Grade{GradeID = 0,  GradeName = "test", IsActive = true},
				new School_Lookup_Grade{GradeID = 1, GradeName = "test2", IsActive = true},

			}.AsQueryable();

			var mockSet = new Mock<DbSet<School_Lookup_Grade>>();
			mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Provider).Returns(data.Provider);
			mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Expression).Returns(data.Expression);
			mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			var dataClass = new List<School_Lookup_Class>{
				new School_Lookup_Class{GradeID = 0, ClassID=0 ,IsActive = true, ClassName = "classTest1"},
				new School_Lookup_Class{GradeID = 1, ClassID = 1, IsActive = true, ClassName = "classTest2"},

			}.AsQueryable();

			var mockSetClass = new Mock<DbSet<School_Lookup_Class>>();
			mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Provider).Returns(dataClass.Provider);
			mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Expression).Returns(dataClass.Expression);
			mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.ElementType).Returns(dataClass.ElementType);
			mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.GetEnumerator()).Returns(dataClass.GetEnumerator());

			var dataClassGroup = new List<tt_ClassGroup>{
				new tt_ClassGroup{groupName = "sub1",  classId = 1},
				new tt_ClassGroup{groupName = "sub2", classId = 10},

			}.AsQueryable();

			var mockSetClassGroup = new Mock<DbSet<tt_ClassGroup>>();
			mockSetClassGroup.As<IQueryable<tt_ClassGroup>>().Setup(m => m.Provider).Returns(dataClassGroup.Provider);
			mockSetClassGroup.As<IQueryable<tt_ClassGroup>>().Setup(m => m.Expression).Returns(dataClassGroup.Expression);
			mockSetClassGroup.As<IQueryable<tt_ClassGroup>>().Setup(m => m.ElementType).Returns(dataClassGroup.ElementType);
			mockSetClassGroup.As<IQueryable<tt_ClassGroup>>().Setup(m => m.GetEnumerator()).Returns(dataClassGroup.GetEnumerator());



			var mockDB = new Mock<DataModel>();
			mockDB.Setup(item => item.School_Lookup_Grade).Returns(mockSet.Object);
			mockDB.Setup(item => item.School_Lookup_Class).Returns(mockSetClass.Object);
			mockDB.Setup(item => item.tt_ClassGroup).Returns(mockSetClassGroup.Object);



			var list = new YearsList(mockDB.Object);
			list.Create();

			test = list.GetList();

		}

		[Test]
		public void ElementNameTest()
		{
			Assert.AreEqual(test.Name.ToString(), "Students_List");
		}


		[Test]
		public void ClassRightTest()
		{

			Assert.AreEqual(test.Elements("Year").Elements("Group").Elements("Name").Where(item => item.Value.Equals("classTest1")).Count(), 1);
			Assert.AreEqual(test.Elements("Year").Elements("Group").Elements("Name").Where(item => item.Value.Equals("notclassTest1")).Count(), 0);

		}

		[Test]
		public void GradeRightTest()
		{

			Assert.AreEqual(test.Elements("Year").Elements("Name").Where(item => item.Value.Equals("test")).Count(), 1);
			Assert.AreEqual(test.Elements("Year").Elements("Name").Where(item => item.Value.Equals("nottest")).Count(), 0);

		}

		[Test]
		public void SubGroupRightTest()
		{
			Console.WriteLine(test);
			Assert.AreEqual(test.Elements("Year").Elements("Group").Elements("Subgroup").Where(item => item.Value.Equals("sub1")).Count(), 1);
			Assert.AreEqual(test.Elements("Year").Elements("Group").Elements("Subgroup").Where(item => item.Value.Equals("sub2")).Count(), 0);

		}
       
	}
}
