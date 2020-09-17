using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace LibraryWebAPI.Models

{
    public class IssueModel
    {
        public int IID { get; set; }
        public string  Username { get; set; }
        public string Bookname { get; set; }
        public string IssueDate { get; set; }
        public int NoofDays { get; set; }
        public string ExpReturnDate { get; set; }
        public string ActualReturnDate { get; set; }
        public long Fine { get; set; }


      
    }
}