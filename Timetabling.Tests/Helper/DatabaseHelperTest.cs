using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Timetabling.DB;
using Timetabling.Helper;
using Timetabling.Objects;
using Timetabling.Resources;

namespace Timetabling.Tests.Helper
{
    internal class DatabaseHelperExposer : DatabaseHelper
    {
        public DatabaseHelperExposer(DataModel model) : base(model) { }
        public new int CreateTimetable(DataModel model, Timetable tt) => base.CreateTimetable(model, tt);
        public new void CreateActivities(DataModel model, int ttId, Timetable tt) => base.CreateActivities(model, ttId, tt);
        public new void CreateActivityTeacherRelations(DataModel model, int activityId, Activity activity) => base.CreateActivityTeacherRelations(model, activityId, activity);
        public new void CreateActivityClassRelations(DataModel model, int activityId, Activity activity) => base.CreateActivityClassRelations(model, activityId, activity);
    }

    internal class DatabaseHelperTest
    {

        [Test]
        public void ConstructorTest()
        {
            var model = new Mock<DataModel>();
            var dbHelper = new DatabaseHelper(model.Object);

            Assert.AreEqual(model.Object, dbHelper.Model);
        }

        [Test]
        public void SaveTimetableTest()
        {
            var model = new DataModel(Effort.DbConnectionFactory.CreateTransient());

            using (var context = new DatabaseHelper(model))
            {
                var tt = CreateTimetable();
                Assert.DoesNotThrow(() => context.SaveTimetable(tt));
            }
        }

        [Test]
        public void CreateTimetableTest()
        {
            var model = new DataModel(Effort.DbConnectionFactory.CreateTransient());

            using (var context = new DatabaseHelperExposer(model))
            {

                // Create timetable
                var tt = CreateTimetable();
                context.CreateTimetable(model, tt);

                // Get timetable entry
                var ttEntry = from t in model.Timetables select t;

                // Perform checks
                Assert.AreEqual("Test Timetable", ttEntry.First().Name);
            }

        }

        [Test]
        public void CreateActivitiesTest()
        {
            var model = new DataModel(Effort.DbConnectionFactory.CreateTransient());

            using (var context = new DatabaseHelperExposer(model))
            {

                // Create activities
                var tt = CreateTimetable();
                context.CreateActivities(model, 1000, tt);

                // Get timetable entry
                var ttEntry = from a in model.TimetableActivities select a;

                // Verify timetable ID matches
                Assert.AreEqual(1000, ttEntry.First().TimetableId);

                // Verify day is parsed correctly and matches
                Assert.AreEqual(5, ttEntry.First().Day);

                // Verify timeslot is parsed correctly and matches
                Assert.AreEqual(9, ttEntry.First().Timeslot);

                // Verify subject matches
                Assert.AreEqual(2, ttEntry.First().SubjectId);

                // Verify collection params match
                Assert.AreEqual(true, ttEntry.First().IsCollection);
                Assert.AreEqual(259, ttEntry.First().CollectionId);
            }

        }

        [Test]
        public void CreateActivityTeacherRelationsTest()
        {
            var model = new DataModel(Effort.DbConnectionFactory.CreateTransient());

            using (var context = new DatabaseHelperExposer(model))
            {

                // Create activity / teacher relations
                var tt = CreateTimetable();
                context.CreateActivityTeacherRelations(model, 2, tt.Activities[0].Resource);

                // Get timetable entries
                var ttEntry = from a in model.TimetableActivityTeachers orderby a.Id select a;
                var teacherEntry1 = ttEntry.First();
                var teacherEntry2 = ttEntry.Skip(1).First();

                // Check first teacher
                Assert.AreEqual(2, teacherEntry1.ActivityId);
                Assert.AreEqual(59, teacherEntry1.TeacherId);

                // Check second teacher
                Assert.AreEqual(2, teacherEntry2.ActivityId);
                Assert.AreEqual(60, teacherEntry2.TeacherId);

            }

        }

        [Test]
        public void CreateActivityClassRelationsTest()
        {
            var model = new DataModel(Effort.DbConnectionFactory.CreateTransient());

            using (var context = new DatabaseHelperExposer(model))
            {

                // Create activities / teacher relations
                var tt = CreateTimetable();
                context.CreateActivityClassRelations(model, 5, tt.Activities[0].Resource);

                // Get timetable entries
                var ttEntry = from a in model.TimetableActivityClasses orderby a.Id select a;

                // Check student set
                Assert.AreEqual(5, ttEntry.First().ActivityId);
                Assert.AreEqual(9, ttEntry.First().ClassId);

            }

        }

        [Test]
        public void GetDayFromStringTest()
        {
            Assert.AreEqual((int)Days.Friday, DatabaseHelper.GetDayFromString("Friday"));
        }

        [Test]
        public void GetDayFromInvalidStringTest()
        {
            Assert.Throws<ArgumentException>(() => DatabaseHelper.GetDayFromString("Weekend"));
        }

        private Timetable CreateTimetable()
        {

            var activities = new List<Timetable.TimetableActivity>
            {
                new Timetable.TimetableActivity
                {
                    Id = "1",
                    Day = "Friday",
                    Hour = "9",
                    Resource = new Activity
                    {
                        Id = 2,
                        Subject = 2,
                        Students = new Dictionary<string, int> {{"ClassName", 9}},
                        Teachers = new List<int> {59, 60},
                        IsCollection = true,
                        CollectionId = 259
                    }
                }
            };

            var tt = new Timetable
            {
                Activities = activities,
                PlacedActivities = 1,
                ConflictWeight = 0,
                SoftConflicts = new List<string> {"Test Conflict"}
            };

            return tt;
        }

    }
}
