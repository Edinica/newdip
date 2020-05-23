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
    public class WorkersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Workers
        public IQueryable<Worker> GetWorkers()
        {
            return db.Workers;
        }

        // GET: api/Workers/5
        [ResponseType(typeof(Worker))]
        public IHttpActionResult GetWorker(int id)
        {
            List<Floor> Floor = db.Floors.Where(xx => xx.BuildingId == id).ToList();
            List<Room> rooms = new List<Room>();
            //получаем все комнаты этажей
            foreach (var element in Floor)
            {
                List<Room> temp = db.Rooms.Where(x => x.FloorId == element.FloorId).ToList();
                foreach (var pum in temp)
                    rooms.Add(pum);
            }
            List<Worker> workers = new List<Worker>();
            //получаем все public заметки помещений
            foreach (var element in rooms)
            {
                List<Worker> temp = db.Workers.Where(x => x.RoomId == element.RoomId).ToList();
                foreach (var pum in temp)
                    workers.Add(pum);
            }
            foreach (var pum in workers)
                pum.Room = null;
            return Ok(workers);
        }

        // PUT: api/Workers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorker(int id, Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != worker.Id)
            {
                return BadRequest();
            }

            db.Entry(worker).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
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

        // POST: api/Workers
        [ResponseType(typeof(Worker))]
        public IHttpActionResult PostWorker(Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Workers.Add(worker);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = worker.Id }, worker);
        }

        // DELETE: api/Workers/5
        [ResponseType(typeof(Worker))]
        public IHttpActionResult DeleteWorker(int id)
        {
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return NotFound();
            }

            db.Workers.Remove(worker);
            db.SaveChanges();

            return Ok(worker);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkerExists(int id)
        {
            return db.Workers.Count(e => e.Id == id) > 0;
        }
    }
}