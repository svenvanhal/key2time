using System;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public abstract class AbstractConstraint
	{
		protected int weight { get; set; }
		protected XElement constraint { get; set; }
		protected DataModel dB { get; set; }


		public AbstractConstraint(DataModel _dB)
		{
			dB = _dB;
		}

		public void SetWeight(int w)
		{
			weight = w;
		}

		public void SetElement(string s)
		{
			constraint = new XElement(s);
		}

		public abstract XElement ToXelement();
		public abstract void Create();

	}
}