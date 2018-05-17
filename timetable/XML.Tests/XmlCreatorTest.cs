using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Timetable.timetable.XML
{
    [TestFixture()]
    public class XmlCreatorTest
    {
        [Test()]
        public void SingletonTest()
        {
			XmlCreator xmlCreator = XmlCreator.Instance;
			XmlCreator xmlCreator2 = XmlCreator.Instance;

			Assert.AreEqual(xmlCreator, xmlCreator2);

        }

		[Test]
		public void CheckCorrectFetElement(){
			XmlCreator xmlCreator = XmlCreator.Instance;

			Assert.AreEqual(xmlCreator.Writer().Elements("fet").Count(), 1);
		}
		[Test]
        public void CheckCorrectAddElement()
        {
            XmlCreator xmlCreator = XmlCreator.Instance;
			xmlCreator.AddToRoot(new XElement("test", "value"));
			Assert.AreEqual(xmlCreator.Writer().Elements("fet").Elements("test").Count(), 1);
        }

		[Test]
        public void CheckCorrectAddElementArray()
        {
            XmlCreator xmlCreator = XmlCreator.Instance;
			List<XElement> list = new List<XElement>();
			list.Add(new XElement("test", "value"));
			list.Add(new XElement("test", "value2"));
            
			xmlCreator.AddToRoot(list.ToArray());
            Assert.AreEqual(xmlCreator.Writer().Elements("fet").Elements("test").Count(), 3);
        }

		[Test]
        public void CheckSave()
        {
            XmlCreator xmlCreator = XmlCreator.Instance;
           
			Assert.IsTrue(xmlCreator.Save());
        }

    }
}
