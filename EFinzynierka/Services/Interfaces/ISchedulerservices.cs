using EFinzynierka.Models;

namespace EFinzynierka.Services.Interfaces
{
    public interface ISchedulerservices : IEmployeeservices
    {
        public EmployeeModel Get(int id);
        // List<EmployeeModel> GetAll();
        public int PickMonth();
        public int Add(MonthlyModel monthlyModel);
        public int LoadMonth(string id);
        public List<ISchedulerservices> schedulerservices(List<MonthlyModel> monthlyModels);
    }
}
