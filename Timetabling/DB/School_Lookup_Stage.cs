namespace Timetabling.DB
{

	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// School lookup stage.
    /// </summary>
	public partial class School_Lookup_Stage
	{
		/// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.DB.School_Lookup_Stage"/> class.
        /// </summary>
		public School_Lookup_Stage()
		{
			School_Lookup_Grade = new HashSet<School_Lookup_Grade>();
		}
        /// <summary>
        /// Gets or sets the stage identifier.
        /// </summary>
        /// <value>The stage identifier.</value>
		[Key]
		public int StageID { get; set; }
        /// <summary>
        /// Gets or sets the stage code.
        /// </summary>
        /// <value>The stage code.</value>
		public int? StageCode { get; set; }
        /// <summary>
        /// Gets or sets the name of the stage.
        /// </summary>
        /// <value>The name of the stage.</value>
		[Required]
		[StringLength(50)]
		public string StageName { get; set; }
        /// <summary>
        /// Gets or sets the section identifier.
        /// </summary>
        /// <value>The section identifier.</value>
		public int? SectionID { get; set; }
        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>The is active.</value>
		public bool? IsActive { get; set; }
        /// <summary>
        /// Gets or sets the school lookup grade.
        /// </summary>
        /// <value>The school lookup grade.</value>
		public virtual ICollection<School_Lookup_Grade> School_Lookup_Grade { get; set; }
        /// <summary>
        /// Gets or sets the school lookup section.
        /// </summary>
        /// <value>The school lookup section.</value>
		public virtual School_Lookup_Section School_Lookup_Section { get; set; }
	}
}
