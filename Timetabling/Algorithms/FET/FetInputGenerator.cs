using Timetabling.DB;
using Timetabling.Objects;
using Timetabling.XML;

namespace Timetabling.Algorithms.FET
{
    public class FetInputGenerator
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Generates a .FET file for use in the algorithm.
        /// </summary>
        /// <param name="dataModel">Datamodel to construct objects from.</param>
        /// <param name="outputDir">Output directory for the FET file.</param>
        /// <returns>Filename of the generated .FET file.</returns>
        public string GenerateFetFile(DataModel dataModel, string outputDir)
        {

            Logger.Debug("1");

            var xmlCreator = new XmlCreator();

            Logger.Debug("2");
            var daysList = new DaysList(dataModel);
            daysList.Create();

            Logger.Debug("3");
            var hoursList = new HoursList(dataModel);
            hoursList.Create();

            Logger.Debug("4");
            var teachersList = new TeachersList(dataModel);
            teachersList.Create();

            Logger.Debug("5");
            var subjectsList = new SubjectsList(dataModel);
            subjectsList.Create();

            Logger.Debug("6");
            var activitiesList = new ActivitiesList(dataModel);
            activitiesList.Create();

            Logger.Debug("7");
            var yearsList = new YearsList(dataModel);
            yearsList.Create();

            Logger.Debug("8");
            var timeConstraintsList = new TimeConstraintsList(dataModel);
            timeConstraintsList.Create();

            Logger.Debug("9");
            var spaceConstraintsList = new SpaceConstraintsList(dataModel);
            spaceConstraintsList.Create();

            Logger.Debug("10");
            xmlCreator.AddToRoot(daysList.GetList());
            xmlCreator.AddToRoot(hoursList.GetList());
            xmlCreator.AddToRoot(teachersList.GetList());
            xmlCreator.AddToRoot(yearsList.GetList());
            xmlCreator.AddToRoot(subjectsList.GetList());
            xmlCreator.AddToRoot(activitiesList.GetList());
            xmlCreator.AddToRoot(timeConstraintsList.GetList());
            xmlCreator.AddToRoot(spaceConstraintsList.GetList());

            Logger.Debug("11");
            var filepath = xmlCreator.Save(outputDir);

            return filepath;
        }

    }
}
