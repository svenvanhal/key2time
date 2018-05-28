namespace Timetabling.DB
{

	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Lookup year.
	/// </summary>
	public partial class Lookup_Year
	{
		/// <summary>
		/// Gets or sets the year identifier.
		/// </summary>
		/// <value>The year identifier.</value>
		[Key]
		public int YearID { get; set; }

		/// <summary>
		/// Gets or sets the name of the year.
		/// </summary>
		/// <value>The name of the year.</value>
		[Required]
		[StringLength(50)]
		public string YearName { get; set; }
	}
}
