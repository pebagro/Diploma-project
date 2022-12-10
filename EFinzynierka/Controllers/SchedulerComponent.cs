using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
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

        /// <summary>
        /// //////////////
        /// </summary>
        

        DateTime dT = DateTime.Now;
        
        public IActionResult Index()
        {
            return View();
        }


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
