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
        public int NumberOfActivities { get; set; }

        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        /// <value>The group identifier.</value>
        public int GroupID { get; set; }

        /// <summary>
        /// Gets or sets the minimum days. If not specified, it will be 1.
        /// </summary>
        /// <value>The minimum days.</value>
        public int MinimumDays { get; set; } = 1;

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

            var query = from activity in activitiesList.Activities.Values
                        select new { id = activity.GroupId, duration = activity.TotalDuration, length = activity.Duration };

            var result = query.Distinct().Select(item => new ConstraintMinDaysBetweenActivities { GroupID = item.id, NumberOfActivities = item.duration / item.length }.ToXelement()).ToArray();
            return result;
        }

        /// <summary>
        /// Returns the XElement representation of the constraint
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {
            constraint.Add(new XElement("Consecutive_If_Same_Day", true));
            constraint.Add(new XElement("Number_of_Activities", NumberOfActivities));
            for (int i = 0; i < NumberOfActivities; i++)
            {
                constraint.Add(new XElement("Activity_Id", GroupID + i));

            }
            constraint.Add(new XElement("MinDays", MinimumDays));
            return constraint;
        }
    }
}
