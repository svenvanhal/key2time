using NUnit.Framework;
using Timetabling.Objects;
using System.Xml.Linq;

namespace Timetabling.Tests.Objects
{
    [TestFixture()]
    internal class SpaceConstraintsListTest
    {

        XElement test;
        SpaceConstraintsList spaceConstraintsList;

        [SetUp]
        public void Init()
        {
            var testDataModel = new TestDataModel();
            spaceConstraintsList = new SpaceConstraintsList(testDataModel.MockDataModel.Object);
            test = spaceConstraintsList.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Space_Constraints_List", test.Name.ToString());
        }

        [Test]
        public void CreateTest(){
            Assert.AreEqual(1, spaceConstraintsList.Constraints.Count);
        }

    }
}
