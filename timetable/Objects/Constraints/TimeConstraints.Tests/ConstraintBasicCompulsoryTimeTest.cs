﻿using NUnit.Framework;
using System;
namespace Timetable.timetable.Objects.Constraints.TimeConstraints.Tests
{
	[TestFixture()]
	public class ConstraintBasicCompulsoryTimeTest
	{
		[Test()]
		public void TestWeight()
		{
			ConstraintBasicCompulsoryTime constraintBasicCompulsoryTime = new ConstraintBasicCompulsoryTime();
			Assert.AreEqual(constraintBasicCompulsoryTime.weight, 100);
		}
        
		[Test()]
		public void TestToXElement()
		{
			ConstraintBasicCompulsoryTime constraintBasicCompulsoryTime = new ConstraintBasicCompulsoryTime();
			Assert.AreEqual(constraintBasicCompulsoryTime.ToXelement().ToString(), "<ConstraintBasicCompulsoryTime>\n  <Weight_Percentage>100</Weight_Percentage>\n</ConstraintBasicCompulsoryTime>");
		}

		[Test()]
		public void TestToXElementHasElement()
		{
			ConstraintBasicCompulsoryTime constraintBasicCompulsoryTime = new ConstraintBasicCompulsoryTime();
			Assert.IsTrue(constraintBasicCompulsoryTime.ToXelement().HasElements);
		}
	}
}