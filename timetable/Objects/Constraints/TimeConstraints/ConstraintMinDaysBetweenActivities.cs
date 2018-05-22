using System;
using System.Xml.Linq;
using Timetable.timetable.DB;
using System.Linq;

namespace Timetable.timetable.Objects.Constraints.TimeConstraints
{
	public class ConstraintMinDaysBetweenActivities : AbstractConstraint
	{

		public int numberOfActivities { get; set; }
		public int groupId { get; set; }
		int minDays { get; set; } = 1;


		public ConstraintMinDaysBetweenActivities()
		{
			SetElement("ConstraintMinDaysBetweenActivities");
			SetWeight(95);
		}

		public override XElement[] Create(DataModel dB)
		{
			ActivitiesList activitiesList = new ActivitiesList(dB);
			activitiesList.Create();
			var query = from activity in activitiesList.GetList().Elements("Activity")
			                                           select new { id = activity.Elements("Activity_Group_Id").First().Value, duration = activity.Elements("Total_Duration").First().Value };
            
			var result = query.Distinct().Select(item => new ConstraintMinDaysBetweenActivities { groupId = Int32.Parse(item.id), numberOfActivities = Int32.Parse(item.duration) }.ToXelement()).ToArray();
			return result;
		}

		public override XElement ToXelement()
		{
			constraint.Add(new XElement("Consecutive_If_Same_Day", true));
			constraint.Add(new XElement("Number_of_Activities", numberOfActivities));
			for (int i = 0; i < numberOfActivities; i++)
			{
				constraint.Add(new XElement("Activity_Id", groupId + i));

			}
			constraint.Add(new XElement("MinDays", minDays));
			return constraint;
		}
	}
}
