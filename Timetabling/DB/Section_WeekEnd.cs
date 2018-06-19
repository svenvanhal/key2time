namespace Timetabling.DB
{
	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Section week end.
	/// </summary>
	public partial class Section_WeekEnd
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the Day.
		/// </summary>
		/// <value>The name of the Day.</value>
		[Required]
		[StringLength(50)]
		public string dayName { get; set; }

		/// <summary>
		/// Gets or sets the index of the Day.
		/// </summary>
		/// <value>The index of the Day.</value>
		public int dayIndex { get; set; }

		/// <summary>
		/// Gets or sets the section identifier.
		/// </summary>
		/// <value>The section identifier.</value>
		public int sectionId { get; set; }
	}
}
