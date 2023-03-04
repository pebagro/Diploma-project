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

        private readonly IEmployeeservices
            _employeeServices;

        public EmployeeSummary(IEmployeeDataService EmployeeDataService, IEmployeeservices Employeeservices)
        {
            
            _employeeDataService = EmployeeDataService;
            _employeeServices = Employeeservices;
        }

        public async Task<IActionResult> GetEmployeeSummary(int SelectedEmployeeId)
        {
            var employee = _employeeDataService.GetSummary(SelectedEmployeeId);
            var employeesSelectList = ConvertEmployeesToSelectList(_employeeDataService.GetAllEmployeeData());
            var vacationDays = await _employeeServices.GetDaysOffLeftAsync(SelectedEmployeeId);
            
            

            var viewModel = new EmployeeSummaryViewModel()
            {
                SelectedEmployeeId = SelectedEmployeeId,
                EmployeeSummary = employee,
                EmployeesSelectList = employeesSelectList,
                VacationDays = vacationDays,
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