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

namespace newdip.Controllers.Web
{
    public class PointMsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PointMs
        public List<PointM> GetPoints()
        {
            return db.Points.ToList();
        }
        [HttpGet]
        [Route("api/PointMs/FloorPoints")]
        public IHttpActionResult FloorPoints(int level, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var points = db.Floors.
               Include(x => x.Points).
               FirstOrDefault(x => x.BuildingId == id && x.Level == level).
               Points.ToList();
           List<PointM> listpoints = new List<PointM>();
           foreach (var element in points) 
           {
                   PointM point = new PointM();
                   point.X = element.X;
                   point.Y = element.Y;
                   point.IsWaypoint = element.IsWaypoint;
               listpoints.Add(point);
            
           }
           
           if (listpoints == null) { return null; }
            return Ok(listpoints);
        }
        // GET: api/PointMs/5
        [ResponseType(typeof(PointM))]
        public IHttpActionResult GetPointM(int id)
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
            {
                if (pum.EdgesIn.Count != 0) pum.EdgesIn.Clear();
                if (pum.EdgesOut.Count != 0) pum.EdgesOut.Clear();
                if (pum.Floor != null) pum.Floor = null ;
                if (pum.Room != null) pum.Room = null;
            }

            return Ok(points);
        }

        // PUT: api/PointMs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPointM(int id, PointM pointM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pointM.Id)
            {
                return BadRequest();
            }

            db.Entry(pointM).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointMExists(id))
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

        // POST: api/PointMs
        [ResponseType(typeof(PointM))]
        public IHttpActionResult PostPointM(PointM pointM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Points.Add(pointM);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pointM.Id }, pointM);
        }

        // DELETE: api/PointMs/5
        [ResponseType(typeof(PointM))]
        public IHttpActionResult DeletePointM(int id)
        {
            PointM pointM = db.Points.Find(id);
            if (pointM == null)
            {
                return NotFound();
            }

            db.Points.Remove(pointM);
            db.SaveChanges();

            return Ok(pointM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PointMExists(int id)
        {
            return db.Points.Count(e => e.Id == id) > 0;
        }
    }
}