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
using Newtonsoft.Json.Linq;

namespace newdip.Controllers.Web
{
    public class NotesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Notes
        public List<Note> GetNotes()
        {
            return db.Notes.ToList() ;
        }

        // GET: api/Notes/5++
        [ResponseType(typeof(Note))]
        public IHttpActionResult GetNote(int id)
        {
            //получаем все этажи здания
            List<Floor> Floor = db.Floors.Where(xx => xx.BuildingId == id).ToList();
            List<Room> rooms = new List<Room>();
            //получаем все комнаты этажей
            foreach (var element in Floor)
            {
                List<Room> temp = db.Rooms.Where(x => x.FloorId == element.FloorId).ToList();
                foreach (var pum in temp)
                    rooms.Add(pum);
            }
            List<Note> notes = new List<Note>();
            //получаем все public заметки помещений
            foreach (var element in rooms)
            {
                List<Note> temp = db.Notes.Where(x => x.RoomId == element.RoomId&&x.isPublic==true).ToList();
                foreach (var pum in temp)
                    notes.Add(pum);
            }
            foreach (var pum in notes)
                pum.Room = null;
            return Ok(notes);

        }

        // PUT: api/Notes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNote(int id, Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.NoteId)
            {
                return BadRequest();
            }

            db.Entry(note).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(note);
        }

        // POST: api/Notes
        [Route("api/Notes/PostClientNote")]
        public IHttpActionResult PostClientNote(JObject element)
        {
            var temp = element.ToObject<Dictionary<string, int>>();
            int id = temp["id"];
            List<Note> notes = db.Notes
                .Where(x => x.ClientId == id).ToList();
            return Ok(notes);
        }
        // POST: api/Notes
        [Route("api/Notes/PostAddtNote")]
        public IHttpActionResult PostAddNote([FromBody]Note element)
        {
           // element.NoteId = db.Notes.Last().NoteId+1;
            db.Notes.Add(element);
            db.SaveChanges();
            return Ok(
                element
                ) ;
        }

        // DELETE: api/Notes/5
        [ResponseType(typeof(Note))]
        public IHttpActionResult DeleteNote(int id)
        {
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }

            db.Notes.Remove(note);
            db.SaveChanges();

            return Ok(note);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoteExists(int id)
        {
            return db.Notes.Count(e => e.NoteId == id) > 0;
        }
    }
}