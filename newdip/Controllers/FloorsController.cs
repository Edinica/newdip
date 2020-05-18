using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using newdip.Models;

namespace newdip.Controllers
{
    public class FloorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Floors
        public ActionResult Index()
        {
            var floors = db.Floors.Include(f => f.Building);
            return View(floors.ToList());
        }

        // GET: Floors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Floor floor = db.Floors.Find(id);
            if (floor == null)
            {
                return HttpNotFound();
            }
            return View(floor);
        }

        // GET: Floors/Create
        public ActionResult Create(int id)
        {
            ViewBag.BuildingId = db.Buildings.FirstOrDefault(x=>x.BuildingId==id).BuildingId;
            return View();
        }

        // POST: Floors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FloorId,Level,BuildingId")] Floor floor)
        {
            if (ModelState.IsValid)
            {
                //int xxx=ViewBag.BuildingId;
                Building building = db.Buildings.Include(x => x.Floors).FirstOrDefault(x => x.BuildingId == floor.BuildingId);
                bool isexist = false; bool alone = true;
                foreach (var element in building.Floors)
                {
                    if (element.Level == floor.Level) { isexist = true; break; }
                }
                foreach (var element in building.Floors)
                {
                    if (element.Level == floor.Level + 1 ||
                        element.Level == floor.Level - 1) { alone = false; break; }
                }
                if (building.Floors.Count == 0) 
                {
                    Floor newfloor = new Floor();
                    newfloor.BuildingId = floor.BuildingId;
                    newfloor.Level = floor.Level;
                    db.Floors.Add(newfloor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                if (!isexist && !alone)
                {
                    Floor newfloor = new Floor();
                    newfloor.BuildingId = floor.BuildingId;
                    newfloor.Level = floor.Level;
                    db.Floors.Add(newfloor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    if(isexist)
                        return RedirectToAction("WrongFloor");
                    if (alone) return RedirectToAction("Alone");
                }
            }

            ViewBag.BuildingId = new SelectList(db.Buildings, "Id", "Name", floor.BuildingId);
            return View(floor);
        }

        // GET: Floors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Floor floor = db.Floors.Find(id);
            if (floor == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "Id", "Name", floor.BuildingId);
            return View(floor);
        }

        // POST: Floors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FloorId,Level,BuildingId")] Floor floor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(floor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "Id", "Name", floor.BuildingId);
            return View(floor);
        }

        // GET: Floors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Floor floor = db.Floors.Find(id);
            if (floor == null)
            {
                return HttpNotFound();
            }
            return View(floor);
        }
        public ActionResult WrongFloor()
        {
            return View();
        }
        public ActionResult Alone()
        {
            return View();
        }

        // POST: Floors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Floor floor = db.Floors.Find(id);
            db.Floors.Remove(floor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
