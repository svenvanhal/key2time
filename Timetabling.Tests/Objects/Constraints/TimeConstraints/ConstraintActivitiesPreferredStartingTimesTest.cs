using System.Linq;
using NUnit.Framework;
using Timetabling.Objects.Constraints.TimeConstraints;

namespace Timetabling.Tests.Objects.Constraints.TimeConstraints
{

    internal class ConstraintActivitiesPreferredStartingTimesTest
    {

        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintActivitiesPreferredStartingTimes();
            var array = new bool[7, 9];
            array[2, 2] = true;
            var constraintTest = new ConstraintActivitiesPreferredStartingTimes { TimeOffArray = array, SubjectId = 1 };
            var result = constraint.Create(new TestDataModel().MockDataModel.Object);

            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
        }
    }
}
