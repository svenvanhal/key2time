using System;
using System.Collections.Generic;
using System.Linq;
using Timetabling.DB;
using Timetabling.Objects;
using Timetabling.Resources;
using Timetabling.Resources.Constraints;

namespace Implementation.Key2Soft
{

    /// <summary>
    /// Create Timetable resources from database.
    /// </summary>
    public static class DboResourceFactory
    {

        /// <summary>
        /// Retrieves, processes and constructs day objects.
        /// </summary>
        /// <returns>Dictionary with key:id and value:Day.</returns>
        public static Dictionary<int, Day> GetDays()
        {
            var days = new Dictionary<int, Day>();

            // Iterate over days
            foreach (int day in Enum.GetValues(typeof(Days)))
            {
                days.Add(day, new Day
                {
                    Name = Enum.GetName(typeof(Days), day)
                });
            }

            return days;
        }

        /// <summary>
        /// Retrieves, processes and constructs timeslot (or: period, hour) objects.
        /// </summary>
        /// <returns>Dictionary with key:id and value:Timeslot.</returns>
        public static Dictionary<int, Timeslot> GetTimeslots()
        {
            var timeslots = new Dictionary<int, Timeslot>();

            // Iterate over timeslots
            for (var i = 1; i <= 9; i++)
            {
                timeslots.Add(i, new Timeslot
                {
                    Name = $"Period {i}"
                });
            }

            return timeslots;
        }

        /// <summary>
        /// Retrieves, processes and constructs teacher objects.
        /// </summary>
        /// <param name="model">A compatible DataModel.</param>
        /// <returns>Dictionary with key:id and value:Teacher.</returns>
        public static Dictionary<int, Teacher> GetTeachers(DataModel model)
        {
            var teachers = new Dictionary<int, Teacher>();

            // Iterate over teachers
            foreach (var teacher in model.HR_MasterData_Employees.Where(teacher => teacher.IsTeacher == true && teacher.IsActive == true))
            {
                teachers.Add((int)teacher.EmployeeID, new Teacher
                {
                    Id = (int)teacher.EmployeeID,
                    Name = teacher.ShortName
                });
            }

            return teachers;
        }

        /// <summary>
        /// Retrieves, processes and constructs subject objects.
        /// </summary>
        /// <param name="model">A compatible DataModel.</param>
        /// <returns>Dictionary with key:id and value:Subject.</returns>
        public static Dictionary<int, Subject> GetSubjects(DataModel model)
        {
            var subjects = new Dictionary<int, Subject>();

            foreach (var subject in model.Subject_MasterData_Subject.Where(subject => subject.IsActive == true))
            {
                subjects.Add(subject.SubjectID, new Subject
                {
                    Id = subject.SubjectID,
                    Name = subject.SubjectName
                });
            }

            return subjects;
        }

        /// <summary>
        /// Retrieves, processes and constructs room objects.
        /// </summary>
        /// <param name="model">A compatible DataModel.</param>
        /// <returns>Dictionary with key:id and value:Room.</returns>
        public static Dictionary<int, Room> GetRooms(DataModel model)
        {
            var rooms = new Dictionary<int, Room>();

            // Iterate over rooms
            foreach (var room in model.School_BuildingsUnits.Where(room => room.IsActive == true))
            {
                rooms.Add(room.ID, new Room
                {
                    Id = room.ID,
                    Name = room.UnitName
                });
            }

            return rooms;
        }

        /// <summary>
        /// Retrieves, processes and constructs student set (or: year, grade) objects.
        /// </summary>
        /// <param name="model">A compatible DataModel.</param>
        /// <returns>Dictionary with key:id and value:StudentSet.</returns>
        public static Dictionary<int, StudentSet> GetYears(DataModel model)
        {
            var grades = new Dictionary<int, StudentSet>();

            var gradeQuery =
                from grade in model.School_Lookup_Grade
                where grade.IsActive == true
                select new
                {
                    GradeId = grade.GradeID,
                    GradeName = grade.GradeName,
                };

            Console.Read();

            // Loop over all grades
            foreach (var grade in gradeQuery)
            {

                // Create new student ste
                var studentSet = new StudentSet
                {
                    Id = grade.GradeId,
                    Name = grade.GradeName,
                };

                // Find classes in student set
                var groupQuery =
                    from cls in model.School_Lookup_Class
                    join grp in model.Tt_ClassGroup on cls.ClassID equals grp.classId into subGroups
                    where cls.IsActive == true && cls.GradeID == grade.GradeId
                    select new { ClassId = cls.ClassID, cls.ClassName, SubGroups = subGroups };

                foreach (var cls in groupQuery)
                {

                    var group = new Group
                    {
                        Id = cls.ClassId,
                        Name = cls.ClassName,
                    };

                    foreach (var subGroup in cls.SubGroups)
                    {
                        group.SubGroups.Add(subGroup.Id, new SubGroup
                        {
                            Id = subGroup.Id,
                            Name = subGroup.groupName
                        });
                    }

                }

                grades.Add(grade.GradeId, studentSet);
            }

            return grades;
        }

        /// <summary>
        /// Retrieves, processes and constructs activity objects.
        /// N.B.: this method is dependent on previously generated teachers, subjects and student sets.
        /// </summary>
        /// <param name="model">A compatible DataModel.</param>
        /// <param name="resources">Timetabling resources.</param>
        /// <returns>Dictionary with key:id and value:StudentSet.</returns>
        public static Dictionary<int, Activity> GetActivities(DataModel model, TimetableResourceCollection resources)
        {

            var activities = new Dictionary<int, Activity>();

            var query = from activity in model.School_TeacherClass_Subjects
                        join c in model.School_Lookup_Class on activity.ClassID equals c.ClassID
                        join s in model.Subject_SubjectGrade on new { activity.SubjectID, c.GradeID } equals new { s.SubjectID, s.GradeID }
                        select new { activity.TeacherID, activity.SubjectID, c.ClassID, activity.ID, s.NumberOfLlessonsPerWeek, s.NumberOfLlessonsPerDay };

            // Internal activity ID
            var counter = 0;

            // Loop over all activities
            foreach (var activity in query)
            {

                // Create internal activity ID
                var groupId = counter;

                // Add an activity for each lesson
                for (var i = 1; i <= activity.NumberOfLlessonsPerWeek; i++)
                {

                    // Create activity
                    activities.Add(counter, new Activity
                    {
                        Id = counter,
                        GroupId = groupId,
                        Teacher = resources.GetTeacher((int)activity.TeacherID),
                        Subject = resources.GetSubject(activity.SubjectID),
                        Students = resources.GetStudent(activity.ClassID).Groups[0], // TODO: fix this line
                        Duration = activity.NumberOfLlessonsPerDay, // TODO: what does NumberOfLlessonsPerDay mean exactly? Lessons sequential, during the day, min lessons per day, max lessons per day?
                        Lessons = activity.NumberOfLlessonsPerWeek // TODO: what does NumberOfLlessonsPerWeek mean exactly? Idem.
                    });

                    counter++;
                }
            }

            return activities;
        }

        /// <summary>
        /// Retrieves, processes and constructs time constraint objects.
        /// N.B.: this method is dependent on previously generated [resources].
        /// </summary>
        /// <param name="model">A compatible DataModel.</param>
        /// <param name="resources">Timetabling resources.</param>
        /// <returns>Dictionary with key:id and value:TimeConstraint.</returns>
        // TODO: specify which resources are needed in XML-doc summary.
        public static Dictionary<int, TimeConstraint> GetTimeConstraints(DataModel model, TimetableResourceCollection resources)
        {
            var timeConstraints = new Dictionary<int, TimeConstraint>
            {
                { 1, DboConstraintFactory.CreateBasicTimeConstraint() }
            };

            return timeConstraints;
        }

        /// <summary>
        /// Retrieves, processes and constructs space constraint objects.
        /// N.B.: this method is dependent on previously generated [resources].
        /// </summary>
        /// <param name="model">A compatible DataModel.</param>
        /// <param name="resources">Timetabling resources.</param>
        /// <returns>Dictionary with key:id and value:SpaceConstraint.</returns>
        // TODO: specify which resources are needed in XML-doc summary.
        public static Dictionary<int, SpaceConstraint> GetSpaceConstraints(DataModel model, TimetableResourceCollection resources)
        {

            var spaceConstraints = new Dictionary<int, SpaceConstraint>
            {
                { 1, DboConstraintFactory.CreateBasicSpaceConstraint() }
            };

            return spaceConstraints;
        }

    }
}
