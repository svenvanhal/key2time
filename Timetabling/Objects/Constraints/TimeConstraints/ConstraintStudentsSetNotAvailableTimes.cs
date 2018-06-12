using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.TimeConstraints
{
    /// <summary>
    /// Constraint specifing the time off of the studentset.
    /// </summary>
    internal class ConstraintStudentsSetNotAvailableTimes : AbstractConstraint
    {
        /// <summary>
        /// The number of hours is always 1;
        /// </summary>
        public int NumberOfHours { get; set; } = 0;
        /// <summary>
        /// Gets or sets the students.
        /// </summary>
        /// <value>The students.</value>
        public string Students { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        public List<Days> Days { get; set; } = new List<Days>();

        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>The hour.</value>
        public List<int> Hours { get; set; } = new List<int>();

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetabling.Objects.Constraints.TimeConstraints.ConstraintStudentsSetNotAvailableTimes"/> class.
        /// </summary>
        public ConstraintStudentsSetNotAvailableTimes()
        {
            SetElement("ConstraintStudentsSetNotAvailableTimes");
            SetWeight(100);
        }
        /// <summary>
        /// Creates the array of XElements for the constraint.
        /// </summary>
        /// <returns>The created array.</returns>
        /// <param name="dB">Datamodel.</param>
        public override XElement[] Create(DataModel dB)
        {
            var query = from tf in dB.Tt_TimeOff
                        join cl in dB.School_Lookup_Class on tf.ItemId equals cl.ClassID
                        where tf.ItemType == 3 & cl.IsActive == true
                        select new { tf.day, cl.ClassName, tf.lessonIndex };

            var result = new List<XElement>();
            var check = new List<string>();

            foreach (var item in query)
            {
                if (!check.Contains(item.ClassName))
                {
                    check.Add(item.ClassName);

                    var oneStudentSetTimeOff = query.Where(x => x.ClassName.Equals(item.ClassName)).Select(x => new { x.day, x.lessonIndex });
                    var _daysList = oneStudentSetTimeOff.Select(x => (Days)x.day).ToList();
                    var _hoursList = oneStudentSetTimeOff.Select(x => x.lessonIndex).ToList();
                    result.Add(new ConstraintStudentsSetNotAvailableTimes { Students = item.ClassName, Days = _daysList, Hours = _hoursList, NumberOfHours = _hoursList.Count }.ToXelement());
                }

            }

            return result.ToArray();
        }

        /// <summary>
        /// Return the XElement representation of the constraint
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {

            constraint.Add(new XElement("Students", Students),
                           new XElement("Number_of_Not_Available_Times", NumberOfHours));

            for (var i = 0; i < NumberOfHours; i++)
            {
                constraint.Add(new XElement("Not_Available_Time",
                                            new XElement("Day", Days[i]),
                                            new XElement("Hour", Hours[i])));
            }
            return constraint;
        }




    }
}
