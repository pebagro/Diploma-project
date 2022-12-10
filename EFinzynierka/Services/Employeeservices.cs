using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections;

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
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return employee.IdEmployee;
        }

        public int Edit(EmployeeModel employee)
        {

            _context.Employees.Update(employee);
            _context.SaveChanges();

            return employee.IdEmployee;
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
    }
}
