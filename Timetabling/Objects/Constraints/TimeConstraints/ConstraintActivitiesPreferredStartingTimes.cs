using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Timetabling.Objects.Constraints.TimeConstraints
{

    /// <summary>
    /// Constraint activities preferred starting times.
    /// </summary>
    public class ConstraintActivitiesPreferredStartingTimes : AbstractConstraint
    {
        /// <summary>
        /// The subject.
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// Gets or sets the list of days.
        /// </summary>
        /// <value>The days.</value>
        public bool[,] TimeOffArray { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetabling.Objects.Constraints.TimeConstraints.ConstraintActivitiesPreferredStartingTimes"/> class.
        /// </summary>
        /// <param name="activitiesList">Activities list.</param>
        public ConstraintActivitiesPreferredStartingTimes()
        {
            SetElement("ConstraintActivitiesPreferredStartingTimes");
            SetWeight(100);
        }

        /// <summary>
        /// Create the constraints from the datamodel
        /// </summary>
        /// <returns>The created array of XElements</returns>
        /// <param name="dB">Datamodel.</param>
        public override XElement[] Create(DataModel dB)
        {
            var query = from tf in dB.TimesOff
                        join activity in dB.ClassTeacherSubjects on tf.ItemId equals activity.SubjectId
                        join sub in dB.SubjectGrades on tf.ItemId equals sub.SubjectId
                        where tf.ItemType == 2 && sub.CollectionId == null
                        group new { day = tf.Day, lessonIndex = tf.LessonIndex, tf.ItemId, sub.CollectionId }
            by tf.ItemId into g
                        select g;

            var result = new List<XElement>();
            var hours = new HoursList(dB);

            foreach (var item in query)
            {
                var array = new bool[Enum.GetValues(typeof(Days)).Length, hours.numberOfHours];
                foreach (var set in item)
                {
                    array[set.day, set.lessonIndex - 1] = true;
                }

                result.Add(new ConstraintActivitiesPreferredStartingTimes() { SubjectId = item.First().ItemId, TimeOffArray = array }.ToXelement());
            }

            return result.ToArray();
        }

        /// <summary>
        /// Returns the XElement representation
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {
            constraint.Add(new XElement("Subject_Name", SubjectId));
            var count = 0;

            for (int i = 0; i < TimeOffArray.GetLength(0); i++)
            {
                for (int y = 0; y < TimeOffArray.GetLength(1); y++)
                {
                    if (!TimeOffArray[i, y])
                    {
                        count++;
                        constraint.Add(new XElement("Preferred_Starting_Time",
                                                    new XElement("Preferred_Starting_Day", (Days)i),
                                                    new XElement("Preferred_Starting_Hour", y + 1)));
                    }
                }
            }
            constraint.Add(new XElement("Number_of_Preferred_Starting_Times", count));
            return constraint;
        }
    }
}
