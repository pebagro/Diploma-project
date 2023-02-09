using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;

namespace EFinzynierka.Services
{
    public class Employeeservices : IEmployeeservices
    {

        private readonly DbEFinzynierkaContext _context;
        public Employeeservices(DbEFinzynierkaContext context)
        {
            _context = context;
        }

        public int Add(EmployeeModel employee)
        {

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee.Id;
        }

        public int Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return employee.Id;
        }

        public int Edit(EmployeeModel employee)
        {

            _context.Employees.Update(employee);
            _context.SaveChanges();

            return employee.Id;
        }


        public List<EmployeeModel> GetAll()
        {

            var list = _context.Employees.ToList();

            return list;

        }

        public EmployeeModel Get(int id)
        {
            var cokolwiek = _context.Employees.Find(id);
            return cokolwiek;
        }

        public void Others(int id)
        {
            var employees = _context.Employees.Find(id);
            //_context.Entry(employees).Collection(p => p.MonthlyModels).Load();
        }

        public void Otherss(int id)
        {
            var scheduler = _context.Employees.Find(id);
            
         //   _context.Employees.Include(p => p.MonthlyModels).Where(p => p.Id == id).FirstOrDefault();
        }

        public SchedulerModel Gets(int id)
        {
            throw new NotImplementedException();
        }

        public SchedulerModel tester(int id, int int_month)
        {
            throw new NotImplementedException();
        }

       

        public double GetScheduledHours(int id, int month, int year)
        {
            throw new NotImplementedException();
        }
    }
}

