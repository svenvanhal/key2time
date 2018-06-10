using Timetabling.Resources.Constraints;

namespace Implementation.Key2Soft
{

    /// <summary>
    /// Create Timetable resources from database.
    /// </summary>
    public static class DboConstraintFactory
    {

        public static TimeConstraint CreateBasicTimeConstraint()
        {
            return new BasicTimeConstraint();
        }

        public static SpaceConstraint CreateBasicSpaceConstraint()
        {
            return new BasicSpaceConstraint();
        }

    }
}
