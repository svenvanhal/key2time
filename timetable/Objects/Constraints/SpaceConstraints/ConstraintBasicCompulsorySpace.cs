using System;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class ConstraintBasicCompulsorySpace : AbstractConstraint
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetable.timetable.Objects.ConstraintBasicCompulsorySpace"/> class.
        /// </summary>
		public ConstraintBasicCompulsorySpace() 
		{
			SetElement("ConstraintBasicCompulsorySpace");
			SetWeight(100);
		}

  
        /// <summary>
        /// Returns the XElement
        /// </summary>
        /// <returns>The xelement.</returns>
		public override XElement ToXelement()
		{

			return constraint;
		}
	}
}
