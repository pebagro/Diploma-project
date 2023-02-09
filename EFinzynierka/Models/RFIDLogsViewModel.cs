namespace EFinzynierka.Models
{
    public class RFIDLogsViewModel
    {
        public EmployeeModel Employee { get; set; }
        public List<RFIDLog> Logs { get; set; }
        public bool IsScheduled { get; set; }
    }
}