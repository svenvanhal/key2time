using System;
using System.Collections.Generic;
using Timetabling.DB;
using Timetabling.Objects;
using Timetabling.XML;

namespace Timetabling.Algorithms.FET
{

    internal class FetInputGenerator
    {

        // The FET input format version
        private const string FetVersion = "5.35.7";

        public DataModel Model { get; }

        private readonly DaysList _daysList;
        private readonly HoursList _hoursList;
        private readonly TeachersList _teachersList;
        private readonly SubjectsList _subjectsList;
        private readonly ActivitiesList _activitiesList;
        private readonly YearsList _yearsList;
        private readonly TimeConstraintsList _timeConstraintsList;
        private readonly SpaceConstraintsList _spaceConstraintsList;
        private readonly RoomsList _roomsList;

        public FetInputGenerator(DataModel model)
        {
            Model = model;

            // Create lists from database
            _daysList = new DaysList(Model);
            _hoursList = new HoursList(Model);
            _teachersList = new TeachersList(Model);
            _subjectsList = new SubjectsList(Model);
            _activitiesList = new ActivitiesList(Model);
            _yearsList = new YearsList(Model);
            _timeConstraintsList = new TimeConstraintsList(Model);
            _spaceConstraintsList = new SpaceConstraintsList(Model);
            _roomsList = new RoomsList(Model);
        }

        public IDictionary<int, Activity> GetActivities()
        {
            if (_activitiesList?.Activities == null) throw new InvalidOperationException("The activities list has not yet been generated.");
            return _activitiesList.Activities;
        }

        /// <summary>
        /// Generates a .FET file for use in the algorithm.
        /// </summary>
        /// <param name="outputDir">Output directory for the FET file.</param>
        /// <returns>Filename of the generated .FET file.</returns>
        public string GenerateFetFile(string outputDir)
        {

            // Init XML creator
            var xmlCreator = new XmlCreator(FetVersion);

            // Create FET file
            xmlCreator.AddToRoot(_daysList.Create());
            xmlCreator.AddToRoot(_hoursList.Create());
            xmlCreator.AddToRoot(_teachersList.Create());
            xmlCreator.AddToRoot(_subjectsList.Create());
            xmlCreator.AddToRoot(_yearsList.Create());
            xmlCreator.AddToRoot(_activitiesList.Create());
            xmlCreator.AddToRoot(_timeConstraintsList.Create());
            xmlCreator.AddToRoot(_spaceConstraintsList.Create());
            xmlCreator.AddToRoot(_roomsList.Create());

            return xmlCreator.Save(outputDir);
        }

    }
}
