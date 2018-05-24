using Timetabling.DB;
using System.Linq;
using System.Xml.Linq;

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
		public YearsList(DataModel _dB) : base(_dB)
		{
			SetListElement("Students_List");
		}
      /// <summary>
      /// Create grades, with corresponding groups and subgroups
      /// </summary>
		public override void Create()
		{
			//Creates the different grades
			var query = dB.School_Lookup_Grade.Where(grade => grade.IsActive == true).Select(grade => grade.GradeName);
			foreach (var item in query)
			{
				list.Add(new XElement("Year", new XElement("Name", item)));
			}

			var grades = from c in dB.School_Lookup_Class
						 join grade in dB.School_Lookup_Grade on c.GradeID equals grade.GradeID
						 where c.IsActive == true
						 select new { c.ClassName, grade.GradeName };
           
			//Creates the different groups in a grade
			foreach (var item in grades)
			{
				list.Elements("Year").First(grade => grade.Element("Name").Value.Equals(item.GradeName)).
					Add(new XElement("Group",
									 new XElement("Name", item.ClassName)));
			}

			var groups = from g in dB.Tt_ClassGroup
						 join c in dB.School_Lookup_Class on g.classId equals c.ClassID

						 select new { c.ClassName, g.groupName };
           
			//Creates the different subgroups in eacht group
			foreach (var item in groups)
            {
				list.Elements("Year").Elements("Group").First(g => g.Element("Name").Value.Equals(item.ClassName)).
				    Add(new XElement("Subgroup",
				                     new XElement("Name", item.groupName)));
            }
		}
	}
}
