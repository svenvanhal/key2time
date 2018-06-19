using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;
namespace Timetabling.Objects

{
    /// <summary>
    /// Rooms list.
    /// </summary>
    public class RoomsList : AbstractList
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.RoomsList"/> class.
        /// </summary>
        /// <param name="_db">Db.</param>
        public RoomsList(DataModel _db) : base(_db) => SetListElement("Rooms_List");

        /// <summary>
        /// Creates the Room elements from the datamodel
        /// </summary>
        public override XElement Create()
        {
            var query = dB.Buildings.Where(room => room.IsActive == true)
                          .Select(room => room.Id);

            foreach (var room in query)
            {
                List.Add(new XElement("Room", new XElement("Name", room)));
            }

            return List;
        }

    }
}
