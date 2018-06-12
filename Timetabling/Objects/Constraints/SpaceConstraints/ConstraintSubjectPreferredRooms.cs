using System;
using System.Xml.Linq;
using Timetabling.DB;
using System.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.SpaceConstraints
{
    public class ConstraintSubjectPreferredRooms : AbstractConstraint
    {

        public List<int> rooms { get; set; } = new List<int>();

        public int subjectID { get; set; }

        public ConstraintSubjectPreferredRooms()
        {
            SetElement("ConstraintSubjectPreferredRooms");
            SetWeight(100);
        }

        public override XElement[] Create(DataModel dB)
        {
            var query = dB.Subject_SubjectGrade.Where(item => item.BuildingUnitTypeID != null).Select(item => new { item.SubjectID, item.BuildingUnitTypeID });

            var results = new List<XElement>();

            foreach (var item in query)
            {

                var _rooms = (from b in dB.School_BuildingsUnits
                    where b.TypeID == item.BuildingUnitTypeID && b.IsActive == true
                    select b.ID).DefaultIfEmpty().ToList();

                var constraint = new ConstraintSubjectPreferredRooms {rooms = _rooms, subjectID = (int) item.SubjectID}.ToXelement();
                results.Add(constraint);
            }

            return results.ToArray();
        }

        /// <summary>
        /// Tos the xelement.
        /// </summary>
        /// <returns>The xelement.</returns>
        public override XElement ToXelement()
        {
            constraint.Add(new XElement("Subject", subjectID),
                           new XElement("Number_of_Preferred_Rooms",rooms.Count() ));

            foreach (var room in rooms)
            {
                constraint.Add(new XElement("Preferred_Room", room));
            }
            return constraint;
        }
    }
}
