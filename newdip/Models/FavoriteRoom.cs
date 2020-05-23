using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newdip.Models
{
	public class FavoriteRoom
	{
		[Key]
		public int FavoriteRoomId { get; set; }
		public string Building { get; set; }
		public string Name { get; set; }

		public string Details { get; set; }
		public int? ClientId { get; set; }
		public Client Client { get; set; }
		
	}
}