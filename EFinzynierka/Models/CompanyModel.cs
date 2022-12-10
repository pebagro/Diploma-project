using System.ComponentModel.DataAnnotations;

namespace EFinzynierka.Models
{
    public class CompanyModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
    }
}
