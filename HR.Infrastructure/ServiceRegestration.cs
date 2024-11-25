using HR.Domain.Classes;
using HR.Domain.Classes.Identity;
using HR.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace HR.Infrastructure
{
    public static class ServiceRegestration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services, IConfiguration configuration)
        {

            #region Identity Configurations

            services.AddIdentity<Employee, Role>(option =>
            {
                // Password settings.
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = true;

                // User settings.
                option.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<HRdbContext>().AddDefaultTokenProviders();

            #endregion

            #region Swagger Configurations

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("All", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "HR Managment System",
                    Description = "All Endpoints",

                });
                c.SwaggerDoc("Employees", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Employees",
                    Description = "Employees Web Api in Asp.net Core Version .net8",

                });
                c.SwaggerDoc("Departments", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Departments",
                    Description = "Departments Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("Attendances", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Attendances",
                    Description = "Attendances Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("LeaveRequests", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "LeaveRequests",
                    Description = "LeaveRequests Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("Notifications", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Notifications",
                    Description = "Notifications Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("Payroll", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Payroll",
                    Description = "Payroll Web Api in Asp.net Core Version .net7",

                });
                c.SwaggerDoc("PerformanceReview", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PerformanceReview",
                    Description = "PerformanceReview Web Api in Asp.net Core Version .net7",

                });

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var groupName = apiDesc.GroupName;
                    if (docName == "All")
                        return true;
                    return docName == groupName;
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                }
                c.EnableAnnotations();
            });

            #endregion

            return services;
        }
    }
}
