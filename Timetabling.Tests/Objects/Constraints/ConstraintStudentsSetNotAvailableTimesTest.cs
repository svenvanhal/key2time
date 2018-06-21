using System.Linq;
using NUnit.Framework;
using Timetabling.Objects;
using Timetabling.Objects.Constraints;

namespace Timetabling.Tests.Objects.Constraints
{

    internal class ConstraintStudentsSetNotAvailableTimesTest
    {

        [Test]
        public void TestConstruct()
        {
            var constraint = new ConstraintStudentsSetNotAvailableTimes();
            Assert.AreEqual("ConstraintStudentsSetNotAvailableTimes", constraint.ToXelement().Name.ToString());
        }

        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintStudentsSetNotAvailableTimes();
            var constraintTest = new ConstraintStudentsSetNotAvailableTimes { DaysList = { (Days)2 }, Students = "test", HoursList = { 3 }, NumberOfHours = 1, weight = 100 };
            var constraintTest2 = new ConstraintStudentsSetNotAvailableTimes { DaysList = { (Days)2, (Days)2 }, Students = "test2", HoursList = { 3, 2 }, NumberOfHours = 2, weight = 100 };

            var result = constraint.Create(new TestDataModel().MockDataModel.Object);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }

}
