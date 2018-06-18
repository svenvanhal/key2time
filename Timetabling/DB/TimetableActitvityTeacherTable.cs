using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Table linking teachers and activities.
    /// </summary>
    [Table("TimeTableActitvityTeacher")]
    public class TimetableActitvityTeacherTable
    {

        /// <summary>
        /// ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Teacher ID.
        /// </summary>
        [Column("teacherRefId")]
        public long TeacherId { get; set; }

        /// <summary>
        /// Timetable ID.
        /// </summary>
        [Column("timeTableActivityRefId")]
        public long ActivityId { get; set; }
    }
}
