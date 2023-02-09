using EFinzynierka.Models;

namespace EFinzynierka.Services.Interfaces
{
    public interface IEmployeeservices
    {
        public int Delete(int id);
        public int Edit(EmployeeModel employee);
        public int Add(EmployeeModel employee);
        public EmployeeModel Get(int id);
        public SchedulerModel Gets(int id);
        public List<EmployeeModel> GetAll();
        public void Others(int id);
        public void Otherss(int id);
        public SchedulerModel tester (int id, int int_month);
        double GetScheduledHours(int id, int month, int year);
    }
}

