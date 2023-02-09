using System.ComponentModel.DataAnnotations;

namespace EFinzynierka.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
    }
}
