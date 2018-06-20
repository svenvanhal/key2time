using System.Collections.Generic;
using System.Xml.Linq;
using Timetabling.DB;
using Timetabling.Objects.Constraints;
using Timetabling.Objects.Constraints.SpaceConstraints;

namespace Timetabling.Objects
{

    /// <summary>
    /// Space constraints list.
    /// </summary>
    public class SpaceConstraintsList : AbstractList
    {

        /// <summary>
        /// List of constraints in this list.
        /// </summary>
        public readonly List<AbstractConstraint> Constraints;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.SpaceConstraintsList"/> class.
        /// </summary>
        /// <param name="_dB">D b.</param>
        public SpaceConstraintsList(DataModel _dB) : base(_dB)
        {
            SetListElement("Space_Constraints_List");
            Constraints = new List<AbstractConstraint>();
        }

        /// <summary>
        /// Creates the XElement for each constrain.
        /// </summary>
        public override XElement Create()
        {
            CreateConstraints();
            Constraints.ForEach(item => List.Add(item.Create(dB)));
            return List;
        }

        /// <summary>
        /// Creates the constraints.
        /// </summary>
        private void CreateConstraints()
        {
            List.Add(new ConstraintBasicCompulsorySpace().ToXelement());
            Constraints.Add(new ConstraintRoomNotAvailableTimes());
            Constraints.Add(new ConstraintSubjectPreferredRooms());
        }
    }
}
