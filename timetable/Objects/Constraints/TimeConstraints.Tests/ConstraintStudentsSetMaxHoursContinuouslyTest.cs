using NUnit.Framework;
using System;
namespace Timetable.timetable.Objects.Constraints.TimeConstraints.Tests
{
	[TestFixture()]
	public class ConstraintStudentsSetMaxHoursContinuouslyTest
	{
		[Test()]
		public void TestConstruct()

		{
			ConstraintStudentsSetMaxHoursContinuously constraint = new ConstraintStudentsSetMaxHoursContinuously(1, "test");
			Assert.AreEqual(constraint.weight, 100);
			Assert.AreEqual(constraint.gradeName, "test");
			Assert.AreEqual(constraint.numberOfHours, 1);
		}

		[Test()]
		public void TestToXElement()

		{
			ConstraintStudentsSetMaxHoursContinuously constraint = new ConstraintStudentsSetMaxHoursContinuously(1, "test");
			Assert.AreEqual("<ConstraintStudentsSetMaxHoursContinuously>\n  <Weight_Percentage>100</Weight_Percentage>\n  <Maximum_Hours_Continuously>1</Maximum_Hours_Continuously>\n  <Students>test</Students>\n</ConstraintStudentsSetMaxHoursContinuously>", constraint.ToXelement().ToString());
		}
	}

}
