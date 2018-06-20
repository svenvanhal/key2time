using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;

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
        /// The number of hours.
        /// </summary>
        public int NumberOfHours { get; set; }

        /// <summary>
        /// Gets or sets the list of days.
        /// </summary>
        /// <value>The days.</value>
        public List<Days> DaysList { get; set; } = new List<Days>();

        /// <summary>
        /// Gets or sets the list of hours.
        /// </summary>
        /// <value>The hours.</value>
        public List<int> HoursList { get; set; } = new List<int>();

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetabling.Objects.Constraints.TimeConstraints.ConstraintActivitiesPreferredStartingTimes"/> class.
        /// </summary>
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
                        where tf.ItemType == 2
                        select new { day = tf.Day, tf.ItemId, lessonIndex = tf.LessonIndex };

            var result = new List<XElement>();

            var check = new List<int>(); //List to check subject already done

            foreach (var item in query)
            {
                if (!check.Contains(item.ItemId))
                {
                    check.Add(item.ItemId);

                    var subjectTimeOff = query.Where(x => x.ItemId.Equals(item.ItemId)).Select(x => new { x.day, x.lessonIndex });
                    var daysList = subjectTimeOff.Select(x => (Days)x.day).ToList();
                    var hoursList = subjectTimeOff.Select(x => x.lessonIndex).ToList();
                    result.Add(new ConstraintActivitiesPreferredStartingTimes { SubjectId = item.ItemId, DaysList = daysList, HoursList = hoursList, NumberOfHours = hoursList.Count }.ToXelement());
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Returns the XElement representation
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {
            constraint.Add(new XElement("Subject_Name", SubjectId),
                           new XElement("Number_of_Preferred_Starting_Times", NumberOfHours));
            for (int i = 0; i < NumberOfHours; i++)
            {
                constraint.Add(new XElement("Preferred_Starting_Time",
                                            new XElement("Preferred_Starting_Day", DaysList[i]),
                                            new XElement("Preferred_Starting_Hour", HoursList[i])));
            }
            return constraint;
        }
    }
}
