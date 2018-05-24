using System;
using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;

namespace Timetabling.Objects.Constraints.TimeConstraints
{
	/// <summary>
    /// Constraint that specifies the minimum days between two lessons/activities
    /// </summary>
	public class ConstraintMinDaysBetweenActivities : AbstractConstraint
	{

        /// <summary>
        /// Gets or sets the number of activities.
        /// </summary>
        /// <value>The number of activities.</value>
		public int numberOfActivities { get; set; }
        
		/// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        /// <value>The group identifier.</value>
		public int groupId { get; set; }

        /// <summary>
        /// Gets or sets the minimum days. If not specified, it will be 1.
        /// </summary>
        /// <value>The minimum days.</value>
		int minDays { get; set; } = 1;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetabling.Objects.Constraints.TimeConstraints.ConstraintMinDaysBetweenActivities"/> class.
        /// </summary>
		public ConstraintMinDaysBetweenActivities()
		{
			SetElement("ConstraintMinDaysBetweenActivities");
			SetWeight(95);
		}

		/// <summary>
        /// Creates the array of XElements for the constraint.
        /// </summary>
        /// <returns>The created array.</returns>
        /// <param name="dB">Datamodel.</param>
		public override XElement[] Create(DataModel dB)
		{
			ActivitiesList activitiesList = new ActivitiesList(dB);
			activitiesList.Create();
			var query = from activity in activitiesList.GetList().Elements("Activity")
			                                           select new { id = activity.Elements("Activity_Group_Id").First().Value, duration = activity.Elements("Total_Duration").First().Value , length = activity.Elements("Duration").First().Value};
            
			var result = query.Distinct().Select(item => new ConstraintMinDaysBetweenActivities { groupId = Int32.Parse(item.id), numberOfActivities = Int32.Parse(item.duration) / Int32.Parse(item.length) }.ToXelement()).ToArray();
			return result;
		}

        /// <summary>
        /// Returns the XElement representation of the constraint
        /// </summary>
        /// <returns>The xelement.</returns>
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
