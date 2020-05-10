using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Room 
	{
		[Key]
		public int Id { get; set; }
		public int? FloorId { get; set; }
		public Floor Floor { get; set; }
		public List<Point>Points { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Timetable { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public List<Note> Notes { get; set; }
		public List<Worker> Workers { get; set; }
	}
}