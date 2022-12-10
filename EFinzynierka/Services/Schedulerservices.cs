using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EFinzynierka.Services
{
    public class Schedulerservices : ISchedulerservices
    {
        private readonly DbEFinzynierkaContext _context;

        public Schedulerservices(DbEFinzynierkaContext context)
        {
            _context = context;
        }

        public void GetUser(int id)
        {
            var user_id = _context.Employees.Find(id);
            //poprawic

        }

        public int LoadMonth(string MonthNameStr)
        {

            int month = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(MonthNameStr) + 1;
            return month;
        }

        public int PickMonth()
        {
            int dTyear = DateTime.Now.Year;
            int dMonth = DateTime.Now.Month;
            int DayInMonth = DateTime.DaysInMonth(dTyear, dMonth);
            return DayInMonth;
        }

        public List<ISchedulerservices> schedulerservices(List<MonthlyModel> monthlyModels)
        {
            var schedulerservices = new List<MonthlyModel>();
            foreach (var item in schedulerservices)
            {
                var model = new MonthlyModel();
                {
                    model.IdMonthly = item.IdMonthly;
                    model.DayInMonth = item.DayInMonth;
                    model.HoursScheduled = item.HoursScheduled;
                }
                schedulerservices.Add(model);

            }
            foreach (var item in schedulerservices)
            {
                _context.MonthlyModel.Add(item);
                _context.SaveChanges();
            }
            return schedulerservices.Cast<ISchedulerservices>().ToList();
        }

        public EmployeeModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Edit(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }

        public int Add(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Add(MonthlyModel monthlyModel)
        {
            _context.MonthlyModel.Add(monthlyModel);
            _context.SaveChanges();

            return monthlyModel.IdMonthly;
        }
    }
}