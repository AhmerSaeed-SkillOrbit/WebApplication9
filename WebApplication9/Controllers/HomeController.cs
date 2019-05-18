using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Create_Post(Register register)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //SqlDataReader
                    connection.Open();

                    string sql = "Select * From Register Where Email = '{register.Email}' AND Password = {register.Password}";
                    SqlCommand command = new SqlCommand(sql, connection);
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        //while (dataReader.Read())
                        //{
                        //    Teacher teacher = new Teacher();
                        //    teacher.Id = Convert.ToInt32(dataReader["Id"]);
                        //    teacher.Name = Convert.ToString(dataReader["Name"]);
                        //    teacher.Skills = Convert.ToString(dataReader["Skills"]);
                        //    teacher.TotalStudents = Convert.ToInt32(dataReader["TotalStudents"]);
                        //    teacher.Salary = Convert.ToDecimal(dataReader["Salary"]);
                        //    teacher.AddedOn = Convert.ToDateTime(dataReader["AddedOn"]);
                        //    teacherList.Add(teacher);
                        //}
                    }
                    connection.Close();
                }
            }
            else
                return View();
        }
    }
}
}
