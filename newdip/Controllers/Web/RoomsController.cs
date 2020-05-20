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
    public class RoomsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Rooms
        public IQueryable<Room> GetRooms()
        {
            return db.Rooms;
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(int level, int id, int x, int y)
        {
            Edge edge = new Edge();
            List<Point> points = db.Floors.Where(obj => obj.Level == level && obj.BuildingId == id).Include(obj => obj.Points).FirstOrDefault().Points.ToList();//этаж просмотр
            ///создание и добавление первой точки
            Room result = new Room();
            foreach (var element in points)
            {
                for (int i = -2; i < 3; i++)
                    for (int j = -2; j < 3; j++)
                    {
                        if (element.IsWaypoint && element.X == x + i && element.Y == y + j) //если нашли такую точку на этаже
                        {
                            var room = db.Rooms.Include(obj => obj.Points).ToList();
                            foreach (var vroom in room) //ищем комнату
                            {
                                for (int k = 0; k < vroom.Points.Count; k++)
                                {
                                    if (vroom.Points[k].IsWaypoint &&
                                        vroom.Points[k].X == element.X &&
                                        vroom.Points[k].Y == element.Y)
                                    {
                                        result = new Room(vroom.Name, vroom.Description, vroom.Timetable, vroom.Phone, vroom.Site);
                                        result.Name = "NAme";
                                    }
                                }
                            }

                        }
                    }
            }
            return Ok(result);
        }

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.RoomId)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = room.RoomId }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
            db.SaveChanges();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.RoomId == id) > 0;
        }
    }
}