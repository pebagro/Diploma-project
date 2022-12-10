using EFinzynierka.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFinzynierka.Models
{
    public class MonthList
    {

        [Key]
        public int Id { get; set; }

        public List<MonthlyModel> Items { get; set; }

    }
    public class MonthlyModel
    {
        [Key]
        public int Id { get; set; }


        public int Day { get; set; }
        public int HoursScheduled { get; set; }
        public string? ShiftType { get; set; }
        public int Month { get; set; }
        public int DayInMonth { get; set; }


        [ForeignKey("MonthList")]
        public int IdMonthly { get; set; }
        public MonthList MonthList { get; set; }


    }

}
