using NUnit.Framework;
using Moq;
using Timetabling.Objects;
using System.Xml.Linq;

namespace Timetabling.DB.Tests
{
    [TestFixture()]
    public class TimeConstraintsListTest
    {

        XElement test;
        TimeConstraintsList list;


        [SetUp]
        public void Init()
        {


            var mockDB = new Mock<DataModel>();

            list = new TimeConstraintsList(mockDB.Object);
            test = list.GetList();

        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Time_Constraints_List", test.Name.ToString());
        }

        [Test]
        public void CreateTest()
        {
            var mockDB = new Mock<DataModel>();
            var test = new Mock<TimeConstraintsList>(mockDB.Object);
            test.Setup(item => item.Create()).Verifiable();

            test.Object.Create();

            test.VerifyAll();

        }

    }
}
