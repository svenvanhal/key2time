using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Timetabling.XML;

namespace Timetabling.Tests.XML
{
    [TestFixture()]
    internal class XmlCreatorTest
    {

		[Test]
		public void CheckCorrectFetElement(){
			var xmlCreator = new XmlCreator();

			Assert.AreEqual(xmlCreator.Writer().Elements("fet").Count(), 1);
		}
		[Test]
        public void CheckCorrectAddElement()
        {
            var xmlCreator = new XmlCreator();
            xmlCreator.AddToRoot(new XElement("test", "value"));
			Assert.AreEqual(xmlCreator.Writer().Elements("fet").Elements("test").Count(), 1);
        }

		[Test]
        public void CheckCorrectAddElementArray()
        {
            var xmlCreator = new XmlCreator();
            var list = new List<XElement>
            {
                new XElement("test", "value"),
                new XElement("test", "value2")
            };

            xmlCreator.AddToRoot(list.ToArray());
            Assert.AreEqual(xmlCreator.Writer().Elements("fet").Elements("test").Count(), 2);
        }
    }
}
