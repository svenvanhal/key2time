using System;
using System.Xml.Linq;
using System.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class ConstraintStudentsSetMaxHoursContinuously : AbstractConstraint
	{
		int numberOfHours;
		string gradeName;

		public ConstraintStudentsSetMaxHoursContinuously(int _numberOfHours, string _gradeName)
		{

			SetElement("ConstraintStudentsSetMaxHoursContinuously");
			SetWeight(100);	
			numberOfHours = _numberOfHours;
			gradeName = _gradeName;
		}
              
		public override XElement ToXelement()
		{
			constraint.Add(new XElement("Maximum_Hours_Continuously", numberOfHours),
			               new XElement("Students", gradeName));
			return constraint;
		}
	}
}
