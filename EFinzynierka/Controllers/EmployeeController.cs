using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EFinzynierka.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeservices _Employeeservices;

        public EmployeeController(IEmployeeservices Employeeservices)
        {
            _Employeeservices = Employeeservices;
        }

      
        public IActionResult Index()
        {
            var employeeList = _Employeeservices.GetAll();
            return View(employeeList);
        }

        public IActionResult Create()
        {
            return View();  
        }

        //Post Employee/Create
        [HttpPost,ActionName("Create")]
        public IActionResult Create(
                     [Bind("Surname,Name,Telephone,Contract")] EmployeeModel employee)
        {

            ModelState.Remove("vSchedulerModel");
            ModelState.Remove("Discriminator");
            ModelState.Remove("Fullname");
            if (!ModelState.IsValid)
            {

                return View(employee);
            }

            _Employeeservices.Add(employee);

            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            if (id == null) 
            {
                return (NotFound());
            }

            _Employeeservices.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return (NotFound());
            }
            var employee = _Employeeservices.Get(id);


            return View(employee);
        }


        [HttpPost]
        public IActionResult Edit(EmployeeModel employee)
        {

            if ( employee== null)
            {
                return (NotFound());
            }

            _Employeeservices.Edit(employee);

            return RedirectToAction("Index");
        }

    }
}
