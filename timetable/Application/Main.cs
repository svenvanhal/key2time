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
            var dB = new DataModel();
            XmlCreator xmlCreator = XmlCreator.Instance;

			var daysList = new DaysList(dB);
            daysList.Create();
            
			var hoursList = new HoursList(dB);
			hoursList.Create();

			var teachersList = new TeachersList(dB);
			teachersList.Create();
            
			var subjectsList = new SubjectsList(dB);
			subjectsList.Create();

			var activitiesList = new ActivitiesList(dB);
			activitiesList.Create();

			var yearsList = new YearsList(dB);
			yearsList.Create();
            
			var timeConstraintsList = new TimeConstraintsList(dB);
			timeConstraintsList.Create();

			var spaceConstraintsList = new SpaceConstraintsList(dB);
			spaceConstraintsList.Create();
            
			xmlCreator.AddToRoot(daysList.GetList());
			xmlCreator.AddToRoot(hoursList.GetList());
			xmlCreator.AddToRoot(teachersList.GetList());
			xmlCreator.AddToRoot(yearsList.GetList());
			xmlCreator.AddToRoot(subjectsList.GetList());
			xmlCreator.AddToRoot(activitiesList.GetList());
			xmlCreator.AddToRoot(timeConstraintsList.GetList());
			xmlCreator.AddToRoot(spaceConstraintsList.GetList());


			xmlCreator.Save();

        }

    }
}
