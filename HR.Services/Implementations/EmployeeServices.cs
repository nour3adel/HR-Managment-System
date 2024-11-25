﻿using AutoMapper;
using HR.Domain.Classes;
using HR.Domain.DTOs.Employee;
using HR.Services.Services;
using Microsoft.AspNetCore.Identity;

namespace HR.Services.Implementations
{
    public class EmployeeServices : IEmployeeServices
    {
        #region Fields

        public UserManager<Employee> _userManager;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public EmployeeServices(UserManager<Employee> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        #endregion

        #region Register User
        public async Task<string> RegisterUser(RegisterUserDTO user)
        {
            // Validate email
            if (await _userManager.FindByEmailAsync(user.Email) != null)
            {
                return "Email is already registered.";
            }

            // Validate username
            if (await _userManager.FindByNameAsync(user.UserName) != null)
            {
                return "Username is already taken.";
            }

            // Map DTO to Employee entity
            Employee newEmployee = _mapper.Map<Employee>(user);

            // Create the user
            IdentityResult creationResult = await _userManager.CreateAsync(newEmployee, user.password);
            if (!creationResult.Succeeded)
            {
                var errors = string.Join(", ", creationResult.Errors.Select(e => e.Description));
                return $"User creation failed: {errors}";
            }

            // Assign role to the user
            var roleResult = await _userManager.AddToRoleAsync(newEmployee, "User");
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return $"Failed to assign role: {errors}";
            }

            return "User registration successful.";
        }

        #endregion
    }
}
