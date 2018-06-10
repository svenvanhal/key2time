using System;
using System.Collections.Generic;
using Timetabling.Resources.Constraints;

namespace Timetabling.Resources
{

    /// <summary>
    /// Stores all resources required for timetable generation.
    /// </summary>
    public class TimetableResourceCollection
    {

        /// <summary>
        /// Available days for the timetable.
        /// </summary>
        public Dictionary<int, Day> Days { get; set; }

        /// <summary>
        /// Available periods for the timetable.
        /// </summary>
        public Dictionary<int, Timeslot> Timeslots { get; set; }

        /// <summary>
        /// Available subjects for the timetable.
        /// </summary>
        public Dictionary<int, Subject> Subjects { get; set; }

        /// <summary>
        /// Available teachers for the timetable.
        /// </summary>
        public Dictionary<int, Teacher> Teachers { get; set; }

        /// <summary>
        /// Available rooms for the timetable.
        /// </summary>
        public Dictionary<int, StudentSet> Students { get; set; }

        /// <summary>
        /// The activities to be scheduled in the timetable.
        /// </summary>
        public Dictionary<int, Activity> Activities { get; set; }

        /// <summary>
        /// Available rooms for the timetable.
        /// </summary>
        public Dictionary<int, Room> Rooms { get; set; }

        /// <summary>
        /// Constraints on time resources for this timetable.
        /// </summary>
        public Dictionary<int, TimeConstraint> TimeConstraints { get; set; }

        /// <summary>
        /// Constraints on space / location resources for this timetable.
        /// </summary>
        public Dictionary<int, SpaceConstraint> SpaceConstraints { get; set; }

        private readonly Dictionary<Type, object> _typeDict;

        /// <summary>
        /// Try and retrieve value from collection.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="key">Key to retrieve value for.</param>
        /// <param name="collection">Collection to find key in.</param>
        /// <returns>Value of type T belonging to the specified key.</returns>
        /// <exception cref="ArgumentNullException">When the resource collection is null.</exception>
        /// <exception cref="KeyNotFoundException">When the specified key cannot be found in the resource collection.</exception>
        public T GetValue<T>(int? key, Dictionary<int, T> collection)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (!collection.ContainsKey(key.Value)) throw new KeyNotFoundException("Could not find specified key.");

            return collection[key.Value];
        }

        public Day GetDay(int? key) => GetValue(key, Days);
        public Timeslot GetTimeslot(int? key) => GetValue(key, Timeslots);
        public Subject GetSubject(int? key) => GetValue(key, Subjects);
        public Teacher GetTeacher(int? key) => GetValue(key, Teachers);
        public StudentSet GetStudent(int? key) => GetValue(key, Students);
        public Activity GetActivity(int? key) => GetValue(key, Activities);
        public Room GetRoom(int? key) => GetValue(key, Rooms);
        public TimeConstraint GetTimeConstraint(int? key) => GetValue(key, TimeConstraints);
        public SpaceConstraint GetSpaceConstraint(int? key) => GetValue(key, SpaceConstraints);

    }
}
