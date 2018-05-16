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
    public class DaysListTest
    {
		XElement test;
      
		[SetUp]
        public void Init()
        {
			var data = new List<Section_WeekEnd>{
				new Section_WeekEnd{dayIndex = 0},
				new Section_WeekEnd{dayIndex = 1},
				new Section_WeekEnd{dayIndex = 3},
			}.AsQueryable();

			var mockSet = new Mock<DbSet<Section_WeekEnd>>();
			mockSet.As<IQueryable<Section_WeekEnd>>().Setup(m => m.Provider).Returns(data.Provider); 
			mockSet.As<IQueryable<Section_WeekEnd>>().Setup(m => m.Expression).Returns(data.Expression); 
			mockSet.As<IQueryable<Section_WeekEnd>>().Setup(m => m.ElementType).Returns(data.ElementType); 
			mockSet.As<IQueryable<Section_WeekEnd>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator()); 
			       
			var mockDB = new Mock<DataModel>();
			mockDB.Setup(item => item.Section_WeekEnd).Returns(mockSet.Object);
            
			var list = new DaysList(mockDB.Object);
			list.Create();

			test = list.GetList();  
     
        }

		[Test]
        public void ElementNameTest()
        {
			Assert.AreEqual(test.Name.ToString(), "Days_List");
        }


        [Test]
		public void NumberOfDaysElementTest(){
           
            Assert.AreEqual(test.Elements("Number_of_Days").First().Value, "4");
		}

		[Test]
		public void NumberOfDaysTest()
        {
			Assert.AreEqual(test.Elements("Day").Count(), 4);
        }

		[Test]
		public void WeekendDayTest(){
			Assert.AreEqual(test.Elements("Day").Elements("Name").Where(item => item.Value.Equals(Days.Sunday.ToString())).Count(), 0);
		}

		[Test]
        public void WeekDayTest()
        {
			Assert.AreEqual(test.Elements("Day").Elements("Name").Where(item => item.Value.Equals(Days.Tuesday.ToString())).Count(), 1);
        }
    }
}
