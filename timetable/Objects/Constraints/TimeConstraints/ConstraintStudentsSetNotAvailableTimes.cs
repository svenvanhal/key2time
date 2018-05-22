using System;
using System.Xml.Linq;
using Timetable.timetable.DB;
using System.Linq;
using System.Collections.Generic;

namespace Timetable.timetable.Objects.Constraints.TimeConstraints
{
	public class ConstraintStudentsSetNotAvailableTimes : AbstractConstraint
	{

		int numberOfHours = 1;
		public string students { get; set; }
		public Days day { get; set; }
		public int hour { get; set; }

		public ConstraintStudentsSetNotAvailableTimes()
		{
			SetElement("ConstraintStudentsSetNotAvailableTimes");

		}

		public override XElement[] Create(DataModel dB)
		{
			var query = from tf in dB.Tt_TimeOff
						join cl in dB.School_Lookup_Class on tf.ItemId equals cl.ClassID
						where tf.ItemType == 3

						select new { tf.day, cl.ClassName, tf.lessonIndex, cl.timeOffConstraint };

			var result = new List<XElement>();
			query.AsEnumerable().ToList().ForEach(item => result.Add(new ConstraintStudentsSetNotAvailableTimes { students = item.ClassName, day = (Days)item.day, hour = item.lessonIndex, weight = item.timeOffConstraint }.ToXelement()));

			return result.ToArray();
		}

		public override XElement ToXelement()
		{
			SetWeight(weight);
			constraint.Add(new XElement("Students", students),
						   new XElement("Number_of_Not_Available_Times", numberOfHours),
						   new XElement("Not_Available_Time",
										new XElement("Day", day),
										new XElement("Hour", hour)));
			return constraint;
		}
	}
}
