using Timetabling.DB;
using Timetabling.Objects;
using Timetabling.XML;

namespace Implementation
{

    internal static class FetInputGenerator
    {

        // The FET input format version
        private const string FetVersion = "5.35.6";

        /// <summary>
        /// Generates a .FET file for use in the algorithm.
        /// </summary>
        /// <param name="dataModel">Datamodel to construct objects from.</param>
        /// <param name="outputDir">Output directory for the FET file.</param>
        /// <returns>Filename of the generated .FET file.</returns>
        public static string GenerateFetFile(DataModel dataModel, string outputDir)
        {

            var xmlCreator = new XmlCreator(FetVersion);

            var daysList = new DaysList(dataModel);
            daysList.Create();

            var hoursList = new HoursList(dataModel);
            hoursList.Create();

            var teachersList = new TeachersList(dataModel);
            teachersList.Create();

            var subjectsList = new SubjectsList(dataModel);
            subjectsList.Create();

            var activitiesList = new ActivitiesList(dataModel);
            activitiesList.Create();

            var yearsList = new YearsList(dataModel);
            yearsList.Create();

            var timeConstraintsList = new TimeConstraintsList(dataModel);
            timeConstraintsList.Create();

            var spaceConstraintsList = new SpaceConstraintsList(dataModel);
            spaceConstraintsList.Create();

            var roomsList = new RoomsList(dataModel);
            roomsList.Create();

            xmlCreator.AddToRoot(daysList.GetList());
            xmlCreator.AddToRoot(hoursList.GetList());
            xmlCreator.AddToRoot(teachersList.GetList());
            xmlCreator.AddToRoot(yearsList.GetList());
            xmlCreator.AddToRoot(subjectsList.GetList());
            xmlCreator.AddToRoot(roomsList.GetList());
            xmlCreator.AddToRoot(activitiesList.GetList());
            xmlCreator.AddToRoot(timeConstraintsList.GetList());
            xmlCreator.AddToRoot(spaceConstraintsList.GetList());

            var filepath = xmlCreator.Save(outputDir);

            return filepath;
        }

    }
}
