using System.ComponentModel.DataAnnotations;

namespace EFinzynierka.Models
{
    public class EmployeeModel
    {
        [Key]
        public int id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime CreatedDate { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Contract { get; set; }
        public string? Telephone { get; set; }
        
    }
}
