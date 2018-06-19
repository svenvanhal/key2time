using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Table linking classes and activities.
    /// </summary>
    [Table("TimeTableActitvityClass")]
    public class TimetableActivityClassTable
    {

        /// <summary>
        /// ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Activity ID.
        /// </summary>
        [Column("TimeTableActivityRefId")]
        public long ActivityId { get; set; }

        /// <summary>
        /// Class ID.
        /// </summary>
        [Column("ClassId")]
        public long ClassId { get; set; }
    }
}
