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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ApiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public JsonResult Get()
        {
            string query = @"select EmployeeID as id,Name as name,Email as email,Gender as gender,Location as location from Employee_master";
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
            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"insert into Employee_master
                            (Name,Email,Gender,Location)
                            values(
                            '" + emp.Name + @"',
                            '" + emp.Email + @"',
                            '" + emp.Gender + @"',
                            '" + emp.Location + @"')";

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

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from Employee_master  where EmployeeID='" + id + @"'";
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
            return new JsonResult("Deleted Succesfully");
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"select EmployeeID as id,Name as name,Email as email,Gender as gender,Location as location from Employee_master  where EmployeeID='" + id + @"'";
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
            return new JsonResult(table);
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id, Employee emp)
        {
            string query = @"update Employee_master set
                             Name='" + emp.Name + @"'
                               , Email='" + emp.Email + @"'
                                ,Location='" + emp.Location + @"',
                                 Gender='" + emp.Gender + @"'
                                where EmployeeId= " + id + @"";
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
            return new JsonResult("Updated Succesfully");
        }



    }
}
