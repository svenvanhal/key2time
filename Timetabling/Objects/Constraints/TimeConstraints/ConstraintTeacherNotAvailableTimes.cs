
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

        public int numberOfHours { get; set; } = 0;
        /// <summary>
        /// Gets or sets the teacher id.
        /// </summary>
        /// <value>The teacher.</value>
        public int teacher { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        public List<Days> days { get; set; } = new List<Days>();

        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>The hour.</value>
        public List<int> hours { get; set; } = new List<int>();

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetabling.Objects.Constraints.TimeConstraints.ConstraintTeacherNotAvailableTimes"/> class.
        /// </summary>
        public ConstraintTeacherNotAvailableTimes()
        {
            SetElement("ConstraintTeacherNotAvailableTimes");

        }
        /// <summary>
        /// Creates the array of XElements for the constraint.
        /// </summary>
        /// <returns>The created array.</returns>
        /// <param name="dB">Datamodel.</param>
        public override XElement[] Create(DataModel dB)
        {
            var query = from tf in dB.Tt_TimeOff
                        where tf.ItemType == 1
                        join e in dB.HR_MasterData_Employees on tf.ItemId equals e.EmployeeID
                        where e.IsActive == true && e.IsTeacher == true
                        select new { tf.day, tf.ItemId, tf.lessonIndex, e.timeOffConstraint };

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
                    result.Add(new ConstraintTeacherNotAvailableTimes { teacher = item.ItemId, days = _daysList, hours = _hoursList, numberOfHours = _hoursList.Count, weight = item.timeOffConstraint }.ToXelement());
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
            SetWeight(weight);
            constraint.Add(new XElement("Teacher", teacher),
                           new XElement("Number_of_Not_Available_Times", numberOfHours));
            for (int i = 0; i < numberOfHours; i++)
            {

                constraint.Add(new XElement("Not_Available_Time",
                                            new XElement("Day", days[i]),
                                            new XElement("Hour", hours[i])));
            }
            return constraint;
        }
    }
}
