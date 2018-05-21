using System;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Objects
{
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
