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
		public string ClientName { get; set; } // для публичных заметок, что бы знать чья она
		public int? RoomId { get; set; }
		public Room Room { get; set; }
		public int? ClientId { get; set; }
		public Client Client { get; set; }
		public bool isPublic { get; set; }
	public Note(string text, string roomname,
				   string building = "", bool acsess = false,
				   int noteid = 0, int? roomid = null,
				   int? clientid = null, string clientname = "")
	{
		NoteId = noteid;

		Text = text;

		Date = System.DateTime.Now.ToString("dd/MM/yyyy");

		isPublic = acsess;

		RoomId = roomid;
		ClientId = clientid;

		Building = building;
		RoomName = roomname;
		ClientName = clientname;
	}

		public Note()
		{
		}
	}
}