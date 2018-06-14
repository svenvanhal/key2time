using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.SpaceConstraints
{
    public class ConstraintSubjectPreferredRooms : AbstractConstraint
    {

        public List<int> Rooms { get; set; } = new List<int>();

        public int? SubjectId { get; set; }

        public ConstraintSubjectPreferredRooms()
        {
            SetElement("ConstraintSubjectPreferredRooms");
            SetWeight(100);
        }

        public override XElement[] Create(DataModel dB)
        {
            var results = new List<XElement>();

            var query = dB.Subject_SubjectGrade.Where(item => item.BuildingUnitTypeID != null)
                          .Select(item => new {item.SubjectID, item.BuildingUnitTypeID});

            foreach (var item in query)
            {
                var rooms = (from b in dB.School_BuildingsUnits
                             where b.TypeID == item.BuildingUnitTypeID && b.IsActive == true
                             select b.ID).DefaultIfEmpty().ToList();

                var roomConstraint = new ConstraintSubjectPreferredRooms { Rooms = rooms, SubjectId = item.SubjectID };
                results.Add(roomConstraint.ToXelement());
            }

            return results.ToArray();
        }

        /// <summary>
        /// Tos the xelement.
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {
            constraint.Add(new XElement("Subject", SubjectId),
                           new XElement("Number_of_Preferred_Rooms", Rooms.Count));

            foreach (var room in Rooms)
            {
                constraint.Add(new XElement("Preferred_Room", room));
            }
            return constraint;
        }
    }
}
