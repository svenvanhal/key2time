using NUnit.Framework;
using Timetabling.Objects;

namespace Timetabling.Tests.Objects.Constraints.TimeConstraints.Tests
{

    [TestFixture()]
    internal class ConstraintBasicCompulsoryTimeTest
    {

        [Test()]
        public void TestWeight()
        {
            var constraintBasicCompulsoryTime = new ConstraintBasicCompulsoryTime();
            Assert.AreEqual(constraintBasicCompulsoryTime.weight, 100);
        }

        [Test()]
        public void TestToXElement()
        {
            var constraintBasicCompulsoryTime = new ConstraintBasicCompulsoryTime();
            Assert.AreEqual("<ConstraintBasicCompulsoryTime>" + System.Environment.NewLine + "  <Weight_Percentage>100</Weight_Percentage>" + System.Environment.NewLine + "</ConstraintBasicCompulsoryTime>", constraintBasicCompulsoryTime.ToXelement().ToString());
        }

        [Test()]
        public void TestToXElementHasElement()
        {
            var constraintBasicCompulsoryTime = new ConstraintBasicCompulsoryTime();
            Assert.IsTrue(constraintBasicCompulsoryTime.ToXelement().HasElements);
        }

        [Test]
        public void CreateTest()
        {
            var constraintBasicCompulsoryTime = new ConstraintBasicCompulsoryTime();
            Assert.IsEmpty(constraintBasicCompulsoryTime.Create(null));
        }

    }

}
