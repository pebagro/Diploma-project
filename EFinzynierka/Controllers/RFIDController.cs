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
            using (var serialPort = new SerialPort("COM6", 9600))
            {

                serialPort.Open();
                Console.WriteLine("Port otwarty");
                // Czekaj na odczytanie karty RFID
                string data = serialPort.ReadLine();
                Console.WriteLine(data);
                serialPort.Close();
                Console.WriteLine("Port zamkniety" + "\n");
                // Parse the data to extract the RFID card ID
                int prefixLength = "RFID Tag UID: ".Length;
                string cardId = data.Substring(prefixLength).Trim();

                // Zwróć identyfikator karty RFID
                Console.WriteLine(cardId);
                cardId = cardId.Replace(" ", "");

                // Zwróć identyfikator karty RFID
                return Json(new { CardNumber = cardId });

            }
        }

        [HttpGet]
        public IActionResult LogRFIDCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogRFIDCard(RFIDLog log, string cardnumber)
        {

            //var employee = _context.Employees.SingleOrDefault(e => e.RFIDLog.RFIDCardID == cardnumber);
            var employee = _context.Employees.Where(e => e.RFIDLog.RFIDCardID == cardnumber).FirstOrDefault();
            if (employee != null)
            {
                log.Employee = employee;
            }

            if (employee == null)
            {
                ViewData["Info"] = "Brak przypisanej karty";
                return View();
            }

            log.EmployeeID = employee.Id;
            log.RFIDCardID = cardnumber;
            log.Timestamp = DateTime.UtcNow;
            log.IsEntry = log.IsEntry;

            ModelState.Remove("Employee");
            ModelState.Remove("RFIDCardID");
            if (ModelState.IsValid)
            {
                // Set the Timestamp and IsEntry properties

                // Add the RFIDLog object to the database
                _context.RFIDLogs.Add(log);
                await _context.SaveChangesAsync();



                bool hasShift = _context.Shifts.Any(s => s.EmployeeId == log.EmployeeID && (log.Timestamp >= s.StartTime && log.Timestamp <= s.EndTime) && (log.Timestamp.Date == s.StartTime.Date || log.Timestamp.Date == s.EndTime.Date));
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

            return View(log);
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
