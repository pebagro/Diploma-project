using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
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
            var users = _context.Employees.Find(id);
            return users;
        }

        public async Task<int> GetDaysOffLeftAsync(int employeeId)
        {
            var employee = await _context.Employees
        .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                return 0;
            }

            if (employee.Contract == "UoP")
            {
                var currentYear = DateTime.Now.Year;
                var contractStartDate = employee.StartDate;
                var daysWorked = (DateTime.Now - contractStartDate).TotalDays;
                var yearStartDate = new DateTime(currentYear, 1, 1);
                var daysInYear = (DateTime.IsLeapYear(currentYear) ? 366 : 365);
                var daysInContract = (DateTime.Now - contractStartDate).TotalDays;
                var daysOffPerYear = 20;

                if (daysInContract < daysInYear)
                {
                    var daysOffPerDay = daysOffPerYear / daysInYear;
                    var daysOffLeft = (int)(daysWorked * daysOffPerDay);
                    return daysOffLeft;
                }
                else
                {
                    return daysOffPerYear;
                }
            }
            else
            {
                return 0;
            }
        }



        public double GetScheduledHours(int id, int month, int year)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeSummary> GetEmployeeSummaryAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<(int years, int months)> GetWorkExperienceAsync(int employeeId)
        {
            throw new NotImplementedException();
        }
    }

}