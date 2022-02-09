using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Model
{
    public class Employee : Controller
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }
        public string Location { get; set; }
    }
}
