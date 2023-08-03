using EmpMgnt.Data;
using EmpMgnt.Models;
using EmpMgnt.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace EmpMgnt.Controllers
{
    public class ListController : Controller
    {
        private readonly Configuration _configuration;

        private readonly EmpMgntDbContext _empMgntDbContext;

        List<Employee> employees = new List<Employee>();
        public ListController(IOptions<Configuration> config, EmpMgntDbContext empMgntDbContext)
        {
            _configuration = config.Value;
            _empMgntDbContext = empMgntDbContext;
        }

        //Action method for displaying the list of all employees
        public IActionResult Index()
        {

            //using (SqlConnection connection = new SqlConnection(_configuration.EmployeeConnectionString))
            //{
            //    string query = "SELECT * FROM Employee";

            //    using (SqlCommand command = new SqlCommand(query, connection))
            //    {
            //        connection.Open();
            //        SqlDataReader reader = command.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            Employee employee = new Employee((int)reader["Id"], (string)reader["Name"], (string)reader["Address"],
            //                (string)reader["Mobile"], (DateTime)reader["DOB"]);

            //            employees.Add(employee);
            //        }

            //        reader.Close();
            //    }
            //}
             
            employees = _empMgntDbContext.Employee.ToList();

            return View(employees);
        }

        [HttpPost]
        public IActionResult EditList()
        {
            return View(employees);
        }


        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                //using (SqlConnection connection = new SqlConnection(_configuration.EmployeeConnectionString))
                //{
                //    string query = "Delete from Employee Where (@Id=id)";


                //    using (SqlCommand command = new SqlCommand(query, connection))
                //    {
                //        command.Parameters.AddWithValue("@Id", id);
                //        connection.Open();
                //        command.ExecuteNonQuery();
                //    }
                //}
                Employee employee = _empMgntDbContext.Employee.FirstOrDefault(x => x.Id == id);
                _empMgntDbContext.Employee.Remove(employee);
                _empMgntDbContext.SaveChanges();
                return RedirectToAction("Index", "List");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during database insertion
                // You can log the error or show an error message to the user
                // For simplicity, we're returning the same view with the model containing validation errors
                ModelState.AddModelError("", "An error occurred while adding the employee to the database.");
            }

            return View("Index");
        }
    }
}
