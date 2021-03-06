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
    public class RoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.Floor);
            return View(rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.FloorId = new SelectList(db.Floors, "FloorId", "FloorId");
            return View();
        }

        // POST: Rooms/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomId,FloorId,Name,Description,Timetable,Phone,Site")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FloorId = new SelectList(db.Floors, "FloorId", "FloorId", room.FloorId);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.FloorId = new SelectList(db.Floors, "FloorId", "FloorId", room.FloorId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public void Edit([Bind(Include = "RoomId,FloorId,Name,Description,Timetable,Phone,Site")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                var xxx = db.Rooms.Where(xx => xx.RoomId == room.RoomId).FirstOrDefault();
                //return RedirectToAction("Index");
            }
            //ViewBag.FloorId = new SelectList(db.Floors, "FloorId", "FloorId", room.FloorId);
            //return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var point = db.Points.FirstOrDefault(x => x.RoomId == id);
            List<EdgeM> edges = db.Edges.Where(x => x.PointFromId == point.Id || x.PointToId == point.Id).ToList();
            //int id = Convert.ToInt32(point.Id);
            for (int i = 0; i < edges.Count; i++)
            {
                db.Edges.Remove(edges[i]);
                db.SaveChanges();
            }
            PointM moved = db.Points.Where(xx => xx.Id == point.Id).FirstOrDefault();//этаж просмотр
            ///создание и добавление первой точки
            db.Points.Remove(moved);
            db.SaveChanges();
            Room room = db.Rooms.Find(id);
            room.Workers = null;
            for (int i = 0; i < room.Notes.Count();i++) 
            {
                db.Notes.Remove(room.Notes[i]);
            }
            db.SaveChanges();
            db.Rooms.Remove(room);
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
