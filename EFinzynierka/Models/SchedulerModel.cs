using System.ComponentModel.DataAnnotations.Schema;

namespace EFinzynierka.Models
{

    public class SchedulerModel 
    {
        public int Id { get; set; }

        public int DaysInMonth { get; set; } = 0;
        public int Month { get; set; }
        public string Month_name { get; set; } = null!;
        public List<MonthlyModel> MonthlyModels { get; set; } = null!;

        public virtual EmployeeModel Employee { get; set; }
        public int EmployeeId { get; set; }
        public virtual MonthlyModel Monthly { get; }
        public int MonthlyId { get; set; }
        
        
    }
}
