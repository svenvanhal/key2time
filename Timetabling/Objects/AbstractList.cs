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
        public XElement List { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.AbstractList"/> class.
        /// </summary>
        /// <param name="dataModel">Datamodel.</param>
        protected AbstractList(DataModel dataModel) => dB = dataModel;

        /// <summary>
        /// Create the elements in the list
        /// </summary>
        public abstract XElement Create();

        /// <summary>
        /// Sets the main list element name.
        /// </summary>
        /// <param name="s">S.</param>
        public void SetListElement(string s) => List = new XElement(s);

    }
}
