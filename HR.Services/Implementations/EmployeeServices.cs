using AutoMapper;
using HR.Domain.Classes;
using HR.Domain.Classes.Identity;
using HR.Domain.DTOs.Employee;
using HR.Services.Bases;
using HR.Services.Services;
using Jose;
using Microsoft.AspNetCore.Identity;

namespace HR.Services.Implementations
{
    public class EmployeeServices : ResponseHandler, IEmployeeServices
    {
        #region Fields

        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<Employee> _signin;
        private readonly JwtSettings _jwtSettings;
        #endregion

        #region Constructor
        public EmployeeServices(UserManager<Employee> userManager, SignInManager<Employee> signin, JwtSettings jwtSettings, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signin = signin;
            _jwtSettings = jwtSettings;
            _roleManager = roleManager;
        }
        #endregion

        #region Register User
        public async Task<Response<string>> RegisterUser(RegisterUserDTO user)
        {
            // Validate email
            if (await _userManager.FindByEmailAsync(user.Email) != null)
            {
                return BadRequest<string>("Email is already registered.");
            }

            // Validate username
            if (await _userManager.FindByNameAsync(user.UserName) != null)
            {
                return BadRequest<string>("Username is already taken.");
            }
            if (user.password != user.ConfirmPassword) return BadRequest<string>("Confirm Password is Wrong");

            // Map DTO to Employee entity

            //Employee newEmployee = _mapper.Map<Employee>(user);
            Employee newEmployee = new Employee()
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                DepartmentId = user.DepartmentId,
                Position = user.Position,
                Salary = user.Salary

            };

            // Create the user
            IdentityResult creationResult = await _userManager.CreateAsync(newEmployee, user.password);


            if (!creationResult.Succeeded)
            {
                var errors = string.Join(", ", creationResult.Errors.Select(e => e.Description));
                return BadRequest<string>($"User creation failed: {errors}");
            }

            // Assign role to the user

            var role = user.IsAdmin ? "Manager" : "User";
            // Check if the role exists
            if (!await _roleManager.RoleExistsAsync(role))
            {
                return NotFound<string>("Specified role does not exist.");
            }


            var roleResult = await _userManager.AddToRoleAsync(newEmployee, role);
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return BadRequest<string>($"Failed to assign role: {errors}");
            }




            return Created("User registration successful.");
        }

        #endregion


        #region Change Password
        public async Task<Response<string>> ChangePassword(ChangePasswordDTO pass)
        {
            var user = await _userManager.FindByIdAsync(pass.employeeid);
            if (user == null)
                return BadRequest<string>("User not found");

            var result = await _userManager.ChangePasswordAsync(user, pass.oldpassword, pass.newpassword);
            if (result.Succeeded)
                return Success<string>("Password changed successfully");

            return BadRequest<string>("Failed to change password");
        }


        #endregion



        #region Logout
        public async Task<Response<string>> logout()
        {
            if (!_signin.Context.User.Identity.IsAuthenticated)
                return BadRequest<string>("No user is currently logged in");

            await _signin.SignOutAsync();
            return Success<string>("Logout successful");
        }

        #endregion

        #region Edit Employee
        public async Task<Response<string>> EditUser(EditCutomerDTO userDto)
        {
            // Validate input DTO
            if (userDto == null)
                return BadRequest<string>("Invalid user data.");

            // Validate Customer ID
            var employee = await _userManager.FindByIdAsync(userDto.id);
            if (employee == null)
                return NotFound<string>("User not found.");

            // Update properties
            employee.Email = userDto.email?.Trim();
            employee.PhoneNumber = userDto.phonenumber?.Trim();
            employee.UserName = userDto.username?.Trim();
            employee.FullName = userDto.fullname?.Trim();
            employee.Address = userDto.address?.Trim();

            // Attempt to update the user
            var result = await _userManager.UpdateAsync(employee);

            if (result.Succeeded)
            {
                return Updated<string>("Employee data updated successfully.");
            }

            // Aggregate errors and return detailed response
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest<string>($"Failed to update employee: {errors}");

        }
        #endregion

        #region Get All Users
        public async Task<Response<IEnumerable<SelectCustomerDTO>>> GetAllUsers()
        {
            // Retrieve all users in the "customer" role
            var users = await _userManager.GetUsersInRoleAsync("User");
            if (users == null || !users.Any())
                return NotFound<IEnumerable<SelectCustomerDTO>>("No customers found.");

            // Map users to DTOs using LINQ
            var customerDTOs = users
                .Select(user => new SelectCustomerDTO
                {
                    id = user.Id,
                    fullname = user.FullName,
                    address = user.Address,
                    username = user.UserName,
                    email = user.Email,
                    phonenumber = user.PhoneNumber
                })
                .ToList();

            // Return the mapped DTOs
            return Success<IEnumerable<SelectCustomerDTO>>(customerDTOs);
        }
        #endregion

        #region Get All Managers
        public async Task<Response<IEnumerable<SelectCustomerDTO>>> GetAllManagers()
        {
            // Retrieve all users in the "Manager" role
            var users = await _userManager.GetUsersInRoleAsync("Manager");
            if (users == null || !users.Any())
                return NotFound<IEnumerable<SelectCustomerDTO>>("No Manager found.");

            // Map users to DTOs using LINQ
            var customerDTOs = users
                .Select(user => new SelectCustomerDTO
                {
                    id = user.Id,
                    fullname = user.FullName,
                    address = user.Address,
                    username = user.UserName,
                    email = user.Email,
                    phonenumber = user.PhoneNumber
                })
                .ToList();

            // Return the mapped DTOs
            return Success<IEnumerable<SelectCustomerDTO>>(customerDTOs);
        }
        #endregion

        #region Get All Employees
        public async Task<Response<IEnumerable<SelectCustomerDTO>>> GetAllEmployees()
        {
            // Retrieve all users in the "customer" role
            var Employees = _userManager.Users;
            if (Employees == null || !Employees.Any())
                return NotFound<IEnumerable<SelectCustomerDTO>>("No Employees found.");

            // Map Employees to DTOs using LINQ
            var customerDTOs = Employees
                .Select(emp => new SelectCustomerDTO
                {
                    id = emp.Id,
                    fullname = emp.FullName,
                    address = emp.Address,
                    username = emp.UserName,
                    email = emp.Email,
                    phonenumber = emp.PhoneNumber
                })
                .ToList();

            // Return the mapped DTOs
            return Success<IEnumerable<SelectCustomerDTO>>(customerDTOs);
        }
        #endregion

        #region Get Employee By ID
        public async Task<Response<SelectCustomerDTO>> GetCustomerByID(string id)
        {

            // Retrieve customer by ID
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound<SelectCustomerDTO>($"No customer found with ID = {id}");

            // Map user to DTO
            var customerDto = new SelectCustomerDTO
            {
                id = user.Id,
                fullname = user.FullName,
                address = user.Address,
                username = user.UserName,
                email = user.Email,
                phonenumber = user.PhoneNumber
            };

            // Return the mapped DTO
            return Success(customerDto);
        }
        #endregion

        #region Delete Employee By ID
        public async Task<Response<string>> DeleteUser(string id)
        {

            // Retrieve customer by ID
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound<string>($"No customer found with ID = {id}");
            await _userManager.DeleteAsync(user);
            return Deleted<string>("Employee Deleted Successfully");

        }
        #endregion
    }
}
