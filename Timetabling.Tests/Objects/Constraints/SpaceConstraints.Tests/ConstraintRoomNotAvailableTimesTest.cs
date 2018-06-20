using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using Timetabling.DB;
using Timetabling.Objects;
using Timetabling.Objects.Constraints.SpaceConstraints;

namespace Timetabling.Tests.Objects.Constraints.SpaceConstraints.Tests
{
    [TestFixture()]
    internal class ConstraintRoomNotAvailableTimesTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            var TestDataModel = new TestDataModel();
            test = TestDataModel.MockDataModel;
        }

        [Test()]
        public void TestConstruct()
        {
            var constraint = new ConstraintRoomNotAvailableTimes();
            Assert.AreEqual("ConstraintRoomNotAvailableTimes", constraint.ToXelement().Name.ToString());
        }


        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintRoomNotAvailableTimes();
            var constraintTest = new ConstraintRoomNotAvailableTimes { Day = (Days)2, Room = 4, Hour = 3 };
            var constraintTest2 = new ConstraintRoomNotAvailableTimes { Day = (Days)3, Room = 4, Hour = 3 };

            var result = constraint.Create(test.Object);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }

}
