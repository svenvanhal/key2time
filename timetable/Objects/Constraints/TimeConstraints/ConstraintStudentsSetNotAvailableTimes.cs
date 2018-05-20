using System;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects.Constraints.TimeConstraints
{
	public class ConstraintStudentsSetNotAvailableTimes : AbstractConstraint
    {

		int numberOfHours;
		string students;
		Days day;
		string hour;

        public ConstraintStudentsSetNotAvailableTimes()
        {
			SetElement("ConstraintStudentsSetNotAvailableTimes");
			SetWeight(100);
        }

		public override XElement[] Create(DataModel dB)
		{
			throw new NotImplementedException();
		}

		public override XElement ToXelement()
		{
			constraint.Add(new XElement("Students", students),
			               new XElement("Number_of_Not_Available_Times", numberOfHours),
			               new XElement("Not_Available_Time", 
			                            new XElement("Day", day), 
			                            new XElement("Hour", hour)));
            return constraint;
		}
	}
}
