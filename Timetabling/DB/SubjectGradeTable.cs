﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Subject subject grade.
    /// </summary>
    [Table("Subject_SubjectGrade")]
    public class SubjectGradeTable
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
		public int? SubjectID { get; set; }

		/// <summary>
		/// Gets or sets the grade identifier.
		/// </summary>
		/// <value>The grade identifier.</value>
		public int? GradeID { get; set; }

		/// <summary>
		/// Gets or sets the number of llessons per week.
		/// </summary>
		/// <value>The number of llessons per week.</value>
		public int NumberOfLlessonsPerWeek { get; set; }

		/// <summary>
		/// Gets or sets the number of llessons per Day.
		/// </summary>
		/// <value>The number of llessons per Day.</value>
		public int NumberOfLlessonsPerDay { get; set; }

        /// <summary>
        /// Gets or sets the preffered room type id
        /// </summary>
        /// <value>The preffered room type id.</value>
        public int? BuildingUnitTypeID { get; set; }

        /// <summary>
        /// Gets or sets the collection identifier.
        /// </summary>
        /// <value>The collection identifier.</value>
        public int? CollectionID { get; set; }

		/// <summary>
		/// Gets or sets LookupGradeModel.
		/// </summary>
		/// <value>LookupGradeModel.</value>
		public virtual LookupGradeModel LookupGradeModel { get; set; }

		/// <summary>
		/// Gets or sets LookupGrade1.
		/// </summary>
		/// <value>The school lookup grade1.</value>
		public virtual LookupGradeModel LookupGrade1 { get; set; }

		/// <summary>
		/// Gets or sets SubjectModel.
		/// </summary>
		/// <value>SubjectModel.</value>
		public virtual SubjectModel Subject_MasterData_Subject { get; set; }
	}
}
