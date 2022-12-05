using EFinzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFinzynierka.Controllers
{
    public class SchedulerComponent : Controller
    {
        private readonly DbEFinzynierkaContext _context;

        public SchedulerComponent(DbEFinzynierkaContext context)
        {
            _context = context;
        }

        DateTime dT = DateTime.Now;
        
        public IActionResult Index()
        {
            return View();
        }

       /* [HttpPost]
        public ActionResult Index(
            [Bind("Day,Month,Year,DaysInMonth")] SchedulerModel schedulermodel)
        {
            int int_dTyear = dT.Year;
            schedulermodel.Year = int_dTyear;
            int int_dTmonth = dT.Month;
            schedulermodel.Month = int_dTmonth;
            string str_dTmonth = dT.ToString("MMMM");
            schedulermodel.Months = str_dTmonth;
            schedulermodel.DaysInMonth = DateTime.DaysInMonth(int_dTyear, int_dTmonth);


            *//*List<SchedulerModel> workingHours = new List<SchedulerModel>();
            SchedulerModel hoursList = new SchedulerModel();
            hoursList.DaysInMonth = DateTime.DaysInMonth(dT.Year, dT.Month);
*//*
           
            return View();

        }*/

       /*public ActionResult DisplayDays()
        {


            int[] food = new int[(DateTime.DaysInMonth(dT.Year, dT.Month))];
            food[0] = 32;

            foreach (var item in food)
            {
                System.Console.WriteLine(item);            
            }


         *//*   for (int i = 1; i <= (DateTime.DaysInMonth(dT.Year,dT.Month)); i++)
            {
                days[1].= i;

            }*//*
            return food;
        }
*/

        // GET: SchedulerComponent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchedulerComponent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SchedulerComponent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SchedulerComponent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SchedulerComponent/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SchedulerComponent/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}
