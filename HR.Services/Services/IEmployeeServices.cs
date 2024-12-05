using HR.Domain.DTOs.Employee;
using HR.Services.Bases;

namespace HR.Services.Services
{
    public interface IEmployeeServices
    {
        public Task<Response<string>> RegisterUser(RegisterUserDTO user);
        public Task<Response<string>> ChangePassword(ChangePasswordDTO pass);
        public Task<Response<string>> logout();
        public Task<Response<string>> EditUser(EditCutomerDTO user);
        public Task<Response<IEnumerable<SelectCustomerDTO>>> GetAllEmployees();
        public Task<Response<IEnumerable<SelectCustomerDTO>>> GetAllManagers();
        public Task<Response<IEnumerable<SelectCustomerDTO>>> GetAllUsers();
        public Task<Response<SelectCustomerDTO>> GetCustomerByID(string id);
        public Task<Response<string>> DeleteUser(string id);
    }
}
