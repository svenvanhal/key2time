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
            var data = new List<Tt_TimeOff>{
                new Tt_TimeOff{ItemId = 4, day = 2, lessonIndex = 3, ItemType = 2},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Tt_TimeOff>>();
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tt_TimeOff>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataAc = new List<tt_ActitvityGroup>{
                new tt_ActitvityGroup{ classId = 1, subjectId = 4,  teacherId = 0},
            }.AsQueryable();

            var mockSetAc = new Mock<DbSet<tt_ActitvityGroup>>();
            mockSetAc.As<IQueryable<tt_ActitvityGroup>>().Setup(m => m.Provider).Returns(dataAc.Provider);
            mockSetAc.As<IQueryable<tt_ActitvityGroup>>().Setup(m => m.Expression).Returns(dataAc.Expression);
            mockSetAc.As<IQueryable<tt_ActitvityGroup>>().Setup(m => m.ElementType).Returns(dataAc.ElementType);
            mockSetAc.As<IQueryable<tt_ActitvityGroup>>().Setup(m => m.GetEnumerator()).Returns(dataAc.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Tt_TimeOff).Returns(mockSet.Object);
            mockDB.Setup(item => item.tt_ActitvityGroup).Returns(mockSetAc.Object);

            test = mockDB;

        }

        [Test()]
        public void TestConstruct()

        {
            ConstraintActivitiesPreferredStartingTimes constraint = new ConstraintActivitiesPreferredStartingTimes();
            Assert.AreEqual("ConstraintActivitiesPreferredStartingTimes", constraint.ToXelement().Name.ToString());
        }


        [Test]
        public void CreateTest()
        {
            var constraint = new ConstraintActivitiesPreferredStartingTimes();
            var constraintTest = new ConstraintActivitiesPreferredStartingTimes { days = { (Days)2 }, subject = 4, hours = { 3 }, numberOfHours = 1 };
            var constraintTest2 = new ConstraintActivitiesPreferredStartingTimes { days = { (Days)3, (Days)3, (Days)2 }, subject = 4, hours = { 3, 4, 5 }, numberOfHours = 3 };

            var result = constraint.Create(test.Object);

            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }
}
