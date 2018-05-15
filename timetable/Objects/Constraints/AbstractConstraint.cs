using System;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public abstract class AbstractConstraint
	{
		protected int weight { get; set; }
		protected XElement constraint { get; set; }
              

		public void SetWeight(int w)
		{
			weight = w;
			if (constraint != null)
			{
				var el = constraint.Element("Weight_Percentage");
				if (el == null)
				{

					constraint.Add(new XElement("Weight_Percentage", weight));

				}
				else{
					el.SetValue(w);
				}
			}
			else
			{
				Console.WriteLine("[Error] no element set");
			}
		}

		public void SetElement(string s)
		{
			constraint = new XElement(s);
		}

		public abstract XElement ToXelement();

	}
}