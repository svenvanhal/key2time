using System.Linq;
using System.Xml.Linq;
using Timetable.timetable.DB;
using Timetable.timetable.Objects;
using Timetable.timetable.XML;

namespace Timetable
{
    public class Application
    {
		public static void Main()
        {
            DataModel dB = new DataModel();
            XmlCreator xmlCreator = XmlCreator.Instance;

			TeachersList teachersList = new TeachersList(dB);
			teachersList.Create();

			SubjectsList subjectsList = new SubjectsList(dB);
			subjectsList.Create();
		

            xmlCreator.AddToRoot(new XElement("Institution"));
          
			xmlCreator.AddToRoot(teachersList.GetList());
			xmlCreator.AddToRoot(subjectsList.GetList());


			xmlCreator.Save();

        }

    }
}
