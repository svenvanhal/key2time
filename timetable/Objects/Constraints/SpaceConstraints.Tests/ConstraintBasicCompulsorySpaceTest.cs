using NUnit.Framework;
using System;
namespace Timetabling.Objects.Constraints.TimeConstraints.Tests
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
			Assert.AreEqual( "<ConstraintBasicCompulsorySpace>" +System.Environment.NewLine+
			                "  <Weight_Percentage>100</Weight_Percentage>" + System.Environment.NewLine +"</ConstraintBasicCompulsorySpace>", constraintBasicCompulsorySpace.ToXelement().ToString());
		}

		[Test()]
		public void TestToXElementHasElement()
		{
			ConstraintBasicCompulsorySpace constraintBasicCompulsorySpace = new ConstraintBasicCompulsorySpace();
			Assert.IsTrue(constraintBasicCompulsorySpace.ToXelement().HasElements);
		}

		[Test]
		public void CreateTest(){
			ConstraintBasicCompulsorySpace constraintBasicCompulsorySpace = new ConstraintBasicCompulsorySpace();
			Assert.IsEmpty(constraintBasicCompulsorySpace.Create(null));
		}
	}
}