using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Worker 
	{
		public Worker()
		{
		}

		public Worker(string firstName, string secondName, string lastName)
		{
			FirstName = firstName;
			SecondName = secondName;
			LastName = lastName;
		}

		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string LastName { get; set; }
		public string Status { get; set; }
		public string Details { get; set; }
		public string Email { get; set; }
		public string Site { get; set; }
		public string Phone { get; set; }
		public int? RoomId { get; set; }
		public Room Room { get; set; }

	}
}