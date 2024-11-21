using HR.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HR.Domain.Classes
{
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly ClockInTime { get; set; }

        public TimeOnly? ClockOutTime { get; set; }

        [MaxLength(20)]
        public AttendanceStatus? Status { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}