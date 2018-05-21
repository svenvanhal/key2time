using System;
using System.IO;
using System.Xml.Linq;

namespace Timetabling.XML
{
    public class XmlCreator
    {

        private XDocument Document { get; }
        public XElement Root { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.XML.XmlCreator"/> class.
        /// </summary>
        public XmlCreator()
        {
            Document = new XDocument();
            Document.Add(Root = new XElement("fet"));
        }

        /// <summary>
        /// Retrieves the xDocument associated with the XmlCreator.
        /// </summary>
        /// <returns>XDocument</returns>
        public XDocument Writer() => Document;

        /// <summary>
        /// Saves the created XML tree into a xml file with the given filePath
        /// </summary>
        /// <param name="outputDir">The directory in which to save the output</param>
        /// <returns>The path to the resulting FET file.</returns>
        public string Save(string outputDir)
        {

            var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var fullPath = Path.Combine(outputDir, $"tt_resources_{timestamp}.fet");

            Document.Save(fullPath);

            return fullPath;
        }

        /// <summary>
        /// Adds an element to the root element
        /// </summary>
        /// <param name="xElement">the new element to be added.</param>
        public void AddToRoot(XElement xElement)
        {
            Root.Add(xElement);
        }

        /// <summary>
        /// Adds an arry of elements to the root element
        /// </summary>
        /// <param name="xElements">the new element to be added.</param>
        public void AddToRoot(XElement[] xElements)
        {
            Root.Add(xElements);
        }

    }
}
