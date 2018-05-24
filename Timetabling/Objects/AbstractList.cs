using System;
using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Objects
{
	/// <summary>
    /// Abstract list. Each list to implement this class
    /// </summary>
	public abstract class AbstractList
	{
		/// <summary>
        /// Gets or sets the datamodel
        /// </summary>
        /// <value>dB.</value>
		protected DataModel dB { get; set; }

        /// <summary>
        /// Gets or sets the XElement list.
        /// </summary>
        /// <value>The list.</value>
		protected XElement list { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.AbstractList"/> class.
        /// </summary>
        /// <param name="_dB">Datamodel.</param>
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

				list = new XElement(s);
		
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
