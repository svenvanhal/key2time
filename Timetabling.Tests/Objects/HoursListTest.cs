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
    public class HoursListTest
    {
        XElement test;

        [SetUp]
        public void Init()
        {

            var mockDB = new Mock<DataModel>();
            var `list = new HoursList(mockDB.Object);
            list.Create();

            test = list.GetList();

        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Hours_List", test.Name.ToString());
        }


        [Test]
        public void NumberOfDaysElementTest()
        {

            Assert.AreEqual("8", test.Elements("Number_of_Hours").First().Value);
        }
    }
}
