namespace Timetabling.Resources
{

    public class Activity : Resource
    {

        public int Id { get; set; }
        public int GroupId { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public Group Students { get; set; }
        public int Duration { get; set; }
        public int Lessons { get; set; }

        public int TotalDuration => Duration * Lessons;

    }
}
