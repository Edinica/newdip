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
    public class FavoriteRoomsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FavoriteRooms
        public IQueryable<FavoriteRoom> GetFavoriteRooms()
        {
            return db.FavoriteRooms;
        }

        // GET: api/FavoriteRooms/5
        [ResponseType(typeof(FavoriteRoom))]
        public IHttpActionResult GetFavoriteRoom(int id)
        {
            List<FavoriteRoom> favoriteRooms = db.Clients.Include(x => x.FRooms).FirstOrDefault(x => x.Id == id).FRooms.ToList();

            if (favoriteRooms == null)
            {
                return NotFound();
            }

            return Ok(favoriteRooms);
        }

        // PUT: api/FavoriteRooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFavoriteRoom(int id, FavoriteRoom favoriteRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != favoriteRoom.FavoriteRoomId)
            {
                return BadRequest();
            }

            db.Entry(favoriteRoom).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteRoomExists(id))
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

        // POST: api/FavoriteRooms
        [ResponseType(typeof(FavoriteRoom))]
        public IHttpActionResult PostFavoriteRoom(FavoriteRoom favoriteRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FavoriteRooms.Add(favoriteRoom);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = favoriteRoom.FavoriteRoomId }, favoriteRoom);
        }

        // DELETE: api/FavoriteRooms/5
        [ResponseType(typeof(FavoriteRoom))]
        public IHttpActionResult DeleteFavoriteRoom(int id)
        {
            FavoriteRoom favoriteRoom = db.FavoriteRooms.Find(id);
            if (favoriteRoom == null)
            {
                return NotFound();
            }

            db.FavoriteRooms.Remove(favoriteRoom);
            db.SaveChanges();

            return Ok(favoriteRoom);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FavoriteRoomExists(int id)
        {
            return db.FavoriteRooms.Count(e => e.FavoriteRoomId == id) > 0;
        }
    }
}