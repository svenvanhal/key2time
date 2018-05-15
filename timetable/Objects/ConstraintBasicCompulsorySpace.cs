using System;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class ConstraintBasicCompulsorySpace : AbstractConstraint
	{

		public ConstraintBasicCompulsorySpace(DataModel _dB) : base(_dB)
		{
			SetWeight(100);
			SetElement("ConstraintBasicCompulsorySpace");
		}

		public override void Create()
		{

		}

		public override XElement ToXelement()
		{
			constraint.Add(new XElement("Weight", weight));

			return constraint;
		}
	}
}
