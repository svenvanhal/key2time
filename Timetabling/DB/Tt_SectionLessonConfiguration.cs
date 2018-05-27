namespace Timetabling.DB
{
	/// <summary>
	/// Tt_SectionLessonConfiguration.
	/// </summary>
	public partial class Tt_SectionLessonConfiguration
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the lesson per day.
		/// </summary>
		/// <value>The lesson per day.</value>
		public int lessonPerDay { get; set; }

		/// <summary>
		/// Gets or sets the section identifier.
		/// </summary>
		/// <value>The section identifier.</value>
		public int sectionId { get; set; }
	}
}
