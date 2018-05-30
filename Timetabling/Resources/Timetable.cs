using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace Timetabling.Resources
{

    /// <remarks/>
    [XmlRoot(ElementName = "Activities_Timetable")]
    public class Timetable
    {

        /// <summary>
        /// Describes whether this Timetable has been fully generated or just partially.
        /// </summary>
        [XmlIgnore]
        public bool IsPartial { get; protected set; }

        /// <summary>
        /// List of all activities in this timetable.
        /// </summary>
        [XmlElement("Activity")]
        public List<TimetableActivity> Activities { get; set; }

        /// <summary>
        /// Constructs a Timetable object.
        /// </summary>
        /// <param name="partial">Whether or not this timetable is just partially complete.</param>
        public void SetPartialFlag(bool partial) => IsPartial = partial;

        /// <summary>
        /// Timetable activity, consisting of an id, a day, an hour and a room.
        /// </summary>
        public class TimetableActivity
        {

            /// <summary>
            /// Internal FET id of the activity, e.g. "1"
            /// </summary>
            [XmlElement(Form = XmlSchemaForm.Unqualified)]
            public string Id { get; set; }

            /// <summary>
            /// Day of the activity. e.g. "Monday"
            /// </summary>
            [XmlElement(Form = XmlSchemaForm.Unqualified)]
            public string Day { get; set; }

            /// <summary>
            /// Hour of the activity, e.g. "9.30 - 11.00"
            /// </summary>
            [XmlElement(Form = XmlSchemaForm.Unqualified)]
            public string Hour { get; set; }

            /// <summary>
            /// Hour of the activity, e.g. "B305"
            /// </summary>
            [XmlElement(Form = XmlSchemaForm.Unqualified)]
            public string Room { get; set; }

        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"<Timetable[ {Activities.Count} activities ]>";
        }

    }
}
