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
			ConstraintStudentsSetMaxHoursContinuously constraint = new ConstraintStudentsSetMaxHoursContinuously {numberOfHours = 1, gradeName = "test" };
			Assert.AreEqual(constraint.weight, 100);
			Assert.AreEqual(constraint.gradeName, "test");
			Assert.AreEqual(constraint.numberOfHours, 1);
		}

		[Test()]
		public void TestToXElement()

		{
			ConstraintStudentsSetMaxHoursContinuously constraint = new ConstraintStudentsSetMaxHoursContinuously {numberOfHours = 1, gradeName ="test" };
			Assert.AreEqual("<ConstraintStudentsSetMaxHoursContinuously>"+System.Environment.NewLine+"  <Weight_Percentage>100</Weight_Percentage>"+System.Environment.NewLine+"  <Maximum_Hours_Continuously>1</Maximum_Hours_Continuously>" + System.Environment.NewLine + "  <Students>test</Students>" + System.Environment.NewLine + "</ConstraintStudentsSetMaxHoursContinuously>", constraint.ToXelement().ToString());
		}
	}

}
