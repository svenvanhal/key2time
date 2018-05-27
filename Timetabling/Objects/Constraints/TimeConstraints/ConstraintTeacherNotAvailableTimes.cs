
using System.Xml.Linq;
using System.Linq;
using Timetabling.DB;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.TimeConstraints
{
	/// <summary>
	/// Constraint specifing the time off of a teacher
	/// </summary>
	public class ConstraintTeacherNotAvailableTimes : AbstractConstraint
	{

		int numberOfHours = 1;
		/// <summary>
		/// Gets or sets the teacher id.
		/// </summary>
		/// <value>The teacher.</value>
		public int teacher { get; set; }

		/// <summary>
		/// Gets or sets the day.
		/// </summary>
		/// <value>The day.</value>
		public Days day { get; set; }

		/// <summary>
		/// Gets or sets the hour.
		/// </summary>
		/// <value>The hour.</value>
		public int hour { get; set; }

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="T:Timetabling.Objects.Constraints.TimeConstraints.ConstraintTeacherNotAvailableTimes"/> class.
		/// </summary>
		public ConstraintTeacherNotAvailableTimes()
		{
			SetElement("ConstraintTeacherNotAvailableTimes");

		}
		/// <summary>
		/// Creates the array of XElements for the constraint.
		/// </summary>
		/// <returns>The created array.</returns>
		/// <param name="dB">Datamodel.</param>
		public override XElement[] Create(DataModel dB)
		{
			var query = from tf in dB.Tt_TimeOff
						where tf.ItemType == 1
						join e in dB.HR_MasterData_Employees on tf.ItemId equals e.EmployeeID
						select new { tf.day, tf.ItemId, tf.lessonIndex, e.timeOffConstraint };

			var result = new List<XElement>();
			query.AsEnumerable().ToList().ForEach(item => result.Add(new ConstraintTeacherNotAvailableTimes { teacher = item.ItemId, day = (Days)item.day, hour = item.lessonIndex, weight = item.timeOffConstraint }.ToXelement()));

			return result.ToArray();
		}

		/// <summary>
		/// Return the XElement represention of the constraint
		/// </summary>
		/// <returns>The xelement.</returns>
		public override XElement ToXelement()
		{
			SetWeight(weight);
			constraint.Add(new XElement("Teacher", teacher),
						   new XElement("Number_of_Not_Available_Times", numberOfHours),
						   new XElement("Not_Available_Time",
										new XElement("Day", day),
										new XElement("Hour", hour)));
			return constraint;
		}
	}
}
