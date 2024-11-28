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
            services.AddScoped<IPayrollServices, PayrollServices>();
            services.AddScoped<ILeaveRequestServices, LeaveRequestServices>();
            services.AddScoped<IPerformanceReviewServices, PerformanceReviewServices>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
