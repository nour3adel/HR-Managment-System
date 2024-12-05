using System.ComponentModel.DataAnnotations;

namespace HR.Domain.DTOs.Employee
{
    public class RegisterUserDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public decimal Salary { get; set; }
        public string PhoneNumber { get; set; }
        public string password { get; set; }
        public string ConfirmPassword { get; set; }
        public int DepartmentId { get; set; }
        public bool IsAdmin { get; set; } = false;

    }
}
