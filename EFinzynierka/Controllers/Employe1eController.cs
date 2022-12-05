using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Linq;

namespace EFinzynierka.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DbEFinzynierkaContext _context;
        private readonly Iemployeeservices _Employeeservices;

        public EmployeeController(Iemployeeservices Employeeservices, DbEFinzynierkaContext context)
        {
            _Employeeservices = Employeeservices;
            _context = context;
        }


        public async Task<IActionResult> Index(
            int? pageNumber,
            string currentFilter,
            string searchString)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var employees = from e in _context.Employees select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.Surname.Contains(searchString)
                                       || s.Name.Contains(searchString));
            }
            int pageSize = 15;

            return View(await PaginatedList<EmployeeModel>.CreateAsync(employees, pageNumber ?? 1, pageSize));

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var employee = await _context.Employees.Include(e => e.IdEmployee).FirstOrDefaultAsync(m => m.IdEmployee == id);


            if (employee == null)
            {
                return NotFound();

            }
            return View(employee);
        }

        //Get Employee/Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        //Post Employee/Create
        [HttpPost]
        public async Task<IActionResult> Create(
                     [Bind("Surname,Name,Telephone,Contract")] EmployeeModel employee)
        {
            try
            {
                //employee.IdEmployee = 321;

                ModelState.Remove("vSchedulerModel");
                ModelState.Remove("Discriminator");
                ModelState.Remove("Fullname");
                if (ModelState.IsValid)
                {
                    _context.Add(employee);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Błąd zapisu. " + "Spróbuj ponownie. ");
            }
            return View();
        }

        /*Get Employee edit*/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return (NotFound());
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return (NotFound());
            }

            return View(employee);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return (NotFound());

            }
            var employeeToUpdate = await _context.Employees.FirstOrDefaultAsync(m => m.IdEmployee == id);
            if (await TryUpdateModelAsync<EmployeeModel>(
                employeeToUpdate,
                "",
                s => s.Name, s => s.Surname, s => s.Telephone))
            {
                try
                {

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {

                    ModelState.AddModelError("", "Błąd zapisu. " + "Spróbuj ponownie. ");
                }
            }
            return View(employeeToUpdate);
        }

        /* Get: Employee/Delete */
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return (NotFound());
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.IdEmployee == id);

            if (employee == null)
            {
                return (NotFound());
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Nie udało usunąć się pracownika, spróbuj ponownie. ";
            }
            return View(employee);
        }
        /* POST: Employee/Delete */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });

            }
        }

        // Testowe
        [HttpGet]
        public async Task<IActionResult> EditScheduler(int id, string? month)
        {
            if (id != null && month != null)
            {
                View();
                RedirectToAction("DisplaySchedule", "EmployeeController");
            }

            
                if (id == null) { return (NotFound()); }
                var employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                { return View(employee); }
                else
                { return (NotFound()); }
            
           
        }


        [HttpPost, ActionName("EditScheduler")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchedule(int? id, int? month)
        {
            if (id == null)
            {
                return (NotFound());
            }

           
            


            List<EmployeeModel> list = new List<EmployeeModel> { };
            EmployeeModel workinghour = new EmployeeModel();
            // Baza danych List<EmployeeModel> employee = Inew List;
            var schedulerEdit = await _context.Employees.FirstOrDefaultAsync(m => m.IdEmployee == id);

            if (await TryUpdateModelAsync<EmployeeModel>(schedulerEdit, "",
                s => s.IdEmployee, s => s.workingHours))
                if (month != null)
                {
                    return View();
                    System.Console.WriteLine("chja");
                }

            if (month != null)
            {
                System.Console.WriteLine("chuja");
                var employee = await _context.Employees.FindAsync(id, month);//.FirstOrDefaultAsync(m => m.IdEmployee == id);
                return View(employee);
                //return RedirectToAction("DisplaySchedule");
            }

            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {

                    ModelState.AddModelError("", "Błąd zapisu. " + "Spróbuj ponownie. ");
                }
            }
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = 0; i < 12; i++)
            {
                items.Add(new SelectListItem { Text = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames[i] });
            }

            ViewBag.month = items;
            TempData["items"] = items;
            return View(schedulerEdit);
        }






        [HttpGet]
        public async Task<IActionResult> DisplayScheduler(int? id, int? month)
        {
            if (id == null)
            {
                return (NotFound());
            }
            
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return (NotFound());
            }
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = 0; i < month; i++)
            {
                items.Add(new SelectListItem { Text = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames[i], Value = i.ToString(), });
            }

            ViewBag.month = items;
            return View(employee);
        }


        [HttpPost]
        public async Task<IActionResult> DisplayScheduler(int? pageNumber, int? id, int? month)
        {
            var schedulerDisplay = await _context.Employees.FirstOrDefaultAsync(m => m.IdEmployee == id);
            if (id == null)
            {
                return (NotFound());
            }

            if (month == null)
            {
                return (NotFound());
            }

            var employees = from e in _context.Employees select e;

            employees = employees.Where(s => s.Surname.Any());

            int pageSize = 15;


            return View();
            //return RedirectToAction("DisplaySchedule");
            //return View(await PaginatedList<EmployeeModel>.CreateAsync(employees, pageNumber ?? 1, pageSize));


        }





        private bool EmployeeExist(int id)
        {
            return _context.Employees.Any(e => e.IdEmployee == id);
        }

        public string getFullname(string name, string surname)
        {
            return string.Join(" ", name, surname);
        }



















    }
}
