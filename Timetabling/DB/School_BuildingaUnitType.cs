namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    /// <summary>
    /// School buildinga unit type.
    /// </summary>
    public partial class School_BuildingaUnitType
    {
		/// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        /// <value>The name of the type.</value>
        [StringLength(150)]
        public string TypeName { get; set; }
        /// <summary>
        /// Gets or sets if the unit is active
        /// </summary>
        /// <value>The is active.</value>
        public bool? IsActive { get; set; }
    }
}
