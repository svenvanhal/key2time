using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// School lookup class.
    /// </summary>
    [Table("School_Lookup_Class")]
    public class LookupClassModel
    {

        /// <summary>
        /// Gets or sets the class identifier.
        /// </summary>
        /// <value>The class identifier.</value>
        [Key, Column("ClassID")]
        public int ClassId { get; set; }

        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>The name of the class.</value>
        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the grade identifier.
        /// </summary>
        /// <value>The grade identifier.</value>
        [Column("GradeID")]
        public int? GradeId { get; set; }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>The is active.</value>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the time off constraint.
        /// </summary>
        /// <value>The time off constraint.</value>
        [Column("timeOffConstraint")]
        public int TimeOffConstraint { get; set; }

    }
}
