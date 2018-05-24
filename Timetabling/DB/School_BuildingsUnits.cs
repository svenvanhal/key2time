namespace Timetabling.DB
{
	using System.ComponentModel.DataAnnotations;
	/// <summary>
	/// School buildings units.
	/// </summary>
	public partial class School_BuildingsUnits
    {
		/// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the floor identifier.
        /// </summary>
        /// <value>The floor identifier.</value>
        public int? FloorID { get; set; }
        
		/// <summary>
        /// Gets or sets the name of the unit.
        /// </summary>
        /// <value>The name of the unit.</value>
        [StringLength(150)]
        public string UnitName { get; set; }
        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>The is active.</value>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>The type identifier.</value>
        public int? TypeID { get; set; }
    }
}
