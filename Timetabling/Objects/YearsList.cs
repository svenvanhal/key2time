using System.Collections.Generic;
using Timetabling.DB;
using System.Linq;
using System.Xml.Linq;
using Timetabling.Resources;

namespace Timetabling.Objects
{
    /// <summary>
    /// Years list.
    /// </summary>
    public class YearsList : AbstractList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.YearsList"/> class.
        /// </summary>
        /// <param name="_dB">Database Model</param>
        public YearsList(DataModel _dB) : base(_dB) => SetListElement("Students_List");

        /// <summary>
        /// Create grades, with corresponding groups and subgroups
        /// </summary>
        public override void Create()
        {
            // Creates the different grades
            var query = dB.School_Lookup_Grade.Where(grade => grade.IsActive == true).Select(grade => grade.GradeName);
            foreach (var item in query)
            {
                List.Add(new XElement("Year", new XElement("Name", item)));
            }

            var grades = from c in dB.School_Lookup_Class
                         join grade in dB.School_Lookup_Grade on c.GradeID equals grade.GradeID
                         where c.IsActive == true
                         select new { c.ClassName, grade.GradeName };

            // Creates the different groups in a grade
            foreach (var item in grades)
            {
                List.Elements("Year").First(grade => grade.Element("Name").Value.Equals(item.GradeName))
                    .Add(new XElement("Group", new XElement("Name", item.ClassName)));
            }

            var groups = from g in dB.Tt_ClassGroup
                         join c in dB.School_Lookup_Class on g.classId equals c.ClassID
                         select new { c.ClassName, g.groupName };

            // Creates the different subgroups in eacht group
            foreach (var item in groups)
            {
                var group = List.Elements("Year").Elements("Group").Where(g => g.Element("Name").Value.Equals(item.ClassName));

                if (group.Count() > 0)
                {
                    group.First().Add(new XElement("Subgroup",
                                         new XElement("Name", item.groupName)));
                }
            }
        }

        public static Dictionary<int, StudentSet> GetYears(DataModel model)
        {

            var grades = new Dictionary<int, StudentSet>();

            // Loop over all grades
            foreach (var grade in model.School_Lookup_Grade.Where(grade => grade.IsActive == true))
            {
                var resultGrade = new StudentSet
                {
                    Id = grade.GradeID,
                    Name = grade.GradeName
                };

                // Add groups for each grade
                var groups = from c in model.School_Lookup_Class
                    join g in model.School_Lookup_Grade on c.GradeID equals g.GradeID
                    where c.IsActive == true
                    select new { c.ClassID, c.ClassName };

                foreach (var group in groups)
                {

                    var resultGroup = new Group
                    {
                        Id = group.ClassID,
                        Name = group.ClassName
                    };

                    // Add subgroups for each grade
                    var subGroups = from g in model.Tt_ClassGroup
                        join c in model.School_Lookup_Class on g.classId equals c.ClassID
                        select new { g.Id, g.groupName };

                    foreach (var subGroup in subGroups)
                    {
                        resultGroup.SubGroups.Add(subGroup.Id, new SubGroup
                        {
                            Id = subGroup.Id,
                            Name = subGroup.groupName
                        });
                    }

                    resultGrade.Groups.Add(group.ClassID, resultGroup);
                }

                grades.Add(grade.GradeID, resultGrade);
            }

            return grades;
        }
    }
}
