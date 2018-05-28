namespace Timetabling.DB
{
	/// <summary>
	/// Tt grade lesson.
	/// </summary>
	public partial class Tt_GradeLesson
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the number of lessons.
		/// </summary>
		/// <value>The number of lessons.</value>
		public int numberOfLessons { get; set; }

		/// <summary>
		/// Gets or sets the grade identifier.
		/// </summary>
		/// <value>The grade identifier.</value>
		public int gradeId { get; set; }
	}
}
