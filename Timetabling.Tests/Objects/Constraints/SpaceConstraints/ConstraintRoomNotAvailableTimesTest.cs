using System.Linq;
using NUnit.Framework;
using Timetabling.Objects;
using Timetabling.Objects.Constraints.SpaceConstraints;

namespace Timetabling.Tests.Objects.Constraints.SpaceConstraints
{

    internal class ConstraintRoomNotAvailableTimesTest
    {

        [Test]
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

            var result = constraint.Create(new TestDataModel().MockDataModel.Object);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }

    }

}
