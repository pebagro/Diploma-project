using EFinzynierka.Models;

namespace EFinzynierka.Models
{
    public class RFIDLog
    {
        
        public int ID { get; set; }
        public string RFIDCardID { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsEntry { get; set; }
        public int EmployeeID { get; set; }
        public virtual EmployeeModel Employee { get; set; }
    }
}