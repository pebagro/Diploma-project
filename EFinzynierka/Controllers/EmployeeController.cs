using EFinzynierka.DbDate;
using EFinzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace EFinzynierka.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DbEmployees _context;
        public EmployeeController(DbEmployees context)
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
            var employee = from e in _context.Employees
                           select e;
            int pageSize = 3;
            return View(await PaginatedList<EmployeeController>.CreateAsync(employee.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();

            }
            var employee = await _context.Employees.Include(e => e.EmployeeId).FirstOrDefaultAsync(m => m.Id == id);


            if(employee== null)
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
            [Bind("")])
        {
            try
            {
                if (ModelState.IsValid)
            }
        }



    }
}
