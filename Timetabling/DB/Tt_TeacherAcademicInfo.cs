namespace Timetabling.DB
{

	/// <summary>
	/// Tt_TeacherAcademicInfo
	/// </summary>
	public partial class Tt_TeacherAcademicInfo
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the lessons per week.
		/// </summary>
		/// <value>The lessons per week.</value>
		public int lessonsPerWeek { get; set; }

		/// <summary>
		/// Gets or sets the teacher identifier.
		/// </summary>
		/// <value>The teacher identifier.</value>
		public long teacherId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:Timetabling.DB.Tt_TeacherAcademicInfo"/> is shared.
		/// </summary>
		/// <value><c>true</c> if is shared; otherwise, <c>false</c>.</value>
		public bool IsShared { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:Timetabling.DB.Tt_TeacherAcademicInfo"/> is home
		/// class teacher.
		/// </summary>
		/// <value><c>true</c> if is home class teacher; otherwise, <c>false</c>.</value>
		public bool IsHomeClassTeacher { get; set; }

		/// <summary>
		/// Gets or sets the substitutionlessons per week.
		/// </summary>
		/// <value>The substitutionlessons per week.</value>
		public int substitutionlessonsPerWeek { get; set; }

		/// <summary>
		/// Gets or sets the Tt_TeacherAcademicInfo1
		/// </summary>
		/// <value>Tt_TeacherAcademicInfo.</value>
		public virtual Tt_TeacherAcademicInfo Tt_TeacherAcademicInfo1 { get; set; }

		/// <summary>
		/// Gets or sets Tt_TeacherAcademicInfo2
		/// </summary>
		/// <value>Tt_TeacherAcademicInfo.</value>
		public virtual Tt_TeacherAcademicInfo Tt_TeacherAcademicInfo2 { get; set; }
	}
}
