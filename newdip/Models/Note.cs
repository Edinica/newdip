using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Note
	{
		[Key]
		public int NoteId { get; set; }
		public string Text { get; set; }
		public string Date { get; set; }
		public string RoomName { get; set; } // что бы знать если не загружено здание
		public string Building { get; set; } // нужна для прототы выборки заметок по зданию
		public string UserName { get; set; } // для публичных заметок, что бы знать чья она
		public int? RoomId { get; set; }
		public Room Room { get; set; }
		public int? ClientId { get; set; }
		public Client Client { get; set; }
		public bool IsPublic { get; set; }
	}
}