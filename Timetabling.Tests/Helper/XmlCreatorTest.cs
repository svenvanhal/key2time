using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Timetabling.Helper;

namespace Timetabling.Tests.Helper
{
    [TestFixture]
    internal class XmlCreatorTest
    {

		[Test]
		public void CheckCorrectFetElement(){
			var xmlCreator = new XmlCreator();
			Assert.AreEqual(1, xmlCreator.Document.Elements("fet").Count());
		}

		[Test]
        public void CheckCorrectAddElement()
        {
            var xmlCreator = new XmlCreator();
            xmlCreator.AddToRoot(new XElement("test", "value"));
			Assert.AreEqual(1, xmlCreator.Document.Elements("fet").Elements("test").Count());
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
            Assert.AreEqual(2, xmlCreator.Document.Elements("fet").Elements("test").Count());
        }
    }
}
