using EFinzynierka.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Shift
{
    public int Id { get; set; }
    
    [StartTimeNotLaterThanEndTime]
    public DateTime StartTime { get; set; }

    [Range(typeof(TimeSpan), "00:00:00", "12:00:00", ErrorMessage = "Zaplanowana zmiana może trwać maksymalnie 12h.")]
    public DateTime EndTime { get; set; }

    [ForeignKey("EmployeeId")]
    public int EmployeeId { get; set; }
    public virtual EmployeeModel Employee { get; set; }
}