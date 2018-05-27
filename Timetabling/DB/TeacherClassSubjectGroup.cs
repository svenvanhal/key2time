namespace Timetabling.DB
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	/// <summary>
	/// Teacher class subject group.
	/// </summary>
	[Table("TeacherClassSubjectGroup")]
	public partial class TeacherClassSubjectGroup
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the teacher class subject identifier.
		/// </summary>
		/// <value>The teacher class subject identifier.</value>
		public int teacherClassSubjectId { get; set; }

		/// <summary>
		/// Gets or sets the group identifier.
		/// </summary>
		/// <value>The group identifier.</value>
		[StringLength(10)]
		public string GroupId { get; set; }
	}
}
