using System;
using System.Collections.Generic;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects



{
	using System.Linq;
	using Timetable.timetable.Objects.Constraints.SpaceConstraints;

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
			constraints.ForEach(item => list.Add(item.Create(dB)));
		}

        /// <summary>
        /// Creates the constraints.
        /// </summary>
		private void CreateConstraints()
		{
			list.Add(new ConstraintBasicCompulsorySpace().ToXelement());
			constraints.Add(new ConstraintRoomNotAvailableTimes());

		}       
	}
}
