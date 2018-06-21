using NUnit.Framework;
using Timetabling.Objects;
using System.Linq;
using System.Xml.Linq;

namespace Timetabling.Tests.Objects
{
    [TestFixture()]
    internal class YearsListTest
    {

        XElement test;

        [SetUp]
        public void Init()
        {
           
            var TestDataModel = new TestDataModel();
            var list = new YearsList(TestDataModel.MockDataModel.Object);
            test = list.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Students_List", test.Name.ToString());
        }


        [Test]
        public void ClassRightTest()
        {

            Assert.AreEqual(1, test.Elements("Year").Elements("Group").Elements("Name").Count(item => item.Value.Equals("test")));
            Assert.AreEqual(0, test.Elements("Year").Elements("Group").Elements("Name").Count(item => item.Value.Equals("notclassTest1")));

        }

        [Test]
        public void GradeRightTest()
        {

            Assert.AreEqual(1, test.Elements("Year").Elements("Name").Count(item => item.Value.Equals("gradeTest")));
            Assert.AreEqual(0, test.Elements("Year").Elements("Name").Count(item => item.Value.Equals("noTest")));

        }
    }
}
