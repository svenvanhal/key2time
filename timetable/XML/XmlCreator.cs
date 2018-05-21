using System;
using System.IO;
using System.Xml.Linq;

namespace Timetable.timetable.XML
{
    public class XmlCreator
    {

        private XDocument xDocument { get; }
        private static XmlCreator instance;
        private string pathName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetable.timetable.XML.XmlCreator"/> class.
        /// </summary>
        private XmlCreator()
        {
            var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            pathName = Path.Combine("FET Files", $"Timetable_{timestamp}.fet");
            xDocument = new XDocument();
            xDocument.Add(new XElement("fet"));
        }

        /// <summary>
        /// Gets the instance, if already created. If not, it creates an instance
        /// </summary>
        /// <value>The instance.</value>
        public static XmlCreator Instance => instance ?? new XmlCreator();

        /// <summary>
        /// Retrieves the xDocument associated with the XmlCreator.
        /// </summary>
        /// <returns>XDocument</returns>
        public XDocument Writer() => xDocument;

        /// <summary>
        /// Saves the created XMl tree into a xml file with the given filePath
        /// </summary>
        public void Save() => xDocument.Save(pathName);

        /// <summary>
        /// Adds an element to the root element
        /// </summary>
        /// <param name="xElement">the new element to be added.</param>
        public void AddToRoot(XElement xElement)
        {
            xDocument.Element("fet").Add(xElement);
        }

        /// <summary>
        /// Adds an arry of elements to the root element
        /// </summary>
        /// <param name="xElements">the new element to be added.</param>
        public void AddToRoot(Array xElements)
        {
            xDocument.Element("fet").Add(xElements);
        }

    }
}
