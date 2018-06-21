using System.Linq;
using NUnit.Framework;
using Timetabling.Objects.Constraints;

namespace Timetabling.Tests.Objects.Constraints
{

    public class ConstraintSubjectPrefferedRoomsTest
    {

        [Test]
        public void TestConstruct()
        {
            var constraint = new ConstraintSubjectPreferredRooms();
            Assert.AreEqual("ConstraintSubjectPreferredRooms", constraint.ToXelement().Name.ToString());
        }

        [Test]
        public void TestCreate(){
            var constraint = new ConstraintSubjectPreferredRooms();
            var constraintTest = new ConstraintSubjectPreferredRooms { Rooms = { 1 }, SubjectID = 1 };
            var constraintTest2 = new ConstraintSubjectPreferredRooms { Rooms = { 1 }, SubjectID = 3 };

            var result = constraint.Create(new TestDataModel().MockDataModel.Object);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }
}
