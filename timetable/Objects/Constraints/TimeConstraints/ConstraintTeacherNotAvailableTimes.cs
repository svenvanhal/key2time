using System;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects.Constraints.TimeConstraints
{
	public class ConstraintTeacherNotAvailableTimes : AbstractConstraint
    {

		int numberOfHours;
		string teacher;
		Days day;
		string hour;

        public ConstraintTeacherNotAvailableTimes()
        {
			SetElement("ConstraintTeacherNotAvailableTimes");
			SetWeight(100);
        }

		public override XElement[] Create(DataModel dB)
		{
			throw new NotImplementedException();
		}

		public override XElement ToXelement()
		{
			constraint.Add(new XElement("Teacher", teacher),
			               new XElement("Number_of_Not_Available_Times", numberOfHours),
			               new XElement("Not_Available_Time", 
			                            new XElement("Day", day), 
			                            new XElement("Hour", hour)));
            return constraint;
		}
	}
}
