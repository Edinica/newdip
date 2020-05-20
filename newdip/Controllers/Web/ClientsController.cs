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
using Newtonsoft.Json.Linq;

namespace newdip.Controllers.Web
{
    public class ClientsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Clients
        public IQueryable<Client> GetClients()
        {
            return db.Clients;
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }


        [Route("api/Clients/PostAuthorization")]
        public IHttpActionResult PostAuthorization(JObject element)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var x = element.ToObject<Dictionary<string,string>>();
            string login = x["Login"];
            string pass = x["Pass"];
            Client client = db.Clients.FirstOrDefault(obj => obj.Login == login && obj.Password == pass);
            //db.Clients.Add(client);
            Dictionary<string, string> user = new Dictionary<string, string>();
            user.Add("Id", client.Id.ToString());
            user.Add("Login", client.Login);
            user.Add("Name", client.Name);
            //user.Add("Password", client.Password);
            return Ok(user);
            //return CreatedAtRoute("DefaultApi"
            //    , new { id = 18 }
            //    , client);
        }
        [Route("api/Clients/PostRegister")]
        public IHttpActionResult PostRegister(JObject element)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var x = element.ToObject<Dictionary<string, string>>();//слышно?
            //совсем?
            string login = x["Login"];
            string pass = x["Pass"];
            string name = x["Name"];
            Client client = new Client(login, name, pass);
            //Client client = db.Clients.FirstOrDefault(obj => obj.Login == login && obj.Password == pass);
            db.Clients.Add(client);
            db.SaveChanges();
            Dictionary<string, string> user = new Dictionary<string, string>();
            user.Add("Id", client.Id.ToString());
            user.Add("Login", client.Login);
            user.Add("Name", client.Name);
            //user.Add("Password", client.Password);
            return Ok(user);
            //return CreatedAtRoute("DefaultApi"
            //    , new { id = 18 }
            //    , client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.Id == id) > 0;
        }
    }
}