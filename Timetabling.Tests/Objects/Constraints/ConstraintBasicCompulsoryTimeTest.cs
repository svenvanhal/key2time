using NUnit.Framework;
using Timetabling.Objects.Constraints;

namespace Timetabling.Tests.Objects.Constraints
{

    [TestFixture]
    internal class ConstraintBasicCompulsoryTimeTest
    {

        [Test]
        public void TestWeight()
        {
            var constraintBasicCompulsoryTime = new ConstraintBasicCompulsoryTime();
            Assert.AreEqual(100, constraintBasicCompulsoryTime.weight);
        }

        [Test]
        public void TestToXElement()
        {
            var constraintBasicCompulsoryTime = new ConstraintBasicCompulsoryTime();
            Assert.AreEqual("<ConstraintBasicCompulsoryTime>" + System.Environment.NewLine + "  <Weight_Percentage>100</Weight_Percentage>" + System.Environment.NewLine + "</ConstraintBasicCompulsoryTime>", constraintBasicCompulsoryTime.ToXelement().ToString());
        }

        [Test]
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
