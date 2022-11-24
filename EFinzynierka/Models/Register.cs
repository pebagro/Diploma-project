using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFinzynierka.Models
{
    public class Register
    {
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }

        [EmailAddress]
        [Required]public string Email { get; set; }
        [Required]public string Password { get; set; }
    }
}
