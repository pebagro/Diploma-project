using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;

namespace EFinzynierka.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly DbEFinzynierkaContext _context;

        public EmployeeDataService(DbEFinzynierkaContext context)
        {
            _context = context;
        }

        public EmployeeModel GetEmployeeData(int employeeId)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == employeeId);
        }

        public IEnumerable<EmployeeModel> GetAllEmployeeData()
        {

            return _context.Employees.Select(e => new EmployeeModel
            {
                Id = e.Id,
                Name = e.Name,
                Surname = e.Surname
            });
        }

        public double GetWeeklyScheduledHours(int employeeId)
        {
            var employeeShifts = _context.Shifts
            .Where(s => s.EmployeeId == employeeId).ToList();
            var weeklyScheduledHours = employeeShifts.Sum(s => (s.EndTime - s.StartTime).TotalHours);
            return weeklyScheduledHours;
        }

        public int GetLateArrivalsCount(int employeeId)
        {
            var lateArrivals = _context.Shifts
                .Where(s => s.EmployeeId == employeeId
                    && s.StartTime > s.StartTime)
                .Count();
            return lateArrivals;
        }

        public int GetNoClockInsCount(int employeeId)
        {
            var noClockIns = _context.RFIDLogs
                .Where(r => r.EmployeeID == employeeId
                    && r.IsEntry == false)
                .Count();
            return noClockIns;
        }


        public EmployeeSummary GetSummary(int employeeId)
        {
            //Pętla startowa - w momencie kiedy pracownik ma Id = 0 to wyświetla pierwszego znalezionego pracownika
            if (employeeId == 0)
            {
                employeeId = _context.Employees.FirstOrDefault().Id;
            }

            // Wyciągnięcie z bazy danych informacji o pracowniku
            var employee = _context.Employees
             .Include(s => s.Shifts)
             .Include(r => r.RFIDLog)
             .Where(e => e.Id == employeeId)
             .FirstOrDefault();


            //Przypisanie zmiennych do funkcji 
            var weeklyScheduledHours = GetWeeklyScheduledHours(employeeId);
            var lateArrivals = GetLateArrivalsCount(employeeId);
            var noClockIns = GetNoClockInsCount(employeeId);
            var employeeWorkTime = GetWorkExperience(employeeId);
            var daysOffLeft = GetDaysOffLeftAsync(employeeId);

            //Przypisanie zmiennych do nowego obiektu typu EmployeeSummary
            var employeeSummary = new EmployeeSummary()
            {
                Id = employeeId,
                Name = employee.Name,
                Surname = employee.Surname,
                Contract = employee.Contract,
                WeeklyScheduledHours = (int)weeklyScheduledHours,
                LateArrivals = lateArrivals,
                NoClockIn = noClockIns,
                WorkExperienceYears = employeeWorkTime.WorkExperienceYears,
                WorkExperienceMonths = employeeWorkTime.WorkExperienceMonths,
                DaysOffLeft = daysOffLeft
            };
            //Zwrócenie obiektu z danymi do widoku
            return employeeSummary;
        }

        public double GetWeeklyHours(int employeeId, DateTime startDate, DateTime endDate)
        {
            return _context.Shifts
                .Where(s => s.EmployeeId == employeeId && s.StartTime >= startDate && s.EndTime <= endDate)
                .Sum(s => (s.EndTime - s.StartTime).TotalHours);
        }

        public double GetMonthlyHours(int employeeId, int month, int year)
        {
            return _context.Shifts
                .Where(s => s.EmployeeId == employeeId && s.StartTime.Month == month && s.StartTime.Year == year)
                .Sum(s => (s.EndTime - s.StartTime).TotalHours);
        }

        public int GetRFIDLogins(int employeeId, DateTime startDate, DateTime endDate)
        {
            return _context.RFIDLogs
                .Count(r => r.EmployeeID == employeeId && r.Timestamp >= startDate && r.Timestamp <= endDate);
        }


        public (int WorkExperienceYears, int WorkExperienceMonths) GetWorkExperience(int employeeId)
        {
            var employee = _context.Employees
                .FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                return (0, 0);
            }

            var workExperience = DateTime.Now - employee.StartDate;
            var years = workExperience.Days / 365;
            var months = workExperience.Days % 365 / 30;

            return (years, months);
        }

        /*public int CalculateVacationDays(int employeeId)
        {
            var employee = GetEmployeeData(employeeId);

            // Jeśli pracownik nie istnieje, zwróć 0
            if (employee == null)
            {
                return 0;
            }

            // Sprawdź, czy pracownik jest zatrudniony na umowę o pracę
            if (employee.Contract != "UoP")
            {
                return -1;
            }

            // Oblicz liczbę dni urlopu na podstawie stażu pracy
            var workExperience = CalculateWorkExperience(employee);
            int vacationDays = 0;
            if (workExperience < 6)
            {
                vacationDays = 20;
            }
            else if (workExperience >= 6 && workExperience < 10)
            {
                vacationDays = 26;
            }
            else if (workExperience >= 10)
            {
                vacationDays = 26 + 2 * (workExperience - 10);
            }

            return vacationDays;
        }*/

        private int CalculateWorkExperience(EmployeeModel employee)
        {
            var startDate = employee.StartDate;
            var currentDate = DateTime.Now;
            var workExperience = (currentDate.Year - startDate.Year) * 12 + (currentDate.Month - startDate.Month);
            return workExperience;
        }

        public int GetDaysOffLeftAsync(int employeeId)
        {
            var employee = _context.Employees.FindAsync(employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"Pracownik o ID {employeeId} nie znaleziony");
            }

            if (employee.Result.Contract == "UoP")
            {
                var currentYear = DateTime.Now.Year;
                var contractStartDate = employee.Result.StartDate;
                var daysWorked = (DateTime.Now - contractStartDate).TotalDays;
                var monthsWorked = (int)Math.Floor(daysWorked / 30.44); // średnia liczba dni w miesiącu
                var daysInYear = (DateTime.IsLeapYear(currentYear) ? 366 : 365); // zmienna typu dummy do testów
                var daysOffPerYear = 20;
                var daysOffPerMonth = (int)Math.Floor(daysOffPerYear / 12.0);

                if (monthsWorked < 12)
                {
                    var daysOffLeft = (int)(monthsWorked * daysOffPerMonth);
                    return daysOffLeft;
                }
                else
                {
                    return daysOffPerYear;
                }
            }
            else
            // Przy umowie innej niz UoP metoda zwraca nam -1
            // co jest informacją dla widoku że jest to inny rodzaj umowy
            {
                return -1;
            }
        }

    }
}