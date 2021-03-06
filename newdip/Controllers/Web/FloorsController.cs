﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using newdip.Models;
using Newtonsoft.Json;

namespace newdip.Controllers.Web
{
    public class wwww 
    {
        public int chislo { get; set; }
        public int a { get; set; }
        public int b { get; set; }
        public int c { get; set; }
    }
    public class FloorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Floors
        public List<Floor> GetFloors()
        {
            return db.Floors.ToList() ;
        }
        [HttpGet]
        [Route("api/Floors/Edges")]
        [ResponseType(typeof(Floor))]
        public IHttpActionResult Edges(int level,int id)
        {

            List<Floor> Floors = db.Floors.Where(xx => xx.BuildingId == id).ToList();
            Floor floor = db.Floors.FirstOrDefault(xx => xx.BuildingId == id && xx.Level==level);
            List<PointM> points = new List<PointM>();
            foreach (var element in Floors)
            {
                List<PointM> temp = db.Points.Include(x=>x.EdgesIn).Include(x=>x.EdgesOut).Where(x => x.FloorId == element.FloorId).ToList();
                foreach (var pum in temp)
                    points.Add(pum);
            }
            List<EdgeM> edgeMs = new List<EdgeM>();
            foreach (var elem in points)
            {
                if (elem.FloorId == floor.FloorId)
                {
                    for (int i = 0; i < elem.EdgesOut.Count(); i++)
                    {
                        var edge = new EdgeM();
                        edge.PointFrom = new PointM();
                        edge.PointFrom.X = elem.X;
                        edge.PointFrom.Y = elem.Y;
                        edge.PointFrom.Id = elem.Id;
                        edge.PointFrom.IsWaypoint = elem.IsWaypoint;
                        edge.PointTo = new PointM();
                        edge.PointTo.X = elem.EdgesOut[i].PointTo.X;
                        edge.PointTo.Y = elem.EdgesOut[i].PointTo.Y;
                        edge.PointTo.Id=elem.EdgesOut[i].PointTo.Id;
                        edge.PointTo.IsWaypoint = elem.EdgesOut[i].PointTo.IsWaypoint;
                        edgeMs.Add(edge);
                    }
                }
            }
                return Ok(edgeMs);
                //Floor xxx = db.Floors.FirstOrDefault(x => x.BuildingId == id && (x.Level == level || x.Level == level));
                //List<PointM> points = db.Points.
                //    Include(x => x.EdgesIn).Include(x=>x.EdgesOut).
                //    Where(x => x.FloorId == xxx.FloorId).ToList();
                //if (points == null) { return null; }
                //List<EdgeM> edgeMs = new List<EdgeM>();
                //foreach (var elem in points)
                //{
                //    var edge = new EdgeM();
                //    for (int i = 0; i < elem.EdgesOut.Count(); i++)
                //    {
                //        edge.PointFrom = new PointM();
                //        edge.PointFrom.X = elem.X;
                //        edge.PointFrom.Y = elem.Y;
                //        edge.PointTo = new PointM();
                //        edge.PointTo.X = elem.EdgesOut[i].PointTo.X;
                //        edge.PointTo.Y = elem.EdgesOut[i].PointTo.Y;
                //        edgeMs.Add(edge);
                //    }
                //}
                //var json = JsonConvert.SerializeObject(edgeMs);
            //List<Floor> xxx = db.Floors.Where(x => x.BuildingId == id).ToList();
            //return Ok(xxx);
        }

        // GET: api/Floors/5
        [ResponseType(typeof(Floor))]
        public IHttpActionResult GetFloor(int id)
        {

            //var xxx = db.Floors.FirstOrDefault(x => x.BuildingId == id && x.Level == level);
            //var points = db.Floors.
            //    Include(x => x.Points).
            //    FirstOrDefault(x => x.BuildingId == id && x.Level == level).
            //    Points;
            //if (points == null) { return null; }
            //List<PointM> floor = points.ToList();
            //if (floor == null)
            //{
            //    return NotFound();
            //}
            //wwww el = new wwww();
            //el.a = 1;el.b = 2;el.c = 3;el.chislo = 123;
            //var ww = JsonConvert.SerializeObject(el);
            //List<EdgeM> edgeMs = new List<EdgeM>();
            //foreach (var elem in floor) 
            //{
            //    var edge = new EdgeM();
            //    for (int i = 0; i < elem.EdgesOut.Count(); i++)
            //    {
            //        edge.PointFrom = new PointM();
            //        edge.PointFrom.X = elem.X;
            //        edge.PointFrom.Y = elem.Y;
            //        edge.PointTo = new PointM();
            //        edge.PointTo.X = elem.EdgesOut[i].PointTo.X;
            //        edge.PointTo.Y = elem.EdgesOut[i].PointTo.Y;
            //        edgeMs.Add(edge); 
            //    }
            //}
            // var json = JsonConvert.SerializeObject(edgeMs);
            List<Floor> xxx = db.Floors.Where(x => x.BuildingId == id).ToList();
            return Ok(xxx);
        }

        // PUT: api/Floors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFloor(int id, Floor floor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != floor.FloorId)
            {
                return BadRequest();
            }

            db.Entry(floor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FloorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Floors
        [ResponseType(typeof(Floor))]
        public IHttpActionResult PostFloor(Floor floor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Floors.Add(floor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = floor.FloorId }, floor);
        }

        // DELETE: api/Floors/5
        [ResponseType(typeof(Floor))]
        public IHttpActionResult DeleteFloor(int id)
        {
            Floor floor = db.Floors.Find(id);
            if (floor == null)
            {
                return NotFound();
            }

            db.Floors.Remove(floor);
            db.SaveChanges();

            return Ok(floor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FloorExists(int id)
        {
            return db.Floors.Count(e => e.FloorId == id) > 0;
        }
    }
}