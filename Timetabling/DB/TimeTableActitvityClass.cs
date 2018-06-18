using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Table linking classes and activities.
    /// </summary>
    [Table("TimeTableActitvityClass")]
    public class TimeTableActitvityClass
    {

        /// <summary>
        /// ID.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// Activity ID.
        /// </summary>
        [Column("TimeTableActivityRefId")]
        public long TimetableId { get; set; }

        /// <summary>
        /// Class ID.
        /// </summary>
        [Column("classId")]
        public long ClassId { get; set; }
    }
}
