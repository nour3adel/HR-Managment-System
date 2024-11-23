using System.ComponentModel.DataAnnotations;

namespace HR.Domain.DTOs.Employee
{
    public class RegisterUserDTO
    {
        [MaxLength(100)]
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Inavlid Email")]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public string? Address { get; set; }
        [Phone(ErrorMessage = "Inavlid Phone")]
        public string PhoneNumber { get; set; }
        [Required]
        public string password { get; set; }
        public string ConfirmPassword { get; set; }

        public int DepartmentId { get; set; }

    }
}
