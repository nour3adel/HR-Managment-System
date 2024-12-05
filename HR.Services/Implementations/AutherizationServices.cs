using HR.Domain.Classes;
using HR.Domain.Classes.Identity;
using HR.Domain.DTOs.Autherization;
using HR.Domain.Helpers;
using HR.Services.Bases;
using HR.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR.Services.Implementations
{
    public class AutherizationServices : ResponseHandler, IAutherizationServices
    {
        #region Fields

        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JwtSettings _jwtSettings;



        #endregion

        #region Constructor
        public AutherizationServices(UserManager<Employee> userManager, RoleManager<Role> roleManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings;

        }

        #endregion


        #region Add New Role
        public async Task<Response<string>> AddNewRole(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return Created("New Role Added Successfully");
            return BadRequest<string>("Failed");

        }
        #endregion

        #region Edit Role Name
        public async Task<Response<string>> EditRoleName(EditRoleDTO editRoleDTO)
        {
            //check role is exist or not
            var role = await _roleManager.FindByNameAsync(editRoleDTO.OldName);
            if (role == null)
                return NotFound<string>("No Role With This Name");
            role.Name = editRoleDTO.NewName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return Updated<string>("Role Name Edited Successfully");
            var errors = string.Join("-", result.Errors);
            return BadRequest<string>(errors);
        }
        #endregion

        #region Get All Roles
        public async Task<Response<IEnumerable<string>>> GetAllRoles()
        {
            var roleNames = await _roleManager.Roles.Select(role => role.Name).ToListAsync();
            return Success<IEnumerable<string>>(roleNames);
        }
        #endregion

        #region Get Roles For Specific User
        public async Task<Response<IEnumerable<string>>> GetRolesForUser(string userId)
        {
            // Fetch the user by ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest<IEnumerable<string>>("User not found.");
            }

            // Get the roles for the user
            var roleNames = await _userManager.GetRolesAsync(user);

            return Success<IEnumerable<string>>(roleNames);
        }

        #endregion

        #region Remove Role
        public async Task<Response<string>> RemoveRole(string roleName)
        {
            var role = await _roleManager.FindByIdAsync(roleName);
            if (role == null) return NotFound<string>($"No Role With Name = {roleName} is exist");
            //Chech if user has this role or not
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            //return exception 
            if (users != null && users.Count() > 0) return BadRequest<string>("Many Users Has this Roles !!!");
            //delete
            var result = await _roleManager.DeleteAsync(role);
            //success
            if (result.Succeeded) return Deleted<string>("Role Has been Deleted Successfully");
            //problem
            var errors = string.Join("-", result.Errors);
            return BadRequest<string>(errors);
        }
        #endregion
    }
}
