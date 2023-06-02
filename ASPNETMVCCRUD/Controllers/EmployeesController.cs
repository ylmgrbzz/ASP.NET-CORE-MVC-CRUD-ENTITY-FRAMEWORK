using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETMVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        public readonly MVCDemoDbContext mvcDemoContext;

        public EmployeesController(MVCDemoDbContext mvcDemoContext)
        {
            this.mvcDemoContext = mvcDemoContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department
            };

            mvcDemoContext.Employees.Add(employee);
            mvcDemoContext.SaveChanges();
            return View();
        }
    }
}
