using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Information about the current academic year.
    /// </summary>
    [Table("School_Lookup_AcademicYear")]
    public class AcademicYearModel
    {

        /// <summary>
        /// Row ID.
        /// </summary>
        [Key, Column("AcademicYearId")]
        public int AcademicYearId { get; set; }

        /// <summary>
        /// Whether or not this set is active.
        /// </summary>
        public bool? IsActive { get; set; }

    }
}
