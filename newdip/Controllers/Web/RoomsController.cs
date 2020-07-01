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
        public List<Room> GetRooms()
        {
            return db.Rooms.ToList();
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(
            //int level,
            int id
            //, int x, int y
            )
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
            foreach (var pum in rooms)
                pum.Floor = null ;
            return Ok(rooms);
        }
        [HttpGet]
        [ResponseType(typeof(Room))]
        [Route("api/Rooms/Room")]
        public IHttpActionResult Room(int level,int id,int x, int y)
        {
            List<Room> rooms = db.Floors.Include(obj=>obj.Rooms).Include(obj=>obj.Points).Where(obj => obj.Level == level && obj.BuildingId == id).FirstOrDefault().Rooms.ToList();//список комнат этажа
            List<PointM> points = db.Floors.Where(obj => obj.Level == level && obj.BuildingId == id).Include(obj => obj.Points).FirstOrDefault().Points.ToList();//список точек этажа
            bool isexist = false; int? fid = points[0].FloorId;
            ///создание и добавление первой точки
            Room result = new Room();
            foreach (var element in points)
            {
                for (int i = -5; i < 6; i++)
                    for (int j = -5; j < 6; j++)
                    {
                        if (element.IsWaypoint && element.X == x + i && element.Y == y + j) //если нашли такую точку комнаты на этаже
                        {
                            isexist = true;
                            //var room = db.Rooms.Include(obj => obj.Points).ToList();
                            foreach (var vroom in rooms) //ищем комнату
                            {
                                fid = vroom.FloorId;
                                
                                    if (vroom.Points[0].IsWaypoint &&
                                        vroom.Points[0].X == element.X &&
                                        vroom.Points[0].Y == element.Y)
                                    {
                                        result = new Room(vroom.Name, vroom.Description, vroom.Timetable, vroom.Phone, vroom.Site);
                                        result.RoomId=vroom.RoomId;
                                        result.FloorId = vroom.FloorId; //возвращаем комнату
                                        List<Worker> workers = db.Workers.Where(xx => xx.RoomId == result.RoomId).ToList();
                                        for (int w = 0; w < workers.Count(); w++) 
                                        {
                                        result.Workers.Add(new Worker(workers[w].FirstName, workers[w].SecondName, workers[w].LastName));
                                        }    
                                    }
                                

                            }
                            if (result.FloorId == null)
                                {
                                    result = new Room();
                                    if (fid != null)
                                        result.FloorId = fid;
                                result.Description = "";
                                result.Name = "";
                                result.Timetable = "";
                                result.Phone = "";
                                result.Site = "";
                                db.Rooms.Add(result);
                                db.SaveChanges();
                                    result = db.Rooms.ToList().Last();
                                
                                    //element.Room = result;
                                //element.RoomId = result.RoomId;
                                result.Points.Add(element);
                                db.SaveChanges(); //   return Ok(result);
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