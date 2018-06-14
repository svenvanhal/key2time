namespace Timetabling.DB
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// Subject master data subject.
    /// </summary>
    public partial class Subject_MasterData_Subject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.DB.Subject_MasterData_Subject"/> class.
        /// </summary>
        public Subject_MasterData_Subject()
        {
            Subject_SubjectGrade = new HashSet<Subject_SubjectGrade>();
        }

        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>The subject identifier.</value>
        [Key]
        public int SubjectID { get; set; }

        /// <summary>
        /// Gets or sets the name of the subject.
        /// </summary>
        /// <value>The name of the subject.</value>
        [StringLength(100)]
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        /// <value>The short name.</value>
        [StringLength(10)]
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the subject category identifier.
        /// </summary>
        /// <value>The subject category identifier.</value>
        public int? SubjectCategoryID { get; set; }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>The is active.</value>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>The section.</value>
        public int? Section { get; set; }

        /// <summary>
        /// Gets or sets the is shared.
        /// </summary>
        /// <value>The is shared.</value>
        public bool? IsShared { get; set; }

        /// <summary>
        /// Gets or sets the is multi per day.
        /// </summary>
        /// <value>The is multi per day.</value>
        public bool? IsMultiPerDay { get; set; }

        /// <summary>
        /// Gets or sets the collection identifier.
        /// </summary>
        /// <value>The collection identifier.</value>
        public int? CollectionID { get; set; }

        /// <summary>
        /// Gets or sets Subject_SubjectGrade.
        /// </summary>
        /// <value>Subject_SubjectGrade.</value>
        public virtual ICollection<Subject_SubjectGrade> Subject_SubjectGrade { get; set; }
    }
}
