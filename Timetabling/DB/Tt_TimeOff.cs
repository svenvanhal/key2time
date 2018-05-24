namespace Timetabling.DB
{
	using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Tt time off.
    /// </summary>
	public partial class Tt_TimeOff
    {
		/// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        public int day { get; set; }

        /// <summary>
        /// Gets or sets the index of the lesson.
        /// </summary>
        /// <value>The index of the lesson.</value>
        public int lessonIndex { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int status { get; set; }

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
