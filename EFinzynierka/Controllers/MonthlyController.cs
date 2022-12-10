
using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EFinzynierka.Controllers
{
    public class MonthlyController : Controller
    {
        private readonly IEmployeeservices _Employeeservices;
        private readonly ISchedulerservices _Schedulerservices;
        public MonthlyController(IEmployeeservices Employeeservices, ISchedulerservices Schedulerservices)
        {
            _Employeeservices = Employeeservices;
            _Schedulerservices = Schedulerservices;
        }

        /// <summary>
        /// 
        /// </summary>


        // GET: MonthlyController
        [HttpGet]
        public ActionResult Index()
        {
            List<EmployeeModel> list = new List<EmployeeModel>();
            var liste = _Employeeservices.GetAll();
            list.AddRange(liste);
            
            return View(list);
        }

        // GET: MonthlyController/Details/5
        public ActionResult List()
        {
            var employeeList = _Employeeservices.GetAll;
            TempData["employeeList"] = "employeeList";
            return View(employeeList);
        }


        // GET: MonthlyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MonthlyController/Create
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

        // GET: MonthlyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MonthlyController/Edit/5
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

        // GET: MonthlyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MonthlyController/Delete/5
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

        public ActionResult GetPartial(int id)
        {
            
            var dayInMonth = _Schedulerservices.PickMonth();
            MonthlyModel model = new MonthlyModel();
            model.DayInMonth = dayInMonth;
            return PartialView("_GetMonth", model);
        }

        public IActionResult HourSaving(int id, List<MonthList> months)
        {
            var schedulerservices = new List<MonthlyModel>();
            foreach (var item in schedulerservices)
            {
                var model = new MonthlyModel();
                {
                   // model.EmployeeModel = item.EmployeeModel;
                    model.IdMonthly = item.IdMonthly;
                    model.DayInMonth = item.DayInMonth;
                    model.HoursScheduled = item.HoursScheduled;
                }
                _Schedulerservices.Add(model);

            }


            return View();
        }
    
    }
    
}
