﻿using System;
using Timetable.timetable.DB;
using System.Linq;
using System.Xml.Linq;

namespace Timetable.timetable.Objects
{
	public class ActivitiesList : AbstractList
	{
		private int counter = 0;
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Timetable.timetable.Objects.ActivitiesList"/> class.
		/// </summary>
		/// <param name="_db">Database model</param>
		public ActivitiesList(DataModel _db) : base(_db)
		{
			SetListElement("Activities_List");
		}
		/// <summary>
		/// Create the list with Activity XElements
		/// </summary>
		public override void Create()
		{
			var query = from activity in dB.School_TeacherClass_Subjects
						join c in dB.School_Lookup_Class on activity.ClassID equals c.ClassID
						join s in dB.Subject_SubjectGrade on new { activity.SubjectID, c.GradeID } equals new { s.SubjectID, s.GradeID }
						select new { activity.TeacherID, activity.SubjectID, c.ClassName, activity.ID, s.NumberOfLlessonsPerWeek };
			foreach (var item in query)
			{
				int groupId = counter;

				for (int i = 0; i < item.NumberOfLlessonsPerWeek; i++)
				{
					list.Add(new XElement("Activity",
										  new XElement("Teacher", item.TeacherID),
										  new XElement("Subject", item.SubjectID),
										  new XElement("Students", item.ClassName), 
										  new XElement("Id", counter),
					                      new XElement("Activity_Group_Id", groupId), 

										  new XElement("Duration", '1'),
										  new XElement("Total_Duration", '1')
									)
								);

					counter++;
				}
			}

		}
	}


}
