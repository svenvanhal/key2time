using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Time off table.
    /// </summary>
    [Table("Tt_TimeOff")]
    public class TimeOffModel
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Day.
        /// </summary>
        /// <value>The Day.</value>
        [Column("Day")]
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets the index of the lesson.
        /// </summary>
        /// <value>The index of the lesson.</value>
        [Column("LessonIndex")]
        public int LessonIndex { get; set; }

		/// <summary>
		/// Gets or sets the type of the item.
		/// </summary>
		/// <value>The type of the item.</value>
		public int ItemType { get; set; }

		/// <summary>
		/// Gets or sets the item identifier.
		/// </summary>
		/// <value>The item identifier.</value>
		public int ItemId { get; set; }
	}
}
