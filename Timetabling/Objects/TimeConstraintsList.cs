using System.Collections.Generic;
using Timetabling.DB;
using System.Xml.Linq;
using Timetabling.Objects.Constraints.TimeConstraints;

namespace Timetabling.Objects
{
   /// <summary>
   /// Time constraints list.
   /// </summary>
	public class TimeConstraintsList : AbstractList
	{

		List<AbstractConstraint> constraints;
		List<XElement> result;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.TimeConstraintsList"/> class.
        /// </summary>
        /// <param name="_dB">D b.</param>
		public TimeConstraintsList(DataModel _dB) : base(_dB)
		{
			SetListElement("Time_Constraints_List");
			constraints = new List<AbstractConstraint>();
			result = new List<XElement>();
		}
		/// <summary>
		/// Create the XElements of the constraints.
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
			list.Add(new ConstraintBasicCompulsoryTime().ToXelement());
		//	constraints.Add(new ConstraintStudentsSetMaxHoursContinuously());
			constraints.Add(new ConstraintTeacherNotAvailableTimes());
			constraints.Add(new ConstraintStudentsSetNotAvailableTimes());
			constraints.Add(new ConstraintMinDaysBetweenActivities());
		}
	}
}
