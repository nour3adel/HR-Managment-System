using AutoMapper;
using HR.Domain.Classes;
using HR.Domain.DTOs.Employee;
using HR.Domain.DTOs.Payroll;

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
