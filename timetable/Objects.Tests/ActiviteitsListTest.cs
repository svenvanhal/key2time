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
    public class ActivitiesListTest
    {

		XElement test;

        [SetUp]
        public void Init()
        {
			
			var data = new List<School_TeacherClass_Subjects>{
				new School_TeacherClass_Subjects{ID = 1, ClassID = 1, SubjectID = 1,  TeacherID = 0},
				new School_TeacherClass_Subjects{ID = 2, ClassID = 2, SubjectID = 0,  TeacherID = 4},
			}.AsQueryable();

			var mockSet = new Mock<DbSet<School_TeacherClass_Subjects>>();
			mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.Provider).Returns(data.Provider); 
			mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.Expression).Returns(data.Expression); 
			mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.ElementType).Returns(data.ElementType); 
			mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator()); 
			       
			var data2 = new List<School_Lookup_Class>{
				new School_Lookup_Class{ClassName = "test", ClassID = 1},
				new School_Lookup_Class{ClassName = "test2", ClassID = 2},
            }.AsQueryable();

			var mockSet2 = new Mock<DbSet<School_Lookup_Class>>();
			mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Provider).Returns(data2.Provider); 
			mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Expression).Returns(data2.Expression); 
			mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.ElementType).Returns(data2.ElementType); 
			mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.GetEnumerator()).Returns(data2.GetEnumerator()); 
                           
			var mockDB = new Mock<DataModel>();
			mockDB.Setup(item => item.School_TeacherClass_Subjects).Returns(mockSet.Object);
			mockDB.Setup(item => item.School_Lookup_Class).Returns(mockSet2.Object);

			var list = new ActivitiesList(mockDB.Object);
			list.Create();
			test = list.GetList();
           
        }

		[Test]
        public void ElementNameTest()
        {
			Assert.AreEqual("Activities_List",test.Name.ToString());
        }
        
		[Test]
        public void ActivityIDRightTest()
        {
			Assert.AreEqual(1,test.Elements("Activity").Elements("Id").Count(item => item.Value.Equals("1")));

        }

        [Test]
        public void ActivityIDWrongTest()
        {
			Assert.AreEqual(0,test.Elements("Activity").Elements("Id").Count(item => item.Value.Equals("3")));

        }
        [Test]
		public void ActivityTeacherRightTest(){
			Assert.AreEqual(1,test.Elements("Activity").Elements("Teacher").Count(item => item.Value.Equals("0")));

		}

		[Test]
        public void ActivityTeacherWrongTest()
        {
			Assert.AreEqual(0,test.Elements("Activity").Elements("Teacher").Count(item => item.Value.Equals("3")));

        }

		[Test]
        public void ActivitySubjectRightTest()
        {
			Assert.AreEqual(1,test.Elements("Activity").Elements("Subject").Count(item => item.Value.Equals("0")));

        }

        [Test]
        public void ActivitySubjectWrongTest()
        {
			Assert.AreEqual(0,test.Elements("Activity").Elements("Subject").Count(item => item.Value.Equals("3")));

        }

		[Test]
        public void ActivityClassRightTest()
        {
			Assert.AreEqual(1,test.Elements("Activity").Elements("Students").Count(item => item.Value.Equals("test")));

        }

        [Test]
        public void ActivityClassWrongTest()
        {
			Assert.AreEqual(0,test.Elements("Activity").Elements("Students").Count(item => item.Value.Equals("wrong")));

        }
    }
}
