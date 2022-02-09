using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApp.Model;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace ApiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post(Login login)
        {
            if (login.Email == null)
            {
                return BadRequest("Inavlid email id");
            }

            string query = "select ID as id from User_master WHERE EMAIL = '" + login.Email + @"' AND PASSWORD =  '" + login.password + @"'";
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

            //if ((login.Email =="hemant@gmail.com") && (login.password =="hemant"))
            if(table.Rows.Count>0)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Key@1234fgretheergthyjkujyhtgrwfeghfdsfbgndefrgthyjhgfdf"));

                var SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.Email),
                    new Claim(ClaimTypes.Role, "Admin")

                };
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials:SigningCredentials
                    ) ;

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });


            }

            return Unauthorized();
        }

    }
}