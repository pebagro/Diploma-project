using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Text;
using System.Data;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;

namespace EFinzynierka.Controllers
{
    public class RFIDController : Controller
    {
        private readonly DbEFinzynierkaContext _context;

        public RFIDController(DbEFinzynierkaContext context)
        {
            _context = context;
        }

        public IActionResult ChooseEmployee()
        {
            // Pobierz listę pracowników z bazy danych
            var employees = _context.Employees.ToList();

            // Przekazujemy listę pracowników do widoku jako model
            return View(employees);
        }

        [HttpPost]
        public ActionResult ReadFromReader()
        {
            // Otwórz port szeregowy
            using (var serialPort = new SerialPort("COM4", 9600))
            {

                serialPort.Open();
                Console.WriteLine("Port otwarty");
                // Czekaj na odczytanie karty RFID
                string data = serialPort.ReadLine();
                Console.WriteLine(data);
                serialPort.Close();
                Console.WriteLine("Port zamkniety" + "\n");
                // Parse the data to extract the RFID card ID
                int prefixLength = "UID taga :".Length;
                string cardId = data.Substring(prefixLength).Trim();

                // Zwróć identyfikator karty RFID
                Console.WriteLine(cardId);
                cardId = cardId.Replace(" ", "");

                // Przypisz numer karty RFID do właściwości modelu
                var rfidLog = new RFIDLog { RFIDCardID = cardId };
                return Json(rfidLog);
            }
        }

        [HttpGet]
        public IActionResult LogRFIDCard()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogRFIDCard(RFIDLog log, string cardnumber, bool isEntry)
        {
            // Pobierz pracownika przypisanego do numeru karty RFID
            var employee = await _context.Employees.SingleOrDefaultAsync(e => e.CardInfo == log.RFIDCardID);

            if (employee == null)
            {
                ViewData["Info"] = "Brak przypisanej karty";
                return View();
            }


            // Sprawdź, czy pracownik ma dyżur w bieżącym czasie
            DateTime timestamp = DateTime.UtcNow;
            bool hasShift = await _context.Shifts.AnyAsync(s => s.EmployeeId == employee.Id && (timestamp >= s.StartTime && timestamp <= s.EndTime) && (timestamp.Date == s.StartTime.Date || timestamp.Date == s.EndTime.Date));

            // Utwórz nowy obiekt RFIDLog i przypisz go do pracownika
            log.Employee = employee;
            log.EmployeeID = employee.Id;
            log.RFIDCardID = employee.CardInfo;
            log.Timestamp = timestamp;
            log.IsEntry = isEntry;


            // Dodanie obiektu log do bazy danych
            _context.RFIDLogs.Add(log);
            try
            {
                await _context.SaveChangesAsync();
            
            }
catch (DbUpdateException ex)
{
    // Error handling
    Console.WriteLine(ex.InnerException.Message);
    throw;
}
            // Pętla która wyrzuca w konsoli informacje czy czas jest zaplanowany - pętla czysto testowa
            if (hasShift)
            {
                Console.WriteLine("Czas zaplanowany");
            }
            else
            {
                Console.WriteLine("Czas niezaplanowany");
            }

            return RedirectToAction("LogRFIDCard");
        }

        public IActionResult ViewLogs(int employeeId)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Id == employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            var logs = _context.RFIDLogs.Where(l => l.EmployeeID == employeeId).ToList();
            ViewBag.EmployeeName = employee.Name;
            return View(logs);
        }

        public IActionResult ShowRFIDLogs(int employeeId, DataView dataView)
        {
            // Pobierz pracownika o podanym id
            var employee = _context.Employees.SingleOrDefault(e => e.Id == employeeId);

            // Pobierz logi RFID dla tego pracownika
            var logs = _context.RFIDLogs.Where(l => l.EmployeeID == employeeId).ToList();

            // Dla każdego logu sprawdź, czy czas jest zaplanowany w shiftach pracownika
            foreach (var log in logs)
            {
                bool hasShift = _context.Shifts.Any(s => s.EmployeeId == log.EmployeeID && (log.Timestamp >= s.StartTime && log.Timestamp <= s.EndTime) && (log.Timestamp.Date == s.StartTime.Date || log.Timestamp.Date == s.EndTime.Date));
                if (hasShift)
                {
                    ViewData["hasShift"] = "Na czas";

                }
                else
                {
                    ViewData["hasShift"] = "Spóźniony";
                }
            }

            // Przekazujemy pracownika i logi do widoku jako modele
            return View(new RFIDLogsViewModel { Employee = employee, Logs = logs });
        }
    }

}
