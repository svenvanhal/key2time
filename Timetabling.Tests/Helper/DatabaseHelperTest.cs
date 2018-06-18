using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Timetabling.Objects;
using Timetabling.Resources;

namespace Timetabling.Tests.Helper
{
    internal class DatabaseHelperTest
    {
        private Timetable Timetable { get; set; }

        [SetUp]
        public void SetUp()
        {

            var activities = new List<Timetable.TimetableActivity>
            {
                new Timetable.TimetableActivity
                {
                    Id = "1",
                    Day = "Monday",
                    Hour = "1",
                    Resource = new Activity
                    {
                        Id = 2,
                        Subject = 5,
                        Students = new Dictionary<string, int>{{ "ClassName", 9 }},
                        Teachers = new List<int>{ 59 },
                        IsCollection = true,
                        CollectionId = 259
                    }
                }
            };

            Timetable = new Timetable
            {
                Activities = activities,
                PlacedActivities = 1,
                ConflictWeight = 0,
                SoftConflicts = new List<string> {"Test Conflict"}
            };
        }


    }
}
