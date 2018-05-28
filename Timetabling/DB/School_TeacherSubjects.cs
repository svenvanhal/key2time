namespace Timetabling.DB
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	/// <summary>
	/// School teacher subjects.
	/// </summary>
	public partial class School_TeacherSubjects
	{
		/// <summary>
		/// Gets or sets the teacher identifier.
		/// </summary>
		/// <value>The teacher identifier.</value>
		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long TeacherID { get; set; }

		/// <summary>
		/// Gets or sets the subject identifier.
		/// </summary>
		/// <value>The subject identifier.</value>
		[Key]
		[Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int SubjectID { get; set; }

		/// <summary>
		/// Gets or sets the grade identifier.
		/// </summary>
		/// <value>The grade identifier.</value>
		public int? gradeId { get; set; }

		/// <summary>
		/// Gets or sets HR_MasterData_Employees.
		/// </summary>
		/// <value>The hr master data employees.</value>
		public virtual HR_MasterData_Employees HR_MasterData_Employees { get; set; }

		/// <summary>
		/// Gets or sets Subject_MasterData_Subject.
		/// </summary>
		/// <value>The subject master data subject.</value>
		public virtual Subject_MasterData_Subject Subject_MasterData_Subject { get; set; }
	}
}
