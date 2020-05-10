using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Building 
	{ 
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }
		public string Addrees { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public string TimeTable { get; set; }
		public List<Floor>Floors { get; set; }
		/*public Building()
		{
			Floors = new List<Floor>();
		}*/

	}
}