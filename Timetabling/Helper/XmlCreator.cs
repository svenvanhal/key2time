using System;
using System.IO;
using System.Xml.Linq;

namespace Timetabling.Helper
{

    /// <summary>
    /// Creates an XDocument.
    /// </summary>
	public class XmlCreator
	{

        /// <summary>
        /// Document the XmlCreator is operating on.
        /// </summary>
	    public XDocument Document { get; }

        /// <summary>
        /// Root element of the document.
        /// </summary>
		public XElement Root { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Timetabling.Helper.XmlCreator"/> class.
		/// </summary>
		public XmlCreator(string fetVersion = null)
		{
		    Root = new XElement("fet");

            // Set FET version if supplied
		    if (fetVersion != null) Root.SetAttributeValue("version", fetVersion);

            Document = new XDocument();
			Document.Add(Root);
		}

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
