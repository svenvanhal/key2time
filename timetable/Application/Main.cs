﻿using System.Linq;
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

			DaysList daysList = new DaysList(dB);
            daysList.Create();

			TeachersList teachersList = new TeachersList(dB);
			teachersList.Create();

			SubjectsList subjectsList = new SubjectsList(dB);
			subjectsList.Create();

			ActivitiesList activitiesList = new ActivitiesList(dB);
			activitiesList.Create();
           
            xmlCreator.AddToRoot(new XElement("Institution"));

			xmlCreator.AddToRoot(daysList.GetList());
			xmlCreator.AddToRoot(teachersList.GetList());
			xmlCreator.AddToRoot(subjectsList.GetList());
			xmlCreator.AddToRoot(activitiesList.GetList());


			xmlCreator.Save();

        }

    }
}
