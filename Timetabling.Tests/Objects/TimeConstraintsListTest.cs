using NUnit.Framework;
using Moq;
using Timetabling.Objects;
using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Tests.Objects
{
    [TestFixture()]
    internal class TimeConstraintsListTest
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
            Assert.AreEqual("Time_Constraints_List", test.Name.ToString());
        }

    }
}
