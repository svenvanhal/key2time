using System;
using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.TimeConstraints
{
    /// <summary>
    /// Constraint activities same starting time.
    /// </summary>
    public class ConstraintActivitiesSameStartingTime : AbstractConstraint
    {
        List<int> IdList = new List<int>();

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetabling.Objects.Constraints.TimeConstraints.ConstraintActivitiesSameStartingTime"/> class.
        /// </summary>
        public ConstraintActivitiesSameStartingTime()
        {
            SetElement("ConstraintActivitiesSameStartingTime");
            SetWeight(100);
        }
        /// <summary>
        /// Create the constraints
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="dB">Datamodel.</param>
        public override XElement[] Create(DataModel dB)
        {
            ActivitiesList activitiesList = new ActivitiesList(dB);
            activitiesList.Create();

            List<int> check = new List<int>();
            List<XElement> result = new List<XElement>();

            var activities = activitiesList.Activities.Values;

            foreach (var item in activities)
            {
                if (item.CollectionId != -1 &&!check.Contains(item.CollectionId))
                {
                    check.Add(item.CollectionId);

                    //Gets the ids of the same lesson group
                    var ids = activities.Where(x => x.CollectionId == item.CollectionId).Select(x => new { x.Id, x.NumberLessonOfWeek });

                    //Groups the ids on the order od which lesson is first in the week
                    var group = from a in ids
                                group a.Id by a.NumberLessonOfWeek into g
                                select g.ToList();

                    foreach (var g in group)
                    {
                        if (g.Count > 1)
                            result.Add(new ConstraintActivitiesSameStartingTime { IdList = g }.ToXelement());
                    }
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Returns a XElement representation of the constraints
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {
            constraint.Add(new XElement("Number_of_Activities", IdList.Count));
            foreach (var item in IdList)
            {
                constraint.Add(new XElement("Activity_Id", item));
            }

            return constraint;
        }
    }
}
