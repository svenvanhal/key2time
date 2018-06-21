using System.Linq;
using NUnit.Framework;
using Timetabling.Objects;
using Timetabling.Objects.Constraints.TimeConstraints;

namespace Timetabling.Tests.Objects.Constraints.TimeConstraints
{

    internal class ConstraintTeacherSetNotAvailableTimesTest
    {

        [Test]
        public void TestConstruct()
        {
            ConstraintTeacherNotAvailableTimes constraint = new ConstraintTeacherNotAvailableTimes();
            Assert.AreEqual( "ConstraintTeacherNotAvailableTimes",constraint.ToXelement().Name.ToString());
        }


        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintTeacherNotAvailableTimes();
            var constraintTest = new ConstraintTeacherNotAvailableTimes { DaysList = { (Days)2 }, Teacher = 4, HoursList = { 3 }, weight = 50, NumberOfHours = 1 };
            var constraintTest2 = new ConstraintTeacherNotAvailableTimes { DaysList = { (Days)3, (Days)3, (Days)2 }, Teacher = 4, HoursList = { 3, 4, 5 }, weight = 30, NumberOfHours = 3 };

            var result = constraint.Create(new TestDataModel().MockDataModel.Object);
          
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));

            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }
}
