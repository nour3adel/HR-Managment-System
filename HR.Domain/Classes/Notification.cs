using HR.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HR.Domain.Classes
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }


        [MaxLength(500)]
        public string? MessageContent { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(20)]
        public NotificationType Type { get; set; }

        // Navigation property
        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee? Employee { get; set; }

    }
}