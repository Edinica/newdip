﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using newdip.Models;

namespace newdip.Controllers
{
    public class FloorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public class elem
        {
            public int id { get; set; }
            public int level { get; set; }
        }

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
        public ActionResult Create(int? id)
        {
            ViewBag.BuildingId = db.Buildings.FirstOrDefault(x=>x.BuildingId==id).BuildingId;
            ViewBag.Name = db.Buildings.FirstOrDefault(x => x.BuildingId == id).Name;
            return View();
        }

        // POST: Floors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
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
                    return RedirectToAction("Index", "Buildings");
                }
                if (!isexist && !alone)
                {
                    Floor newfloor = new Floor();
                    newfloor.BuildingId = floor.BuildingId;
                    newfloor.Level = floor.Level;
                    db.Floors.Add(newfloor);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Buildings");
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
        [System.Web.Http.HttpPost]
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
        public ActionResult Delete(int? id,int? level)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Floor floor = db.Floors.Where(xx=>xx.BuildingId==id&&xx.Level==level).FirstOrDefault();
            if (floor == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuID = floor.BuildingId;
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
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed([FromBody]elem floor)
        {
            Floor etazh = db.Floors.Include(x => x.Points).Include(x => x.Rooms).FirstOrDefault(x => x.BuildingId == floor.id && x.Level == floor.level);//one
            List<PointM> points = db.Points.Include(x => x.EdgesIn).Include(x => x.EdgesOut).Where(x => x.FloorId == etazh.FloorId).ToList();
            List<Room> rooms = db.Rooms.Include(obj => obj.Points).Include(x => x.Workers).Include(x => x.Notes).Where(obj => obj.FloorId == etazh.FloorId).ToList();


            foreach (var element in etazh.Rooms)
            {
                element.Workers.Clear();
                //element.Notes.Clear();
                //element.Points.Clear();
            }
            //db.SaveChanges();

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < points[i].EdgesIn.Count(); j++)
                {
                    db.Edges.Remove(points[i].EdgesIn[j]);
                }
                for (int j = 0; j < points[i].EdgesOut.Count(); j++)
                {
                    db.Edges.Remove(points[i].EdgesOut[j]);
                }
                db.Points.Remove(points[i]);
            }//-edges&&points
            db.SaveChanges();

            for (int i = 0; i < rooms.Count; i++)
            {
                for (int j = 0; j < rooms[i].Notes.Count(); j++)
                {
                    db.Notes.Remove(rooms[i].Notes[j]);
                }
                db.Rooms.Remove(rooms[i]);
            }//-notes&&rooms
            db.SaveChanges();
            db.Floors.Remove(etazh);
            db.SaveChanges();
            return RedirectToAction("Index", "Buildings", null) ;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void CopyFloor([FromBody] Floor floor)
        {
            var floors = db.Floors.Where(x => x.BuildingId == floor.BuildingId).ToList();//list
            Floor etazh = db.Floors.Include(x=>x.Points).Include(x=>x.Rooms).FirstOrDefault(x => x.BuildingId == floor.BuildingId && x.Level == floor.Level);//one
            List<PointM> points = db.Points.Include(x=>x.EdgesIn).Include(x => x.EdgesOut).Where(x => x.FloorId == etazh.FloorId).ToList();
            List<Room> rooms = db.Rooms.Include(obj => obj.Points).Where(obj => obj.FloorId == etazh.FloorId).ToList();
            List<PointM> newpoints = new List<PointM>();
            List<EdgeM> newedges = new List<EdgeM>();
            List<Room> newrooms = new List<Room>();
            Floor newfloor = new Floor(floors.Last().Level+1,etazh.BuildingId);
            db.Floors.Add(newfloor);
            db.SaveChanges();
            floors = db.Floors.Where(x => x.BuildingId == floor.BuildingId).ToList();//list
            foreach (var obj in points) 
            {
                PointM point = new PointM();
                point.X = obj.X;
                point.Y = obj.Y;
                point.IsWaypoint = obj.IsWaypoint;
                point.FloorId = newfloor.FloorId;
                newpoints.Add(point);
                db.Points.Add(point);
                db.SaveChanges();

            }
            for (int i=0;i<points.Count;i++) 
            {
                foreach (var param in points[i].EdgesOut)
                {
                    EdgeM edge = new EdgeM();
                    edge.PointFromId = newpoints[i].Id;
                    edge.Weight = param.Weight;
                    edge.PointToId = newpoints[points.IndexOf(param.PointTo)].Id;
                    newedges.Add(edge);
                    db.Edges.Add(edge);
                    db.SaveChanges();
                }            
            }
            //foreach (var obj in rooms)
            //{
            //    Room room = new Room();
            //    room.FloorId = newfloor.FloorId;
            //    newrooms.Add(room);
            //    db.Rooms.Add(room);
            //    db.SaveChanges();

            //}
            //if(rooms.Count!=0)
            //for (int i=0;i<points.Count;i++)
            //{
            //    if (points[i].RoomId != null)
            //    {

            //        newpoints[i].RoomId = newrooms[rooms.IndexOf(rooms.First(x => x.RoomId == points[i].RoomId))].RoomId;
            //    }
            //}
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void ClearFloor([FromBody] Floor floor)
        {
            Floor etazh = db.Floors.Include(x => x.Points).Include(x => x.Rooms).FirstOrDefault(x => x.BuildingId == floor.BuildingId && x.Level == floor.Level);//one
            List<PointM> points = db.Points.Include(x => x.EdgesIn).Include(x => x.EdgesOut).Where(x => x.FloorId == etazh.FloorId).ToList();
            List<Room> rooms = db.Rooms.Include(obj => obj.Points).Include(x=>x.Workers).Include(x=>x.Notes).Where(obj => obj.FloorId == etazh.FloorId).ToList();


            foreach (var element in etazh.Rooms) 
            {
                element.Workers.Clear();
                //element.Notes.Clear();
                //element.Points.Clear();
            }
            //db.SaveChanges();
            
            for (int i=0;i<points.Count;i++)
            {
                for (int j=0;j<points[i].EdgesIn.Count();j++) 
                {
                    db.Edges.Remove(points[i].EdgesIn[j]);
                }
                for (int j = 0; j < points[i].EdgesOut.Count(); j++)
                {
                    db.Edges.Remove(points[i].EdgesOut[j]);
                }
                db.Points.Remove(points[i]);
            }//-edges&&points
            db.SaveChanges();

            for (int i = 0; i < rooms.Count; i++)
            {
                for (int j = 0; j < rooms[i].Notes.Count(); j++)
                {
                    db.Notes.Remove(rooms[i].Notes[j]);
                }
                db.Rooms.Remove(rooms[i]);
            }//-notes&&rooms
            db.SaveChanges();
        }
    }
}
