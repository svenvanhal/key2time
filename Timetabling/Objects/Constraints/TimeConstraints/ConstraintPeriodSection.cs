using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.TimeConstraints
{
    /// <summary>
    /// Constraint period section. This constraints creates the timeoff for the weekends
    /// </summary>
    public class ConstraintPeriodSection : AbstractConstraint
    {

        /// <summary>
        /// Students for whom this constraint holds.
        /// </summary>
        public string Students { get; set; }

        /// <summary>
        /// Days for which this constraint holds.
        /// </summary>
        public List<int> Days { get; set; } = new List<int>();

        /// <summary>
        /// Numer of hours for which this constraint holds.
        /// </summary>
        public int NumberOfHours { get; set; } = 8; //default

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetabling.Objects.Constraints.TimeConstraints.ConstraintPeriodSection"/> class.
        /// </summary>
        public ConstraintPeriodSection()
        {
            SetElement("ConstraintStudentsSetNotAvailableTimes"); //creates a studentnotavailabletimes constraint element.
            SetWeight(100); // Weekend has always a weight of 100 
        }

        /// <summary>
        /// Gets the data from the datamodel and creates the constraints
        /// </summary>
        /// <returns>The created list of constraints</returns>
        /// <param name="dB">Datamodel</param>
        public override XElement[] Create(DataModel dB)
        {
            var query = from grade in dB.School_Lookup_Grade
                        join stage in dB.School_Lookup_Stage on grade.StageID equals stage.StageID
                        join weekend in dB.Section_WeekEnd on stage.SectionID equals weekend.sectionId
                        where grade.IsActive == true
                        select new { grade.GradeName, weekend.dayIndex };

            var hours = new HoursList(dB);
            hours.Create();

            var grades = new List<string>(); //A tempeorary list to check for duplicates

            var result = new List<XElement>();

            foreach (var item in query)
            {
                // Continue if we've already seen this grade
                if (grades.Contains(item.GradeName)) continue;

                grades.Add(item.GradeName);
                var temp = query.Where(x => x.GradeName.Equals(item.GradeName)).Select(x => x.dayIndex);

                result.Add(new ConstraintPeriodSection
                {
                    Students = item.GradeName,
                    Days = temp.ToList(),
                    NumberOfHours = hours.numberOfHours
                }.ToXelement());
            }

            return result.ToArray();
        }

        /// <summary>
        /// Returns the XElement representation of the constraint
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {

            constraint.Add(new XElement("Students", Students),
                           new XElement("Number_of_Not_Available_Times", NumberOfHours * Days.Count));
            
            //For each hour of each day of the weekend, a not available time is added
            foreach (Days day in Days)
            {
                for (var i = 1; i <= NumberOfHours; i++)
                {
                    
                    constraint.Add(new XElement("Not_Available_Time",
                                   new XElement("Day", day),
                                   new XElement("Hour", i)));
                }
            }

            return constraint;
        }

    }
}
