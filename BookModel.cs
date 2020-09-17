using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebAPI.Models
{
    public class BookModel
    {
        public int BID { get; set; }
        public string Bookname { get; set; }
        public long  Price { get; set; }
        public string Genre { get; set; }

    }
}