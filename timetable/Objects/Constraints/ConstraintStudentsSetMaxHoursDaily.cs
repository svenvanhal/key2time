using System;
using System.Xml.Linq;

namespace Timetable.timetable.Objects
{
	public class ConstraintStudentsSetMaxHoursDaily : AbstractConstraint
    {
		int maxHoursDaily;
		string grade;
        public ConstraintStudentsSetMaxHoursDaily(int _maxHoursDaily, string _grade)
        {
			SetElement("ConstraintStudentsSetMaxHoursDaily");
            SetWeight(100); 
			maxHoursDaily = _maxHoursDaily;
			grade = _grade;
        }

		public override XElement ToXelement()
		{
			constraint.Add(new XElement("Maximum_Hours_Daily", maxHoursDaily),
                           new XElement("Students", grade));
            return constraint;
		}
	}
}
