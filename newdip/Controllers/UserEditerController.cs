using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using newdip;
using newdip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Geo.Controllers
{
    public class UserEditerController : Controller
    {
        // GET: UserEditer
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {


            //ApplicationUserManager userManager = new ApplicationUserManager();
            //userManager.AddToRole
            return View(db.Users.Where(xx => xx.Roles.Count == 1).ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult MakeAdmin(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("MakeAdmin")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult MakeAdminConfirmed(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            userManager.AddToRole(user.Id, "Admin");
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}