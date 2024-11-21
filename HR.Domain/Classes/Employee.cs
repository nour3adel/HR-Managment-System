using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HR.Domain.Classes
{

    public class Employee : IdentityUser
    {

        [Column(TypeName = "Money")]
        public decimal Salary { get; set; }

        [Required]
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        public Department? Department { get; set; }

    }
}