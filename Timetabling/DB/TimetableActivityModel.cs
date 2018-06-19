using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Table containing all activities for timetables.
    /// </summary>
    [Table("TimeTableActivity")]
    public class TimetableActivityModel
    {

        /// <summary>
        /// Activity ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Timetable to which this activity belongs.
        /// </summary>
        [Column("timeTableRefId")]
        public int TimetableId { get; set; }

        /// <summary>
        /// Subject of activity.
        /// </summary>
        [Column("SubjectId")]
        public long? SubjectId { get; set; }

        /// <summary>
        /// Whether or not this activity is for a collection of subjects.
        /// </summary>
        [Column("isCollection", Order = 2)]
        public bool IsCollection { get; set; }

        /// <summary>
        /// Collection ID.
        /// </summary>
        [Column("collectionRefId")]
        public int? CollectionId { get; set; }

        /// <summary>
        /// The Day this activity is scheduled for.
        /// </summary>
        [Column("DayIndex")]
        public int Day { get; set; }

        /// <summary>
        /// The timeslot this activity is scheduled for.
        /// </summary>
        [Column("timeSlotOrder")]
        public int Timeslot { get; set; }
    }
}
