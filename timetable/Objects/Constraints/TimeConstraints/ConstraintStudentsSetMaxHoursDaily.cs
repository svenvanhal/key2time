using System;
using System.Xml.Linq;

namespace Timetable.timetable.Objects
{
	public class ConstraintStudentsSetMaxHoursDaily : AbstractConstraint
    {
		int maxHoursDaily;
		string gradeName;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetable.timetable.Objects.ConstraintStudentsSetMaxHoursDaily"/> class.
        /// </summary>
        /// <param name="_maxHoursDaily">Max hours daily.</param>
        /// <param name="_gradeName">Grade name.</param>
        public ConstraintStudentsSetMaxHoursDaily(int _maxHoursDaily, string _gradeName)
        {
			SetElement("ConstraintStudentsSetMaxHoursDaily");
            SetWeight(100); 
			maxHoursDaily = _maxHoursDaily;
			gradeName = _gradeName;
        }

        /// <summary>
        /// Returns the XELement 
        /// </summary>
        /// <returns>The xelement.</returns>
		public override XElement ToXelement()
		{
			constraint.Add(new XElement("Maximum_Hours_Daily", maxHoursDaily),
                           new XElement("Students", gradeName));
            return constraint;
		}
	}
}
