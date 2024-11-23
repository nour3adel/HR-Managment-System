using HR.Domain.DTOs.Employee;

namespace HR.Services.Services
{
    public interface IEmployeeServices
    {
        public Task<string> RegisterUser(RegisterUserDTO user);
    }
}
