using System;
using System.IO;
using Timetabling.Algorithms;
using Timetabling.DB;
using Timetabling.Objects;
using Timetabling.XML;

namespace Timetabling
{

    /// <summary>
    /// Program entrypoint.
    /// </summary>
    public static class Application
    {

        /// <summary>
        /// Temporary method used for development purposes.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {

            // Instantiate algorithm
            var fetPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Helper.Util.GetAppSetting("FetBinaryLocation"));
            var fetAlgo = new FetAlgorithm(fetPath);

            // Generate timetable
            fetAlgo.Execute("./testdata/fet/United-Kingdom/Hopwood/Hopwood.fet");

#if DEBUG
            // Keep console window open
            Console.Read();
#endif
        }

        public static void Main2()
        {
            var dB = new DataModel();
            var xmlCreator = XmlCreator.Instance;

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
