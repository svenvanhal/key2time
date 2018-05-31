using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Objects.Constraints.TimeConstraints
{
	
    /// <summary>
    /// Constraint for the basic compulsary time, such as that no teacher can be on two places in the same timeslot.
    /// </summary>
	public class ConstraintBasicCompulsoryTime : AbstractConstraint
	{
        
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.Constraints.TimeConstraints.ConstraintBasicCompulsoryTime"/> class.
        /// </summary>
		public ConstraintBasicCompulsoryTime() 
		{
			SetElement("ConstraintBasicCompulsoryTime");
			SetWeight(100);
		}

		/// <summary>
        /// Creates the array of XElements for the constraint.
        /// </summary>
        /// <returns>The created array.</returns>
        /// <param name="dB">Datamodel.</param>
		public override XElement[] Create(DataModel dB) => new XElement[0];

	    /// <summary>
        /// Returns the Xelement representation of the constraint
        /// </summary>
        /// <returns>The xelement.</returns>
		public override XElement ToXelement() => constraint;

	}
}
