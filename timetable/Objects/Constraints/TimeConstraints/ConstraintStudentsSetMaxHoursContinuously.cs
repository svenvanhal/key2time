using System;
using System.Xml.Linq;
using System.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class ConstraintStudentsSetMaxHoursContinuously : AbstractConstraint
	{
		public int numberOfHours { get; }
		public string gradeName { get; }
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetable.timetable.Objects.ConstraintStudentsSetMaxHoursContinuously"/> class.
        /// </summary>
        /// <param name="_numberOfHours">Number of hours.</param>
        /// <param name="_gradeName">Grade name.</param>
		public ConstraintStudentsSetMaxHoursContinuously(int _numberOfHours, string _gradeName)
		{

			SetElement("ConstraintStudentsSetMaxHoursContinuously");
			SetWeight(100);	
			numberOfHours = _numberOfHours;
			gradeName = _gradeName;
		}
        
        /// <summary>
        /// Returns the XElement
        /// </summary>
        /// <returns>The xelement.</returns>
		public override XElement ToXelement()
		{
			constraint.Add(new XElement("Maximum_Hours_Continuously", numberOfHours),
			               new XElement("Students", gradeName));
			return constraint;
		}
	}
}
