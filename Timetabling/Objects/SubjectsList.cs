using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;

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
        public override XElement Create()
        {
            var query = dB.Subject_MasterData_Subject.Where(subject => subject.IsActive == true)
                          .Select(subject => new { subject.SubjectID });

            var query2 = dB.Subject_SubjectGrade.Select(coll => coll.CollectionID).Distinct();

            List<int?> check = new List<int?>();
            foreach (var subject in query)
            {
                List.Add(new XElement("Subject", new XElement("Name", subject.SubjectID)));
            }
            query2.ToList().ForEach( item => List.Add(new XElement("Subject", new XElement("Name", "coll" + item))));
       
                  
            return List;
        }

    }
}