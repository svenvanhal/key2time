using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Subject subject grade.
    /// </summary>
    [Table("Subject_SubjectGrade")]
    public class SubjectGradeModel
	{
		/// <summary>
		/// Gets or sets the subject grade identifier.
		/// </summary>
		/// <value>The subject grade identifier.</value>
		[Column("SubjectGradeID"), Key]
		public int SubjectGradeId { get; set; }

        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>The subject identifier.</value>
        [Column("SubjectId")]
        public int? SubjectId { get; set; }

        /// <summary>
        /// Gets or sets the grade identifier.
        /// </summary>
        /// <value>The grade identifier.</value>
        [Column("GradeID")]
        public int? GradeId { get; set; }

        /// <summary>
        /// Gets or sets the number of lessons per week.
        /// </summary>
        /// <value>The number of lessons per week.</value>
        [Column("NumberOfLlessonsPerWeek")]
        public int NumberOfLessonsPerWeek { get; set; }

        /// <summary>
        /// Gets or sets the number of lessons per day.
        /// </summary>
        /// <value>The number of lessons per day.</value>
        [Column("NumberOfLlessonsPerDay")]
        public int NumberOfLessonsPerDay { get; set; }

        /// <summary>
        /// Gets or sets the preffered room type id
        /// </summary>
        /// <value>The preffered room type id.</value>
        [Column("BuildingUnitTypeID")]
        public int? BuildingUnitTypeId { get; set; }

        /// <summary>
        /// Gets or sets the collection identifier.
        /// </summary>
        /// <value>The collection identifier.</value>
        [Column("CollectionID")]
        public int? CollectionId { get; set; }

	}
}
