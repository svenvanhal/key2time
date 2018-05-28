using System;
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
		public string students { get; set; }
		public List<int> days { get; set; } = new List<int>();
		public int numberOfHours { get; set; } = 8; //default

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
						select new { grade.GradeName, weekend.dayIndex }
										;

			var result = new List<XElement>();

			HoursList hours = new HoursList(dB); 
			hours.Create();

			List<String> grades = new List<string>(); //A tempeorary list to check for duplicates

			foreach (var item in query)
			{
				if (!grades.Contains(item.GradeName)) //Only add when not exisiting 
				{
					grades.Add(item.GradeName);
					var temp = query.Where(x => x.GradeName.Equals(item.GradeName)).Select(x => x.dayIndex);
					result.Add(new ConstraintPeriodSection { students = item.GradeName, days = temp.ToList(), numberOfHours = hours.numberOfHours }.ToXelement());
				}
			}

			return result.ToArray();
		}
        /// <summary>
        /// Returns the XElement representation of the constraint
        /// </summary>
        /// <returns>The xelement.</returns>
		public override XElement ToXelement()
		{
            
		
			constraint.Add(new XElement("Students", students),
						   new XElement("Number_of_Not_Available_Times", numberOfHours * days.Count));

            //For each hour of each day of the weekend, a not available time is added
			foreach (Days day in days)
			{
				for (int i = 1; i <= numberOfHours; i++)
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
