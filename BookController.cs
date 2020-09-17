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
    [EnableCors(origins: "*", headers: "*",methods: "*")]

    public class BookController : ApiController
    {
        DXCTrainingDBEntities db = new DXCTrainingDBEntities();
        [HttpGet]
        public IEnumerable<BookModel> GetBooks()
        {
            var result = db.Books.ToList();
            List<BookModel> data = new List<BookModel>();
            foreach (var r in result)
            {

                BookModel m = new BookModel();
                m.BID = r.BID;
                m.Bookname = r.Bookname;
                m.Price = (long)r.Price;
                m.Genre = r.Genre;

                data.Add(m);
            }
            return data;
        }
       
        [HttpPost]
        public string Post([FromBody] Book b)
        {
            db.Books.Add(b);
            var res = db.SaveChanges();
            if (res > 0)
                return "New Books added";
            else
                return "error adding problem";
        }
        [HttpPut]
        public string Update(int id, [FromBody] Book b)
        {
            var olddata = (from t in db.Books
                           where t.BID == id
                           select t).SingleOrDefault();
            if (olddata == null)
                throw new Exception("Book invalid");

            else
            {
                olddata.BID = id;
                olddata.Bookname =b.Bookname;
                olddata.Price = b.Price;
                olddata.Genre = b.Genre;

                var res = db.SaveChanges();
                if (res > 0)
                    return "Book Updated";
            }
            return "Error Updating Data";
        }
    }
}