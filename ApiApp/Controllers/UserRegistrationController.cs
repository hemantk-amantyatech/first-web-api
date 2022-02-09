using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApp.Model;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ApiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Post(UserRegistration userRegis)
        {
            string query = @"insert into User_master
                            (NAME,EMAIL,PASSWORD)
                            values(
                            '" + userRegis.Name + @"',
                            '" + userRegis.Email + @"',
                            '" + userRegis.password + @"')";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added Succesfully");
        }
    }
}
