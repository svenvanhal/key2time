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
    internal class TeachersListTest
    {

        XElement test;

        [SetUp]
        public void Init()
        {
            var TestDataModel = new TestDataModel();
            var list = new TeachersList(TestDataModel.MockDataModel.Object);
            test = list.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Teachers_List", test.Name.ToString());
        }

        [Test]
        public void TeacherRightTest()
        {
            Assert.AreEqual(1, test.Elements("Teacher").Elements("Name").Count(item => item.Value.Equals("0")));
        }

        [Test]
        public void TeacherNotTeacher()
        {
            Assert.AreEqual(0, test.Elements("Teacher").Elements("Name").Count(item => item.Value.Equals("1")));
        }

        [Test]
        public void TeacherNotActive()
        {
            Assert.AreEqual(0, test.Elements("Teacher").Elements("Name").Count(item => item.Value.Equals("2")));
        }

    }
}
