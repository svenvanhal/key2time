using System;
using System.Collections.Generic;
using Timetabling.DB;
using Timetabling.Helper;
using Timetabling.Objects;

namespace Timetabling.Algorithms.FET
{

    internal class FetInputGenerator
    {

        // The FET input format version
        private const string FetVersion = "5.35.7";

        public DataModel Model { get; }

        /// <summary>
        /// Days.
        /// </summary>
        public readonly DaysList DaysList;

        /// <summary>
        /// Timeslots.
        /// </summary>
        public readonly HoursList HoursList;

        /// <summary>
        /// Teachers.
        /// </summary>
        public readonly TeachersList TeachersList;

        /// <summary>
        /// Subjects.
        /// </summary>
        public readonly SubjectsList SubjectsList;

        /// <summary>
        /// Activities.
        /// </summary>
        public readonly ActivitiesList ActivitiesList;

        /// <summary>
        /// Student sets / grades / years.
        /// </summary>
        public readonly YearsList YearsList;

        /// <summary>
        /// Time constraints.
        /// </summary>
        public readonly TimeConstraintsList TimeConstraintsList;

        /// <summary>
        /// Space constraints.
        /// </summary>
        public readonly SpaceConstraintsList SpaceConstraintsList;

        /// <summary>
        /// Rooms.
        /// </summary>
        public readonly RoomsList RoomsList;

        public FetInputGenerator(DataModel model)
        {
            Model = model;

            // Initialize list objects
            DaysList = new DaysList(Model);
            HoursList = new HoursList(Model);
            TeachersList = new TeachersList(Model);
            SubjectsList = new SubjectsList(Model);
            ActivitiesList = new ActivitiesList(Model);
            YearsList = new YearsList(Model);
            TimeConstraintsList = new TimeConstraintsList(Model);
            SpaceConstraintsList = new SpaceConstraintsList(Model);
            RoomsList = new RoomsList(Model);
        }

        public IDictionary<int, Activity> GetActivities()
        {
            if (ActivitiesList.Activities == null) throw new InvalidOperationException("The activities list has not yet been generated.");
            return ActivitiesList.Activities;
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
            xmlCreator.AddToRoot(DaysList.Create());
            xmlCreator.AddToRoot(HoursList.Create());
            xmlCreator.AddToRoot(TeachersList.Create());
            xmlCreator.AddToRoot(SubjectsList.Create());
            xmlCreator.AddToRoot(YearsList.Create());
            xmlCreator.AddToRoot(ActivitiesList.Create());
            xmlCreator.AddToRoot(TimeConstraintsList.Create());
            xmlCreator.AddToRoot(SpaceConstraintsList.Create());
            xmlCreator.AddToRoot(RoomsList.Create());

            return xmlCreator.Save(outputDir);
        }

    }
}
