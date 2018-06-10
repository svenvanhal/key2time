using System.Collections.Generic;

namespace Timetabling.Resources
{


    public class Group
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<int, SubGroup> SubGroups { get; set; } = new Dictionary<int, SubGroup>();

    }
}
