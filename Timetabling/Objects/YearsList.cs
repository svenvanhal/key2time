using Timetabling.DB;
using System.Linq;
using System.Xml.Linq;
using System;
using System.Collections.Generic;


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
        public override XElement Create()
        {
            var query = from g in dB.School_Lookup_Grade
                        where g.IsActive == true
                        join c in dB.School_Lookup_Class on g.GradeID equals c.GradeID into t
                        from c in t.DefaultIfEmpty()
                        where c.IsActive == true
                        join gr in dB.Tt_ClassGroup on c.ClassID equals gr.classId into tt
                        from gr in tt.DefaultIfEmpty()
                        select new { g.GradeName, c.ClassName, gr.groupName };

            var grades = query.Select(item => item.GradeName).Distinct().ToList();
            var classes = query.Where(item => item.GradeName != null && item.ClassName != null).Select(item => new { item.GradeName, item.ClassName }).Distinct().ToList();
            var groups = query.Where(item => item.groupName != null && item.ClassName != null).Select(item => new { item.ClassName, item.groupName }).Distinct().ToList();

            AddGrades(grades);
            AddClasses(classes);
            AddGroups(groups);
        }

        private void AddGrades(List<string> grades)
        {
            foreach (var item in grades)
            {
                List.Add(new XElement("Year", new XElement("Name", item)));
            }
        }

        private void AddClasses(dynamic classes)
        {

            // Creates the different groups in a grade
            foreach (var item in classes)
            {
                List.Elements("Year").First(grade => grade.Element("Name").Value.Equals(item.GradeName))
                    .Add(new XElement("Group", new XElement("Name", item.ClassName)));
            }
        }

        private void AddGroups(dynamic groups)
        {

            // Creates the different subgroups in eacht group
            foreach (var item in groups)
            {
                var group = List.Elements("Year").Elements("Group").Where(g => g.Element("Name").Value.Equals(item.ClassName));

                if (group.Any())
                {
                    group.First().Add(new XElement("Subgroup",
                                         new XElement("Name", item.groupName)));
                }
            }

            return List;
        }
    }
}
