using System;
using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects
{
	public class ConstraintStudentsSetMaxHoursDaily : AbstractConstraint
	{
		public int maxHoursDaily { get; set; }
		public string gradeName { get; set; }



		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="T:Timetabling.Objects.ConstraintStudentsSetMaxHoursDaily"/> class.
		/// </summary>
		/// <param name="_maxHoursDaily">Max hours daily.</param>
		/// <param name="_gradeName">Grade name.</param>
		public ConstraintStudentsSetMaxHoursDaily()
		{
			SetElement("ConstraintStudentsSetMaxHoursDaily");
			SetWeight(100);

		}

		/// <summary>
		/// Returns the XELement 
		/// </summary>
		/// <returns>The xelement.</returns>
		public override XElement ToXelement()
		{
			constraint.Add(new XElement("Maximum_Hours_Daily", maxHoursDaily),
						   new XElement("Students", gradeName));
			return constraint;
		}
        
		public override XElement[] Create(DataModel dB)
		{ //TO DO: Check query
			var query = from g in dB.Tt_GradeLesson
						join l in dB.School_Lookup_Grade on g.gradeId equals l.GradeID
						select new { g.numberOfLessons, l.GradeName };


			List<XElement> result = new List<XElement>();
			query.AsEnumerable().ToList().ForEach(item => result.Add(new ConstraintStudentsSetMaxHoursDaily { maxHoursDaily = item.numberOfLessons, gradeName = item.GradeName }.ToXelement())
						  );
			return result.ToArray();
		}
	}
}
