using Microsoft.AspNetCore.Mvc;
using EFinzynierka.Services;
using EFinzynierka.Services.Interfaces;
using EFinzynierka.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFinzynierka.Controllers
{
    public class EmployeeSummary : Controller
    {
        private readonly IEmployeeDataService _employeeDataService;

        public EmployeeSummary(IEmployeeDataService EmployeeDataService)
        {
            _employeeDataService = EmployeeDataService;
        }

        public IActionResult GetEmployeeSummary(int SelectedEmployeeId)
        {
            var employee = _employeeDataService.GetSummary(SelectedEmployeeId);
            var employeesSelectList = ConvertEmployeesToSelectList(_employeeDataService.GetAllEmployeeData());

            var viewModel = new EmployeeSummaryViewModel()
            {
                SelectedEmployeeId = SelectedEmployeeId,
                EmployeeSummary = employee,
                EmployeesSelectList = employeesSelectList,
            };

            return View("Index", viewModel);
        }


        private IEnumerable<SelectListItem> ConvertEmployeesToSelectList(IEnumerable<EmployeeModel> employees)
        {
            return employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = $"{e.Name} {e.Surname}"
            });
        }


        [HttpGet("{employeeId}/weeklyhours")]
        public ActionResult<double> GetWeeklyHours(int employeeId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var weeklyHours = _employeeDataService.GetWeeklyHours(employeeId, startDate, endDate);
            return weeklyHours;
        }


        [HttpGet("{employeeId}/monthlyhours")]
        public ActionResult<double> GetMonthlyHours(int employeeId, [FromQuery] int month, [FromQuery] int year)
        {
            var monthlyHours = _employeeDataService.GetMonthlyHours(employeeId, month, year);
            return monthlyHours;
        }


        [HttpGet("{employeeId}/rfidlogins")]
        public ActionResult<int> GetRFIDLogins(int employeeId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var rfidLogins = _employeeDataService.GetRFIDLogins(employeeId, startDate, endDate);
            return rfidLogins;
        }


    }
}