using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Timetabling.DB;
using System.Linq;
using System.Data.Entity;
using Timetabling.Objects.Constraints.SpaceConstraints;

namespace Timetabling.Tests.Objects.Constraints.SpaceConstraints.Tests
{
    [TestFixture()]
    public class ConstraintSubjectPrefferedRoomsTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            test = new TestDataModel().MockDataModel;
        }

        [Test()]
        public void TestConstruct()
        {
            var constraint = new ConstraintSubjectPreferredRooms();
            Assert.AreEqual("ConstraintSubjectPreferredRooms", constraint.ToXelement().Name.ToString());
        }


        [Test()]
        public void TestCreate(){
            var constraint = new ConstraintSubjectPreferredRooms();
            var constraintTest = new ConstraintSubjectPreferredRooms {Rooms = {1}, SubjectID = 1 };
            var constraintTest2 = new ConstraintSubjectPreferredRooms { Rooms = { 1 }, SubjectID = 3 };

            var result = constraint.Create(test.Object);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }
}
