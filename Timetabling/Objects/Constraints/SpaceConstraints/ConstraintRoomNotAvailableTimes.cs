using System;
using System.Xml.Linq;
using System.Linq;
using Timetabling.DB;
using System.Collections.Generic;

namespace Timetabling.Objects.Constraints.SpaceConstraints
{
	public class ConstraintRoomNotAvailableTimes : AbstractConstraint
	{

		int numberOfHours = 1;
		public int room { get; set; }
		public Days day { get; set; }
		public int hour { get; set; }

		public ConstraintRoomNotAvailableTimes()
		{
			SetElement("ConstraintRoomNotAvailableTimes");

		}
        
		public override XElement[] Create(DataModel dB)
		{
			var query = from tf in dB.Tt_TimeOff
						where tf.ItemType == 4
						join e in dB.School_BuildingsUnits on tf.ItemId equals e.ID
						select new { tf.day, tf.ItemId, tf.lessonIndex };

			var result = new List<XElement>();
			query.AsEnumerable().ToList().ForEach(item => result.Add(new ConstraintRoomNotAvailableTimes { room = item.ItemId, day = (Days)item.day, hour = item.lessonIndex }.ToXelement()));

			return result.ToArray();
		}

		public override XElement ToXelement()
		{
			SetWeight(weight);
			constraint.Add(new XElement("Room", room),
						   new XElement("Number_of_Not_Available_Times", numberOfHours),
						   new XElement("Not_Available_Time",
										new XElement("Day", day),
										new XElement("Hour", hour)));
			return constraint;
		}
	}
}
