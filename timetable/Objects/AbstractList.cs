using System;
using System.Linq;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public abstract class AbstractList
	{
		protected DataModel dB { get; set; }
		protected XElement list { get; set; }

		public AbstractList(DataModel _dB)
		{
			dB = _dB;
		}

		public abstract void Create();

		public void SetListElement(String s){
			if (list == null)
			{
				list = new XElement(s);
			}
			else
				Console.Write("[Error] List is already Set"); //Temp Excepetion
           
			}

		public XElement GetList()
		{
			return list;
		}
	}
}
