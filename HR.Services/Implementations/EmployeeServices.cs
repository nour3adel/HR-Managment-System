using AutoMapper;
using HR.Domain.Classes;
using HR.Domain.DTOs.Employee;
using HR.Services.Services;
using Microsoft.AspNetCore.Identity;

namespace HR.Services.Implementations
{
    public class EmployeeServices : IEmployeeServices
    {
        public UserManager<Employee> _userManager;
        private readonly IMapper _mapper;
        public EmployeeServices(UserManager<Employee> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<string> RegisterUser(RegisterUserDTO user)
        {
            //check if email is exist
            Employee existemail = await _userManager.FindByEmailAsync(user.Email);
            if (existemail != null) return "Email Exist";
            //check if username is exist
            Employee existusername = await _userManager.FindByNameAsync(user.UserName);
            if (existusername != null) return "Username Exist";


            Employee employee = _mapper.Map<Employee>(user);

            //create user
            IdentityResult createdUser = await _userManager.CreateAsync(employee, user.password);
            if (!createdUser.Succeeded)
            {
                return "Creation Failed";
            }
            //add role
            await _userManager.AddToRoleAsync(employee, "User");

            return "Success";

        }
    }
}
