using HR.Domain.DTOs.Department;
using HR.Services.Bases;

namespace HR.Services.Services
{
    public interface IDepartmentServices
    {
        public Task<Response<IEnumerable<GetDepartmentDTO>>> GetAllDepartments();
        public Task<Response<GetDepartmentDTO>> GetDepartmentByID(int id);
    }
}
