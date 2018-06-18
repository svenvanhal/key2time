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

        /// <summary>
        /// Gets or sets the list of teachers.
        /// </summary>
        /// <value>The teachers.</value>
        public List<int> Teachers { get; set; }

        /// <summary>
        /// Gets or sets the subject. Is always one subject.
        /// </summary>
        /// <value>The subject.</value>
        public int Subject { get; set; }

        /// <summary>
        /// Gets or sets the list of students.
        /// </summary>
        /// <value>The students.</value>
        public Dictionary<string, int> Students { get; set; }

        /// <summary>
        /// Gets or sets the duration of this activity.
        /// </summary>
        /// <value>The duration.</value>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the total duration of all activities in the same group.
        /// </summary>
        /// <value>The total duration.</value>
        public int TotalDuration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Timetabling.Objects.Activity"/> is a collection.
        /// </summary>
        /// <value><c>true</c> if a collection; otherwise, <c>false</c>.</value>
        public bool IsCollection { get; set; }

        /// <summary>
        /// Gets or sets the collection identifier.
        /// </summary>
        /// <value>The collection identifier.</value>
        public int CollectionId { get; set; } = -1;

        /// <summary>
        /// Gets or sets which lesson of the group it is by the number of lesson of the week.
        /// </summary>
        /// <value>The number lesson of week.</value>
        public int NumberLessonOfWeek { get; set; }

        /// <summary>
        /// Gets or sets the collection string, created by SetCollection
        /// </summary>
        /// <value>The collection string.</value>
        public string CollectionString { get; set; } = "";

        /// <summary>
        /// Returns the XElement representation of activity
        /// </summary>
        /// <returns>The XE lement.</returns>
        public XElement ToXElement(){
           var element =  new XElement("Activity",
                                       new XElement("Id", Id),
                                       new XElement("Activity_Group_Id", GroupId),
                                       new XElement("Duration", Duration),
                                       new XElement("Total_Duration", TotalDuration));

            foreach(String item in Students.Keys){
                element.Add(new XElement("Students", item));
            }

            Teachers.ForEach(item => element.Add(new XElement("Teacher", item)));

            //if the activity is a collection, it makes a temporary subject name with prefix coll. 
            if (IsCollection)
            {
                element.Add(new XElement("Subject", "coll" + CollectionId));
            }
            else
            {
                element.Add(new XElement("Subject", Subject));
            }
            return element;
        }

        /// <summary>
        /// Sets a collection and creates the collectionstring, which is based on the collectionId and grade.
        /// </summary>
        /// <param name="_CollectionId">Collection identifier.</param>
        /// <param name="grade">Grade.</param>
        public void SetCollection(int _CollectionId, string grade){
            this.CollectionId = _CollectionId;
            IsCollection = true;
            CollectionString = "coll" + CollectionId + "-" + grade;
        

        }
    }
}
