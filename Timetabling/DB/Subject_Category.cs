namespace Timetabling.DB
{
	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Subject category.
	/// </summary>
	public partial class Subject_Category
	{
		/// <summary>
		/// Gets or sets the subject category identifier.
		/// </summary>
		/// <value>The subject category identifier.</value>
		[Key]
		public int SubjectCategoryID { get; set; }

		/// <summary>
		/// Gets or sets the name of the subject category.
		/// </summary>
		/// <value>The name of the subject category.</value>
		[StringLength(50)]
		public string SubjectCategoryName { get; set; }

		/// <summary>
		/// Gets or sets the is active.
		/// </summary>
		/// <value>The is active.</value>
		public bool? IsActive { get; set; }
	}
}
