
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace EFinzynierka.Models
{

    public class EmployeeModel
    {

        [Key]
        public int IdEmployee { get; set; }
      
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        public string? Contract { get; set; }
        public string? Telephone { get; set; }

        [NotMapped, AllowNull]
        public DateOnly? DateOfBirth { get; set; }
        [NotMapped, AllowNull]
        public DateOnly? StartDate { get; set; }

        public List<MonthlyModel>? Monthly { get; set; }
    }

}
