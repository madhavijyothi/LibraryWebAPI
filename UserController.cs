using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using LibraryWebAPI.Models;


namespace LibraryWebAPI.Controllers
{
    [EnableCors(origins:"*" ,headers:"*",methods:"*")]
    public class UserController : ApiController
    {
        DXCTrainingDBEntities db = new DXCTrainingDBEntities();
        [HttpGet]
        
        public IEnumerable<UserModel> GetUser()
        {
            var result = db.Users.ToList();
            List<UserModel> data = new List<UserModel>();
            foreach (var r in result)
            {
                UserModel m = new UserModel();
                m.UID = r.UID;
                m.Username = r.Username;
                m.Address = r.Address;
                m.MobNo = r.MobNo;

                data.Add(m);
            }
            return data;
        }
        [HttpGet]
        public User GetUser(int id)
        {
            var result = db.Users.Where(x => x.UID == id).SingleOrDefault();
            return result;
        }
        [HttpPost]
        public string Post([FromBody] User u)
        {
            db.Users.Add(u);
            var res = db.SaveChanges();
            if (res > 0)
                return "New user added";
            else
                return "error adding problem";
        }
        [HttpPut]
        public string Update(int id, [FromBody] User u)
        {
            var olddata = (from t in db.Users
                           where t.UID == id
                           select t).SingleOrDefault();
            if (olddata == null)
                throw new Exception("Issue invalid");

            else
            {
                olddata.UID = id ;
                olddata.MobNo = u.MobNo;
                olddata.Address = u.Address;
                olddata.Username = u.Username;
                
                var res = db.SaveChanges();
                if (res > 0)
                    return "Data Updated";
            }
            return "Error Updating Data";
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var data = db.Users.Where(x => x.UID == id).SingleOrDefault();
            if (data == null)
                return NotFound();
            else
            {
                db.Users.Remove(data);
                var res = db.SaveChanges();
                if (res > 0)
                    return Ok();
            }
            return NotFound();
        }
    }
}
    