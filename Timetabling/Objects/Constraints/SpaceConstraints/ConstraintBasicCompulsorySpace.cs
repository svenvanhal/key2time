using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Objects
{
	/// <summary>
    /// Constraint basic compulsory space.
    /// </summary>
	public class ConstraintBasicCompulsorySpace : AbstractConstraint
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.ConstraintBasicCompulsorySpace"/> class.
        /// </summary>
		public ConstraintBasicCompulsorySpace() 
		{
			SetElement("ConstraintBasicCompulsorySpace");
			SetWeight(100);
		}
        /// <summary>
        /// Create the specified list of constraints
        /// </summary>
        /// <returns>The created elemetns.</returns>
        /// <param name="dB">Datamodel.</param>
		public override XElement[] Create(DataModel dB)
		{
			return new XElement[0];
		}


		/// <summary>
		/// Returns the XElement
		/// </summary>
		/// <returns>The xelement.</returns>
		public override XElement ToXelement()
		{

			return constraint;
		}
	}
}
