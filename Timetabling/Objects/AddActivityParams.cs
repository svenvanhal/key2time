using System;
using System.Collections.Generic;

namespace Timetabling.Objects
{
    public class AddActivityParams
    {
        public int SubjectId { get; set; }
        public List<string> StudentsList { get; set; }
        public List<int> TeachersList { get; set;  }
        public bool IsCollection { get; set; }
        public int NumberOfLessonsPerDay { get; set; }
        public int NumberOfLessonsPerWeek { get; set; }
        public int? CollectionID { get; set; }
        public string GradeName { get; set; }
    }
}
