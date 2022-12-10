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
    }
}
