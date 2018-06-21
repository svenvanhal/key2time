using System.Linq;
using Moq;
using NUnit.Framework;
using Timetabling.DB;
using Timetabling.Objects.Constraints;

namespace Timetabling.Tests.Objects.Constraints
{
    internal class ConstraintMinDaysBetweenActivitiesTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            test = new TestDataModel().MockDataModel;
        }

        [Test]
        public void TestConstruct()
        {
            var constraint = new ConstraintMinDaysBetweenActivities();
            Assert.AreEqual("ConstraintMinDaysBetweenActivities", constraint.ToXelement().Name.ToString());
        }


        [Test]
        public void CreateNumberOfActivitiesTest()
        {
            var constraint = new ConstraintMinDaysBetweenActivities();

            var result = constraint.Create(test.Object);
            Assert.AreEqual("4", result.First().Elements("Number_of_Activities").First().Value);
            Assert.AreNotEqual("6", result.First().Elements("Number_of_Activities").First().Value);
        }

        [Test]
        public void CreateNumberOfActivityElementsTest()
        {
            var constraint = new ConstraintMinDaysBetweenActivities();
            var result = constraint.Create(test.Object);

            Assert.AreEqual(4, result.First().Elements("Activity_Id").Count());
        }
    }

}
