namespace Timetabling.DB
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	using System.Xml.Linq;

    /// <summary>
    /// Tt class.
    /// </summary>
	public partial class Tt_Class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Timetabling.DB.Tt_Class"/> class.
		/// </summary>
		public Tt_Class()
		{
			Tt_ClassGroup = new HashSet<Tt_ClassGroup>();
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the class.
		/// </summary>
		/// <value>The name of the class.</value>
		[Required]
		[StringLength(100)]
		public string className { get; set; }

		/// <summary>
		/// Gets or sets the short name.
		/// </summary>
		/// <value>The short name.</value>
		[Required]
		[StringLength(100)]
		public string shortName { get; set; }

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public int color { get; set; }

		/// <summary>
		/// Gets or sets the grade identifier.
		/// </summary>
		/// <value>The grade identifier.</value>
		public int gradeId { get; set; }

		/// <summary>
		/// Gets or sets the supervisor identifier.
		/// </summary>
		/// <value>The supervisor identifier.</value>
		public int supervisorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Timetabling.DB.Tt_Class"/> is shared.
        /// </summary>
        /// <value><c>true</c> if is shared; otherwise, <c>false</c>.</value>
		public bool IsShared { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:Timetabling.DB.Tt_Class"/> is home.
		/// </summary>
		/// <value><c>true</c> if is home; otherwise, <c>false</c>.</value>
		public bool IsHome { get; set; }

        /// <summary>
		/// Gets or sets Tt_ClassGroup
        /// </summary>
		/// <value>Tt_ClassGroup.</value>
		public virtual ICollection<Tt_ClassGroup> Tt_ClassGroup { get; set; }
      

	}
}
