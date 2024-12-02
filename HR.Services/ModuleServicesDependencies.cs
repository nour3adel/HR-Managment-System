using HR.Services.Implementations;
using HR.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HR.Services
{
    public static class ModuleServicesDependencies
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IAttendanceServices, AttendanceService>();
            services.AddScoped<ILeaveRequestServices, LeaveRequestServices>();
            services.AddScoped<INotificationServices, NotificationServices>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAutherizationServices, AutherizationServices>();
            services.AddScoped<IDepartmentServices, DepartmentServices>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
