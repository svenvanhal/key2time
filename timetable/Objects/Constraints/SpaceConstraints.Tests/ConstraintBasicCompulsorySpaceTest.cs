using NUnit.Framework;
using System;
namespace Timetable.timetable.Objects.Constraints.TimeConstraints.Tests
{
	[TestFixture()]
	public class ConstraintBasicCompulsorySpaceTest
	{
		[Test()]
		public void TestWeight()
		{
			ConstraintBasicCompulsorySpace constraintBasicCompulsorySpace = new ConstraintBasicCompulsorySpace();
			Assert.IsTrue(constraintBasicCompulsorySpace.ToXelement().Element("Weight_Percentage").Value.Equals("100"));
		}

		[Test()]
		public void TestToXElement()
		{
			ConstraintBasicCompulsorySpace constraintBasicCompulsorySpace = new ConstraintBasicCompulsorySpace();
			Assert.AreEqual(constraintBasicCompulsorySpace.ToXelement().ToString(), "<ConstraintBasicCompulsorySpace>\n  <Weight_Percentage>100</Weight_Percentage>\n</ConstraintBasicCompulsorySpace>");
		}

		[Test()]
		public void TestToXElementHasElement()
		{
			ConstraintBasicCompulsorySpace constraintBasicCompulsorySpace = new ConstraintBasicCompulsorySpace();
			Assert.IsTrue(constraintBasicCompulsorySpace.ToXelement().HasElements);
		}
	}
}