using newdip.Models;
using Newtonsoft.Json;
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
        [HttpGet]
        [ActionName("Register")]
        public string Register()
        {
       
            return "Hello World!";
        }
        [HttpGet]
        public string Register1(string s)
        {

            return "Hello World!"+s;
        }

      // [HttpPost]
      // public IHttpActionResult Register(string WUser)
      // {
      //     if (!ModelState.IsValid)
      //     {
      //         return BadRequest(ModelState);
      //     }
      //     var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(WUser);
      //     
      //     Client client = new Client(values["Login"], values["Name"], values["Pass"]);
      //     db.Clients.Add(client);
      //     db.SaveChanges();
      //     //client= db.Clients.Where(x => x.Login == webuser.Login).Include(x => x.Notes).Include(x=>x.FRooms).FirstOrDefault();
      //     Dictionary<string, string> user = new Dictionary<string, string>();
      //     user.Add("Name", client.Name);
      //     user.Add("Login", client.Login);
      //     user.Add("Id", client.Id.ToString());
      //     return Ok(user);
      // }
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