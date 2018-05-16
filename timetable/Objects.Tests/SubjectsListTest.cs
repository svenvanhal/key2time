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
    public class SubjectListTest
    {

		XElement test;

        [SetUp]
        public void Init()
        {
			var data = new List<Subject_MasterData_Subject>{
				new Subject_MasterData_Subject{SubjectID = 0,  IsActive = true},
				new Subject_MasterData_Subject{SubjectID = 1, IsActive = false},
			
			}.AsQueryable();

			var mockSet = new Mock<DbSet<Subject_MasterData_Subject>>();
			mockSet.As<IQueryable<Subject_MasterData_Subject>>().Setup(m => m.Provider).Returns(data.Provider); 
			mockSet.As<IQueryable<Subject_MasterData_Subject>>().Setup(m => m.Expression).Returns(data.Expression); 
			mockSet.As<IQueryable<Subject_MasterData_Subject>>().Setup(m => m.ElementType).Returns(data.ElementType); 
			mockSet.As<IQueryable<Subject_MasterData_Subject>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator()); 
			       
			var mockDB = new Mock<DataModel>();
			mockDB.Setup(item => item.Subject_MasterData_Subject).Returns(mockSet.Object);

			var list = new SubjectsList(mockDB.Object);
			list.Create();

			test = list.GetList();
           
        }

		[Test]
        public void ElementNameTest()
        {
			Assert.AreEqual(test.Name.ToString(), "Subjects_List");
        }


        [Test]
		public void SubjectRightTest(){
			Assert.AreEqual(test.Elements("Subject").Elements("Name").Where(item => item.Value.Equals("0")).Count(), 1);

		}

        [Test]
		public void SubjectNotInDB(){
			Assert.AreEqual(test.Elements("Subject").Elements("Name").Where(item => item.Value.Equals("4")).Count(), 0);    

		}

        [Test]
		public void SubjectNotActive(){
			Assert.AreEqual(test.Elements("Subject").Elements("Name").Where(item => item.Value.Equals("1")).Count(), 0);    
       
		}
    }
}
