using System;
using System.Xml.Linq;
using System.Linq;
using Timetable.timetable.DB;
using System.Collections.Generic;

namespace Timetable.timetable.Objects
{
	public class ConstraintStudentsSetMaxHoursContinuously : AbstractConstraint
	{
		public int numberOfHours { get; set; } = 0;
		public string gradeName { get; set; } = "";
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="T:Timetable.timetable.Objects.ConstraintStudentsSetMaxHoursContinuously"/> class.
		/// </summary>
		/// <param name="_numberOfHours">Number of hours.</param>
		/// <param name="_gradeName">Grade name.</param>
		public ConstraintStudentsSetMaxHoursContinuously()
		{

			SetElement("ConstraintStudentsSetMaxHoursContinuously");
			SetWeight(100);

		}


		/// <summary>
		/// Returns the XElement
		/// </summary>
		/// <returns>The xelement.</returns>
		public override XElement ToXelement()
		{
			constraint.Add(new XElement("Maximum_Hours_Continuously", numberOfHours),
						   new XElement("Students", gradeName));
			return constraint;
		}

		public override XElement[] Create(DataModel dB)
		{
			var query = from g in dB.tt_GradeLesson
						join l in dB.School_Lookup_Grade on g.gradeId equals l.GradeID
						select new { g.numberOfLessons, l.GradeName };

			var result = new List<XElement>();
			query.AsEnumerable().ToList().ForEach(item => result.Add(new ConstraintStudentsSetMaxHoursContinuously { numberOfHours = item.numberOfLessons, gradeName = item.GradeName }.ToXelement()));

			return result.ToArray();


		}

	}
}
