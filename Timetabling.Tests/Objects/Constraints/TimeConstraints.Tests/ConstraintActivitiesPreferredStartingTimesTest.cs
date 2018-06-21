using NUnit.Framework;
using System.Linq;
using Moq;
using Timetabling.DB;
using System.Collections.Generic;
using System.Data.Entity;
using Timetabling.Objects.Constraints.TimeConstraints;
using Timetabling.Objects;

namespace Timetabling.Tests.Objects.Constraints.TimeConstraints.Tests
{
    [TestFixture()]
    internal class ConstraintActivitiesPreferredStartingTimesTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            test = new TestDataModel().MockDataModel;

        }

        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintActivitiesPreferredStartingTimes();
            var array = new bool[7, 9];
            array[2, 2] = true;
            var constraintTest = new ConstraintActivitiesPreferredStartingTimes { TimeOffArray = array, SubjectId = 1, };
            var result = constraint.Create(test.Object);

            result.ToList().ForEach(System.Console.WriteLine);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
        }
    }
}
