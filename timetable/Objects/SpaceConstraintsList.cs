using System;
using System.Collections.Generic;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects



{
	using System.Linq;
	public class SpaceConstraintsList : AbstractList
	{

		List<AbstractConstraint> constraints;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetable.timetable.Objects.SpaceConstraintsList"/> class.
        /// </summary>
        /// <param name="_dB">D b.</param>
		public SpaceConstraintsList(DataModel _dB) : base(_dB)
		{
			SetListElement("Space_Constraints_List");
			constraints = new List<AbstractConstraint>();
		}
        /// <summary>
        /// Creates the XElement for each constrain.
        /// </summary>
		public override void Create()
		{
			CreateConstraints();
			list.Add(constraints.Select(constraint => constraint.ToXelement()).ToArray());
		}

        /// <summary>
        /// Creates the constraints.
        /// </summary>
		private void CreateConstraints()
		{
			constraints.Add(new ConstraintBasicCompulsorySpace());

	


		}
        
	}
}
