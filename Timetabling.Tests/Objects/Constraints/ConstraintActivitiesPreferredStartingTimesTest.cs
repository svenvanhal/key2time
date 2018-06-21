using System.Linq;
using NUnit.Framework;
using Timetabling.Objects;
using Timetabling.Objects.Constraints;

namespace Timetabling.Tests.Objects.Constraints
{

    internal class ConstraintActivitiesPreferredStartingTimesTest
    {

        [Test]
        public void TestConstruct()

        {
            ConstraintActivitiesPreferredStartingTimes constraint = new ConstraintActivitiesPreferredStartingTimes();
            Assert.AreEqual("ConstraintActivitiesPreferredStartingTimes", constraint.ToXelement().Name.ToString());
        }


        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintActivitiesPreferredStartingTimes();
            var constraintTest = new ConstraintActivitiesPreferredStartingTimes { DaysList = { (Days)2 }, SubjectId = 1, HoursList = { 3 }, NumberOfHours = 1 };
            var constraintTest2 = new ConstraintActivitiesPreferredStartingTimes { DaysList = { (Days)3, (Days)3, (Days)2 }, SubjectId = 1, HoursList = { 3, 4, 5 }, NumberOfHours = 3 };

            var result = constraint.Create(new TestDataModel().MockDataModel.Object);

            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }
}
