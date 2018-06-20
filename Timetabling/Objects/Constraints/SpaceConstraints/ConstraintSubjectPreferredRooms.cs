using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.SpaceConstraints
{
    public class ConstraintSubjectPreferredRooms : AbstractConstraint
    {
        /// <summary>
        /// Gets or sets the list of preffered rooms
        /// </summary>
        /// <value>The rooms.</value>
        public List<int> Rooms { get; set; } = new List<int>();

        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>The subject identifier.</value>
        public int? SubjectID { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Timetabling.Objects.Constraints.SpaceConstraints.ConstraintSubjectPreferredRooms"/> class.
        /// </summary>
        public ConstraintSubjectPreferredRooms()
        {
            SetElement("ConstraintSubjectPreferredRooms");
            SetWeight(100);
        }

        /// <summary>
        /// Create the specified constraints list of preffered rooms from the datamodel
        /// </summary>
        /// <returns>The created consraints</returns>
        /// <param name="dB">D b.</param>
        public override XElement[] Create(DataModel dB)
        {
            var results = new List<XElement>();

            var query = dB.SubjectGrades.Where(item => item.BuildingUnitTypeId != null)
                          .Select(item => new {SubjectID = item.SubjectId, BuildingUnitTypeID = item.BuildingUnitTypeId});

            foreach (var item in query)
            {
                var rooms = (from b in dB.Buildings
                             where b.TypeId == item.BuildingUnitTypeID && b.IsActive == true
                             select b.Id).DefaultIfEmpty().ToList();

                var roomConstraint = new ConstraintSubjectPreferredRooms { Rooms = rooms, SubjectID = item.SubjectID };
                results.Add(roomConstraint.ToXelement());
            }

            return results.ToArray();
        }

        /// <summary>
        /// Return the XElement representation
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {
            constraint.Add(new XElement("Subject", SubjectID),
                           new XElement("Number_of_Preferred_Rooms", Rooms.Count));

            foreach (var room in Rooms)
            {
                constraint.Add(new XElement("Preferred_Room", room));
            }
            return constraint;
        }
    }
}
