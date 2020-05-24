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
    public class EdgeMsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EdgeMs
        public List<EdgeM> GetEdges()
        {
            return db.Edges.ToList();
        }

        // GET: api/EdgeMs/5
        [ResponseType(typeof(EdgeM))]
        public IHttpActionResult GetEdgeM(int id)
        {
            List<Floor> Floor = db.Floors.Where(xx => xx.BuildingId == id).ToList();
            List<PointM> points = new List<PointM>();
            foreach (var element in Floor)
            {
                List<PointM> temp = db.Points.Where(x => x.FloorId == element.FloorId).ToList();
                foreach (var pum in temp)
                    points.Add(pum);
            }
            List<EdgeM> edges = new List<EdgeM>();
            foreach (var element in points)
            {
                List<EdgeM> temp = db.Edges.Where(x => x.PointToId == element.Id).ToList();
                foreach (var pum in temp)
                    edges.Add(pum);
            }
            foreach (var pum in edges)
            { pum.PointTo = null;
                pum.PointFrom = null;
            }
            return Ok(edges);
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