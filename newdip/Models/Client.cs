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
		public List<Note> Notes { get; set; }
		public List<FRoom> FRooms { get; set; }

		public Client(string login, string name, string password):this()
		{
			Login = login;
			Name = name;
			Password = password;

		}
		public Client()
		{
			Notes=new List<Note>();

			FRooms = new List<FRoom>();

		}
	}
}