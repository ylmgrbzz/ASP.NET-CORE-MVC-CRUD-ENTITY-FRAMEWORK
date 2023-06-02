using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department
            };

            await mvcDemoContext.Employees.AddAsync(employee);
            await mvcDemoContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var employee = await mvcDemoContext.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (employee != null)
            {
                var viewEmployeeModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department
                };
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel updateEmployeeRequest)
        {
            var employee = await mvcDemoContext.Employees.FirstOrDefaultAsync(e => e.Id == updateEmployeeRequest.Id);
            if (employee != null)

            {
                employee.Name = updateEmployeeRequest.Name;
                employee.Email = updateEmployeeRequest.Email;
                employee.Salary = updateEmployeeRequest.Salary;
                employee.DateOfBirth = updateEmployeeRequest.DateOfBirth;
                employee.Department = updateEmployeeRequest.Department;
                mvcDemoContext.Employees.Update(employee);
                await mvcDemoContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
