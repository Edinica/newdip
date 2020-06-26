using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using newdip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace newdip.Controllers
{
	//[RequireHttps]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			IList<string> roles = new List<string> { "Роль не определена" };
			ApplicationUserManager userManager = HttpContext.GetOwinContext()
													.GetUserManager<ApplicationUserManager>();
			ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
			if (user != null)
				roles = userManager.GetRoles(user.Id);
			return View(roles);
		}

		[Authorize]
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{

			ApplicationDbContext db = new ApplicationDbContext();

			return View(db.Users.Where(xx => xx.Roles.Count == 2).ToList());
		}
	}
}