using System;
using System.Collections.Generic;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects



{
	using System.Linq;
	public class SpaceConstraintsList : AbstractList
	{

		List<AbstractConstraint> constraints;

		public SpaceConstraintsList(DataModel _dB) : base(_dB)
		{
			SetListElement("Space_Constraints_List");
			constraints = new List<AbstractConstraint>();
		}

		public override void Create()
		{
			CreateConstraints();
			list.Add(constraints.Select(constraint => constraint.ToXelement()).ToArray());
		}
        
		private void CreateConstraints(){
			constraints.Add(new ConstraintBasicCompulsorySpace(dB));

		}

	}
}
