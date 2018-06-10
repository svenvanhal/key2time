using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;
using Timetabling.Resources;

namespace Timetabling.Objects
{
    /// <summary>
    /// Subjects list.
    /// </summary>
    public class SubjectsList : AbstractList
    {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.SubjectsList"/> class.
        /// </summary>
        /// <param name="_dB">Database Model.</param>
        public SubjectsList(DataModel _dB) : base(_dB) => SetListElement("Subjects_List");

        /// <summary>
        /// Create the subjects elements list.
        /// </summary>
        public override void Create()
        {
            var query = dB.Subject_MasterData_Subject.Where(subject => subject.IsActive == true)
                          .Select(subject => subject.SubjectID);

            foreach (var subject in query)
            {
                List.Add(new XElement("Subject", new XElement("Name", subject)));
            }
        }

        public static Dictionary<int, Subject> GetSubjects(DataModel model)
        {

            var subjects = new Dictionary<int, Subject>();

            var query = model.Subject_MasterData_Subject.Where(subject => subject.IsActive == true);

            foreach (var subject in query)
            {
                subjects.Add(subject.SubjectID, new Subject
                {
                    Id = subject.SubjectID,
                    Name = subject.SubjectName
                });
            }

            return subjects;
        }

    }
}