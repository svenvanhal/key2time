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
                        join c in dB.School_Lookup_Class on g.GradeID equals c.GradeId into t
                        from c in t.DefaultIfEmpty()
                        select new { g.GradeName, c.ClassName };

            var grades = query.Select(item => item.GradeName).Distinct().ToList();
            var classes = query.Where(item => item.GradeName != null && item.ClassName != null).Select(item => new { item.GradeName, item.ClassName }).Distinct().ToList();

            AddGrades(grades);
            AddClasses(classes);

            return List;
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
    }
}
