using System;
using Timetable.timetable.DB;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Timetable.timetable.Objects
{
	public class YearsList : AbstractList
	{
		/// <summary>
        /// Initializes a new instance of the <see cref="T:Timetable.timetable.Objects.YearsList"/> class.
        /// </summary>
        /// <param name="_dB">Database Model</param>
		public YearsList(DataModel _dB) : base(_dB)
		{
			SetListElement("Students_List");
		}
      /// <summary>
      /// Create grades, with corresponding groups and subgroups
      /// </summary>
		public override void Create()
		{
			var query = dB.School_Lookup_Grade.Where(grade => grade.IsActive == true).Select(grade => grade.GradeName);
			foreach (var item in query)
			{
				list.Add(new XElement("Year", new XElement("Name", item)));
			}

			var grades = from c in dB.School_Lookup_Class
						 join grade in dB.School_Lookup_Grade on c.GradeID equals grade.GradeID
						 where c.IsActive == true
						 select new { c.ClassName, grade.GradeName };

			foreach (var item in grades)
			{
				list.Elements("Year").Where(grade => grade.Element("Name").Value.Equals(item.GradeName)).First().
					Add(new XElement("Group",
									 new XElement("Name", item.ClassName)));
			}

			var groups = from g in dB.tt_ClassGroup
						 join c in dB.School_Lookup_Class on g.classId equals c.ClassID

						 select new { c.ClassName, g.groupName };
            
			foreach (var item in groups)
            {
				list.Elements("Year").Elements("Group").Where(g => g.Element("Name").Value.Equals(item.ClassName)).First().
				    Add(new XElement("Subgroup",
				                     new XElement("Name", item.groupName)));
            }
		}
	}
}
