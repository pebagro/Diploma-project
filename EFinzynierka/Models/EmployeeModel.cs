using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EFinzynierka.Models
{

    public class EmployeeModel
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Contract { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string CardInfo { get;set; } = null!;
        public int AuthLevel { get; set; } = 0;

        public List<Shift> Shifts { get; set; } = null!;

        public RFIDLog RFIDLog { get; set; } = null!;
        
        public DateTime DateOfBirth { get; set; }
        public DateTime StartDate { get; set; }

    }

}
