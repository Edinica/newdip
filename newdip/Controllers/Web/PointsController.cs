using System;
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
    public class PointsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Points
        public IQueryable<Point> GetPoints()
        {
            return db.Points;
        }

        // GET: api/Points?level&id
        [ResponseType(typeof(Point))]
        public IHttpActionResult GetPoint(int level, int id)
        {
            var points = db.Floors.
                Include(x => x.Points).
                FirstOrDefault(x => x.BuildingId == id && x.Level == level).
                Points.ToList();
            List<Point> listpoints = new List<Point>();
            foreach (var element in points) 
            {
                    Point point = new Point();
                    point.X = element.X;
                    point.Y = element.Y;
                    point.IsWaypoint = element.IsWaypoint;
                listpoints.Add(point);
             
            }

            if (listpoints == null) { return null; }
            //List<Point> floor = points.ToList();
            //if (floor == null)
            //{
            //    return NotFound();
            //}
            //wwww el = new wwww();
            //el.a = 1; el.b = 2; el.c = 3; el.chislo = 123;
            //var ww = JsonConvert.SerializeObject(el);
            //List<Edge> edgeMs = new List<Edge>();
            //foreach (var elem in floor)
            //{
            //    var edge = new Edge();
            //    for (int i = 0; i < elem.EdgesOut.Count(); i++)
            //    {
            //        edge.PointFrom = new Point();
            //        edge.PointFrom.X = elem.X;
            //        edge.PointFrom.Y = elem.Y;
            //        edge.PointTo = new Point();
            //        edge.PointTo.X = elem.EdgesOut[i].PointTo.X;
            //        edge.PointTo.Y = elem.EdgesOut[i].PointTo.Y;
            //        edgeMs.Add(edge);
            //    }
            //}

            return Ok(listpoints);
        }

        // PUT: api/Points/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPoint(int id, Point point)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != point.Id)
            {
                return BadRequest();
            }

            db.Entry(point).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointExists(id))
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

        // POST: api/Points
        [ResponseType(typeof(Point))]
        public IHttpActionResult PostPoint(Point point)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Points.Add(point);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = point.Id }, point);
        }

        // DELETE: api/Points/5
        [ResponseType(typeof(Point))]
        public IHttpActionResult DeletePoint(int id)
        {
            Point point = db.Points.Find(id);
            if (point == null)
            {
                return NotFound();
            }

            db.Points.Remove(point);
            db.SaveChanges();

            return Ok(point);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PointExists(int id)
        {
            return db.Points.Count(e => e.Id == id) > 0;
        }
    }
}