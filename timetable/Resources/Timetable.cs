using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace Timetabling.Resources
{

    /// <remarks/>
    [XmlRoot(ElementName = "Activities_Timetable")]
    public class Timetable
    {

        /// <summary>
        /// List of all activities in this timetable.
        /// </summary>
        [XmlElement("Activity")]
        public TimetableActivity[] Activities { get; set; }

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
    }
}