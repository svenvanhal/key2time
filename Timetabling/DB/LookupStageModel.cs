using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// School lookup stage.
    /// </summary>
    [Table("School_Lookup_Stage")]
    public class LookupStageModel
	{

		/// <summary>
		/// Gets or sets the stage identifier.
		/// </summary>
		/// <value>The stage identifier.</value>
		[Key, Column("StageID")]
		public int StageId { get; set; }

        /// <summary>
        /// Gets or sets the section identifier.
        /// </summary>
        /// <value>The section identifier.</value>
        [Column("SectionId")]
        public int? SectionId { get; set; }

		/// <summary>
		/// Gets or sets the is active.
		/// </summary>
		/// <value>The is active.</value>
		public bool? IsActive { get; set; }

	}
}
