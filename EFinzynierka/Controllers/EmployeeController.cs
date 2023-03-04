using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System;

namespace EFinzynierka.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeservices _Employeeservices;
        private readonly DbEFinzynierkaContext _context;

        public EmployeeController(IEmployeeservices Employeeservices, DbEFinzynierkaContext context)
        {
            _context = context;
            _Employeeservices = Employeeservices;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.Include(e => e.Shifts).ToList();

            return View(employees);
        }

        public IActionResult Create()
        {
            return View();  
        }

        //Post Employee/Create
        [HttpPost, ActionName("Create")]
        public IActionResult Create(
             [Bind("Surname,Name,Telephone,Contract,CardInfo")] EmployeeModel employee, [BindNever] List<Shift> shifts, [BindNever] RFIDLog RFIDLog)
        {
            ModelState.Remove("Shifts");
            ModelState.Remove("RFIDLog");
            employee.RFIDLog = new RFIDLog();
            employee.RFIDLog.RFIDCardID = employee.CardInfo;
            
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            // Ustaw RFIDCardID pracownika na podane ID karty RFID
            

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

 

        public IActionResult AddShift(int id)
        {
            ViewBag.EmployeeModelId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddShift(Shift shift, int EmployeeId)
        {
            // Find the employee model with the specified ID
            var employeeModel = _context.Employees.Find(EmployeeId);
            if (employeeModel == null)
            {
                return NotFound();
            }

            
            // Set the EmployeeId and EmployeeModel properties of the shift object
            shift.EmployeeId = employeeModel.Id;
            //shift.Employee.Id = shift.EmployeeId;
            shift.Employee = employeeModel;

            // Add the shift and save the changes
            _context.Shifts.Add(shift);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditShift(int shiftId)
        {
            var shift = _context.Shifts.Find(shiftId);
            return View(shift);
        }

        /*[HttpPost]
        public ActionResult EditShift(Shift shift)
        {
            _context.Shifts.Update(shift);
            _context.SaveChanges();

            return RedirectToAction("Index");
*/

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult EditShift(int shiftId, Shift updatedShift)
            {
                if (shiftId != updatedShift.Id)
                {
                    return BadRequest();
                }

                var shift = _context.Shifts.Find(shiftId);

                if (shift == null)
                {
                    return NotFound();
                }

                shift.StartTime = updatedShift.StartTime;
                shift.EndTime = updatedShift.EndTime;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }



        public ActionResult DeleteShift(int shiftId)
        {
            var shift = _context.Shifts.Find(shiftId);
            return View(shift);
        }


        [HttpPost]
        public IActionResult DeleteShifts(Shift shift)
        {
            // Remove the shift and save the changes
            _context.Shifts.Remove(shift);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ScheduledHours(int id, int month, int year)
        {
            // Find the employee with the specified ID
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Calculate the total number of hours for the employee in the specified month
            var shifts = employee.Shifts.Where(s => s.StartTime.Month == month && s.StartTime.Year == year);
            var totalHours = shifts.Sum(s => (s.EndTime - s.StartTime).TotalHours);

            // Display the number of scheduled hours for the employee in the specified month
            ViewBag["Godziny"] = totalHours.ToString();
            return View();
        }
    }
}
