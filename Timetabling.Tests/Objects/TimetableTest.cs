using System.Collections.Generic;
using NUnit.Framework;
using Timetabling.Objects;

namespace Timetabling.Tests.Objects
{

    class TimetableTest
    {

        [Test]
        public void TimetableNotPartialTest()
        {

            var tt = new Timetable
            {
                PlacedActivities = 1,
                Activities = new List<Timetable.TimetableActivity>
                {
                    new Timetable.TimetableActivity()
                }
            };

            Assert.IsFalse(tt.IsPartial);
        }

        [Test]
        public void TimetablePartialTest()
        {

            var tt = new Timetable
            {
                PlacedActivities = 0,
                Activities = new List<Timetable.TimetableActivity> { new Timetable.TimetableActivity() }
            };

            Assert.IsTrue(tt.IsPartial);
        }

        [Test]
        public void TimetablePartialNullTest()
        {

            var tt = new Timetable
            {
                Activities = null
            };

            Assert.DoesNotThrow(() => {
                var _ = tt.IsPartial;
            });
        }

    }
}
