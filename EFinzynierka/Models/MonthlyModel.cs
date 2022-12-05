using EFinzynierka.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MonthlyModel : SchedulerModel
{
    [Key][ForeignKey("TypeId")]
    public virtual EmployeeModel? EmployeeModel { get; set; }
    
    public int TypeId { get; set; }
    [ForeignKey("TypeId")]
    public virtual SchedulerModel? SchedulerModel { get; set; }
    
    //public virtual List<YearDTO> Items { get; set; }
}

public class YearDTO : MonthlyModel
{
    [ForeignKey("TypeId")]
    public virtual MonthlyModel? MonthlyModels { get; set; }
    public virtual MonthDTO? MonthDTO { get; set; }

    public DateTime Year { get; set; }
    public virtual MonthDTO January { get; set; }
    public virtual MonthDTO February { get; set; }
    public virtual MonthDTO March { get; set; }
    public virtual MonthDTO April { get; set; }
    public virtual MonthDTO May { get; set; }
    public virtual MonthDTO June { get; set; }
    public virtual MonthDTO July { get; set; }
    public virtual MonthDTO August { get; set; }
    public virtual MonthDTO September { get; set; }
    public virtual MonthDTO October { get; set; }
    public virtual MonthDTO November { get; set; }
    public virtual MonthDTO December { get; set; }

}

public class MonthDTO : YearDTO
{

    
    public virtual YearDTO? YearDTOs { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public int DayInMonth { get; set; }
    public virtual List<ScheduledDay> scheduledDays { get; set; }

}

public class ScheduledDay :MonthDTO
{
    public virtual MonthDTO MonthDTOs { get; set; }
    public int Day { get; set; }
    public int HoursScheduled { get; set; }
    public string? ShiftType { get; set; }
}