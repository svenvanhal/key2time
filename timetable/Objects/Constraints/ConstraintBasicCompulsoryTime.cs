﻿using System;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class ConstraintBasicCompulsoryTime : AbstractConstraint
	{

		public ConstraintBasicCompulsoryTime() 
		{
			SetElement("ConstraintBasicCompulsoryTime");
			SetWeight(100);

		}
		public override XElement ToXelement()
		{
			return constraint;
		}
	}
}
