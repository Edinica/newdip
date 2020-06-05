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
		public List<PointM>Points { get; set; }
		public List<Room>Rooms { get; set; }
		public Floor()
		{
			Points = new List<PointM>();
			Rooms = new List<Room>();
		}
		public Floor(int level, int? bid, Building building ) 
		{
			this.Level = level;
			this.BuildingId = bid;
			this.Building = building;
			Points = new List<PointM>();
			Rooms = new List<Room>();
		}
		public Floor(int level, int floorid = 0, int buildingid = 0) : this()
		{
			FloorId = floorid;

			this.Level = level;

			this.BuildingId = buildingid;
		}

		public Floor(int level, int? buildingId):this()
		{
			Level = level;
			BuildingId = buildingId;
		}

		public Floor(int level,Floor floor)
		{

			Level = level;
			BuildingId = floor.BuildingId;
			Building = floor.Building;
			Points = floor.Points;
			Rooms = floor.Rooms;
		}
	}
}