using EFinzynierka.Models;

namespace EFinzynierka.Services.Interfaces
{
    public interface ISchedulerservices : IEmployeeservices
    {
        public EmployeeModel Get(int id);
        // List<EmployeeModel> GetAll();
        public SchedulerModel PickMonth();
        public int Add(MonthlyModel monthlyModel);
        public int AddScheduler(SchedulerModel schedulerModel);
        public SchedulerModel LoadMonth(int id, int month);
        public List<ISchedulerservices> schedulerservices(List<MonthlyModel> monthlyModels);
        public SchedulerModel tester(int id, int int_month);
    }
}
