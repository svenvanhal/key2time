namespace Timetabling.DB
{
	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Tt class group.
	/// </summary>
	public partial class Tt_ClassGroup
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the class identifier.
		/// </summary>
		/// <value>The class identifier.</value>
		public int classId { get; set; }

		/// <summary>
		/// Gets or sets the name of the group.
		/// </summary>
		/// <value>The name of the group.</value>
		[Required]
		[StringLength(100)]
		public string groupName { get; set; }
	}
}
