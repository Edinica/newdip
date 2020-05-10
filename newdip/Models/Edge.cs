using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Edge 
	{
		[Key]
		public int Id { get; set; }
		public int Weight { get; set; }
		public int? PointId { get; set; }
		public int? SPointId { get; set; }
		public Point Point { get; set; }
		public Point SPoint { get; set; }
	}
}