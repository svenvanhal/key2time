namespace Timetabling.DB
{

	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Lookup month class.
	/// </summary>
	public partial class Lookup_Month
	{
		/// <summary>
		/// Gets or sets the month identifier.
		/// </summary>
		/// <value>The month identifier.</value>
		[Key]
		public int MonthID { get; set; }
		/// <summary>
		/// Gets or sets the name of the month.
		/// </summary>
		/// <value>The name of the month.</value>
		[Required]
		[StringLength(50)]
		public string MonthName { get; set; }
		/// <summary>
		/// Gets or sets of the month is active
		/// </summary>
		/// <value>The is active.</value>
		public bool? IsActive { get; set; }
		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>The code.</value>
		public int? Code { get; set; }
	}
}
