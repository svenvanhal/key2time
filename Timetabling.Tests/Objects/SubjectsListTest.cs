using NUnit.Framework;
using Moq;
using System.Data.Entity;
using Timetabling.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Tests.Objects
{
    [TestFixture()]
    internal class SubjectListTest
    {

        XElement test;

        [SetUp]
        public void Init()
        {
            
            var TestDataModel = new TestDataModel();
            var list = new SubjectsList(TestDataModel.MockDataModel.Object);
            test = list.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Subjects_List", test.Name.ToString());
        }


        [Test]
        public void SubjectRightTest()
        {
            Assert.AreEqual(1, test.Elements("Subject").Elements("Name").Count(item => item.Value.Equals("1")));

        }

        [Test]
        public void SubjectNotInDB()
        {
            Assert.AreEqual(0, test.Elements("Subject").Elements("Name").Count(item => item.Value.Equals("4")));

        }

        [Test]
        public void SubjectNotActive()
        {
            Assert.AreEqual(0, test.Elements("Subject").Elements("Name").Count(item => item.Value.Equals("0")));

        }
    }
}
