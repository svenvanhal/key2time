namespace Timetabling.Resources.Constraints
{

    /// <summary>
    /// Constraint on space or time resources.
    /// </summary>
    public abstract class AbstractConstraint
    {

        /// <summary>
        /// Weight / importance of the constraint (0 - 100). 100 means the constraint cannot be violated in the generated timetable.
        /// </summary>
        public int Weight;

    }

    public abstract class TimeConstraint : AbstractConstraint {}
    public abstract class SpaceConstraint : AbstractConstraint {}

}
