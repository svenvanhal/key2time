using System;
using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;

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
                        group new { id = activity.GroupId, activity.TotalDuration, activity.Duration }
                        by activity.GroupId into g
                        where g.Count() > 1
                        select g;

            var result = new List<ConstraintMinDaysBetweenActivities>();
            foreach (var item in query)
            {
                var firstElement = item.First();
                result.Add(new ConstraintMinDaysBetweenActivities { GroupID = firstElement.id, NumberOfActivities = (int)Math.Ceiling(firstElement.TotalDuration / (double)firstElement.Duration) });
            }
            return result.Select(item => item.ToXelement()).ToArray();

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
