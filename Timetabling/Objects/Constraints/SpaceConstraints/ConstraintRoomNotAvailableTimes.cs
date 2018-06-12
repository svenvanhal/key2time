using System.Xml.Linq;
using System.Linq;
using Timetabling.DB;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.SpaceConstraints
{

    /// <summary>
    /// Constraint for the timeoff of a room.
    /// </summary>
    public class ConstraintRoomNotAvailableTimes : AbstractConstraint
    {

        int numberOfHours = 1;

        /// <summary>
        /// Gets or sets the room id.
        /// </summary>
        /// <value>The room.</value>
        public int room { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        public Days day { get; set; }

        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>The hour.</value>
        public int hour { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetabling.Objects.Constraints.SpaceConstraints.ConstraintRoomNotAvailableTimes"/> class.
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
            var query = from tf in dB.Tt_TimeOff
                        where tf.ItemType == 4
                        join e in dB.School_BuildingsUnits on tf.ItemId equals e.ID
                        where e.IsActive == true
                        select new { tf.day, tf.ItemId, tf.lessonIndex };

            var result = new List<XElement>();
            query.AsEnumerable().ToList().ForEach(item => result.Add(new ConstraintRoomNotAvailableTimes { room = item.ItemId, day = (Days)item.day, hour = item.lessonIndex }.ToXelement()));

            return result.ToArray();
        }

        /// <summary>
        /// Returns the XElement representation of the constraint
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {
            constraint.Add(new XElement("Room", room),
                           new XElement("Number_of_Not_Available_Times", numberOfHours),
                           new XElement("Not_Available_Time",
                                        new XElement("Day", day),
                                        new XElement("Hour", hour)));
            return constraint;
        }

    }
}
