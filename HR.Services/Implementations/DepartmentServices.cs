using HR.Domain.DTOs.Department;
using HR.Infrastructure.Interfaces;
using HR.Services.Bases;
using HR.Services.Services;

namespace HR.Services.Implementations
{
    public class DepartmentServices : ResponseHandler, IDepartmentServices
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentServices(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<IEnumerable<GetDepartmentDTO>>> GetAllDepartments()
        {
            var result = await _repository.GetAllDepartments();
            if (result == null) return NotFound<IEnumerable<GetDepartmentDTO>>("No Departments Found");
            var departmentDTOs = result.Select(department => new GetDepartmentDTO
            {

                Id = department.Id,
                Name = department.Name,
                EmployeeNames = department.Employees.Select(x => x.FullName).ToArray()

            }).ToList();
            return Success<IEnumerable<GetDepartmentDTO>>(departmentDTOs);
        }

        public async Task<Response<GetDepartmentDTO>> GetDepartmentByID(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound<GetDepartmentDTO>($"No Department With ID = {id}");
            var departmentDTOs = new GetDepartmentDTO
            {

                Id = result.Id,
                Name = result.Name,
                EmployeeNames = result.Employees.Select(x => x.FullName).ToArray()

            };
            return Success<GetDepartmentDTO>(departmentDTOs);
        }
    }
}
