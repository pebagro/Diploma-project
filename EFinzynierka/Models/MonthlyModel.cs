using EFinzynierka.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFinzynierka.Models
{
    public class MonthlyModel
    {

        public int Id { get; set; }
        public int Day { get; set; }
        public int HoursScheduled { get; set; }
        [NotMapped]
        public virtual EmployeeModel Employee { get; set; }
        public  SchedulerModel Schedules { get; set; }
        
    }
}
