using EFinzynierka.Controllers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace EFinzynierka.Models
{

    public class SchedulerModel 
    {
        [Key]
        public int IdScheduler { get; set; }
        public int DaysInMonth { get; set; }
        public int Duration { get; set; }
    }
}
