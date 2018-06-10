using System.Collections.Generic;

namespace Timetabling.Resources
{

    public class StudentSet : Resource
    {

        public int Id { get; set; }
        public Dictionary<int, Group> Groups { get; set; } = new Dictionary<int, Group>();

    }
}
