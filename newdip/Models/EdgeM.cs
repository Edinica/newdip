using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class EdgeM 
	{
		[Key]
		public int Id { get; set; }
		public double Weight { get;  set; }
		public int? PointFromId { get; set; }
		public int? PointToId { get; set; }
		public PointM PointFrom { get; set; }
		public PointM PointTo { get; set; }
		public EdgeM() { }

		public EdgeM(double weight, int? pointFirId = null, int? pointSecId = null, int edgeId = 0)
		{
			Id = edgeId;
			Weight = weight;
			PointFromId = pointFirId;
			PointToId = pointSecId;
		}
	}
}