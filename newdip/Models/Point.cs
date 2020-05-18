using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Point
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
		public virtual List<Edge> EdgesIn { get; set; }
		public virtual List<Edge> EdgesOut { get; set; }

		public Point()
		{
			EdgesIn = new List<Edge>();
			EdgesOut = new List<Edge>();
		}
		public Point(double x, double y,
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