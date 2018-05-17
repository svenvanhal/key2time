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
    public class HoursListTest
    {
		XElement test;
      
		[SetUp]
        public void Init()
        {
			
			var mockDB = new Mock<DataModel>();
			var list = new HoursList(mockDB.Object);
			list.Create();

			test = list.GetList();  
     
        }

		[Test]
        public void ElementNameTest()
        {
			Assert.AreEqual(test.Name.ToString(), "Hours_List");
        }


        [Test]
		public void NumberOfDaysElementTest(){
           
            Assert.AreEqual(test.Elements("Number_of_Hours").First().Value, "8");
		}
    }
}
