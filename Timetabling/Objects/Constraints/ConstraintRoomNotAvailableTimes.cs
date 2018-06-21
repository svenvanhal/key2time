using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Objects.Constraints
{

    /// <summary>
    /// Constraint for the timeoff of a room.
    /// </summary>
    public class ConstraintRoomNotAvailableTimes : AbstractConstraint
    {

        /// <summary>
        /// The number of hours is always 1;
        /// </summary>
        public int NumberOfHours { get; set; } = 1;

        /// <summary>
        /// Gets or sets the room id.
        /// </summary>
        /// <value>The room.</value>
        public int Room { get; set; }

        /// <summary>
        /// Gets or sets the Day.
        /// </summary>
        /// <value>The Day.</value>
        public Days Day { get; set; }

        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>The hour.</value>
        public int Hour { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetabling.Objects.Constraints.ConstraintRoomNotAvailableTimes"/> class.
        /// </summary>
        public ConstraintRoomNotAvailableTimes()
        {
            SetElement("ConstraintRoomNotAvailableTimes");
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
                        where tf.ItemType == 4
                        join e in dB.Buildings on tf.ItemId equals e.Id
                        where e.IsActive == true
                        select new { day = tf.Day, tf.ItemId, lessonIndex = tf.LessonIndex };

            var result = new List<XElement>();
            query.AsEnumerable().ToList().ForEach(item => result.Add(new ConstraintRoomNotAvailableTimes { Room = item.ItemId, Day = (Days)item.day, Hour = item.lessonIndex }.ToXelement()));

            return result.ToArray();
        }

        /// <summary>
        /// Returns the XElement representation of the constraint
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {
            constraint.Add(new XElement("Room", Room),
                           new XElement("Number_of_Not_Available_Times", NumberOfHours),
                           new XElement("Not_Available_Time",
                                        new XElement("Day", Day),
                                        new XElement("Hour", Hour)));
            return constraint;
        }

    }
}
