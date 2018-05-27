using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.TimeConstraints
{
	/// <summary>
	/// Constraint specifing the time off of the studentset.
	/// </summary>
	public class ConstraintStudentsSetNotAvailableTimes : AbstractConstraint
	{
		/// <summary>
		/// The number of hours is always 1;
		/// </summary>
		int numberOfHours = 1;
		/// <summary>
		/// Gets or sets the students.
		/// </summary>
		/// <value>The students.</value>
		public string students { get; set; }

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
		/// <see cref="T:Timetabling.Objects.Constraints.TimeConstraints.ConstraintStudentsSetNotAvailableTimes"/> class.
		/// </summary>
		public ConstraintStudentsSetNotAvailableTimes()
		{
			SetElement("ConstraintStudentsSetNotAvailableTimes");

		}


		/// <summary>
		/// Creates the array of XElements for the constraint.
		/// </summary>
		/// <returns>The created array.</returns>
		/// <param name="dB">Datamodel.</param>
		public override XElement[] Create(DataModel dB)
		{
			var query = from tf in dB.Tt_TimeOff
						join cl in dB.School_Lookup_Class on tf.ItemId equals cl.ClassID
						where tf.ItemType == 3

						select new { tf.day, cl.ClassName, tf.lessonIndex, cl.timeOffConstraint };

			var result = new List<XElement>();

			query.AsEnumerable().ToList().ForEach(item => result.Add(new ConstraintStudentsSetNotAvailableTimes { students = item.ClassName, day = (Days)item.day, hour = item.lessonIndex, weight = item.timeOffConstraint }.ToXelement()));

			return result.ToArray();
		}

		/// <summary>
		/// Return the XElement representation of the constraint
		/// </summary>
		/// <returns>The xelement.</returns>
		public override XElement ToXelement()
		{
			SetWeight(weight);
			constraint.Add(new XElement("Students", students),
						   new XElement("Number_of_Not_Available_Times", numberOfHours),
						   new XElement("Not_Available_Time",
										new XElement("Day", day),
										new XElement("Hour", hour)));
			return constraint;
		}




	}
}
