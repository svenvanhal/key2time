using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Information about the current academic year.
    /// </summary>
    [Table("Academic_Quarter")]
    public class AcademicQuarterModel
    {

        /// <summary>
        /// Row ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Whether or not this set is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Academic Year ID.
        /// </summary>
        [Column("AcademicYearID")]
        public int? AcademicYearId { get; set; }

        /// <summary>
        /// Quarter ID.
        /// </summary>
        public int? QuarterId { get; set; }


        /// <summary>
        /// Section ID.
        /// </summary>
        public int? SectionId { get; set; }
    }
}
