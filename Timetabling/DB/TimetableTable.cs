using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{
    
    /// <summary>
    /// Table containing all timetables.
    /// </summary>
    [Table("TimeTable")]
    public class TimetableTable
    {

        /// <summary>
        /// Timetable ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the timetable.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Section ID.
        /// </summary>
        [Column("sectionId")]
        public int SectionId { get; set; }

        /// <summary>
        /// Quarter ID.
        /// </summary>
        [Column("quarterId")]
        public int QuarterId { get; set; }

        /// <summary>
        /// Academic year ID.
        /// </summary>
        [Column("academicYearId")]
        public int AcademicYearId { get; set; }

        /// <summary>
        /// Creation date of timetable.
        /// </summary>
        [Column("creattionDate", TypeName = "datetime2")]
        public DateTime CreationDate { get; set; }
    }
}
