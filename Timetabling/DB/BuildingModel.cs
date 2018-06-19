using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// School buildings units.
    /// </summary>
    [Table("Buildings")]
    public class BuildingModel
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the is active.
		/// </summary>
		/// <value>The is active.</value>
		public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>The type identifier.</value>
        [Column("TypeID")]
        public int? TypeId { get; set; }
	}
}
