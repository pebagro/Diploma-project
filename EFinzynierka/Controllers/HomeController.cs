using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using EFinzynierka;

namespace EFinzynierka.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbEFinzynierkaContext _context;

        

        public HomeController(ILogger<HomeController> logger, DbEFinzynierkaContext context)
        {
            
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Pobierz dane o odczytanych kartach RFID z bazy danych
            var logs = _context.RFIDLogs.ToList();

            // Oblicz liczbę odczytanych kart RFID oznaczonych jako wejścia do pracy
            var entryCount = logs.Count(l => l.IsEntry);

            // Przekaz dane do widoku "Index"
            return View(entryCount);
            

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}