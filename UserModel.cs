using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebAPI.Models
{
    public class UserModel
    {
        public int UID { get; set; }
        public string Username { get; set; }
        public long MobNo { get; set; }
        public string Address { get; set; }

    }
}