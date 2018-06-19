using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// School lookup grade.
    /// </summary>
    [Table("School_Lookup_Grade")]
    public class LookupGradeModel
	{

		/// <summary>
		/// Gets or sets the grade identifier.
		/// </summary>
		/// <value>The grade identifier.</value>
		[Key, Column("GradeID")]
		public int GradeId { get; set; }

		/// <summary>
		/// Gets or sets the name of the grade.
		/// </summary>
		/// <value>The name of the grade.</value>
		[Required]
		[StringLength(50)]
		public string GradeName { get; set; }

        /// <summary>
        /// Gets or sets the stage identifier.
        /// </summary>
        /// <value>The stage identifier.</value>
        [Column("StageId")]
        public int? StageId { get; set; }

		/// <summary>
		/// Gets or sets the is active.
		/// </summary>
		/// <value>The is active.</value>
		public bool? IsActive { get; set; }

	}
}
