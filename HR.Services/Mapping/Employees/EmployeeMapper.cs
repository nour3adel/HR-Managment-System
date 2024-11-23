using AutoMapper;
using HR.Domain.Classes;
using HR.Domain.DTOs.Employee;

namespace HR.Services.Mapping.Employees
{
    public partial class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            AddUserMapping();
        }
        public void AddUserMapping()
        {
            CreateMap<RegisterUserDTO, Employee>();

        }
    }
}
