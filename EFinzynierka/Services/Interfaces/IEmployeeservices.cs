using EFinzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFinzynierka.Services.Interfaces
{
    public interface IEmployeeservices
    {

        //public string getFullname(string name, string surname);
        //public bool EmployeeExist(EmployeeModel employee);
        public int Delete(int id);
        public int Edit(EmployeeModel employee);
        public int Add(EmployeeModel employee);
        public EmployeeModel Get(int id);
        List<EmployeeModel> GetAll();
     /*   public EmployeeModel EmployeeExist(EmployeeModel employee);
        public EmployeeModel Edit(EmployeeModel employee);
        public EmployeeModel Delete(EmployeeModel employee);*/

/*i
        List<EmployeeController> GetAll();
        Iemployeeservices (int id);  */
    }
}
