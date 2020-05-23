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
        public IQueryable<PointM> GetPoints()
        {
            return db.Points;
        }

        // GET: api/Points?level&id
        [ResponseType(typeof(PointM))]
        public IHttpActionResult GetPoint(
            //int level, 
            int id)
        {
            //var points = db.Floors.
            //    Include(x => x.Points).
            //    FirstOrDefault(x => x.BuildingId == id && x.Level == level).
            //    Points.ToList();
            //List<PointM> listpoints = new List<PointM>();
            //foreach (var element in points) 
            //{
            //        PointM point = new PointM();
            //        point.X = element.X;
            //        point.Y = element.Y;
            //        point.IsWaypoint = element.IsWaypoint;
            //    listpoints.Add(point);
            // 
            //}
            //
            //if (listpoints == null) { return null; }
            List<Floor> Floor = db.Floors.Where(xx => xx.BuildingId == id).ToList();
            List<PointM> points = new List<PointM>();
            foreach (var element in Floor)
            {
                List<PointM> temp = db.Points.Where(x => x.FloorId == element.FloorId).ToList();
                foreach (var pum in temp)
                    points.Add(pum);
            }
            foreach (var pum in points)
                pum.Floor = null;
            return Ok(points);
        }

        // PUT: api/Points/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPoint(int id, PointM point)
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
        [ResponseType(typeof(PointM))]
        public IHttpActionResult PostPoint(PointM point)
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
        [ResponseType(typeof(PointM))]
        public IHttpActionResult DeletePoint(int id)
        {
            PointM point = db.Points.Find(id);
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