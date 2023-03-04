using EFinzynierka.Models;

namespace EFinzynierka.Services.Interfaces
{
    public interface IEmployeeservices
    {
        public int Delete(int id);
        public int Edit(EmployeeModel employee);
        public int Add(EmployeeModel employee);
        public EmployeeModel Get(int id);
        public List<EmployeeModel> GetAll();
        double GetScheduledHours(int id, int month, int year);
        Task<EmployeeSummary> GetEmployeeSummaryAsync(int employeeId);
        Task<(int years, int months)> GetWorkExperienceAsync(int employeeId);
        Task<int> GetDaysOffLeftAsync(int employeeId);
    }
}

