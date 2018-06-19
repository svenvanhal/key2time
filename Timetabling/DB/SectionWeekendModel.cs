using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Section week end.
    /// </summary>
    [Table("Section_WeekEnd")]
    public class SectionWeekendModel
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
        [Column("dayName")]
		public string DayName { get; set; }

        /// <summary>
        /// Gets or sets the index of the Day.
        /// </summary>
        /// <value>The index of the Day.</value>
        [Column("dayIndex")]
        public int DayIndex { get; set; }

        /// <summary>
        /// Gets or sets the section identifier.
        /// </summary>
        /// <value>The section identifier.</value>
        [Column("sectionId")]
        public int SectionId { get; set; }
	}
}
