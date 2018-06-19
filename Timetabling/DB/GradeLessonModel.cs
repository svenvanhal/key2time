using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{
	/// <summary>
	/// Grade / lesson relationship table.
	/// </summary>
	public class GradeLessonModel
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
        [Column("numberOfLessons")]
        public int NumberOfLessons { get; set; }

        /// <summary>
        /// Gets or sets the grade identifier.
        /// </summary>
        /// <value>The grade identifier.</value>
        [Column("GradeId")]
        public int GradeId { get; set; }
	}
}
