using EmpMgnt.Data;
using EmpMgnt.Models;
using EmpMgnt.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace EmpMgnt.Controllers
{
    public class FormController : Controller
    {
        private readonly Configuration _configuration;

        private readonly EmpMgntDbContext _empMgntDbContext;

        public FormController(IOptions<Configuration> config, EmpMgntDbContext empMgntDbContext)
        {
            _configuration = config.Value;
            _empMgntDbContext = empMgntDbContext;
        }
        public IActionResult Index()
        {
            Employee employee = new Employee();
            return View(employee);
        }

        [HttpPost]
        [ActionName("AddEmployee")]
        public ActionResult AddEmployee()
        {
            Random random = new Random();
            var Id = random.Next(0, 9);
            var Name = Request.Form["Name"];
            var Address = Request.Form["Address"];
            var Mobile = Request.Form["Mobile"];
            var dob = Request.Form["DOB"];
            DateTime date = DateTime.Parse(dob);

            Employee employee = new Employee()
            {
                Name = Name,
                Address = Address,
                Mobile = Mobile,
                DOB = date
            };

            try
            {
                //using (SqlConnection connection = new SqlConnection(_configuration.EmployeeConnectionString))
                //{
                //    string query = "INSERT INTO Employee (Name, Address, Mobile, DOB) " +
                //                   "VALUES (@Name, @Address, @Mobile, @DOB)";

                //    using (SqlCommand command = new SqlCommand(query, connection))
                //    {
                //        command.Parameters.AddWithValue("@Name", employee.Name);
                //        command.Parameters.AddWithValue("@Address", employee.Address);
                //        command.Parameters.AddWithValue("@Mobile", employee.Mobile);
                //        command.Parameters.AddWithValue("@DOB", employee.DOB);

                //        connection.Open();
                //        command.ExecuteNonQuery();
                //    }
                //}

                _empMgntDbContext.Employee.Add(employee);
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


        [ActionName("ModifyEmployee")]
        public ActionResult ModifyEmployee()
        {
            string id = Request.Query["id"];
            Employee employee;
            try
            {
                //using (SqlConnection connection = new SqlConnection(_configuration.EmployeeConnectionString))
                //{
                //    string query = "Select * from Employee where Id=@id";

                //    using (SqlCommand command = new SqlCommand(query, connection))
                //    {
                //        connection.Open();
                //        command.Parameters.AddWithValue("@Id", id);
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            if (reader.Read())
                //            {

                //                Id = reader.GetInt32(0);
                //                Name = reader.GetString(1);
                //                Address = reader.GetString(2);
                //                Mobile = reader.GetString(3);
                //                DOB = reader.GetDateTime(4);
                //                employee = new Employee();
                //                return View("Modify", employee);
                //            }
                //        }
                //    }
                //}
                employee = _empMgntDbContext.Employee.FirstOrDefault(x => x.Id == int.Parse(id));
                return View("Modify", employee);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding the employee to the database.");
            }
            return View("Modify");
        }


        [ActionName("ModifyEmployeeData")]
        public ActionResult ModifyEmployeeData()
        {
            var Id =int.Parse(Request.Form["Id"]);
            var Name = Request.Form["Name"];
            var Address = Request.Form["Address"];
            var Mobile = Request.Form["Mobile"];
            var DOB = Request.Form["DOB"];
            DateTime date = DateTime.Parse(DOB);

            Employee employee = new Employee()
            {
                Id = Id,
                Name = Name,
                Address = Address,
                Mobile = Mobile,
                DOB = date,
            };

            try
            {
                //using (SqlConnection connection = new SqlConnection(_configuration.EmployeeConnectionString))
                //{
                //    string query = "Update Employee SET Name=@Name, Address=@Address, Mobile=@Mobile, DOB=@DOB " +
                //                    "Where Id=@Id";


                //    using (SqlCommand command = new SqlCommand(query, connection))
                //    {
                //        command.Parameters.AddWithValue("@Id",employee.Id);
                //        command.Parameters.AddWithValue("@Name", employee.Name);
                //        command.Parameters.AddWithValue("@Address", employee.Address);
                //        command.Parameters.AddWithValue("@Mobile", employee.Mobile);
                //        command.Parameters.AddWithValue("@DOB", employee.DOB);

                //        connection.Open();
                //        command.ExecuteNonQuery();
                //    }
                //}

                _empMgntDbContext.Employee.Update(employee);
                _empMgntDbContext.SaveChanges();

                return RedirectToAction("Index","List");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding the employee to the database.");
            }
            return View("Modify");
        }
    }
}
