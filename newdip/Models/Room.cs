using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Room 
	{
		[Key]
		public int RoomId { get; set; }
		public int? FloorId { get; set; }
		public Floor Floor { get; set; }
		public List<PointM>Points { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Timetable { get; set; }
		public string Phone { get; set; }
		public string Site { get; set; }
		public List<Note> Notes { get; set; }
		public List<Worker> Workers { get; set; }
		public Room() 
		{
			Points = new List<PointM>();
			Notes = new List<Note>();
			Workers = new List<Worker>();
		}

		public Room(string name, string description, string timetable, string phone, string site):this()
		{
			Name = name;
			Description = description;
			Timetable = timetable;
			Phone = phone;
			Site = site;
		}
		public Room(string name, string description = null,
				   string timetable = null,
				   string phone = null, string site = null,
				   int floorid = 0, int roomid = 0) : this()
		{
			RoomId = roomid;

			Name = name;
			FloorId = floorid;
			Description = description;

			Timetable = timetable;
			Phone = phone;
			Site = site;
		}
	}
}