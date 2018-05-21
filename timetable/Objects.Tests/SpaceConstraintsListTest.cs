using NUnit.Framework;
using System;
using Moq;
using System.Data.Entity;
using Timetabling.Objects;
using System.Collections.Generic;
using System.Linq; 
using System.Xml.Linq; 

namespace Timetabling.DB.Tests
{
    [TestFixture()]
    public class SpaceConstraintsListTest
    {

		XElement test;

        [SetUp]
        public void Init()
        {
			
	         
			var mockDB = new Mock<DataModel>();
		

			var list = new SpaceConstraintsList(mockDB.Object);
			test = list.GetList();
           
        }

		[Test]
        public void ElementNameTest()
        {
			Assert.AreEqual("Space_Constraints_List", test.Name.ToString() );
        }
        
    }
}
