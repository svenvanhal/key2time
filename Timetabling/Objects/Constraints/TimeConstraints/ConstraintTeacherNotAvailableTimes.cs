using System;
using System.Xml.Linq;
using System.Linq;
using Timetabling.DB;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.TimeConstraints
{
	public class ConstraintTeacherNotAvailableTimes : AbstractConstraint
	{

		int numberOfHours = 1;
		public int teacher { get; set; }
		public Days day { get; set; }
		public int hour { get; set; }

		public ConstraintTeacherNotAvailableTimes()
		{
			SetElement("ConstraintTeacherNotAvailableTimes");

		}

		public override XElement[] Create(DataModel dB)
		{
			var query = from tf in dB.Tt_TimeOff
						where tf.ItemType == 1
						join e in dB.HR_MasterData_Employees on tf.ItemId equals e.EmployeeID
						select new { tf.day, tf.ItemId, tf.lessonIndex, e.timeOffConstraint };

			var result = new List<XElement>();
			query.AsEnumerable().ToList().ForEach(item => result.Add(new ConstraintTeacherNotAvailableTimes { teacher = item.ItemId, day = (Days)item.day, hour = item.lessonIndex, weight = item.timeOffConstraint }.ToXelement()));

			return result.ToArray();
		}

		public override XElement ToXelement()
		{
			SetWeight(weight);
			constraint.Add(new XElement("Teacher", teacher),
						   new XElement("Number_of_Not_Available_Times", numberOfHours),
						   new XElement("Not_Available_Time",
										new XElement("Day", day),
										new XElement("Hour", hour)));
			return constraint;
		}
	}
}
