namespace Timetabling.DB
{
	using System.ComponentModel.DataAnnotations.Schema;

	/// <summary>
	/// Subject class lesson.
	/// </summary>
	[Table("SubjectClassLesson")]
	public partial class SubjectClassLesson
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the subject identifier.
		/// </summary>
		/// <value>The subject identifier.</value>
		public int SubjectId { get; set; }

		/// <summary>
		/// Gets or sets the class identifier.
		/// </summary>
		/// <value>The class identifier.</value>
		public int classId { get; set; }

		/// <summary>
		/// Gets or sets the number of lesson per week.
		/// </summary>
		/// <value>The number of lesson per week.</value>
		public int numberOfLessonPerWeek { get; set; }
	}
}
