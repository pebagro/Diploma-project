using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EFinzynierka.Models
{
    public class Register
    {
        
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        [NotMapped]
        public DateOnly Birthday { get; set; }
        [EmailAddress]
        [Required]public string Email { get; set; }
        [Required]public string Password { get; set; }
    }
}
