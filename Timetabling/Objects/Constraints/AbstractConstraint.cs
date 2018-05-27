using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Objects
{
	/// <summary>
	/// Abstract constraint. Each constraint need to implement this class
	/// </summary>
	public abstract class AbstractConstraint
	{
		/// <summary>
		/// Gets or sets the weight, default is 100
		/// </summary>
		/// <value>The weight.</value>
		public int weight { get; set; } = 100;

		/// <summary>
		/// Gets or sets the constraint XElement.
		/// </summary>
		/// <value>The constraint.</value>
		public XElement constraint { get; set; }

		/// <summary>
		/// Sets the weight.
		/// </summary>
		/// <param name="w">The width.</param>
		public void SetWeight(int w)
		{
			weight = w;
			constraint.Add(new XElement("Weight_Percentage", weight));

		}
		/// <summary>
		/// Sets the element.
		/// </summary>
		/// <param name="s">S.</param>
		public void SetElement(string s)
		{
			constraint = new XElement(s);
		}
		/// <summary>
		/// Returns the XElement
		/// </summary>
		/// <returns>The xelement.</returns>
		public abstract XElement ToXelement();

		/// <summary>
		/// Creates the array of XElements for the constraint.
		/// </summary>
		/// <returns>The created array.</returns>
		/// <param name="dB">Datamodel.</param>
		public abstract XElement[] Create(DataModel dB);
	}
}