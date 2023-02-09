using EFinzynierka.Models;

namespace EFinzynierka.Services.Interfaces
{
    public interface IEmployeeDataService
    {

        public EmployeeModel GetEmployeeData(int employeeId);
        public IEnumerable<EmployeeModel> GetAllEmployeeData();
        public EmployeeSummary GetSummary(int employeeId);
        public double GetWeeklyHours(int employeeId, DateTime startDate, DateTime endDate);
        public double GetMonthlyHours(int employeeId, int month, int year);
        public int GetRFIDLogins(int employeeId, DateTime startDate, DateTime endDate);
        public (int WorkExperienceYears, int WorkExperienceMonths) GetWorkExperience(int employeeId);
    }

}