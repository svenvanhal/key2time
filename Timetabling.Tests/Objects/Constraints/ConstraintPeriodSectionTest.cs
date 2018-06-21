using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Timetabling.Objects;
using Timetabling.Objects.Constraints;

namespace Timetabling.Tests.Objects.Constraints
{

    internal class ConstraintPeriodSectionTest
    {

        [Test]
        public void TestConstruct()
        {
            var constraint = new ConstraintPeriodSection();
            Assert.AreEqual("ConstraintStudentsSetNotAvailableTimes", constraint.ToXelement().Name.ToString());
        }


        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintPeriodSection();
            var constraintTest = new ConstraintPeriodSection { DaysList = new List<Days> { (Days)0, (Days)1, (Days)3 }, NumberOfHours = 9, Students = "gradeTest" };
            var constraintTestFail = new ConstraintPeriodSection { DaysList = new List<Days> { 0 }, NumberOfHours = 9, Students = "not" };

            var result = constraint.Create(new TestDataModel().MockDataModel.Object);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTestFail.ToXelement().ToString())));
        }
    }

}
