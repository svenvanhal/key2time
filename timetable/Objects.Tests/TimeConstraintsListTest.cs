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
    public class TimeConstraintsListTest
    {

		XElement test;

        [SetUp]
        public void Init()
        {
			
	         
			var mockDB = new Mock<DataModel>();
		
			var list = new TimeConstraintsList(mockDB.Object);
			test = list.GetList();
           
        }

		[Test]
        public void ElementNameTest()
        {
			Assert.AreEqual("Time_Constraints_List", test.Name.ToString() );
        }
        
    }
}
