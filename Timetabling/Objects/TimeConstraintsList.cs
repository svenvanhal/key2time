using System.Collections.Generic;
using Timetabling.DB;
using Timetabling.Objects.Constraints;
using Timetabling.Objects.Constraints.TimeConstraints;

namespace Timetabling.Objects
{
    /// <summary>
    /// Time constraints list.
    /// </summary>
    public class TimeConstraintsList : AbstractList
    {

        /// <summary>
        /// Constraints in this list.
        /// </summary>
        protected readonly List<AbstractConstraint> Constraints = new List<AbstractConstraint>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.TimeConstraintsList"/> class.
        /// </summary>
        /// <param name="_dB">D b.</param>
        public TimeConstraintsList(DataModel _dB) : base(_dB) => SetListElement("Time_Constraints_List");

        /// <summary>
        /// Create the XElements of the constraints.
        /// </summary>
        public override void Create()
        {
            CreateConstraints();
            Constraints.ForEach(item => List.Add(item.Create(dB)));
        }

        /// <summary>
        /// Creates the constraints.
        /// </summary>
        private void CreateConstraints()
        {
            List.Add(new ConstraintBasicCompulsoryTime().ToXelement());
            Constraints.Add(new ConstraintTeacherNotAvailableTimes());
            Constraints.Add(new ConstraintStudentsSetNotAvailableTimes());
            Constraints.Add(new ConstraintMinDaysBetweenActivities());
            Constraints.Add(new ConstraintPeriodSection());
        }

    }
}
