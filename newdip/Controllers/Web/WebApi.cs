using newdip.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace newdip.Controllers
{
	public class WebApi : ApiController
	{
        ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Client> Get()
        {
            return db.Clients.ToList();
        }

        public Client Get(int id)
        {
            return db.Clients.Find(id);
        }

        public class WebUser 
        {
            public string Name { get; set; }
            public string Login { get; set; }
            public string Pass { get; set; }
        }
        public IHttpActionResult Post([FromBody] WebUser webuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Client client = new Client(webuser.Login, webuser.Name, webuser.Pass);
            db.Clients.Add(client);
            db.SaveChanges();
            //client= db.Clients.Where(x => x.Login == webuser.Login).Include(x => x.Notes).Include(x=>x.FRooms).FirstOrDefault();
            return Ok(client);
        }
        public IHttpActionResult Put([FromBody]Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(client).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(client);
        }

        //public IHttpActionResult Delete(int id)
        //{
        //    Client friend = db.Clients.Find(id);
        //    if (friend != null)
        //    {
        //        db.Friends.Remove(friend);
        //        db.SaveChanges();
        //        return Ok(friend);
        //    }
        //    return NotFound();
        //}
    }
}