﻿using System;
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
    public class BuildingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Buildings
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Buildings.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult RenderList(int Id, int level)
        {
            var floor = db.Floors.Include(x => x.Points).FirstOrDefault(x => x.Level == level && x.BuildingId == Id);
            var points = floor.Points.ToList();
            return PartialView("PartialListPoints", points);

        }

        // GET: Buildings/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Include(x=>x.Floors).FirstOrDefault(x=>x.BuildingId==id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "BuildingId,Name,Addrees,Description,Site,TimeTable")] Building building)
        {
            if (ModelState.IsValid)
            {
                db.Buildings.Add(building);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(building);
        }

        // GET: Buildings/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        [Authorize]
        public ActionResult NF()
        {
            return View();
        }
        // POST: Buildings/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "BuildingId,Name,Addrees,Description,Site,TimeTable")] Building building)
        {
            if (ModelState.IsValid)
            {
                db.Entry(building).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(building);
        }

        public ActionResult Plan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Where(x => x.BuildingId == id).Include(x => x.Floors).FirstOrDefault();
            if (building == null)
            {
                return HttpNotFound();
            }
            //building.Floors = db.Floors.Where(x=>x.BuildingId==id).Include(x => x.Points);
            //return RedirectToAction("AddLevel", new { id = id }); ;
            //return RedirectToAction("Index");
            return View(
                building
                );
            
        }
        [Authorize]
        public ActionResult AddLevel(int? id)//Метод для добавления этажа к зданию
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Floors.Add(new Floor(db.Buildings.Include(x => x.Floors).FirstOrDefault(x=>x.BuildingId==id).Floors.Count+1,id, db.Buildings.FirstOrDefault(x => x.BuildingId == id)));
            db.SaveChanges();
            Building building = db.Buildings.Where(x => x.BuildingId == id).Include(x => x.Floors).FirstOrDefault();

            //db.Buildings.Where(x => x.Id == id).FirstOrDefault().Floors.Add(db.Floors.LastOrDefault(x=>x.BuildingId==id));
            //db.SaveChanges();
            if (building == null)
            {
                return HttpNotFound();
            }
            //building.Floors = db.Floors.Where(x=>x.BuildingId==id).Include(x => x.Points);
            //return RedirectToAction("Plan", new {id=id });
            return RedirectToAction("Index");
            //return View(
            //    building
            //    );

        }
        // GET: Buildings/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Building building = db.Buildings.Find(id);
            db.Buildings.Remove(building);
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
