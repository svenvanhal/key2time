using System;
using System.Collections.Generic;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects



{
	using System.Linq;
	public class TimeConstraintsList : AbstractList
	{

		List<AbstractConstraint> constraints;

		public TimeConstraintsList(DataModel _dB) : base(_dB)
		{
			SetListElement("Time_Constraints_List");
			constraints = new List<AbstractConstraint>();
		}

		public override void Create()
		{
			CreateConstraints();
			list.Add(constraints.Select(constraint => constraint.ToXelement()).ToArray());
		}

		private void CreateConstraints(){
			constraints.Add(new ConstraintBasicCompulsoryTime());
			CreateConstraintStudentsSetMaxHoursContinuously();

		}

		private void CreateConstraintStudentsSetMaxHoursContinuously(){

			var query = from g in dB.tt_GradeLesson
                        join l in dB.School_Lookup_Grade on g.gradeId equals l.GradeID
                        select new { g.numberOfLessons, l.GradeName };

            foreach(var item in query){
                constraints.Add(new ConstraintStudentsSetMaxHoursContinuously(item.numberOfLessons, item.GradeName));
            } 
		}

	}
}
