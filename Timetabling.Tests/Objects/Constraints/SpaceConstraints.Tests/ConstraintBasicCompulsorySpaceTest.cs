using NUnit.Framework;
using Timetabling.Objects.Constraints.SpaceConstraints;

namespace Timetabling.Tests.Objects.Constraints.SpaceConstraints.Tests
{
    [TestFixture()]
    internal class ConstraintBasicCompulsorySpaceTest
    {

        [Test()]
        public void TestWeight()
        {
            var constraintBasicCompulsorySpace = new ConstraintBasicCompulsorySpace();
            Assert.AreEqual("100", constraintBasicCompulsorySpace.ToXelement().Element("Weight_Percentage").Value);
        }

        [Test()]
        public void TestToXElement()
        {
            var constraintBasicCompulsorySpace = new ConstraintBasicCompulsorySpace();
            Assert.AreEqual("<ConstraintBasicCompulsorySpace>" + System.Environment.NewLine +
                            "  <Weight_Percentage>100</Weight_Percentage>" + System.Environment.NewLine + "</ConstraintBasicCompulsorySpace>", constraintBasicCompulsorySpace.ToXelement().ToString());
        }

        [Test()]
        public void TestToXElementHasElement()
        {
            var constraintBasicCompulsorySpace = new ConstraintBasicCompulsorySpace();
            Assert.IsTrue(constraintBasicCompulsorySpace.ToXelement().HasElements);
        }

        [Test]
        public void CreateTest()
        {
            var constraintBasicCompulsorySpace = new ConstraintBasicCompulsorySpace();
            Assert.IsEmpty(constraintBasicCompulsorySpace.Create(null));
        }

    }
}
