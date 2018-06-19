namespace Timetabling.DB
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	/// <summary>
	/// School lookup grade.
	/// </summary>
	public partial class School_Lookup_Grade
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Timetabling.DB.School_Lookup_Grade"/> class.
		/// </summary>
		public School_Lookup_Grade()
		{
			School_Lookup_Class = new HashSet<School_Lookup_Class>();
			Subject_SubjectGrade = new HashSet<SubjectGradeTable>();
			Subject_SubjectGrade1 = new HashSet<SubjectGradeTable>();
		}

		/// <summary>
		/// Gets or sets the grade identifier.
		/// </summary>
		/// <value>The grade identifier.</value>
		[Key]
		public int GradeID { get; set; }

		/// <summary>
		/// Gets or sets the name of the grade.
		/// </summary>
		/// <value>The name of the grade.</value>
		[Required]
		[StringLength(50)]
		public string GradeName { get; set; }

		/// <summary>
		/// Gets or sets the stage identifier.
		/// </summary>
		/// <value>The stage identifier.</value>
		public int? StageID { get; set; }

		/// <summary>
		/// Gets or sets the is active.
		/// </summary>
		/// <value>The is active.</value>
		public bool? IsActive { get; set; }

		/// <summary>
		/// Gets or sets the School_Lookup_Class.
		/// </summary>
		/// <value>The School_Lookup_Class.</value>
		public virtual ICollection<School_Lookup_Class> School_Lookup_Class { get; set; }

		/// <summary>
		/// Gets or sets the School_Lookup_Stage
		/// </summary>
		/// <value>The School_Lookup_Stage.</value>
		public virtual School_Lookup_Stage School_Lookup_Stage { get; set; }

		/// <summary>
		/// Gets or sets the SubjectGradeTable.
		/// </summary>
		/// <value>The SubjectGradeTable.</value>
		public virtual ICollection<SubjectGradeTable> Subject_SubjectGrade { get; set; }

		/// <summary>
		/// Gets or sets SubjectGradeTable.
		/// </summary>
		/// <value>The SubjectGradeTable.</value>
		public virtual ICollection<SubjectGradeTable> Subject_SubjectGrade1 { get; set; }
	}
}
