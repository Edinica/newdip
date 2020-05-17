using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class PointM
	{
		[Key]
		public int Id { get; set; }
		public double X { get; set; }
		public double Y { get; set; }
		public bool IsWaypoint { get;  set; }
		public int? FloorId { get; set; }
		public Floor Floor { get; set; }
		public int? RoomId { get; set; }
		public Room Room { get; set; }
		public virtual List<EdgeM> EdgesIn { get; set; }
		public virtual List<EdgeM> EdgesOut { get; set; }

		public PointM()
		{
			EdgesIn = new List<EdgeM>();
			EdgesOut = new List<EdgeM>();
		}
		public PointM(double x, double y,
					  bool isWaypoint = false, int pointId = 0,
					  int? floorId = null, int? roomId = null)
		{
			Id = pointId;
			X = x;
			Y = y;
			IsWaypoint = isWaypoint;
			FloorId = floorId;
			RoomId = roomId;
		}

	}
}