using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Edge 
	{
		[Key]
		public int Id { get; set; }
		public double Weight { get;  set; }
		public int? PointFromId { get; set; }
		public int? PointToId { get; set; }
		public Point PointFrom { get; set; }
		public Point PointTo { get; set; }
		public Edge() { }

		public Edge(double weight, int? pointFirId = null, int? pointSecId = null, int edgeId = 0)
		{
			Id = edgeId;
			Weight = weight;
			PointFromId = pointFirId;
			PointToId = pointSecId;
		}
	}
}