using System;
using System.Xml;
using System.Xml.Linq;


namespace Timetable.timetable.XML
{
    public class XmlCreator 
    {
        private XDocument xDocument;
        private static XmlCreator instance;
        private string pathName;

        private XmlCreator()
        {
            DateTime localDate = DateTime.Now;
            pathName = "Timetable:"+localDate +".xml";
            xDocument = new XDocument();
         
        }

        public static XmlCreator Instance {
            get { 
                if (instance == null )
                {
                    instance = new XmlCreator();
                }
                return instance;
                }
        }


        public XDocument Writer(){
            return xDocument;
        }

    }
}
