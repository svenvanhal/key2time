using NUnit.Framework;

using Timetabling.Objects;
using System.Xml.Linq;


namespace Timetabling.Tests.Objects
{
    [TestFixture()]
    internal class TimeConstraintsListTest
    {

        XElement test;
        TimeConstraintsList TimeConstraintsList;

        [SetUp]
        public void Init()
        {
            var TestDataModel = new TestDataModel();

            TimeConstraintsList = new TimeConstraintsList(TestDataModel.MockDataModel.Object);
            TimeConstraintsList.Create();
            test = TimeConstraintsList.List;
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Time_Constraints_List", test.Name.ToString());
        }

        [Test]
        public void CreateTest()
        {
            Assert.AreEqual(5, TimeConstraintsList.Constraints.Count);
        }
    }
}
