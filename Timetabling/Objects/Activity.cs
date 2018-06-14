using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Timetabling.Objects
{
    public class Activity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        /// <value>The group identifier.</value>
        public int GroupId { get; set; }
        public List<int> Teachers { get; set; }
        public int Subject { get; set; }
        public List<string> Students { get; set; }
        public int Duration { get; set; }
        public int TotalDuration { get; set; }
        public int LessonGroupId { get; set; }
        public bool IsCollection { get; set; }
        public int CollectionId { get; set; } = -1;
        public int NumberLessonOfWeek { get; set; }
        public string CollectionString { get; set; } = "";

        public XElement ToXElement(){
           var element =  new XElement("Activity",
                                       new XElement("Id", Id),
                                       new XElement("Activity_Group_Id", GroupId),
                                       new XElement("Duration", Duration),
                                       new XElement("Total_Duration", TotalDuration));

            Students.ForEach(item => element.Add(new XElement("Students", item)));
            Teachers.ForEach(item => element.Add(new XElement("Teacher", item)));
            if (IsCollection)
                element.Add(new XElement("Subject","coll"+CollectionId));
            else
                element.Add(new XElement("Subject", Subject));
            
            return element;
        }

        public void SetCollection(int _CollectionId, string grade){
            this.CollectionId = _CollectionId;
            IsCollection = true;
            CollectionString = "coll" + CollectionId + "-" + grade;
        

        }
    }
}
