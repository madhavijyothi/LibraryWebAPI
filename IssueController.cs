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
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class IssueController : ApiController
    {
        DXCTrainingDBEntities db = new DXCTrainingDBEntities();
        [HttpGet]

        public IEnumerable<IssueModel>GetIssues()
        {
            var result = db.Issues.ToList();
            List<IssueModel> data = new List<IssueModel>();
            foreach (var r in result)
            {
                IssueModel m = new IssueModel();
                m.IID = r.IID;            
                m.Bookname = r.Book.Bookname;
                m.Username = r.User.Username;
                m.IssueDate = r.IssueDate;
                m.NoofDays = (int)r.NoofDays;
                m.ExpReturnDate = r.ExpReturnDate;
                m.Fine = r.Fine;
                m.ActualReturnDate = r.ActualReturnDate;

              
                data.Add(m);

            }
            return data;
        }
        [HttpGet]
        public Issue GetIssue(int id)
        {
            var result = db.Issues.Where(x => x.IID == id).SingleOrDefault();
            return result;
        }
     
        [HttpPost]
        public string Post([FromBody] Issue i)
        {
            db.Issues.Add(i);
            var res = db.SaveChanges();
            if (res > 0)
                return "New Issue added";

            else
                return "error adding problem";
        }
        [HttpPut]
        public string Update(int id, [FromBody] Issue i)
        {
            var olddata = (from t in db.Issues
                           where t.IID == id
                           select t).SingleOrDefault();

            if (olddata == null)

                throw new Exception("Issue invalid");

            else
            {
                string actdate = i.ActualReturnDate.ToString();
                string expdate = i.ExpReturnDate.ToString();
           //    olddata.IID = id;
             //   olddata.IssueDate = i.IssueDate;
             //   olddata.NoofDays = i.NoofDays;
              olddata.ExpReturnDate = i.ExpReturnDate;
                olddata.ActualReturnDate = i.ActualReturnDate;


                TimeSpan ts = Convert.ToDateTime(actdate).Subtract(Convert.ToDateTime(expdate));
                int day = ts.Days;

                if(day>0)
                {
                    olddata.Fine = (10 * day);
                }

                else
                {
                    olddata.Fine = 0;
                }




                var res = db.SaveChanges();
                if (res > 0)
                    return "Data Updated";
            }
            return "Error Updating Data";
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var data = db.Issues.Where(x => x.IID == id).SingleOrDefault();
            if (data == null)
                return NotFound();
            else
            {
                db.Issues.Remove(data);
                var res = db.SaveChanges();
                if (res > 0)
                    return Ok();
            }
            return NotFound();
        }
    }
}
