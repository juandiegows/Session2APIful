using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Session2APIful.Models.Request
{
    public class LoginRequest
    {
        public String User { set; get; }
        public String Password { set; get; }
    }
}