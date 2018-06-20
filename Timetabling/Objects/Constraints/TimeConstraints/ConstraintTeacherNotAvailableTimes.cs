
using System.Xml.Linq;
using System.Linq;
using Timetabling.DB;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.TimeConstraints
{
    /// <summary>
    /// Constraint specifing the time off of a teacher
    /// </summary>
    public class ConstraintTeacherNotAvailableTimes : AbstractConstraint
    {

        public int NumberOfHours { get; set; } = 0;
        /// <summary>
        /// Gets or sets the teacher id.
        /// </summary>
        /// <value>The teacher.</value>
        public int Teacher { get; set; }

        /// <summary>
        /// Gets or sets the Day.
        /// </summary>
        /// <value>The Day.</value>
        public List<Days> DaysList { get; set; } = new List<Days>();

        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>The hour.</value>
        public List<int> HoursList { get; set; } = new List<int>();

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetabling.Objects.Constraints.TimeConstraints.ConstraintTeacherNotAvailableTimes"/> class.
        /// </summary>
        public ConstraintTeacherNotAvailableTimes()
        {
            SetElement("ConstraintTeacherNotAvailableTimes");
            SetWeight(100);
        }
        /// <summary>
        /// Creates the array of XElements for the constraint.
        /// </summary>
        /// <returns>The created array.</returns>
        /// <param name="dB">Datamodel.</param>
        public override XElement[] Create(DataModel dB)
        {
            var query = from tf in dB.TimesOff
                        where tf.ItemType == 1
                        join e in dB.Employees on tf.ItemId equals e.EmployeeId
                        where e.IsActive == true && e.IsTeacher == true
                        select new { day = tf.Day, tf.ItemId, lessonIndex = tf.LessonIndex, timeOffConstraint = e.TimeOffConstraint };

            var result = new List<XElement>();

            var check = new List<int>(); //List to check teachers already done

            foreach (var item in query)
            {
                if (!check.Contains(item.ItemId))
                {
                    check.Add(item.ItemId);

                    var oneTeacherTimeOff = query.Where(x => x.ItemId.Equals(item.ItemId)).Select(x => new { x.day, x.lessonIndex });
                    var _daysList = oneTeacherTimeOff.Select(x => (Days)x.day).ToList();
                    var _hoursList = oneTeacherTimeOff.Select(x => x.lessonIndex).ToList();
                    result.Add(new ConstraintTeacherNotAvailableTimes { Teacher = item.ItemId, DaysList = _daysList, HoursList = _hoursList, NumberOfHours = _hoursList.Count }.ToXelement());
                }

            }

            return result.ToArray();
        }

        /// <summary>
        /// Return the XElement represention of the constraint
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {
            
            constraint.Add(new XElement("Teacher", Teacher),
                           new XElement("Number_of_Not_Available_Times", NumberOfHours));
            for (int i = 0; i < NumberOfHours; i++)
            {

                constraint.Add(new XElement("Not_Available_Time",
                                            new XElement("Day", DaysList[i]),
                                            new XElement("Hour", HoursList[i])));
            }
            return constraint;
        }
    }
}
