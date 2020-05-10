using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newdip.Models
{
	public class FRoom
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Details { get; set; }
		public int? ClientId { get; set; }
		public Client client { get; set; }
		
	}
}