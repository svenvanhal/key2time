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
    internal class ActivitiesListTest
    {

        XElement test;

        ActivitiesList list;

        [SetUp]
        public void Init()
        {
            var testDataModel = new TestDataModel();
            list = new ActivitiesList(testDataModel.MockDataModel.Object);
            test = list.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Activities_List", test.Name.ToString());
        }

        [Test]
        public void ActivityIDRightTest()
        {
            Assert.AreEqual(1, test.Elements("Activity").Elements("Id").Count(item => item.Value.Equals("1")));

        }

        [Test]
        public void ActivityGroupIDTest()
        {
            Assert.AreEqual(4, test.Elements("Activity").Elements("Activity_Group_Id").Count(item => item.Value.Equals("1")));

        }
        [Test]
        public void ActivityTeacherRightTest()
        {
            Assert.AreEqual(4, test.Elements("Activity").Elements("Teacher").Count(item => item.Value.Equals("0")));

        }

        [Test]
        public void ActivityTeacherWrongTest()
        {
            Assert.AreEqual(0, test.Elements("Activity").Elements("Teacher").Count(item => item.Value.Equals("3")));

        }

        [Test]
        public void ActivitySubjectRightTest()
        {
            Assert.AreEqual(2, test.Elements("Activity").Elements("Subject").Count(item => item.Value.Equals("0")));

        }

        [Test]
        public void ActivitySubjectWrongTest()
        {
            Assert.AreEqual(0, test.Elements("Activity").Elements("Subject").Count(item => item.Value.Equals("3")));

        }

        [Test]
        public void ActivityClassRightTest()
        {
            Assert.AreEqual(2, test.Elements("Activity").Elements("Students").Count(item => item.Value.Equals("test2")));

        }

        [Test]
        public void ActivityClassWrongTest()
        {
            Assert.AreEqual(0, test.Elements("Activity").Elements("Students").Count(item => item.Value.Equals("wrong")));

        }
    }
}
