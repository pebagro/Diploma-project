using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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

            return employee.IdEmployee;
        }

        public int Delete(int id)
        {

            //var entity = _context.Employees.Find(x => x.Id == id);
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return employee.IdEmployee;
        }

        /*[HttpGet]
        public employee Edit(Employee employee)
        {

            var employeeToUpdate = _context.Employees.Find(employee.IdEmployee);
            return employeeToUpdate;
        }*/

        public int Edit(EmployeeModel employee)
        {

            _context.Employees.Update(employee);
            _context.SaveChanges();

            return employee.IdEmployee;
        }


        public List<EmployeeModel> GetAll()
        {
            _context.Employees.ToList();
            return _context.Employees.ToList();
        }

        public EmployeeModel Get(int id)
        {
            var cokolwiek = _context.Employees.Find(id);
            return cokolwiek;
        }

        /* public Employee List(Employee employee)
         {
             var 
         }*/

    }
}

/*public int Add(EmployeeModel employee)
{
    throw new NotImplementedException();
}*/

/*        public int Edit(EmployeeModel employee)
        {
            var employee = await _context.Employees.FindAsync(id);
            employee = _context.Employees.Add(employee);
            return (employee);
        }


        public IActionResult Index()
        {
            return 1;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return 1;
        }

        public IActionResult Add(EmployeeModel employee)
        {
            return 1;
        }

        [HttpGet]
        public IActionResult List_employee()
        {
            var employee = _context.GetAll();
            return List<employee>;
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employee.Find(id);
            _context.Employee.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Get(int id)
        {
            var employee = _context.Employee.Find(id);

            return employee;
        }

        EmployeeModel IEmployeeservices.Add(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }

        public EmployeeModel EmployeeExist(EmployeeModel employee)
        {
            var employeer = _context.Employee.Find(id);

            return employeer;
        }

        EmployeeModel IEmployeeservices.Edit(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }

        public EmployeeModel Delete(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }
    }
}*/