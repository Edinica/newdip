using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Point
	{
		[Key]
		public int Id { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public bool IsMt { get; set; }
		public int? FloorId { get; set; }
		public Floor Floor { get; set; }
		public int? RoomId { get; set; }
		public Room Room { get; set; }
		public List<Edge>Edges { get; set; }

		public Point()
		{
			Edges = new List<Edge>();
		}
		public Point(bool value=false) 
		{
			Edges = new List<Edge>();
			if(!value)IsMt = false;
		}

	}
}