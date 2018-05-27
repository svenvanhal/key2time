using System;
using System.Xml.Linq;
using System.Linq;
using Timetabling.DB;
using System.Collections.Generic;

namespace Timetabling.Objects
{
	/// <summary>
	/// Constraint specifies the total numer of hours a studentset can have in a week
	/// </summary>
	public class ConstraintStudentsSetMaxHoursContinuously : AbstractConstraint
	{

		/// <summary>
		/// Gets or sets the number of hours.
		/// </summary>
		/// <value>The number of hours.</value>
		public int numberOfHours { get; set; } = 0;

		/// <summary>
		/// Gets or sets the name of the grade.
		/// </summary>
		/// <value>The name of the grade.</value>
		public string gradeName { get; set; } = "";

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="T:Timetabling.Objects.ConstraintStudentsSetMaxHoursContinuously"/> class.
		/// </summary>
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

		/// <summary>
		/// Creates the array of XElements for the constraint.
		/// </summary>
		/// <returns>The created array.</returns>
		/// <param name="dB">Datamodel.</param>
		public override XElement[] Create(DataModel dB)
		{
			var query = from g in dB.Tt_GradeLesson
						join l in dB.School_Lookup_Grade on g.gradeId equals l.GradeID
						select new { g.numberOfLessons, l.GradeName };

			var result = new List<XElement>();
			query.AsEnumerable().ToList().ForEach(item => result.Add(new ConstraintStudentsSetMaxHoursContinuously { numberOfHours = item.numberOfLessons, gradeName = item.GradeName }.ToXelement()));

			return result.ToArray();


		}

	}
}
