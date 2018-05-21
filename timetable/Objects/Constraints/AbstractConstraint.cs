using System;
using System.Xml.Linq;
using Timetable.timetable.DB;
using System.Linq;

namespace Timetable.timetable.Objects
{
	public abstract class AbstractConstraint
	{
		public int weight { get; set; } = 100;
		public XElement constraint { get; set; }

		/// <summary>
		/// Sets the weight.
		/// </summary>
		/// <param name="w">The width.</param>
		public void SetWeight(int w)
		{
			weight = w;
			constraint.Add(new XElement("Weight_Percentage", weight));

		}
		/// <summary>
		/// Sets the element.
		/// </summary>
		/// <param name="s">S.</param>
		public void SetElement(string s)
		{
			constraint = new XElement(s);
		}
		/// <summary>
		/// Returns the XElement
		/// </summary>
		/// <returns>The xelement.</returns>
		public abstract XElement ToXelement();
		public abstract XElement[] Create(DataModel dB);
	}
}