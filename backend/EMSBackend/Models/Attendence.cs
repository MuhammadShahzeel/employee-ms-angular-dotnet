namespace EMSBackend.Models
{
    public class Attendence
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int Type { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }

    public enum AttendenceType
    {
        Present = 1,
        Leave = 2,
          
    }

} 
