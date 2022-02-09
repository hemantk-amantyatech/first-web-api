using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Model
{
    public class Login: Controller
    {
        public string Email { get; set; }

        public string password { get; set; }

        public string Usertype { get; set; }
    }
}
