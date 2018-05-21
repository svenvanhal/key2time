using System;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Objects
{
	public class ConstraintBasicCompulsoryTime : AbstractConstraint
	{

		public ConstraintBasicCompulsoryTime() 
		{
			SetElement("ConstraintBasicCompulsoryTime");
			SetWeight(100);

		}

		public override XElement[] Create(DataModel dB)
		{
			return new XElement[0];
		}

		public override XElement ToXelement()
		{
			return constraint;
		}
	}
}
