using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Objects.Constraints
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
        /// <see cref="T:Timetabling.Objects.Constraints.ConstraintStudentsSetNotAvailableTimes"/> class.
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
            var query = from tf in dB.TimesOff
                        join cl in dB.ClassesLookup on tf.ItemId equals cl.ClassId
                        where tf.ItemType == 3 && cl.IsActive == true
                        select new { day = tf.Day, cl.ClassName, lessonIndex = tf.LessonIndex };
            var result = new List<XElement>();
            var check = new List<string>();

            foreach (var item in query)
            {
                // Skip if duplicate
                if (check.Contains(item.ClassName)) continue;
                check.Add(item.ClassName);

                var oneStudentSetTimeOff = query.Where(x => x.ClassName.Equals(item.ClassName)).Select(x => new { x.day, x.lessonIndex });
                var daysList = oneStudentSetTimeOff.Select(x => (Days)x.day).ToList();
                var hoursList = oneStudentSetTimeOff.Select(x => x.lessonIndex).ToList();
                result.Add(new ConstraintStudentsSetNotAvailableTimes { Students = item.ClassName, DaysList = daysList, HoursList = hoursList, NumberOfHours = hoursList.Count }.ToXelement());

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
                                            new XElement("Day", DaysList[i]),
                                            new XElement("Hour", HoursList[i])));
            }
            return constraint;
        }




    }
}
