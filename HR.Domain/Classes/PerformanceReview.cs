using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HR.Domain.Classes
{
    public class PerformanceReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(500)]
        public string? Review { get; set; }

        [Required]
        [Range(1, 10)]
        public int RatingScore { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        // Navigation property
        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee? Employee { get; set; }
    }

}