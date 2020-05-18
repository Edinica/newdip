using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Floor 
	{
		[Key]
		public int FloorId { get; set; }
		public int Level { get; set; }
		public int? BuildingId { get; set; }
		public Building Building { get; set; }
		public List<Point>Points { get; set; }
		public List<Room>Rooms { get; set; }
		public Floor()
		{
			Points = new List<Point>();
			Rooms = new List<Room>();
		}
		public Floor(int level, int? bid, Building building ) 
		{
			this.Level = level;
			this.BuildingId = bid;
			this.Building = building;
			Points = new List<Point>();
			Rooms = new List<Room>();
		}
	}
}