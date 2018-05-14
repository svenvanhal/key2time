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

        /// <summary>
        /// Create the elements in the list
        /// </summary>
		public abstract void Create();

        /// <summary>
        /// Sets the main list element name.
        /// </summary>
        /// <param name="s">S.</param>
		public void SetListElement(String s){
			if (list == null)
			{
				list = new XElement(s);
			}
			else
				Console.Write("[Error] List is already Set"); //Temp Excepetion
           
			}
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns>The list.</returns>
		public XElement GetList()
		{
			return list;
		}
	}
}
