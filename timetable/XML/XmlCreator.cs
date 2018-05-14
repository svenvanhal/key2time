using System;
using System.Xml;
using System.Xml.Linq;


namespace Timetable.timetable.XML
{
	public class XmlCreator
	{
		private XDocument xDocument;
		private static XmlCreator instance;
		private string pathName { get; set; }


		/// <summary>
		/// Initializes a new instance of the <see cref="T:Timetable.timetable.XML.XmlCreator"/> class.
		/// </summary>
		private XmlCreator()
		{
			DateTime localDate = DateTime.Now;
			pathName = "FET Files/Timetable:" + localDate + ".fet";
			xDocument = new XDocument();
			xDocument.Add(new XElement("fet"));

		}

		/// <summary>
		/// Gets the instance, if already created. If not, it creates an instance
		/// </summary>
		/// <value>The instance.</value>
		public static XmlCreator Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new XmlCreator();
				}
				return instance;
			}
		}


		public XDocument Writer()
		{ //Temp, Should me private
			return xDocument;
		}

		/// <summary>
		/// Saves the created XMl tree into a xml file with the given filePath
		/// </summary>
		public void Save()
		{
			xDocument.Save(pathName);
		}

		/// <summary>
		/// Adds an element to the root element
		/// 
		/// </summary>
		/// <param name="xElement">the new element to be added.</param>
		public void AddToRoot(XElement xElement)
		{
			xDocument.Element("fet").Add(xElement);
		}

	}
}
