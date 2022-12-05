using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace EFinzynierka.Models
{

    public class EmployeeModel// : SchedulerModel
    {
        [Key]
        public int IdEmployee { get; set; }
        [ForeignKey("IdScheduler")]
        public virtual SchedulerModel SchedulerModels { get; set; }
        [ForeignKey("IdScheduler")]
        public virtual MonthlyModel MonthlyModel { get; set; } 

        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        public string? Contract { get; set; }
        public string? Telephone { get; set; }
        [NotMapped, AllowNull]
        public DateOnly DateOfBirth { get; set; }
        [NotMapped, AllowNull]
        public DateOnly? StartDate { get; set; }

    }

}
