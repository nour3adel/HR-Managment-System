
using HR.Domain.DTOs.Autherization;
using HR.Services.Bases;

namespace HR.Services.Services
{
    public interface IAutherizationServices
    {
        public Task<Response<string>> AddNewRole(string roleName);
        public Task<Response<string>> AddRoleToEmployee(string employeeID, string role);

        public Task<Response<string>> RemoveRole(string roleName);
        public Task<Response<string>> EditRoleName(EditRoleDTO editRoleDTO);

        public Task<Response<IEnumerable<string>>> GetAllRoles();
        public Task<Response<IEnumerable<string>>> GetRolesForUser(string userId);
    }
}
