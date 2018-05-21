using NUnit.Framework;
using System;
using System.Linq;
using System.Xml.Linq;
using Moq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects.Tests
{
	public class AcitivityTagListTest{
	XElement test;

    [SetUp]
    public void Init()
    {

        var mockDB = new Mock<DataModel>();
		var list = new ActivityTagList(mockDB.Object);
        test = list.GetList();

    }

    [Test]
    public void ElementNameTest()
    {
        Assert.AreEqual(test.Name.ToString(), "Activity_Tags_List");
    }

}
}
