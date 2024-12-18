using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HR.Domain.Classes
{
    public class Payroll
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        [Range(2020, 2024)]
        public int Year { get; set; }

        [Column(TypeName = "Money")]
        public decimal Bonus { get; set; }

        [Column(TypeName = "Money")]
        public decimal Deduction { get; set; }

        // Navigation property
        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee? Employee { get; set; }
    }
}