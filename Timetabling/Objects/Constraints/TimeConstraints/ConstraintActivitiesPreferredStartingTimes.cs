using System;
using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.TimeConstraints
{

  
    public class ConstraintActivitiesPreferredStartingTimes : AbstractConstraint
    {

        public int subject;
        public int numberOfHours;
        public List<Days> days { get; set; } = new List<Days>();
        public List<int> hours { get; set; } = new List<int>();

        public ConstraintActivitiesPreferredStartingTimes()
        {
            SetElement("ConstraintActivitiesPreferredStartingTimes");
            SetWeight(100);

        }

        public override XElement[] Create(DataModel dB)
        {
            var query = from tf in dB.Tt_TimeOff
                        where tf.ItemType == 2
                        select new { tf.day, tf.ItemId, tf.lessonIndex };
           
            var result = new List<XElement>();

            var check = new List<int>(); //List to check subject already done

            foreach (var item in query)
            {
                if (!check.Contains(item.ItemId))
                {
                    check.Add(item.ItemId);

                    var subjectTimeOff = query.Where(x => x.ItemId.Equals(item.ItemId)).Select(x => new { x.day, x.lessonIndex });
                    var _daysList = subjectTimeOff.Select(x => (Days)x.day).ToList();
                    var _hoursList = subjectTimeOff.Select(x => x.lessonIndex).ToList();
                    result.Add(new ConstraintActivitiesPreferredStartingTimes { subject = item.ItemId, days = _daysList, hours = _hoursList, numberOfHours = _hoursList.Count }.ToXelement());
                }

            }
            return result.ToArray();

        }

        public override XElement ToXelement()
        {
            constraint.Add(new XElement("Subject", subject),
                           new XElement("Number_of_Preferred_Starting_Times", numberOfHours));
            for (int i = 0; i < numberOfHours; i++)
            {

                constraint.Add(new XElement("Preferred_Starting_Time",
                                            new XElement("Preferred_Starting_Day", days[i]),
                                            new XElement("Preferred_Starting_Hour", hours[i])));
            }
            return constraint;
        }
    }
}
