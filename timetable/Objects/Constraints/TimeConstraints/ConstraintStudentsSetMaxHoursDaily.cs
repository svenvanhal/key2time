using System;
using System.Xml.Linq;
using Timetable.timetable.DB;
using System.Linq;
using System.Collections.Generic;

namespace Timetable.timetable.Objects
{
	public class ConstraintStudentsSetMaxHoursDaily : AbstractConstraint
	{
		public int maxHoursDaily { get; set; }
		public string gradeName { get; set; }



		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="T:Timetable.timetable.Objects.ConstraintStudentsSetMaxHoursDaily"/> class.
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
		{ 
			var query = from g in dB.tt_GradeLesson
						join l in dB.School_Lookup_Grade on g.gradeId equals l.GradeID
						join sec in dB.School_Lookup_Stage on l.StageID equals sec.StageID
						join con in dB.tt_SectionLessonConfiguration on sec.SectionID equals con.sectionId

						select new { con.lessonPerDay, l.GradeName };


			List<XElement> result = new List<XElement>();
			query.AsEnumerable().ToList().ForEach(item => result.Add(new ConstraintStudentsSetMaxHoursDaily { maxHoursDaily = item.lessonPerDay, gradeName = item.GradeName }.ToXelement())
						  );
			return result.ToArray();
		}
	}
}
