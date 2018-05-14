namespace Timetable.timetable.DB
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	using System.Xml.Linq;
	using Timetable.timetable.XML;

	public partial class tt_Class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Timetable.timetable.DB.tt_Class"/> class.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public tt_Class()
		{
			tt_ClassGroup = new HashSet<tt_ClassGroup>();
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

		public bool IsShared { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:Timetable.timetable.DB.tt_Class"/> is home.
		/// </summary>
		/// <value><c>true</c> if is home; otherwise, <c>false</c>.</value>
		public bool IsHome { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<tt_ClassGroup> tt_ClassGroup { get; set; }


		/// <summary>
		/// Returns the XElement representation of class
		/// </summary>
		/// <returns>The XElement.</returns>
		public XElement ToXElement()
		{
			return new XElement("grade", new XElement("ClassID", this.Id),
								new XElement("Classname", this.className));
		}

	}
}
