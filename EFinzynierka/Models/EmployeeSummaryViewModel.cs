using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFinzynierka.Models
{
    public class EmployeeSummaryViewModel
    {
        public int SelectedEmployeeId { get; set; }
        public int employeeId { get; set; }
        public IEnumerable<EmployeeModel> Employees { get; set; }
        public EmployeeSummary EmployeeSummary { get; set; }
        public int LateArrivals { get; set; }
        public int WeeklyScheduledHours { get; set; }
        public int NoClockIn { get; set; }
        public IEnumerable<SelectListItem> EmployeesSelectList { get; set; }
        public int VacationDays { get; set; }
        
    }
}
