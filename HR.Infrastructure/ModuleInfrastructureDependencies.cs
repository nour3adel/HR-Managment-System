using HR.Infrastructure.Common;
using HR.Infrastructure.Interfaces;
using HR.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
