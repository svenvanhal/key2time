namespace Timetabling.DB
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;


	/// <summary>
	/// School lookup section.
	/// </summary>
	public partial class School_Lookup_Section
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Timetabling.DB.School_Lookup_Section"/> class.
		/// </summary>
		public School_Lookup_Section()
		{
			School_Lookup_Stage = new HashSet<School_Lookup_Stage>();
		}

		/// <summary>
		/// Gets or sets the section identifier.
		/// </summary>
		/// <value>The section identifier.</value>
		[Key]
		public int SectionID { get; set; }

		/// <summary>
		/// Gets or sets the name of the section.
		/// </summary>
		/// <value>The name of the section.</value>
		[StringLength(250)]
		public string SectionName { get; set; }

		/// <summary>
		/// Gets or sets the short name.
		/// </summary>
		/// <value>The short name.</value>
		[StringLength(20)]
		public string ShortName { get; set; }

		/// <summary>
		/// Gets or sets the is active.
		/// </summary>
		/// <value>The is active.</value>
		public bool? IsActive { get; set; }

		/// <summary>
		/// Gets or sets the max lessons per day.
		/// </summary>
		/// <value>The max lessons per day.</value>
		public int? maxLessonsPerDay { get; set; }

		/// <summary>
		/// Gets or sets the days per week.
		/// </summary>
		/// <value>The days per week.</value>
		public int? daysPerWeek { get; set; }

		/// <summary>
		/// Gets or sets the School_Lookup_Stage.
		/// </summary>
		/// <value>The School_Lookup_Stage.</value>
		public virtual ICollection<School_Lookup_Stage> School_Lookup_Stage { get; set; }
	}
}
