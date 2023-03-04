namespace EFinzynierka.Models
{
    public class EmployeeSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int WeeklyScheduledHours { get; set; }
        public int LateArrivals { get; set; }
        public int NoClockIn { get; set; }
        public int WorkExperienceYears { get; set; }
        public int WorkExperienceMonths { get; set; }
        public int DaysOffLeft { get; set; }
        public string Contract { get; set; }
    }
}
