using System;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class ConstraintBasicCompulsorySpace : AbstractConstraint
	{

		public ConstraintBasicCompulsorySpace() 
		{
			SetElement("ConstraintBasicCompulsorySpace");
			SetWeight(100);
		}

  

		public override XElement ToXelement()
		{

			return constraint;
		}
	}
}
