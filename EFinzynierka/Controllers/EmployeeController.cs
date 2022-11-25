using EFinzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace EFinzynierka.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DbEFinzynierkaContext _context;

        public EmployeeController(DbEFinzynierkaContext context)
        {

            _context = context;
        }

        public async Task<IActionResult> Index(
            int? pageNumber,
            string currentFilter,
            string searchString
        )
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
            int pageSize = 3;

            return View(await PaginatedList<EmployeeModel>.CreateAsync(employees, pageNumber ?? 1, pageSize));

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var employee = await _context.Employees.Include(e => e.EmployeeId).FirstOrDefaultAsync(m => m.id == id);


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
        public async Task<IActionResult> Create(
            [Bind("Name,Surname,Telephone")] EmployeeController employee)
        {
            try
            {
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
            var employeeToUpdate = await _context.Employees.FirstOrDefaultAsync(m => m.id == id);
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

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.id == id);

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
        private bool EmployeeExist(int id)
        {
            return _context.Employees.Any(e => e.id == id);
        }
    }
}
