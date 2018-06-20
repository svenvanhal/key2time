using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Timetabling.Objects
{

    /// <remarks/>
    [XmlRoot(ElementName = "Activities_Timetable")]
    public class Timetable
    {

        /// <summary>
        /// List of all activities in this timetable.
        /// </summary>
        [XmlElement("Activity")]
        public List<TimetableActivity> Activities { get; set; }

        /// <summary>
        /// Number of actually scheduled activities in this timetable.
        /// </summary>
        [XmlIgnore]
        public int PlacedActivities { get; internal set; }

        /// <summary>
        /// Total weight of broken constraints.
        /// </summary>
        [XmlIgnore]
        public double ConflictWeight { get; internal set; }

        /// <summary>
        /// Contains all the violated soft constraints for this timetable. Directly passes through the FET warnings.
        /// </summary>
        [XmlIgnore]
        public List<string> SoftConflicts { get; internal set; }

        /// <summary>
        /// Academic Year ID.
        /// </summary>
        [XmlIgnore]
        public int AcademicYearId { get; set; }

        /// <summary>
        /// Quarter ID.
        /// </summary>
        [XmlIgnore]
        public int QuarterId { get; set; }

        /// <summary>
        /// Section ID.
        /// </summary>
        [XmlIgnore]
        public int SectionId { get; set; }

        /// <summary>
        /// Folder where the timetable generation logs are stored.
        /// </summary>
        [XmlIgnore]
        public string OutputFolder { get; set; }

        /// <summary>
        /// Describes whether this Timetable has been fully generated or just partially.
        /// </summary>
        public bool IsPartial => Activities != null && PlacedActivities < Activities.Count;

        /// <summary>
        /// Timetable activity, consisting of an id, a Day, an hour and a room.
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

            /// <summary>
            /// Original activity resource.
            /// </summary>
            [XmlIgnore]
            public Activity Resource { get; set; }

        }

        /// <inheritdoc />
        public override string ToString() => $"<Timetable[{PlacedActivities}/{Activities.Count} activities]>";
    }
}
