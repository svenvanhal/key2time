namespace Timetabling.DB
{

    /// <summary>
    /// Tt actitvity group.
    /// </summary>
    public partial class Tt_ActitvityGroup
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the activity reference identifier.
        /// </summary>
        /// <value>The activity reference identifier.</value>
        public int ActivityRefID { get; set; }

        /// <summary>
        /// Gets or sets the teacher identifier.
        /// </summary>
        /// <value>The teacher identifier.</value>
        public long teacherId { get; set; }

        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>The subject identifier.</value>
        public int subjectId { get; set; }

        /// <summary>
        /// Gets or sets the room identifier.
        /// </summary>
        /// <value>The room identifier.</value>
        public int roomId { get; set; }

        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        /// <value>The group identifier.</value>
        public int groupId { get; set; }

        /// <summary>
        /// Gets or sets the class identifier.
        /// </summary>
        /// <value>The class identifier.</value>
        public int classId { get; set; }

        /// <summary>
        /// Gets or sets the grade identifier.
        /// </summary>
        /// <value>The grade identifier.</value>
        public int gradeId { get; set; }
    }
}
