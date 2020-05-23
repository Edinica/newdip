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
            List<FavoriteRoom> favoriteRooms = db.FavoriteRooms.Where(x => x.ClientId == id).ToList();

            if (favoriteRooms == null)
            {
                return NotFound();
            }

            return Ok(favoriteRooms);
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

    }
}