using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Client 
	{
		[Key]
		public int Id { get; set; }
		public string Login { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public List<Note>Notes { get; set; }
		public List<FRoom>FRooms { get; set; }
	}
}