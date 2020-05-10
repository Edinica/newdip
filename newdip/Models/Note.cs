using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Note
	{
		[Key]
		public int Id { get; set; }
		public string Text { get; set; }
		public string Date { get; set; }
		public int? RoomId { get; set; }
		public Room Room { get; set; }
		public int? ClientId { get; set; }
		public Client Client { get; set; }
		public bool IsPublic { get; set; }
	}
}