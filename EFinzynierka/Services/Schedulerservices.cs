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

        public int Add(MonthlyModel monthlyModel)
        {

            _context.MonthlyModel.Add(monthlyModel);
            _context.SaveChanges();

            return monthlyModel.Id;
        }

        public int AddScheduler(SchedulerModel schedulerModel)
        {

            _context.Scheduler.Add(schedulerModel);
            _context.SaveChanges();

            return schedulerModel.Id;
        }

        public void GetUser(int id)
        {

            var user_id = _context.Employees.Find(id);
            //poprawic

        }

        public SchedulerModel LoadMonth(int id,int month)
        {
   
            SchedulerModel schedulerModel = new SchedulerModel();
            int dTyear = DateTime.Now.Year;
            int daysinmonth  = DateTime.DaysInMonth(dTyear, month+1);
            int month_choosed = month;

            schedulerModel.Month = month;
            schedulerModel.DaysInMonth = daysinmonth;
                

            
            
            // +1 w month z powodu tego iż funkcja DateTime ma 13 miesięcy - blank month 
            // o wartości NULL

            return schedulerModel;
        }

        public SchedulerModel PickMonth()
        {
            int dTyear = DateTime.Now.Year;
            int dMonth = DateTime.Now.Month;
            int DayInMonth = DateTime.DaysInMonth(dTyear, dMonth);
            SchedulerModel schedulerModel = new SchedulerModel();
            schedulerModel.DaysInMonth = DayInMonth;
            CultureInfo ci = new CultureInfo("pl-PL");
            schedulerModel.Month_name = DateTime.Now.ToString("MMMM", ci).ToUpperInvariant();
            return schedulerModel;
        }

       


        public List<ISchedulerservices> schedulerservices(List<MonthlyModel> monthlyModels)
        {
            var schedulerservices = new List<MonthlyModel>();
            foreach (var item in schedulerservices)
            {
                var model = new MonthlyModel();
                {
                    model.Id = item.Id;
                //    model.DayInMonth = item.DayInMonth;
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


        public void Others(int id)
        {
            throw new NotImplementedException();
        }

        public void Otherss(int id)
        {
            throw new NotImplementedException();
        }

        public SchedulerModel Gets(int id)
        {
            var cokolwiek = _context.Scheduler.Find(id);
            return cokolwiek;
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